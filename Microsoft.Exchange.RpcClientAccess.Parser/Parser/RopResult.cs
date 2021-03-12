using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000220 RID: 544
	internal abstract class RopResult : Result
	{
		// Token: 0x06000BDC RID: 3036 RVA: 0x000263C4 File Offset: 0x000245C4
		protected RopResult(RopId ropId, ErrorCode errorCode, IServerObject returnObject) : base(ropId)
		{
			this.errorCode = errorCode;
			this.returnObject = returnObject;
			this.returnObjectHandle = ServerObjectHandle.None;
			this.serverObjectHandleTableIndex = 0;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000263ED File Offset: 0x000245ED
		protected RopResult(Reader reader) : base(reader)
		{
			this.serverObjectHandleTableIndex = reader.ReadByte();
			this.errorCode = (ErrorCode)reader.ReadUInt32();
			this.returnObjectHandle = ServerObjectHandle.None;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00026419 File Offset: 0x00024619
		public ErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00026421 File Offset: 0x00024621
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x00026429 File Offset: 0x00024629
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00026432 File Offset: 0x00024632
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0002643A File Offset: 0x0002463A
		internal IServerObject ReturnObject
		{
			get
			{
				return this.returnObject;
			}
			set
			{
				this.returnObject = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00026443 File Offset: 0x00024643
		internal byte HandleTableIndex
		{
			get
			{
				return this.serverObjectHandleTableIndex;
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002644C File Offset: 0x0002464C
		internal static RopResult Parse(Reader reader, RopResult.ResultParserDelegate parseSuccessfulResult, RopResult.ResultParserDelegate parseFailedResult)
		{
			ErrorCode errorCode = (ErrorCode)reader.PeekUInt32(2L);
			ErrorCode errorCode2 = errorCode;
			if (errorCode2 == ErrorCode.None)
			{
				return parseSuccessfulResult(reader);
			}
			return parseFailedResult(reader);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00026478 File Offset: 0x00024678
		internal static long CalculateResultSize(RopResult result)
		{
			long position;
			using (CountWriter countWriter = new CountWriter())
			{
				result.Serialize(countWriter);
				position = countWriter.Position;
			}
			return position;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000264B8 File Offset: 0x000246B8
		internal DiagnosticContextResult GetDiagnosticContextResult(long maxSize)
		{
			if (this.diagnosticInfoProvider == null)
			{
				return null;
			}
			if (maxSize <= 20L)
			{
				return null;
			}
			uint threadId;
			uint requestId;
			DiagnosticContextFlags flags;
			byte[] data;
			this.diagnosticInfoProvider.GetDiagnosticData(maxSize - 20L, out threadId, out requestId, out flags, out data);
			return new DiagnosticContextResult(threadId, requestId, flags, data);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000264F8 File Offset: 0x000246F8
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte(this.serverObjectHandleTableIndex);
			writer.WriteUInt32((uint)this.errorCode);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00026519 File Offset: 0x00024719
		internal void SetServerObjectHandleIndex(byte tableIndex)
		{
			this.serverObjectHandleTableIndex = tableIndex;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00026522 File Offset: 0x00024722
		internal ServerObjectHandle GetServerObjectHandle(ServerObjectMap map)
		{
			if (this.returnObject != null)
			{
				this.returnObjectHandle = map.Add(this.returnObject);
				this.returnObject = null;
			}
			return this.returnObjectHandle;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002654B File Offset: 0x0002474B
		internal void SynchronizeForTest(RopResult otherResult)
		{
			this.returnObject = otherResult.returnObject;
			this.returnObjectHandle = otherResult.returnObjectHandle;
			base.String8Encoding = otherResult.String8Encoding;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00026571 File Offset: 0x00024771
		public void SetDiagnosticInfoProvider(IDiagnosticInfoProvider diagnosticInfoProvider)
		{
			this.diagnosticInfoProvider = diagnosticInfoProvider;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002657C File Offset: 0x0002477C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002659C File Offset: 0x0002479C
		internal virtual void AppendToString(StringBuilder stringBuilder)
		{
			stringBuilder.Append(base.GetType()).Append("(").Append(base.RopId).Append(").");
			stringBuilder.Append(" Error code=").Append(this.ErrorCode);
			stringBuilder.Append("(0x").Append(((uint)this.ErrorCode).ToString("X8")).Append(")");
		}

		// Token: 0x040006A9 RID: 1705
		internal const int HeaderSize = 6;

		// Token: 0x040006AA RID: 1706
		private readonly ErrorCode errorCode;

		// Token: 0x040006AB RID: 1707
		private Exception exception;

		// Token: 0x040006AC RID: 1708
		private IServerObject returnObject;

		// Token: 0x040006AD RID: 1709
		private ServerObjectHandle returnObjectHandle;

		// Token: 0x040006AE RID: 1710
		private byte serverObjectHandleTableIndex;

		// Token: 0x040006AF RID: 1711
		private IDiagnosticInfoProvider diagnosticInfoProvider;

		// Token: 0x02000221 RID: 545
		// (Invoke) Token: 0x06000BEF RID: 3055
		internal delegate RopResult ResultParserDelegate(Reader reader);
	}
}
