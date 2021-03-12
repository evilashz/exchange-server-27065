using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x0200015C RID: 348
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OABFileHeader
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0003A75B File Offset: 0x0003895B
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0003A763 File Offset: 0x00038963
		public int Version { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003A76C File Offset: 0x0003896C
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0003A774 File Offset: 0x00038974
		public uint CRC { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003A77D File Offset: 0x0003897D
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0003A785 File Offset: 0x00038985
		public int RecordCount { get; set; }

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003A790 File Offset: 0x00038990
		public static OABFileHeader ReadFrom(BinaryReader reader)
		{
			return new OABFileHeader
			{
				Version = (int)reader.ReadUInt32("Version"),
				CRC = reader.ReadUInt32("CRC"),
				RecordCount = (int)reader.ReadUInt32("RecordCount")
			};
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003A7D7 File Offset: 0x000389D7
		public void WriteTo(BinaryWriter writer)
		{
			writer.Write((uint)this.Version);
			writer.Write(this.CRC);
			writer.Write((uint)this.RecordCount);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003A800 File Offset: 0x00038A00
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("Version: ");
			stringBuilder.AppendLine(this.Version.ToString());
			stringBuilder.Append("CRC: ");
			stringBuilder.AppendLine(this.CRC.ToString("X8"));
			stringBuilder.Append("RecordCount: ");
			stringBuilder.AppendLine(this.RecordCount.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x04000777 RID: 1911
		public static readonly int DefaultVersion = 32;
	}
}
