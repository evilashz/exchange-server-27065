using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000323 RID: 803
	internal sealed class RopRegisterSynchronizationNotifications : InputRop
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x000339D3 File Offset: 0x00031BD3
		internal override RopId RopId
		{
			get
			{
				return RopId.RegisterSynchronizationNotifications;
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000339DA File Offset: 0x00031BDA
		internal static Rop CreateRop()
		{
			return new RopRegisterSynchronizationNotifications();
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000339E4 File Offset: 0x00031BE4
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId[] folderIds, uint[] folderChangeNumbers)
		{
			Util.ThrowOnNullArgument(folderIds, "folderIds");
			Util.ThrowOnNullArgument(folderChangeNumbers, "folderChangeNumbers");
			if (folderIds.Length != folderChangeNumbers.Length)
			{
				throw new ArgumentException("Number of elements in list of Folder Ids and list of change numbers do not match");
			}
			if (folderIds.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("folderIds.Length", folderIds.Length, "Number of elements in list of Folder Ids exceeds the maximum allowed.");
			}
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.folderIds = folderIds;
			this.folderChangeNumbers = folderChangeNumbers;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00033A58 File Offset: 0x00031C58
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16((ushort)this.folderIds.Length);
			for (int i = 0; i < this.folderIds.Length; i++)
			{
				this.folderIds[i].Serialize(writer);
			}
			for (int j = 0; j < this.folderChangeNumbers.Length; j++)
			{
				writer.WriteUInt32(this.folderChangeNumbers[j]);
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00033AC2 File Offset: 0x00031CC2
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00033AF0 File Offset: 0x00031CF0
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopRegisterSynchronizationNotifications.resultFactory;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00033AF8 File Offset: 0x00031CF8
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			ushort num = reader.ReadUInt16();
			if (num == 0)
			{
				this.folderIds = Array<StoreId>.Empty;
				this.folderChangeNumbers = Array<uint>.Empty;
				return;
			}
			reader.CheckBoundary((uint)num, 8U);
			this.folderIds = new StoreId[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				this.folderIds[i] = StoreId.Parse(reader);
			}
			reader.CheckBoundary((uint)num, 4U);
			this.folderChangeNumbers = new uint[(int)num];
			for (int j = 0; j < (int)num; j++)
			{
				this.folderChangeNumbers[j] = reader.ReadUInt32();
			}
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00033B93 File Offset: 0x00031D93
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00033BA8 File Offset: 0x00031DA8
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.RegisterSynchronizationNotifications(serverObject, this.folderIds, this.folderChangeNumbers, RopRegisterSynchronizationNotifications.resultFactory);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00033BC8 File Offset: 0x00031DC8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			if (this.folderIds != null)
			{
				stringBuilder.Append(" folderIds=[");
				Util.AppendToString<StoreId>(stringBuilder, this.folderIds);
				stringBuilder.Append("]");
			}
			if (this.folderChangeNumbers != null)
			{
				stringBuilder.Append(" folderChangeNumbers=[");
				Util.AppendToString<uint>(stringBuilder, this.folderChangeNumbers);
				stringBuilder.Append("]");
			}
		}

		// Token: 0x04000A3A RID: 2618
		private const RopId RopType = RopId.RegisterSynchronizationNotifications;

		// Token: 0x04000A3B RID: 2619
		private static RegisterSynchronizationNotificationsResultFactory resultFactory = new RegisterSynchronizationNotificationsResultFactory();

		// Token: 0x04000A3C RID: 2620
		private StoreId[] folderIds;

		// Token: 0x04000A3D RID: 2621
		private uint[] folderChangeNumbers;
	}
}
