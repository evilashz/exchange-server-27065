using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000302 RID: 770
	internal sealed class RopModifyPermissions : InputRop
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x000317A2 File Offset: 0x0002F9A2
		internal override RopId RopId
		{
			get
			{
				return RopId.ModifyPermissions;
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000317A6 File Offset: 0x0002F9A6
		internal static Rop CreateRop()
		{
			return new RopModifyPermissions();
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000317B0 File Offset: 0x0002F9B0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(string.Format(" ModifyPermissionsFlags={0}", this.modifyPermissionsFlags));
			stringBuilder.Append(" Permissions={");
			Util.AppendToString<ModifyTableRow>(stringBuilder, this.permissions);
			stringBuilder.Append("}");
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00031804 File Offset: 0x0002FA04
		internal void SetInput(byte logonIndex, byte handleTableIndex, ModifyPermissionsFlags modifyPermissionsFlags, ModifyTableRow[] permissions)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.modifyPermissionsFlags = modifyPermissionsFlags;
			this.permissions = permissions;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0003181D File Offset: 0x0002FA1D
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.modifyPermissionsFlags);
			writer.WriteSizedModifyTableRows(this.permissions, string8Encoding);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00031840 File Offset: 0x0002FA40
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0003186E File Offset: 0x0002FA6E
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopModifyPermissions.resultFactory;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00031875 File Offset: 0x0002FA75
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.modifyPermissionsFlags = (ModifyPermissionsFlags)reader.ReadByte();
			this.permissions = reader.ReadSizeAndModifyTableRowArray();
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00031897 File Offset: 0x0002FA97
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000318AC File Offset: 0x0002FAAC
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			for (int i = 0; i < this.permissions.Length; i++)
			{
				this.permissions[i].ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x000318E5 File Offset: 0x0002FAE5
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ModifyPermissions(serverObject, this.modifyPermissionsFlags, this.permissions, RopModifyPermissions.resultFactory);
		}

		// Token: 0x040009BA RID: 2490
		private const RopId RopType = RopId.ModifyPermissions;

		// Token: 0x040009BB RID: 2491
		private static ModifyPermissionsResultFactory resultFactory = new ModifyPermissionsResultFactory();

		// Token: 0x040009BC RID: 2492
		private ModifyPermissionsFlags modifyPermissionsFlags;

		// Token: 0x040009BD RID: 2493
		private ModifyTableRow[] permissions;
	}
}
