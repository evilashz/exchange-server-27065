using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000F1 RID: 241
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TooManyPilotMailboxesException : MigrationPermanentException
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x0001011A File Offset: 0x0000E31A
		public TooManyPilotMailboxesException() : base(UpgradeHandlerStrings.TooManyPilotMailboxes)
		{
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00010127 File Offset: 0x0000E327
		public TooManyPilotMailboxesException(Exception innerException) : base(UpgradeHandlerStrings.TooManyPilotMailboxes, innerException)
		{
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00010135 File Offset: 0x0000E335
		protected TooManyPilotMailboxesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001013F File Offset: 0x0000E33F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
