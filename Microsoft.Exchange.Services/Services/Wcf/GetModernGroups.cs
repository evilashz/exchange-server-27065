using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage.Inference.GroupingModel;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.FederatedDirectory;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000922 RID: 2338
	internal class GetModernGroups : ServiceCommand<GetModernGroupsResponse>
	{
		// Token: 0x060043C1 RID: 17345 RVA: 0x000E609B File Offset: 0x000E429B
		public GetModernGroups(CallContext context) : base(context)
		{
			WarmupGroupManagementDependency.WarmUpAsyncIfRequired(base.CallContext.AccessingPrincipal);
			OwsLogRegistry.Register(GetModernGroups.GetModernGroupsActionName, typeof(GetModernGroupsMetadata), new Type[0]);
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x000E60F4 File Offset: 0x000E42F4
		protected override GetModernGroupsResponse InternalExecute()
		{
			ExTraceGlobals.ModernGroupsTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "GetModernGroups.InternalExecute: Retrieving modern groups for user {0}.", base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress);
			List<GroupMailbox> source;
			if (base.CallContext.FeaturesManager != null && base.CallContext.FeaturesManager.IsFeatureSupported("ModernGroupsNewArchitecture"))
			{
				source = this.GetJoinedGroupsSupplementedByAAD();
			}
			else
			{
				source = this.GetJoinedGroupsFromMailboxOnly();
			}
			List<GroupMailbox> groupMailboxes = (from mailbox in source
			where mailbox.IsPinned
			orderby mailbox.PinDate descending
			select mailbox).ToList<GroupMailbox>();
			List<GroupMailbox> groupMailboxes2 = (from mailbox in source
			where !mailbox.IsPinned
			orderby mailbox.JoinDate descending
			select mailbox).ToList<GroupMailbox>();
			return new GetModernGroupsResponse
			{
				JoinedGroups = GetModernGroups.ConvertToModernGroupTypeList(groupMailboxes2),
				PinnedGroups = GetModernGroups.ConvertToModernGroupTypeList(groupMailboxes),
				RecommendedGroups = this.GetRecommendedModernGroupTypeList(3),
				IsModernGroupsAddressListPresent = this.IsModernGroupsAddressListPresent()
			};
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x000E6230 File Offset: 0x000E4430
		private static ModernGroupType[] ConvertToModernGroupTypeList(List<GroupMailbox> groupMailboxes)
		{
			ModernGroupType[] array = new ModernGroupType[groupMailboxes.Count];
			for (int i = 0; i < groupMailboxes.Count; i++)
			{
				GroupMailbox groupMailbox = groupMailboxes[i];
				array[i] = new ModernGroupType
				{
					DisplayName = groupMailbox.DisplayName,
					SmtpAddress = (string)groupMailbox.SmtpAddress,
					IsPinned = groupMailbox.IsPinned,
					GroupObjectType = groupMailbox.Type
				};
			}
			return array;
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x000E62A4 File Offset: 0x000E44A4
		private bool IsModernGroupsAddressListPresent()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.CallContext.AccessingPrincipal.MailboxInfo.OrganizationId), 125, "IsModernGroupsAddressListPresent", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\jsonservice\\servicecommands\\GetModernGroups.cs");
			return AddressBookBase.IsModernGroupsAddressListPresent(base.CallContext.EffectiveCaller.ClientSecurityContext, tenantOrTopologyConfigurationSession, base.CallContext.AccessingPrincipal.MailboxInfo.Configuration.AddressBookPolicy);
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x000E6314 File Offset: 0x000E4514
		private List<GroupMailbox> GetJoinedGroupsFromMailboxOnly()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			UserMailboxLocator mailbox = UserMailboxLocator.Instantiate(adrecipientSession, base.CallContext.AccessingADUser);
			GroupMembershipMailboxReader groupMembershipMailboxReader = new GroupMembershipMailboxReader(mailbox, adrecipientSession, base.MailboxIdentityMailboxSession);
			return groupMembershipMailboxReader.GetJoinedGroups().ToList<GroupMailbox>();
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x000E6360 File Offset: 0x000E4560
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
				base.CallContext.ProtocolLog.Set(GetModernGroupsMetadata.AADOnlyGroupCount, queueSize);
				base.CallContext.ProtocolLog.Set(GetModernGroupsMetadata.AADLatency, (int)groupMembershipAADReader.AADLatency.TotalMilliseconds);
				if (!queuedGroupJoinInvoker.ProcessQueue(userMailboxLocator, base.CallContext.ProtocolLog.ActivityId))
				{
					groupsLogger.LogTrace("Queue was not processed.", new object[0]);
				}
			}
			return result;
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x000E64F8 File Offset: 0x000E46F8
		private ModernGroupType[] GetRecommendedModernGroupTypeList(int requestCount)
		{
			ModernGroupType[] result = null;
			if (VariantConfiguration.GetSnapshot(base.CallContext.AccessingADUser.GetContext(null), null, null).Inference.InferenceGroupingModel.Enabled)
			{
				IReadOnlyList<IRecommendedGroupInfo> recommendedGroupInfos = GroupRecommendationsHelper.GetRecommendedGroupInfos(base.MailboxIdentityMailboxSession, delegate(string message)
				{
					ExTraceGlobals.ModernGroupsTracer.TraceDebug((long)this.GetHashCode(), message);
				}, delegate(Exception e)
				{
					ExTraceGlobals.ModernGroupsTracer.TraceError<string>((long)this.GetHashCode(), "Exception while retrieving modern group recommendations: {0}", e.Message);
				});
				if (recommendedGroupInfos != null)
				{
					int num = Math.Min(requestCount, recommendedGroupInfos.Count);
					List<ModernGroupType> list = new List<ModernGroupType>(num);
					for (int i = 0; i < num; i++)
					{
						IRecommendedGroupInfo recommendedGroupInfo = recommendedGroupInfos[i];
						list.Add(GroupRecommendationsHelper.ConvertLatentGroupRecommendationToModernGroupType(recommendedGroupInfo, base.CallContext.AccessingADUser.PrimarySmtpAddress));
					}
					result = list.ToArray();
				}
			}
			return result;
		}

		// Token: 0x04002787 RID: 10119
		private static readonly string GetModernGroupsActionName = typeof(GetModernGroups).Name;
	}
}
