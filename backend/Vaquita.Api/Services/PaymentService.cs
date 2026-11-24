using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGroupMemberRepository _memberRepository;

    public PaymentService(
        IPaymentRepository repository,
        IGroupRepository groupRepository,
        IUserRepository userRepository,
        IGroupMemberRepository memberRepository)
    {
        _repository = repository;
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _memberRepository = memberRepository;
    }

    public async Task<IEnumerable<Payment>> GetByGroupIdAsync(int groupId)
    {
        var group = await _groupRepository.GetByIdAsync(groupId);

        if (group == null)
            throw new KeyNotFoundException("No se encontró el grupo.");

        return await _repository.GetByGroupIdAsync(groupId);
    }

    public async Task AddAsync(int groupId, Payment payment)
    {
        if (payment.Amount <= 0)
            throw new ArgumentException("El importe debe ser mayor a cero.");

        if (payment.PayerUserId == payment.ReceiverUserId)
            throw new ArgumentException("El pagador y el receptor deben ser distintos.");

        var group = await _groupRepository.GetByIdAsync(groupId);

        if (group == null)
            throw new KeyNotFoundException("No se encontró el grupo.");

        var payer = await _userRepository.GetByIdAsync(payment.PayerUserId);
        var receiver = await _userRepository.GetByIdAsync(payment.ReceiverUserId);

        if (payer == null || receiver == null)
            throw new KeyNotFoundException("No se encontró uno de los usuarios.");

        var payerIsMember = await _memberRepository
            .GetByGroupAndUserAsync(groupId, payment.PayerUserId);

        var receiverIsMember = await _memberRepository
            .GetByGroupAndUserAsync(groupId, payment.ReceiverUserId);

        if (payerIsMember == null || receiverIsMember == null)
            throw new ArgumentException("Ambos usuarios deben pertenecer al grupo.");

        payment.GroupId = groupId;

        if (payment.Date == default)
            payment.Date = DateTime.UtcNow;

        await _repository.AddAsync(payment);
    }
}
