namespace Vaquita.Api.Models;

public class Expense {
  public int Id {get; set;}

  public string Description {get; set;} = string.Empty;

  public decimal Amount {get; set;}

  public DateTime Date {get; set;}

  public string? Category {get; set;}

  public int RegisteredByUserId {get; set;}

  public int PaidByUserId {get; set;}

  public int GroupId {get; set;}

  public User? RegisteredByUser {get; set;}

  public User? PaidByUser {get; set;}

  public Group? Group {get; set;}
}
