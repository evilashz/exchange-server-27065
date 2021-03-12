using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E7C RID: 3708
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AuditPolicyUtility
	{
		// Token: 0x060080A1 RID: 32929 RVA: 0x002329F8 File Offset: 0x00230BF8
		static AuditPolicyUtility()
		{
			AuditPolicyUtility.WorkloadToPolicyGuidMap = new Dictionary<Workload, Guid>();
			AuditPolicyUtility.WorkloadToPolicyGuidMap.Add(Workload.Exchange, AuditPolicyUtility.ExchangeAuditPolicyGuid);
			AuditPolicyUtility.WorkloadToPolicyGuidMap.Add(Workload.SharePoint, AuditPolicyUtility.SharepointAuditPolicyGuid);
			AuditPolicyUtility.WorkloadToPolicyGuidMap.Add(Workload.OneDriveForBusiness, AuditPolicyUtility.OneDriveForBusinessAuditPolicyGuid);
			AuditPolicyUtility.WorkloadToRuleGuidMap = new Dictionary<Workload, Guid>();
			AuditPolicyUtility.WorkloadToRuleGuidMap.Add(Workload.Exchange, AuditPolicyUtility.ExchangeAuditRuleGuid);
			AuditPolicyUtility.WorkloadToRuleGuidMap.Add(Workload.SharePoint, AuditPolicyUtility.SharepointAuditRuleGuid);
			AuditPolicyUtility.WorkloadToRuleGuidMap.Add(Workload.OneDriveForBusiness, AuditPolicyUtility.OneDriveForBusinessAuditRuleGuid);
			AuditPolicyUtility.PolicyGuidSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			AuditPolicyUtility.PolicyGuidSet.Add(AuditPolicyUtility.ExchangeAuditPolicyGuid.ToString());
			AuditPolicyUtility.PolicyGuidSet.Add(AuditPolicyUtility.SharepointAuditPolicyGuid.ToString());
			AuditPolicyUtility.PolicyGuidSet.Add(AuditPolicyUtility.OneDriveForBusinessAuditPolicyGuid.ToString());
			AuditPolicyUtility.RuleGuidSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			AuditPolicyUtility.RuleGuidSet.Add(AuditPolicyUtility.ExchangeAuditRuleGuid.ToString());
			AuditPolicyUtility.RuleGuidSet.Add(AuditPolicyUtility.SharepointAuditRuleGuid.ToString());
			AuditPolicyUtility.RuleGuidSet.Add(AuditPolicyUtility.OneDriveForBusinessAuditRuleGuid.ToString());
			AuditPolicyUtility.InitializeAuditOperationConversionMap();
		}

		// Token: 0x060080A2 RID: 32930 RVA: 0x00232BB0 File Offset: 0x00230DB0
		private static void InitializeAuditOperationConversionMap()
		{
			AuditPolicyUtility.AuditOperationConversionMap = new List<MailboxAuditOperations?>(Enumerable.Repeat<MailboxAuditOperations?>(null, 15));
			AuditPolicyUtility.AuditOperationConversionMap[0] = new MailboxAuditOperations?(MailboxAuditOperations.None);
			AuditPolicyUtility.AuditOperationConversionMap[1] = new MailboxAuditOperations?(MailboxAuditOperations.None);
			AuditPolicyUtility.AuditOperationConversionMap[2] = new MailboxAuditOperations?(MailboxAuditOperations.Update | MailboxAuditOperations.Create);
			AuditPolicyUtility.AuditOperationConversionMap[3] = new MailboxAuditOperations?(MailboxAuditOperations.FolderBind | MailboxAuditOperations.MessageBind);
			AuditPolicyUtility.AuditOperationConversionMap[4] = new MailboxAuditOperations?(MailboxAuditOperations.Copy | MailboxAuditOperations.Move);
			AuditPolicyUtility.AuditOperationConversionMap[5] = new MailboxAuditOperations?(MailboxAuditOperations.SoftDelete | MailboxAuditOperations.HardDelete);
			AuditPolicyUtility.AuditOperationConversionMap[6] = new MailboxAuditOperations?(MailboxAuditOperations.None);
			AuditPolicyUtility.AuditOperationConversionMap[7] = new MailboxAuditOperations?(MailboxAuditOperations.SendAs | MailboxAuditOperations.SendOnBehalf);
			AuditPolicyUtility.AuditOperationConversionMap[8] = new MailboxAuditOperations?(MailboxAuditOperations.None);
			AuditPolicyUtility.AuditOperationConversionMap[9] = new MailboxAuditOperations?(MailboxAuditOperations.None);
			AuditPolicyUtility.AuditOperationConversionMap[10] = new MailboxAuditOperations?(MailboxAuditOperations.None);
			AuditPolicyUtility.AuditOperationConversionMap[11] = new MailboxAuditOperations?(MailboxAuditOperations.None);
			AuditPolicyUtility.AuditOperationConversionMap[12] = new MailboxAuditOperations?(MailboxAuditOperations.None);
			AuditPolicyUtility.AuditOperationConversionMap[13] = new MailboxAuditOperations?(MailboxAuditOperations.None);
			AuditPolicyUtility.AuditOperationConversionMap[14] = new MailboxAuditOperations?(MailboxAuditOperations.None);
		}

		// Token: 0x060080A3 RID: 32931 RVA: 0x00232CE9 File Offset: 0x00230EE9
		public static bool GetPolicyGuidFromWorkload(Workload workload, out Guid policyGuid)
		{
			return AuditPolicyUtility.WorkloadToPolicyGuidMap.TryGetValue(workload, out policyGuid);
		}

		// Token: 0x060080A4 RID: 32932 RVA: 0x00232CF7 File Offset: 0x00230EF7
		public static bool GetRuleGuidFromWorkload(Workload workload, out Guid ruleGuid)
		{
			return AuditPolicyUtility.WorkloadToRuleGuidMap.TryGetValue(workload, out ruleGuid);
		}

		// Token: 0x060080A5 RID: 32933 RVA: 0x00232D05 File Offset: 0x00230F05
		public static bool IsAuditConfigurationPolicy(string identity)
		{
			return AuditPolicyUtility.PolicyGuidSet.Contains(AuditPolicyUtility.ExtractAuditConfigurationObjectName(identity));
		}

		// Token: 0x060080A6 RID: 32934 RVA: 0x00232D17 File Offset: 0x00230F17
		public static bool IsAuditConfigurationRule(string identity)
		{
			return AuditPolicyUtility.RuleGuidSet.Contains(AuditPolicyUtility.ExtractAuditConfigurationObjectName(identity));
		}

		// Token: 0x060080A7 RID: 32935 RVA: 0x00232D2C File Offset: 0x00230F2C
		public static MailboxAuditOperations ConvertPolicyOperationsToMailboxOperations(IEnumerable<AuditableOperations> policyOperations)
		{
			MailboxAuditOperations mailboxAuditOperations = MailboxAuditOperations.None;
			if (policyOperations != null)
			{
				foreach (AuditableOperations auditableOperations in policyOperations)
				{
					if (auditableOperations >= AuditableOperations.None && auditableOperations < AuditableOperations.Count)
					{
						mailboxAuditOperations |= AuditPolicyUtility.AuditOperationConversionMap[(int)auditableOperations].Value;
					}
				}
			}
			return mailboxAuditOperations;
		}

		// Token: 0x060080A8 RID: 32936 RVA: 0x00232D94 File Offset: 0x00230F94
		public static void RetrieveAuditPolicy(OrganizationId orgId, out AuditPolicyCacheEntry cacheEntry)
		{
			bool flag;
			if (!AuditPolicyCache.Instance.GetAuditPolicy(orgId, out cacheEntry, out flag, null) || flag)
			{
				PolicyAuditOperations policyAuditOperations;
				PolicyLoadStatus loadStatus = AuditPolicyUtility.LoadAuditableOperations(orgId, Workload.Exchange, out policyAuditOperations);
				AuditPolicyCacheEntry auditPolicyCacheEntry = new AuditPolicyCacheEntry((policyAuditOperations == null) ? MailboxAuditOperations.None : AuditPolicyUtility.ConvertPolicyOperationsToMailboxOperations(policyAuditOperations.AuditOperationsDelegate), loadStatus);
				AuditPolicyCache.Instance.UpdateAuditPolicy(orgId, ref auditPolicyCacheEntry, null);
				cacheEntry = auditPolicyCacheEntry;
			}
		}

		// Token: 0x060080A9 RID: 32937 RVA: 0x00232E24 File Offset: 0x00231024
		public static PolicyLoadStatus LoadAuditableOperations(OrganizationId orgId, Workload workload, out PolicyAuditOperations auditOperations)
		{
			if (orgId == null)
			{
				auditOperations = null;
				return PolicyLoadStatus.Unknown;
			}
			Exception ex = null;
			PolicyLoadStatus loadStatus = PolicyLoadStatus.Unknown;
			PolicyAuditOperations operations = null;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					AuditPolicyUtility.LoadAuditableOperationsInternal(orgId, workload, out loadStatus, out operations);
				});
			}
			catch (GrayException ex2)
			{
				ex = ex2;
			}
			catch (Exception ex3)
			{
				ex = ex3;
				ExWatson.SendReportAndCrashOnAnotherThread(ex3);
			}
			if (ex != null)
			{
				ExTraceGlobals.SessionTracer.TraceError<Workload, OrganizationId, Exception>(0L, "Error occurred while trying to load audit configuration for workload {0} of organization: '{1}'. Exception details: {2}", workload, orgId, ex);
				ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_ErrorLoadAuditPolicyConfiguration, orgId.ToString(), new object[]
				{
					workload,
					orgId,
					ex
				});
			}
			auditOperations = operations;
			return loadStatus;
		}

		// Token: 0x060080AA RID: 32938 RVA: 0x00232F28 File Offset: 0x00231128
		private static void LoadAuditableOperationsInternal(OrganizationId orgId, Workload workload, out PolicyLoadStatus loadStatus, out PolicyAuditOperations operations)
		{
			loadStatus = PolicyLoadStatus.Unknown;
			operations = null;
			Guid guid;
			if (!AuditPolicyUtility.GetRuleGuidFromWorkload(workload, out guid))
			{
				return;
			}
			using (PolicyConfigProvider policyConfigProvider = PolicyConfigProviderManager<ExPolicyConfigProviderManager>.Instance.CreateForProcessingEngine(orgId))
			{
				if (policyConfigProvider != null)
				{
					PolicyConfigConverterBase converterByType = PolicyConfigConverterTable.GetConverterByType(typeof(PolicyRuleConfig), true);
					IConfigurable[] array = converterByType.GetFindStorageObjectsDelegate((ExPolicyConfigProvider)policyConfigProvider)(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, guid.ToString()), ((ExPolicyConfigProvider)policyConfigProvider).GetPolicyConfigContainer(null), true, null);
					if (array != null)
					{
						if (!array.Any<IConfigurable>())
						{
							loadStatus = PolicyLoadStatus.NotExist;
						}
						else
						{
							loadStatus = PolicyLoadStatus.FailedToLoad;
							AuditConfigurationRule auditConfigurationRule = new AuditConfigurationRule((RuleStorage)array[0]);
							auditConfigurationRule.PopulateTaskProperties();
							loadStatus = PolicyLoadStatus.Loaded;
							operations = new PolicyAuditOperations
							{
								AuditOperationsDelegate = auditConfigurationRule.AuditOperation
							};
						}
					}
				}
			}
		}

		// Token: 0x060080AB RID: 32939 RVA: 0x00233010 File Offset: 0x00231210
		private static string ExtractAuditConfigurationObjectName(string identity)
		{
			int num = identity.LastIndexOf('\\');
			if (num == -1)
			{
				num = identity.LastIndexOf('/');
			}
			if (num != -1)
			{
				return identity.Substring(num + 1);
			}
			return identity;
		}

		// Token: 0x040056BC RID: 22204
		private static Guid ExchangeAuditPolicyGuid = new Guid("8D4D2060-EE8E-46A8-8D72-24922956FBA5");

		// Token: 0x040056BD RID: 22205
		private static Guid SharepointAuditPolicyGuid = new Guid("91F20F6F-7EF9-4561-9A38-D771452D5E45");

		// Token: 0x040056BE RID: 22206
		private static Guid OneDriveForBusinessAuditPolicyGuid = new Guid("A415DCCE-19A0-4153-B137-EB6FD67995B5");

		// Token: 0x040056BF RID: 22207
		private static Dictionary<Workload, Guid> WorkloadToPolicyGuidMap = null;

		// Token: 0x040056C0 RID: 22208
		private static Guid ExchangeAuditRuleGuid = new Guid("8740D262-1260-4153-9112-C9BEC24650E0");

		// Token: 0x040056C1 RID: 22209
		private static Guid SharepointAuditRuleGuid = new Guid("989A3A6C-DC40-4FA4-8307-BEB3ECE992E9");

		// Token: 0x040056C2 RID: 22210
		private static Guid OneDriveForBusinessAuditRuleGuid = new Guid("6486E9BE-477A-4615-AE7C-383111FC01B1");

		// Token: 0x040056C3 RID: 22211
		private static Dictionary<Workload, Guid> WorkloadToRuleGuidMap = null;

		// Token: 0x040056C4 RID: 22212
		private static HashSet<string> PolicyGuidSet = null;

		// Token: 0x040056C5 RID: 22213
		private static HashSet<string> RuleGuidSet = null;

		// Token: 0x040056C6 RID: 22214
		private static List<MailboxAuditOperations?> AuditOperationConversionMap;
	}
}
