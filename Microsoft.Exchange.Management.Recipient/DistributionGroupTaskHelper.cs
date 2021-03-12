using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class DistributionGroupTaskHelper
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000898C File Offset: 0x00006B8C
		internal static void CheckMembershipRestriction(ADGroup distributionGroup, Task.ErrorLoggerDelegate errorLogger)
		{
			if (GroupTypeFlags.SecurityEnabled == (distributionGroup.GroupType & GroupTypeFlags.SecurityEnabled))
			{
				if (distributionGroup.MemberJoinRestriction != MemberUpdateType.Closed && distributionGroup.MemberJoinRestriction != MemberUpdateType.ApprovalRequired)
				{
					errorLogger(new RecipientTaskException(Strings.ErrorJoinRestrictionInvalid), ExchangeErrorCategory.Client, null);
				}
				if (distributionGroup.MemberDepartRestriction != MemberUpdateType.Closed)
				{
					errorLogger(new RecipientTaskException(Strings.ErrorDepartRestrictionInvalid), ExchangeErrorCategory.Client, null);
				}
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000089F4 File Offset: 0x00006BF4
		internal static void CheckModerationInMixedEnvironment(ADRecipient recipient, Task.TaskWarningLoggingDelegate warningLogger, LocalizedString warningText)
		{
			if (!recipient.ModerationEnabled || !recipient.IsModified(ADRecipientSchema.ModerationEnabled))
			{
				return;
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 103, "CheckModerationInMixedEnvironment", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\DistributionList\\DistributionGroupTaskHelper.cs");
			ADPagedReader<MiniServer> adpagedReader = topologyConfigurationSession.FindPagedMiniServer(null, QueryScope.SubTree, DistributionGroupTaskHelper.filter, null, 1, DistributionGroupTaskHelper.ExchangeVersionProperties);
			using (IEnumerator<MiniServer> enumerator = adpagedReader.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					MiniServer miniServer = enumerator.Current;
					warningLogger(warningText);
				}
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008A88 File Offset: 0x00006C88
		internal static string GetGroupNameWithNamingPolicy(Organization organization, ADUser user, ADGroup group, string groupName, PropertyDefinition property, Task.ErrorLoggerDelegate errorLogger)
		{
			DistributionGroupTaskHelper.ValidateGroupNameWithBlockedWordsList(organization, group, groupName, property, errorLogger);
			if (organization.DistributionGroupNamingPolicy == null)
			{
				errorLogger(new RecipientTaskException(Strings.ErrorDistributionGroupNamingPolicy), ExchangeErrorCategory.ServerOperation, organization.Identity);
			}
			string appliedName = organization.DistributionGroupNamingPolicy.GetAppliedName(groupName, user);
			return appliedName.Trim();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008ADC File Offset: 0x00006CDC
		internal static void ValidateGroupNameWithBlockedWordsList(Organization organization, ADGroup group, string groupName, PropertyDefinition property, Task.ErrorLoggerDelegate errorLogger)
		{
			if (organization == null)
			{
				throw new ArgumentNullException("organization");
			}
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (string.IsNullOrEmpty(groupName))
			{
				throw new ArgumentNullException("groupName");
			}
			string text = groupName.ToLower();
			foreach (string text2 in organization.DistributionGroupNameBlockedWordsList)
			{
				if (text.Contains(text2.ToLower()))
				{
					throw new DataValidationException(new PropertyValidationError(Strings.ErrorGroupNameContainBlockedWords(text2), property, null));
				}
			}
		}

		// Token: 0x04000033 RID: 51
		internal static readonly ComparisonFilter EarlierThanMinimumE14VersionFilter = new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.E14MinVersion);

		// Token: 0x04000034 RID: 52
		internal static readonly ComparisonFilter EarlierThanMinimumE2007VersionFilter = new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.E2007MinVersion);

		// Token: 0x04000035 RID: 53
		internal static readonly BitMaskAndFilter HubRoleFilter = new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL);

		// Token: 0x04000036 RID: 54
		internal static readonly QueryFilter filter = new OrFilter(new QueryFilter[]
		{
			DistributionGroupTaskHelper.EarlierThanMinimumE2007VersionFilter,
			new AndFilter(new QueryFilter[]
			{
				DistributionGroupTaskHelper.EarlierThanMinimumE14VersionFilter,
				DistributionGroupTaskHelper.HubRoleFilter
			})
		});

		// Token: 0x04000037 RID: 55
		internal static readonly PropertyDefinition[] ExchangeVersionProperties = new PropertyDefinition[]
		{
			ADObjectSchema.ExchangeVersion
		};
	}
}
