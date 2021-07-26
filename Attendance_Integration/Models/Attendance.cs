namespace Attendance_Integration.Models
{
    public class Attendance
    {
        public string staff_id { get; set; }
        public string shift_id { get; set; }
        public string Date { get; set; }
        public string check_in { get; set; }
        public string check_out { get; set; }
    }
}