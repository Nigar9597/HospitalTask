using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalTask.Areas.ViewModels.Department3VMs
{
    public class BatchAssignVM
    { 
        public int Department3Id { get; set; }
        public List<SelectListItem> AllDoctor3s { get; set; }

        public List<int> Doctor3Ids { get; set; }
    }
}
