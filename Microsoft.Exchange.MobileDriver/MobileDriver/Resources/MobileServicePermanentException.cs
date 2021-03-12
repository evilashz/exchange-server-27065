using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000057 RID: 87
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileServicePermanentException : MobilePermanentException
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		public MobileServicePermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000CAE1 File Offset: 0x0000ACE1
		public MobileServicePermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000CAEB File Offset: 0x0000ACEB
		protected MobileServicePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000CAF5 File Offset: 0x0000ACF5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
