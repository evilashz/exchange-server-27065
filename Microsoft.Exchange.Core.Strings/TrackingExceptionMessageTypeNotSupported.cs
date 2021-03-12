using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000021 RID: 33
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TrackingExceptionMessageTypeNotSupported : LocalizedException
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000D32F File Offset: 0x0000B52F
		public TrackingExceptionMessageTypeNotSupported() : base(CoreStrings.TrackingMessageTypeNotSupported)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000D33C File Offset: 0x0000B53C
		public TrackingExceptionMessageTypeNotSupported(Exception innerException) : base(CoreStrings.TrackingMessageTypeNotSupported, innerException)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000D34A File Offset: 0x0000B54A
		protected TrackingExceptionMessageTypeNotSupported(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000D354 File Offset: 0x0000B554
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
