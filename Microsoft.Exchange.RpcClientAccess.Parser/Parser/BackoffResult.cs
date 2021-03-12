using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000224 RID: 548
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BackoffResult : Result
	{
		// Token: 0x06000BFE RID: 3070 RVA: 0x000267D4 File Offset: 0x000249D4
		internal BackoffResult(byte logonId, uint duration, BackoffRopData[] ropData, byte[] additionalData, Encoding string8Encoding) : base(RopId.Backoff)
		{
			if (ropData == null)
			{
				throw new ArgumentNullException("ropData");
			}
			if (ropData.Length > 255)
			{
				throw new ArgumentException("Cannot contain more than 255 entries", "ropData");
			}
			this.logonId = logonId;
			this.duration = duration;
			this.ropData = ropData;
			this.additionalData = additionalData;
			base.String8Encoding = string8Encoding;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002683C File Offset: 0x00024A3C
		internal BackoffResult(Reader reader) : base(reader)
		{
			this.logonId = reader.ReadByte();
			this.duration = reader.ReadUInt32();
			byte b = reader.ReadByte();
			this.ropData = new BackoffRopData[(int)b];
			for (int i = 0; i < (int)b; i++)
			{
				this.ropData[i] = new BackoffRopData(reader);
			}
			this.additionalData = reader.ReadSizeAndByteArray();
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x000268AA File Offset: 0x00024AAA
		public byte LogonId
		{
			get
			{
				return this.logonId;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x000268B2 File Offset: 0x00024AB2
		public uint Duration
		{
			get
			{
				return this.duration;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x000268BA File Offset: 0x00024ABA
		public BackoffRopData[] RopData
		{
			get
			{
				return this.ropData;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000268C2 File Offset: 0x00024AC2
		public byte[] AdditionalData
		{
			get
			{
				return this.additionalData;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000268CC File Offset: 0x00024ACC
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte(this.LogonId);
			writer.WriteUInt32(this.Duration);
			writer.WriteByte((byte)this.ropData.Length);
			foreach (BackoffRopData backoffRopData in this.RopData)
			{
				backoffRopData.Serialize(writer);
			}
			writer.WriteSizedBytes(this.AdditionalData);
		}

		// Token: 0x040006B2 RID: 1714
		private readonly byte logonId;

		// Token: 0x040006B3 RID: 1715
		private readonly uint duration;

		// Token: 0x040006B4 RID: 1716
		private readonly BackoffRopData[] ropData;

		// Token: 0x040006B5 RID: 1717
		private readonly byte[] additionalData;
	}
}
