using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.Management.ClassificationDefinitions;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Classification;
using Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003CD RID: 973
	internal static class PolicyNudgeConfigurationUtils
	{
		// Token: 0x06001B4B RID: 6987 RVA: 0x0009BD20 File Offset: 0x00099F20
		internal static IEnumerable<Tuple<T1, T2>> OuterJoin<T1, T2, TID>(IEnumerable<T1> lefts, Func<T1, TID> leftId, IEnumerable<T2> rights, Func<T2, TID> rightId)
		{
			IEnumerable<Tuple<T1, T2>> first = from left in lefts
			join rightItem in rights on leftId(left) equals rightId(rightItem) into tempRight
			from rightItem in tempRight.DefaultIfEmpty<T2>()
			select new Tuple<T1, T2>(left, rightItem);
			IEnumerable<Tuple<T1, T2>> second = from right2 in rights
			join left2 in lefts on rightId(right2) equals leftId(left2) into tempLeft
			from left2 in tempLeft.DefaultIfEmpty<T1>()
			select new Tuple<T1, T2>(left2, right2);
			return first.Union(second);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0009BDDC File Offset: 0x00099FDC
		internal static void MarkElementAsApply(XmlElement element, bool apply)
		{
			if (!apply)
			{
				while (element.HasChildNodes)
				{
					element.RemoveChild(element.FirstChild);
				}
			}
			else if (!element.HasChildNodes)
			{
				element.InnerText = " ";
			}
			element.SetAttribute("apply", apply.ToString());
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0009BE2C File Offset: 0x0009A02C
		internal static string GetExchangeLocaleFromOutlookCultureTag(string outlookCultureTag)
		{
			if (outlookCultureTag == null)
			{
				return null;
			}
			CultureInfo cultureInfo = null;
			try
			{
				cultureInfo = new CultureInfo(outlookCultureTag);
				goto IL_35;
			}
			catch (CultureNotFoundException)
			{
				return null;
			}
			IL_15:
			if (LanguagePackInfo.expectedCultureLcids.Contains(cultureInfo.LCID))
			{
				return cultureInfo.Name;
			}
			cultureInfo = cultureInfo.Parent;
			IL_35:
			if (cultureInfo == CultureInfo.InvariantCulture)
			{
				return null;
			}
			goto IL_15;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0009BE8C File Offset: 0x0009A08C
		internal static bool CanOutlookSupportFullPnrXml(string engineVersion)
		{
			try
			{
				if (ExchangeBuild.Parse(engineVersion) >= ClassificationDefinitionConstants.CompatibleEngineVersion)
				{
					return true;
				}
			}
			catch (ArgumentException)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(RequestDetailsLogger.Current, "Invalid Outlook MCE Version", engineVersion);
			}
			return false;
		}

		// Token: 0x020003CE RID: 974
		internal class AdMessageStrings : ETRToPNRTranslator.IMessageStrings
		{
			// Token: 0x06001B55 RID: 6997 RVA: 0x0009BED8 File Offset: 0x0009A0D8
			public AdMessageStrings(CachedOrganizationConfiguration serverConfig, string outlookCultureTag)
			{
				this.serverConfig = serverConfig;
				this.OutlookCultureTag = outlookCultureTag;
			}

			// Token: 0x06001B56 RID: 6998 RVA: 0x0009BEEE File Offset: 0x0009A0EE
			public PolicyTipMessage Get(ETRToPNRTranslator.OutlookActionTypes type)
			{
				return this.Get(Tuple.Create<string, PolicyTipMessageConfigAction>(PolicyNudgeConfigurationUtils.GetExchangeLocaleFromOutlookCultureTag(this.OutlookCultureTag), this.GetPolicyTipAction(type)));
			}

			// Token: 0x17000334 RID: 820
			// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0009BF0D File Offset: 0x0009A10D
			// (set) Token: 0x06001B58 RID: 7000 RVA: 0x0009BF15 File Offset: 0x0009A115
			public string OutlookCultureTag { get; private set; }

			// Token: 0x06001B59 RID: 7001 RVA: 0x0009BF20 File Offset: 0x0009A120
			private PolicyTipMessageConfigAction GetPolicyTipAction(ETRToPNRTranslator.OutlookActionTypes outlookActionType)
			{
				switch (outlookActionType)
				{
				case ETRToPNRTranslator.OutlookActionTypes.NotifyOnly:
					return PolicyTipMessageConfigAction.NotifyOnly;
				case ETRToPNRTranslator.OutlookActionTypes.RejectMessage:
				case ETRToPNRTranslator.OutlookActionTypes.RejectUnlessFalsePositiveOverride:
					return PolicyTipMessageConfigAction.Reject;
				case ETRToPNRTranslator.OutlookActionTypes.RejectUnlessSilentOverride:
				case ETRToPNRTranslator.OutlookActionTypes.RejectUnlessExplicitOverride:
					return PolicyTipMessageConfigAction.RejectOverride;
				default:
					throw new IndexOutOfRangeException();
				}
			}

			// Token: 0x17000335 RID: 821
			// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0009BF56 File Offset: 0x0009A156
			public PolicyTipMessage Url
			{
				get
				{
					return this.Get(Tuple.Create<string, PolicyTipMessageConfigAction>(string.Empty, PolicyTipMessageConfigAction.Url));
				}
			}

			// Token: 0x06001B5B RID: 7003 RVA: 0x0009BF6C File Offset: 0x0009A16C
			private PolicyTipMessage Get(Tuple<string, PolicyTipMessageConfigAction> key)
			{
				PerTenantPolicyNudgeRulesCollection.PolicyTipMessages messages = this.serverConfig.PolicyNudgeRules.Messages;
				PolicyTipMessage result;
				if (!messages.TryGetValue(key, out result))
				{
					throw new IndexOutOfRangeException();
				}
				return result;
			}

			// Token: 0x040011E6 RID: 4582
			private CachedOrganizationConfiguration serverConfig;
		}

		// Token: 0x020003CF RID: 975
		internal class AdDistributionListResolver : ETRToPNRTranslator.IDistributionListResolver
		{
			// Token: 0x06001B5C RID: 7004 RVA: 0x0009BF9C File Offset: 0x0009A19C
			public AdDistributionListResolver(CachedOrganizationConfiguration serverConfig, ADObjectId senderADObjectId)
			{
				this.serverConfig = serverConfig;
				this.senderADObjectId = senderADObjectId;
			}

			// Token: 0x06001B5D RID: 7005 RVA: 0x0009BFE0 File Offset: 0x0009A1E0
			public IEnumerable<IVersionedItem> Get(string distributionList)
			{
				GroupConfiguration groupInformation = this.serverConfig.GroupsConfiguration.GetGroupInformation(distributionList);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.serverConfig.OrganizationId), 260, "Get", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\PolicyNudges\\PolicyNudgeConfigurationUtils.cs");
				HashSet<GroupConfiguration> hashSet = new HashSet<GroupConfiguration>(new PolicyNudgeConfigurationUtils.AdDistributionListResolver.GroupConfigurationEqualityComparer());
				this.GetAllGroups(groupInformation, tenantOrRootOrgRecipientSession, hashSet);
				return (from g in hashSet
				select new VersionedItem(g.Id.ToString(), g.Version)).DefaultIfEmpty(new VersionedItem(distributionList, DateTime.MinValue));
			}

			// Token: 0x06001B5E RID: 7006 RVA: 0x0009C098 File Offset: 0x0009A298
			private void GetAllGroups(GroupConfiguration group, IRecipientSession session, HashSet<GroupConfiguration> groups)
			{
				if (group == null || groups.Add(group))
				{
					return;
				}
				foreach (GroupConfiguration group2 in from memberGroupGuid in @group.GroupGuids
				select this.serverConfig.GroupsConfiguration.GetGroupInformation(session, memberGroupGuid))
				{
					this.GetAllGroups(group2, session, groups);
				}
			}

			// Token: 0x06001B5F RID: 7007 RVA: 0x0009C120 File Offset: 0x0009A320
			public bool IsMemberOf(string distributionList)
			{
				return this.serverConfig.GroupsConfiguration.IsMemberOf(this.senderADObjectId, distributionList);
			}

			// Token: 0x040011E8 RID: 4584
			private CachedOrganizationConfiguration serverConfig;

			// Token: 0x040011E9 RID: 4585
			private ADObjectId senderADObjectId;

			// Token: 0x020003D0 RID: 976
			private class GroupConfigurationEqualityComparer : IEqualityComparer<GroupConfiguration>
			{
				// Token: 0x06001B61 RID: 7009 RVA: 0x0009C139 File Offset: 0x0009A339
				public bool Equals(GroupConfiguration x, GroupConfiguration y)
				{
					return (x == null && y == null) || (x != null && y != null && x.Id == y.Id);
				}

				// Token: 0x06001B62 RID: 7010 RVA: 0x0009C15C File Offset: 0x0009A35C
				public int GetHashCode(GroupConfiguration obj)
				{
					if (obj == null)
					{
						return 0;
					}
					return obj.Id.GetHashCode();
				}
			}
		}

		// Token: 0x020003D1 RID: 977
		internal class DataClassificationResolver : ETRToPNRTranslator.IDataClassificationResolver
		{
			// Token: 0x06001B64 RID: 7012 RVA: 0x0009C18A File Offset: 0x0009A38A
			public DataClassificationResolver(CachedOrganizationConfiguration organizationConfig)
			{
				this.organizationConfig = organizationConfig;
			}

			// Token: 0x06001B65 RID: 7013 RVA: 0x0009C1B8 File Offset: 0x0009A3B8
			public bool IsVersionedDataClassification(string dataClassificationId, string rulePackageId)
			{
				bool result = false;
				if (this.organizationConfig != null && this.organizationConfig.ClassificationDefinitions != null)
				{
					ClassificationRulePackage classificationRulePackage = this.organizationConfig.ClassificationDefinitions.FirstOrDefault((ClassificationRulePackage rulePack) => string.Equals(rulePack.ID, rulePackageId, StringComparison.OrdinalIgnoreCase));
					if (classificationRulePackage != null && classificationRulePackage.VersionedDataClassificationIds != null)
					{
						result = classificationRulePackage.VersionedDataClassificationIds.Contains(dataClassificationId);
					}
				}
				return result;
			}

			// Token: 0x040011EB RID: 4587
			private CachedOrganizationConfiguration organizationConfig;
		}
	}
}
