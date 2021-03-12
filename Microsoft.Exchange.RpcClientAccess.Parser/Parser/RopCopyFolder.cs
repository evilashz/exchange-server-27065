using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200028A RID: 650
	internal sealed class RopCopyFolder : RopCopyMoveFolderBase
	{
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x0002A6E9 File Offset: 0x000288E9
		internal override RopId RopId
		{
			get
			{
				return RopId.CopyFolder;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0002A6ED File Offset: 0x000288ED
		protected override bool IsCopyFolder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002A6F0 File Offset: 0x000288F0
		internal static Rop CreateRop()
		{
			return new RopCopyFolder();
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0002A6F7 File Offset: 0x000288F7
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = CopyFolderResultFactory.Parse(reader);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0002A710 File Offset: 0x00028910
		protected override void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			CopyFolderResultFactory resultFactory = new CopyFolderResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
			this.result = ropHandler.CopyFolder(sourceServerObject, destinationServerObject, base.ReportProgress, base.Recurse, base.FolderId, base.FolderName.StringValue, resultFactory);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002A75B File Offset: 0x0002895B
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new CopyFolderResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0002A76E File Offset: 0x0002896E
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0002A784 File Offset: 0x00028984
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" FID=").Append(base.FolderId.ToString());
			stringBuilder.Append(" Name=[").Append(base.FolderName).Append("]");
			stringBuilder.Append(" Recurse=").Append(base.Recurse);
			stringBuilder.Append(" Progress=").Append(base.ReportProgress);
		}

		// Token: 0x04000747 RID: 1863
		private const RopId RopType = RopId.CopyFolder;
	}
}
