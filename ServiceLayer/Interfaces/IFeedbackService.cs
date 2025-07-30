using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IFeedbackService
    {
        List<Feedbacks> GetAllFeedbacks();
        Feedbacks GetFeedback(int id);
        void DeleteFeedback(int id);
    }
}
