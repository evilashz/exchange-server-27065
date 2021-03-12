using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x020002D8 RID: 728
	internal struct AttachmentFile
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x0005469D File Offset: 0x0005289D
		public AttachmentFile(string fileName, string url)
		{
			this.FileName = fileName;
			this.FileURL = url;
		}

		// Token: 0x04000D2E RID: 3374
		public readonly string FileName;

		// Token: 0x04000D2F RID: 3375
		public readonly string FileURL;
	}
}
