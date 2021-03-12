using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006CC RID: 1740
	[DebuggerDisplay("{PartitionFqdn}-{Version}")]
	[DataContract]
	internal class TopologyVersion : IExtensibleDataObject
	{
		// Token: 0x060050A4 RID: 20644 RVA: 0x0012AD51 File Offset: 0x00128F51
		public TopologyVersion(string partitionFqdn, int version)
		{
			if (string.IsNullOrEmpty(partitionFqdn))
			{
				throw new ArgumentException("partitionFqdn");
			}
			this.PartitionFqdn = partitionFqdn;
			this.Version = version;
		}

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x060050A5 RID: 20645 RVA: 0x0012AD7A File Offset: 0x00128F7A
		// (set) Token: 0x060050A6 RID: 20646 RVA: 0x0012AD82 File Offset: 0x00128F82
		[DataMember(IsRequired = true)]
		public string PartitionFqdn { get; set; }

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x060050A7 RID: 20647 RVA: 0x0012AD8B File Offset: 0x00128F8B
		// (set) Token: 0x060050A8 RID: 20648 RVA: 0x0012AD93 File Offset: 0x00128F93
		[DataMember(IsRequired = true)]
		public int Version { get; set; }

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x060050A9 RID: 20649 RVA: 0x0012AD9C File Offset: 0x00128F9C
		// (set) Token: 0x060050AA RID: 20650 RVA: 0x0012ADA4 File Offset: 0x00128FA4
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x060050AB RID: 20651 RVA: 0x0012ADAD File Offset: 0x00128FAD
		public override string ToString()
		{
			return this.PartitionFqdn + ":" + this.Version;
		}
	}
}
