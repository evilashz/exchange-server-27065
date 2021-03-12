using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000087 RID: 135
	internal struct ReplId : IEquatable<ReplId>, IFormattable
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0000CA31 File Offset: 0x0000AC31
		public ReplId(ushort replIdValue)
		{
			this.replIdValue = replIdValue;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000CA3A File Offset: 0x0000AC3A
		internal ushort Value
		{
			get
			{
				return this.replIdValue;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public static ReplId Parse(Reader reader)
		{
			ushort num = reader.ReadUInt16();
			return new ReplId(num);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000CA5E File Offset: 0x0000AC5E
		internal void Serialize(Writer writer)
		{
			writer.WriteUInt16(this.replIdValue);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public bool Equals(ReplId other)
		{
			return this.replIdValue == other.replIdValue;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000CA7D File Offset: 0x0000AC7D
		public override bool Equals(object other)
		{
			return other is ReplId && this.Equals((ReplId)other);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000CA98 File Offset: 0x0000AC98
		public override int GetHashCode()
		{
			return this.replIdValue.GetHashCode();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (format != null && (format == "B" || format == "X"))
			{
				return this.replIdValue.ToString("X", formatProvider);
			}
			return string.Format(formatProvider, "ReplId: {0:X}", new object[]
			{
				this.replIdValue
			});
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000CB16 File Offset: 0x0000AD16
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000CB20 File Offset: 0x0000AD20
		public string ToBareString()
		{
			return this.ToString("B", null);
		}

		// Token: 0x040001CD RID: 461
		private readonly ushort replIdValue;
	}
}
