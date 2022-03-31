using DevTest.ViewModels.Dtos;
using LTHL.VIEW_MODELS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTest.Services.UserManagement
{
    public interface IUserManagement
    {
        Response<List<PersonDetailDtoModal>> GetPeople(int organizationId);
        bool ClaimSchedule(int organizationId);





    }
}
