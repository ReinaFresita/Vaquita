namespace Vaquita.Api.Models;

public class User{
    public int Id {get; set;}

    public string Name {get; set;} = string.Empty;

    public string Surname {get; set;} = string.Empty;

    public string Nickname {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<GroupMember> GroupMembers {get; set;} = new List<GroupMember>();

    public ICollection<Expense> RegisteredExpenses {get; set;} = new List<Expense>();

    public ICollection<Expense> PaidExpenses {get; set;} = new List<Expense>();

    public ICollection<Payment> SentPayments {get; set;} = new List<Payment>();

    public ICollection<Payment> ReceivedPayments {get; set;} = new List<Payment>();
}
