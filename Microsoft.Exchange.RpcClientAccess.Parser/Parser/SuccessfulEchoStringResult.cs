using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002A3 RID: 675
	internal sealed class SuccessfulEchoStringResult : RopResult
	{
		// Token: 0x06000F06 RID: 3846 RVA: 0x0002BD6D File Offset: 0x00029F6D
		internal SuccessfulEchoStringResult(string returnValue, string outParameter) : base(RopId.EchoString, ErrorCode.None, null)
		{
			this.returnValue = returnValue;
			this.outParameter = outParameter;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002BD8A File Offset: 0x00029F8A
		internal SuccessfulEchoStringResult(Reader reader) : base(reader)
		{
			this.outParameter = reader.ReadAsciiString(StringFlags.Sized16);
			this.returnValue = reader.ReadAsciiString(StringFlags.Sized16);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0002BDAD File Offset: 0x00029FAD
		internal static RopResult Parse(Reader reader)
		{
			return new SuccessfulEchoStringResult(reader);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0002BDB5 File Offset: 0x00029FB5
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteAsciiString(this.outParameter, StringFlags.Sized16);
			writer.WriteAsciiString(this.returnValue, StringFlags.Sized16);
		}

		// Token: 0x0400079C RID: 1948
		private readonly string returnValue;

		// Token: 0x0400079D RID: 1949
		private readonly string outParameter;
	}
}
