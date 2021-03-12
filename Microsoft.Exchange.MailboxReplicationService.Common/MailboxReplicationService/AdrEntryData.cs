using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000050 RID: 80
	[DataContract]
	internal sealed class AdrEntryData
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x000078B3 File Offset: 0x00005AB3
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x000078BB File Offset: 0x00005ABB
		[DataMember]
		public PropValueData[] Values { get; set; }

		// Token: 0x06000418 RID: 1048 RVA: 0x000078C4 File Offset: 0x00005AC4
		public override string ToString()
		{
			return string.Format("ADRENTRY: {0}", CommonUtils.ConcatEntries<PropValueData>(this.Values, null));
		}
	}
}
