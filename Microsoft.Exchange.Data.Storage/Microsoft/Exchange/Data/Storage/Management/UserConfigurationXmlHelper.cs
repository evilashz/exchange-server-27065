using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009EF RID: 2543
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class UserConfigurationXmlHelper
	{
		// Token: 0x06005D0D RID: 23821 RVA: 0x0018AA98 File Offset: 0x00188C98
		internal static ConfigurableObject Fill(ConfigurableObject configObject, ProviderPropertyDefinition property, UserConfigurationXmlHelper.GetXmlUserConfigurationDelegate getXmlUserConfigurationDelegate)
		{
			return UserConfigurationXmlHelper.Fill(configObject, property, (bool createIfNonexisting) => getXmlUserConfigurationDelegate(createIfNonexisting));
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x0018AAC8 File Offset: 0x00188CC8
		internal static ConfigurableObject Fill(ConfigurableObject configObject, ProviderPropertyDefinition property, UserConfigurationXmlHelper.GetReadableXmlUserConfigurationDelegate getXmlUserConfigurationDelegate)
		{
			Util.ThrowOnNullArgument(configObject, "configObject");
			Util.ThrowOnNullArgument(property, "property");
			using (IReadableUserConfiguration readableUserConfiguration = getXmlUserConfigurationDelegate(false))
			{
				if (readableUserConfiguration == null)
				{
					return null;
				}
				using (Stream xmlStream = readableUserConfiguration.GetXmlStream())
				{
					DataContractSerializer dataContractSerializer = new DataContractSerializer(property.Type);
					configObject[property] = dataContractSerializer.ReadObject(xmlStream);
				}
			}
			return configObject;
		}

		// Token: 0x06005D0F RID: 23823 RVA: 0x0018AB54 File Offset: 0x00188D54
		internal static void Save(ConfigurableObject configObject, ProviderPropertyDefinition property, UserConfigurationXmlHelper.GetXmlUserConfigurationDelegate getXmlUserConfigurationDelegate)
		{
			UserConfigurationXmlHelper.Save(configObject, SaveMode.NoConflictResolution, property, getXmlUserConfigurationDelegate);
		}

		// Token: 0x06005D10 RID: 23824 RVA: 0x0018AB60 File Offset: 0x00188D60
		internal static void Save(ConfigurableObject configObject, SaveMode saveMode, ProviderPropertyDefinition property, UserConfigurationXmlHelper.GetXmlUserConfigurationDelegate getXmlUserConfigurationDelegate)
		{
			Util.ThrowOnNullArgument(configObject, "configObject");
			Util.ThrowOnNullArgument(property, "property");
			bool flag = false;
			do
			{
				using (UserConfiguration userConfiguration = getXmlUserConfigurationDelegate(!flag))
				{
					using (Stream xmlStream = userConfiguration.GetXmlStream())
					{
						DataContractSerializer dataContractSerializer = new DataContractSerializer(property.Type);
						xmlStream.SetLength(0L);
						dataContractSerializer.WriteObject(xmlStream, configObject[property]);
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

		// Token: 0x020009F0 RID: 2544
		// (Invoke) Token: 0x06005D12 RID: 23826
		internal delegate UserConfiguration GetXmlUserConfigurationDelegate(bool createIfNonexisting);

		// Token: 0x020009F1 RID: 2545
		// (Invoke) Token: 0x06005D16 RID: 23830
		internal delegate IReadableUserConfiguration GetReadableXmlUserConfigurationDelegate(bool createIfNonexisting);
	}
}
