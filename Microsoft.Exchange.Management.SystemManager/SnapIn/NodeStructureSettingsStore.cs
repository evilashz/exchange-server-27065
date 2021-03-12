using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000294 RID: 660
	[Serializable]
	public class NodeStructureSettingsStore
	{
		// Token: 0x06001BE3 RID: 7139 RVA: 0x000796E0 File Offset: 0x000778E0
		public NodeStructureSettingsStore(string localOnPremiseKey)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\MMC\\NodeTypes\\{A8DE63D9-A83F-4ee5-B723-E88F5DC21264}\\Extensions\\NameSpace", false))
			{
				if (registryKey != null)
				{
					string[] valueNames = registryKey.GetValueNames();
					foreach (string text in valueNames)
					{
						if (!text.Equals(localOnPremiseKey, StringComparison.InvariantCultureIgnoreCase))
						{
							using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\MMC\\SnapIns\\" + text, false))
							{
								if (registryKey2 != null)
								{
									string text2 = registryKey2.GetValue("ApplicationBase").ToString();
									string version = string.Empty;
									if (!ConfigurationContext.Setup.BinPath.Equals(text2, StringComparison.InvariantCultureIgnoreCase))
									{
										version = text2.Substring(ConfigurationContext.Setup.BinPath.Length + 1);
									}
									else
									{
										version = NodeStructureSettingsStore.DominantVersion;
									}
									this.slots.Add(new Slot
									{
										Key = text,
										Version = version
									});
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00079824 File Offset: 0x00077A24
		public string[] SlotsKey
		{
			get
			{
				return (from c in this.slots
				select c.Key).ToArray<string>();
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00079870 File Offset: 0x00077A70
		public string GetVersion(string slotKey)
		{
			return (from c in this.slots
			where c.Key == slotKey
			select c).First<Slot>().Version;
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x000798AB File Offset: 0x00077AAB
		// (set) Token: 0x06001BE7 RID: 7143 RVA: 0x000798B3 File Offset: 0x00077AB3
		public Fqdn LocalOnPremiseRemotePSServer { get; set; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x000798BC File Offset: 0x00077ABC
		public IList<Uri> RecentServerUris
		{
			get
			{
				return this.recentServerUris;
			}
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000798E4 File Offset: 0x00077AE4
		public void Load()
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\AdminTools", false))
				{
					if (registryKey != null)
					{
						this.serializedSettings = (registryKey.GetValue(NodeStructureSettingsStore.NodeStructureSettings) as byte[]);
						if (this.serializedSettings != null)
						{
							NodeStructureSettingsStore nodeStructureSettingsStore = WinformsHelper.DeSerialize(this.serializedSettings) as NodeStructureSettingsStore;
							if (nodeStructureSettingsStore.settings != null)
							{
								if ((from c in nodeStructureSettingsStore.settings
								where c.State == NodeStructureSettingState.Used
								select c).Count<NodeStructureSetting>() <= (from c in this.slots
								where c.Version == NodeStructureSettingsStore.DominantVersion
								select c).Count<Slot>())
								{
									using (IEnumerator<NodeStructureSetting> enumerator = nodeStructureSettingsStore.settings.GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											NodeStructureSetting nodeStructureSetting = enumerator.Current;
											if (nodeStructureSetting.State == NodeStructureSettingState.Used)
											{
												this.organizationSettings.Add(new OrganizationSetting
												{
													CredentialKey = nodeStructureSetting.CredentialKey,
													DisplayName = nodeStructureSetting.DisplayName,
													LogonWithDefaultCredential = nodeStructureSetting.LogonWithDefaultCredential,
													Type = nodeStructureSetting.Type,
													Uri = nodeStructureSetting.Uri,
													SupportedVersionList = new SupportedVersionList(NodeStructureSettingsStore.DominantVersion)
												});
											}
										}
										goto IL_172;
									}
								}
							}
							this.organizationSettings = (nodeStructureSettingsStore.organizationSettings ?? new List<OrganizationSetting>());
							this.recentServerUris = (nodeStructureSettingsStore.recentServerUris ?? new List<Uri>());
							IL_172:
							this.LocalOnPremiseRemotePSServer = nodeStructureSettingsStore.LocalOnPremiseRemotePSServer;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00079ACC File Offset: 0x00077CCC
		public void Save()
		{
			try
			{
				byte[] array = WinformsHelper.Serialize(this);
				if (this.serializedSettings == null || !WinformsHelper.ByteArrayEquals(array, this.serializedSettings))
				{
					using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\AdminTools"))
					{
						registryKey.SetValue(NodeStructureSettingsStore.NodeStructureSettings, array, RegistryValueKind.Binary);
					}
				}
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x17000686 RID: 1670
		public OrganizationSetting this[int pos]
		{
			get
			{
				return this.organizationSettings[pos];
			}
		}

		// Token: 0x17000687 RID: 1671
		public OrganizationSetting this[string key]
		{
			get
			{
				return (from c in this.organizationSettings
				where c.Key == key
				select c).First<OrganizationSetting>();
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x00079BB2 File Offset: 0x00077DB2
		public int Count
		{
			get
			{
				return this.organizationSettings.Count;
			}
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00079BC0 File Offset: 0x00077DC0
		public bool IsDuplicatedName(string organizationName)
		{
			foreach (OrganizationSetting organizationSetting in this.organizationSettings)
			{
				if (string.Equals(organizationSetting.DisplayName, organizationName, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00079C30 File Offset: 0x00077E30
		public IList<OrganizationSetting> GetInitiatedOrganizationInReverseOrder(ref string extendVersion)
		{
			IList<OrganizationSetting> list = this.organizationSettings.Reverse<OrganizationSetting>().ToList<OrganizationSetting>();
			IList<OrganizationSetting> list2 = new List<OrganizationSetting>();
			extendVersion = string.Empty;
			foreach (OrganizationSetting organizationSetting in list)
			{
				if (this.IsRegisteredSlot(organizationSetting.Key) && organizationSetting.SupportedVersionList == null)
				{
					list2.Add(organizationSetting);
				}
				else if (organizationSetting.SupportedVersionList != null && organizationSetting.SupportedVersionList.Count > 0)
				{
					IList<Slot> compatibleSlots = this.GetCompatibleSlots(organizationSetting.SupportedVersionList);
					if (compatibleSlots.Count<Slot>() == 0)
					{
						compatibleSlots = this.slots;
					}
					IEnumerable<Slot> source = from c in compatibleSlots
					where !this.IsInUse(c.Key)
					select c;
					if (source.Count<Slot>() > 0)
					{
						organizationSetting.Key = source.First<Slot>().Key;
						list2.Add(organizationSetting);
					}
					else if (compatibleSlots.Count<Slot>() > 0)
					{
						extendVersion = compatibleSlots.First<Slot>().Version;
					}
					else
					{
						extendVersion = organizationSetting.SupportedVersionList[0].ToString();
					}
				}
			}
			return list2;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00079D8C File Offset: 0x00077F8C
		public List<Slot> GetCompatibleSlots(SupportedVersionList supportedVersionList)
		{
			return (from c in this.slots
			where supportedVersionList.IsSupported(c.Version)
			select c).ToList<Slot>();
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00079DC2 File Offset: 0x00077FC2
		public IList<OrganizationSetting> GetOrganizationSetting()
		{
			return this.organizationSettings.ToList<OrganizationSetting>();
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00079DDA File Offset: 0x00077FDA
		public bool HasAvailableSlots
		{
			get
			{
				return (from s in this.slots
				where !s.Removed
				select s).Count<Slot>() > this.organizationSettings.Count<OrganizationSetting>();
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00079E3C File Offset: 0x0007803C
		public bool HasAvailableDominantSlots
		{
			get
			{
				SupportedVersionList dominantVersion = new SupportedVersionList(NodeStructureSettingsStore.DominantVersion);
				IEnumerable<Slot> enumerable = from s in this.slots
				where !s.Removed && dominantVersion.IsSupported(s.Version)
				select s;
				foreach (Slot slot in enumerable)
				{
					bool flag = true;
					foreach (OrganizationSetting organizationSetting in this.organizationSettings)
					{
						if (string.Equals(slot.Key, organizationSetting.Key, StringComparison.InvariantCultureIgnoreCase))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00079F30 File Offset: 0x00078130
		public void RemoveOrganization(string key)
		{
			this.organizationSettings.Remove(this[key]);
			Slot slot = (from c in this.slots
			where c.Key == key
			select c).First<Slot>();
			slot.Removed = true;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00079FB0 File Offset: 0x000781B0
		public bool IsInUse(string key)
		{
			return (from c in this.organizationSettings
			where c.Key == key && null == c.SupportedVersionList
			select c).Count<OrganizationSetting>() > 0;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0007A000 File Offset: 0x00078200
		private bool IsRegisteredSlot(string key)
		{
			return (from c in this.SlotsKey
			where string.Equals(c, key, StringComparison.InvariantCultureIgnoreCase)
			select c).Count<string>() > 0;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0007A03C File Offset: 0x0007823C
		public string Add(string displayName, Uri uri, bool logonWithDefaultCredential, OrganizationType type, SupportedVersionList supportedVersionList)
		{
			if (!this.HasAvailableSlots)
			{
				throw new IndexOutOfRangeException("No more slots are available.");
			}
			OrganizationSetting organizationSetting = new OrganizationSetting();
			organizationSetting.DisplayName = displayName;
			organizationSetting.Uri = uri;
			organizationSetting.LogonWithDefaultCredential = logonWithDefaultCredential;
			organizationSetting.Type = type;
			organizationSetting.Key = this.AllocateSlot(supportedVersionList);
			this.organizationSettings.Add(organizationSetting);
			if (type != OrganizationType.Cloud && !this.RecentServerUris.Contains(uri))
			{
				this.RecentServerUris.Add(uri);
			}
			return organizationSetting.Key;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0007A114 File Offset: 0x00078314
		private string AllocateSlot(SupportedVersionList supportedVersionList)
		{
			IEnumerable<Slot> source = from s in this.slots
			where supportedVersionList.IsSupported(s.Version) && !s.Removed && !this.IsInUse(s.Key)
			select s;
			if (source.Count<Slot>() > 0)
			{
				return source.First<Slot>().Key;
			}
			IEnumerable<Slot> source2 = from s in this.slots
			where !s.Removed && !this.IsInUse(s.Key)
			select s;
			if (source2.Count<Slot>() > 0)
			{
				return source2.First<Slot>().Key;
			}
			return null;
		}

		// Token: 0x04000A53 RID: 2643
		private const string userKeyRoot = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\AdminTools";

		// Token: 0x04000A54 RID: 2644
		private const string availableSlotsRoot = "SOFTWARE\\Microsoft\\MMC\\NodeTypes\\{A8DE63D9-A83F-4ee5-B723-E88F5DC21264}\\Extensions\\NameSpace";

		// Token: 0x04000A55 RID: 2645
		private const string slotsRoot = "SOFTWARE\\Microsoft\\MMC\\SnapIns";

		// Token: 0x04000A56 RID: 2646
		public static string DominantVersion = ConfigurationContext.Setup.GetExecutingVersion().ToString();

		// Token: 0x04000A57 RID: 2647
		internal static string NodeStructureSettings = "NodeStructureSettings";

		// Token: 0x04000A58 RID: 2648
		[NonSerialized]
		private byte[] serializedSettings;

		// Token: 0x04000A59 RID: 2649
		private IList<Slot> slots = new List<Slot>();

		// Token: 0x04000A5A RID: 2650
		private IList<OrganizationSetting> organizationSettings = new List<OrganizationSetting>();

		// Token: 0x04000A5B RID: 2651
		private IList<Uri> recentServerUris = new List<Uri>();

		// Token: 0x04000A5C RID: 2652
		private IList<NodeStructureSetting> settings;
	}
}
