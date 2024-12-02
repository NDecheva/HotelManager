using HotelManager.Shared.Attributes;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Services
{
    [AutoBind]
    public class ClientService : BaseCrudService<ClientDto, IClientRepository>, IClientService
    {
        public ClientService(IClientRepository repository) : base(repository)
        {

        }
    }
}
