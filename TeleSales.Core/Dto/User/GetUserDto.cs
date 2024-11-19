﻿namespace TeleSales.Core.Dto.User;

public class GetUserDto
{
    public long id { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}