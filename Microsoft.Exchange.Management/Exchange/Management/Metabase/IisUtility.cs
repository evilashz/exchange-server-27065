using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Clients;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Web.Administration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004BC RID: 1212
	internal static class IisUtility
	{
		// Token: 0x06002A31 RID: 10801 RVA: 0x000A8586 File Offset: 0x000A6786
		public static char[] GetReservedUriCharacters()
		{
			return (char[])IisUtility.reservedUriCharacters.Clone();
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x000A8598 File Offset: 0x000A6798
		public static ReadOnlyCollection<MetabaseProperty> DefaultWebDirProperties
		{
			get
			{
				if (IisUtility.defaultVDirProperties == null)
				{
					IisUtility.defaultVDirProperties = new List<MetabaseProperty>
					{
						new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Anonymous),
						new MetabaseProperty("DirBrowseFlags", MetabasePropertyTypes.DirBrowseFlags.ShowDate | MetabasePropertyTypes.DirBrowseFlags.ShowSize | MetabasePropertyTypes.DirBrowseFlags.ShowExtension | MetabasePropertyTypes.DirBrowseFlags.ShowLongDate | MetabasePropertyTypes.DirBrowseFlags.EnableDefaultDoc),
						new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read)
					}.AsReadOnly();
				}
				return IisUtility.defaultVDirProperties;
			}
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000A8608 File Offset: 0x000A6808
		public static DirectoryEntry CreateIISDirectoryEntry(string iisDirectoryEntryPath)
		{
			return IisUtility.CreateIISDirectoryEntry(iisDirectoryEntryPath, null, null);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000A8612 File Offset: 0x000A6812
		internal static DirectoryEntry CreateIISDirectoryEntry(string iisDirectoryEntryPath, Task.TaskErrorLoggingReThrowDelegate writeError, object identity)
		{
			return IisUtility.CreateIISDirectoryEntry(iisDirectoryEntryPath, writeError, identity, true);
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000A8620 File Offset: 0x000A6820
		internal static DirectoryEntry CreateIISDirectoryEntry(string iisDirectoryEntryPath, Task.TaskErrorLoggingReThrowDelegate writeError, object identity, bool reThrow)
		{
			DirectoryEntry result = null;
			if (iisDirectoryEntryPath == null)
			{
				throw new ArgumentNullException("iisDirectoryEntryPath");
			}
			try
			{
				DirectoryEntry.Exists(iisDirectoryEntryPath);
				result = new DirectoryEntry(iisDirectoryEntryPath);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147463168)
				{
					if (writeError == null)
					{
						throw new IISNotInstalledException(ex);
					}
					writeError(new IISNotInstalledException(ex), ErrorCategory.NotInstalled, identity, reThrow);
				}
				if (ex.ErrorCode == -2147023174)
				{
					if (writeError == null)
					{
						throw new IISNotReachableException(IisUtility.GetHostName(iisDirectoryEntryPath), ex.Message);
					}
					writeError(new IISNotReachableException(IisUtility.GetHostName(iisDirectoryEntryPath), ex.Message), ErrorCategory.ReadError, identity, reThrow);
				}
				else
				{
					COMDetailException innerException;
					try
					{
						innerException = new COMDetailException(ex.Message, iisDirectoryEntryPath, IisUtility.GetDetailInfoForException(IisUtility.GetHostName(iisDirectoryEntryPath)), ex);
					}
					catch (Exception ex2)
					{
						innerException = new COMDetailException(ex.Message, iisDirectoryEntryPath, ex2.Message, ex);
					}
					if (writeError == null)
					{
						throw new IISGeneralCOMException(ex.Message, ex.ErrorCode, innerException);
					}
					writeError(new IISGeneralCOMException(ex.Message, ex.ErrorCode, innerException), ErrorCategory.NotInstalled, identity, reThrow);
				}
			}
			return result;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000A8740 File Offset: 0x000A6940
		internal static string WebSiteFromMetabasePath(string metabasePath)
		{
			return metabasePath.Substring(0, metabasePath.IndexOf("/", 6));
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000A8755 File Offset: 0x000A6955
		internal static string ServerFromWebSite(string webSitePath)
		{
			return webSitePath.Substring(6);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000A8760 File Offset: 0x000A6960
		public static bool PropertyExists(DirectoryEntry directoryEntry, string propertyName)
		{
			ArrayList arrayList = new ArrayList(directoryEntry.Properties.PropertyNames);
			arrayList.Sort();
			return arrayList.BinarySearch(propertyName) >= 0;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000A8794 File Offset: 0x000A6994
		public static bool ApplicationPoolIsEmpty(string appPoolId, string serverName)
		{
			string hostName = Dns.GetHostName();
			IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
			string strB = null;
			if (hostEntry != null)
			{
				strB = hostEntry.HostName;
			}
			if (string.Compare(serverName, "localhost", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(serverName, hostName, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(serverName, strB, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return IisUtility.ApplicationPoolIsEmpty(appPoolId);
			}
			bool result = true;
			Stack stack = new Stack();
			string iisDirectoryEntryPath = "IIS://" + serverName + "/W3SVC";
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(iisDirectoryEntryPath))
			{
				stack.Push(directoryEntry);
				while (stack.Count > 0)
				{
					DirectoryEntry directoryEntry2 = (DirectoryEntry)stack.Pop();
					if (IisUtility.PropertyExists(directoryEntry2, "AppPoolId"))
					{
						string text = (string)directoryEntry2.Properties["AppPoolId"].Value;
						if (text != null && text == appPoolId)
						{
							result = false;
							break;
						}
					}
					foreach (object obj in directoryEntry2.Children)
					{
						DirectoryEntry directoryEntry3 = (DirectoryEntry)obj;
						if (directoryEntry3.SchemaClassName == "IIsWebService" || directoryEntry3.SchemaClassName == "IIsWebServer" || directoryEntry3.SchemaClassName == "IIsWebVirtualDir" || directoryEntry3.SchemaClassName == "IIsWebDirectory")
						{
							stack.Push(directoryEntry3);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000A894C File Offset: 0x000A6B4C
		public static bool ApplicationPoolIsEmpty(string applicationPool)
		{
			List<string> list = new List<string>(25);
			int num = IMSAdminBaseHelper.GetDataPaths("/LM/W3SVC", MBIdentifier.AppPoolId, MBDataType.String, ref list);
			if (num == -2147024893)
			{
				throw new RemoveVirtualDirectoryCouldNotAccessWebServicesRootException();
			}
			if (num < 0)
			{
				throw new RemoveVirtualDirectoryApplicationPoolSearchError(applicationPool, num);
			}
			if (list.Count == 0)
			{
				return true;
			}
			MSAdminBase msadminBase = new MSAdminBase();
			IMSAdminBase imsadminBase = (IMSAdminBase)msadminBase;
			int bufferSize = 200;
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				using (MetadataRecord metadataRecord = new MetadataRecord(bufferSize))
				{
					metadataRecord.Identifier = MBIdentifier.AppPoolId;
					metadataRecord.Attributes = MBAttributes.None;
					metadataRecord.UserType = MBUserType.Server;
					metadataRecord.DataType = MBDataType.String;
					int num2;
					num = imsadminBase.GetData(SafeMetadataHandle.MetadataMasterRootHandle, list[i], metadataRecord, out num2);
					if (num == -2147024774)
					{
						if (flag)
						{
							throw new RemoveVirtualDirectoryGetApplicationPoolUnknownError(list[i]);
						}
						flag = true;
						bufferSize = num2;
						i--;
					}
					else
					{
						flag = false;
						if (num != -2146646015)
						{
							if (num != -2147024891)
							{
								if (num != -2147024893)
								{
									if (num < 0)
									{
										throw new RemoveVirtualDirectoryGetApplicationPoolUnknownError(list[i]);
									}
									string strB = Marshal.PtrToStringUni(metadataRecord.DataBuf.DangerousGetHandle());
									if (string.Compare(applicationPool, strB, true, CultureInfo.InvariantCulture) == 0)
									{
										return false;
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000A8AB8 File Offset: 0x000A6CB8
		public static bool IsSupportedIisVersion(string serverName)
		{
			return (long)IisUtility.GetIisMajorVersion(serverName) >= 6L;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000A8AC8 File Offset: 0x000A6CC8
		public static bool IsIis7(string serverName)
		{
			return IisUtility.GetIisMajorVersion(serverName) == 7;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000A8AD4 File Offset: 0x000A6CD4
		private static int GetIisMajorVersion(string serverName)
		{
			int result;
			using (RegistryKey registryKey = IisUtility.OpenRegistryRoot(RegistryHive.LocalMachine, serverName))
			{
				if (registryKey == null)
				{
					throw new GetIISVersionException(serverName);
				}
				using (RegistryKey registryKey2 = registryKey.OpenSubKey("Software\\Microsoft\\InetStp"))
				{
					if (registryKey2 == null)
					{
						throw new GetIISVersionException(serverName);
					}
					result = (int)registryKey2.GetValue("MajorVersion");
				}
			}
			return result;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000A8B54 File Offset: 0x000A6D54
		public static object GetIisPropertyValue(string propertyName, ICollection propertyList)
		{
			object result = null;
			if (propertyList != null)
			{
				foreach (object obj in propertyList)
				{
					MetabaseProperty metabaseProperty = (MetabaseProperty)obj;
					if (metabaseProperty.Name == propertyName)
					{
						result = metabaseProperty.Value;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000A8BC0 File Offset: 0x000A6DC0
		public static bool Exists(string path)
		{
			bool result;
			try
			{
				result = DirectoryEntry.Exists(path);
			}
			catch (COMException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000A8BEC File Offset: 0x000A6DEC
		public static bool Exists(string path, string type)
		{
			bool result;
			try
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry(path))
				{
					result = (directoryEntry.SchemaClassName == type);
				}
			}
			catch (COMException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000A8C3C File Offset: 0x000A6E3C
		public static bool Exists(string parent, string name, string type)
		{
			bool result;
			try
			{
				using (DirectoryEntry directoryEntry = IisUtility.FindWebObject(parent, name, type))
				{
					result = (directoryEntry != null);
				}
			}
			catch (WebObjectNotFoundException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000A8C88 File Offset: 0x000A6E88
		public static string GetHostName(string iisPath)
		{
			Regex regex = new Regex("^\\s*[I|i][I|i][S|s]://(?<hostname>[^/]+)", RegexOptions.Compiled);
			Match match = regex.Match(iisPath);
			if (!match.Success)
			{
				throw new ArgumentException(Strings.ErrorIISPathInvalid(iisPath));
			}
			string value = match.Groups["hostname"].Value;
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1408, "GetHostName", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Metabase\\IisUtility.cs");
			Server server;
			if (string.Compare(value, "localhost", true) == 0)
			{
				server = topologyConfigurationSession.FindLocalServer();
			}
			else
			{
				ServerIdParameter serverIdParameter = ServerIdParameter.Parse(value);
				IEnumerable<Server> objects = serverIdParameter.GetObjects<Server>(null, topologyConfigurationSession);
				IEnumerator<Server> enumerator = objects.GetEnumerator();
				if (!enumerator.MoveNext())
				{
					throw new ManagementObjectNotFoundException(Strings.ErrorServerNotFound(serverIdParameter.ToString()));
				}
				server = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new ManagementObjectAmbiguousException(Strings.ErrorServerNotUnique(serverIdParameter.ToString()));
				}
			}
			return server.Fqdn;
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000A8D74 File Offset: 0x000A6F74
		public static string GetWebSiteName(string webSiteRootPath)
		{
			string result;
			try
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry(webSiteRootPath))
				{
					using (DirectoryEntry parent = directoryEntry.Parent)
					{
						if (parent != null)
						{
							result = (((string)parent.Properties["ServerComment"].Value) ?? string.Empty);
						}
						else
						{
							result = string.Empty;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is DirectoryNotFoundException || ex is COMException)
				{
					throw new WebObjectNotFoundException(Strings.ExceptionWebObjectNotFound(webSiteRootPath), ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000A8E20 File Offset: 0x000A7020
		public static string GetWebSiteRoot(string metabasePath)
		{
			if (string.IsNullOrEmpty(metabasePath))
			{
				return string.Empty;
			}
			int length = metabasePath.LastIndexOf('/');
			return metabasePath.Substring(0, length);
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000A8E4C File Offset: 0x000A704C
		public static string FindWebSitePath(string webServicesRoot, string webSiteName)
		{
			string text = null;
			string result;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(webServicesRoot))
			{
				foreach (object obj in directoryEntry.Children)
				{
					using (DirectoryEntry directoryEntry2 = (DirectoryEntry)obj)
					{
						if (string.CompareOrdinal(directoryEntry2.SchemaClassName, "IIsWebServer") == 0)
						{
							string strA = (string)directoryEntry2.Properties["ServerComment"].Value;
							if (string.Compare(strA, webSiteName, false, Thread.CurrentThread.CurrentUICulture) == 0)
							{
								if (text != null)
								{
									throw new IisUtilityCannotDisambiguateWebSiteException(webSiteName, text, directoryEntry2.Path);
								}
								text = directoryEntry2.Path;
							}
						}
					}
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000A8F18 File Offset: 0x000A7118
		public static string[] FindAllWebSites(string hostName)
		{
			string iisDirectoryEntryPath = string.Format("IIS://{0}/W3SVC", hostName);
			List<string> list = new List<string>();
			string[] result;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(iisDirectoryEntryPath))
			{
				foreach (object obj in directoryEntry.Children)
				{
					using (DirectoryEntry directoryEntry2 = (DirectoryEntry)obj)
					{
						if (string.CompareOrdinal(directoryEntry2.SchemaClassName, "IIsWebServer") == 0)
						{
							string item = (string)directoryEntry2.Properties["ServerComment"].Value;
							list.Add(item);
						}
					}
				}
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000A8FDC File Offset: 0x000A71DC
		public static IList<string> FindAllWebSitePaths(string hostName)
		{
			string iisDirectoryEntryPath = "IIS://" + hostName + "/W3SVC";
			List<string> list = new List<string>();
			IList<string> result;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(iisDirectoryEntryPath))
			{
				foreach (object obj in directoryEntry.Children)
				{
					DirectoryEntry directoryEntry2 = (DirectoryEntry)obj;
					if (string.CompareOrdinal(directoryEntry2.SchemaClassName, "IIsWebServer") == 0)
					{
						list.Add(directoryEntry2.Path);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000A9090 File Offset: 0x000A7290
		public static string FindWebSiteRootPath(string webSiteName, string hostName)
		{
			string text = IisUtility.CreateAbsolutePath(IisUtility.AbsolutePathType.Host, hostName, null, null);
			string webServicesRoot = IisUtility.CreateAbsolutePath(IisUtility.AbsolutePathType.WebServicesRoot, hostName, null, null);
			string text2 = IisUtility.FindWebSitePath(webServicesRoot, webSiteName);
			if (text2 == null)
			{
				return null;
			}
			text2 = text2.Substring(text.Length);
			return text2 + "/ROOT";
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000A90D8 File Offset: 0x000A72D8
		public static string FindWebSiteRoot(string webSiteName, string hostName)
		{
			string webServicesRoot = string.Format("IIS://{0}/W3SVC", hostName);
			string text = IisUtility.FindWebSitePath(webServicesRoot, webSiteName);
			if (text == null)
			{
				throw new WebObjectNotFoundException(Strings.ErrorCanNotFindWebObject(webSiteName, "IIsWebServer", hostName));
			}
			return text + "/ROOT";
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000A911C File Offset: 0x000A731C
		public static string CreateAbsolutePath(IisUtility.AbsolutePathType pathType, string hostName, string webSiteRootPath, string virtualDirectory)
		{
			StringBuilder stringBuilder = new StringBuilder("IIS://");
			stringBuilder.Append(hostName);
			switch (pathType)
			{
			case IisUtility.AbsolutePathType.WebServicesRoot:
				stringBuilder.Append("/W3SVC");
				break;
			case IisUtility.AbsolutePathType.WebSiteRoot:
				stringBuilder.Append(webSiteRootPath);
				break;
			case IisUtility.AbsolutePathType.VirtualDirectory:
				stringBuilder.Append(webSiteRootPath);
				stringBuilder.Append('/');
				stringBuilder.Append(virtualDirectory);
				break;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000A9190 File Offset: 0x000A7390
		public static void SetProperties(DirectoryEntry webObj, ICollection properties)
		{
			if (webObj == null || properties == null)
			{
				throw new ArgumentException(Strings.ErrorWebObjOrPropertiesNull);
			}
			foreach (object obj in properties)
			{
				MetabaseProperty metabaseProperty = (MetabaseProperty)obj;
				if (metabaseProperty.EraseOldValue)
				{
					webObj.Properties[metabaseProperty.Name].Value = metabaseProperty.Value;
				}
				else
				{
					webObj.Properties[metabaseProperty.Name].Add(metabaseProperty.Value);
				}
			}
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000A9238 File Offset: 0x000A7438
		public static void SetProperty(DirectoryEntry webObj, string propertyName, object propertyValue, bool eraseOldValue)
		{
			if (webObj == null || propertyName == null)
			{
				throw new ArgumentException(Strings.ErrorWebObjOrPropertiesNull);
			}
			if (eraseOldValue)
			{
				webObj.Properties[propertyName].Value = propertyValue;
				return;
			}
			webObj.Properties[propertyName].Add(propertyValue);
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000A9284 File Offset: 0x000A7484
		public static MetabaseProperty[] GetProperties(DirectoryEntry webObj)
		{
			if (webObj == null)
			{
				throw new ArgumentNullException("webObj");
			}
			int num = 0;
			int num2 = 0;
			PropertyCollection properties = webObj.Properties;
			MetabaseProperty[] array = null;
			try
			{
				if (properties != null)
				{
					foreach (object obj in properties.PropertyNames)
					{
						string text = (string)obj;
						num2++;
					}
				}
				array = new MetabaseProperty[num2];
				if (properties != null)
				{
					foreach (object obj2 in properties.PropertyNames)
					{
						string text2 = (string)obj2;
						array[num++] = new MetabaseProperty(text2, webObj.Properties[text2].Value, true);
					}
				}
			}
			catch (COMException ex)
			{
				throw new IISGeneralCOMException(ex.Message, ex.ErrorCode, ex);
			}
			return array;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000A939C File Offset: 0x000A759C
		public static DirectoryEntry FindWebObject(string parent, string name, string type)
		{
			DirectoryEntry result;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(parent))
			{
				try
				{
					result = directoryEntry.Children.Find(name, type);
				}
				catch (Exception ex)
				{
					if (ex is DirectoryNotFoundException || ex is COMException)
					{
						throw new WebObjectNotFoundException(Strings.ExceptionWebObjectNotFound(name), ex);
					}
					throw;
				}
			}
			return result;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000A9408 File Offset: 0x000A7608
		public static void DeleteWebObject(string parent, string name, string type)
		{
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(parent))
			{
				DirectoryEntry entry = IisUtility.FindWebObject(parent, name, type);
				directoryEntry.Children.Remove(entry);
				directoryEntry.CommitChanges();
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000A9454 File Offset: 0x000A7654
		public static DirectoryEntry FindWebDirObject(string parent, string name)
		{
			DirectoryEntry result;
			try
			{
				result = IisUtility.FindWebObject(parent, name, "IIsWebVirtualDir");
			}
			catch (WebObjectNotFoundException)
			{
				result = IisUtility.FindWebObject(parent, name, "IIsWebDirectory");
			}
			return result;
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000A9494 File Offset: 0x000A7694
		public static bool WebDirObjectExists(string parent, string name)
		{
			bool result;
			try
			{
				IisUtility.FindWebDirObject(parent, name);
				result = true;
			}
			catch (WebObjectNotFoundException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000A94C4 File Offset: 0x000A76C4
		public static void DeleteWebDirObject(string parent, string name)
		{
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(parent))
			{
				DirectoryEntry entry = IisUtility.FindWebDirObject(parent, name);
				directoryEntry.Children.Remove(entry);
				directoryEntry.CommitChanges();
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000A9510 File Offset: 0x000A7710
		public static string GetAppPoolRootPath(string webSite)
		{
			if (string.IsNullOrEmpty(webSite))
			{
				throw new ArgumentException(Strings.ErrorWebSiteNullOrEmpty);
			}
			return "IIS://" + webSite + "/W3SVC/AppPools";
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000A9548 File Offset: 0x000A7748
		public static DirectoryEntry CreateApplicationPool(string hostName, string appPoolName)
		{
			if (!IisUtility.IsSupportedIisVersion(hostName))
			{
				throw new UnmatchedIisVersionException(Strings.ExceptionInvalidIisVersion);
			}
			string appPoolRootPath = IisUtility.GetAppPoolRootPath(hostName);
			DirectoryEntry result;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(appPoolRootPath))
			{
				if (IisUtility.Exists(appPoolRootPath, appPoolName, "IIsApplicationPool"))
				{
					throw new WebObjectAlreadyExistsException(Strings.ExceptionWebObjectAlreadyExists(appPoolName));
				}
				DirectoryEntry directoryEntry2 = directoryEntry.Children.Add(appPoolName, "IIsApplicationPool");
				IisUtility.SetProperty(directoryEntry2, "PeriodicRestartTime", 0, true);
				IisUtility.SetProperty(directoryEntry2, "IdleTimeout", 0, true);
				IisUtility.SetProperty(directoryEntry2, "ManagedRuntimeVersion", "v4.0", true);
				directoryEntry2.CommitChanges();
				IisUtility.CommitMetabaseChanges(hostName);
				result = directoryEntry2;
			}
			return result;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000A9604 File Offset: 0x000A7804
		public static void DeleteApplicationPool(string hostName, string appPoolName)
		{
			if (!IisUtility.IsSupportedIisVersion(hostName))
			{
				throw new UnmatchedIisVersionException(Strings.ExceptionInvalidIisVersion);
			}
			string appPoolRootPath = IisUtility.GetAppPoolRootPath(hostName);
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(appPoolRootPath))
			{
				DirectoryEntry entry = IisUtility.FindWebObject(appPoolRootPath, appPoolName, "IIsApplicationPool");
				directoryEntry.Children.Remove(entry);
				directoryEntry.CommitChanges();
				IisUtility.CommitMetabaseChanges(hostName);
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000A9674 File Offset: 0x000A7874
		public static void AssignApplicationPool(DirectoryEntry vdir, string appPoolName, MetabasePropertyTypes.AppIsolated appMode, bool createAppPool)
		{
			if (vdir == null)
			{
				throw new ArgumentNullException("vdir");
			}
			object[] args = new object[]
			{
				appMode,
				appPoolName,
				createAppPool
			};
			vdir.Invoke("AppCreate3", args);
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000A96BB File Offset: 0x000A78BB
		public static void AssignApplicationPool(DirectoryEntry vdir, string appPoolName)
		{
			IisUtility.AssignApplicationPool(vdir, appPoolName, MetabasePropertyTypes.AppIsolated.Pooled, false);
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000A96C8 File Offset: 0x000A78C8
		public static DirectoryEntry CreateVirtualDirectory(string parentPath, string physicalPath, string name, string applicationPool, MetabasePropertyTypes.AppPoolIdentityType appPoolIdentityType, MetabasePropertyTypes.ManagedPipelineMode appPoolManagedPipelineMode, ICollection customProperties)
		{
			DirectoryEntry directoryEntry = IisUtility.CreateWebDirObject(parentPath, physicalPath, name);
			if (customProperties != null)
			{
				IisUtility.SetProperties(directoryEntry, customProperties);
			}
			directoryEntry.CommitChanges();
			string hostName = IisUtility.GetHostName(parentPath);
			if (!string.IsNullOrEmpty(applicationPool) && IisUtility.IsSupportedIisVersion(hostName))
			{
				string appPoolRootPath = IisUtility.GetAppPoolRootPath(hostName);
				if (!IisUtility.Exists(appPoolRootPath, applicationPool, "IIsApplicationPool"))
				{
					using (DirectoryEntry directoryEntry2 = IisUtility.CreateApplicationPool(hostName, applicationPool))
					{
						IisUtility.SetProperty(directoryEntry2, "AppPoolIdentityType", (int)appPoolIdentityType, true);
						IisUtility.SetProperty(directoryEntry2, "managedPipelineMode", (int)appPoolManagedPipelineMode, true);
						directoryEntry2.CommitChanges();
					}
				}
				IisUtility.AssignApplicationPool(directoryEntry, applicationPool);
			}
			return directoryEntry;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000A9774 File Offset: 0x000A7974
		public static DirectoryEntry CreateWebDirObject(string parent, string path, string vdirName)
		{
			DirectoryEntry result;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(parent))
			{
				if (path == null)
				{
					if (IisUtility.Exists(parent, vdirName, "IIsWebDirectory"))
					{
						throw new WebObjectAlreadyExistsException(Strings.ExceptionWebObjectAlreadyExists(vdirName));
					}
					result = IisUtility.CreateWebDirectory(directoryEntry, vdirName);
				}
				else
				{
					if (IisUtility.Exists(parent, vdirName, "IIsWebVirtualDir"))
					{
						throw new WebObjectAlreadyExistsException(Strings.ExceptionWebObjectAlreadyExists(vdirName));
					}
					result = IisUtility.CreateVirtualDirectory(directoryEntry, path, vdirName);
				}
			}
			return result;
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000A97F0 File Offset: 0x000A79F0
		public static DirectoryEntry CreateVirtualDirectory(DirectoryEntry parentWebDir, string path, string name)
		{
			if (parentWebDir == null)
			{
				throw new ArgumentNullException("parentWebDir");
			}
			DirectoryEntry directoryEntry = parentWebDir.Children.Add(name, "IIsWebVirtualDir");
			IisUtility.SetProperties(directoryEntry, IisUtility.DefaultWebDirProperties);
			IisUtility.SetProperty(directoryEntry, "Path", path, true);
			directoryEntry.CommitChanges();
			return directoryEntry;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000A983C File Offset: 0x000A7A3C
		public static void ParseApplicationRootPath(string applicationRootPath, ref string webSiteRootPath, ref string virtualDirectoryName)
		{
			string text = null;
			IisUtility.ParseApplicationRootPath(applicationRootPath, ref webSiteRootPath, ref text, ref virtualDirectoryName);
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000A9858 File Offset: 0x000A7A58
		public static void ParseApplicationRootPath(string applicationRootPath, ref string webSiteRootPath, ref string webSitePath, ref string virtualDirectoryName)
		{
			int num = applicationRootPath.LastIndexOf('/');
			if (num == -1)
			{
				throw new IisUtilityInvalidApplicationRootPathException(applicationRootPath);
			}
			webSiteRootPath = applicationRootPath.Substring(0, num);
			virtualDirectoryName = applicationRootPath.Substring(num + 1);
			num = webSiteRootPath.LastIndexOf('/');
			webSitePath = webSiteRootPath.Substring(0, num);
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000A98A4 File Offset: 0x000A7AA4
		public static DirectoryEntry CreateWebDirectory(DirectoryEntry parentWebDir, string name)
		{
			if (parentWebDir == null)
			{
				throw new ArgumentNullException("parentWebDir");
			}
			DirectoryEntry directoryEntry = parentWebDir.Children.Add(name, "IIsWebDirectory");
			IisUtility.SetProperties(directoryEntry, IisUtility.DefaultWebDirProperties);
			directoryEntry.CommitChanges();
			return directoryEntry;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000A98E4 File Offset: 0x000A7AE4
		public static void ManageApplicationPool(IisUtility.AppPoolAction action, string webSiteName, string appPoolName)
		{
			string appPoolRootPath = IisUtility.GetAppPoolRootPath(webSiteName);
			using (DirectoryEntry directoryEntry = IisUtility.FindWebObject(appPoolRootPath, appPoolName, "IIsApplicationPool"))
			{
				if (directoryEntry == null)
				{
					throw new WebObjectNotFoundException(Strings.ExceptionWebObjectNotFound(appPoolName));
				}
				string methodName = action.ToString();
				directoryEntry.Invoke(methodName, null);
			}
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000A9948 File Offset: 0x000A7B48
		public static DirectoryEntry FindOrCreateWebObject(string parent, string name, string type, out bool created)
		{
			created = false;
			string str = "/";
			if (parent[parent.Length - 1] == '/')
			{
				str = "";
			}
			parent + str + name;
			DirectoryEntry directoryEntry = null;
			try
			{
				directoryEntry = IisUtility.FindWebObject(parent, name, string.Empty);
				if (string.IsNullOrEmpty((string)directoryEntry.InvokeGet("keyType")))
				{
					directoryEntry.InvokeSet("keyType", new object[]
					{
						type
					});
					directoryEntry.CommitChanges();
				}
				return directoryEntry;
			}
			catch (WebObjectNotFoundException)
			{
			}
			catch (Exception)
			{
				if (directoryEntry != null)
				{
					directoryEntry.Close();
				}
				throw;
			}
			try
			{
				using (DirectoryEntry directoryEntry2 = new DirectoryEntry(parent))
				{
					directoryEntry = directoryEntry2.Children.Add(name, type);
					created = true;
				}
			}
			catch (COMException innerException)
			{
				throw new IisUtilityWebObjectNotCreatedException(parent, name, type, innerException);
			}
			return directoryEntry;
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000A9A44 File Offset: 0x000A7C44
		internal static bool CheckForAuthenticationMethod(DirectoryEntry virtualDirectory, AuthenticationMethodFlags method)
		{
			return IisUtility.CheckForAuthenticationMethod(virtualDirectory, method, false);
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000A9A50 File Offset: 0x000A7C50
		internal static bool CheckForAuthenticationMethod(DirectoryEntry virtualDirectory, AuthenticationMethodFlags method, bool ignoreAnonymousOnCert)
		{
			PropertyValueCollection propertyValueCollection = virtualDirectory.Properties["AuthFlags"];
			if (propertyValueCollection.Value == null)
			{
				return false;
			}
			int num = (int)propertyValueCollection.Value;
			PropertyValueCollection propertyValueCollection2 = virtualDirectory.Properties["NTAuthenticationProviders"];
			string[] source = new string[0];
			if (propertyValueCollection2.Value != null)
			{
				source = ((string)propertyValueCollection2.Value).Split(new char[]
				{
					','
				});
			}
			bool flag = source.Contains("NTLM", StringComparer.Ordinal) && source.Contains("Negotiate", StringComparer.Ordinal);
			bool flag2 = source.Contains("Negotiate:MSOIDSSP", StringComparer.Ordinal);
			bool result = false;
			if (method <= AuthenticationMethodFlags.WSSecurity)
			{
				switch (method)
				{
				case AuthenticationMethodFlags.None:
					return 0L == 0L;
				case AuthenticationMethodFlags.Basic:
					return (2L & (long)num) == 2L;
				case AuthenticationMethodFlags.Ntlm:
					return (4L & (long)num) == 4L && flag;
				case AuthenticationMethodFlags.Basic | AuthenticationMethodFlags.Ntlm:
				case AuthenticationMethodFlags.Basic | AuthenticationMethodFlags.Fba:
				case AuthenticationMethodFlags.Ntlm | AuthenticationMethodFlags.Fba:
				case AuthenticationMethodFlags.Basic | AuthenticationMethodFlags.Ntlm | AuthenticationMethodFlags.Fba:
					return result;
				case AuthenticationMethodFlags.Fba:
					if (OwaIsapiFilter.IsFbaEnabled(virtualDirectory))
					{
						return (2L & (long)num) == 2L;
					}
					return result;
				case AuthenticationMethodFlags.Digest:
					return (16L & (long)num) == 16L;
				default:
					if (method != AuthenticationMethodFlags.WSSecurity)
					{
						return result;
					}
					break;
				}
			}
			else
			{
				if (method == AuthenticationMethodFlags.Certificate)
				{
					return (ignoreAnonymousOnCert || (1L & (long)num) == 1L) && IisUtility.IsSslCertificateEnabled(virtualDirectory);
				}
				if (method != AuthenticationMethodFlags.OAuth)
				{
					if (method != AuthenticationMethodFlags.LiveIdNegotiate)
					{
						return result;
					}
					return (4L & (long)num) == 4L && flag2;
				}
			}
			result = ((1L & (long)num) == 1L);
			return result;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000A9BEC File Offset: 0x000A7DEC
		private static bool IsSslCertificateEnabled(DirectoryEntry vDir)
		{
			int? num = (int?)vDir.Properties["AccessSSLFlags"].Value;
			int num2 = 32;
			return num != null && (num.Value & num2) == num2;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000A9C30 File Offset: 0x000A7E30
		private static void SetSslCertificateEnabled(DirectoryEntry vDir, bool enabled)
		{
			PropertyValueCollection propertyValueCollection = vDir.Properties["AccessSSLFlags"];
			uint num = (uint)((propertyValueCollection.Value != null) ? ((int)propertyValueCollection.Value) : 264);
			uint num2 = enabled ? (num | 32U) : (num & 4294967199U);
			if (propertyValueCollection.Value != null)
			{
				propertyValueCollection.Value = num2;
				return;
			}
			propertyValueCollection.Add(num2);
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000A9C9C File Offset: 0x000A7E9C
		internal static void SetAuthenticationMethod(DirectoryEntry virtualDirectory, AuthenticationMethodFlags method, bool value)
		{
			PropertyValueCollection propertyValueCollection = virtualDirectory.Properties["AuthFlags"];
			PropertyValueCollection propertyValueCollection2 = virtualDirectory.Properties["NTAuthenticationProviders"];
			int num = 0;
			if (propertyValueCollection.Value != null)
			{
				num = (int)propertyValueCollection.Value;
			}
			uint num2 = (uint)num;
			uint num3 = 0U;
			string[] source = new string[0];
			if (propertyValueCollection2.Value != null)
			{
				source = ((string)propertyValueCollection2.Value).Split(new char[]
				{
					','
				});
			}
			bool flag = source.Contains("NTLM", StringComparer.OrdinalIgnoreCase) && source.Contains("Negotiate", StringComparer.OrdinalIgnoreCase);
			bool flag2 = source.Contains("Negotiate:MSOIDSSP", StringComparer.OrdinalIgnoreCase);
			if (method <= AuthenticationMethodFlags.LiveIdBasic)
			{
				if (method <= AuthenticationMethodFlags.WindowsIntegrated)
				{
					switch (method)
					{
					case AuthenticationMethodFlags.None:
						num2 = 0U;
						goto IL_188;
					case AuthenticationMethodFlags.Basic:
						num3 = 2U;
						goto IL_188;
					case AuthenticationMethodFlags.Ntlm:
						break;
					case AuthenticationMethodFlags.Basic | AuthenticationMethodFlags.Ntlm:
					case AuthenticationMethodFlags.Basic | AuthenticationMethodFlags.Fba:
					case AuthenticationMethodFlags.Ntlm | AuthenticationMethodFlags.Fba:
					case AuthenticationMethodFlags.Basic | AuthenticationMethodFlags.Ntlm | AuthenticationMethodFlags.Fba:
						goto IL_188;
					case AuthenticationMethodFlags.Fba:
						return;
					case AuthenticationMethodFlags.Digest:
						num3 = 16U;
						goto IL_188;
					default:
						if (method != AuthenticationMethodFlags.WindowsIntegrated)
						{
							goto IL_188;
						}
						break;
					}
					num3 = 4U;
					flag = value;
					if (!value && (num2 & num3) != 0U && flag2)
					{
						value = true;
						goto IL_188;
					}
					goto IL_188;
				}
				else if (method != AuthenticationMethodFlags.LiveIdFba && method != AuthenticationMethodFlags.LiveIdBasic)
				{
					goto IL_188;
				}
			}
			else if (method <= AuthenticationMethodFlags.Certificate)
			{
				if (method == AuthenticationMethodFlags.WSSecurity)
				{
					num3 = 1U;
					goto IL_188;
				}
				if (method != AuthenticationMethodFlags.Certificate)
				{
					goto IL_188;
				}
				num3 = 1U;
				IisUtility.SetSslCertificateEnabled(virtualDirectory, value);
				goto IL_188;
			}
			else if (method != AuthenticationMethodFlags.NegoEx)
			{
				if (method != AuthenticationMethodFlags.LiveIdNegotiate)
				{
					goto IL_188;
				}
				num3 = 4U;
				flag2 = value;
				if (!value && (num2 & num3) != 0U && flag)
				{
					value = true;
					goto IL_188;
				}
				goto IL_188;
			}
			if (value)
			{
				num2 = 0U;
			}
			IL_188:
			if (value)
			{
				num2 |= num3;
			}
			else
			{
				num3 ^= uint.MaxValue;
				num2 &= num3;
			}
			if (virtualDirectory.Properties["AuthFlags"].Value != null)
			{
				virtualDirectory.Properties["AuthFlags"].Value = num2;
			}
			else
			{
				virtualDirectory.Properties["AuthFlags"].Add(num2);
			}
			List<string> list = new List<string>(4);
			if (flag2)
			{
				list.Add("Negotiate:MSOIDSSP");
			}
			if (flag)
			{
				list.Add("Negotiate");
				list.Add("NTLM");
			}
			string text = string.Join(",", list.ToArray());
			if (string.Compare((string)propertyValueCollection2.Value, text, StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				if (virtualDirectory.Properties["NTAuthenticationProviders"].Value != null)
				{
					virtualDirectory.Properties["NTAuthenticationProviders"].Value = text;
					return;
				}
				virtualDirectory.Properties["NTAuthenticationProviders"].Add(text);
			}
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000A9F3C File Offset: 0x000A813C
		internal static void SetAuthenticationMethod(DirectoryEntry virtualDirectory, MetabasePropertyTypes.AuthFlags mask, bool value)
		{
			PropertyValueCollection propertyValueCollection = virtualDirectory.Properties["AuthFlags"];
			uint num = (uint)((propertyValueCollection.Value != null) ? ((int)propertyValueCollection.Value) : 0);
			if (value)
			{
				num |= (uint)mask;
			}
			else
			{
				num &= (uint)(~(uint)mask);
			}
			if (propertyValueCollection.Value != null)
			{
				propertyValueCollection.Value = num;
				return;
			}
			propertyValueCollection.Add(num);
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000A9FA4 File Offset: 0x000A81A4
		public static bool SSLEnabled(string webObjectPath)
		{
			if (webObjectPath.ToLower().IndexOf("/w3svc/") == -1)
			{
				return false;
			}
			do
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry(webObjectPath))
				{
					int? num;
					try
					{
						num = (int?)directoryEntry.Properties["AccessSSLFlags"].Value;
					}
					catch (COMException)
					{
						return false;
					}
					if (num == null)
					{
						int startIndex = webObjectPath.LastIndexOf('/');
						webObjectPath = webObjectPath.Remove(startIndex);
					}
					else
					{
						if ((num.Value & 8) == 0)
						{
							return false;
						}
						return true;
					}
				}
			}
			while (!webObjectPath.ToLower().EndsWith("w3svc"));
			return false;
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000AA05C File Offset: 0x000A825C
		public static string GetWebSiteSslCertificate(string webObjectPath)
		{
			string result;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(webObjectPath))
			{
				MetabaseProperty[] properties = IisUtility.GetProperties(directoryEntry);
				object[] array = (object[])IisUtility.GetIisPropertyValue("SSLCertHash", properties);
				if (array == null)
				{
					result = null;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in array)
					{
						string text = obj.ToString();
						if (text.Length == 1)
						{
							stringBuilder.Append('0');
						}
						stringBuilder.Append(text);
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000AA100 File Offset: 0x000A8300
		public static bool SslRequiredOnTheRoot(string webSiteName)
		{
			string text;
			if (string.IsNullOrEmpty(webSiteName))
			{
				text = "IIS://localhost/W3SVC/1/Root";
			}
			else
			{
				string webServicesRoot = IisUtility.CreateAbsolutePath(IisUtility.AbsolutePathType.WebServicesRoot, "localhost", null, null);
				text = IisUtility.FindWebSitePath(webServicesRoot, webSiteName);
				text += "/ROOT";
			}
			bool result;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(text))
			{
				try
				{
					int? num = (int?)directoryEntry.Properties["AccessSSLFlags"].Value;
					if (num != null)
					{
						result = ((num.Value & 8) != 0);
					}
					else
					{
						result = false;
					}
				}
				catch (COMException)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000AA1B0 File Offset: 0x000A83B0
		public static void SetSslCertificateByName(string webSiteName, X509Certificate2 certificate, bool requireSsl)
		{
			string webSitePath;
			if (string.IsNullOrEmpty(webSiteName))
			{
				webSitePath = "IIS://localhost/W3SVC/1";
			}
			else
			{
				string webServicesRoot = IisUtility.CreateAbsolutePath(IisUtility.AbsolutePathType.WebServicesRoot, "localhost", null, null);
				webSitePath = IisUtility.FindWebSitePath(webServicesRoot, webSiteName);
			}
			IisUtility.SetSslCertificate(webSitePath, certificate, requireSsl);
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000AA1EC File Offset: 0x000A83EC
		public static void SetSslCertificate(string webSitePath, X509Certificate2 certificate, bool requireSsl)
		{
			IMSAdminBase imsadminBase = null;
			try
			{
				imsadminBase = IMSAdminBaseHelper.Create();
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147221164)
				{
					throw new IISNotInstalledException(ex);
				}
				throw;
			}
			if (!DirectoryEntry.Exists(webSitePath))
			{
				throw new FormsAuthenticationMarkPathErrorPathNotFoundException(webSitePath);
			}
			int timeOut = 15000;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(webSitePath))
			{
				MetabaseProperty[] properties = IisUtility.GetProperties(directoryEntry);
				if (IisUtility.GetIisPropertyValue("SecureBindings", properties) == null)
				{
					IisUtility.SetProperty(directoryEntry, "SecureBindings", ":443:", false);
					directoryEntry.CommitChanges();
				}
			}
			int startIndex = webSitePath.LastIndexOf('/');
			string text = "/LM/W3SVC" + webSitePath.Substring(startIndex);
			SafeMetadataHandle safeMetadataHandle;
			Hresults hresult = (Hresults)IMSAdminBaseHelper.OpenKey(imsadminBase, SafeMetadataHandle.MetadataMasterRootHandle, text, MBKeyAccess.Write, timeOut, out safeMetadataHandle);
			IisUtility.CheckSuccess(text, hresult, 0);
			using (safeMetadataHandle)
			{
				byte[] certHash = certificate.GetCertHash();
				MetadataRecord metadataRecord = new MetadataRecord(certHash.Length);
				using (metadataRecord)
				{
					Marshal.Copy(certHash, 0, metadataRecord.DataBuf.DangerousGetHandle(), certHash.Length);
					metadataRecord.Identifier = MBIdentifier.SslCertHash;
					metadataRecord.Attributes = MBAttributes.Inherit;
					metadataRecord.UserType = MBUserType.Server;
					metadataRecord.DataType = MBDataType.Binary;
					hresult = (Hresults)imsadminBase.SetData(safeMetadataHandle, string.Empty, metadataRecord);
					IisUtility.CheckSuccess(text, hresult, (int)metadataRecord.Identifier);
				}
				metadataRecord = new MetadataRecord("MY");
				using (metadataRecord)
				{
					metadataRecord.Identifier = MBIdentifier.SslStoreName;
					metadataRecord.Attributes = MBAttributes.Inherit;
					metadataRecord.UserType = MBUserType.Server;
					hresult = (Hresults)imsadminBase.SetData(safeMetadataHandle, string.Empty, metadataRecord);
					IisUtility.CheckSuccess(text, hresult, (int)metadataRecord.Identifier);
				}
			}
			if (requireSsl)
			{
				webSitePath += "/ROOT";
				using (DirectoryEntry directoryEntry2 = IisUtility.CreateIISDirectoryEntry(webSitePath))
				{
					int num = 264;
					try
					{
						int? num2 = (int?)directoryEntry2.Properties["AccessSSLFlags"].Value;
						if (num2 != null)
						{
							num |= num2.Value;
						}
					}
					catch (COMException)
					{
					}
					directoryEntry2.Properties["AccessSSLFlags"].Value = num;
					directoryEntry2.CommitChanges();
				}
			}
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000AA480 File Offset: 0x000A8680
		public static void FindWebObjectsRecursively(DirectoryEntry parent, string objectClass, List<DirectoryEntry> results)
		{
			foreach (object obj in parent.Children)
			{
				DirectoryEntry directoryEntry = (DirectoryEntry)obj;
				try
				{
					IisUtility.FindWebObjectsRecursively(directoryEntry, objectClass, results);
					if (objectClass == null || string.Compare(directoryEntry.SchemaClassName, objectClass) == 0)
					{
						results.Add(directoryEntry);
						directoryEntry = null;
					}
				}
				finally
				{
					if (directoryEntry != null)
					{
						directoryEntry.Dispose();
					}
				}
			}
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000AA4F0 File Offset: 0x000A86F0
		public static bool IsAnyWebVirtualDirUsingThisExecutableInScriptMap(string hostName, string executableName)
		{
			string iisDirectoryEntryPath = IisUtility.CreateAbsolutePath(IisUtility.AbsolutePathType.WebServicesRoot, hostName, null, null);
			List<DirectoryEntry> list = new List<DirectoryEntry>();
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(iisDirectoryEntryPath))
			{
				try
				{
					IisUtility.FindWebObjectsRecursively(directoryEntry, "IIsWebVirtualDir", list);
					foreach (DirectoryEntry directoryEntry2 in list)
					{
						foreach (object obj in directoryEntry2.Properties["ScriptMaps"])
						{
							string[] array = ((string)obj).Split(new char[]
							{
								','
							});
							if (array.Length >= 2 && string.Compare(array[1], executableName, true, CultureInfo.InvariantCulture) == 0)
							{
								return true;
							}
						}
					}
				}
				finally
				{
					foreach (DirectoryEntry directoryEntry3 in list)
					{
						directoryEntry3.Dispose();
					}
				}
			}
			return false;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000AA658 File Offset: 0x000A8858
		public static int CommitMetabaseChanges(string server)
		{
			if (server != null && (server.Equals("localhost") || server.Equals("")))
			{
				server = null;
			}
			IMSAdminBase imsadminBase = IMSAdminBaseHelper.Create(server);
			return imsadminBase.SaveData();
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000AA694 File Offset: 0x000A8894
		public static DirectoryEntry FindWebDirFromDisplayString(string hostName, string displayString)
		{
			int num = displayString.Length;
			num = displayString.LastIndexOf(')');
			if (num == -1)
			{
				return null;
			}
			num--;
			int num2 = num;
			int num3 = 1;
			for (;;)
			{
				num = displayString.LastIndexOfAny(new char[]
				{
					'(',
					')'
				}, num, num + 1);
				if (num == -1)
				{
					break;
				}
				if (displayString[num] == ')')
				{
					num3++;
				}
				else
				{
					num3--;
					if (num3 == 0)
					{
						goto Block_4;
					}
				}
			}
			return null;
			Block_4:
			int num4 = num + 1;
			num--;
			string webSiteName = displayString.Substring(num4, num2 - num4 + 1).Trim();
			string name = displayString.Substring(0, num).Trim();
			string parent = IisUtility.FindWebSiteRoot(webSiteName, hostName);
			return IisUtility.FindWebDirObject(parent, name);
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000AA74C File Offset: 0x000A894C
		private static void CheckSuccess(string iisAdminPath, Hresults hresult, int propertyId)
		{
			if (hresult == Hresults.ErrorPathBusy)
			{
				throw new FormsAuthenticationErrorPathBusyException(iisAdminPath);
			}
			if (hresult == Hresults.ErrorPathNotFound)
			{
				throw new FormsAuthenticationMarkPathErrorPathNotFoundException(iisAdminPath);
			}
			if (hresult == Hresults.EAccessDenied)
			{
				throw new FormsAuthenticationMarkPathAccessDeniedException(iisAdminPath, propertyId);
			}
			if (hresult == Hresults.ErrorNotEnoughMemory)
			{
				throw new OutOfMemoryException();
			}
			if (hresult == (Hresults)(-2146646008))
			{
				throw new FormsAuthenticationMarkPathCannotMarkSecureAttributeException(iisAdminPath, propertyId);
			}
			if (hresult < (Hresults)0)
			{
				throw new FormsAuthenticationMarkPathUnknownSetError(iisAdminPath, propertyId, (int)hresult);
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000AA7B4 File Offset: 0x000A89B4
		public static RegistryKey OpenRegistryRoot(RegistryHive hive, string serverName)
		{
			if (!string.IsNullOrEmpty(serverName) && !IisUtility.IsLocalMachine(serverName))
			{
				RegistryKey result;
				try
				{
					result = RegistryKey.OpenRemoteBaseKey(hive, serverName);
				}
				catch (IOException ex)
				{
					throw new InvalidOperationException(Strings.ErrorAccessingRegistryRaisesException(serverName, ex.Message));
				}
				return result;
			}
			switch (hive)
			{
			case RegistryHive.ClassesRoot:
				return Registry.ClassesRoot;
			case RegistryHive.CurrentUser:
				return Registry.CurrentUser;
			case RegistryHive.LocalMachine:
				return Registry.LocalMachine;
			case RegistryHive.Users:
				return Registry.Users;
			case RegistryHive.PerformanceData:
				return Registry.PerformanceData;
			case RegistryHive.CurrentConfig:
				return Registry.CurrentConfig;
			default:
				throw new ArgumentException(Strings.ErrorInvalidRegistryHive(hive.ToString()), "hive");
			}
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000AA870 File Offset: 0x000A8A70
		internal static bool VerifyModuleInstalled(string metabasePath, string moduleName, bool useApplicationHostConfigForModules, bool isChildVDirApplication = false)
		{
			bool result = false;
			if (string.IsNullOrEmpty(metabasePath))
			{
				throw new ArgumentException("Metabase Path missing");
			}
			if (string.IsNullOrEmpty(moduleName))
			{
				throw new ArgumentException("Module Name missing");
			}
			DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(metabasePath);
			using (ServerManager serverManager = ServerManager.OpenRemote(IisUtility.GetHostName(metabasePath)))
			{
				ConfigurationSection section;
				if (useApplicationHostConfigForModules)
				{
					Configuration configuration = serverManager.GetApplicationHostConfiguration();
					string text = isChildVDirApplication ? (IisUtility.GetWebSiteName(directoryEntry.Parent.Parent.Path) + string.Format("/{0}/{1}", directoryEntry.Parent.Name, directoryEntry.Name)) : (IisUtility.GetWebSiteName(directoryEntry.Parent.Path) + "/" + directoryEntry.Name);
					section = configuration.GetSection("system.webServer/modules", text);
				}
				else
				{
					Configuration configuration = isChildVDirApplication ? serverManager.Sites[IisUtility.GetWebSiteName(directoryEntry.Parent.Parent.Path)].Applications[string.Format("/{0}/{1}", directoryEntry.Parent.Name, directoryEntry.Name)].GetWebConfiguration() : serverManager.Sites[IisUtility.GetWebSiteName(directoryEntry.Parent.Path)].Applications["/" + directoryEntry.Name].GetWebConfiguration();
					section = configuration.GetSection("system.webServer/modules");
				}
				if (section == null)
				{
					throw new WebObjectNotFoundException(Strings.ExceptionWebObjectNotFound(metabasePath));
				}
				foreach (ConfigurationElement configurationElement in section.GetCollection())
				{
					string a = configurationElement.Attributes["name"].Value.ToString();
					if (string.Equals(a, moduleName, StringComparison.OrdinalIgnoreCase))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000AAA7C File Offset: 0x000A8C7C
		private static bool IsLocalMachine(string machineName)
		{
			string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(false);
			return string.Compare(machineName, "localhost", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(machineName, Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(machineName, localComputerFqdn, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000AAABC File Offset: 0x000A8CBC
		private static string GetDetailInfoForException(string serverName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("server name: ");
			stringBuilder.Append(serverName);
			stringBuilder.Append("\r\nlocal machine name: ");
			stringBuilder.Append(Environment.MachineName);
			try
			{
				stringBuilder.Append("\r\nlocal machine fqdn: ");
				stringBuilder.Append(NativeHelpers.GetLocalComputerFqdn(false));
			}
			catch (Exception)
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001F87 RID: 8071
		private const uint HKEY_LOCAL_MACHINE = 2147483650U;

		// Token: 0x04001F88 RID: 8072
		private const uint ValidIISMajorVersion = 6U;

		// Token: 0x04001F89 RID: 8073
		private const string WebServiceRoot = "/LM/W3SVC";

		// Token: 0x04001F8A RID: 8074
		private const string InetStp = "Software\\Microsoft\\InetStp";

		// Token: 0x04001F8B RID: 8075
		private const string MajorVersion = "MajorVersion";

		// Token: 0x04001F8C RID: 8076
		private const string ModulesSection = "system.webServer/modules";

		// Token: 0x04001F8D RID: 8077
		public const string DefaultWebSitePathNoRoot = "IIS://localhost/W3SVC/1";

		// Token: 0x04001F8E RID: 8078
		public const string DefaultWebSitePath = "IIS://localhost/W3SVC/1/Root";

		// Token: 0x04001F8F RID: 8079
		public const string LocalHost = "localhost";

		// Token: 0x04001F90 RID: 8080
		private static readonly char[] reservedUriCharacters = new char[]
		{
			';',
			'/',
			'?',
			':',
			'&',
			'=',
			'+',
			'$',
			',',
			'\\'
		};

		// Token: 0x04001F91 RID: 8081
		private static ReadOnlyCollection<MetabaseProperty> defaultVDirProperties;

		// Token: 0x020004BD RID: 1213
		public enum AppPoolAction
		{
			// Token: 0x04001F93 RID: 8083
			Start,
			// Token: 0x04001F94 RID: 8084
			Stop,
			// Token: 0x04001F95 RID: 8085
			Recycle
		}

		// Token: 0x020004BE RID: 1214
		public enum AbsolutePathType
		{
			// Token: 0x04001F97 RID: 8087
			Host,
			// Token: 0x04001F98 RID: 8088
			WebServicesRoot,
			// Token: 0x04001F99 RID: 8089
			WebSiteRoot,
			// Token: 0x04001F9A RID: 8090
			VirtualDirectory
		}
	}
}
