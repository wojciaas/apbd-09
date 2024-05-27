using System.ComponentModel.DataAnnotations;

namespace apbd_09_orm.RequestModels;

public record RequestClientTripDTO(
    [Required]
    string FirstName,
    [Required]
    string LastName,
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    [Phone]
    string Telephone,
    [Required]
    [RegularExpression(@"\d{11}")]
    string Pesel,
    [Required]
    int IdTrip,
    [Required]
    string TripName,
    [DataType(DataType.Date)]
    DateTime? PaymentDate
    );