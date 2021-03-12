using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000405 RID: 1029
	internal struct ManifestEnvelope
	{
		// Token: 0x04001724 RID: 5924
		public const int MaxChunkSize = 65280;

		// Token: 0x04001725 RID: 5925
		public ManifestEnvelope.ManifestFormats Format;

		// Token: 0x04001726 RID: 5926
		public byte MajorVersion;

		// Token: 0x04001727 RID: 5927
		public byte MinorVersion;

		// Token: 0x04001728 RID: 5928
		public byte Magic;

		// Token: 0x04001729 RID: 5929
		public ushort TotalChunks;

		// Token: 0x0400172A RID: 5930
		public ushort ChunkNumber;

		// Token: 0x02000B5F RID: 2911
		public enum ManifestFormats : byte
		{
			// Token: 0x04003436 RID: 13366
			SimpleXmlFormat = 1
		}
	}
}
