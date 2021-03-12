using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO;
using System.Security;
using Microsoft.Exchange.Security.Dkm;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x0200000F RID: 15
	public static class ManageEdsConnectionStrings
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000048AC File Offset: 0x00002AAC
		static ManageEdsConnectionStrings()
		{
			ManageEdsConnectionStrings.Initialize();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000048C0 File Offset: 0x00002AC0
		public static void Initialize()
		{
			object obj = null;
			if (CommonUtils.TryGetRegistryValue(ManageEdsConnectionStrings.DiagnosticsRegistryKey, "SimpleEncrypt", null, out obj) && !(obj.ToString() == string.Empty) && !(obj.ToString() == "0"))
			{
				ManageEdsConnectionStrings.encrypt = new ManageEdsConnectionStrings.SimpleEncrypt();
			}
			else if (CommonUtils.TryGetRegistryValue(ManageEdsConnectionStrings.DiagnosticsRegistryKey, "AesEncryptFile", null, out obj) && obj.ToString() != string.Empty)
			{
				ManageEdsConnectionStrings.encrypt = new ManageEdsConnectionStrings.AesEncrypt(obj.ToString());
			}
			else
			{
				ManageEdsConnectionStrings.encrypt = new ManageEdsConnectionStrings.DkmEncrypt();
			}
			if (CommonUtils.TryGetRegistryValue(ManageEdsConnectionStrings.DiagnosticsRegistryKey, "SimpleConnectionStrings", null, out obj) && !(obj.ToString() == string.Empty) && !(obj.ToString() == "0"))
			{
				ManageEdsConnectionStrings.connectionStrings = new ManageEdsConnectionStrings.SimpleConnectionStrings();
				return;
			}
			if (CommonUtils.TryGetRegistryValue(ManageEdsConnectionStrings.DiagnosticsRegistryKey, "ConnectionStringsFile", null, out obj) && obj.ToString() != string.Empty)
			{
				ManageEdsConnectionStrings.connectionStrings = new ManageEdsConnectionStrings.FileConnectionStrings(obj.ToString());
				return;
			}
			ManageEdsConnectionStrings.connectionStrings = new ManageEdsConnectionStrings.AdConnectionStrings();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000049D9 File Offset: 0x00002BD9
		public static void AddEdsConnectionString(string edsSqlSchemaVersion, string connectionString)
		{
			if (string.IsNullOrEmpty(edsSqlSchemaVersion))
			{
				throw new ArgumentNullException("edsSqlSchemaVersion");
			}
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentNullException("connectionString");
			}
			ManageEdsConnectionStrings.connectionStrings.Add(edsSqlSchemaVersion, connectionString);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004A0D File Offset: 0x00002C0D
		public static void RemoveEdsConnectionString(string guid)
		{
			if (string.IsNullOrEmpty(guid))
			{
				throw new ArgumentNullException("guid");
			}
			ManageEdsConnectionStrings.connectionStrings.Remove(guid);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004A2D File Offset: 0x00002C2D
		public static List<string> GetEdsConnectionStrings()
		{
			return ManageEdsConnectionStrings.connectionStrings.Get();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004A39 File Offset: 0x00002C39
		internal static bool DkmEncryptString(string password, out string encryptedPassword)
		{
			return ManageEdsConnectionStrings.encrypt.EncryptString(password, out encryptedPassword);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004A47 File Offset: 0x00002C47
		internal static bool DkmDecryptString(string encryptedPassword, out SecureString decryptedString)
		{
			return ManageEdsConnectionStrings.encrypt.DecryptString(encryptedPassword, out decryptedString);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004A58 File Offset: 0x00002C58
		private static string GenerateConnectionString(string edsSqlSchemaVersion, string connectionString)
		{
			string value = Guid.NewGuid().ToString();
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
			dbConnectionStringBuilder.ConnectionString = connectionString;
			object obj;
			if (dbConnectionStringBuilder.TryGetValue("Site", out obj))
			{
				dbConnectionStringBuilder.Remove("Site");
			}
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(dbConnectionStringBuilder.ConnectionString);
			object obj2;
			if (!sqlConnectionStringBuilder.TryGetValue("Password", out obj2))
			{
				throw new ArgumentException("The connection string must contain a password.");
			}
			string password;
			if (!ManageEdsConnectionStrings.DkmEncryptString(obj2.ToString(), out password))
			{
				throw new ApplicationException("Unable to encrypt password");
			}
			sqlConnectionStringBuilder.Password = password;
			object obj3;
			if (!sqlConnectionStringBuilder.TryGetValue("User ID", out obj3) || string.IsNullOrEmpty(obj3.ToString()))
			{
				throw new ArgumentException("The connection string must contains a User ID");
			}
			if (!sqlConnectionStringBuilder.TryGetValue("Initial Catalog", out obj3) || string.IsNullOrEmpty(obj3.ToString()))
			{
				throw new ArgumentException("The connection string must contains an Initial Catalog");
			}
			if (!sqlConnectionStringBuilder.TryGetValue("Data Source", out obj3) || string.IsNullOrEmpty(obj3.ToString()))
			{
				throw new ArgumentException("The connection string must contains a Data Source");
			}
			DbConnectionStringBuilder dbConnectionStringBuilder2 = new DbConnectionStringBuilder();
			dbConnectionStringBuilder2.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
			if (!string.IsNullOrEmpty((string)obj))
			{
				dbConnectionStringBuilder2.Add("Site", obj);
			}
			dbConnectionStringBuilder2.Add("Guid", value);
			dbConnectionStringBuilder2.Add("EdsSqlSchemaVersion", edsSqlSchemaVersion);
			return dbConnectionStringBuilder2.ToString();
		}

		// Token: 0x040002D5 RID: 725
		private const string MsiInstallPathRegistryValue = "MsiInstallPath";

		// Token: 0x040002D6 RID: 726
		internal static readonly string DiagnosticsRegistryKey = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\ExchangeServer\\v15\\Diagnostics";

		// Token: 0x040002D7 RID: 727
		private static ManageEdsConnectionStrings.IConnectionStrings connectionStrings;

		// Token: 0x040002D8 RID: 728
		private static ManageEdsConnectionStrings.IEncrypt encrypt;

		// Token: 0x02000010 RID: 16
		private interface IEncrypt
		{
			// Token: 0x06000034 RID: 52
			bool EncryptString(string password, out string encryptedPassword);

			// Token: 0x06000035 RID: 53
			bool DecryptString(string encryptedPassword, out SecureString decryptedString);
		}

		// Token: 0x02000011 RID: 17
		private interface IConnectionStrings
		{
			// Token: 0x06000036 RID: 54
			void Add(string edsSqlSchemaVersion, string connectionString);

			// Token: 0x06000037 RID: 55
			void Remove(string guid);

			// Token: 0x06000038 RID: 56
			List<string> Get();
		}

		// Token: 0x02000012 RID: 18
		private class SimpleEncrypt : ManageEdsConnectionStrings.IEncrypt
		{
			// Token: 0x06000039 RID: 57 RVA: 0x00004BB4 File Offset: 0x00002DB4
			public bool EncryptString(string password, out string encryptedPassword)
			{
				encryptedPassword = password;
				return true;
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00004BBC File Offset: 0x00002DBC
			public bool DecryptString(string encryptedPassword, out SecureString decryptedString)
			{
				SecureString secureString = new SecureString();
				foreach (char c in encryptedPassword)
				{
					secureString.AppendChar(c);
				}
				decryptedString = secureString;
				return true;
			}
		}

		// Token: 0x02000013 RID: 19
		private class AesEncrypt : ManageEdsConnectionStrings.IEncrypt
		{
			// Token: 0x0600003C RID: 60 RVA: 0x00004BFD File Offset: 0x00002DFD
			public AesEncrypt(string file)
			{
				this.file = file;
			}

			// Token: 0x0600003D RID: 61 RVA: 0x00004C0C File Offset: 0x00002E0C
			public bool EncryptString(string password, out string encryptedPassword)
			{
				DiagnosticsPasswordEncryption encryption = this.GetEncryption();
				if (encryption == null)
				{
					encryptedPassword = null;
					return false;
				}
				encryptedPassword = encryption.EncryptString(password);
				return true;
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00004C34 File Offset: 0x00002E34
			public bool DecryptString(string encryptedPassword, out SecureString decryptedString)
			{
				DiagnosticsPasswordEncryption encryption = this.GetEncryption();
				if (encryption == null)
				{
					decryptedString = null;
					return false;
				}
				string text = encryption.DecryptString(encryptedPassword);
				SecureString secureString = new SecureString();
				foreach (char c in text)
				{
					secureString.AppendChar(c);
				}
				decryptedString = secureString;
				return true;
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00004C8C File Offset: 0x00002E8C
			private DiagnosticsPasswordEncryption GetEncryption()
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(this.file, false))
					{
						string keyString = streamReader.ReadLine();
						string initialVectorString = streamReader.ReadLine();
						return new DiagnosticsPasswordEncryption(keyString, initialVectorString);
					}
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("Unable to get encryption key/vector from file {0}, error: {1}", new object[]
					{
						this.file,
						ex
					});
				}
				return null;
			}

			// Token: 0x040002D9 RID: 729
			private readonly string file;
		}

		// Token: 0x02000014 RID: 20
		private class SimpleConnectionStrings : ManageEdsConnectionStrings.IConnectionStrings
		{
			// Token: 0x06000040 RID: 64 RVA: 0x00004D10 File Offset: 0x00002F10
			public void Add(string edsSqlSchemaVersion, string connectionString)
			{
				List<string> edsEndpointObject = this.GetEdsEndpointObject();
				if (edsEndpointObject != null)
				{
					List<string> list = new List<string>(edsEndpointObject);
					string item = ManageEdsConnectionStrings.GenerateConnectionString(edsSqlSchemaVersion, connectionString);
					list.Add(item);
					this.SetEdsEndpointObject(list);
					return;
				}
				throw new ApplicationException("The EDS endpoint object does not exist.");
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00004D50 File Offset: 0x00002F50
			public void Remove(string guid)
			{
				List<string> edsEndpointObject = this.GetEdsEndpointObject();
				if (edsEndpointObject == null)
				{
					throw new ApplicationException("The EDS endpoint object does not exist.");
				}
				List<string> list = new List<string>(edsEndpointObject.Count);
				foreach (string text in edsEndpointObject)
				{
					if (!text.Contains(guid))
					{
						list.Add(text);
					}
				}
				if (list.Count != edsEndpointObject.Count)
				{
					this.SetEdsEndpointObject(list);
					return;
				}
				throw new ArgumentException("The guid specified does not exist.");
			}

			// Token: 0x06000042 RID: 66 RVA: 0x00004DE8 File Offset: 0x00002FE8
			public List<string> Get()
			{
				List<string> list = new List<string>();
				List<string> edsEndpointObject = this.GetEdsEndpointObject();
				if (edsEndpointObject != null)
				{
					list.AddRange(edsEndpointObject);
				}
				return list;
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00004E10 File Offset: 0x00003010
			public virtual void SetEdsEndpointObject(List<string> edsEndpoint)
			{
				try
				{
					Registry.SetValue(ManageEdsConnectionStrings.DiagnosticsRegistryKey, "SqlConnectionString", edsEndpoint.ToArray());
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("Unable to write {0}\\{1}, error {2}:", new object[]
					{
						ManageEdsConnectionStrings.DiagnosticsRegistryKey,
						"SqlConnectionString",
						ex
					});
				}
			}

			// Token: 0x06000044 RID: 68 RVA: 0x00004E70 File Offset: 0x00003070
			public virtual List<string> GetEdsEndpointObject()
			{
				string[] array = new string[0];
				try
				{
					array = (string[])Registry.GetValue(ManageEdsConnectionStrings.DiagnosticsRegistryKey, "SqlConnectionString", array);
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("Unable to read {0}\\{1}, error {2}:", new object[]
					{
						ManageEdsConnectionStrings.DiagnosticsRegistryKey,
						"SqlConnectionString",
						ex
					});
				}
				return new List<string>(array);
			}

			// Token: 0x040002DA RID: 730
			public const string SqlConnectionString = "SqlConnectionString";
		}

		// Token: 0x02000015 RID: 21
		private class FileConnectionStrings : ManageEdsConnectionStrings.SimpleConnectionStrings
		{
			// Token: 0x06000046 RID: 70 RVA: 0x00004EE4 File Offset: 0x000030E4
			public FileConnectionStrings(string file)
			{
				this.file = file;
			}

			// Token: 0x06000047 RID: 71 RVA: 0x00004EF4 File Offset: 0x000030F4
			public override void SetEdsEndpointObject(List<string> edsEndpoint)
			{
				using (StreamWriter streamWriter = new StreamWriter(this.file, false))
				{
					foreach (string value in edsEndpoint)
					{
						streamWriter.WriteLine(value);
					}
				}
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00004F68 File Offset: 0x00003168
			public override List<string> GetEdsEndpointObject()
			{
				List<string> list = new List<string>();
				if (File.Exists(this.file))
				{
					using (StreamReader streamReader = new StreamReader(this.file, false))
					{
						while (!streamReader.EndOfStream)
						{
							list.Add(streamReader.ReadLine());
						}
					}
				}
				return list;
			}

			// Token: 0x040002DB RID: 731
			private readonly string file;
		}

		// Token: 0x02000016 RID: 22
		private class DkmEncrypt : ManageEdsConnectionStrings.IEncrypt
		{
			// Token: 0x06000049 RID: 73 RVA: 0x00004FC8 File Offset: 0x000031C8
			public DkmEncrypt()
			{
				object obj = null;
				if (CommonUtils.TryGetRegistryValue(ManageEdsConnectionStrings.DiagnosticsRegistryKey, "MsiInstallPath", null, out obj))
				{
					this.edsPath = obj.ToString();
					return;
				}
				this.edsPath = null;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00005008 File Offset: 0x00003208
			public bool EncryptString(string password, out string encryptedPassword)
			{
				bool result = false;
				try
				{
					ExchangeGroupKey exchangeGroupKey = new ExchangeGroupKey(this.edsPath, "Microsoft Exchange Diagnostics DKM");
					encryptedPassword = exchangeGroupKey.ClearStringToEncryptedString(password);
					result = true;
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("DKM query failed to encrypt due to: {0}", new object[]
					{
						ex
					});
					encryptedPassword = null;
				}
				return result;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00005064 File Offset: 0x00003264
			public bool DecryptString(string encryptedPassword, out SecureString decryptedString)
			{
				bool result = false;
				try
				{
					ExchangeGroupKey exchangeGroupKey = new ExchangeGroupKey(this.edsPath, "Microsoft Exchange Diagnostics DKM");
					decryptedString = exchangeGroupKey.EncryptedStringToSecureString(encryptedPassword);
					result = true;
				}
				catch (Exception ex)
				{
					decryptedString = null;
					Logger.LogErrorMessage("DKM query failed to decrypt due to: {0}", new object[]
					{
						ex
					});
				}
				return result;
			}

			// Token: 0x040002DC RID: 732
			private readonly string edsPath;
		}

		// Token: 0x02000017 RID: 23
		private class AdConnectionStrings : ManageEdsConnectionStrings.IConnectionStrings
		{
			// Token: 0x0600004C RID: 76 RVA: 0x000050C0 File Offset: 0x000032C0
			public void Add(string edsSqlSchemaVersion, string connectionString)
			{
				SearchResult edsEndpointObject = ManageEdsConnectionStrings.AdConnectionStrings.GetEdsEndpointObject();
				if (edsEndpointObject != null)
				{
					string value = ManageEdsConnectionStrings.GenerateConnectionString(edsSqlSchemaVersion, connectionString);
					using (DirectoryEntry directoryEntry = new DirectoryEntry(edsEndpointObject.Path))
					{
						directoryEntry.Properties["serviceBindingInformation"].Add(value);
						directoryEntry.CommitChanges();
						return;
					}
				}
				throw new ApplicationException("The EDS endpoint object does not exist.");
			}

			// Token: 0x0600004D RID: 77 RVA: 0x00005130 File Offset: 0x00003330
			public void Remove(string guid)
			{
				SearchResult edsEndpointObject = ManageEdsConnectionStrings.AdConnectionStrings.GetEdsEndpointObject();
				if (edsEndpointObject != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = edsEndpointObject.Properties["serviceBindingInformation"];
					string text = null;
					foreach (object obj in resultPropertyValueCollection)
					{
						string text2 = (string)obj;
						if (text2.Contains(guid))
						{
							text = text2;
							break;
						}
					}
					if (text != null)
					{
						using (DirectoryEntry directoryEntry = new DirectoryEntry(edsEndpointObject.Path))
						{
							directoryEntry.Properties["serviceBindingInformation"].Remove(text);
							directoryEntry.CommitChanges();
							return;
						}
					}
					throw new ArgumentException("The guid specified does not exist.");
				}
				throw new ApplicationException("The EDS endpoint object does not exist.");
			}

			// Token: 0x0600004E RID: 78 RVA: 0x00005210 File Offset: 0x00003410
			public List<string> Get()
			{
				List<string> list = new List<string>();
				SearchResult edsEndpointObject = ManageEdsConnectionStrings.AdConnectionStrings.GetEdsEndpointObject();
				if (edsEndpointObject != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = edsEndpointObject.Properties["serviceBindingInformation"];
					foreach (object obj in resultPropertyValueCollection)
					{
						string item = (string)obj;
						list.Add(item);
					}
				}
				return list;
			}

			// Token: 0x0600004F RID: 79 RVA: 0x0000528C File Offset: 0x0000348C
			private static SearchResult GetEdsEndpointObject()
			{
				string[] propertiesToLoad = new string[]
				{
					"serviceBindingInformation"
				};
				string path;
				try
				{
					using (DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://RootDSE"))
					{
						string str = directoryEntry.Properties["configurationNamingContext"].Value.ToString();
						path = "LDAP://CN=Microsoft Exchange,CN=Services," + str;
					}
				}
				catch (Exception ex)
				{
					Logger.LogErrorMessage("Unable to query the configurationNamingContext, the error is:{0}", new object[]
					{
						ex
					});
					return null;
				}
				SearchResult result;
				try
				{
					using (DirectoryEntry directoryEntry2 = new DirectoryEntry(path))
					{
						using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry2, "(&(name=EdsSqlEndpoints)(objectClass=serviceConnectionPoint))", propertiesToLoad))
						{
							result = directorySearcher.FindOne();
						}
					}
				}
				catch (Exception ex2)
				{
					Logger.LogErrorMessage("Failed to find EdsSqlEndpoints object, the error is:{0}", new object[]
					{
						ex2
					});
					result = null;
				}
				return result;
			}

			// Token: 0x040002DD RID: 733
			private const string Filter = "(&(name=EdsSqlEndpoints)(objectClass=serviceConnectionPoint))";

			// Token: 0x040002DE RID: 734
			private const string ServiceBindingInformation = "serviceBindingInformation";
		}
	}
}
