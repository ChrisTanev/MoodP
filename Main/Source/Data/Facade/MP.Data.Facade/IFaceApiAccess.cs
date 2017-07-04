using System.Collections.Generic;
using System.Threading.Tasks;

namespace MP.Data.Facade
{
  public  interface IFaceApiAccess
  {
      Task<List<MoodResult>> Post(byte[] pic);


  }
}
