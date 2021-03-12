using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E0 RID: 1760
	[Serializable]
	public class AlternateServiceAccountConfiguration
	{
		// Token: 0x06005147 RID: 20807 RVA: 0x0012CBB4 File Offset: 0x0012ADB4
		private AlternateServiceAccountConfiguration(string machineName)
		{
			this.machineName = machineName;
		}

		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x0012CBCE File Offset: 0x0012ADCE
		public ReadOnlyCollection<AlternateServiceAccountCredential> EffectiveCredentials
		{
			get
			{
				return this.AllCredentials;
			}
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x0012CBD8 File Offset: 0x0012ADD8
		public override string ToString()
		{
			IList<AlternateServiceAccountCredential> effectiveCredentials = this.EffectiveCredentials;
			return DirectoryStrings.AlternateServiceAccountConfigurationDisplayFormat((effectiveCredentials.Count > 0) ? effectiveCredentials[0].ToString() : DirectoryStrings.AlternateServiceAccountCredentialNotSet, (effectiveCredentials.Count > 1) ? effectiveCredentials[1].ToString() : DirectoryStrings.AlternateServiceAccountCredentialNotSet, (effectiveCredentials.Count > 2) ? DirectoryStrings.AlternateServiceAccountConfigurationDisplayFormatMoreDataAvailable : string.Empty);
		}

		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x0012CC52 File Offset: 0x0012AE52
		// (set) Token: 0x0600514B RID: 20811 RVA: 0x0012CC59 File Offset: 0x0012AE59
		internal static bool TestOnlyUseAlternateKeyAndDisableAccountCheck { private get; set; }

		// Token: 0x0600514C RID: 20812 RVA: 0x0012CC7C File Offset: 0x0012AE7C
		internal static RegistryWatcher CreateRegistryWatcher()
		{
			RegistryWatcher watcher = null;
			new AlternateServiceAccountConfiguration(null).DoRegistryOperation(false, delegate(RegistryKey key)
			{
				watcher = new RegistryWatcher(AlternateServiceAccountConfiguration.RootRegistryKeyName, true);
			}, new Func<string, LocalizedString>(DirectoryStrings.FailedToReadAlternateServiceAccountConfigFromRegistry));
			return watcher;
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0012CCC0 File Offset: 0x0012AEC0
		internal static void EnsureCanDoCryptoOperations()
		{
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				if (!current.IsSystem && !AlternateServiceAccountConfiguration.TestOnlyUseAlternateKeyAndDisableAccountCheck)
				{
					throw new InvalidOperationException("Only processes running under LocalSystem account can manage Alternate Service Account settings for a computer.");
				}
			}
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x0012CD0C File Offset: 0x0012AF0C
		internal static AlternateServiceAccountConfiguration LoadFromRegistry(string machineName)
		{
			return AlternateServiceAccountConfiguration.LoadFromRegistry(machineName, false);
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x0012CD15 File Offset: 0x0012AF15
		internal static AlternateServiceAccountConfiguration LoadWithPasswordsFromRegistry()
		{
			return AlternateServiceAccountConfiguration.LoadFromRegistry(null, true);
		}

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x06005150 RID: 20816 RVA: 0x0012CD1E File Offset: 0x0012AF1E
		internal ReadOnlyCollection<AlternateServiceAccountCredential> AllCredentials
		{
			get
			{
				return new ReadOnlyCollection<AlternateServiceAccountCredential>(this.credentials);
			}
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x0012CD2C File Offset: 0x0012AF2C
		internal void ApplyPasswords(SecureString[] passwords)
		{
			if (this.credentials.Count != passwords.Length)
			{
				throw new ArgumentException("Credential and password counts don't match", "passwords");
			}
			for (int i = 0; i < this.credentials.Count; i++)
			{
				this.credentials[i].ApplyPassword(passwords[i]);
			}
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x0012CD9C File Offset: 0x0012AF9C
		internal AlternateServiceAccountCredential AddCredential(PSCredential credential)
		{
			AlternateServiceAccountCredential newCredential = AlternateServiceAccountCredential.Create(TimeSpan.FromMilliseconds((double)(10 * this.credentials.Count)), credential);
			this.DoRegistryOperation(false, delegate(RegistryKey key)
			{
				newCredential.SaveToRegistry(key);
			}, new Func<string, LocalizedString>(DirectoryStrings.FailedToWriteAlternateServiceAccountConfigToRegistry));
			this.credentials.Insert(0, newCredential);
			return newCredential;
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x0012CE44 File Offset: 0x0012B044
		internal bool RemoveAllCredentials()
		{
			bool result = this.AllCredentials.Count > 0;
			this.DoRegistryOperation(true, delegate(RegistryKey unused)
			{
				using (RegistryKey registryKey = this.OpenHive())
				{
					registryKey.DeleteSubKeyTree(AlternateServiceAccountConfiguration.RootRegistryKeyName);
				}
			}, new Func<string, LocalizedString>(DirectoryStrings.FailedToWriteAlternateServiceAccountConfigToRegistry));
			foreach (AlternateServiceAccountCredential alternateServiceAccountCredential in this.credentials)
			{
				alternateServiceAccountCredential.Dispose();
			}
			this.credentials.Clear();
			return result;
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x0012CEE8 File Offset: 0x0012B0E8
		internal void RemoveCredential(AlternateServiceAccountCredential credential)
		{
			this.DoRegistryOperation(false, delegate(RegistryKey key)
			{
				credential.Remove(key);
			}, new Func<string, LocalizedString>(DirectoryStrings.FailedToWriteAlternateServiceAccountConfigToRegistry));
			this.credentials.Remove(credential);
			credential.Dispose();
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06005155 RID: 20821 RVA: 0x0012CF3E File Offset: 0x0012B13E
		private static string RootRegistryKeyName
		{
			get
			{
				if (!AlternateServiceAccountConfiguration.TestOnlyUseAlternateKeyAndDisableAccountCheck)
				{
					return "SYSTEM\\CurrentControlSet\\Services\\MSExchangeServiceHost\\ServiceAccounts";
				}
				return "SYSTEM\\CurrentControlSet\\Services\\MSExchangeServiceHost\\TestOnly_ServiceAccounts";
			}
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x0012CF78 File Offset: 0x0012B178
		private static AlternateServiceAccountConfiguration LoadFromRegistry(string machineName, bool decryptPasswords)
		{
			AlternateServiceAccountConfiguration result = new AlternateServiceAccountConfiguration(machineName);
			result.DoRegistryOperation(true, delegate(RegistryKey rootKey)
			{
				result.credentials.AddRange(AlternateServiceAccountCredential.LoadFromRegistry(rootKey, decryptPasswords));
			}, new Func<string, LocalizedString>(DirectoryStrings.FailedToReadAlternateServiceAccountConfigFromRegistry));
			result.credentials.Sort();
			return result;
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0012CFD8 File Offset: 0x0012B1D8
		private void DoRegistryOperation(bool isReadOnly, Action<RegistryKey> action, Func<string, LocalizedString> errorMessage)
		{
			string rootRegistryKeyName = AlternateServiceAccountConfiguration.RootRegistryKeyName;
			try
			{
				using (RegistryKey registryKey = this.OpenHive())
				{
					using (RegistryKey registryKey2 = isReadOnly ? registryKey.OpenSubKey(rootRegistryKeyName) : registryKey.CreateSubKey(rootRegistryKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree))
					{
						if (registryKey2 != null)
						{
							action(registryKey2);
						}
						else if (!isReadOnly)
						{
							throw new DataSourceTransientException(errorMessage(rootRegistryKeyName));
						}
					}
				}
			}
			catch (IOException innerException)
			{
				throw new DataSourceTransientException(errorMessage(rootRegistryKeyName), innerException);
			}
			catch (SecurityException innerException2)
			{
				throw new DataSourceOperationException(errorMessage(rootRegistryKeyName), innerException2);
			}
			catch (UnauthorizedAccessException innerException3)
			{
				throw new DataSourceOperationException(errorMessage(rootRegistryKeyName), innerException3);
			}
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x0012D0B0 File Offset: 0x0012B2B0
		private RegistryKey OpenHive()
		{
			if (this.machineName == null)
			{
				return Registry.LocalMachine;
			}
			return RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, this.machineName);
		}

		// Token: 0x0400371E RID: 14110
		private const string ServiceAccountConfigurationRegkey = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeServiceHost\\ServiceAccounts";

		// Token: 0x0400371F RID: 14111
		private const string TestOnlyServiceAccountConfigurationRegkey = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeServiceHost\\TestOnly_ServiceAccounts";

		// Token: 0x04003720 RID: 14112
		private readonly List<AlternateServiceAccountCredential> credentials = new List<AlternateServiceAccountCredential>();

		// Token: 0x04003721 RID: 14113
		[NonSerialized]
		private readonly string machineName;
	}
}
