using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A83 RID: 2691
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxLocationException : ADOperationException
	{
		// Token: 0x06007F69 RID: 32617 RVA: 0x001A40EA File Offset: 0x001A22EA
		public MailboxLocationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F6A RID: 32618 RVA: 0x001A40F3 File Offset: 0x001A22F3
		public MailboxLocationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F6B RID: 32619 RVA: 0x001A40FD File Offset: 0x001A22FD
		protected MailboxLocationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F6C RID: 32620 RVA: 0x001A4107 File Offset: 0x001A2307
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
