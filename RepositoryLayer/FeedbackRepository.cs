using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly EShopContext _context;
        public FeedbackRepository(EShopContext context)
        {
            _context = context;
        }

        public void DeleteFeedback(int id)
        {
            var feedback = _context.Feedbacks.Find(id);
            if (feedback == null) return;
            _context.Feedbacks.Remove(feedback);
            _context.SaveChanges();
        }

        public List<Feedbacks> GetAllFeedbacks() => _context.Feedbacks.Include(f => f.Member).Include(f => f.Product).ToList();

        public Feedbacks GetFeedback(int id) => _context.Feedbacks.Include(f => f.Member).Include(f => f.Product).FirstOrDefault(f => f.Id == id);

    }
}
