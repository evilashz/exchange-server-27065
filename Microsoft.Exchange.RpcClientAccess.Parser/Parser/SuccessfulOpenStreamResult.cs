using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000278 RID: 632
	internal class SuccessfulOpenStreamResult : RopResult
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x00029778 File Offset: 0x00027978
		internal SuccessfulOpenStreamResult(IServerObject serverObject, uint streamSize) : base(RopId.OpenStream, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			this.streamSize = streamSize;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00029799 File Offset: 0x00027999
		internal SuccessfulOpenStreamResult(Reader reader) : base(reader)
		{
			this.streamSize = reader.ReadUInt32();
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000297AE File Offset: 0x000279AE
		internal static SuccessfulOpenStreamResult Parse(Reader reader)
		{
			return new SuccessfulOpenStreamResult(reader);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000297B6 File Offset: 0x000279B6
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.streamSize);
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x000297CB File Offset: 0x000279CB
		public uint StreamSize
		{
			get
			{
				return this.streamSize;
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000297D4 File Offset: 0x000279D4
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Size=0x").Append(this.StreamSize.ToString("X"));
		}

		// Token: 0x04000721 RID: 1825
		private readonly uint streamSize;
	}
}
