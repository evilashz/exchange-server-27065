using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000EF RID: 239
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidE14MailboxesException : MigrationPermanentException
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x000100BC File Offset: 0x0000E2BC
		public InvalidE14MailboxesException() : base(UpgradeHandlerStrings.InvalidE14Mailboxes)
		{
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000100C9 File Offset: 0x0000E2C9
		public InvalidE14MailboxesException(Exception innerException) : base(UpgradeHandlerStrings.InvalidE14Mailboxes, innerException)
		{
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000100D7 File Offset: 0x0000E2D7
		protected InvalidE14MailboxesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000100E1 File Offset: 0x0000E2E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
