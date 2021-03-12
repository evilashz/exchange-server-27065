using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UnifiedContent;

namespace Microsoft.Filtering
{
	// Token: 0x02000010 RID: 16
	internal sealed class FileFipsDataStreamFilteringRequest : FipsDataStreamFilteringRequest
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002A0C File Offset: 0x00000C0C
		private FileFipsDataStreamFilteringRequest(string fileName, Stream fileStream, ContentManager contentManager) : base(Guid.NewGuid().ToString(), contentManager)
		{
			this.FileName = fileName;
			this.FileStream = fileStream;
			this.RecoveryOptions = RecoveryOptions.None;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002A48 File Offset: 0x00000C48
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002A50 File Offset: 0x00000C50
		public Stream FileStream { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002A59 File Offset: 0x00000C59
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002A61 File Offset: 0x00000C61
		public string FileName { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002A6A File Offset: 0x00000C6A
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002A72 File Offset: 0x00000C72
		public override RecoveryOptions RecoveryOptions { get; set; }

		// Token: 0x06000033 RID: 51 RVA: 0x00002A8C File Offset: 0x00000C8C
		public static FileFipsDataStreamFilteringRequest CreateInstance(string fileName, Stream fileStream, ContentManager contentManager)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("fileName", fileName);
			ArgumentValidator.ThrowIfInvalidValue<Stream>("fileStream", fileStream, (Stream stream) => stream != null || stream.Length > 0L);
			ArgumentValidator.ThrowIfNull("contentManager", contentManager);
			return new FileFipsDataStreamFilteringRequest(fileName, fileStream, contentManager);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002ADF File Offset: 0x00000CDF
		protected override void Serialize(UnifiedContentSerializer unifiedContentSerializer, bool bypassBodyTextTruncation = true)
		{
			unifiedContentSerializer.AddStream(UnifiedContentSerializer.EntryId.Attachment, this.FileStream, this.FileName);
		}
	}
}
