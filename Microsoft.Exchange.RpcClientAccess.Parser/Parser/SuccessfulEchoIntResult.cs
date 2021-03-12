using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002A1 RID: 673
	internal sealed class SuccessfulEchoIntResult : RopResult
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002BC2C File Offset: 0x00029E2C
		internal SuccessfulEchoIntResult(int returnValue, int outParameter) : base(RopId.EchoInt, ErrorCode.None, null)
		{
			this.returnValue = returnValue;
			this.outParameter = outParameter;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002BC49 File Offset: 0x00029E49
		internal SuccessfulEchoIntResult(Reader reader) : base(reader)
		{
			this.outParameter = reader.ReadInt32();
			this.returnValue = reader.ReadInt32();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002BC6A File Offset: 0x00029E6A
		public static RopResult Parse(Reader reader)
		{
			return new SuccessfulEchoIntResult(reader);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002BC72 File Offset: 0x00029E72
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteInt32(this.outParameter);
			writer.WriteInt32(this.returnValue);
		}

		// Token: 0x04000797 RID: 1943
		private readonly int returnValue;

		// Token: 0x04000798 RID: 1944
		private readonly int outParameter;
	}
}
