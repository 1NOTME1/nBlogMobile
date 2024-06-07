using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AppMobilenBlog.Helpers
{
    public static class ResponseHelper
    {
        public async static Task<bool> HandleRequest(this Task serviceMethod)
        {
            try
            {
                await serviceMethod;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
