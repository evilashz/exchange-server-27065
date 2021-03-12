using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200030B RID: 779
	internal sealed class RopMoveFolder : RopCopyMoveFolderBase
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00031DD9 File Offset: 0x0002FFD9
		internal override RopId RopId
		{
			get
			{
				return RopId.MoveFolder;
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00031DDD File Offset: 0x0002FFDD
		internal static Rop CreateRop()
		{
			return new RopMoveFolder();
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00031DE4 File Offset: 0x0002FFE4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = MoveFolderResultFactory.Parse(reader);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00031DFC File Offset: 0x0002FFFC
		protected override void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			MoveFolderResultFactory resultFactory = new MoveFolderResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
			this.result = ropHandler.MoveFolder(sourceServerObject, destinationServerObject, base.ReportProgress, base.FolderId, base.FolderName.StringValue, resultFactory);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00031E41 File Offset: 0x00030041
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new MoveFolderResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00031E54 File Offset: 0x00030054
		internal void SetInput(byte logonIndex, byte sourceHandleTableIndex, byte destinationHandleTableIndex, bool reportProgress, bool useUnicode, StoreId folderId, string folderName)
		{
			base.SetInput(logonIndex, sourceHandleTableIndex, destinationHandleTableIndex, reportProgress, false, useUnicode, folderId, folderName);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00031E73 File Offset: 0x00030073
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00031E88 File Offset: 0x00030088
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" FID=").Append(base.FolderId.ToString());
			stringBuilder.Append(" Name=[").Append(base.FolderName).Append("]");
			stringBuilder.Append(" Progress=").Append(base.ReportProgress);
		}

		// Token: 0x040009D4 RID: 2516
		private const RopId RopType = RopId.MoveFolder;
	}
}
