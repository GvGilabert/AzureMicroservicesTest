using AutoMapper;

namespace AzureMicroservicesTest.Api.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order, Models.Order>().ForMember(x => x.Total, opt => opt.Ignore());
            CreateMap<Db.OrderItem, Models.OrderItem>();
        }
    }
}
