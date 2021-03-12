using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.FederatedDirectory;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200033F RID: 831
	internal sealed class GetUserUnifiedGroups : SingleStepServiceCommand<GetUserUnifiedGroupsRequest, GetUserUnifiedGroupsResponseMessage>
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x0007D050 File Offset: 0x0007B250
		public GetUserUnifiedGroups(CallContext callContext, GetUserUnifiedGroupsRequest request) : base(callContext, request)
		{
			this.requestedGroupsSets = request.ValidateParams();
			WarmupGroupManagementDependency.WarmUpAsyncIfRequired(base.CallContext.AccessingPrincipal);
			OwsLogRegistry.Register(GetUserUnifiedGroups.GetGetUserUnifiedGroupsActionName, typeof(GetUserUnifiedGroupsMetadata), new Type[0]);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0007D090 File Offset: 0x0007B290
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetUserUnifiedGroupsResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0007D0B8 File Offset: 0x0007B2B8
		internal override ServiceResult<GetUserUnifiedGroupsResponseMessage> Execute()
		{
			GetUserUnifiedGroups.Tracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "GetUserUnifiedGroups.Execute: Retrieving unified groups for user {0}.", base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress);
			List<GroupMailbox> allGroups;
			if (GetUserUnifiedGroups.IsModernGroupsNewArchitecture(base.CallContext.AccessingADUser))
			{
				allGroups = this.GetJoinedGroupsSupplementedByAAD();
			}
			else
			{
				allGroups = this.GetJoinedGroupsFromMailboxOnly();
			}
			GetUserUnifiedGroupsResponseMessage getUserUnifiedGroupsResponseMessage = new GetUserUnifiedGroupsResponseMessage();
			List<UnifiedGroupsSet> list = new List<UnifiedGroupsSet>();
			foreach (RequestedUnifiedGroupsSet requestedUnifiedGroupsSet in this.requestedGroupsSets)
			{
				int totalGroups = 0;
				List<GroupMailbox> resultGroupsList = GetUserUnifiedGroups.GetResultGroupsList(allGroups, requestedUnifiedGroupsSet, out totalGroups);
				UnifiedGroupsSet item = new UnifiedGroupsSet
				{
					FilterType = requestedUnifiedGroupsSet.FilterType,
					TotalGroups = totalGroups,
					Groups = GetUserUnifiedGroups.ConvertToUnifiedGroupArray(resultGroupsList)
				};
				list.Add(item);
			}
			getUserUnifiedGroupsResponseMessage.GroupsSets = list.ToArray();
			return new ServiceResult<GetUserUnifiedGroupsResponseMessage>(getUserUnifiedGroupsResponseMessage);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0007D198 File Offset: 0x0007B398
		private static UnifiedGroup[] ConvertToUnifiedGroupArray(List<GroupMailbox> groupMailboxes)
		{
			UnifiedGroup[] array = new UnifiedGroup[groupMailboxes.Count];
			for (int i = 0; i < groupMailboxes.Count; i++)
			{
				GroupMailbox groupMailbox = groupMailboxes[i];
				array[i] = new UnifiedGroup
				{
					DisplayName = groupMailbox.DisplayName,
					SmtpAddress = (string)groupMailbox.SmtpAddress,
					LegacyDN = groupMailbox.Locator.LegacyDn,
					IsFavorite = groupMailbox.IsPinned,
					AccessType = (UnifiedGroupAccessType)groupMailbox.Type
				};
			}
			return array;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0007D248 File Offset: 0x0007B448
		private static List<GroupMailbox> GetResultGroupsList(List<GroupMailbox> allGroups, RequestedUnifiedGroupsSet requestedSet, out int totalGroupsNumber)
		{
			totalGroupsNumber = 0;
			if (allGroups == null || !allGroups.Any<GroupMailbox>())
			{
				return allGroups;
			}
			List<GroupMailbox> list = allGroups;
			switch (requestedSet.FilterType)
			{
			case UnifiedGroupsFilterType.Favorites:
				list = (from mailbox in allGroups
				where mailbox.IsPinned
				select mailbox).ToList<GroupMailbox>();
				break;
			case UnifiedGroupsFilterType.ExcludeFavorites:
				list = (from mailbox in allGroups
				where !mailbox.IsPinned
				select mailbox).ToList<GroupMailbox>();
				break;
			}
			switch (requestedSet.SortType)
			{
			case UnifiedGroupsSortType.DisplayName:
				list = GetUserUnifiedGroups.GetSortedGroupList<string>(list, (GroupMailbox mailbox) => mailbox.DisplayName, requestedSet.SortDirection);
				break;
			case UnifiedGroupsSortType.JoinDate:
				list = GetUserUnifiedGroups.GetSortedGroupList<ExDateTime>(list, (GroupMailbox mailbox) => mailbox.JoinDate, requestedSet.SortDirection);
				break;
			case UnifiedGroupsSortType.FavoriteDate:
				list = GetUserUnifiedGroups.GetSortedGroupList<ExDateTime>(list, (GroupMailbox mailbox) => mailbox.PinDate, requestedSet.SortDirection);
				break;
			}
			totalGroupsNumber = list.Count;
			if (requestedSet.GroupsLimit > 0 && requestedSet.GroupsLimit < totalGroupsNumber)
			{
				list = list.Take(requestedSet.GroupsLimit).ToList<GroupMailbox>();
			}
			return list;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0007D3A4 File Offset: 0x0007B5A4
		private static List<GroupMailbox> GetSortedGroupList<T>(List<GroupMailbox> groups, Func<GroupMailbox, T> sortFunction, SortDirection sortDirection)
		{
			List<GroupMailbox> result;
			if (sortDirection == SortDirection.Descending)
			{
				result = groups.OrderByDescending(sortFunction).ToList<GroupMailbox>();
			}
			else
			{
				result = groups.OrderBy(sortFunction).ToList<GroupMailbox>();
			}
			return result;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0007D3D4 File Offset: 0x0007B5D4
		private List<GroupMailbox> GetJoinedGroupsSupplementedByAAD()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			ADUser accessingADUser = base.CallContext.AccessingADUser;
			GroupsLogger groupsLogger = new GroupsLogger(GroupTaskName.GetGroupMembership, base.CallContext.ProtocolLog.ActivityId);
			UserMailboxLocator userMailboxLocator = UserMailboxLocator.Instantiate(adrecipientSession, accessingADUser);
			QueuedGroupJoinInvoker queuedGroupJoinInvoker = new QueuedGroupJoinInvoker();
			GroupMembershipAADReader groupMembershipAADReader = new GroupMembershipAADReader(accessingADUser, groupsLogger);
			groupsLogger.LogTrace("Retrieving Joined Groups. User={0}. TenantDomain={1}", new object[]
			{
				userMailboxLocator.ExternalId,
				base.CallContext.AccessingADUser.PrimarySmtpAddress.Domain
			});
			List<GroupMailbox> result;
			try
			{
				GroupMembershipMailboxReader mailboxReader = new GroupMembershipMailboxReader(userMailboxLocator, adrecipientSession, base.MailboxIdentityMailboxSession);
				GroupMailboxCollectionBuilder groupMailboxCollectionBuilder = new GroupMailboxCollectionBuilder(adrecipientSession, groupsLogger);
				GroupMembershipCompositeReader groupMembershipCompositeReader = new GroupMembershipCompositeReader(groupMembershipAADReader, mailboxReader, queuedGroupJoinInvoker, groupsLogger, groupMailboxCollectionBuilder);
				result = groupMembershipCompositeReader.GetJoinedGroups().ToList<GroupMailbox>();
			}
			finally
			{
				int queueSize = queuedGroupJoinInvoker.QueueSize;
				base.CallContext.ProtocolLog.Set(GetUserUnifiedGroupsMetadata.AADOnlyGroupCount, queueSize);
				base.CallContext.ProtocolLog.Set(GetUserUnifiedGroupsMetadata.AADLatency, (int)groupMembershipAADReader.AADLatency.TotalMilliseconds);
				if (!queuedGroupJoinInvoker.ProcessQueue(userMailboxLocator, base.CallContext.ProtocolLog.ActivityId))
				{
					groupsLogger.LogTrace("Queue was not processed.", new object[0]);
				}
			}
			return result;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0007D538 File Offset: 0x0007B738
		private List<GroupMailbox> GetJoinedGroupsFromMailboxOnly()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			UserMailboxLocator mailbox = UserMailboxLocator.Instantiate(adrecipientSession, base.CallContext.AccessingADUser);
			GroupMembershipMailboxReader groupMembershipMailboxReader = new GroupMembershipMailboxReader(mailbox, adrecipientSession, base.MailboxIdentityMailboxSession);
			return groupMembershipMailboxReader.GetJoinedGroups().ToList<GroupMailbox>();
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0007D584 File Offset: 0x0007B784
		private static bool IsModernGroupsNewArchitecture(ADUser user)
		{
			ExchangeConfigurationUnit configurationUnit = null;
			if (user.OrganizationId != null && user.OrganizationId.ConfigurationUnit != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(user.OrganizationId);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 294, "IsModernGroupsNewArchitecture", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\GetUserUnifiedGroups.cs");
				configurationUnit = tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(user.OrganizationId.ConfigurationUnit);
			}
			return VariantConfiguration.GetSnapshot(user.GetContext(configurationUnit), null, null).OwaClientServer.ModernGroupsNewArchitecture.Enabled;
		}

		// Token: 0x04000FCB RID: 4043
		private static readonly string GetGetUserUnifiedGroupsActionName = typeof(GetUserUnifiedGroups).Name;

		// Token: 0x04000FCC RID: 4044
		private static readonly Trace Tracer = ExTraceGlobals.GetUserUnifiedGroupsTracer;

		// Token: 0x04000FCD RID: 4045
		private readonly RequestedUnifiedGroupsSet[] requestedGroupsSets;
	}
}
