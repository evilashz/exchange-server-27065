using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000058 RID: 88
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileServiceTransientException : MobileTransientException
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000CAFF File Offset: 0x0000ACFF
		public MobileServiceTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000CB08 File Offset: 0x0000AD08
		public MobileServiceTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000CB12 File Offset: 0x0000AD12
		protected MobileServiceTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
