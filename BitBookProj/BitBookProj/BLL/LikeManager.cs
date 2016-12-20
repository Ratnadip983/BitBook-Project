using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookProj.DAL;
using BitBookProj.Models;

namespace BitBookProj.BLL
{
    public class LikeManager
    {
        LikeGateway _likeGateway=new LikeGateway();

        public bool Add(Like like)
        {
            return _likeGateway.Add(like);
        }

        public bool Delete(Like like)
        {
            return _likeGateway.Delete(like);
        }

        public int LikeCount(int statusId)
        {
            return LikeCount(statusId);
        }
    }
}