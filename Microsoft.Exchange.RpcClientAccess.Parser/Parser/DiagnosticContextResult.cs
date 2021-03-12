using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200023A RID: 570
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DiagnosticContextResult : RopResult
	{
		// Token: 0x06000C70 RID: 3184 RVA: 0x00027510 File Offset: 0x00025710
		internal DiagnosticContextResult(uint threadId, uint requestId, DiagnosticContextFlags flags, byte[] data) : base(RopId.DiagnosticContext, ErrorCode.None, null)
		{
			this.threadId = threadId;
			this.requestId = requestId;
			this.flags = flags;
			this.data = data;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002753C File Offset: 0x0002573C
		internal DiagnosticContextResult(Reader reader) : base(reader)
		{
			byte b = reader.ReadByte();
			if (b != 255)
			{
				throw new BufferParseException("Did not recognize DiagnosticContext format: " + b);
			}
			this.threadId = reader.ReadUInt32();
			this.requestId = reader.ReadUInt32();
			this.flags = (DiagnosticContextFlags)reader.ReadByte();
			uint count = reader.ReadUInt32();
			this.data = reader.ReadBytes(count);
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000275AD File Offset: 0x000257AD
		public uint ThreadId
		{
			get
			{
				return this.threadId;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x000275B5 File Offset: 0x000257B5
		public uint RequestId
		{
			get
			{
				return this.requestId;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000275BD File Offset: 0x000257BD
		public DiagnosticContextFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000275C5 File Offset: 0x000257C5
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000275D0 File Offset: 0x000257D0
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte(byte.MaxValue);
			writer.WriteUInt32(this.threadId);
			writer.WriteUInt32(this.requestId);
			writer.WriteByte((byte)this.flags);
			writer.WriteUInt32((uint)this.data.Length);
			writer.WriteBytes(this.data);
		}

		// Token: 0x040006D3 RID: 1747
		internal const int DiagnosticHeaderSize = 20;

		// Token: 0x040006D4 RID: 1748
		private readonly uint threadId;

		// Token: 0x040006D5 RID: 1749
		private readonly uint requestId;

		// Token: 0x040006D6 RID: 1750
		private readonly DiagnosticContextFlags flags;

		// Token: 0x040006D7 RID: 1751
		private readonly byte[] data;
	}
}
