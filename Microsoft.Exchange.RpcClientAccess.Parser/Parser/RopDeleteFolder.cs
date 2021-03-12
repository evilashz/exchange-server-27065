using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000298 RID: 664
	internal sealed class RopDeleteFolder : InputRop
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0002B5E9 File Offset: 0x000297E9
		internal override RopId RopId
		{
			get
			{
				return RopId.DeleteFolder;
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0002B5ED File Offset: 0x000297ED
		internal static Rop CreateRop()
		{
			return new RopDeleteFolder();
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002B5F4 File Offset: 0x000297F4
		internal void SetInput(byte logonIndex, byte handleTableIndex, DeleteFolderFlags deleteFolderFlags, StoreId folderId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.deleteFolderFlags = deleteFolderFlags;
			this.folderId = folderId;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0002B60D File Offset: 0x0002980D
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.deleteFolderFlags);
			this.folderId.Serialize(writer);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002B62F File Offset: 0x0002982F
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = DeleteFolderResult.Parse(reader);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002B645 File Offset: 0x00029845
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopDeleteFolder.resultFactory;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0002B64C File Offset: 0x0002984C
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.deleteFolderFlags = (DeleteFolderFlags)reader.ReadByte();
			this.folderId = StoreId.Parse(reader);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0002B66E File Offset: 0x0002986E
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0002B683 File Offset: 0x00029883
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.DeleteFolder(serverObject, this.deleteFolderFlags, this.folderId, RopDeleteFolder.resultFactory);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0002B6A4 File Offset: 0x000298A4
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.deleteFolderFlags);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
		}

		// Token: 0x04000782 RID: 1922
		private const RopId RopType = RopId.DeleteFolder;

		// Token: 0x04000783 RID: 1923
		private static DeleteFolderResultFactory resultFactory = new DeleteFolderResultFactory();

		// Token: 0x04000784 RID: 1924
		private DeleteFolderFlags deleteFolderFlags;

		// Token: 0x04000785 RID: 1925
		private StoreId folderId;
	}
}
