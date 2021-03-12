using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000973 RID: 2419
	internal static class DlpUtils
	{
		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x06005653 RID: 22099 RVA: 0x00162C0C File Offset: 0x00160E0C
		public static Version MaxSupportedVersion
		{
			get
			{
				return new Version("15.0.3.2");
			}
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x00162C44 File Offset: 0x00160E44
		public static IEnumerable<ADComplianceProgram> GetInstalledTenantDlpPolicies(IConfigDataProvider dataSession, string name)
		{
			Guid guid;
			if (Guid.TryParse(name, out guid))
			{
				IList<ADComplianceProgram> list = (from x in DlpUtils.GetDlpPolicies(dataSession, DlpUtils.TenantDlpPoliciesCollectionName, null)
				where x.ImmutableId.Equals(guid)
				select x).ToList<ADComplianceProgram>();
				if (!list.Any<ADComplianceProgram>())
				{
					list = DlpUtils.GetDlpPolicies(dataSession, DlpUtils.TenantDlpPoliciesCollectionName, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid)).ToList<ADComplianceProgram>();
				}
				if (list.Any<ADComplianceProgram>())
				{
					return list;
				}
			}
			return DlpUtils.GetDlpPolicies(dataSession, DlpUtils.TenantDlpPoliciesCollectionName, new TextFilter(ADObjectSchema.Name, name, MatchOptions.FullString, MatchFlags.Default));
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x00162CE0 File Offset: 0x00160EE0
		public static IEnumerable<ADComplianceProgram> GetInstalledTenantDlpPolicies(IConfigDataProvider dataSession)
		{
			return DlpUtils.GetDlpPolicies(dataSession, DlpUtils.TenantDlpPoliciesCollectionName, null);
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x00162CF0 File Offset: 0x00160EF0
		public static IEnumerable<ADComplianceProgram> GetOutOfBoxDlpTemplates(IConfigDataProvider dataSession, string name)
		{
			Guid guid;
			QueryFilter filter;
			if (Guid.TryParse(name, out guid))
			{
				filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid);
			}
			else
			{
				filter = new TextFilter(ADObjectSchema.Name, name, MatchOptions.FullString, MatchFlags.Default);
			}
			return DlpUtils.GetDlpPolicies(dataSession, DlpUtils.OutOfBoxDlpPoliciesCollectionName, filter);
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x00162D35 File Offset: 0x00160F35
		public static IEnumerable<ADComplianceProgram> GetOutOfBoxDlpTemplates(IConfigDataProvider dataSession)
		{
			return DlpUtils.GetDlpPolicies(dataSession, DlpUtils.OutOfBoxDlpPoliciesCollectionName, null);
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x00162D44 File Offset: 0x00160F44
		public static void SaveOutOfBoxDlpTemplates(IConfigDataProvider dataSession, IEnumerable<DlpPolicyTemplateMetaData> dlpTemplates)
		{
			ADComplianceProgramCollection dlpPolicyCollection = DlpUtils.GetDlpPolicyCollection(dataSession, DlpUtils.OutOfBoxDlpPoliciesCollectionName);
			foreach (DlpPolicyTemplateMetaData dlpPolicyTemplateMetaData in dlpTemplates)
			{
				ADComplianceProgram adcomplianceProgram = dlpPolicyTemplateMetaData.ToAdObject();
				adcomplianceProgram.OrganizationId = dlpPolicyCollection.OrganizationId;
				adcomplianceProgram.SetId(dlpPolicyCollection.Id.GetChildId(dlpPolicyTemplateMetaData.Name));
				dataSession.Save(adcomplianceProgram);
			}
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x00162DC4 File Offset: 0x00160FC4
		public static void DeleteOutOfBoxDlpPolicies(IConfigDataProvider dataSession)
		{
			List<ADComplianceProgram> list = DlpUtils.GetOutOfBoxDlpTemplates(dataSession).ToList<ADComplianceProgram>();
			list.ForEach(new Action<ADComplianceProgram>(dataSession.Delete));
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x00162DF0 File Offset: 0x00160FF0
		public static void DeleteOutOfBoxDlpPolicy(IConfigDataProvider dataSession, string templateName)
		{
			ADComplianceProgram instance = DlpUtils.GetOutOfBoxDlpTemplates(dataSession, templateName).FirstOrDefault<ADComplianceProgram>();
			dataSession.Delete(instance);
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x00162E14 File Offset: 0x00161014
		public static void AddTenantDlpPolicy(IConfigDataProvider dataSession, DlpPolicyMetaData dlpPolicy, string organizationParameterValue, out IEnumerable<PSObject> results)
		{
			results = null;
			ADComplianceProgram adcomplianceProgram = dlpPolicy.ToAdObject();
			ADComplianceProgramCollection dlpPolicyCollection = DlpUtils.GetDlpPolicyCollection(dataSession, DlpUtils.TenantDlpPoliciesCollectionName);
			adcomplianceProgram.OrganizationId = dlpPolicyCollection.OrganizationId;
			adcomplianceProgram.SetId(dlpPolicyCollection.Id.GetChildId(dlpPolicy.Name));
			dataSession.Save(adcomplianceProgram);
			IEnumerable<string> cmdlets = Utils.AddOrganizationScopeToCmdlets(dlpPolicy.PolicyCommands, organizationParameterValue);
			string domainController = null;
			ADSessionSettings sessionSettings = null;
			MessagingPoliciesSyncLogDataSession messagingPoliciesSyncLogDataSession = dataSession as MessagingPoliciesSyncLogDataSession;
			if (messagingPoliciesSyncLogDataSession != null)
			{
				domainController = messagingPoliciesSyncLogDataSession.LastUsedDc;
				sessionSettings = messagingPoliciesSyncLogDataSession.SessionSettings;
			}
			try
			{
				results = CmdletRunner.RunCmdlets(cmdlets);
			}
			catch (ParseException e)
			{
				DlpUtils.HandleScriptExecutionError(adcomplianceProgram, DlpUtils.GetErrorHandlingDataSession(domainController, sessionSettings, dataSession), e);
			}
			catch (RuntimeException e2)
			{
				DlpUtils.HandleScriptExecutionError(adcomplianceProgram, DlpUtils.GetErrorHandlingDataSession(domainController, sessionSettings, dataSession), e2);
			}
			catch (CmdletExecutionException e3)
			{
				DlpUtils.HandleScriptExecutionError(adcomplianceProgram, DlpUtils.GetErrorHandlingDataSession(domainController, sessionSettings, dataSession), e3);
			}
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x00162F04 File Offset: 0x00161104
		private static void HandleScriptExecutionError(ADComplianceProgram adDlpPolicy, IConfigDataProvider dataSession, Exception e)
		{
			DlpUtils.DeleteEtrsByDlpPolicy(adDlpPolicy.ImmutableId, dataSession);
			dataSession.Delete(adDlpPolicy);
			throw new DlpPolicyScriptExecutionException(e.Message);
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x00162F24 File Offset: 0x00161124
		private static IConfigDataProvider GetErrorHandlingDataSession(string domainController, ADSessionSettings sessionSettings, IConfigDataProvider dataSession)
		{
			if (!string.IsNullOrEmpty(domainController) && sessionSettings != null)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(domainController, false, ConsistencyMode.IgnoreInvalid, sessionSettings, 316, "GetErrorHandlingDataSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\DlpPolicy\\Utils.cs");
				return new MessagingPoliciesSyncLogDataSession(tenantOrTopologyConfigurationSession, null, null);
			}
			return dataSession;
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x00162F64 File Offset: 0x00161164
		public static bool TryGetTransportRules(IConfigDataProvider dataSession, out IEnumerable<TransportRule> rules, out string error)
		{
			string text = Utils.RuleCollectionNameFromRole();
			QueryFilter filter = new TextFilter(ADObjectSchema.Name, text, MatchOptions.FullString, MatchFlags.Default);
			TransportRuleCollection[] array = (TransportRuleCollection[])dataSession.Find<TransportRuleCollection>(filter, null, true, null);
			if (array.Any<TransportRuleCollection>())
			{
				rules = dataSession.FindPaged<TransportRule>(null, array[0].Id, false, null, 0);
				error = null;
				return true;
			}
			rules = null;
			error = string.Format("Unable to find rule collection {0}. Tenant is not provisioned for Exchange Transport Rules.", text);
			return false;
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x00162FF4 File Offset: 0x001611F4
		internal static IEnumerable<Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule> GetTransportRules(IConfigDataProvider dataSession, Func<Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule, bool> selector)
		{
			ADRuleStorageManager adruleStorageManager;
			IEnumerable<TransportRuleHandle> transportRuleHandles = DlpUtils.GetTransportRuleHandles(dataSession, out adruleStorageManager);
			IEnumerable<Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule> source = from ruleHandle in transportRuleHandles
			select Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule.CreateFromInternalRule(TransportRulePredicate.GetAvailablePredicateMappings(), TransportRuleAction.GetAvailableActionMappings(), ruleHandle.Rule, ruleHandle.AdRule.Priority, ruleHandle.AdRule);
			return source.Where(selector);
		}

		// Token: 0x06005660 RID: 22112 RVA: 0x00163084 File Offset: 0x00161284
		public static List<string> GetEtrsForDlpPolicy(Guid dlpGuid, IConfigDataProvider dataSession)
		{
			ADRuleStorageManager adruleStorageManager;
			IEnumerable<TransportRuleHandle> transportRuleHandles = DlpUtils.GetTransportRuleHandles(dataSession, out adruleStorageManager);
			IEnumerable<Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule> source = from ruleHandle in transportRuleHandles
			select Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule.CreateFromInternalRule(TransportRulePredicate.GetAvailablePredicateMappings(), TransportRuleAction.GetAvailableActionMappings(), ruleHandle.Rule, ruleHandle.AdRule.Priority, ruleHandle.AdRule);
			return (from rule in source
			where rule.DlpPolicyId == dlpGuid
			select rule.ToCmdlet()).ToList<string>();
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x00163104 File Offset: 0x00161304
		internal static IEnumerable<TransportRuleHandle> GetTransportRuleHandles(IConfigDataProvider dataSession, out ADRuleStorageManager ruleStorageManager)
		{
			ruleStorageManager = new ADRuleStorageManager(Utils.RuleCollectionNameFromRole(), dataSession);
			ruleStorageManager.LoadRuleCollection();
			return ruleStorageManager.GetRuleHandles();
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x00163121 File Offset: 0x00161321
		internal static TransportRule GetTransportRuleByName(IConfigDataProvider dataSession, string collectionName, string ruleName)
		{
			return DlpUtils.GetTransportRuleUnParsed(dataSession, collectionName, new TextFilter(ADObjectSchema.Name, ruleName, MatchOptions.FullString, MatchFlags.Default));
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x00163137 File Offset: 0x00161337
		internal static TransportRule GetTransportRuleByGuid(IConfigDataProvider dataSession, string collectionName, Guid objectGuid)
		{
			return DlpUtils.GetTransportRuleUnParsed(dataSession, collectionName, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, objectGuid));
		}

		// Token: 0x06005664 RID: 22116 RVA: 0x00163154 File Offset: 0x00161354
		internal static ADComplianceProgram GetDlpPolicyByName(IConfigDataProvider dataSession, string collectionName, string name)
		{
			QueryFilter filter = new TextFilter(ADObjectSchema.Name, name, MatchOptions.FullString, MatchFlags.Default);
			return DlpUtils.GetDlpPolicies(dataSession, collectionName, filter).FirstOrDefault<ADComplianceProgram>();
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x0016317C File Offset: 0x0016137C
		public static void DeleteEtrsByDlpPolicy(Guid dlpGuid, IConfigDataProvider dataSession)
		{
			IEnumerable<TransportRule> enumerable;
			string message;
			if (!DlpUtils.TryGetTransportRules(dataSession, out enumerable, out message))
			{
				throw new InvalidOperationException(message);
			}
			foreach (TransportRule transportRule in enumerable)
			{
				TransportRule transportRule2 = (TransportRule)TransportRuleParser.Instance.GetRule(transportRule.Xml);
				Guid guid;
				if (transportRule2.TryGetDlpPolicyId(out guid) && guid.Equals(dlpGuid))
				{
					dataSession.Delete(transportRule);
				}
			}
		}

		// Token: 0x06005666 RID: 22118 RVA: 0x00163208 File Offset: 0x00161408
		private static IEnumerable<ADComplianceProgram> GetDlpPolicies(IConfigDataProvider dataSession, string collectionName, QueryFilter filter)
		{
			ADComplianceProgramCollection dlpPolicyCollection = DlpUtils.GetDlpPolicyCollection(dataSession, collectionName);
			return dataSession.FindPaged<ADComplianceProgram>(filter, dlpPolicyCollection.Id, false, null, 0);
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x00163230 File Offset: 0x00161430
		private static ADComplianceProgramCollection GetDlpPolicyCollection(IConfigDataProvider dataSession, string collectionName)
		{
			QueryFilter filter = new TextFilter(ADObjectSchema.Name, collectionName, MatchOptions.FullString, MatchFlags.Default);
			ADComplianceProgramCollection[] array = (ADComplianceProgramCollection[])dataSession.Find<ADComplianceProgramCollection>(filter, null, true, null);
			if (array.Length != 1)
			{
				throw new DlpPolicyInvalidCollectionException();
			}
			return array[0];
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x0016326C File Offset: 0x0016146C
		private static TransportRule GetTransportRuleUnParsed(IConfigDataProvider dataSession, string collectionName, QueryFilter filter)
		{
			ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(collectionName, dataSession);
			adruleStorageManager.LoadRuleCollectionWithoutParsing(filter);
			TransportRule result = null;
			if (adruleStorageManager.Count > 0)
			{
				adruleStorageManager.GetRuleWithoutParsing(0, out result);
			}
			return result;
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x0016329D File Offset: 0x0016149D
		internal static IConfigDataProvider CreateOrgSession(string domainController)
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 547, "CreateOrgSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\DlpPolicy\\Utils.cs");
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x001632C0 File Offset: 0x001614C0
		internal static IEnumerable<DlpPolicyTemplateMetaData> LoadDlpPolicyTemplates(byte[] data)
		{
			IEnumerable<DlpPolicyTemplateMetaData> result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				result = DlpPolicyParser.ParseDlpPolicyTemplates(memoryStream);
			}
			return result;
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x001632F8 File Offset: 0x001614F8
		internal static IEnumerable<DlpPolicyMetaData> LoadDlpPolicyInstances(byte[] data)
		{
			IEnumerable<DlpPolicyMetaData> result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				result = DlpPolicyParser.ParseDlpPolicyInstances(memoryStream);
			}
			return result;
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x00163330 File Offset: 0x00161530
		internal static DlpPolicyTemplateMetaData LoadOutOfBoxDlpTemplate(Fqdn domainController, string templateName)
		{
			IConfigDataProvider dataSession = DlpUtils.CreateOrgSession(domainController);
			ADComplianceProgram adcomplianceProgram = DlpUtils.GetOutOfBoxDlpTemplates(dataSession, templateName).FirstOrDefault<ADComplianceProgram>();
			if (adcomplianceProgram != null)
			{
				return DlpPolicyParser.ParseDlpPolicyTemplate(adcomplianceProgram.TransportRulesXml);
			}
			return null;
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x00163368 File Offset: 0x00161568
		internal static Tuple<RuleState, RuleMode> DlpStateToRuleState(DlpPolicyState state)
		{
			switch (state)
			{
			case DlpPolicyState.Disabled_Audit:
				return new Tuple<RuleState, RuleMode>(RuleState.Disabled, RuleMode.Audit);
			case DlpPolicyState.Disabled_AuditAndNotify:
				return new Tuple<RuleState, RuleMode>(RuleState.Disabled, RuleMode.AuditAndNotify);
			case DlpPolicyState.Disabled_Enforce:
				return new Tuple<RuleState, RuleMode>(RuleState.Disabled, RuleMode.Enforce);
			case DlpPolicyState.Enabled_Audit:
				return new Tuple<RuleState, RuleMode>(RuleState.Enabled, RuleMode.Audit);
			case DlpPolicyState.Enabled_AuditAndNotify:
				return new Tuple<RuleState, RuleMode>(RuleState.Enabled, RuleMode.AuditAndNotify);
			case DlpPolicyState.Enabled_Enforce:
				return new Tuple<RuleState, RuleMode>(RuleState.Enabled, RuleMode.Enforce);
			default:
				return new Tuple<RuleState, RuleMode>(RuleState.Disabled, RuleMode.Enforce);
			}
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x001633CE File Offset: 0x001615CE
		internal static DlpPolicyState RuleStateToDlpState(RuleState state, RuleMode mode)
		{
			if (state == RuleState.Disabled && mode == RuleMode.Audit)
			{
				return DlpPolicyState.Disabled_Audit;
			}
			if (state == RuleState.Disabled && mode == RuleMode.AuditAndNotify)
			{
				return DlpPolicyState.Disabled_AuditAndNotify;
			}
			if (state == RuleState.Disabled && mode == RuleMode.Enforce)
			{
				return DlpPolicyState.Disabled_Enforce;
			}
			if (state == RuleState.Enabled && mode == RuleMode.Audit)
			{
				return DlpPolicyState.Enabled_Audit;
			}
			if (state == RuleState.Enabled && mode == RuleMode.AuditAndNotify)
			{
				return DlpPolicyState.Enabled_AuditAndNotify;
			}
			if (state == RuleState.Enabled && mode == RuleMode.Enforce)
			{
				return DlpPolicyState.Enabled_Enforce;
			}
			return DlpPolicyState.Disabled_Audit;
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x00163410 File Offset: 0x00161610
		internal static ILookup<string, Microsoft.Exchange.MessagingPolicies.Rules.Rule> GetDataClassificationsInUse(IConfigDataProvider tenantSession, IEnumerable<string> dataClassificationIds, IEqualityComparer<string> dataClassificationIdComparer = null)
		{
			ArgumentValidator.ThrowIfNull("tenantSession", tenantSession);
			ArgumentValidator.ThrowIfNull("dataClassificationIds", dataClassificationIds);
			if (!dataClassificationIds.Any<string>())
			{
				return Enumerable.Empty<Microsoft.Exchange.MessagingPolicies.Rules.Rule>().ToLookup((Microsoft.Exchange.MessagingPolicies.Rules.Rule rule) => null);
			}
			ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(Utils.RuleCollectionNameFromRole(), tenantSession);
			adruleStorageManager.LoadRuleCollection();
			return DlpUtils.GetDataClassificationsReferencedByRuleCollection(adruleStorageManager.GetRuleCollection(), dataClassificationIds, dataClassificationIdComparer);
		}

		// Token: 0x06005670 RID: 22128 RVA: 0x00163600 File Offset: 0x00161800
		internal static ILookup<string, Microsoft.Exchange.MessagingPolicies.Rules.Rule> GetDataClassificationsReferencedByRuleCollection(IEnumerable<Microsoft.Exchange.MessagingPolicies.Rules.Rule> ruleCollection, IEnumerable<string> dataClassificationIds, IEqualityComparer<string> dataClassificationIdComparer = null)
		{
			ArgumentValidator.ThrowIfNull("dataClassificationIds", dataClassificationIds);
			if (!dataClassificationIds.Any<string>() || ruleCollection == null || !ruleCollection.Any<Microsoft.Exchange.MessagingPolicies.Rules.Rule>())
			{
				return Enumerable.Empty<Microsoft.Exchange.MessagingPolicies.Rules.Rule>().ToLookup((Microsoft.Exchange.MessagingPolicies.Rules.Rule rule) => null);
			}
			HashSet<string> dataClassificationIdsSet = (dataClassificationIdComparer != null) ? new HashSet<string>(dataClassificationIds, dataClassificationIdComparer) : new HashSet<string>(dataClassificationIds);
			return (from dataClassificationIdAndTransportRuleAssociation in ruleCollection.SelectMany(delegate(Microsoft.Exchange.MessagingPolicies.Rules.Rule transportRule)
			{
				SupplementalData supplementalData = new SupplementalData();
				transportRule.GetSupplementalData(supplementalData);
				Dictionary<string, string> dictionary = supplementalData.Get("DataClassification");
				return dictionary.Keys;
			}, (Microsoft.Exchange.MessagingPolicies.Rules.Rule transportRule, string dataClassificationId) => new
			{
				ReferencedDataClassificationId = dataClassificationId,
				ReferencingRule = transportRule
			})
			where dataClassificationIdsSet.Contains(dataClassificationIdAndTransportRuleAssociation.ReferencedDataClassificationId)
			select dataClassificationIdAndTransportRuleAssociation).ToLookup(dataClassificationIdAndTransportRuleAssociation => dataClassificationIdAndTransportRuleAssociation.ReferencedDataClassificationId, dataClassificationIdAndTransportRuleAssociation => dataClassificationIdAndTransportRuleAssociation.ReferencingRule);
		}

		// Token: 0x040031F9 RID: 12793
		public static string OutOfBoxDlpPoliciesCollectionName = "MailflowPolicyTemplatesRtm";

		// Token: 0x040031FA RID: 12794
		public static string TenantDlpPoliciesCollectionName = "InstalledMailflowPoliciesRtm";
	}
}
