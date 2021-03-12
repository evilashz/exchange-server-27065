using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED7 RID: 3799
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxDatabaseVersionUnsupportedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A90C RID: 43276 RVA: 0x0028AF6C File Offset: 0x0028916C
		public MailboxDatabaseVersionUnsupportedPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A90D RID: 43277 RVA: 0x0028AF75 File Offset: 0x00289175
		public MailboxDatabaseVersionUnsupportedPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A90E RID: 43278 RVA: 0x0028AF7F File Offset: 0x0028917F
		protected MailboxDatabaseVersionUnsupportedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A90F RID: 43279 RVA: 0x0028AF89 File Offset: 0x00289189
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
