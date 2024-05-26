using System.Runtime.Serialization;

namespace apbd_09_orm.Exceptions;

public class DomainException(string message) : Exception(message);