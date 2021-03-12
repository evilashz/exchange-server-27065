using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000A2 RID: 162
	[ComVisible(true)]
	[Serializable]
	public class AppDomainUnloadedException : SystemException
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x0001E8F1 File Offset: 0x0001CAF1
		public AppDomainUnloadedException() : base(Environment.GetResourceString("Arg_AppDomainUnloadedException"))
		{
			base.SetErrorCode(-2146234348);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001E90E File Offset: 0x0001CB0E
		public AppDomainUnloadedException(string message) : base(message)
		{
			base.SetErrorCode(-2146234348);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001E922 File Offset: 0x0001CB22
		public AppDomainUnloadedException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146234348);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001E937 File Offset: 0x0001CB37
		protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
