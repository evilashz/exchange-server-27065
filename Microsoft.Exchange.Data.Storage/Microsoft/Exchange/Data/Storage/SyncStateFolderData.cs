using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E31 RID: 3633
	public class SyncStateFolderData
	{
		// Token: 0x1700219F RID: 8607
		// (get) Token: 0x06007DAD RID: 32173 RVA: 0x0022A273 File Offset: 0x00228473
		// (set) Token: 0x06007DAE RID: 32174 RVA: 0x0022A27B File Offset: 0x0022847B
		public string Name { get; set; }

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x06007DAF RID: 32175 RVA: 0x0022A284 File Offset: 0x00228484
		// (set) Token: 0x06007DB0 RID: 32176 RVA: 0x0022A28C File Offset: 0x0022848C
		public DateTime Created { get; set; }

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x06007DB1 RID: 32177 RVA: 0x0022A295 File Offset: 0x00228495
		// (set) Token: 0x06007DB2 RID: 32178 RVA: 0x0022A29D File Offset: 0x0022849D
		public string StorageType { get; set; }

		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x06007DB3 RID: 32179 RVA: 0x0022A2A6 File Offset: 0x002284A6
		// (set) Token: 0x06007DB4 RID: 32180 RVA: 0x0022A2AE File Offset: 0x002284AE
		public string SyncStateBlob { get; set; }

		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x06007DB5 RID: 32181 RVA: 0x0022A2B7 File Offset: 0x002284B7
		// (set) Token: 0x06007DB6 RID: 32182 RVA: 0x0022A2BF File Offset: 0x002284BF
		public int SyncStateSize { get; set; }

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x06007DB7 RID: 32183 RVA: 0x0022A2C8 File Offset: 0x002284C8
		// (set) Token: 0x06007DB8 RID: 32184 RVA: 0x0022A2D0 File Offset: 0x002284D0
		public List<FolderMappingData> FolderMapping { get; set; }
	}
}
