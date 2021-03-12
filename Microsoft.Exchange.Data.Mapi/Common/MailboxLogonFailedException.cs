using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000053 RID: 83
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxLogonFailedException : MapiLogonFailedException
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000E68E File Offset: 0x0000C88E
		public MailboxLogonFailedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000E697 File Offset: 0x0000C897
		public MailboxLogonFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000E6A1 File Offset: 0x0000C8A1
		protected MailboxLogonFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000E6AB File Offset: 0x0000C8AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
