using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x0200014F RID: 335
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OABCompressedBlock
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00038876 File Offset: 0x00036A76
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x0003887E File Offset: 0x00036A7E
		public CompressionBlockFlags Flags { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00038887 File Offset: 0x00036A87
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x0003888F File Offset: 0x00036A8F
		public int CompressedLength { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00038898 File Offset: 0x00036A98
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x000388A0 File Offset: 0x00036AA0
		public int UncompressedLength { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x000388A9 File Offset: 0x00036AA9
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x000388B1 File Offset: 0x00036AB1
		public uint CRC { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000388BA File Offset: 0x00036ABA
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x000388C2 File Offset: 0x00036AC2
		public byte[] Data { get; set; }

		// Token: 0x06000D7E RID: 3454 RVA: 0x000388CC File Offset: 0x00036ACC
		public static OABCompressedBlock ReadFrom(BinaryReader reader)
		{
			OABCompressedBlock oabcompressedBlock = new OABCompressedBlock();
			oabcompressedBlock.Flags = (CompressionBlockFlags)reader.ReadUInt32("Flags");
			oabcompressedBlock.CompressedLength = (int)reader.ReadUInt32("CompressedLength");
			oabcompressedBlock.UncompressedLength = (int)reader.ReadUInt32("UncompressedLength");
			oabcompressedBlock.CRC = reader.ReadUInt32("CRC");
			oabcompressedBlock.Data = reader.ReadBytes(oabcompressedBlock.CompressedLength, "Data");
			return oabcompressedBlock;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003893B File Offset: 0x00036B3B
		public void WriteTo(BinaryWriter writer)
		{
			writer.Write((uint)this.Flags);
			writer.Write((uint)this.CompressedLength);
			writer.Write((uint)this.UncompressedLength);
			writer.Write(this.CRC);
			writer.Write(this.Data);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003897C File Offset: 0x00036B7C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("Flags: ");
			stringBuilder.AppendLine(this.Flags.ToString());
			stringBuilder.Append("CompressedLength: ");
			stringBuilder.AppendLine(this.CompressedLength.ToString());
			stringBuilder.Append("UncompressedLength: ");
			stringBuilder.AppendLine(this.UncompressedLength.ToString());
			stringBuilder.Append("CRC: ");
			stringBuilder.AppendLine(this.CRC.ToString("X8"));
			return stringBuilder.ToString();
		}
	}
}
