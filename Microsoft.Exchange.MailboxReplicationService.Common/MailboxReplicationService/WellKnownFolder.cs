using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004E RID: 78
	[DataContract]
	internal sealed class WellKnownFolder
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x000077B0 File Offset: 0x000059B0
		public WellKnownFolder(int wkfType, byte[] entryId)
		{
			this.WKFType = wkfType;
			this.EntryId = entryId;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000077C6 File Offset: 0x000059C6
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x000077CE File Offset: 0x000059CE
		[DataMember]
		public byte[] EntryId { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000077D7 File Offset: 0x000059D7
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x000077DF File Offset: 0x000059DF
		[DataMember]
		public int WKFType { get; set; }
	}
}
