using System;

namespace SmartphonesApi.App.Constants.CustomExceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() : base(CustomErrorCodes.RecordNotFound)
        {
        }
    }
}