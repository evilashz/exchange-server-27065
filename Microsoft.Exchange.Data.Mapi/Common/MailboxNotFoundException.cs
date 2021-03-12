using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x0200004E RID: 78
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxNotFoundException : MapiObjectNotFoundException
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000E57A File Offset: 0x0000C77A
		public MailboxNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000E583 File Offset: 0x0000C783
		public MailboxNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000E58D File Offset: 0x0000C78D
		protected MailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000E597 File Offset: 0x0000C797
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
