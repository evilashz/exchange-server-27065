using System;
using System.Runtime.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class ForwardSyncProbeException : Exception
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00009C24 File Offset: 0x00007E24
		public ForwardSyncProbeException()
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009C2C File Offset: 0x00007E2C
		public ForwardSyncProbeException(string message) : base(message)
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00009C35 File Offset: 0x00007E35
		public ForwardSyncProbeException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009C3F File Offset: 0x00007E3F
		protected ForwardSyncProbeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
