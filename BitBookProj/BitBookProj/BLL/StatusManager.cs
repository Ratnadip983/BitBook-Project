using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookProj.DAL;
using BitBookProj.Models;

namespace BitBookProj.BLL
{
    public class StatusManager
    {
        StatusGateway _statusGateway=new StatusGateway();
        public bool Add(Status status)
        {
            return _statusGateway.Add(status);
        }

        public List<Status> GetStatusById(int id)
        {
            return _statusGateway.GetStatusById(id);
        }
    }
}