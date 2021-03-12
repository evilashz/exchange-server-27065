using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000346 RID: 838
	internal class ForwardSyncDataAccessHelper
	{
		// Token: 0x06001CE5 RID: 7397 RVA: 0x0007F99A File Offset: 0x0007DB9A
		public ForwardSyncDataAccessHelper(string serviceInstanceName)
		{
			this.InitializeSession();
			this.serviceInstanceObjectId = SyncServiceInstance.GetServiceInstanceObjectId(serviceInstanceName);
			this.divergenceContainerObjectId = SyncServiceInstance.GetDivergenceContainerId(this.serviceInstanceObjectId);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0007F9C8 File Offset: 0x0007DBC8
		public void InitializeServiceInstanceADStructure()
		{
			if (this.configSession.Read<SyncServiceInstance>(this.serviceInstanceObjectId) == null)
			{
				SyncServiceInstance syncServiceInstance = new SyncServiceInstance();
				syncServiceInstance.SetId(this.serviceInstanceObjectId);
				this.configSession.Save(syncServiceInstance);
			}
			if (this.configSession.Read<ADContainer>(this.divergenceContainerObjectId) == null)
			{
				ADContainer adcontainer = new ADContainer();
				adcontainer.SetId(this.divergenceContainerObjectId);
				this.configSession.Save(adcontainer);
			}
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0007FA3C File Offset: 0x0007DC3C
		public void PersistNewDivergence(SyncObjectId syncObjectId, DateTime divergenceTime, bool incrementalOnly, bool linkRelated, bool temporary, bool tenantWide, string[] errors)
		{
			this.PersistNewDivergence(syncObjectId, divergenceTime, incrementalOnly, linkRelated, temporary, tenantWide, errors, false, true, null);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0007FA60 File Offset: 0x0007DC60
		public void PersistNewDivergence(SyncObjectId syncObjectId, DateTime divergenceTime, bool incrementalOnly, bool linkRelated, bool temporary, bool tenantWide, string[] errors, bool validationDivergence, bool retriable, IDictionary divergenceInfoTable)
		{
			FailedMSOSyncObject failedMSOSyncObject = new FailedMSOSyncObject();
			failedMSOSyncObject.LoadDivergenceInfoXml();
			failedMSOSyncObject.SetId(this.GetDivergenceObjectId(syncObjectId));
			failedMSOSyncObject.ObjectId = syncObjectId;
			failedMSOSyncObject.DivergenceTimestamp = new DateTime?(divergenceTime);
			failedMSOSyncObject.IsIncrementalOnly = incrementalOnly;
			failedMSOSyncObject.IsLinkRelated = linkRelated;
			failedMSOSyncObject.IsTemporary = temporary;
			failedMSOSyncObject.DivergenceCount = 1;
			failedMSOSyncObject.IsTenantWideDivergence = tenantWide;
			failedMSOSyncObject.IsValidationDivergence = validationDivergence;
			failedMSOSyncObject.IsRetriable = retriable;
			if (failedMSOSyncObject.IsValidationDivergence)
			{
				failedMSOSyncObject.IsIgnoredInHaltCondition = true;
			}
			else
			{
				failedMSOSyncObject.IsIgnoredInHaltCondition = false;
			}
			failedMSOSyncObject.Errors = new MultiValuedProperty<string>();
			if (errors != null)
			{
				ForwardSyncDataAccessHelper.AddErrors(errors, failedMSOSyncObject);
			}
			if (divergenceInfoTable != null)
			{
				ForwardSyncDataAccessHelper.SetDivergenceInfoValues(divergenceInfoTable, failedMSOSyncObject);
				failedMSOSyncObject.SaveDivergenceInfoXml();
			}
			this.SaveDivergence(failedMSOSyncObject);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0007FB18 File Offset: 0x0007DD18
		public virtual FailedMSOSyncObject GetExistingDivergence(SyncObjectId syncObjectId)
		{
			ADObjectId divergenceObjectId = this.GetDivergenceObjectId(syncObjectId);
			return this.configSession.Read<FailedMSOSyncObject>(divergenceObjectId);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0007FB49 File Offset: 0x0007DD49
		public virtual IEnumerable<FailedMSOSyncObject> FindDivergence(QueryFilter filter)
		{
			return from x in this.configSession.FindPaged<FailedMSOSyncObject>(this.divergenceContainerObjectId, QueryScope.OneLevel, filter, null, 0)
			where !string.IsNullOrEmpty(x.ExternalDirectoryObjectId)
			select x;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0007FB84 File Offset: 0x0007DD84
		public void UpdateExistingDivergence(FailedMSOSyncObject divergence, int occurenceCount, bool incrementalOnly, bool linkRelated, bool temporary, bool tenantWide, string[] errors, int errorListLengthLimit)
		{
			this.UpdateExistingDivergence(divergence, occurenceCount, incrementalOnly, linkRelated, temporary, tenantWide, errors, errorListLengthLimit, false, true, null);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0007FBA8 File Offset: 0x0007DDA8
		public void UpdateExistingDivergence(FailedMSOSyncObject divergence, int occurenceCount, bool incrementalOnly, bool linkRelated, bool temporary, bool tenantWide, string[] errors, int errorListLengthLimit, bool validationDivergence, bool retriable, IDictionary divergenceInfoTable)
		{
			divergence.IsIncrementalOnly = (divergence.IsIncrementalOnly && incrementalOnly);
			divergence.IsLinkRelated = (divergence.IsLinkRelated || linkRelated);
			divergence.IsTemporary = temporary;
			divergence.IsTenantWideDivergence = (divergence.IsTenantWideDivergence || tenantWide);
			divergence.IsValidationDivergence = (divergence.IsValidationDivergence && validationDivergence);
			divergence.IsRetriable = retriable;
			if (!validationDivergence && divergence.IsValidationDivergence)
			{
				divergence.DivergenceCount = 1;
			}
			else
			{
				divergence.DivergenceCount += occurenceCount;
			}
			if (divergence.Errors == null)
			{
				divergence.Errors = new MultiValuedProperty<string>();
			}
			if (errors != null)
			{
				ForwardSyncDataAccessHelper.AddErrors(errors, divergence);
			}
			while (divergence.Errors.Count > 0 && divergence.Errors.Count > errorListLengthLimit)
			{
				divergence.Errors.RemoveAt(0);
			}
			if (divergenceInfoTable != null)
			{
				ForwardSyncDataAccessHelper.SetDivergenceInfoValues(divergenceInfoTable, divergence);
				divergence.SaveDivergenceInfoXml();
			}
			this.SaveDivergence(divergence);
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0007FC86 File Offset: 0x0007DE86
		public virtual void DeleteDivergence(FailedMSOSyncObject divergence)
		{
			ForwardSyncDataAccessHelper.CleanUpDivergenceIds(this.configSession, divergence);
			this.configSession.Delete(divergence);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0007FCA0 File Offset: 0x0007DEA0
		internal static IConfigurationSession CreateSession(bool isReadOnly = false)
		{
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(isReadOnly, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 241, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\ForwardSyncDataAccessHelper.cs");
			configurationSession.UseConfigNC = false;
			return configurationSession;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0007FCD6 File Offset: 0x0007DED6
		internal static void CleanUpDivergenceIds(IConfigurationSession session, FailedMSOSyncObject divergence)
		{
			divergence.ExternalDirectoryOrganizationId = null;
			divergence.ExternalDirectoryObjectId = null;
			session.Save(divergence);
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0007FCF0 File Offset: 0x0007DEF0
		internal static ObjectId GetRootId(FailedMSOSyncObjectIdParameter identityParameter)
		{
			if (identityParameter != null && identityParameter.IsServiceInstanceDefinied)
			{
				ADObjectId adobjectId = SyncServiceInstance.GetServiceInstanceObjectId(identityParameter.ServiceInstance.InstanceId);
				return SyncServiceInstance.GetDivergenceContainerId(adobjectId);
			}
			return SyncServiceInstance.GetMsoSyncRootContainer();
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0007FD25 File Offset: 0x0007DF25
		protected virtual void SaveDivergence(FailedMSOSyncObject divergence)
		{
			this.configSession.Save(divergence);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0007FD33 File Offset: 0x0007DF33
		protected virtual ADObjectId GetDivergenceObjectId(SyncObjectId syncObjectId)
		{
			return this.divergenceContainerObjectId.GetChildId(FailedMSOSyncObject.GetObjectName(syncObjectId));
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0007FD48 File Offset: 0x0007DF48
		private static void SetDivergenceInfoValues(IDictionary divergenceInfoTable, FailedMSOSyncObject divergence)
		{
			Type type = divergence.GetType();
			foreach (object obj in divergenceInfoTable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (dictionaryEntry.Key != null && dictionaryEntry.Value != null)
				{
					PropertyInfo property = type.GetProperty(dictionaryEntry.Key.ToString());
					if (property != null)
					{
						property.SetValue(divergence, (string)dictionaryEntry.Value, null);
					}
				}
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0007FDE4 File Offset: 0x0007DFE4
		private static void AddErrors(IEnumerable<string> errors, FailedMSOSyncObject divergence)
		{
			foreach (string text in errors)
			{
				string item = text;
				if (text.Length > 100000)
				{
					item = text.Substring(0, 100000);
				}
				if (!divergence.Errors.Contains(item))
				{
					divergence.Errors.Add(item);
				}
				if (text.Contains("WorkflowDelayCreationException") && divergence.IsLinkRelated && divergence.ObjectId.ObjectClass == DirectoryObjectClass.Group)
				{
					divergence.Comment = "PSDivergenceV2_Ignore, GroupMailbox divergence should be ignored by V2";
				}
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0007FE8C File Offset: 0x0007E08C
		private void InitializeSession()
		{
			this.configSession = ForwardSyncDataAccessHelper.CreateSession(false);
		}

		// Token: 0x04001869 RID: 6249
		private const int MaxErrorLength = 100000;

		// Token: 0x0400186A RID: 6250
		private readonly ADObjectId serviceInstanceObjectId;

		// Token: 0x0400186B RID: 6251
		private readonly ADObjectId divergenceContainerObjectId;

		// Token: 0x0400186C RID: 6252
		private IConfigurationSession configSession;
	}
}
