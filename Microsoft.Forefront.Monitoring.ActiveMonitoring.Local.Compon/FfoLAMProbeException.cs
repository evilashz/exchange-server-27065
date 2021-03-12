using System;
using System.Runtime.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class FfoLAMProbeException : Exception
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000090B0 File Offset: 0x000072B0
		public FfoLAMProbeException()
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000090B8 File Offset: 0x000072B8
		public FfoLAMProbeException(string message) : base(message)
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000090C1 File Offset: 0x000072C1
		public FfoLAMProbeException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000090CB File Offset: 0x000072CB
		protected FfoLAMProbeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
