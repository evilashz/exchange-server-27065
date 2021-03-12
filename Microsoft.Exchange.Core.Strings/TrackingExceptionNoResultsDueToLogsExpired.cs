using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200001F RID: 31
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TrackingExceptionNoResultsDueToLogsExpired : LocalizedException
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0000D2D1 File Offset: 0x0000B4D1
		public TrackingExceptionNoResultsDueToLogsExpired() : base(CoreStrings.TrackingWarningNoResultsDueToLogsExpired)
		{
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000D2DE File Offset: 0x0000B4DE
		public TrackingExceptionNoResultsDueToLogsExpired(Exception innerException) : base(CoreStrings.TrackingWarningNoResultsDueToLogsExpired, innerException)
		{
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000D2EC File Offset: 0x0000B4EC
		protected TrackingExceptionNoResultsDueToLogsExpired(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000D2F6 File Offset: 0x0000B4F6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
