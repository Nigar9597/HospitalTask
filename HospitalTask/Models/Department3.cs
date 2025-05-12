namespace HospitalTask.Models
{
    public class Department3:BaseModel
    {
        public string? Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Doctor3>? Doctor3s { get; set; }
    }
}
