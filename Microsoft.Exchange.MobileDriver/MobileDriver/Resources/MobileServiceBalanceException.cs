using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x0200005E RID: 94
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileServiceBalanceException : MobileServiceTransientException
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
		public MobileServiceBalanceException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		public MobileServiceBalanceException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		protected MobileServiceBalanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000CC06 File Offset: 0x0000AE06
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
