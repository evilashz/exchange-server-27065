using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000027E0 File Offset: 0x00001BE0
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000027C8 File Offset: 0x00001BC8
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000027B4 File Offset: 0x00001BB4
		public Exception(string message) : base(message)
		{
		}
	}
}
