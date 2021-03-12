using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200000B RID: 11
	[Cmdlet("Select", "UserForReconciliation")]
	public sealed class SelectUserForReconciliation : Task
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000041C0 File Offset: 0x000023C0
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000041D7 File Offset: 0x000023D7
		[Parameter(Mandatory = true)]
		[ValidateCount(2, 2)]
		public ADObjectId[] User
		{
			get
			{
				return (ADObjectId[])base.Fields["User"];
			}
			set
			{
				base.Fields["User"] = value;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000041EA File Offset: 0x000023EA
		protected override bool IsKnownException(Exception e)
		{
			return e is DataSourceOperationException || e is DataSourceTransientException || e is DataValidationException || e is ManagementObjectNotFoundException || e is ManagementObjectAmbiguousException || base.IsKnownException(e);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004220 File Offset: 0x00002420
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.User == null || this.User.Length != 2 || this.User[0] == null || this.User[1] == null || !this.User[0].PartitionGuid.Equals(this.User[1].PartitionGuid))
			{
				base.WriteError(new TaskException(Strings.ErrorSelectUserCmdletOnlyWorksForTwoUsers), ExchangeErrorCategory.Client, null);
			}
			this.recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAllTenantsObjectId(this.User[0]), 70, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\common\\SelectUserForReconciliation.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000042CC File Offset: 0x000024CC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADObjectId adobjectId = this.User[0];
			ADObjectId adobjectId2 = this.User[1];
			base.InternalProcessRecord();
			if (adobjectId != null && adobjectId2 != null)
			{
				ADObjectId adobjectId3 = this.recipientSession.ChooseBetweenAmbiguousUsers(adobjectId, adobjectId2);
				if (adobjectId3 != null)
				{
					if (adobjectId.Equals(adobjectId3))
					{
						base.WriteObject(adobjectId2);
					}
					else
					{
						base.WriteObject(adobjectId);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400000D RID: 13
		private ITenantRecipientSession recipientSession;
	}
}
