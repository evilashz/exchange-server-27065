using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x0200005F RID: 95
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileServiceNetworkException : MobileServiceTransientException
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000CC10 File Offset: 0x0000AE10
		public MobileServiceNetworkException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000CC19 File Offset: 0x0000AE19
		public MobileServiceNetworkException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000CC23 File Offset: 0x0000AE23
		protected MobileServiceNetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000CC2D File Offset: 0x0000AE2D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
