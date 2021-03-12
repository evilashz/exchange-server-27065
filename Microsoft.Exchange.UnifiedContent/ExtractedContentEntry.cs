using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x02000004 RID: 4
	internal class ExtractedContentEntry
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002764 File Offset: 0x00000964
		internal ExtractedContentEntry(Stream entryStream, long entryPosition)
		{
			this.Properties = new Dictionary<string, object>();
			using (SharedContentReader sharedContentReader = new SharedContentReader(entryStream))
			{
				this.EntryPos = entryPosition;
				sharedContentReader.ValidateEntryId(572662306U);
				this.ParentPos = sharedContentReader.ReadInt64();
				this.NextSiblingPos = sharedContentReader.ReadInt64();
				this.FirstChildPos = sharedContentReader.ReadInt64();
				this.FileName = sharedContentReader.ReadString();
				this.Properties.Add("Parsing::ParsingKeys::Subject", sharedContentReader.ReadString());
				this.TextExtractionStatus = sharedContentReader.ReadUInt32();
				this.RefId = sharedContentReader.ReadUInt32();
				this.TextStream = sharedContentReader.ReadStream();
				sharedContentReader.ValidateAtEndOfEntry();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002828 File Offset: 0x00000A28
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002830 File Offset: 0x00000A30
		internal long EntryPos { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002839 File Offset: 0x00000A39
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002841 File Offset: 0x00000A41
		internal long ParentPos { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000284A File Offset: 0x00000A4A
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002852 File Offset: 0x00000A52
		internal long FirstChildPos { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000285B File Offset: 0x00000A5B
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002863 File Offset: 0x00000A63
		internal long NextSiblingPos { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000286C File Offset: 0x00000A6C
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002874 File Offset: 0x00000A74
		internal string FileName { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000287D File Offset: 0x00000A7D
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002885 File Offset: 0x00000A85
		internal uint TextExtractionStatus { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000288E File Offset: 0x00000A8E
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002896 File Offset: 0x00000A96
		internal uint RefId { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000289F File Offset: 0x00000A9F
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000028A7 File Offset: 0x00000AA7
		internal Stream TextStream { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000028B0 File Offset: 0x00000AB0
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000028B8 File Offset: 0x00000AB8
		internal Dictionary<string, object> Properties { get; private set; }

		// Token: 0x04000006 RID: 6
		public const string Subject = "Parsing::ParsingKeys::Subject";

		// Token: 0x04000007 RID: 7
		private const uint EntryId = 572662306U;
	}
}
