using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x0200005F RID: 95
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceBandSettingsStorageDiagnosableResult
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00009F34 File Offset: 0x00008134
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00009F3C File Offset: 0x0000813C
		[DataMember]
		public PersistedBandDefinition[] PersistedBands { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00009F45 File Offset: 0x00008145
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00009F4D File Offset: 0x0000814D
		[DataMember]
		public Band[] ActiveBands { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00009F56 File Offset: 0x00008156
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00009F5E File Offset: 0x0000815E
		[DataMember]
		public PersistedBandDefinition ModifiedBand { get; set; }
	}
}
