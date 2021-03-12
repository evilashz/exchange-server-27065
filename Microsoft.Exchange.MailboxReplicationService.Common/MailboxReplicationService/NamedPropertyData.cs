using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200017A RID: 378
	internal class NamedPropertyData
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x00020ADB File Offset: 0x0001ECDB
		public NamedPropertyData(Guid npGuid, string npName, PropType npType)
		{
			this.NPGuid = npGuid;
			this.NPName = npName;
			this.NPType = npType;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x00020B00 File Offset: 0x0001ED00
		public Guid NPGuid { get; private set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00020B09 File Offset: 0x0001ED09
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x00020B11 File Offset: 0x0001ED11
		public string NPName { get; private set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00020B1A File Offset: 0x0001ED1A
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x00020B22 File Offset: 0x0001ED22
		public PropType NPType { get; private set; }
	}
}
