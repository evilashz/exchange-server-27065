using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002A5 RID: 677
	internal sealed class RopEmptyFolder : RopEmptyFolderBase
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0002BE89 File Offset: 0x0002A089
		internal override RopId RopId
		{
			get
			{
				return RopId.EmptyFolder;
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0002BE8D File Offset: 0x0002A08D
		internal static Rop CreateRop()
		{
			return new RopEmptyFolder();
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0002BE94 File Offset: 0x0002A094
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = EmptyFolderResultFactory.Parse(reader);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0002BEAC File Offset: 0x0002A0AC
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			EmptyFolderResultFactory resultFactory = new EmptyFolderResultFactory(base.LogonIndex);
			this.result = ropHandler.EmptyFolder(serverObject, base.ReportProgress, base.EmptyFolderFlags, resultFactory);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002BEDF File Offset: 0x0002A0DF
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new EmptyFolderResultFactory(base.LogonIndex);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0002BEEC File Offset: 0x0002A0EC
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x040007A0 RID: 1952
		private const RopId RopType = RopId.EmptyFolder;
	}
}
