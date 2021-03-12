using System;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Sqm;
using Microsoft.Win32;

namespace Microsoft.Exchange.ManagementGUI
{
	// Token: 0x02000002 RID: 2
	internal class ManagementGuiSqmSession : SqmSession
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static ManagementGuiSqmSession Instance
		{
			get
			{
				if (ManagementGuiSqmSession.instance == null)
				{
					ManagementGuiSqmSession.instance = new ManagementGuiSqmSession();
				}
				return ManagementGuiSqmSession.instance;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E8 File Offset: 0x000002E8
		public ManagementGuiSqmSession() : base(SqmAppID.Admin, SqmSession.Scope.Process)
		{
			base.Open();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000023B4 File Offset: 0x000005B4
		protected override void OnCreate()
		{
			base.OnCreate();
			if (base.Enabled)
			{
				this.worker = new BackgroundWorker();
				this.worker.DoWork += delegate(object param0, DoWorkEventArgs param1)
				{
					try
					{
						this.osInfo = Environment.OSVersion.VersionString;
					}
					catch (InvalidOperationException)
					{
					}
					try
					{
						Version installedVersion = ConfigurationContext.Setup.InstalledVersion;
						if (installedVersion != null)
						{
							this.versionInfo = installedVersion.ToString();
						}
					}
					catch (TaskException)
					{
					}
					try
					{
						if (this.ldapHostName == null)
						{
							using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings\\MSExchange"))
							{
								if (registryKey != null)
								{
									object value = registryKey.GetValue("LdapPort");
									if (value != null)
									{
										this.ldapHostName = Environment.MachineName + ":" + value.ToString() + "/";
									}
								}
								else
								{
									this.ldapHostName = string.Empty;
								}
							}
						}
						if (this.ldapHostName != null)
						{
							string arg;
							using (DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("LDAP://{0}RootDse", this.ldapHostName)))
							{
								arg = (string)directoryEntry.Properties["configurationNamingContext"].Value;
							}
							this.configNCUri = string.Format("LDAP://{0}{1}", this.ldapHostName, arg);
						}
						if (string.IsNullOrEmpty(this.ldapHostName) && !string.IsNullOrEmpty(this.configNCUri))
						{
							using (DirectorySearcher directorySearcher = new DirectorySearcher(new DirectoryEntry(this.configNCUri)))
							{
								directorySearcher.Filter = "(objectClass=msExchExchangeServer)";
								directorySearcher.PropertiesToLoad.Clear();
								SearchResultCollection searchResultCollection = directorySearcher.FindAll();
								if (searchResultCollection != null)
								{
									this.orgSize = (uint)searchResultCollection.Count;
								}
							}
						}
					}
					catch (SecurityException)
					{
					}
					catch (UnauthorizedAccessException)
					{
					}
					catch (COMException)
					{
					}
					catch (ActiveDirectoryObjectNotFoundException)
					{
					}
					catch (ActiveDirectoryOperationException)
					{
					}
					catch (ActiveDirectoryServerDownException)
					{
					}
					this.staticConfiguationCollected = true;
				};
				this.worker.RunWorkerAsync();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002404 File Offset: 0x00000604
		protected override void OnClosing()
		{
			base.OnClosing();
			if (this.staticConfiguationCollected)
			{
				ManagementGuiSqmSession.Instance.SetDataPoint(SqmDataID.DATAID_OS, this.osInfo);
				ManagementGuiSqmSession.Instance.SetDataPoint(SqmDataID.DATAID_EMC_VERSION, this.versionInfo);
				ManagementGuiSqmSession.Instance.SetDataPoint(SqmDataID.DATAID_ORGANIZATION_SIZE, this.orgSize);
			}
			if (this.worker != null)
			{
				this.worker.Dispose();
				this.worker = null;
			}
		}

		// Token: 0x04000001 RID: 1
		private string ldapHostName;

		// Token: 0x04000002 RID: 2
		private string configNCUri;

		// Token: 0x04000003 RID: 3
		private BackgroundWorker worker;

		// Token: 0x04000004 RID: 4
		private static ManagementGuiSqmSession instance;

		// Token: 0x04000005 RID: 5
		private string osInfo = "Unknown";

		// Token: 0x04000006 RID: 6
		private string versionInfo = "Unknown";

		// Token: 0x04000007 RID: 7
		private uint orgSize = 2147483646U;

		// Token: 0x04000008 RID: 8
		private bool staticConfiguationCollected;

		// Token: 0x02000003 RID: 3
		public enum GuiElementCategory : uint
		{
			// Token: 0x0400000A RID: 10
			ResultPane = 1U,
			// Token: 0x0400000B RID: 11
			Page,
			// Token: 0x0400000C RID: 12
			Form
		}
	}
}
