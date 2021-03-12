using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200033B RID: 827
	[DataContract]
	public abstract class NodeInfo : BaseRow
	{
		// Token: 0x06002F3A RID: 12090 RVA: 0x000902D3 File Offset: 0x0008E4D3
		protected NodeInfo() : base(null, null)
		{
		}

		// Token: 0x17001EEA RID: 7914
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000902DD File Offset: 0x0008E4DD
		// (set) Token: 0x06002F3C RID: 12092 RVA: 0x000902E5 File Offset: 0x0008E4E5
		[DataMember]
		public string ID { get; internal set; }

		// Token: 0x17001EEB RID: 7915
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x000902EE File Offset: 0x0008E4EE
		// (set) Token: 0x06002F3E RID: 12094 RVA: 0x000902F6 File Offset: 0x0008E4F6
		[DataMember]
		public string Name { get; internal set; }

		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x000902FF File Offset: 0x0008E4FF
		// (set) Token: 0x06002F40 RID: 12096 RVA: 0x00090307 File Offset: 0x0008E507
		[DataMember]
		public bool CanNewSubNode { get; internal set; }
	}
}
