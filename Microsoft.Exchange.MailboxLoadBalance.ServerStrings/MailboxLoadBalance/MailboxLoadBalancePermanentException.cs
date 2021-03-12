using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000006 RID: 6
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxLoadBalancePermanentException : AnchorPermanentException
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000255F File Offset: 0x0000075F
		public MailboxLoadBalancePermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002568 File Offset: 0x00000768
		public MailboxLoadBalancePermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002572 File Offset: 0x00000772
		protected MailboxLoadBalancePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000257C File Offset: 0x0000077C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
