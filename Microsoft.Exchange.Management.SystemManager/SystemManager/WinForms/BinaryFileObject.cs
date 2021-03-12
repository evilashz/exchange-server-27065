using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000A9 RID: 169
	public class BinaryFileObject
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x00014390 File Offset: 0x00012590
		public BinaryFileObject(string fileName, byte[] fileData)
		{
			this.FileName = fileName;
			this.FileData = fileData;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x000143A6 File Offset: 0x000125A6
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x000143AE File Offset: 0x000125AE
		public string FileName { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000143B7 File Offset: 0x000125B7
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x000143BF File Offset: 0x000125BF
		public byte[] FileData { get; private set; }
	}
}
