using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000F0 RID: 240
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidE15MailboxesException : MigrationPermanentException
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x000100EB File Offset: 0x0000E2EB
		public InvalidE15MailboxesException() : base(UpgradeHandlerStrings.InvalidE15Mailboxes)
		{
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000100F8 File Offset: 0x0000E2F8
		public InvalidE15MailboxesException(Exception innerException) : base(UpgradeHandlerStrings.InvalidE15Mailboxes, innerException)
		{
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00010106 File Offset: 0x0000E306
		protected InvalidE15MailboxesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00010110 File Offset: 0x0000E310
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
