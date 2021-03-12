using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200011A RID: 282
	public class AttributeMetadata
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x0001EBB7 File Offset: 0x0001CDB7
		internal AttributeMetadata()
		{
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0001EBBF File Offset: 0x0001CDBF
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0001EBC7 File Offset: 0x0001CDC7
		public string AttributeName { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0001EBD0 File Offset: 0x0001CDD0
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
		public int Version { get; private set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0001EBE1 File Offset: 0x0001CDE1
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0001EBE9 File Offset: 0x0001CDE9
		public DateTime LastWriteTime { get; private set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0001EBF2 File Offset: 0x0001CDF2
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x0001EBFA File Offset: 0x0001CDFA
		public Guid OriginatingInvocationId { get; private set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0001EC03 File Offset: 0x0001CE03
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x0001EC0B File Offset: 0x0001CE0B
		public long OriginatingUpdateSequenceNumber { get; private set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0001EC14 File Offset: 0x0001CE14
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x0001EC1C File Offset: 0x0001CE1C
		public long LocalUpdateSequenceNumber { get; private set; }

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001EC28 File Offset: 0x0001CE28
		public static AttributeMetadata Parse(byte[] binary)
		{
			if (binary == null)
			{
				throw new ArgumentNullException("binary");
			}
			Exception innerException = null;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(binary))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.Unicode))
					{
						AttributeMetadata attributeMetadata = new AttributeMetadata();
						int num = binaryReader.ReadInt32();
						attributeMetadata.Version = binaryReader.ReadInt32();
						attributeMetadata.LastWriteTime = AttributeMetadata.ReadFileTimeUtc(binaryReader);
						attributeMetadata.OriginatingInvocationId = new Guid(binaryReader.ReadBytes(16));
						attributeMetadata.OriginatingUpdateSequenceNumber = binaryReader.ReadInt64();
						attributeMetadata.LocalUpdateSequenceNumber = binaryReader.ReadInt64();
						memoryStream.Seek((long)num, SeekOrigin.Begin);
						attributeMetadata.AttributeName = AttributeMetadata.ReadNullTerminatedString(binaryReader);
						return attributeMetadata;
					}
				}
			}
			catch (ArgumentException ex)
			{
				innerException = ex;
			}
			catch (IOException ex2)
			{
				innerException = ex2;
			}
			throw new FormatException(DataStrings.InvalidFormat, innerException);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001ED30 File Offset: 0x0001CF30
		internal static DateTime ReadFileTimeUtc(BinaryReader reader)
		{
			long num = reader.ReadInt64();
			long num2 = num;
			if (num2 <= 0L && num2 >= -1L)
			{
				switch ((int)(num2 - -1L))
				{
				case 0:
					return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
				case 1:
					return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
				}
			}
			return DateTime.FromFileTimeUtc(num);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001ED84 File Offset: 0x0001CF84
		internal static string ReadNullTerminatedString(BinaryReader reader)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c = reader.ReadChar();
				if (c == '\0')
				{
					break;
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}
	}
}
