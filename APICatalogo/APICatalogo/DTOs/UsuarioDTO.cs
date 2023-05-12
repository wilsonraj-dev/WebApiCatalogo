using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;

public class UsuarioDTO
{
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
}
