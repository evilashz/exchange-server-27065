using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000022 RID: 34
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TrackingExceptionNoResultsDueToUntrackableMessagePath : LocalizedException
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0000D35E File Offset: 0x0000B55E
		public TrackingExceptionNoResultsDueToUntrackableMessagePath() : base(CoreStrings.TrackingWarningNoResultsDueToUntrackableMessagePath)
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000D36B File Offset: 0x0000B56B
		public TrackingExceptionNoResultsDueToUntrackableMessagePath(Exception innerException) : base(CoreStrings.TrackingWarningNoResultsDueToUntrackableMessagePath, innerException)
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000D379 File Offset: 0x0000B579
		protected TrackingExceptionNoResultsDueToUntrackableMessagePath(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000D383 File Offset: 0x0000B583
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
