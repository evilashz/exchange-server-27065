using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009ED RID: 2541
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class UserConfigurationDictionaryHelper
	{
		// Token: 0x06005D05 RID: 23813 RVA: 0x0018A8D8 File Offset: 0x00188AD8
		public static void Fill(UserConfiguration userConfiguration, ConfigurableObject configurableObject, IEnumerable<ProviderPropertyDefinition> appliedProperties)
		{
			IDictionary dictionary = userConfiguration.GetDictionary();
			foreach (ProviderPropertyDefinition providerPropertyDefinition in appliedProperties)
			{
				if (dictionary.Contains(providerPropertyDefinition.Name))
				{
					configurableObject.propertyBag[providerPropertyDefinition] = StoreValueConverter.ConvertValueFromStore(providerPropertyDefinition, dictionary[providerPropertyDefinition.Name]);
				}
			}
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x0018A94C File Offset: 0x00188B4C
		internal static ConfigurableObject Fill(ConfigurableObject configObject, ProviderPropertyDefinition[] appliedProperties, UserConfigurationDictionaryHelper.GetDictionaryUserConfigurationDelegate getDictionaryUserConfigurationDelegate)
		{
			Util.ThrowOnNullArgument(configObject, "configObject");
			Util.ThrowOnNullArgument(appliedProperties, "appliedProperties");
			using (UserConfiguration userConfiguration = getDictionaryUserConfigurationDelegate(false))
			{
				if (userConfiguration == null)
				{
					return null;
				}
				UserConfigurationDictionaryHelper.Fill(userConfiguration, configObject, appliedProperties);
			}
			return configObject;
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x0018A9A8 File Offset: 0x00188BA8
		internal static void Save(ConfigurableObject configObject, ProviderPropertyDefinition[] appliedProperties, UserConfigurationDictionaryHelper.GetDictionaryUserConfigurationDelegate getDictionaryUserConfigurationDelegate)
		{
			UserConfigurationDictionaryHelper.Save(configObject, SaveMode.NoConflictResolution, appliedProperties, getDictionaryUserConfigurationDelegate);
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x0018A9B4 File Offset: 0x00188BB4
		internal static void Save(ConfigurableObject configObject, SaveMode saveMode, ProviderPropertyDefinition[] appliedProperties, UserConfigurationDictionaryHelper.GetDictionaryUserConfigurationDelegate getDictionaryUserConfigurationDelegate)
		{
			Util.ThrowOnNullArgument(configObject, "configObject");
			Util.ThrowOnNullArgument(appliedProperties, "appliedProperties");
			bool flag = false;
			do
			{
				using (UserConfiguration userConfiguration = getDictionaryUserConfigurationDelegate(!flag))
				{
					IDictionary dictionary = userConfiguration.GetDictionary();
					foreach (ProviderPropertyDefinition providerPropertyDefinition in appliedProperties)
					{
						if (configObject.IsModified(providerPropertyDefinition))
						{
							object obj = configObject[providerPropertyDefinition];
							if (obj == null)
							{
								dictionary.Remove(providerPropertyDefinition.Name);
							}
							else
							{
								dictionary[providerPropertyDefinition.Name] = StoreValueConverter.ConvertValueToStore(obj);
							}
						}
					}
					try
					{
						userConfiguration.Save(saveMode);
						break;
					}
					catch (ObjectExistedException)
					{
						if (flag)
						{
							throw;
						}
						flag = true;
					}
				}
			}
			while (flag);
		}

		// Token: 0x020009EE RID: 2542
		// (Invoke) Token: 0x06005D0A RID: 23818
		internal delegate UserConfiguration GetDictionaryUserConfigurationDelegate(bool createIfNonexisting);
	}
}
