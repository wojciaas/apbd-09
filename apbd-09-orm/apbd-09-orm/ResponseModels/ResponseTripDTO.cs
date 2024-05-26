using apbd_09_orm.Entities;

namespace apbd_09_orm.ResponseModels;

public record ResponseTripDTO(
    string Name,
    string Description,
    DateTime DateFrom,
    DateTime DateTo,
    int MaxPeople,
    IEnumerable<ResponseCountryDTO> Countries,
    IEnumerable<ResponseClientDTO> Clients
    );