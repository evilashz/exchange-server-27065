using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200003E RID: 62
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SiteMonitorException : TopologyServiceTransientException
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		public SiteMonitorException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000F0F9 File Offset: 0x0000D2F9
		public SiteMonitorException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000F103 File Offset: 0x0000D303
		protected SiteMonitorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000F10D File Offset: 0x0000D30D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
