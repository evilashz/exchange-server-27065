using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000362 RID: 866
	internal sealed class RopWritePerUserInformation : InputRop
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00036D05 File Offset: 0x00034F05
		internal override RopId RopId
		{
			get
			{
				return RopId.WritePerUserInformation;
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00036D09 File Offset: 0x00034F09
		internal static Rop CreateRop()
		{
			return new RopWritePerUserInformation();
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00036D10 File Offset: 0x00034F10
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" LTID=[").Append(this.longTermId).Append("]");
			stringBuilder.Append(" HasFinished=").Append(this.hasFinished);
			stringBuilder.Append(" DataOffset=").Append(this.dataOffset);
			if (this.data != null)
			{
				stringBuilder.Append(" Data=[");
				Util.AppendToString(stringBuilder, this.data);
				stringBuilder.Append("]");
			}
			if (this.replicaGuid != null)
			{
				stringBuilder.Append(" ReplicaGuid=").Append(this.replicaGuid.Value);
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00036DD2 File Offset: 0x00034FD2
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreLongTermId longTermId, bool hasFinished, uint dataOffset, byte[] data, Guid? replicaGuid)
		{
			Util.ThrowOnNullArgument(data, "data");
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.longTermId = longTermId;
			this.hasFinished = hasFinished;
			this.dataOffset = dataOffset;
			this.data = data;
			this.replicaGuid = replicaGuid;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00036E10 File Offset: 0x00035010
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.longTermId.Serialize(writer);
			writer.WriteBool(this.hasFinished);
			writer.WriteUInt32(this.dataOffset);
			writer.WriteSizedBytes(this.data);
			if (this.replicaGuid != null)
			{
				writer.WriteGuid(this.replicaGuid.Value);
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00036E73 File Offset: 0x00035073
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00036EA1 File Offset: 0x000350A1
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopWritePerUserInformation.resultFactory;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00036EA8 File Offset: 0x000350A8
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			this.longTermId = StoreLongTermId.Parse(reader);
			this.hasFinished = reader.ReadBool();
			this.dataOffset = reader.ReadUInt32();
			this.data = reader.ReadSizeAndByteArray();
			if (this.dataOffset == 0U && !logonTracker.ParseIsPublicLogon(base.LogonIndex))
			{
				this.replicaGuid = new Guid?(reader.ReadGuid());
				return;
			}
			this.replicaGuid = null;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x00036F22 File Offset: 0x00035122
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00036F38 File Offset: 0x00035138
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.WritePerUserInformation(serverObject, this.longTermId, this.hasFinished, this.dataOffset, this.data, this.replicaGuid, RopWritePerUserInformation.resultFactory);
		}

		// Token: 0x04000B1B RID: 2843
		private const RopId RopType = RopId.WritePerUserInformation;

		// Token: 0x04000B1C RID: 2844
		private static WritePerUserInformationResultFactory resultFactory = new WritePerUserInformationResultFactory();

		// Token: 0x04000B1D RID: 2845
		private StoreLongTermId longTermId;

		// Token: 0x04000B1E RID: 2846
		private bool hasFinished;

		// Token: 0x04000B1F RID: 2847
		private uint dataOffset;

		// Token: 0x04000B20 RID: 2848
		private byte[] data;

		// Token: 0x04000B21 RID: 2849
		private Guid? replicaGuid;
	}
}
