using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000293 RID: 659
	internal sealed class RopCreateFolder : InputOutputRop
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0002AEAD File Offset: 0x000290AD
		internal override RopId RopId
		{
			get
			{
				return RopId.CreateFolder;
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0002AEB1 File Offset: 0x000290B1
		internal static Rop CreateRop()
		{
			return new RopCreateFolder();
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0002AEB8 File Offset: 0x000290B8
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, FolderType folderType, bool useUnicode, CreateFolderFlags flags, string displayName, string folderComment, StoreLongTermId? longTermId)
		{
			Util.ThrowOnNullArgument(displayName, "displayName");
			Util.ThrowOnNullArgument(folderComment, "folderComment");
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.folderType = folderType;
			this.useUnicode = useUnicode;
			this.flags = flags;
			this.displayName = String8.Create(displayName);
			this.folderComment = String8.Create(folderComment);
			this.longTermId = longTermId;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0002AF20 File Offset: 0x00029120
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.folderType);
			writer.WriteBool(this.useUnicode);
			writer.WriteByte((byte)this.flags);
			writer.WriteBool(this.longTermId != null);
			if (this.useUnicode)
			{
				writer.WriteUnicodeString(this.displayName.StringValue, StringFlags.IncludeNull);
				writer.WriteUnicodeString(this.folderComment.StringValue, StringFlags.IncludeNull);
			}
			else
			{
				writer.WriteString8(this.displayName.StringValue, string8Encoding, StringFlags.IncludeNull);
				writer.WriteString8(this.folderComment.StringValue, string8Encoding, StringFlags.IncludeNull);
			}
			if (this.longTermId != null)
			{
				this.longTermId.Value.Serialize(writer);
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0002AFE0 File Offset: 0x000291E0
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulCreateFolderResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002B00E File Offset: 0x0002920E
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopCreateFolder.resultFactory;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0002B018 File Offset: 0x00029218
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.folderType = (FolderType)reader.ReadByte();
			this.useUnicode = reader.ReadBool();
			CreateFolderFlags createFolderFlags = (CreateFolderFlags)reader.ReadByte();
			bool flag = reader.ReadBool();
			this.displayName = String8.Parse(reader, this.useUnicode, StringFlags.IncludeNull);
			this.folderComment = String8.Parse(reader, this.useUnicode, StringFlags.IncludeNull);
			if (flag)
			{
				this.longTermId = new StoreLongTermId?(StoreLongTermId.Parse(reader));
			}
			if ((createFolderFlags & CreateFolderFlags.ReservedForLegacySupport) != CreateFolderFlags.None)
			{
				this.flags = CreateFolderFlags.OpenIfExists;
				return;
			}
			this.flags = createFolderFlags;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0002B0A5 File Offset: 0x000292A5
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0002B0BA File Offset: 0x000292BA
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			this.displayName.ResolveString8Values(string8Encoding);
			this.folderComment.ResolveString8Values(string8Encoding);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0002B0DC File Offset: 0x000292DC
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.CreateFolder(serverObject, this.folderType, this.flags, this.displayName.StringValue, this.folderComment.StringValue, this.longTermId, RopCreateFolder.resultFactory);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0002B124 File Offset: 0x00029324
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Type=").Append(this.folderType);
			stringBuilder.Append(" Unicode=").Append(this.useUnicode);
			stringBuilder.Append(" Flags=[").Append(this.flags).Append("]");
			stringBuilder.Append(" Display Name=[").Append(this.displayName).Append("]");
			stringBuilder.Append(" Comment=[").Append(this.folderComment).Append("]");
			stringBuilder.Append(" LTID=[").Append(this.longTermId).Append("]");
		}

		// Token: 0x04000767 RID: 1895
		private const RopId RopType = RopId.CreateFolder;

		// Token: 0x04000768 RID: 1896
		private static CreateFolderResultFactory resultFactory = new CreateFolderResultFactory();

		// Token: 0x04000769 RID: 1897
		private FolderType folderType;

		// Token: 0x0400076A RID: 1898
		private bool useUnicode;

		// Token: 0x0400076B RID: 1899
		private CreateFolderFlags flags;

		// Token: 0x0400076C RID: 1900
		private String8 displayName;

		// Token: 0x0400076D RID: 1901
		private String8 folderComment;

		// Token: 0x0400076E RID: 1902
		private StoreLongTermId? longTermId;
	}
}
