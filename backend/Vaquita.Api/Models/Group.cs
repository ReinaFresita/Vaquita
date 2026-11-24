namespace Vaquita.Api.Models;

public class Group {
    public int Id {get; set;}

    public string Description {get; set;} = string.Empty;

    public ICollection<GroupMember> GroupMembers {get; set;} = new List<GroupMember>();

    public ICollection<Expense> Expenses {get; set;} = new List<Expense>();

    public ICollection<Payment> Payments {get; set;} = new List<Payment>();
}
