using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x0200005C RID: 92
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileServiceCapabilityException : MobileServicePermanentException
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000CB9B File Offset: 0x0000AD9B
		public MobileServiceCapabilityException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public MobileServiceCapabilityException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		protected MobileServiceCapabilityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
