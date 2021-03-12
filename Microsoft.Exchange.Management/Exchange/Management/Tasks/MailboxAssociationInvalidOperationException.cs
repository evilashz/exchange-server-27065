using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200119E RID: 4510
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxAssociationInvalidOperationException : RecipientTaskException
	{
		// Token: 0x0600B708 RID: 46856 RVA: 0x002A0D07 File Offset: 0x0029EF07
		public MailboxAssociationInvalidOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B709 RID: 46857 RVA: 0x002A0D10 File Offset: 0x0029EF10
		public MailboxAssociationInvalidOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B70A RID: 46858 RVA: 0x002A0D1A File Offset: 0x0029EF1A
		protected MailboxAssociationInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B70B RID: 46859 RVA: 0x002A0D24 File Offset: 0x0029EF24
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
