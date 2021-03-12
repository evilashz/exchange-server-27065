using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005D RID: 93
	[DataContract]
	internal sealed class ICSViewData
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00008BE5 File Offset: 0x00006DE5
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x00008BED File Offset: 0x00006DED
		[DataMember(EmitDefaultValue = false)]
		public bool Conversation { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00008BF6 File Offset: 0x00006DF6
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00008BFE File Offset: 0x00006DFE
		[DataMember(EmitDefaultValue = false)]
		public int[] CoveringPropertyTags { get; set; }

		// Token: 0x0600049E RID: 1182 RVA: 0x00008C15 File Offset: 0x00006E15
		public override string ToString()
		{
			return string.Format("ICS: Conversation={0}, {1}", this.Conversation, CommonUtils.ConcatEntries<int>(this.CoveringPropertyTags, (int ptag) => ptag.ToString("X")));
		}
	}
}
