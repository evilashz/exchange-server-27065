using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200027E RID: 638
	internal sealed class SuccessfulReadPerUserInformationResult : RopResult
	{
		// Token: 0x06000DCD RID: 3533 RVA: 0x00029BBD File Offset: 0x00027DBD
		internal SuccessfulReadPerUserInformationResult(bool hasFinished, byte[] data) : base(RopId.ReadPerUserInformation, ErrorCode.None, null)
		{
			Util.ThrowOnNullArgument(data, "data");
			this.hasFinished = hasFinished;
			this.data = data;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00029BE2 File Offset: 0x00027DE2
		internal SuccessfulReadPerUserInformationResult(Reader reader) : base(reader)
		{
			this.hasFinished = reader.ReadBool();
			this.data = reader.ReadSizeAndByteArray();
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00029C03 File Offset: 0x00027E03
		internal bool HasFinished
		{
			get
			{
				return this.hasFinished;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00029C0B File Offset: 0x00027E0B
		internal byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00029C13 File Offset: 0x00027E13
		internal static SuccessfulReadPerUserInformationResult Parse(Reader reader)
		{
			return new SuccessfulReadPerUserInformationResult(reader);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00029C1B File Offset: 0x00027E1B
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.hasFinished, 1);
			writer.WriteSizedBytes(this.data);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00029C40 File Offset: 0x00027E40
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" HasFinished=").Append(this.hasFinished);
			stringBuilder.Append(" Data=[");
			Util.AppendToString(stringBuilder, this.data);
			stringBuilder.Append("]");
		}

		// Token: 0x0400072A RID: 1834
		private readonly bool hasFinished;

		// Token: 0x0400072B RID: 1835
		private readonly byte[] data;
	}
}
