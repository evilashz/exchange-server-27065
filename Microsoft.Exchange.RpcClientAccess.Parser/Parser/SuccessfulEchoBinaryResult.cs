using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200029F RID: 671
	internal sealed class SuccessfulEchoBinaryResult : RopResult
	{
		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002BAED File Offset: 0x00029CED
		internal SuccessfulEchoBinaryResult(int returnValue, byte[] outParameter) : base(RopId.EchoBinary, ErrorCode.None, null)
		{
			this.returnValue = returnValue;
			this.outParameter = outParameter;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002BB0A File Offset: 0x00029D0A
		internal SuccessfulEchoBinaryResult(Reader reader) : base(reader)
		{
			this.returnValue = reader.ReadInt32();
			this.outParameter = reader.ReadSizeAndByteArray();
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002BB2B File Offset: 0x00029D2B
		internal static RopResult Parse(Reader reader)
		{
			return new SuccessfulEchoBinaryResult(reader);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002BB33 File Offset: 0x00029D33
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteInt32(this.returnValue);
			writer.WriteSizedBytes(this.outParameter);
		}

		// Token: 0x04000792 RID: 1938
		private readonly int returnValue;

		// Token: 0x04000793 RID: 1939
		private readonly byte[] outParameter;
	}
}
