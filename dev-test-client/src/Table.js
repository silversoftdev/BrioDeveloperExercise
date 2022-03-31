import React, { useState, useEffect } from "react";
import axios from "axios";
import {
  Card,
  Typography,
  Table,
  Menu,
  Layout,
  Spin,
  Input,
  Dropdown,
  Modal,
  message,
  Tooltip,
  Button,
  DatePicker,
  Row,
  Col,
  Descriptions,
  Select,
} from "antd";
const { Header } = Layout;
const { Option } = Select;
export default function TableData(props) {
  const [data, setData] = useState([]);
  const [selectedOrg, setSelectedOrg] = useState("");
  const [loading, setLoading] = useState(false)
  useEffect(() => {}, []);

  const columns = [
    {
      title: "First Name",
      dataIndex: "firstName",
    },
    {
        title: "Middle Name",
        dataIndex: "middle",
      },
    {
      title: "LastName",
      dataIndex: "lastName",
    },
    {
        title: "Total Claim",
        dataIndex: "claims",
        render:(data)=>{
            return( <p>{data.length}</p>)
        }
      },
      {
        title: "Total Phones",
        dataIndex: "phones",
        render:(data)=>{
            debugger
         return( <p>{data.length}</p>)
        }
      },
   
  ];
  const handleSearch=()=>{
      if(selectedOrg)
      {  setLoading(true)
          axios.get(`https://localhost:44381/api/UserManagement/getPersons/${selectedOrg}`,)
          .then(res=>{
              debugger;
            setData(res.data.data);
            setLoading(false)
          }).catch(err=>{
            setLoading(false)
          })
      }
  }
  return (
    <Layout className='site-layout bg-white'>
      <Header className="d">Header</Header>
      <Layout style={{ marginTop: 20 }}>
        <Row>
          <Col span={3} offset={2}>
            <Select
              showSearch
              placeholder="Select a Organization"
              optionFilterProp="children"
              onChange={(e,event) => {
                  debugger;
                  setSelectedOrg(event.value)
              }}
              filterOption={(input, option) =>
                option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
              }
            >
              <Option value="1">Organization1</Option>
              <Option value="2">Organization2</Option>
              <Option value="3">Organization3</Option>
              <Option value="4">Organization4</Option>
            </Select>

          </Col>
          <Col span={4} offset={1}>
              <Button type="primary" onClick={()=>handleSearch()}>Search</Button>
          </Col>
        </Row>
        <Row style={{marginTop:10}}>
            <Col lg={24} md={24}>
        <Table
        loading={loading}
          bordered
          dataSource={data}
          scroll={{ x: 600 }}
          columns={columns}
        />
        </Col>
        </Row>
      </Layout>
    </Layout>
  );
}
