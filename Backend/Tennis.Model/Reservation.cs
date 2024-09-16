using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Model;

public class Reservation
{
    public int? Id { get; set; }
    public DateTime ReservationDate { get; set; }
    public int CourtId { get; set; }
    public Guid UserId { get; set; }
}
