using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000235 RID: 565
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RolesUtility
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x0005319A File Offset: 0x0005139A
		private RolesUtility()
		{
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000531A4 File Offset: 0x000513A4
		public static Version GetUnpackedVersion(string roleName)
		{
			Version result = null;
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, false);
			string text = (string)Registry.GetValue(keyName, "UnpackedVersion", null);
			if (text == null)
			{
				keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, true);
				text = (string)Registry.GetValue(keyName, "UnpackedVersion", null);
			}
			if (text != null)
			{
				result = new Version(text);
			}
			return result;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0005320C File Offset: 0x0005140C
		public static Version GetUnpackedDatacenterVersion(string roleName)
		{
			Version result = null;
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, false);
			string text = (string)Registry.GetValue(keyName, "UnpackedDatacenterVersion", null);
			if (text != null)
			{
				result = new Version(text);
			}
			return result;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0005324C File Offset: 0x0005144C
		public static string GetRoleKeyByName(string roleName, bool legacyRequested)
		{
			string result = roleName;
			if (!legacyRequested && roleName != null)
			{
				if (!(roleName == "BridgeheadRole"))
				{
					if (roleName == "GatewayRole")
					{
						result = "EdgeTransportRole";
					}
				}
				else
				{
					result = "HubTransportRole";
				}
			}
			if (roleName == "AdminToolsRole")
			{
				result = "AdminTools";
			}
			return result;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000532A4 File Offset: 0x000514A4
		public static void GetConfiguringStatus(ref ConfigurationStatus status)
		{
			string name = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(status.Role, false);
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				if (registryKey != null)
				{
					InstallationModes action = InstallationModes.Unknown;
					string text = (string)registryKey.GetValue("Action", null);
					if (text != null)
					{
						action = (InstallationModes)Enum.Parse(typeof(InstallationModes), text);
					}
					status.Action = action;
					object value = registryKey.GetValue("Watermark", null);
					status.Watermark = ((value != null) ? value.ToString() : null);
				}
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0005334C File Offset: 0x0005154C
		public static void SetConfiguringStatus(ConfigurationStatus status)
		{
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(status.Role, false);
			Registry.SetValue(keyName, "Action", status.Action);
			Registry.SetValue(keyName, "Watermark", status.Watermark);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0005339C File Offset: 0x0005159C
		public static void ClearConfiguringStatus(ConfigurationStatus status)
		{
			string name = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(status.Role, false);
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name, true);
			if (registryKey != null)
			{
				registryKey.DeleteValue("Action", false);
				registryKey.DeleteValue("Watermark", false);
				if (registryKey.SubKeyCount == 0 && registryKey.ValueCount == 0)
				{
					registryKey.Close();
					string name2 = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\";
					RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey(name2, true);
					registryKey2.DeleteSubKey(RolesUtility.GetRoleKeyByName(status.Role, false), false);
					registryKey2.Close();
					return;
				}
				registryKey.Close();
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00053434 File Offset: 0x00051634
		public static void SetPostSetupVersion(string roleName, Version version)
		{
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, false);
			string text = (string)Registry.GetValue(keyName, "PostSetupVersion", "<unset>");
			TaskLogger.Trace("Updating postsetup version from {0} to {1}", new object[]
			{
				text,
				version
			});
			Registry.SetValue(keyName, "PostSetupVersion", (version == null) ? "" : version.ToString());
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000534A4 File Offset: 0x000516A4
		public static Version GetPostSetupVersion(string roleName)
		{
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, false);
			string text = (string)Registry.GetValue(keyName, "PostSetupVersion", null);
			Version result = null;
			if (text != null && text != "")
			{
				result = new Version(text);
			}
			return result;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000534F0 File Offset: 0x000516F0
		public static Version GetConfiguredVersion(string roleName)
		{
			Version result = null;
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, false);
			string text = (string)Registry.GetValue(keyName, "ConfiguredVersion", null);
			if (text == null)
			{
				keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, true);
				text = (string)Registry.GetValue(keyName, "ConfiguredVersion", null);
			}
			if (text != null)
			{
				result = new Version(text);
			}
			else if (roleName == "AdminToolsRole")
			{
				Version unpackedVersion = RolesUtility.GetUnpackedVersion(roleName);
				if (unpackedVersion != null && unpackedVersion < AdminToolsRole.FirstConfiguredVersion)
				{
					result = unpackedVersion;
				}
			}
			return result;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00053584 File Offset: 0x00051784
		public static void SetConfiguredVersion(string roleName, Version configuredVersion)
		{
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, false);
			string text = (string)Registry.GetValue(keyName, "ConfiguredVersion", "<unset>");
			TaskLogger.Trace("Updating configured version from {0} to {1}", new object[]
			{
				text,
				configuredVersion
			});
			Registry.SetValue(keyName, "ConfiguredVersion", configuredVersion.ToString());
			if (RolesUtility.GetPostSetupVersion(roleName) == null)
			{
				RolesUtility.SetPostSetupVersion(roleName, new Version("0.0.0.0"));
			}
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, false)))
			{
				registryKey.SetValue("ConfiguredVersion", configuredVersion.ToString());
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0005364C File Offset: 0x0005184C
		public static void DeleteConfiguredVersion(string roleName)
		{
			string name = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, false);
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name, true);
			registryKey.DeleteValue("ConfiguredVersion", false);
			registryKey.DeleteValue("PostSetupVersion", false);
			registryKey.Close();
			try
			{
				Registry.LocalMachine.DeleteSubKeyTree("SOFTWARE\\Wow6432Node\\Microsoft\\ExchangeServer\\v15\\" + RolesUtility.GetRoleKeyByName(roleName, false));
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x000536C8 File Offset: 0x000518C8
		public static ParameterCollection ReadSetupParameters(bool isDatacenter)
		{
			string text = null;
			text = (Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeLabs", "ParametersFile", null) as string);
			if (string.IsNullOrEmpty(text))
			{
				if (isDatacenter)
				{
					throw new RegistryValueMissingOrInvalidException("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeLabs", "ParametersFile");
				}
				text = Path.Combine(Role.SetupComponentInfoFilePath, "bin\\EnterpriseServiceEndpointsConfig.xml");
			}
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("parameter.xsd");
			xmlReaderSettings.Schemas.Add(null, XmlReader.Create(manifestResourceStream));
			ParameterCollection parameterCollection = null;
			using (XmlReader xmlReader = XmlReader.Create(text, xmlReaderSettings))
			{
				try
				{
					parameterCollection = (ParameterCollection)RolesUtility.GetParameterSerializer().Deserialize(xmlReader);
				}
				catch (InvalidOperationException ex)
				{
					throw new XmlDeserializationException(text, ex.Message, (ex.InnerException == null) ? string.Empty : ex.InnerException.Message, ex);
				}
			}
			TaskLogger.Log(Strings.LoadedParameters(text, parameterCollection.Count));
			return parameterCollection;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000537D8 File Offset: 0x000519D8
		public static SetupComponentInfo ReadSetupComponentInfoFile(string fileName)
		{
			SetupComponentInfo setupComponentInfo = null;
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("component.xsd");
			xmlReaderSettings.Schemas.Add(null, XmlReader.Create(manifestResourceStream));
			string text;
			using (XmlReader xmlReader = RolesUtility.CreateXmlReader(fileName, xmlReaderSettings, out text))
			{
				try
				{
					setupComponentInfo = (SetupComponentInfo)RolesUtility.GetComponentSerializer().Deserialize(xmlReader);
					setupComponentInfo.PopulateTasksProperty(Path.GetFileNameWithoutExtension(fileName));
					setupComponentInfo.ValidateDatacenterAttributes();
				}
				catch (InvalidOperationException ex)
				{
					throw new XmlDeserializationException(text, ex.Message, (ex.InnerException == null) ? string.Empty : ex.InnerException.Message);
				}
				TaskLogger.Log(Strings.LoadedComponentWithTasks(setupComponentInfo.Name, setupComponentInfo.Tasks.Count, text));
			}
			return setupComponentInfo;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000538C4 File Offset: 0x00051AC4
		public static XmlReader CreateXmlReader(string fileName, XmlReaderSettings settings, out string usedResourceName)
		{
			if (File.Exists(fileName))
			{
				usedResourceName = fileName;
				return XmlReader.Create(fileName, settings);
			}
			string fileName2 = Path.GetFileName(fileName);
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName2);
			usedResourceName = "res://" + fileName2;
			return XmlReader.Create(manifestResourceStream, settings);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0005390C File Offset: 0x00051B0C
		public static XmlSerializer GetParameterSerializer()
		{
			if (RolesUtility.serializerParameter == null)
			{
				lock (RolesUtility.serializerLock)
				{
					if (RolesUtility.serializerParameter == null)
					{
						RolesUtility.serializerParameter = new XmlSerializer(typeof(ParameterCollection));
					}
				}
			}
			return RolesUtility.serializerParameter;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0005396C File Offset: 0x00051B6C
		public static XmlSerializer GetComponentSerializer()
		{
			if (RolesUtility.serializerComponent == null)
			{
				lock (RolesUtility.serializerLock)
				{
					if (RolesUtility.serializerComponent == null)
					{
						RolesUtility.serializerComponent = new SetupComponentInfoSerializer();
					}
				}
			}
			return RolesUtility.serializerComponent;
		}

		// Token: 0x04000818 RID: 2072
		private static XmlSerializer serializerParameter;

		// Token: 0x04000819 RID: 2073
		private static SetupComponentInfoSerializer serializerComponent;

		// Token: 0x0400081A RID: 2074
		private static object serializerLock = new object();
	}
}
