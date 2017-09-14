using System;
using System.Collections.Generic;

namespace MyWallet.WebApi.Models
{
    public class ErrorReport
    {
        public string Category { get; set; }
        public ICollection<ErrorItem> ErrorItemCollection { get; set; }

        public static ICollection<ErrorItem> GetExceptionList(int errorCode, Exception exception)
        {
            var exList = new List<ErrorItem>();
            var hasInner = false;
            var actualException = exception;
            do
            {
                exList.Add(new  ErrorItem
                {
                    Description = actualException.Message,
                    ErrorCode = errorCode
                });

                if (actualException.InnerException == null) continue;

                hasInner = true;
                actualException = actualException.InnerException;

            } while (hasInner);
            return exList;
        }
    }
    public class ErrorItem
    {
        public string Description { get; set; }
        public int ErrorCode { get; set; }
    }

}