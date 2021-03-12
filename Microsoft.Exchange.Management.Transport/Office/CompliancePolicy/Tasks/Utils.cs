using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;
using Microsoft.Office.CompliancePolicy.Validators;
using Microsoft.SharePoint.Client;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x02000136 RID: 310
	internal class Utils
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003110A File Offset: 0x0002F30A
		internal static bool ValidateContentDateParameter(DateTime? contentDateFrom, DateTime? contentDateTo)
		{
			return contentDateFrom == null || contentDateTo == null || contentDateFrom.Value < contentDateTo.Value;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00031133 File Offset: 0x0002F333
		internal static void ValidateNotForestWideOrganization(OrganizationId organizationId)
		{
			if (Globals.IsMicrosoftHostedOnly && organizationId == OrganizationId.ForestWideOrgId)
			{
				throw new ForestWideOrganizationException();
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003114F File Offset: 0x0002F34F
		internal static void ThrowIfNotRunInEOP()
		{
			if (!ExPolicyConfigProvider.IsFFOOnline)
			{
				throw new ErrorOnlyAllowInEopException();
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003115E File Offset: 0x0002F35E
		internal static void ValidateWorkloadParameter(Workload workload)
		{
			if (workload != Workload.Exchange && workload != Workload.SharePoint && workload != (Workload.Exchange | Workload.SharePoint))
			{
				throw new InvalidCompliancePolicyWorkloadException();
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003117C File Offset: 0x0002F37C
		internal static void ThrowIfRulesInPolicyAreTooAdvanced(IEnumerable<RuleStorage> ruleStorages, PolicyStorage policyStorage, Task task, IConfigurationSession datasession)
		{
			foreach (PsComplianceRuleBase psComplianceRuleBase in from x in ruleStorages
			select new PsComplianceRuleBase(x))
			{
				psComplianceRuleBase.PopulateTaskProperties(task, datasession);
				if (psComplianceRuleBase.ReadOnly)
				{
					throw new RulesInPolicyIsTooAdvancedToModifyException(policyStorage.Name, psComplianceRuleBase.Name);
				}
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00031220 File Offset: 0x0002F420
		internal static void ValidateDataClassification(IEnumerable<Hashtable> hashtables)
		{
			ArgumentValidator.ThrowIfNull("hashtables", hashtables);
			foreach (Hashtable hashtable in hashtables)
			{
				IEnumerable<string> source = hashtable.Keys.Cast<string>();
				if (!source.Contains("name", StringComparer.InvariantCultureIgnoreCase))
				{
					throw new SensitiveInformationDoesNotContainIdException();
				}
				using (IEnumerator<string> enumerator2 = (from key in source
				where !PsContentContainsSensitiveInformationPredicate.CmdletParameterNameToEngineKeyMapping.Keys.Contains(key, StringComparer.InvariantCultureIgnoreCase)
				select key).GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						string invalidParameter = enumerator2.Current;
						throw new InvalidSensitiveInformationParameterNameException(invalidParameter);
					}
				}
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x000312F4 File Offset: 0x0002F4F4
		internal static void ValidateAccessScopeIsPredicate(AccessScope? accessScope)
		{
			if (accessScope != null && !Utils.SupporttedAccessScopes.Contains(accessScope.Value))
			{
				throw new InvalidAccessScopeIsPredicateException();
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00031318 File Offset: 0x0002F518
		internal static BindingStorage CreateNewBindingStorage(ADObjectId tenantId, Workload workload, Guid policyId)
		{
			string text = workload.ToString() + policyId.ToString();
			BindingStorage bindingStorage = new BindingStorage
			{
				MasterIdentity = Guid.NewGuid(),
				Name = text,
				PolicyId = policyId,
				Workload = workload
			};
			bindingStorage[ADObjectSchema.OrganizationalUnitRoot] = tenantId;
			bindingStorage.SetId(tenantId.GetDescendantId(PolicyStorage.PoliciesContainer).GetChildId(policyId.ToString()).GetChildId(text));
			return bindingStorage;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000313E4 File Offset: 0x0002F5E4
		internal static void MergeBindings(MultiValuedProperty<BindingMetadata> bindings, MultiValuedProperty<BindingMetadata> addedBindings, MultiValuedProperty<BindingMetadata> removedBindings, bool forceClear)
		{
			ArgumentValidator.ThrowIfNull("bindings", bindings);
			ArgumentValidator.ThrowIfNull("addedBindings", addedBindings);
			ArgumentValidator.ThrowIfNull("removedBindings", removedBindings);
			if (forceClear && bindings.Any<BindingMetadata>())
			{
				bindings.Clear();
				return;
			}
			using (MultiValuedProperty<BindingMetadata>.Enumerator enumerator = removedBindings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BindingMetadata item = enumerator.Current;
					BindingMetadata bindingMetadata = bindings.FirstOrDefault((BindingMetadata p) => p.ImmutableIdentity == item.ImmutableIdentity);
					if (bindingMetadata != null)
					{
						bindings.Remove(bindingMetadata);
					}
				}
			}
			using (MultiValuedProperty<BindingMetadata>.Enumerator enumerator2 = addedBindings.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					BindingMetadata item = enumerator2.Current;
					BindingMetadata bindingMetadata2 = bindings.FirstOrDefault((BindingMetadata p) => p.ImmutableIdentity == item.ImmutableIdentity);
					if (bindingMetadata2 == null)
					{
						bindings.Add(item);
					}
					else if (!string.Equals(bindingMetadata2.DisplayName, item.DisplayName, StringComparison.InvariantCulture) || !string.Equals(bindingMetadata2.Name, item.Name, StringComparison.InvariantCultureIgnoreCase))
					{
						int index = bindings.IndexOf(bindingMetadata2);
						bindings[index] = item;
					}
				}
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000315B0 File Offset: 0x0002F7B0
		internal static void PopulateScopeStorages(BindingStorage bindingStorage, MultiValuedProperty<BindingMetadata> scopes)
		{
			ArgumentValidator.ThrowIfNull("bindingStorage", bindingStorage);
			ArgumentValidator.ThrowIfNull("scopes", scopes);
			if (scopes.Changed)
			{
				object[] removed = scopes.Removed;
				for (int i = 0; i < removed.Length; i++)
				{
					BindingMetadata removedScope = (BindingMetadata)removed[i];
					ScopeStorage scopeStorage = bindingStorage.AppliedScopes.Find((ScopeStorage item) => string.Equals(BindingMetadata.FromStorage(item.Scope).ImmutableIdentity, removedScope.ImmutableIdentity, StringComparison.OrdinalIgnoreCase));
					scopeStorage.Mode = Mode.PendingDeletion;
					scopeStorage.PolicyVersion = CombGuidGenerator.NewGuid();
				}
				object[] added = scopes.Added;
				for (int j = 0; j < added.Length; j++)
				{
					BindingMetadata addedScope = (BindingMetadata)added[j];
					ScopeStorage scopeStorage2 = bindingStorage.AppliedScopes.Find((ScopeStorage item) => string.Equals(BindingMetadata.FromStorage(item.Scope).ImmutableIdentity, addedScope.ImmutableIdentity, StringComparison.OrdinalIgnoreCase));
					if (scopeStorage2 == null)
					{
						Guid objectGuid = Guid.NewGuid();
						scopeStorage2 = new ScopeStorage();
						scopeStorage2[ADObjectSchema.OrganizationalUnitRoot] = bindingStorage.OrganizationalUnitRoot;
						scopeStorage2.Name = objectGuid.ToString();
						scopeStorage2.SetId(new ADObjectId(PolicyStorage.PoliciesContainer.GetChildId(scopeStorage2.Name).DistinguishedName, objectGuid));
						bindingStorage.AppliedScopes.Add(scopeStorage2);
					}
					scopeStorage2.Mode = Mode.Enforce;
					scopeStorage2.Scope = BindingMetadata.ToStorage(addedScope);
					scopeStorage2.PolicyVersion = CombGuidGenerator.NewGuid();
				}
				bindingStorage.PolicyVersion = CombGuidGenerator.NewGuid();
				scopes.ResetChangeTracking();
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00031734 File Offset: 0x0002F934
		internal static MultiValuedProperty<BindingMetadata> GetScopesFromStorage(BindingStorage bindingStorage)
		{
			ArgumentValidator.ThrowIfNull("bindingStorage", bindingStorage);
			MultiValuedProperty<BindingMetadata> multiValuedProperty = new MultiValuedProperty<BindingMetadata>();
			if (bindingStorage.AppliedScopes.Any<ScopeStorage>())
			{
				foreach (ScopeStorage scopeStorage in bindingStorage.AppliedScopes)
				{
					if (scopeStorage.Mode == Mode.Enforce)
					{
						multiValuedProperty.TryAdd(BindingMetadata.FromStorage(scopeStorage.Scope));
					}
				}
			}
			multiValuedProperty.ResetChangeTracking();
			return multiValuedProperty;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000317C0 File Offset: 0x0002F9C0
		internal static Guid GetUniversalIdentity(UnifiedPolicyStorageBase storageObject)
		{
			ArgumentValidator.ThrowIfNull("storageObject", storageObject);
			if (!(storageObject.MasterIdentity == Guid.Empty))
			{
				return storageObject.MasterIdentity;
			}
			return storageObject.Guid;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x000317EC File Offset: 0x0002F9EC
		internal static IList<BindingStorage> LoadBindingStoragesByPolicy(IConfigDataProvider dataSession, UnifiedPolicyStorageBase policyStorage)
		{
			ArgumentValidator.ThrowIfNull("dataSession", dataSession);
			ArgumentValidator.ThrowIfNull("policyStorage", policyStorage);
			Guid universalIdentity = Utils.GetUniversalIdentity(policyStorage);
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ExPolicyConfigProvider.IsFFOOnline ? UnifiedPolicyStorageBaseSchema.ContainerProp : BindingStorageSchema.PolicyId, universalIdentity);
			return dataSession.Find<BindingStorage>(filter, null, false, null).Cast<BindingStorage>().ToList<BindingStorage>();
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003184C File Offset: 0x0002FA4C
		internal static ObjectId GetRootId(IConfigDataProvider dataSession)
		{
			ExPolicyConfigProvider exPolicyConfigProvider = dataSession as ExPolicyConfigProvider;
			if (exPolicyConfigProvider != null)
			{
				return exPolicyConfigProvider.GetPolicyConfigContainer(null);
			}
			return null;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000318A0 File Offset: 0x0002FAA0
		internal static IList<RuleStorage> LoadRuleStoragesByPolicy(IConfigDataProvider dataSession, PolicyStorage policyStorage, ObjectId rootId)
		{
			Guid policyId = Utils.GetUniversalIdentity(policyStorage);
			return (from RuleStorage x in dataSession.Find<RuleStorage>(new ComparisonFilter(ComparisonOperator.Equal, RuleStorageSchema.ParentPolicyId, policyId), rootId, true, null)
			where x.ParentPolicyId.Equals(policyId)
			select x).ToList<RuleStorage>();
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00031914 File Offset: 0x0002FB14
		internal static IList<PolicyStorage> LoadPolicyStorages(IConfigDataProvider dataSession, PolicyScenario scenarioType)
		{
			return (from PolicyStorage s in dataSession.Find<PolicyStorage>(null, Utils.GetRootId(dataSession), false, null)
			where s.Scenario == scenarioType
			select s).ToList<PolicyStorage>();
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00031958 File Offset: 0x0002FB58
		internal static void RemovePolicyStorageBase(IConfigDataProvider dataSession, WriteVerboseDelegate writeVerboseDelegate, IEnumerable<UnifiedPolicyStorageBase> policyStorageBases)
		{
			ArgumentValidator.ThrowIfNull("dataSession", dataSession);
			ArgumentValidator.ThrowIfNull("writeVerboseDelegate", writeVerboseDelegate);
			if (policyStorageBases != null)
			{
				foreach (UnifiedPolicyStorageBase unifiedPolicyStorageBase in policyStorageBases)
				{
					writeVerboseDelegate(Strings.VerboseDeletePolicyStorageBaseObject(unifiedPolicyStorageBase.Name, unifiedPolicyStorageBase.GetType().Name));
					dataSession.Delete(unifiedPolicyStorageBase);
				}
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000319D8 File Offset: 0x0002FBD8
		internal static bool ExecutingUserIsForestWideAdmin(Task task)
		{
			return task.ExecutingUserOrganizationId == null || task.ExecutingUserOrganizationId.Equals(OrganizationId.ForestWideOrgId);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000319FA File Offset: 0x0002FBFA
		internal static MultiValuedProperty<string> BindingParameterGetter(object bindingParameter)
		{
			if (bindingParameter != null)
			{
				return (MultiValuedProperty<string>)bindingParameter;
			}
			return MultiValuedProperty<string>.Empty;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00031A0B File Offset: 0x0002FC0B
		internal static MultiValuedProperty<string> BindingParameterSetter(MultiValuedProperty<string> value)
		{
			return value ?? MultiValuedProperty<string>.Empty;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00031A18 File Offset: 0x0002FC18
		internal static ADUser GetUserObjectByExternalDirectoryObjectId(string externalDirectoryObjectId, IConfigurationSession configurationSession)
		{
			if (string.IsNullOrWhiteSpace(externalDirectoryObjectId))
			{
				return null;
			}
			if (!ExPolicyConfigProvider.IsFFOOnline)
			{
				return Utils.GetRecipientSession(configurationSession).FindADUserByExternalDirectoryObjectId(externalDirectoryObjectId);
			}
			IEnumerable<ADUser> source = UserIdParameter.Parse(externalDirectoryObjectId).GetObjects<ADUser>(null, Utils.GetRecipientSession(configurationSession)).ToArray<ADUser>();
			if (source.Count<ADUser>() == 1)
			{
				return source.First<ADUser>();
			}
			return null;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00031A6C File Offset: 0x0002FC6C
		internal static ADUser GetUserObjectByObjectId(ADObjectId objectId, IConfigurationSession configurationSession)
		{
			if (objectId == null)
			{
				return null;
			}
			return Utils.GetRecipientSession(configurationSession).FindADUserByObjectId(objectId);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00031A80 File Offset: 0x0002FC80
		internal static IRecipientSession GetRecipientSession(IConfigurationSession configurationSession)
		{
			OrganizationId organizationId = configurationSession.GetOrgContainer().OrganizationId;
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromExternalDirectoryOrganizationId(new Guid(organizationId.ToExternalDirectoryOrganizationId())), 803, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\UnifiedPolicy\\Utils.cs");
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00031AC4 File Offset: 0x0002FCC4
		internal static string ConvertObjectIdentityInFfo(string identity)
		{
			if (!string.IsNullOrWhiteSpace(identity) && ExPolicyConfigProvider.IsFFOOnline)
			{
				string[] array = identity.Split(new string[]
				{
					string.Format("/{0}/", "Microsoft Exchange Hosted Organizations"),
					"/Configuration/"
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Count<string>() == 3)
				{
					return string.Format("{0}\\{1}", Regex.Unescape(array[1]), Regex.Unescape(array[2]));
				}
			}
			return identity;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00031B2F File Offset: 0x0002FD2F
		internal static void RedactBinding(BindingMetadata binding, bool redactImmutableId)
		{
			binding.DisplayName = SuppressingPiiData.Redact(binding.DisplayName);
			binding.Name = SuppressingPiiData.Redact(binding.Name);
			if (redactImmutableId)
			{
				binding.ImmutableIdentity = SuppressingPiiData.Redact(binding.ImmutableIdentity);
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00031B67 File Offset: 0x0002FD67
		internal static PiiMap GetSessionPiiMap(ExchangeRunspaceConfiguration config)
		{
			if (config != null && config.PiiMapId != null)
			{
				return PiiMapManager.Instance.GetOrAdd(config.PiiMapId);
			}
			return null;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00031BB0 File Offset: 0x0002FDB0
		internal static void WrapSharePointCsomCall(Uri siteUrl, ICredentials credentials, Action<ClientContext> doWork)
		{
			try
			{
				using (ClientContext clientContext = new ClientContext(siteUrl))
				{
					clientContext.Credentials = credentials;
					clientContext.ExecutingWebRequest += delegate(object sender, WebRequestEventArgs args)
					{
						args.WebRequestExecutor.RequestHeaders.Add(HttpRequestHeader.Authorization, "Bearer");
						args.WebRequestExecutor.WebRequest.PreAuthenticate = true;
					};
					doWork(clientContext);
				}
			}
			catch (CommunicationException ex)
			{
				throw new SpCsomCallException(Strings.ErrorSharePointCallFailed(ex.Message), ex);
			}
			catch (ClientRequestException ex2)
			{
				throw new SpCsomCallException(Strings.ErrorSharePointCallFailed(ex2.Message), ex2);
			}
			catch (ServerException ex3)
			{
				throw new SpCsomCallException(Strings.ErrorSharePointCallFailed(ex3.Message), ex3);
			}
			catch (TimeoutException ex4)
			{
				throw new SpCsomCallException(Strings.ErrorSharePointCallFailed(ex4.Message), ex4);
			}
			catch (IOException ex5)
			{
				throw new SpCsomCallException(Strings.ErrorSharePointCallFailed(ex5.Message), ex5);
			}
			catch (WebException ex6)
			{
				throw new SpCsomCallException(Strings.ErrorSharePointCallFailed(ex6.Message), ex6);
			}
			catch (FormatException ex7)
			{
				throw new SpCsomCallException(Strings.ErrorSharePointCallFailed(ex7.Message), ex7);
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00031CFC File Offset: 0x0002FEFC
		internal static bool TryGetMockCsomProvider(out ISharepointCsomProvider mockCsomProvider)
		{
			mockCsomProvider = null;
			string stringFromConfig = Utils.GetStringFromConfig("MockCsomProviderType");
			string stringFromConfig2 = Utils.GetStringFromConfig("MockCsomProviderAssemblyPath");
			if (!string.IsNullOrWhiteSpace(stringFromConfig) && File.Exists(stringFromConfig2))
			{
				string stringFromConfig3 = Utils.GetStringFromConfig("MockSharepointSites");
				Assembly assembly = Assembly.LoadFrom(stringFromConfig2);
				mockCsomProvider = (ISharepointCsomProvider)Activator.CreateInstance(assembly.GetType(stringFromConfig, true), new object[]
				{
					stringFromConfig3
				});
				return true;
			}
			return false;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00031D6C File Offset: 0x0002FF6C
		internal static bool GetBoolFromConfig(string key, bool defaultValue = false)
		{
			bool result;
			if (!bool.TryParse(Utils.GetStringFromConfig(key), out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00031D8C File Offset: 0x0002FF8C
		internal static int GetIntFromConfig(string key, int defaultValue)
		{
			int result;
			if (!int.TryParse(Utils.GetStringFromConfig(key), out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00031DAC File Offset: 0x0002FFAC
		internal static string GetStringFromConfig(string key)
		{
			string result = null;
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
			if (configuration != null)
			{
				KeyValueConfigurationElement keyValueConfigurationElement = configuration.AppSettings.Settings[key];
				if (keyValueConfigurationElement != null && !string.IsNullOrEmpty(keyValueConfigurationElement.Value))
				{
					result = keyValueConfigurationElement.Value;
				}
			}
			return result;
		}

		// Token: 0x0400045D RID: 1117
		internal const string NameParameterName = "Name";

		// Token: 0x0400045E RID: 1118
		internal const string PolicyParameterName = "Policy";

		// Token: 0x0400045F RID: 1119
		internal const string AddExchangeBindingParameterName = "AddExchangeBinding";

		// Token: 0x04000460 RID: 1120
		internal const string RemoveExchangeBindingParameterName = "RemoveExchangeBinding";

		// Token: 0x04000461 RID: 1121
		internal const string AddSharePointBindingParameterName = "AddSharePointBinding";

		// Token: 0x04000462 RID: 1122
		internal const string RemoveSharePointBindingParameterName = "RemoveSharePointBinding";

		// Token: 0x04000463 RID: 1123
		internal const string AddOneDriveBindingParameterName = "AddOneDriveBinding";

		// Token: 0x04000464 RID: 1124
		internal const string RemoveOneDriveBindingParameterName = "RemoveOneDriveBinding";

		// Token: 0x04000465 RID: 1125
		internal const string HoldContentParameterName = "HoldContent";

		// Token: 0x04000466 RID: 1126
		internal const string ContentDateFromParameterName = "ContentDateFrom";

		// Token: 0x04000467 RID: 1127
		internal const string ContentDateToParameterName = "ContentDateTo";

		// Token: 0x04000468 RID: 1128
		internal const string ContentMatchQueryParameterName = "ContentMatchQuery";

		// Token: 0x04000469 RID: 1129
		internal const string CommentParameterName = "Comment";

		// Token: 0x0400046A RID: 1130
		internal const string EnabledParameterName = "Enabled";

		// Token: 0x0400046B RID: 1131
		internal const string PasswordRequiredName = "PasswordRequired";

		// Token: 0x0400046C RID: 1132
		internal const string ForceDeletionParameterName = "ForceDeletion";

		// Token: 0x0400046D RID: 1133
		internal const string AuditOperationParameterName = "AuditOperation";

		// Token: 0x0400046E RID: 1134
		internal const string TargetGroupsParameterName = "TargetGroups";

		// Token: 0x0400046F RID: 1135
		internal const string ExclusionListParameterName = "ExclusionList";

		// Token: 0x04000470 RID: 1136
		internal const string ForceParameterName = "Force";

		// Token: 0x04000471 RID: 1137
		internal const string FullSyncParameterName = "FullSync";

		// Token: 0x04000472 RID: 1138
		internal const string RetryDistributionParameterName = "RetryDistribution";

		// Token: 0x04000473 RID: 1139
		internal const string UpdateObjectIdParameterName = "UpdateObjectId";

		// Token: 0x04000474 RID: 1140
		internal const string DeleteObjectIdParameterName = "DeleteObjectId";

		// Token: 0x04000475 RID: 1141
		internal const string ObjectTypeParameterName = "ObjectType";

		// Token: 0x04000476 RID: 1142
		internal const string WorkloadParameterName = "Workload";

		// Token: 0x04000477 RID: 1143
		internal const string PolicyCenterSiteOwnerParameterName = "PolicyCenterSiteOwner";

		// Token: 0x04000478 RID: 1144
		internal const string LoadOnlyParameterName = "LoadOnly";

		// Token: 0x04000479 RID: 1145
		internal const string ForceInitializeParameterName = "ForceInitialize";

		// Token: 0x0400047A RID: 1146
		internal const string LoadOnlyParameterSetName = "LoadOnly";

		// Token: 0x0400047B RID: 1147
		internal const string InitializeParameterSetName = "Initialize";

		// Token: 0x0400047C RID: 1148
		internal const string DisabledParameterName = "Disabled";

		// Token: 0x0400047D RID: 1149
		internal const string HoldDurationDisplayHintParameterName = "HoldDurationDisplayHint";

		// Token: 0x0400047E RID: 1150
		internal const string ContentContainsSensitiveInformationParameterName = "ContentContainsSensitiveInformation";

		// Token: 0x0400047F RID: 1151
		internal const string ContentPropertyContainsWordsParameterName = "ContentPropertyContainsWords";

		// Token: 0x04000480 RID: 1152
		internal const string AccessScopeIsParameterName = "AccessScopeIs";

		// Token: 0x04000481 RID: 1153
		internal const string BlockAccessParameterName = "BlockAccess";

		// Token: 0x04000482 RID: 1154
		internal const string MockCsomProviderTypeKey = "MockCsomProviderType";

		// Token: 0x04000483 RID: 1155
		internal const string MockCsomProviderAssemblyPathKey = "MockCsomProviderAssemblyPath";

		// Token: 0x04000484 RID: 1156
		internal const string MockSharepointSitesKey = "MockSharepointSites";

		// Token: 0x04000485 RID: 1157
		internal const int CompliancePolicyCountLimit = 1000;

		// Token: 0x04000486 RID: 1158
		internal const int ExBindingCountLimit = 1000;

		// Token: 0x04000487 RID: 1159
		internal const int SpBindingCountLimit = 100;

		// Token: 0x04000488 RID: 1160
		internal static readonly string[] MockSharepointSitesSeparator = new string[]
		{
			"|"
		};

		// Token: 0x04000489 RID: 1161
		internal static readonly IEnumerable<Type> KnownExceptions = new Type[]
		{
			typeof(CompliancePolicyAlreadyExistsException),
			typeof(ComplianceRuleAlreadyExistsException),
			typeof(PolicyAndIdentityParameterUsedTogetherException),
			typeof(CompliancePolicyCountExceedsLimitException),
			typeof(InvalidCompliancePolicyWorkloadException),
			typeof(InvalidCompliancePolicyBindingException),
			typeof(MulipleComplianceRulesFoundInPolicyException),
			typeof(InvalidComplianceRulePredicateException),
			typeof(InvalidComplianceRuleActionException),
			typeof(BindingCannotCombineAllWithIndividualBindingsException),
			typeof(ForestWideOrganizationException),
			typeof(TroubleshootingCmdletException),
			typeof(SpCsomCallException),
			typeof(ErrorOnlyAllowInEopException),
			typeof(ErrorInvalidPolicyCenterSiteOwnerException),
			typeof(TaskObjectIsTooAdvancedException),
			typeof(StoragePermanentException),
			typeof(StorageTransientException),
			typeof(SensitiveInformationCmdletException)
		};

		// Token: 0x0400048A RID: 1162
		internal static readonly IEnumerable<AccessScope> SupporttedAccessScopes = new AccessScope[]
		{
			AccessScope.Internal,
			AccessScope.Internal | AccessScope.External
		};

		// Token: 0x02000137 RID: 311
		public static class BindingParameters
		{
			// Token: 0x0400048E RID: 1166
			internal const string All = "All";
		}

		// Token: 0x02000138 RID: 312
		public static class WorkloadNames
		{
			// Token: 0x0400048F RID: 1167
			internal const string Exchange = "Exchange";

			// Token: 0x04000490 RID: 1168
			internal const string Sharepoint = "Sharepoint";

			// Token: 0x04000491 RID: 1169
			internal const string OneDriveBusiness = "OneDriveBusiness";
		}
	}
}
