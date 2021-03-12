using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class AntispamUpdates
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002EAB File Offset: 0x000010AB
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002EB3 File Offset: 0x000010B3
		public AntispamUpdateMode UpdateMode
		{
			get
			{
				return this.updateMode;
			}
			set
			{
				this.updateMode = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002EBC File Offset: 0x000010BC
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002EC4 File Offset: 0x000010C4
		public string LatestContentFilterVersion
		{
			get
			{
				return this.contentFilterVersion;
			}
			set
			{
				this.contentFilterVersion = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002ECD File Offset: 0x000010CD
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002ED5 File Offset: 0x000010D5
		public bool SpamSignatureUpdatesEnabled
		{
			get
			{
				return this.spamSignatureUpdatesEnabled;
			}
			set
			{
				this.spamSignatureUpdatesEnabled = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002EDE File Offset: 0x000010DE
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002EE6 File Offset: 0x000010E6
		public string LatestSpamSignatureVersion
		{
			get
			{
				return this.spamSignatureVersion;
			}
			set
			{
				this.spamSignatureVersion = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002EEF File Offset: 0x000010EF
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002EF7 File Offset: 0x000010F7
		public bool IPReputationUpdatesEnabled
		{
			get
			{
				return this.ipReputationUpdatesEnabled;
			}
			set
			{
				this.ipReputationUpdatesEnabled = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002F00 File Offset: 0x00001100
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002F08 File Offset: 0x00001108
		public string LatestIPReputationVersion
		{
			get
			{
				return this.ipReputationVersion;
			}
			set
			{
				this.ipReputationVersion = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002F11 File Offset: 0x00001111
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002F19 File Offset: 0x00001119
		public OptInStatus MicrosoftUpdate
		{
			get
			{
				return this.microsoftUpdate;
			}
			set
			{
				this.microsoftUpdate = value;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F24 File Offset: 0x00001124
		internal void LoadConfiguration(string computerName)
		{
			using (RegistryKey registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, computerName))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Hygiene"))
				{
					if (registryKey2 != null)
					{
						this.UpdateMode = (AntispamUpdateMode)registryKey2.GetValue("Mode", AntispamUpdateMode.Disabled);
						this.LatestContentFilterVersion = (registryKey2.GetValue("SmartDatVersion", string.Empty) as string);
						this.LatestIPReputationVersion = (registryKey2.GetValue("SmartDRPVersion", string.Empty) as string);
						this.LatestSpamSignatureVersion = (registryKey2.GetValue("SmartFNGVersion", string.Empty) as string);
						this.IPReputationUpdatesEnabled = ((int)registryKey2.GetValue("SmartDRPEnabled", 0) != 0);
						this.SpamSignatureUpdatesEnabled = ((int)registryKey2.GetValue("SmartFNGEnabled", 0) != 0);
						this.MicrosoftUpdate = (OptInStatus)registryKey2.GetValue("OptIn", OptInStatus.NotConfigured);
					}
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003050 File Offset: 0x00001250
		internal void SaveConfiguration(string computerName)
		{
			using (RegistryKey registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, computerName))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Hygiene", true) ?? registryKey.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Hygiene"))
				{
					registryKey2.SetValue("Mode", (int)this.UpdateMode, RegistryValueKind.DWord);
					registryKey2.SetValue("SmartDRPEnabled", this.IPReputationUpdatesEnabled ? 1 : 0, RegistryValueKind.DWord);
					registryKey2.SetValue("SmartFNGEnabled", this.SpamSignatureUpdatesEnabled ? 1 : 0, RegistryValueKind.DWord);
					registryKey2.SetValue("OptIn", (int)this.MicrosoftUpdate, RegistryValueKind.DWord);
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003124 File Offset: 0x00001324
		internal bool IsPremiumSKUInstalled()
		{
			this.LoadConfiguration(string.Empty);
			return this.UpdateMode == AntispamUpdateMode.Automatic && this.SpamSignatureUpdatesEnabled;
		}

		// Token: 0x0400001A RID: 26
		private const string DefaultKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Hygiene";

		// Token: 0x0400001B RID: 27
		private const string ModeValue = "Mode";

		// Token: 0x0400001C RID: 28
		private const string ContentFilterVersionValue = "SmartDatVersion";

		// Token: 0x0400001D RID: 29
		private const string IPReputationVersionValue = "SmartDRPVersion";

		// Token: 0x0400001E RID: 30
		private const string SpamSignatureVersionValue = "SmartFNGVersion";

		// Token: 0x0400001F RID: 31
		private const string IPReputationUpdatesEnabledValue = "SmartDRPEnabled";

		// Token: 0x04000020 RID: 32
		private const string SpamSignatureUpdatesEnabledValue = "SmartFNGEnabled";

		// Token: 0x04000021 RID: 33
		private const string OptInValue = "OptIn";

		// Token: 0x04000022 RID: 34
		private AntispamUpdateMode updateMode;

		// Token: 0x04000023 RID: 35
		private string contentFilterVersion;

		// Token: 0x04000024 RID: 36
		private bool spamSignatureUpdatesEnabled;

		// Token: 0x04000025 RID: 37
		private string spamSignatureVersion;

		// Token: 0x04000026 RID: 38
		private bool ipReputationUpdatesEnabled;

		// Token: 0x04000027 RID: 39
		private string ipReputationVersion;

		// Token: 0x04000028 RID: 40
		private OptInStatus microsoftUpdate;
	}
}
