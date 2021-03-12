using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x0200000B RID: 11
	internal class SharedContentWriter : BinaryWriter
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002FF7 File Offset: 0x000011F7
		internal SharedContentWriter(Stream stream) : base(stream, Encoding.Unicode)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003005 File Offset: 0x00001205
		public static long ComputeLength(string str)
		{
			return (long)(8 + str.Length * 2 + 2);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003014 File Offset: 0x00001214
		public static long ComputeLength(Stream stream)
		{
			return 8L + stream.Length;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000301F File Offset: 0x0000121F
		public override void Write(string str)
		{
			this.Write(((long)str.Length + 1L) * 2L);
			this.Write(str.ToCharArray());
			this.Write('\0');
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003047 File Offset: 0x00001247
		public override void Write(byte[] buffer)
		{
			this.Write(buffer.Length);
			base.Write(buffer);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003059 File Offset: 0x00001259
		internal void Write(Stream stream)
		{
			this.Write(stream.Length);
			stream.Position = 0L;
			stream.CopyTo(this.BaseStream);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000307B File Offset: 0x0000127B
		internal void Write(UnifiedContentSerializer.EntryId id)
		{
			this.Write((uint)id);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003084 File Offset: 0x00001284
		internal void Write(UnifiedContentSerializer.PropertyId id)
		{
			this.Write((uint)id);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000308D File Offset: 0x0000128D
		internal void ValidateAtEndOfEntry()
		{
			if (this.BaseStream.Position != this.BaseStream.Length)
			{
				throw new FormatException("Shared Content Entry invalid");
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030B2 File Offset: 0x000012B2
		protected override void Dispose(bool disposing)
		{
			this.BaseStream.Flush();
		}

		// Token: 0x04000033 RID: 51
		public const int EntryIdSize = 4;

		// Token: 0x04000034 RID: 52
		public const int PropertyIdSize = 4;

		// Token: 0x04000035 RID: 53
		public const int SharedEntryPosSize = 8;

		// Token: 0x04000036 RID: 54
		public const int RawEntryPosSize = 8;

		// Token: 0x04000037 RID: 55
		public const int ExtractedEntryPosSize = 8;

		// Token: 0x04000038 RID: 56
		public const int StreamLengthSize = 8;
	}
}
