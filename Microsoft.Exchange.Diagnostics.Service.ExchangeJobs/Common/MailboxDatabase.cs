using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using Microsoft.ExLogAnalyzer;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Diagnostics.Service.ExchangeJobs.Common
{
	// Token: 0x0200000A RID: 10
	internal sealed class MailboxDatabase : IEquatable<MailboxDatabase>
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00003E74 File Offset: 0x00002074
		public MailboxDatabase(string directoryPath)
		{
			if (string.IsNullOrEmpty(directoryPath))
			{
				throw new ArgumentNullException("directoryPath");
			}
			using (DirectoryEntry directoryEntry = new DirectoryEntry(directoryPath))
			{
				byte[] b = MailboxDatabase.ValidateDirectoryProperty<byte[]>(directoryEntry, "ObjectGuid");
				string text = MailboxDatabase.ValidateDirectoryProperty<string>(directoryEntry, "Name");
				string path = MailboxDatabase.ValidateDirectoryProperty<string>(directoryEntry, "msExchEDBFile");
				string text2 = MailboxDatabase.ValidateDirectoryProperty<string>(directoryEntry, "msExchESEParamLogFilePath");
				string text3 = MailboxDatabase.ValidateDirectoryProperty<string>(directoryEntry, "msExchESEParamBaseName");
				string text4 = MailboxDatabase.ValidateDirectoryProperty<string>(directoryEntry, "msExchDatabaseGroup");
				this.guid = new Guid(b);
				this.name = text;
				this.edbFolderPath = Path.GetDirectoryName(path);
				this.logFolderPath = text2;
				this.logFilePrefix = text3;
				this.databaseGroup = text4;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003F40 File Offset: 0x00002140
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003F48 File Offset: 0x00002148
		internal Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003F50 File Offset: 0x00002150
		internal string DatabaseGroup
		{
			get
			{
				return this.databaseGroup;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003F58 File Offset: 0x00002158
		internal string EdbFolderPath
		{
			get
			{
				return this.edbFolderPath;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003F60 File Offset: 0x00002160
		internal string LogFolderPath
		{
			get
			{
				return this.logFolderPath;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003F68 File Offset: 0x00002168
		internal string LogFilePrefix
		{
			get
			{
				return this.logFilePrefix;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003F70 File Offset: 0x00002170
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MailboxDatabase);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003F7E File Offset: 0x0000217E
		public bool Equals(MailboxDatabase other)
		{
			return other != null && this.name.Equals(other.name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003F97 File Offset: 0x00002197
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.name);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003FC4 File Offset: 0x000021C4
		public bool TryLoadStatistics(out Dictionary<string, float> statistics)
		{
			bool result = false;
			statistics = null;
			try
			{
				ulong num = 0UL;
				ulong num2 = 0UL;
				int[] array = new int[2];
				int[] array2 = array;
				long[] array3 = new long[2];
				long[] array4 = array3;
				long[] array5 = new long[2];
				long[] array6 = array5;
				using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=EDS", Environment.MachineName, null, null, null))
				{
					exRpcAdmin.GetDatabaseSize(this.guid, out num, out num2);
					PropValue[][] mailboxTable = exRpcAdmin.GetMailboxTable(this.guid, MailboxDatabase.MailboxTableProperties);
					foreach (PropValue[] source in mailboxTable)
					{
						Dictionary<PropTag, PropValue> dictionary = (from value in source
						where value.Value != null
						select value).ToDictionary((PropValue value) => value.PropTag);
						PropValue propValue;
						int num3 = dictionary.TryGetValue(PropTag.DateDiscoveredAbsentInDS, out propValue) ? 1 : 0;
						if (dictionary.TryGetValue(PropTag.MessageSizeExtended, out propValue))
						{
							array4[num3] += (long)propValue.Value;
						}
						if (dictionary.TryGetValue(PropTag.DeletedMessageSizeExtended, out propValue))
						{
							array6[num3] += (long)propValue.Value;
						}
						array2[num3]++;
					}
				}
				statistics = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase);
				statistics.Add("AvailableNewMailboxSpace", num2);
				statistics.Add("DisconnectedTotalItemSize", (float)array4[1]);
				statistics.Add("DisconnectedTotalDeletedItemSize", (float)array6[1]);
				statistics.Add("MailboxCount", (float)array2[0]);
				statistics.Add("DisconnectedMailboxCount", (float)array2[1]);
				statistics.Add("CatalogSize", this.GetCatalogSize());
				statistics.Add("LogSize", this.GetLogFileSize());
				long num4 = (long)(num - num2);
				long num5 = array4[0] + array6[0] + array4[1] + array6[1];
				statistics.Add("DatabasePhysicalUsedSize", (float)num4);
				statistics.Add("DatabaseLogicalSize", (float)num5);
				if (num4 > 0L)
				{
					statistics.Add("LogicalPhysicalSizeRatio", (float)((double)num5 / (double)num4));
				}
				else
				{
					statistics.Add("LogicalPhysicalSizeRatio", 0f);
				}
				result = true;
			}
			catch (MapiExceptionMdbOffline)
			{
			}
			catch (Exception ex)
			{
				Log.LogErrorMessage("An exception occurred while trying to gather database '{0}' statistics: '{1}'", new object[]
				{
					this.name,
					ex
				});
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000042AC File Offset: 0x000024AC
		internal static bool TryDiscoverLocalMailboxDatabases(out IEnumerable<MailboxDatabase> databases)
		{
			bool result = false;
			databases = null;
			try
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://RootDSE"))
				{
					using (DirectoryEntry directoryEntry2 = new DirectoryEntry("LDAP://CN=Microsoft Exchange,CN=Services," + directoryEntry.Properties["configurationNamingContext"].Value.ToString()))
					{
						using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry2))
						{
							directorySearcher.CacheResults = false;
							directorySearcher.Filter = string.Format("(&(ObjectClass=msExchExchangeServer)(cn={0}))", Environment.MachineName);
							directorySearcher.PropertiesToLoad.Add("msExchHostServerBL");
							using (SearchResultCollection searchResultCollection = directorySearcher.FindAll())
							{
								List<MailboxDatabase> list = new List<MailboxDatabase>();
								foreach (object obj in searchResultCollection)
								{
									SearchResult searchResult = (SearchResult)obj;
									ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["msExchHostServerBL"];
									using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											object obj2 = enumerator2.Current;
											string str = MailboxDatabase.NavigateUpDistinguishedName(obj2.ToString());
											MailboxDatabase item = new MailboxDatabase("LDAP://" + str);
											list.Add(item);
										}
										break;
									}
								}
								if (list.Any<MailboxDatabase>())
								{
									databases = list;
									result = true;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LogErrorMessage("An exception occurred while trying to query AD: '{0}'", new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000044F4 File Offset: 0x000026F4
		internal static string NavigateUpDistinguishedName(string distinguishedName)
		{
			if (string.IsNullOrEmpty(distinguishedName))
			{
				throw new ArgumentNullException("distinguishedName");
			}
			string result = string.Empty;
			for (int i = 0; i < distinguishedName.Length; i++)
			{
				if (distinguishedName[i] == ',' && distinguishedName[(i - 1 < 0) ? 0 : (i - 1)] != '\\')
				{
					result = distinguishedName.Substring(i + 1);
					break;
				}
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000455C File Offset: 0x0000275C
		internal float GetLogFileSize()
		{
			float num = 0f;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(this.logFolderPath);
				FileInfo[] files;
				if (!string.IsNullOrEmpty(this.logFilePrefix))
				{
					files = directoryInfo.GetFiles(this.logFilePrefix + "*");
				}
				else
				{
					files = directoryInfo.GetFiles();
				}
				foreach (FileInfo fileInfo in files)
				{
					num += (float)fileInfo.Length;
				}
			}
			catch (Exception ex)
			{
				Log.LogErrorMessage("Unable to calculate the log size. LogFolderPath: '{0}', LogFilePrefix: '{1}', Exception: '{2}'", new object[]
				{
					this.logFolderPath,
					this.logFilePrefix,
					ex
				});
			}
			return num;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004614 File Offset: 0x00002814
		internal float GetCatalogSize()
		{
			float num = 0f;
			try
			{
				string[] directories = Directory.GetDirectories(this.edbFolderPath, string.Format("{0}*.Single", this.guid));
				foreach (string path in directories)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(path);
					if (directoryInfo.Exists)
					{
						FileInfo[] files = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
						foreach (FileInfo fileInfo in files)
						{
							num += (float)fileInfo.Length;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LogErrorMessage("Unable to calculate the catalog size. EdbFolderPath: '{0}', Exception: '{1}'", new object[]
				{
					this.edbFolderPath,
					ex
				});
			}
			return num;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000046E8 File Offset: 0x000028E8
		private static TValue ValidateDirectoryProperty<TValue>(DirectoryEntry entry, string propertyName) where TValue : class
		{
			if (!entry.Properties.Contains(propertyName) && !propertyName.Equals("msExchDatabaseGroup"))
			{
				throw new InvalidOperationException(string.Format("The directory object with the path '{0}', does not contain the required '{1}' property.", entry.Path, propertyName));
			}
			if (!entry.Properties.Contains(propertyName))
			{
				return default(TValue);
			}
			TValue tvalue = entry.Properties[propertyName].Value as TValue;
			if (tvalue == null)
			{
				throw new InvalidOperationException(string.Format("The value of the '{0}' property cannot be null.", propertyName));
			}
			return tvalue;
		}

		// Token: 0x04000058 RID: 88
		public const string DatabasePhysicalUsedSizeProperty = "DatabasePhysicalUsedSize";

		// Token: 0x04000059 RID: 89
		public const string AvailableNewMailboxSpaceProperty = "AvailableNewMailboxSpace";

		// Token: 0x0400005A RID: 90
		public const string DatabaseLogicalSizeProperty = "DatabaseLogicalSize";

		// Token: 0x0400005B RID: 91
		public const string LogicalPhysicalSizeRatioProperty = "LogicalPhysicalSizeRatio";

		// Token: 0x0400005C RID: 92
		public const string DisconnectedTotalItemSizeProperty = "DisconnectedTotalItemSize";

		// Token: 0x0400005D RID: 93
		public const string DisconnectedTotalDeletedItemSizeProperty = "DisconnectedTotalDeletedItemSize";

		// Token: 0x0400005E RID: 94
		public const string MailboxCountProperty = "MailboxCount";

		// Token: 0x0400005F RID: 95
		public const string DisconnectedMailboxCountProperty = "DisconnectedMailboxCount";

		// Token: 0x04000060 RID: 96
		public const string CatalogSizeProperty = "CatalogSize";

		// Token: 0x04000061 RID: 97
		public const string LogSizeProperty = "LogSize";

		// Token: 0x04000062 RID: 98
		private const string DirectoryMailboxDatabaseGroupProperty = "msExchDatabaseGroup";

		// Token: 0x04000063 RID: 99
		private static readonly PropTag[] MailboxTableProperties = new PropTag[]
		{
			PropTag.MessageSizeExtended,
			PropTag.DeletedMessageSizeExtended,
			PropTag.DateDiscoveredAbsentInDS
		};

		// Token: 0x04000064 RID: 100
		private readonly string name;

		// Token: 0x04000065 RID: 101
		private readonly Guid guid;

		// Token: 0x04000066 RID: 102
		private readonly string databaseGroup;

		// Token: 0x04000067 RID: 103
		private readonly string edbFolderPath;

		// Token: 0x04000068 RID: 104
		private readonly string logFolderPath;

		// Token: 0x04000069 RID: 105
		private readonly string logFilePrefix;
	}
}
