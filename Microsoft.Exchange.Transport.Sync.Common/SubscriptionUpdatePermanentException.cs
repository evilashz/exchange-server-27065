using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000006 RID: 6
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionUpdatePermanentException : LocalizedException
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00004850 File Offset: 0x00002A50
		public SubscriptionUpdatePermanentException() : base(Strings.SubscriptionUpdatePermanentException)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000485D File Offset: 0x00002A5D
		public SubscriptionUpdatePermanentException(Exception innerException) : base(Strings.SubscriptionUpdatePermanentException, innerException)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000486B File Offset: 0x00002A6B
		protected SubscriptionUpdatePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004875 File Offset: 0x00002A75
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
