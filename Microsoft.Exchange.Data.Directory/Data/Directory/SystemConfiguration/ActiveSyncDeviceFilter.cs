using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200030F RID: 783
	[Serializable]
	public class ActiveSyncDeviceFilter : XMLSerializableBase
	{
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x0009B2C3 File Offset: 0x000994C3
		// (set) Token: 0x06002429 RID: 9257 RVA: 0x0009B2CB File Offset: 0x000994CB
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x0009B2D4 File Offset: 0x000994D4
		// (set) Token: 0x0600242B RID: 9259 RVA: 0x0009B2DC File Offset: 0x000994DC
		[XmlElement("ApplyForAllDevices")]
		public bool ApplyForAllDevices { get; set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x0009B2E5 File Offset: 0x000994E5
		// (set) Token: 0x0600242D RID: 9261 RVA: 0x0009B2ED File Offset: 0x000994ED
		[XmlArray("Rules")]
		[XmlArrayItem("Rule")]
		public List<ActiveSyncDeviceFilterRule> Rules { get; set; }

		// Token: 0x0600242E RID: 9262 RVA: 0x0009B2F6 File Offset: 0x000994F6
		public ActiveSyncDeviceFilter() : this(null, null)
		{
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0009B300 File Offset: 0x00099500
		public ActiveSyncDeviceFilter(string name, List<ActiveSyncDeviceFilterRule> rules)
		{
			this.Name = name;
			this.Rules = rules;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0009B316 File Offset: 0x00099516
		public ActiveSyncDeviceFilter(string name, bool applyForAllDevices)
		{
			this.Name = name;
			this.ApplyForAllDevices = applyForAllDevices;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x0009B34C File Offset: 0x0009954C
		public override int GetHashCode()
		{
			int hashCode = this.Name.GetHashCode() ^ this.ApplyForAllDevices.GetHashCode();
			if (this.Rules != null)
			{
				this.Rules.ForEach(delegate(ActiveSyncDeviceFilterRule rule)
				{
					hashCode ^= rule.GetHashCode();
				});
			}
			return hashCode;
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0009B3AC File Offset: 0x000995AC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			ActiveSyncDeviceFilter activeSyncDeviceFilter = obj as ActiveSyncDeviceFilter;
			return activeSyncDeviceFilter != null && (this.Rules != null || activeSyncDeviceFilter.Rules == null) && (this.Rules == null || activeSyncDeviceFilter.Rules != null) && (string.Equals(this.Name, activeSyncDeviceFilter.Name, StringComparison.OrdinalIgnoreCase) && this.ApplyForAllDevices == activeSyncDeviceFilter.ApplyForAllDevices) && ((this.Rules == null && activeSyncDeviceFilter.Rules == null) || (this.Rules.Count == activeSyncDeviceFilter.Rules.Count && !this.Rules.Except(activeSyncDeviceFilter.Rules).Any<ActiveSyncDeviceFilterRule>()));
		}
	}
}
