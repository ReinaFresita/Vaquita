namespace Vaquita.Api.Models;

public class Payment {
    public int Id {get; set;}

    public decimal Amount {get; set;}

    public DateTime Date {get; set;}

    public int GroupId {get; set;}

    public int PayerUserId {get; set;}

    public int ReceiverUserId {get; set;}

    public Group? Group {get; set;}

    public User? PayerUser {get; set;}

    public User? ReceiverUser {get; set;}
}
