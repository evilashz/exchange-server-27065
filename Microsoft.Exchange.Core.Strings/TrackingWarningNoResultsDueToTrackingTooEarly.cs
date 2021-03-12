using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200001E RID: 30
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TrackingWarningNoResultsDueToTrackingTooEarly : LocalizedException
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x0000D2A2 File Offset: 0x0000B4A2
		public TrackingWarningNoResultsDueToTrackingTooEarly() : base(CoreStrings.TrackingWarningNoResultsDueToTrackingTooEarly)
		{
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000D2AF File Offset: 0x0000B4AF
		public TrackingWarningNoResultsDueToTrackingTooEarly(Exception innerException) : base(CoreStrings.TrackingWarningNoResultsDueToTrackingTooEarly, innerException)
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000D2BD File Offset: 0x0000B4BD
		protected TrackingWarningNoResultsDueToTrackingTooEarly(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000D2C7 File Offset: 0x0000B4C7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
