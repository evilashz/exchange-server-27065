using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000150 RID: 336
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OABCompressedHeader
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00038A2A File Offset: 0x00036C2A
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x00038A32 File Offset: 0x00036C32
		public int MaxVersion { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00038A3B File Offset: 0x00036C3B
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x00038A43 File Offset: 0x00036C43
		public int MinVersion { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00038A4C File Offset: 0x00036C4C
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00038A54 File Offset: 0x00036C54
		public int MaximumCompressionBlockSize { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00038A5D File Offset: 0x00036C5D
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x00038A65 File Offset: 0x00036C65
		public uint UncompressedFileSize { get; set; }

		// Token: 0x06000D8A RID: 3466 RVA: 0x00038A70 File Offset: 0x00036C70
		public static OABCompressedHeader ReadFrom(BinaryReader reader)
		{
			return new OABCompressedHeader
			{
				MaxVersion = (int)reader.ReadUInt32("MaxVersion"),
				MinVersion = (int)reader.ReadUInt32("MinVersion"),
				MaximumCompressionBlockSize = (int)reader.ReadUInt32("MaximumCompressionBlockSize"),
				UncompressedFileSize = reader.ReadUInt32("UncompressedFileSize")
			};
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00038AC8 File Offset: 0x00036CC8
		public void WriteTo(BinaryWriter writer)
		{
			writer.Write((uint)this.MaxVersion);
			writer.Write((uint)this.MinVersion);
			writer.Write((uint)this.MaximumCompressionBlockSize);
			writer.Write(this.UncompressedFileSize);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00038AFC File Offset: 0x00036CFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("MaxVersion: ");
			stringBuilder.AppendLine(this.MaxVersion.ToString());
			stringBuilder.Append("MinVersion: ");
			stringBuilder.AppendLine(this.MinVersion.ToString());
			stringBuilder.Append("MaximumCompressionBlockSize: ");
			stringBuilder.AppendLine(this.MaximumCompressionBlockSize.ToString());
			stringBuilder.Append("UncompressedFileSize: ");
			stringBuilder.AppendLine(this.UncompressedFileSize.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0400072D RID: 1837
		public static readonly int DefaultMaxVersion = 3;

		// Token: 0x0400072E RID: 1838
		public static readonly int DefaultMinVersion = 1;
	}
}
