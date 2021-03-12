using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000090 RID: 144
	internal class ADEntry
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x00013DF9 File Offset: 0x00011FF9
		public ADEntry(List<string> names, string smtpAddress, Guid objectGuid, RecipientType recipientType, Guid dialPlanGuid, List<Guid> addressListGuids)
		{
			this.Names = names;
			this.SmtpAddress = smtpAddress;
			this.ObjectGuid = objectGuid;
			this.RecipientType = recipientType;
			this.DialPlanGuid = dialPlanGuid;
			this.AddressListGuids = addressListGuids;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00013E2E File Offset: 0x0001202E
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00013E36 File Offset: 0x00012036
		public List<string> Names { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00013E3F File Offset: 0x0001203F
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x00013E47 File Offset: 0x00012047
		public string SmtpAddress { get; private set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00013E50 File Offset: 0x00012050
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00013E58 File Offset: 0x00012058
		public Guid ObjectGuid { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00013E61 File Offset: 0x00012061
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x00013E69 File Offset: 0x00012069
		public RecipientType RecipientType { get; private set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00013E72 File Offset: 0x00012072
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00013E7A File Offset: 0x0001207A
		public Guid DialPlanGuid { get; private set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00013E83 File Offset: 0x00012083
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00013E8B File Offset: 0x0001208B
		public List<Guid> AddressListGuids { get; private set; }
	}
}
