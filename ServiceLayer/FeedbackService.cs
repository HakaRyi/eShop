using BOs.Entities;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public void DeleteFeedback(int id) => _feedbackRepository.DeleteFeedback(id);

        public List<Feedbacks> GetAllFeedbacks() => _feedbackRepository.GetAllFeedbacks();

        public Feedbacks GetFeedback(int id) => _feedbackRepository.GetFeedback(id);
    }
}
