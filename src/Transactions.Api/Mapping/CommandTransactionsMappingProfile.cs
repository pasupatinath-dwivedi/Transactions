using AutoMapper;
using Tansactions.Api.Model;
using Transactions.Api.Command;

namespace Transactions.Api.Mapping
{
    public class CommandTransactionsMappingProfile : Profile
    {
        public CommandTransactionsMappingProfile()
        {
            AllowNullCollections = true;

            CreateMap<CreateTransactionCommand, Transaction>();
            CreateMap<UpdateTransactionCommand, Transaction>();
        }
    }
}
