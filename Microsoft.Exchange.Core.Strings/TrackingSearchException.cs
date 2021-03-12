using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200001D RID: 29
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TrackingSearchException : LocalizedException
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0000D225 File Offset: 0x0000B425
		public TrackingSearchException(LocalizedString reason) : base(CoreStrings.TrackingSearchException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000D23A File Offset: 0x0000B43A
		public TrackingSearchException(LocalizedString reason, Exception innerException) : base(CoreStrings.TrackingSearchException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000D250 File Offset: 0x0000B450
		protected TrackingSearchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (LocalizedString)info.GetValue("reason", typeof(LocalizedString));
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000D27A File Offset: 0x0000B47A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000D29A File Offset: 0x0000B49A
		public LocalizedString Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000367 RID: 871
		private readonly LocalizedString reason;
	}
}
