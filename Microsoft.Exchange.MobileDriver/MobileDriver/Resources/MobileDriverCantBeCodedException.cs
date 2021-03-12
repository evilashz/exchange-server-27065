using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000062 RID: 98
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverCantBeCodedException : MobilePermanentException
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000CC85 File Offset: 0x0000AE85
		public MobileDriverCantBeCodedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000CC8E File Offset: 0x0000AE8E
		public MobileDriverCantBeCodedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000CC98 File Offset: 0x0000AE98
		protected MobileDriverCantBeCodedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000CCA2 File Offset: 0x0000AEA2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
