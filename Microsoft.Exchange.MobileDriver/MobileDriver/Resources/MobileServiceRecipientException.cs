using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x0200005A RID: 90
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileServiceRecipientException : MobileServicePermanentException
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000CB4D File Offset: 0x0000AD4D
		public MobileServiceRecipientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CB56 File Offset: 0x0000AD56
		public MobileServiceRecipientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CB60 File Offset: 0x0000AD60
		protected MobileServiceRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
