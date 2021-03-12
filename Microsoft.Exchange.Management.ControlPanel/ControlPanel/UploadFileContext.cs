using System;
using System.IO;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005FC RID: 1532
	public class UploadFileContext
	{
		// Token: 0x060044BA RID: 17594 RVA: 0x000CF8CE File Offset: 0x000CDACE
		public UploadFileContext(string fileName, Stream fileStream)
		{
			this.FileName = fileName;
			this.FileStream = fileStream;
		}

		// Token: 0x1700268F RID: 9871
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x000CF8E4 File Offset: 0x000CDAE4
		// (set) Token: 0x060044BC RID: 17596 RVA: 0x000CF8EC File Offset: 0x000CDAEC
		public string FileName { get; private set; }

		// Token: 0x17002690 RID: 9872
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x000CF8F5 File Offset: 0x000CDAF5
		// (set) Token: 0x060044BE RID: 17598 RVA: 0x000CF8FD File Offset: 0x000CDAFD
		public Stream FileStream { get; private set; }
	}
}
