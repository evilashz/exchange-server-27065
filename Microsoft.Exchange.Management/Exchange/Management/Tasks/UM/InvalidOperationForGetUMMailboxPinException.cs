using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011FF RID: 4607
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidOperationForGetUMMailboxPinException : LocalizedException
	{
		// Token: 0x0600B9D2 RID: 47570 RVA: 0x002A673C File Offset: 0x002A493C
		public InvalidOperationForGetUMMailboxPinException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B9D3 RID: 47571 RVA: 0x002A6745 File Offset: 0x002A4945
		public InvalidOperationForGetUMMailboxPinException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B9D4 RID: 47572 RVA: 0x002A674F File Offset: 0x002A494F
		protected InvalidOperationForGetUMMailboxPinException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B9D5 RID: 47573 RVA: 0x002A6759 File Offset: 0x002A4959
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
