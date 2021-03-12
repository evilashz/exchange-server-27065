using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000060 RID: 96
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverPermanentException : MobilePermanentException
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public MobileDriverPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000CC40 File Offset: 0x0000AE40
		public MobileDriverPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000CC4A File Offset: 0x0000AE4A
		protected MobileDriverPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000CC54 File Offset: 0x0000AE54
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
