using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200008F RID: 143
	public class ClusdbSnapshotMaker
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x00013BF5 File Offset: 0x00011DF5
		public ClusdbSnapshotMaker(string[] filterRootKeys = null, string rootKeyName = null, bool isForceIncludeRootProperties = false) : this(null, filterRootKeys, rootKeyName, isForceIncludeRootProperties)
		{
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013C01 File Offset: 0x00011E01
		internal ClusdbSnapshotMaker(AmClusterHandle clusterHandle = null, string[] filterRootKeys = null, string rootKeyName = null, bool isForceIncludeRootProperties = false)
		{
			this.clusterHandle = clusterHandle;
			this.rootKeyName = rootKeyName;
			this.filterRootKeys = filterRootKeys;
			this.isForceIncludeRootProperties = isForceIncludeRootProperties;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00013C26 File Offset: 0x00011E26
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00013C2E File Offset: 0x00011E2E
		public bool IsProduceDxStoreFormat { get; set; }

		// Token: 0x06000541 RID: 1345 RVA: 0x00013C50 File Offset: 0x00011E50
		public bool IsIncludeKey(string keyName)
		{
			return this.filterRootKeys == null || this.filterRootKeys.Length == 0 || this.filterRootKeys.Any((string filterKey) => Utils.IsEqual(filterKey, keyName, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00013C98 File Offset: 0x00011E98
		public XElement GetXElementSnapshot(string keyName = null)
		{
			bool flag = false;
			if (this.clusterHandle == null || this.clusterHandle.IsInvalid)
			{
				this.clusterHandle = ClusapiMethods.OpenCluster(null);
				flag = true;
			}
			XElement xelementSnapshotInternal;
			try
			{
				using (IDistributedStoreKey distributedStoreKey = ClusterDbKey.GetBaseKey(this.clusterHandle, DxStoreKeyAccessMode.Read))
				{
					this.baseKey = distributedStoreKey;
					xelementSnapshotInternal = this.GetXElementSnapshotInternal(keyName);
				}
			}
			finally
			{
				if (flag && this.clusterHandle != null && !this.clusterHandle.IsInvalid)
				{
					this.clusterHandle.Close();
					this.clusterHandle = null;
				}
			}
			return xelementSnapshotInternal;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00013D3C File Offset: 0x00011F3C
		private XElement GetXElementSnapshotInternal(string keyName)
		{
			bool flag = false;
			bool flag2 = true;
			string keyName2;
			string value;
			if (!string.IsNullOrEmpty(keyName))
			{
				keyName2 = Utils.CombinePathNullSafe(this.rootKeyName, keyName);
				value = keyName.Substring(keyName.LastIndexOf('\\') + 1);
			}
			else
			{
				keyName2 = this.rootKeyName;
				value = "\\";
				flag = true;
				if (this.filterRootKeys.Length > 1)
				{
					flag2 = false;
				}
			}
			XElement xelement = new XElement("Key", new XAttribute("Name", value));
			XElement xelement2 = xelement;
			if (this.IsProduceDxStoreFormat && flag)
			{
				xelement.Add(new XElement("Key", new XAttribute("Name", "Private")));
				XElement xelement3 = new XElement("Key", new XAttribute("Name", "Public"));
				xelement.Add(xelement3);
				xelement2 = xelement3;
			}
			IDistributedStoreKey distributedStoreKey = flag ? this.baseKey : this.baseKey.OpenKey(keyName2, DxStoreKeyAccessMode.Read, false, null);
			try
			{
				if (flag2 || this.isForceIncludeRootProperties)
				{
					foreach (string propertyName in distributedStoreKey.GetValueNames(null))
					{
						bool flag3;
						RegistryValueKind kind;
						object value2 = distributedStoreKey.GetValue(propertyName, out flag3, out kind, null);
						if (value2 != null && flag3)
						{
							PropertyValue propertyValue = new PropertyValue(value2, kind);
							xelement2.Add(propertyValue.ToXElement(propertyName));
						}
					}
				}
				IEnumerable<string> subkeyNames = distributedStoreKey.GetSubkeyNames(null);
				if (subkeyNames != null)
				{
					foreach (string path in subkeyNames)
					{
						string keyName3 = Utils.CombinePathNullSafe(keyName ?? string.Empty, path);
						if (!flag || this.IsIncludeKey(keyName3))
						{
							XElement xelementSnapshotInternal = this.GetXElementSnapshotInternal(keyName3);
							xelement2.Add(xelementSnapshotInternal);
						}
					}
				}
			}
			finally
			{
				if (distributedStoreKey != null && !flag)
				{
					distributedStoreKey.Dispose();
				}
			}
			return xelement;
		}

		// Token: 0x040002CD RID: 717
		private readonly string[] filterRootKeys;

		// Token: 0x040002CE RID: 718
		private readonly string rootKeyName;

		// Token: 0x040002CF RID: 719
		private readonly bool isForceIncludeRootProperties;

		// Token: 0x040002D0 RID: 720
		private AmClusterHandle clusterHandle;

		// Token: 0x040002D1 RID: 721
		private IDistributedStoreKey baseKey;
	}
}
