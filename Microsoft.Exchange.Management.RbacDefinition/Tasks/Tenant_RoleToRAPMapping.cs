using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000296 RID: 662
	internal class Tenant_RoleToRAPMapping
	{
		// Token: 0x040002FF RID: 767
		internal static RoleToRAPMapping Definition = new RoleToRAPMapping(new RoleToRAPAssignmentDefinition[]
		{
			new RoleToRAPAssignmentDefinition(RoleType.MyBaseOptions, new string[]
			{
				"*"
			}, "14.00.0000.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyContactInformation, new string[]
			{
				"*"
			}, "14.00.0000.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyCustomApps, new string[]
			{
				"*"
			}, "15.01.0138.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyDistributionGroupMembership, new string[]
			{
				"AutoGroupPermissions"
			}, "14.00.0000.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyDistributionGroups, new string[]
			{
				"AutoGroupPermissions"
			}, "14.00.0000.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyMailSubscriptions, new string[]
			{
				"*"
			}, "14.00.0000.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyMarketplaceApps, new string[]
			{
				"*"
			}, "15.00.0469.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyProfileInformation, new string[]
			{
				"*"
			}, "14.00.0000.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyReadWriteMailboxApps, new string[]
			{
				"*"
			}, "15.01.0138.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyRetentionPolicies, new string[]
			{
				"*"
			}, "14.00.0000.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyTeamMailboxes, new string[]
			{
				"*"
			}, "15.00.0444.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyTextMessaging, new string[]
			{
				"*"
			}, "14.00.0000.000"),
			new RoleToRAPAssignmentDefinition(RoleType.MyVoiceMail, new string[]
			{
				"*"
			}, "14.00.0000.000"),
			new RoleToRAPAssignmentDefinition(RoleType.OrgMarketplaceApps, new string[]
			{
				"*"
			}, "15.00.0469.000")
		});
	}
}
