using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002CF RID: 719
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RopGetPerUserGuid : InputRop
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0002EA2E File Offset: 0x0002CC2E
		internal override RopId RopId
		{
			get
			{
				return RopId.GetPerUserGuid;
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0002EA32 File Offset: 0x0002CC32
		internal static Rop CreateRop()
		{
			return new RopGetPerUserGuid();
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0002EA39 File Offset: 0x0002CC39
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreLongTermId publicFolderLongTermId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.publicFolderLongTermId = publicFolderLongTermId;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0002EA4A File Offset: 0x0002CC4A
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.publicFolderLongTermId.Serialize(writer);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0002EA60 File Offset: 0x0002CC60
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetPerUserGuidResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0002EA8E File Offset: 0x0002CC8E
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetPerUserGuid.resultFactory;
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0002EA95 File Offset: 0x0002CC95
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.publicFolderLongTermId = StoreLongTermId.Parse(reader);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0002EAAB File Offset: 0x0002CCAB
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetPerUserGuid(serverObject, this.publicFolderLongTermId, RopGetPerUserGuid.resultFactory);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0002EADA File Offset: 0x0002CCDA
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" PublicFolder=").Append(this.publicFolderLongTermId);
		}

		// Token: 0x0400082F RID: 2095
		private const RopId RopType = RopId.GetPerUserGuid;

		// Token: 0x04000830 RID: 2096
		private static readonly GetPerUserGuidResultFactory resultFactory = new GetPerUserGuidResultFactory();

		// Token: 0x04000831 RID: 2097
		private StoreLongTermId publicFolderLongTermId;
	}
}
