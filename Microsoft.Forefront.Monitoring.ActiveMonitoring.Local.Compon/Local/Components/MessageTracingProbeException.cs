using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Local.Components
{
	// Token: 0x020002A8 RID: 680
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MessageTracingProbeException : LocalizedException
	{
		// Token: 0x0600168A RID: 5770 RVA: 0x00048AC7 File Offset: 0x00046CC7
		public MessageTracingProbeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00048AD0 File Offset: 0x00046CD0
		public MessageTracingProbeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00048ADA File Offset: 0x00046CDA
		protected MessageTracingProbeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00048AE4 File Offset: 0x00046CE4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
