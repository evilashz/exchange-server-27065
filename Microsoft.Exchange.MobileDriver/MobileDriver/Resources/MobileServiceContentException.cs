using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x0200005B RID: 91
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileServiceContentException : MobileServicePermanentException
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000CB74 File Offset: 0x0000AD74
		public MobileServiceContentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000CB7D File Offset: 0x0000AD7D
		public MobileServiceContentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000CB87 File Offset: 0x0000AD87
		protected MobileServiceContentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000CB91 File Offset: 0x0000AD91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
