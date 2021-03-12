using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Tools
{
	// Token: 0x02000D03 RID: 3331
	internal sealed class SupportedToolsData
	{
		// Token: 0x06007FF4 RID: 32756 RVA: 0x0020B44F File Offset: 0x0020964F
		private SupportedToolsData(string dataFile, string schemaFile)
		{
			this.dataFile = dataFile;
			this.schemaFile = schemaFile;
		}

		// Token: 0x06007FF5 RID: 32757 RVA: 0x0020B468 File Offset: 0x00209668
		internal static SupportedToolsData GetSupportedToolData(string dataFile, string schemaFile)
		{
			SupportedToolsData supportedToolsData = new SupportedToolsData(dataFile, schemaFile);
			supportedToolsData.ReadFile();
			supportedToolsData.Validate();
			return supportedToolsData;
		}

		// Token: 0x06007FF6 RID: 32758 RVA: 0x0020B48C File Offset: 0x0020968C
		internal bool RequiresTenantVersion()
		{
			foreach (ToolInfoData toolInfo in this.toolData.toolInformation)
			{
				if (SupportedToolsData.ContainsTenantVersionInformation(toolInfo))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007FF7 RID: 32759 RVA: 0x0020B4C8 File Offset: 0x002096C8
		internal SupportedVersion GetSupportedVersion(ToolId toolId, Version tenantVersion)
		{
			foreach (ToolInfoData toolInfoData in this.toolData.toolInformation)
			{
				if (toolInfoData.id == toolId)
				{
					return SupportedToolsData.GetSupportedVersion(toolInfoData, tenantVersion);
				}
			}
			return null;
		}

		// Token: 0x06007FF8 RID: 32760 RVA: 0x0020B50C File Offset: 0x0020970C
		private static SupportedVersion GetSupportedVersion(ToolInfoData toolInfo, Version tenantVersion)
		{
			SupportedVersion supportedVersion = null;
			if (tenantVersion != null && toolInfo.customSupportedVersion != null)
			{
				foreach (CustomSupportedVersion customSupportedVersion2 in toolInfo.customSupportedVersion)
				{
					if (SupportedToolsData.IsInRange(customSupportedVersion2, tenantVersion))
					{
						supportedVersion = customSupportedVersion2;
						break;
					}
				}
			}
			return supportedVersion ?? toolInfo.defaultSupportedVersion;
		}

		// Token: 0x06007FF9 RID: 32761 RVA: 0x0020B560 File Offset: 0x00209760
		private static bool IsInRange(CustomSupportedVersion versionInfo, Version tenantVersion)
		{
			if (versionInfo == null || tenantVersion == null)
			{
				return false;
			}
			Version version = SupportedToolsData.GetVersion(versionInfo.minTenantVersion, SupportedToolsData.MinimumVersion);
			Version version2 = SupportedToolsData.GetVersion(versionInfo.maxTenantVersion, SupportedToolsData.MaximumVersion);
			return version2 >= tenantVersion && version <= tenantVersion;
		}

		// Token: 0x06007FFA RID: 32762 RVA: 0x0020B5AF File Offset: 0x002097AF
		private static bool ContainsTenantVersionInformation(ToolInfoData toolInfo)
		{
			return toolInfo.customSupportedVersion != null && toolInfo.customSupportedVersion.Length > 0;
		}

		// Token: 0x06007FFB RID: 32763 RVA: 0x0020B5C8 File Offset: 0x002097C8
		private void ReadFile()
		{
			if (!File.Exists(this.dataFile))
			{
				throw new FileNotFoundException(Strings.SupportedToolsCannotFindFile, this.dataFile);
			}
			if (!File.Exists(this.schemaFile))
			{
				throw new FileNotFoundException(Strings.SupportedToolsCannotFindFile, this.schemaFile);
			}
			try
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.Schemas.Add(string.Empty, this.schemaFile);
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				using (XmlReader xmlReader = XmlReader.Create(this.dataFile, xmlReaderSettings))
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(supportedTools));
					this.toolData = (supportedTools)xmlSerializer.Deserialize(xmlReader);
				}
			}
			catch (IOException e)
			{
				SupportedToolsData.HandleDataReadException(e);
			}
			catch (XmlException e2)
			{
				SupportedToolsData.HandleDataReadException(e2);
			}
			catch (XmlSchemaException e3)
			{
				SupportedToolsData.HandleDataReadException(e3);
			}
			catch (InvalidOperationException ex)
			{
				SupportedToolsData.HandleDataReadException(ex.InnerException ?? ex);
			}
		}

		// Token: 0x06007FFC RID: 32764 RVA: 0x0020B6F4 File Offset: 0x002098F4
		private void Validate()
		{
			this.ValidateDuplicatedToolEntries();
			this.ValidateToolEntries();
		}

		// Token: 0x06007FFD RID: 32765 RVA: 0x0020B704 File Offset: 0x00209904
		private void ValidateToolEntries()
		{
			foreach (ToolInfoData toolInfoData in this.toolData.toolInformation)
			{
				if (toolInfoData.defaultSupportedVersion != null && !SupportedToolsData.IsValidVersionRange(toolInfoData.defaultSupportedVersion.minSupportedVersion, toolInfoData.defaultSupportedVersion.latestVersion))
				{
					SupportedToolsData.ReportInconsistentDataException(Strings.SupportedToolsDataInvalidToolVersionRange(toolInfoData.id.ToString()));
				}
				if (toolInfoData.customSupportedVersion != null)
				{
					foreach (CustomSupportedVersion customSupportedVersion2 in toolInfoData.customSupportedVersion)
					{
						if (!SupportedToolsData.IsValidVersionRange(customSupportedVersion2.minSupportedVersion, customSupportedVersion2.latestVersion))
						{
							SupportedToolsData.ReportInconsistentDataException(Strings.SupportedToolsDataInvalidToolVersionRange(toolInfoData.id.ToString()));
						}
						if (!SupportedToolsData.IsValidVersionRange(customSupportedVersion2.minTenantVersion, customSupportedVersion2.maxTenantVersion))
						{
							SupportedToolsData.ReportInconsistentDataException(Strings.SupportedToolsDataInvalidTenantVersionRange(toolInfoData.id.ToString()));
						}
					}
					if (SupportedToolsData.ContainsOverlappingVersionRanges(toolInfoData.customSupportedVersion))
					{
						SupportedToolsData.ReportInconsistentDataException(Strings.SupportedToolsDataOverlappingTenantVersionRanges(toolInfoData.id.ToString()));
					}
				}
			}
		}

		// Token: 0x06007FFE RID: 32766 RVA: 0x0020B824 File Offset: 0x00209A24
		private static bool ContainsOverlappingVersionRanges(CustomSupportedVersion[] versions)
		{
			List<CustomSupportedVersion> list = new List<CustomSupportedVersion>();
			foreach (CustomSupportedVersion customSupportedVersion in versions)
			{
				if (SupportedToolsData.Overlap(customSupportedVersion, list))
				{
					return true;
				}
				list.Add(customSupportedVersion);
			}
			return false;
		}

		// Token: 0x06007FFF RID: 32767 RVA: 0x0020B868 File Offset: 0x00209A68
		private static bool Overlap(CustomSupportedVersion newRange, IEnumerable<CustomSupportedVersion> existingRanges)
		{
			foreach (CustomSupportedVersion range in existingRanges)
			{
				if (SupportedToolsData.Overlap(newRange, range))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008000 RID: 32768 RVA: 0x0020B8BC File Offset: 0x00209ABC
		private static bool Overlap(CustomSupportedVersion range1, CustomSupportedVersion range2)
		{
			Version version = SupportedToolsData.GetVersion(range1.minTenantVersion, SupportedToolsData.MinimumVersion);
			Version version2 = SupportedToolsData.GetVersion(range1.maxTenantVersion, SupportedToolsData.MaximumVersion);
			Version version3 = SupportedToolsData.GetVersion(range2.minTenantVersion, SupportedToolsData.MinimumVersion);
			Version version4 = SupportedToolsData.GetVersion(range2.maxTenantVersion, SupportedToolsData.MaximumVersion);
			return version2 >= version3 && version <= version4;
		}

		// Token: 0x06008001 RID: 32769 RVA: 0x0020B920 File Offset: 0x00209B20
		private static bool IsValidVersionRange(string minVersionString, string maxVersionString)
		{
			Version version = SupportedToolsData.GetVersion(minVersionString, SupportedToolsData.MinimumVersion);
			Version version2 = SupportedToolsData.GetVersion(maxVersionString, SupportedToolsData.MaximumVersion);
			return version <= version2;
		}

		// Token: 0x06008002 RID: 32770 RVA: 0x0020B94C File Offset: 0x00209B4C
		private void ValidateDuplicatedToolEntries()
		{
			HashSet<ToolId> hashSet = new HashSet<ToolId>();
			foreach (ToolInfoData toolInfoData in this.toolData.toolInformation)
			{
				if (hashSet.Contains(toolInfoData.id))
				{
					SupportedToolsData.ReportInconsistentDataException(Strings.SupportedToolsDataMultipleToolEntries(toolInfoData.id.ToString()));
				}
				else
				{
					hashSet.Add(toolInfoData.id);
				}
			}
		}

		// Token: 0x06008003 RID: 32771 RVA: 0x0020B9B4 File Offset: 0x00209BB4
		private static void ReportInconsistentDataException(LocalizedString errorMessage)
		{
			throw new InvalidDataException(errorMessage);
		}

		// Token: 0x06008004 RID: 32772 RVA: 0x0020B9C1 File Offset: 0x00209BC1
		private static void HandleDataReadException(Exception e)
		{
			throw new FormatException(Strings.SupportedToolsCannotReadFile, e);
		}

		// Token: 0x06008005 RID: 32773 RVA: 0x0020B9D3 File Offset: 0x00209BD3
		internal static Version GetVersion(string versionString, Version defaultValue)
		{
			if (string.IsNullOrEmpty(versionString))
			{
				return defaultValue;
			}
			return new Version(versionString);
		}

		// Token: 0x04003EC4 RID: 16068
		private const int MaxVersionValue = 999999999;

		// Token: 0x04003EC5 RID: 16069
		internal static readonly Version MinimumVersion = new Version(0, 0, 0, 0);

		// Token: 0x04003EC6 RID: 16070
		internal static readonly Version MaximumVersion = new Version(999999999, 999999999, 999999999, 999999999);

		// Token: 0x04003EC7 RID: 16071
		private readonly string dataFile;

		// Token: 0x04003EC8 RID: 16072
		private readonly string schemaFile;

		// Token: 0x04003EC9 RID: 16073
		private supportedTools toolData;
	}
}
