using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002E1 RID: 737
	internal sealed class RopHardEmptyFolder : RopEmptyFolderBase
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0002F993 File Offset: 0x0002DB93
		internal override RopId RopId
		{
			get
			{
				return RopId.HardEmptyFolder;
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0002F99A File Offset: 0x0002DB9A
		internal static Rop CreateRop()
		{
			return new RopHardEmptyFolder();
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0002F9A1 File Offset: 0x0002DBA1
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = HardEmptyFolderResultFactory.Parse(reader);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0002F9B8 File Offset: 0x0002DBB8
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			HardEmptyFolderResultFactory resultFactory = new HardEmptyFolderResultFactory(base.LogonIndex);
			this.result = ropHandler.HardEmptyFolder(serverObject, base.ReportProgress, base.EmptyFolderFlags, resultFactory);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0002F9EB File Offset: 0x0002DBEB
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new HardEmptyFolderResultFactory(base.LogonIndex);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0002F9F8 File Offset: 0x0002DBF8
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0400086F RID: 2159
		private const RopId RopType = RopId.HardEmptyFolder;
	}
}
