using AutoMapper;
using VentanillaUnica.Tramites.Application.IServices;
using VentanillaUnica.Tramites.Common.Exceptions;
using VentanillaUnica.Tramites.Common.IServices;
using VentanillaUnica.Tramites.Domain.IResources;
using VentanillaUnica.Tramites.Domain.IServices;
using VentanillaUnica.Tramites.Domain.Repositories;
using VentanillaUnica.Tramites.Dtos;

namespace VentanillaUnica.Tramites.Application.Services
{
    public class ParameterAplicationService : IParameterAplicationService
    {
        private readonly IParameterDomainService parameterDomainService;
        private readonly IParameterRepository parameterRepository;
        private readonly IMessagesProvider messagesProvider;
        private readonly IDateTime dateTime;
        private readonly IMapper mapper;
        public ParameterAplicationService(
            IParameterDomainService parameterDomainService,
            IParameterRepository parameterRepository,
            IMessagesProvider messagesProvider,
            IDateTime dateTime,
            IMapper mapper)
        {
            this.parameterDomainService = parameterDomainService;
            this.parameterRepository = parameterRepository;
            this.messagesProvider = messagesProvider;
            this.dateTime = dateTime;
            this.mapper = mapper;
        }

        public async Task<ParameterDto> GetByIdentifierAsync(string identifier)
        {
            if (parameterDomainService.ValidateIdentifier(identifier))
            {
                var message = string.Format(messagesProvider.InvalidParameter, $"{dateTime.Now:dd/MM/yyyy}");
                throw new CustomBusinessException(message);
            }

            return mapper.Map<ParameterDto>(await parameterRepository.GetByIdentifier(identifier));
        }
    }
}
