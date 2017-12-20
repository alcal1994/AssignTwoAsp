using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public interface IAthletesRepository
    { 
        //used for Unit Testing with mock athlete manager table data
        IQueryable<Athlete> Athletes {get; }
        IQueryable<Sport> Sports { get;  }
        Athlete Save(Athlete athlete);
        void Delete(Athlete athlete);
    }
}
