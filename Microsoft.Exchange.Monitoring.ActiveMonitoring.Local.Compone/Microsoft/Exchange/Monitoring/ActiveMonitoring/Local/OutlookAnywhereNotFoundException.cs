using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A6 RID: 1446
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OutlookAnywhereNotFoundException : LocalizedException
	{
		// Token: 0x060026D7 RID: 9943 RVA: 0x000DE0A9 File Offset: 0x000DC2A9
		public OutlookAnywhereNotFoundException() : base(Strings.RcaDiscoveryOutlookAnywhereNotFound)
		{
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000DE0B6 File Offset: 0x000DC2B6
		public OutlookAnywhereNotFoundException(Exception innerException) : base(Strings.RcaDiscoveryOutlookAnywhereNotFound, innerException)
		{
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000DE0C4 File Offset: 0x000DC2C4
		protected OutlookAnywhereNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000DE0CE File Offset: 0x000DC2CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
