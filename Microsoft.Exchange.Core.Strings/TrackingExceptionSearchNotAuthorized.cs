using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000023 RID: 35
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TrackingExceptionSearchNotAuthorized : LocalizedException
	{
		// Token: 0x060003CD RID: 973 RVA: 0x0000D38D File Offset: 0x0000B58D
		public TrackingExceptionSearchNotAuthorized() : base(CoreStrings.TrackingSearchNotAuthorized)
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000D39A File Offset: 0x0000B59A
		public TrackingExceptionSearchNotAuthorized(Exception innerException) : base(CoreStrings.TrackingSearchNotAuthorized, innerException)
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		protected TrackingExceptionSearchNotAuthorized(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000D3B2 File Offset: 0x0000B5B2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
