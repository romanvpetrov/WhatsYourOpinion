using Core.Entities;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOpinionRepository
    {
        public Result Add(Opinion opinion);
        public Result Remove(int opinionId);
        public Result<Opinion> Get(int opinionId);
        public Result<Opinion> GetRandomOpinion(string topicName);
    }
}
