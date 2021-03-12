using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000677 RID: 1655
	internal static class RbacRoleAssignmentCommon
	{
		// Token: 0x06003A9F RID: 15007 RVA: 0x000F7BA8 File Offset: 0x000F5DA8
		internal static void CheckMutuallyExclusiveParameters(Task task)
		{
			task.CheckExclusiveParameters(new object[]
			{
				RbacCommonParameters.ParameterRecipientWriteScope,
				RbacCommonParameters.ParameterCustomRecipientWriteScope,
				RbacCommonParameters.ParameterRecipientOrganizationalUnitScope,
				"ExclusiveRecipientWriteScope",
				RbacCommonParameters.ParameterRecipientRelativeWriteScope,
				"WritableServer",
				"WritableRecipient",
				"ParameterWritableDatabase"
			});
			task.CheckExclusiveParameters(new object[]
			{
				RbacCommonParameters.ParameterConfigWriteScope,
				RbacCommonParameters.ParameterCustomConfigWriteScope,
				"ExclusiveConfigWriteScope",
				"WritableServer",
				"WritableRecipient",
				"ParameterWritableDatabase"
			});
		}

		// Token: 0x04002666 RID: 9830
		internal const string ParameterSetUser = "User";

		// Token: 0x04002667 RID: 9831
		internal const string ParameterSetSecurityGroup = "SecurityGroup";

		// Token: 0x04002668 RID: 9832
		internal const string ParameterSetPolicy = "Policy";

		// Token: 0x04002669 RID: 9833
		internal const string ParameterSetComputer = "Computer";

		// Token: 0x0400266A RID: 9834
		internal const string ParameterSetRelativeRecipientWriteScope = "RelativeRecipientWriteScope";

		// Token: 0x0400266B RID: 9835
		internal const string ParameterSetCustomRecipientWriteScope = "CustomRecipientWriteScope";

		// Token: 0x0400266C RID: 9836
		internal const string ParameterSetDomainOrganizationalUnit = "RecipientOrganizationalUnitScope";

		// Token: 0x0400266D RID: 9837
		internal const string ParameterSetExclusiveScope = "ExclusiveScope";

		// Token: 0x0400266E RID: 9838
		internal static readonly RecipientWriteScopeType[] AllowedRecipientRelativeWriteScope = new RecipientWriteScopeType[]
		{
			RecipientWriteScopeType.None,
			RecipientWriteScopeType.Organization,
			RecipientWriteScopeType.Self,
			RecipientWriteScopeType.MyDistributionGroups,
			RecipientWriteScopeType.MailboxICanDelegate
		};
	}
}
