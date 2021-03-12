using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200092A RID: 2346
	internal class MessagingPoliciesSyncLogDataSession : IConfigDataProvider
	{
		// Token: 0x060053B7 RID: 21431 RVA: 0x00159DAC File Offset: 0x00157FAC
		public MessagingPoliciesSyncLogDataSession(IConfigDataProvider dataSession, string rulesCollectionName = null, string policiesCollectionName = null)
		{
			if (dataSession == null)
			{
				throw new ArgumentNullException("dataSession");
			}
			this.dataSession = dataSession;
			this.rulesCollectionName = (rulesCollectionName ?? Utils.RuleCollectionNameFromRole());
			this.policiesCollectionName = (policiesCollectionName ?? DlpUtils.TenantDlpPoliciesCollectionName);
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x060053B8 RID: 21432 RVA: 0x00159DEC File Offset: 0x00157FEC
		internal string LastUsedDc
		{
			get
			{
				IConfigurationSession configurationSession = this.dataSession as IConfigurationSession;
				if (configurationSession == null)
				{
					return null;
				}
				return configurationSession.LastUsedDc;
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x060053B9 RID: 21433 RVA: 0x00159E10 File Offset: 0x00158010
		internal ADSessionSettings SessionSettings
		{
			get
			{
				IConfigurationSession configurationSession = this.dataSession as IConfigurationSession;
				if (configurationSession == null)
				{
					return null;
				}
				return configurationSession.SessionSettings;
			}
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x00159E34 File Offset: 0x00158034
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			return this.dataSession.Read<T>(identity);
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x00159E42 File Offset: 0x00158042
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			return this.dataSession.Find<T>(filter, rootId, deepSearch, sortBy);
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x00159E54 File Offset: 0x00158054
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			return this.dataSession.FindPaged<T>(filter, rootId, deepSearch, sortBy, pageSize);
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x00159E68 File Offset: 0x00158068
		void IConfigDataProvider.Save(IConfigurable instance)
		{
			if (!TenantSettingSyncLogGenerator.Instance.Enabled)
			{
				this.dataSession.Save(instance);
				return;
			}
			if (instance is TransportRule)
			{
				this.SaveTransportRule((TransportRule)instance);
				return;
			}
			if (instance is ADComplianceProgram)
			{
				this.SaveDlpPolicy((ADComplianceProgram)instance);
				return;
			}
			this.dataSession.Save(instance);
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x00159EC4 File Offset: 0x001580C4
		void IConfigDataProvider.Delete(IConfigurable instance)
		{
			this.dataSession.Delete(instance);
			Guid value;
			if (TenantSettingSyncLogGenerator.Instance.Enabled && (instance is TransportRule || instance is ADComplianceProgram) && this.GetExternalDirectoryOrganizationIdToLog((ADObject)instance, out value))
			{
				TenantSettingSyncLogGenerator.Instance.LogChangesForDelete((ADObject)instance, new Guid?(value));
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x060053BF RID: 21439 RVA: 0x00159F20 File Offset: 0x00158120
		string IConfigDataProvider.Source
		{
			get
			{
				return this.dataSession.Source;
			}
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x00159F30 File Offset: 0x00158130
		private void SaveTransportRule(TransportRule instance)
		{
			bool flag = MessagingPoliciesSyncLogDataSession.IsNameNewOrChangedForTenantScopedObject(instance);
			Guid empty = Guid.Empty;
			bool flag2 = false;
			if (instance.OrganizationId != OrganizationId.ForestWideOrgId && instance.IsChanged(TransportRuleSchema.Xml))
			{
				flag2 = true;
				if (!instance.Guid.Equals(Guid.Empty))
				{
					TransportRule transportRuleByGuid = DlpUtils.GetTransportRuleByGuid(this.dataSession, this.rulesCollectionName, instance.Guid);
					TransportRule transportRule = (TransportRule)TransportRuleParser.Instance.GetRule(transportRuleByGuid.Xml);
					transportRule.TryGetDlpPolicyId(out empty);
				}
			}
			this.dataSession.Save(instance);
			if (flag || flag2)
			{
				TransportRule transportRuleByName = DlpUtils.GetTransportRuleByName(this.dataSession, this.rulesCollectionName, instance.Name);
				Guid empty2 = Guid.Empty;
				List<KeyValuePair<string, object>> list = null;
				if (transportRuleByName != null)
				{
					TransportRule transportRule2 = (TransportRule)TransportRuleParser.Instance.GetRule(transportRuleByName.Xml);
					transportRule2.TryGetDlpPolicyId(out empty2);
					if (!empty2.Equals(empty))
					{
						flag = true;
						list = new List<KeyValuePair<string, object>>();
						list.Add(new KeyValuePair<string, object>("DLPPolicyId", empty2));
					}
				}
				Guid value;
				if (flag && this.GetExternalDirectoryOrganizationIdToLog(transportRuleByName, out value))
				{
					TenantSettingSyncLogGenerator.Instance.LogChangesForSave(transportRuleByName, new Guid?(value), new Guid?(transportRuleByName.ImmutableId), list);
				}
			}
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0015A078 File Offset: 0x00158278
		private void SaveDlpPolicy(ADComplianceProgram instance)
		{
			bool flag = MessagingPoliciesSyncLogDataSession.IsNameNewOrChangedForTenantScopedObject(instance);
			this.dataSession.Save(instance);
			if (flag)
			{
				ADComplianceProgram dlpPolicyByName = DlpUtils.GetDlpPolicyByName(this.dataSession, this.policiesCollectionName, instance.Name);
				Guid value;
				if (this.GetExternalDirectoryOrganizationIdToLog(dlpPolicyByName, out value))
				{
					TenantSettingSyncLogGenerator.Instance.LogChangesForSave(dlpPolicyByName, new Guid?(value), new Guid?(dlpPolicyByName.ImmutableId), null);
				}
			}
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x0015A0DC File Offset: 0x001582DC
		private static bool IsNameNewOrChangedForTenantScopedObject(ADObject instance)
		{
			return instance.OrganizationId != OrganizationId.ForestWideOrgId && (instance.IsChanged(ADObjectSchema.Id) || instance.IsChanged(ADObjectSchema.RawName));
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x0015A10C File Offset: 0x0015830C
		private bool GetExternalDirectoryOrganizationIdToLog(ADObject instance, out Guid externalDirectoryOrganizationId)
		{
			externalDirectoryOrganizationId = Guid.Empty;
			if (instance != null && instance.OrganizationId != OrganizationId.ForestWideOrgId)
			{
				ADOperationResult externalDirectoryOrganizationId2 = SystemConfigurationTasksHelper.GetExternalDirectoryOrganizationId(this.dataSession, instance.OrganizationId, out externalDirectoryOrganizationId);
				TenantSettingSyncLogGenerator.Instance.AddEventLogOnADException(externalDirectoryOrganizationId2);
				return externalDirectoryOrganizationId2.Succeeded && externalDirectoryOrganizationId != Guid.Empty;
			}
			return false;
		}

		// Token: 0x04003101 RID: 12545
		private IConfigDataProvider dataSession;

		// Token: 0x04003102 RID: 12546
		private readonly string rulesCollectionName;

		// Token: 0x04003103 RID: 12547
		private readonly string policiesCollectionName;
	}
}
