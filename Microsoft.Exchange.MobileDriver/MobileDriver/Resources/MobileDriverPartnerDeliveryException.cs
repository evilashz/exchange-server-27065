using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000068 RID: 104
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverPartnerDeliveryException : MobilePermanentException
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000CD6F File Offset: 0x0000AF6F
		public MobileDriverPartnerDeliveryException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public MobileDriverPartnerDeliveryException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000CD82 File Offset: 0x0000AF82
		protected MobileDriverPartnerDeliveryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
