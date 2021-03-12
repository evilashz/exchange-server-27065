using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000064 RID: 100
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverStateException : MobilePermanentException
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000CCD3 File Offset: 0x0000AED3
		public MobileDriverStateException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		public MobileDriverStateException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000CCE6 File Offset: 0x0000AEE6
		protected MobileDriverStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
