using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E5 RID: 229
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorQueryingWorkItemException : MigrationTransientException
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x0000FAE6 File Offset: 0x0000DCE6
		public ErrorQueryingWorkItemException() : base(UpgradeHandlerStrings.ErrorQueryingWorkItem)
		{
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000FAF3 File Offset: 0x0000DCF3
		public ErrorQueryingWorkItemException(Exception innerException) : base(UpgradeHandlerStrings.ErrorQueryingWorkItem, innerException)
		{
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000FB01 File Offset: 0x0000DD01
		protected ErrorQueryingWorkItemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000FB0B File Offset: 0x0000DD0B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
