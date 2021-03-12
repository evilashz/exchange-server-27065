using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000411 RID: 1041
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x0005AC64 File Offset: 0x0005A064
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0005AC4C File Offset: 0x0005A04C
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0005AC38 File Offset: 0x0005A038
		public Exception(string message) : base(message)
		{
		}
	}
}
