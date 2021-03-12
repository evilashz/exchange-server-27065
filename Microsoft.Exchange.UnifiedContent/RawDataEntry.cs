using System;
using System.IO;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x02000008 RID: 8
	internal class RawDataEntry
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000028C4 File Offset: 0x00000AC4
		internal RawDataEntry(Stream entryStream, long entryPosition)
		{
			using (SharedContentReader sharedContentReader = new SharedContentReader(entryStream))
			{
				this.entryPosition = entryPosition;
				sharedContentReader.ValidateEntryId(286331153U);
				this.extractedContentEntryPosition = sharedContentReader.ReadInt64();
				this.dataStream = sharedContentReader.ReadStream();
				sharedContentReader.ValidateAtEndOfEntry();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000292C File Offset: 0x00000B2C
		internal long EntryPosition
		{
			get
			{
				return this.entryPosition;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002934 File Offset: 0x00000B34
		internal long ExtractedContentEntryPosition
		{
			get
			{
				return this.extractedContentEntryPosition;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000293C File Offset: 0x00000B3C
		internal Stream DataStream
		{
			get
			{
				return this.dataStream;
			}
		}

		// Token: 0x04000017 RID: 23
		private const uint EntryId = 286331153U;

		// Token: 0x04000018 RID: 24
		private readonly long entryPosition;

		// Token: 0x04000019 RID: 25
		private readonly long extractedContentEntryPosition;

		// Token: 0x0400001A RID: 26
		private readonly Stream dataStream;
	}
}
