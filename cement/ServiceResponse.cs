using System;
using System.Collections.Generic;
using System.Text;

namespace cement;

public class ServiceResponse<T> 
{
    public T Data { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;
    public bool Success { get; set; } = false;
}
