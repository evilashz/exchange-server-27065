using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Management.ClientAccess;
using Microsoft.Exchange.Management.Clients;
using Microsoft.Exchange.Management.IisTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004D2 RID: 1234
	internal static class OwaIsapiFilter
	{
		// Token: 0x06002ACA RID: 10954 RVA: 0x000AB624 File Offset: 0x000A9824
		private static void AllowIsapiExtension(DirectoryEntry virtualDirectory, string groupId)
		{
			string iisserverName = IsapiFilterCommon.GetIISServerName(virtualDirectory);
			OwaIsapiFilter.AllowIsapiExtension(iisserverName, groupId);
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000AB63F File Offset: 0x000A983F
		private static void AllowIsapiExtension(string hostName, string groupId)
		{
			ManageIsapiExtensions.SetStatus(hostName, groupId, OwaIsapiFilter.extensionBinary, true);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000AB64E File Offset: 0x000A984E
		private static void ProhibitIsapiExtension(string hostName, string groupId)
		{
			ManageIsapiExtensions.SetStatus(hostName, groupId, OwaIsapiFilter.extensionBinary, false);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000AB65D File Offset: 0x000A985D
		private static void RemoveFilter(string adsiWebSitePath)
		{
			IsapiFilter.RemoveIsapiFilter(adsiWebSitePath, OwaIsapiFilter.filterName);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000AB66C File Offset: 0x000A986C
		internal static void RemoveFilters(string hostName)
		{
			string iisDirectoryEntryPath = IisUtility.CreateAbsolutePath(IisUtility.AbsolutePathType.WebServicesRoot, hostName, null, null);
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(iisDirectoryEntryPath))
			{
				foreach (object obj in directoryEntry.Children)
				{
					DirectoryEntry directoryEntry2 = (DirectoryEntry)obj;
					if (directoryEntry2.SchemaClassName == "IIsWebServer")
					{
						OwaIsapiFilter.RemoveFilter(directoryEntry2.Path);
					}
				}
			}
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000AB708 File Offset: 0x000A9908
		private static bool IsFbaEnabled(string server, string localMetabasePath)
		{
			IMSAdminBase iisAdmin = IMSAdminBaseHelper.Create(server);
			string metabasePath = "/LM" + localMetabasePath;
			OwaIsapiFilter.FormsAuthPropertyFlags formsAuthPropertyFlags;
			int flags = OwaIsapiFilter.GetFlags(iisAdmin, metabasePath, out formsAuthPropertyFlags);
			if (flags == -2146646015)
			{
				return false;
			}
			if (flags == -2147024891)
			{
				throw new OwaIsapiFilterException(Strings.FormsAuthenticationIsEnabledAccessDeniedException(metabasePath, 45054), flags);
			}
			if (flags == -2147024893)
			{
				throw new OwaIsapiFilterException(Strings.FormsAuthenticationIsEnabledPathNotFoundException(metabasePath, 45054), flags);
			}
			if (flags < 0)
			{
				throw new OwaIsapiFilterException(Strings.FormsAuthenticationIsEnabledUnknownErrorException(metabasePath, 45054), flags);
			}
			return (formsAuthPropertyFlags & OwaIsapiFilter.FormsAuthPropertyFlags.FbaEnabled) != OwaIsapiFilter.FormsAuthPropertyFlags.None;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000AB7A0 File Offset: 0x000A99A0
		private static MetadataRecord CreateFormsRecord(MBAttributes attributes)
		{
			return new MetadataRecord(4)
			{
				Identifier = MBIdentifier.FormsAuthenticationEnabledProperty,
				Attributes = attributes,
				UserType = MBUserType.Server,
				DataType = MBDataType.Dword,
				DataTag = 0
			};
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000AB7DC File Offset: 0x000A99DC
		private static int GetFlags(IMSAdminBase iisAdmin, string metabasePath, out OwaIsapiFilter.FormsAuthPropertyFlags flags)
		{
			flags = OwaIsapiFilter.FormsAuthPropertyFlags.None;
			int result = 0;
			MetadataRecord metadataRecord = OwaIsapiFilter.CreateFormsRecord(MBAttributes.None);
			using (metadataRecord)
			{
				int num;
				result = iisAdmin.GetData(SafeMetadataHandle.MetadataMasterRootHandle, metabasePath, metadataRecord, out num);
				flags = (OwaIsapiFilter.FormsAuthPropertyFlags)Marshal.ReadInt32(metadataRecord.DataBuf.DangerousGetHandle());
			}
			return result;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000AB838 File Offset: 0x000A9A38
		private static void SetFlags(string server, string path, OwaIsapiFilter.FormsAuthPropertyFlags flags)
		{
			IMSAdminBase imsadminBase = IMSAdminBaseHelper.Create(server);
			string text = "/LM" + path;
			SafeMetadataHandle safeMetadataHandle;
			int num = IMSAdminBaseHelper.OpenKey(imsadminBase, SafeMetadataHandle.MetadataMasterRootHandle, text, MBKeyAccess.Write, 15000, out safeMetadataHandle);
			using (safeMetadataHandle)
			{
				if (num == -2147024748)
				{
					throw new FormsAuthenticationErrorPathBusyException(text);
				}
				if (num == -2147024893)
				{
					throw new FormsAuthenticationMarkPathErrorPathNotFoundException(text);
				}
				if (num < 0)
				{
					throw new OwaIsapiFilterException(Strings.FormsAuthenticationMarkPathErrorUnknownOpenError(text), num);
				}
				MetadataRecord metadataRecord = OwaIsapiFilter.CreateFormsRecord(MBAttributes.Inherit);
				using (metadataRecord)
				{
					Marshal.WriteInt32(metadataRecord.DataBuf.DangerousGetHandle(), (int)flags);
					num = imsadminBase.SetData(safeMetadataHandle, string.Empty, metadataRecord);
				}
				if (num == -2147024891)
				{
					throw new FormsAuthenticationMarkPathAccessDeniedException(text, 45054);
				}
				if (num == -2147024888)
				{
					throw new OutOfMemoryException();
				}
				if (num == -2147024893)
				{
					throw new FormsAuthenticationMarkPathErrorPathNotFoundException(text);
				}
				if (num == -2146646008)
				{
					throw new FormsAuthenticationMarkPathCannotMarkSecureAttributeException(text, 45054);
				}
				if (num < 0)
				{
					throw new FormsAuthenticationMarkPathUnknownSetError(text, 45054, num);
				}
			}
			num = IisUtility.CommitMetabaseChanges(server);
			if (num < 0)
			{
				throw new OwaIsapiFilterException(Strings.CommitMetabaseChangesException(server), num);
			}
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000AB97C File Offset: 0x000A9B7C
		private static int GetMarkedPathCount(string server, string webSitePath)
		{
			int num = 0;
			IMSAdminBase iisAdmin = IMSAdminBaseHelper.Create(server);
			webSitePath = "/LM" + webSitePath;
			List<string> list = new List<string>();
			int num2 = IMSAdminBaseHelper.GetDataPaths(iisAdmin, webSitePath, MBIdentifier.FormsAuthenticationEnabledProperty, MBDataType.Dword, ref list);
			if (num2 == -2147024893)
			{
				throw new OwaIsapiFilterException(Strings.FormsAuthenticationDeleteMarksIfUnusedPathNotFoundException(webSitePath), num2);
			}
			if (num2 < 0)
			{
				throw new OwaIsapiFilterException(Strings.FormsAuthenticationDeleteMarksIfUnusedUnknownErrorException(webSitePath, 45054), num2);
			}
			int[] array = new int[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				OwaIsapiFilter.FormsAuthPropertyFlags formsAuthPropertyFlags;
				num2 = OwaIsapiFilter.GetFlags(iisAdmin, list[i], out formsAuthPropertyFlags);
				if (num2 == -2147024891)
				{
					throw new OwaIsapiFilterException(Strings.FormsAuthenticationDeleteMarksIfUnusedCheckMarkAccessDeniedException(list[i]), num2);
				}
				if (num2 != -2147024893 && num2 != -2146646015)
				{
					if (num2 < 0)
					{
						throw new OwaIsapiFilterException(Strings.FormsAuthenticationDeleteMarksIfUnusedUnknownCheckErrorException(list[i]), num2);
					}
					num++;
				}
			}
			if (num == 1 && string.Compare(list[array[0]], webSitePath, true, CultureInfo.InvariantCulture) == 0)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000ABA8E File Offset: 0x000A9C8E
		public static void Install(DirectoryEntry virtualDirectory)
		{
			OwaIsapiFilter.AllowIsapiExtension(virtualDirectory, "MSExchangeClientAccess");
			IsapiFilterCommon.CreateFilter(virtualDirectory, OwaIsapiFilter.filterName, OwaIsapiFilter.filterDirectory, OwaIsapiFilter.extensionBinary);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000ABAB1 File Offset: 0x000A9CB1
		public static void InstallForCafe(DirectoryEntry virtualDirectory)
		{
			OwaIsapiFilter.AllowIsapiExtension(virtualDirectory, "MSExchangeCafe");
			IsapiFilterCommon.CreateFilter(virtualDirectory, OwaIsapiFilter.filterName, OwaIsapiFilter.cafeFilterDirectory, OwaIsapiFilter.cafeExtensionBinary);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000ABAD4 File Offset: 0x000A9CD4
		public static void UninstallIfLastVdir(DirectoryEntry virtualDirectory)
		{
			string iisserverName = IsapiFilterCommon.GetIISServerName(virtualDirectory);
			string iislocalPath = IsapiFilterCommon.GetIISLocalPath(virtualDirectory);
			string text = null;
			string text2 = null;
			string text3 = null;
			IisUtility.ParseApplicationRootPath(iislocalPath, ref text, ref text2, ref text3);
			if (OwaIsapiFilter.GetMarkedPathCount(iisserverName, text2) <= 1)
			{
				OwaIsapiFilter.RemoveFilter("IIS://" + iisserverName + text2);
			}
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000ABB20 File Offset: 0x000A9D20
		public static bool IsFbaEnabled(DirectoryEntry virtualDirectory)
		{
			string iisserverName = IsapiFilterCommon.GetIISServerName(virtualDirectory);
			string iislocalPath = IsapiFilterCommon.GetIISLocalPath(virtualDirectory);
			return OwaIsapiFilter.IsFbaEnabled(iisserverName, iislocalPath);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000ABB44 File Offset: 0x000A9D44
		public static void EnableFba(DirectoryEntry virtualDirectory)
		{
			string iisserverName = IsapiFilterCommon.GetIISServerName(virtualDirectory);
			string iislocalPath = IsapiFilterCommon.GetIISLocalPath(virtualDirectory);
			OwaIsapiFilter.SetFlags(iisserverName, iislocalPath, OwaIsapiFilter.FormsAuthPropertyFlags.FbaEnabled);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000ABB68 File Offset: 0x000A9D68
		public static void DisableFba(DirectoryEntry virtualDirectory)
		{
			string iisserverName = IsapiFilterCommon.GetIISServerName(virtualDirectory);
			string iislocalPath = IsapiFilterCommon.GetIISLocalPath(virtualDirectory);
			OwaIsapiFilter.SetFlags(iisserverName, iislocalPath, OwaIsapiFilter.FormsAuthPropertyFlags.None);
		}

		// Token: 0x04001FEB RID: 8171
		private const string metabaseRoot = "/LM";

		// Token: 0x04001FEC RID: 8172
		private const string adsiPrefix = "IIS://";

		// Token: 0x04001FED RID: 8173
		private const int timeoutValue = 15000;

		// Token: 0x04001FEE RID: 8174
		private const int extensionIndex = 0;

		// Token: 0x04001FEF RID: 8175
		private const int cafeExtensionIndex = 0;

		// Token: 0x04001FF0 RID: 8176
		private static readonly string extensionBinary = Microsoft.Exchange.Management.ClientAccess.IisWebServiceExtension.AllExtensions[0].ExecutableName;

		// Token: 0x04001FF1 RID: 8177
		private static readonly string filterName = "Exchange OWA Cookie Authentication ISAPI Filter";

		// Token: 0x04001FF2 RID: 8178
		private static readonly string filterDirectory = Microsoft.Exchange.Management.ClientAccess.IisWebServiceExtension.AllExtensions[0].RelativePath;

		// Token: 0x04001FF3 RID: 8179
		private static readonly string cafeExtensionBinary = CafeIisWebServiceExtension.AllExtensions[0].ExecutableName;

		// Token: 0x04001FF4 RID: 8180
		private static readonly string cafeFilterDirectory = CafeIisWebServiceExtension.AllExtensions[0].RelativePath;

		// Token: 0x020004D3 RID: 1235
		[Flags]
		private enum FormsAuthPropertyFlags
		{
			// Token: 0x04001FF6 RID: 8182
			None = 0,
			// Token: 0x04001FF7 RID: 8183
			FbaEnabled = 1
		}
	}
}
