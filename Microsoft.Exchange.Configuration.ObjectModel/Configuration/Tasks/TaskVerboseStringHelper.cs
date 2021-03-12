using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000AA RID: 170
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TaskVerboseStringHelper
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x00018FB0 File Offset: 0x000171B0
		public static LocalizedString GetSourceVerboseString(IConfigDataProvider session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (string.IsNullOrEmpty(session.Source))
			{
				return Strings.VerboseNoSource;
			}
			if (!(session is IDirectorySession))
			{
				return Strings.VerboseSource(session.Source);
			}
			IDirectorySession directorySession = (IDirectorySession)session;
			if (directorySession.UseGlobalCatalog)
			{
				return Strings.VerboseSourceFromGC(session.Source);
			}
			return Strings.VerboseSourceFromDC(session.Source);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00019018 File Offset: 0x00017218
		public static LocalizedString GetReadObjectVerboseString(ObjectId id, IConfigDataProvider session, Type type)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (null == type)
			{
				throw new ArgumentNullException("type");
			}
			return Strings.VerboseTaskReadDataObject(id.ToString(), type.Name);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00019068 File Offset: 0x00017268
		public static LocalizedString GetSaveObjectVerboseString(IConfigurable dataObject, IConfigDataProvider session, Type type)
		{
			if (dataObject == null)
			{
				throw new ArgumentNullException("dataObject");
			}
			if (dataObject.Identity == null)
			{
				throw new ArgumentOutOfRangeException("dataObject.Identity");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (null == type)
			{
				throw new ArgumentNullException("type");
			}
			return Strings.VerboseSaveChange(dataObject.Identity.ToString(), type.Name, dataObject.ObjectState.ToString());
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000190DE File Offset: 0x000172DE
		public static LocalizedString GetConfigurableObjectNonChangedProperties(ConfigurableObject dataObject)
		{
			if (dataObject == null)
			{
				throw new ArgumentNullException("dataObject");
			}
			if (dataObject.Identity != null)
			{
				return Strings.VerboseADObjectNoChangedPropertiesWithId(dataObject.Identity.ToString());
			}
			return Strings.VerboseADObjectNoChangedProperties;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001910C File Offset: 0x0001730C
		public static LocalizedString GetConfigurableObjectChangedProperties(ConfigurableObject dataObject)
		{
			if (dataObject == null)
			{
				throw new ArgumentNullException("dataObject");
			}
			StringBuilder stringBuilder = new StringBuilder("{ ");
			IList<PropertyDefinition> changedPropertyDefinitions = dataObject.GetChangedPropertyDefinitions();
			for (int i = 0; i < changedPropertyDefinitions.Count; i++)
			{
				if (changedPropertyDefinitions[i] != ADObjectSchema.ObjectState)
				{
					PropertyDefinition propertyDefinition = changedPropertyDefinitions[i];
					if (propertyDefinition != ActiveDirectoryServerSchema.AssistantMaintenanceScheduleInternal)
					{
						stringBuilder.Append(propertyDefinition.Name);
						ADPropertyDefinition adpropertyDefinition = propertyDefinition as ADPropertyDefinition;
						if (adpropertyDefinition != null)
						{
							stringBuilder.Append('[');
							stringBuilder.Append(TaskVerboseStringHelper.RetrieveLDAPPropertyNames(adpropertyDefinition));
							stringBuilder.Append(']');
						}
						stringBuilder.Append('=');
						if (dataObject[propertyDefinition] is IList)
						{
							IList list = (IList)dataObject[propertyDefinition];
							stringBuilder.Append("{ ");
							for (int j = 0; j < list.Count; j++)
							{
								stringBuilder.Append(TaskVerboseStringHelper.FormatParameterValue(list[j]));
								if (j + 1 < list.Count)
								{
									stringBuilder.Append(", ");
								}
							}
							stringBuilder.Append(" }");
						}
						else
						{
							stringBuilder.Append(TaskVerboseStringHelper.FormatParameterValue(dataObject[propertyDefinition]));
						}
						stringBuilder.Append(", ");
					}
				}
			}
			if (stringBuilder.Length > 2)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			stringBuilder.Append(" }");
			LocalizedString result = LocalizedString.Empty;
			if (dataObject.Identity != null)
			{
				ADObject adobject = dataObject as ADObject;
				if (adobject != null)
				{
					result = Strings.VerboseADObjectChangedPropertiesWithDn(adobject.Name, adobject.Id.ToDNString(), stringBuilder.ToString());
				}
				else
				{
					result = Strings.VerboseADObjectChangedPropertiesWithId(dataObject.Identity.ToString(), stringBuilder.ToString());
				}
			}
			else
			{
				result = Strings.VerboseADObjectChangedProperties(stringBuilder.ToString());
			}
			return result;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000192DC File Offset: 0x000174DC
		private static string RetrieveLDAPPropertyNames(ADPropertyDefinition propDef)
		{
			if (!propDef.IsCalculated)
			{
				return propDef.LdapDisplayName;
			}
			StringBuilder stringBuilder = new StringBuilder(propDef.SupportingProperties.Count * 18);
			foreach (ProviderPropertyDefinition providerPropertyDefinition in propDef.SupportingProperties)
			{
				ADPropertyDefinition adpropertyDefinition = providerPropertyDefinition as ADPropertyDefinition;
				if (adpropertyDefinition != null && !string.IsNullOrEmpty(adpropertyDefinition.LdapDisplayName))
				{
					stringBuilder.Append(adpropertyDefinition.LdapDisplayName);
					stringBuilder.Append(", ");
				}
			}
			if (stringBuilder.Length > 2)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001939C File Offset: 0x0001759C
		private static string FormatParameterValue(object value)
		{
			string result = string.Empty;
			if (value == null)
			{
				result = "$null";
			}
			else if (value is bool)
			{
				result = string.Format("${0}", value.ToString());
			}
			else
			{
				result = string.Format("'{0}'", value.ToString());
			}
			return result;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000193E8 File Offset: 0x000175E8
		public static LocalizedString GetDeleteObjectVerboseString(ObjectId id, IConfigDataProvider session, Type type)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (null == type)
			{
				throw new ArgumentNullException("type");
			}
			return Strings.VerboseDeleteObject(id.ToString(), type.Name);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00019436 File Offset: 0x00017636
		public static LocalizedString GetFindByIdParameterVerboseString(IIdentityParameter id, IConfigDataProvider session, Type type, ObjectId rootId)
		{
			return Strings.VerboseTaskGetDataObjects(id.ToString(), type.Name, (rootId == null) ? "$null" : rootId.ToString());
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001945C File Offset: 0x0001765C
		public static LocalizedString GetFindDataObjectsVerboseString(IConfigDataProvider session, Type type, QueryFilter filter, ObjectId rootId, bool deepSearch)
		{
			return Strings.VerboseTaskFindDataObjects(type.Name, (filter == null) ? "$null" : filter.ToString(), deepSearch ? QueryScope.SubTree.ToString() : QueryScope.OneLevel.ToString(), (rootId == null) ? "$null" : rootId.ToString());
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000194B0 File Offset: 0x000176B0
		public static LocalizedString GetFindDataObjectsInALVerboseString(IConfigDataProvider session, Type type, ADObjectId addressList)
		{
			return Strings.VerboseTaskFindDataObjectsInAL(type.Name, addressList.ToString());
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000194C3 File Offset: 0x000176C3
		public static LocalizedString GetADServerSettings(ADServerSettings serverSettings)
		{
			return TaskVerboseStringHelper.GetADServerSettings(null, serverSettings);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000194CC File Offset: 0x000176CC
		public static LocalizedString GetADServerSettings(string cmdletName, ADServerSettings serverSettings)
		{
			return TaskVerboseStringHelper.GetADServerSettings(cmdletName, serverSettings, null);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000194D8 File Offset: 0x000176D8
		public static LocalizedString GetADServerSettings(string cmdletName, ADServerSettings serverSettings, CultureInfo targetCulture)
		{
			if (serverSettings == null)
			{
				throw new ArgumentNullException("serverSettings");
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (cmdletName != null)
			{
				stringBuilder.Append(Strings.VerboseAdminSessionSettings(cmdletName).ToString(targetCulture));
				stringBuilder.Append(' ');
			}
			stringBuilder.Append(Strings.VerboseAdminSessionSettingsViewForest(serverSettings.ViewEntireForest.ToString()).ToString(targetCulture));
			stringBuilder.Append(", ");
			if (serverSettings.RecipientViewRoot != null)
			{
				stringBuilder.Append(Strings.VerboseAdminSessionSettingsDefaultScope(serverSettings.RecipientViewRoot.ToCanonicalName()).ToString(targetCulture));
				stringBuilder.Append(", ");
			}
			IDictionary<string, Fqdn> configurationDomainControllers = serverSettings.ConfigurationDomainControllers;
			if (configurationDomainControllers.Count > 0)
			{
				stringBuilder.Append(Strings.VerboseAdminSessionSettingsConfigDC(string.Join<Fqdn>(", ", configurationDomainControllers.Values)).ToString(targetCulture));
				stringBuilder.Append(", ");
			}
			IDictionary<string, Fqdn> preferredGlobalCatalogs = serverSettings.PreferredGlobalCatalogs;
			if (preferredGlobalCatalogs.Count > 0)
			{
				stringBuilder.Append(Strings.VerboseAdminSessionSettingsGlobalCatalog(string.Join<Fqdn>(", ", preferredGlobalCatalogs.Values)).ToString(targetCulture));
				stringBuilder.Append(", ");
			}
			MultiValuedProperty<Fqdn> preferredDomainControllers = serverSettings.PreferredDomainControllers;
			if (preferredDomainControllers != null && preferredDomainControllers.Count > 0)
			{
				StringBuilder stringBuilder2 = new StringBuilder("{ ");
				for (int i = 0; i < preferredDomainControllers.Count; i++)
				{
					stringBuilder2.Append(preferredDomainControllers[i].ToString());
					if (i + 1 < preferredDomainControllers.Count)
					{
						stringBuilder2.Append(", ");
					}
				}
				stringBuilder2.Append(" }");
				stringBuilder.Append(Strings.VerboseAdminSessionSettingsDCs(stringBuilder2.ToString()).ToString(targetCulture));
			}
			RunspaceServerSettings runspaceServerSettings = serverSettings as RunspaceServerSettings;
			if (runspaceServerSettings != null)
			{
				if (runspaceServerSettings.UserConfigurationDomainController != null)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(Strings.VerboseAdminSessionSettingsUserConfigDC(runspaceServerSettings.UserConfigurationDomainController).ToString(targetCulture));
				}
				if (runspaceServerSettings.UserPreferredGlobalCatalog != null)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(Strings.VerboseAdminSessionSettingsUserGlobalCatalog(runspaceServerSettings.UserPreferredGlobalCatalog).ToString(targetCulture));
				}
				MultiValuedProperty<Fqdn> userPreferredDomainControllers = runspaceServerSettings.UserPreferredDomainControllers;
				if (userPreferredDomainControllers != null && userPreferredDomainControllers.Count > 0)
				{
					stringBuilder.Append(", ");
					StringBuilder stringBuilder3 = new StringBuilder("{ ");
					for (int j = 0; j < userPreferredDomainControllers.Count; j++)
					{
						stringBuilder3.Append(userPreferredDomainControllers[j].ToString());
						if (j + 1 < userPreferredDomainControllers.Count)
						{
							stringBuilder3.Append(", ");
						}
					}
					stringBuilder3.Append(" }");
					stringBuilder.Append(Strings.VerboseAdminSessionSettingsDCs(stringBuilder3.ToString()).ToString(targetCulture));
				}
			}
			return new LocalizedString(stringBuilder.ToString());
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000197C2 File Offset: 0x000179C2
		public static LocalizedString GetScopeSetVerboseString(ScopeSet scopeSet)
		{
			return new LocalizedString(Strings.VerbosePopulateScopeSet + "{ " + scopeSet.ToLocalizedString() + " }");
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000197F0 File Offset: 0x000179F0
		public static LocalizedString GetProvisioningValidationErrors(ProvisioningValidationError[] errors, int agentIndex)
		{
			if (errors == null || errors.Length <= 0)
			{
				throw new ArgumentNullException("errors");
			}
			StringBuilder stringBuilder = new StringBuilder(errors.Length * 64);
			stringBuilder.Append('{');
			for (int i = 0; i < errors.Length - 1; i++)
			{
				stringBuilder.AppendFormat("{{{0}}}, ", errors[i].Description);
			}
			stringBuilder.AppendFormat("{{{0}}}", errors[errors.Length - 1].Description);
			stringBuilder.Append('}');
			return Strings.ProvisioningValidationErrors(agentIndex, stringBuilder.ToString());
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00019884 File Offset: 0x00017A84
		public static string FormatUserSpecifiedParameters(PropertyBag parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			StringBuilder stringBuilder = new StringBuilder(parameters.Count * 32);
			bool flag = false;
			foreach (object obj in parameters.Keys)
			{
				if (flag)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('-');
				stringBuilder.Append(obj);
				stringBuilder.Append(' ');
				object obj2 = parameters[obj];
				IDictionary dictionary = obj2 as IDictionary;
				if (dictionary != null)
				{
					TaskVerboseStringHelper.BuildDictionary(stringBuilder, dictionary);
				}
				else
				{
					ICollection collection = obj2 as ICollection;
					if (collection != null)
					{
						TaskVerboseStringHelper.BuildCollection(stringBuilder, collection);
					}
					else
					{
						TaskVerboseStringHelper.BuildSingleValue(stringBuilder, obj2);
					}
				}
				flag = true;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00019960 File Offset: 0x00017B60
		private static void BuildDictionary(StringBuilder builder, IDictionary dictionary)
		{
			builder.Append("@{");
			bool flag = false;
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (flag)
				{
					builder.Append(';');
				}
				builder.Append(dictionaryEntry.Key);
				builder.Append('=');
				ICollection collection = dictionaryEntry.Value as ICollection;
				if (collection != null)
				{
					TaskVerboseStringHelper.BuildCollection(builder, collection);
				}
				else
				{
					TaskVerboseStringHelper.BuildSingleValue(builder, dictionaryEntry.Value);
				}
				flag = true;
			}
			builder.Append('}');
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00019A14 File Offset: 0x00017C14
		private static void BuildCollection(StringBuilder builder, ICollection collection)
		{
			byte[] array = collection as byte[];
			if (array != null)
			{
				builder.Append(TaskVerboseStringHelper.TruncateAndConvertByteArray(array, 20));
				return;
			}
			builder.Append('(');
			bool flag = false;
			foreach (object singleValue in collection)
			{
				if (flag)
				{
					builder.Append(',');
				}
				TaskVerboseStringHelper.BuildSingleValue(builder, singleValue);
				flag = true;
			}
			builder.Append(')');
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00019AA4 File Offset: 0x00017CA4
		private static void BuildSingleValue(StringBuilder builder, object singleValue)
		{
			if (singleValue == null)
			{
				builder.Append("$null");
				return;
			}
			builder.Append("\"");
			builder.Append(TaskVerboseStringHelper.TruncateString(singleValue.ToString(), 200));
			builder.Append("\"");
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00019AF0 File Offset: 0x00017CF0
		public static string ExtractIdentityParameter(PropertyBag parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			string result = string.Empty;
			if (parameters.Contains("Identity"))
			{
				object obj = parameters["Identity"];
				if (obj != null)
				{
					result = obj.ToString();
				}
			}
			return result;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00019B35 File Offset: 0x00017D35
		private static string TruncateString(string str, int maxLength)
		{
			if (str.Length > maxLength)
			{
				return str.Substring(0, maxLength) + "[...]";
			}
			return str;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00019B54 File Offset: 0x00017D54
		private static string TruncateAndConvertByteArray(byte[] bytes, int maxLength)
		{
			if (bytes.Length > maxLength)
			{
				return Convert.ToBase64String(bytes, 0, maxLength) + "[...]";
			}
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x0400017E RID: 382
		private const int MaxStringLength = 200;

		// Token: 0x0400017F RID: 383
		private const int MaxByteArrayLength = 20;

		// Token: 0x04000180 RID: 384
		private const string TruncatedSuffix = "[...]";
	}
}
