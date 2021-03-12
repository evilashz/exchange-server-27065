using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000066 RID: 102
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverObjectNotFoundException : MobilePermanentException
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0000CD21 File Offset: 0x0000AF21
		public MobileDriverObjectNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000CD2A File Offset: 0x0000AF2A
		public MobileDriverObjectNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000CD34 File Offset: 0x0000AF34
		protected MobileDriverObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000CD3E File Offset: 0x0000AF3E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
