using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dto;

public partial class AgendaDto
{
    public int? id { get; set; }
    public string title { get; set; }
    public string start { get; set; }
    public string end { get; set; }
 
}
