using DevTest.DbModel;
using DevTest.ViewModels.Dtos;
using LTHL.VIEW_MODELS.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevTest.Services.UserManagement
{
    public class UserManagement : IUserManagement
    {
        private readonly DevTestContext _context;
        public UserManagement(DevTestContext context)
        {
            _context = context;
        }

        public bool ClaimSchedule(int organizationId)
        {
            try
            {
                var organization = _context.Organizations.Include(x => x.People).Include(x => x.Claims).Where(x => x.Id == organizationId).ToList();
                foreach (var org in organization)
                {
                    TestDataDevOrg01 test = new TestDataDevOrg01();

                    test.Column0 = org.OrganizationId;

                    var count = 1;
                    foreach (var item in org.People)
                    {
                        if (count > 81)
                        {
                            break;
                        }
                        test.GetType().GetProperty("Column" + count).SetValue(test, item.FirstName, null);
                        test.GetType().GetProperty("Column" + (count + 1)).SetValue(test, item.Middle, null);
                        test.GetType().GetProperty("Column" + (count + 2)).SetValue(test, item.LastName, null);
                        count = count + 3;

                    }

                    var sql = @"INSERT INTO TestData_DevOrg_01 ([Column 0]
           ,[Column 1]
           ,[Column 2]
           ,[Column 3]
           ,[Column 4]
           ,[Column 5]
           ,[Column 6]
           ,[Column 7]
           ,[Column 8]
           ,[Column 9]
           ,[Column 10]
           ,[Column 11]
           ,[Column 12]
           ,[Column 13]
           ,[Column 14]
           ,[Column 15]
           ,[Column 16]
           ,[Column 17]
           ,[Column 18]
           ,[Column 19]
           ,[Column 20]
           ,[Column 21]
           ,[Column 22]
           ,[Column 23]
           ,[Column 24]
           ,[Column 25]
           ,[Column 26]
           ,[Column 27]
           ,[Column 28]
           ,[Column 29]
           ,[Column 30]
           ,[Column 31]
           ,[Column 32]
           ,[Column 33]
           ,[Column 34]
           ,[Column 35]
           ,[Column 36]
           ,[Column 37]
           ,[Column 38]
           ,[Column 39]
           ,[Column 40]
           ,[Column 41]
           ,[Column 42]
 ,[Column 43]
           ,[Column 44]
           ,[Column 45],[Column 46]) Values  ('" + test.Column0 + "','" + test.Column0 + "','" + test.Column1 + "','" + test.Column2 + "','" + test.Column3 + "','" + test.Column4 + "','" + test.Column5 + "','" + test.Column6 + "','" + test.Column7 + "','" + test.Column8 + "','" + test.Column9 + "','" + test.Column10 + "','" + test.Column11 + "','" + test.Column12 + "','" + test.Column13 + "','" + test.Column14 + "','" + test.Column15 + "','" + test.Column16 + "','" + test.Column17 + "','" + test.Column18 + "','" + test.Column19 + "','" + test.Column20 + "','" + test.Column21 + "','" + test.Column22 + "','" + test.Column23 + "','" + test.Column24 + "','" + test.Column25 + "','" + test.Column26 + "','" + test.Column27 + "','" + test.Column28 + "','" + test.Column29 + "','" + test.Column30 + "','" + test.Column31 + "','" + test.Column32 + "','" + test.Column33 + "','" + test.Column34 + "','" + test.Column36 + "','" + test.Column35 + "','" + test.Column37 + "','" + test.Column38 + "','" + test.Column39 + "','" + test.Column40 + "','" + test.Column41 + "','" + test.Column42 + "','" + test.Column43 + "','" + test.Column44 + "','" + test.Column1 + "')";

                    _context.Database.ExecuteSqlRaw(sql);

                    var d = _context.SaveChanges();

                    #region WRIGHT LOG FILE
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "JobRun_"+ DateTime.Now.ToString(),
                  org.Id.ToString() +" , "+ org.OrganizationId.ToString() + " , " + org.PersonEmails.ToString() + " , " + org.PersonPhones.ToString());
                    #endregion

                    return true;
                }

            }
            catch (Exception ex)
            {

                return false;
            }


            return false;
        }

        //public async Task<Response<PersonDetailDtoModal>> GetPeople(int organizationId)
        //{
        //    var response = new Response<PersonDetailDtoModal>();
        //    try
        //    {
        //        var zpeopel = _context.People.AsQueryable();
        //        var people = zpeopel.Where(x => x.OrganizationId == organizationId).Select(x => new PersonDetailDtoModal()
        //        {
        //            FirstName = x.FirstName,
        //            Middle = x.Middle,
        //            LastName = x.LastName,
        //            phones = _context.PersonPhones.Where(y => y.ExternalPersonId == x.Id).Select(z => new PersonPhoneDto()
        //            {
        //                phone = z.Phone
        //            }).ToList(),
        //            claims = _context.Claims.Where(c => c.ExternalPersonId == x.Id).Select(d => new PersonClaimsDto()
        //            {
        //                ExternalClaimId = d.ExternalClaimId,
        //                ServiceAddress = d.ServiceAddress,
        //                ServiceNumber = d.ServiceNumber,
        //                ServiceName = d.ServiceName,
        //                ServiceCity = d.ServiceCity,
        //                ServiceState = d.ServiceState,

        //            }).ToList()

        //        }).ToList();
        //        response.IsError = false;
        //        response.Message = "Successfully Retrieved People";
        //        response.List = people;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsError = true;
        //        response.Message = ex.Message.ToString();
        //    }


        //    return response;
        //}

        public Response<List<PersonDetailDtoModal>> GetPeople(int organizationId)
        {
            var response = new Response<List<PersonDetailDtoModal>>();
            try
            {
                List<PersonDetailDtoModal> respList = new List<PersonDetailDtoModal>();
                var people = _context.People.Where(x => x.OrganizationId == organizationId).Select(x => new PersonDetailDtoModal()
                {
                    Id = x.Id,
                    ExternalPersonId = x.ExternalPersonId,
                    OrganizationId = x.OrganizationId,
                    FirstName = x.FirstName,
                    Middle = x.Middle,
                    LastName = x.LastName,

                }).ToList();
                foreach (var person in people)
                {
                    List<PersonPhoneDto> phones = new List<PersonPhoneDto>();
                    List<PersonClaimsDto> claims = new List<PersonClaimsDto>();
                    phones = _context.PersonPhones.Where(y => y.ExternalPersonId == person.Id).Select(z => new PersonPhoneDto()
                    {
                        phone = z.Phone
                    }).ToList();
                    claims = _context.Claims.Where(c => c.ExternalPersonId == person.Id).Select(d => new PersonClaimsDto()
                    {
                        ExternalClaimId = d.ExternalClaimId,
                        ServiceAddress = d.ServiceAddress,
                        ServiceNumber = d.ServiceNumber,
                        ServiceName = d.ServiceName,
                        ServiceCity = d.ServiceCity,
                        ServiceState = d.ServiceState,

                    }).ToList();
                    var per = person;
                    per.claims = claims;
                    per.phones = phones;
                    respList.Add(per);
                }
                response.IsError = false;
                response.Message = "Successfully Retrieved People";
                response.Data = respList;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = ex.Message.ToString();
            }


            return response;
        }


    }
}
