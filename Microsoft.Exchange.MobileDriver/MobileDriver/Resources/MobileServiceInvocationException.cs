using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x0200005D RID: 93
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileServiceInvocationException : MobileServicePermanentException
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000CBC2 File Offset: 0x0000ADC2
		public MobileServiceInvocationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000CBCB File Offset: 0x0000ADCB
		public MobileServiceInvocationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000CBD5 File Offset: 0x0000ADD5
		protected MobileServiceInvocationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000CBDF File Offset: 0x0000ADDF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
