using AutoMapper;
using Hlyt.Domain.Commands.Security;
using Hlyt.Web.ViewModels;


namespace Hlyt.Web.Mappers
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<CategoryFormModel, CreateOrUpdateCategoryCommand>();
            //Mapper.CreateMap<ExpenseFormModel, CreateOrUpdateExpenseCommand>();
            Mapper.CreateMap<UserFormModel, UserRegisterCommand>();
        }
    }
}
