using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServiceFramework
{
    public interface IDataTransform
    {
        void TransformResults(string subJobId);
    }
}
