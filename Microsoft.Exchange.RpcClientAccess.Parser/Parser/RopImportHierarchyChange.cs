using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002E7 RID: 743
	internal sealed class RopImportHierarchyChange : InputRop
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0002FC4A File Offset: 0x0002DE4A
		internal override RopId RopId
		{
			get
			{
				return RopId.ImportHierarchyChange;
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0002FC4E File Offset: 0x0002DE4E
		internal static Rop CreateRop()
		{
			return new RopImportHierarchyChange();
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0002FC55 File Offset: 0x0002DE55
		internal void SetInput(byte logonIndex, byte handleIndex, PropertyValue[] hierarchyPropertyValues, PropertyValue[] folderPropertyValues)
		{
			base.SetCommonInput(logonIndex, handleIndex);
			this.hierarchyPropertyValues = hierarchyPropertyValues;
			this.folderPropertyValues = folderPropertyValues;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0002FC6E File Offset: 0x0002DE6E
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteCountAndPropertyValueList(this.hierarchyPropertyValues, string8Encoding, WireFormatStyle.Rop);
			writer.WriteCountAndPropertyValueList(this.folderPropertyValues, string8Encoding, WireFormatStyle.Rop);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0002FC94 File Offset: 0x0002DE94
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulImportHierarchyChangeResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0002FCC2 File Offset: 0x0002DEC2
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ImportHierarchyChange(serverObject, this.hierarchyPropertyValues, this.folderPropertyValues, RopImportHierarchyChange.resultFactory);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0002FCE2 File Offset: 0x0002DEE2
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopImportHierarchyChange.resultFactory;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0002FCE9 File Offset: 0x0002DEE9
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.hierarchyPropertyValues = reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop);
			this.folderPropertyValues = reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0002FD0D File Offset: 0x0002DF0D
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0002FD24 File Offset: 0x0002DF24
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			foreach (PropertyValue propertyValue in this.hierarchyPropertyValues)
			{
				propertyValue.ResolveString8Values(string8Encoding);
			}
			foreach (PropertyValue propertyValue2 in this.folderPropertyValues)
			{
				propertyValue2.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0002FD94 File Offset: 0x0002DF94
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" HeaderProps=[");
			Util.AppendToString<PropertyValue>(stringBuilder, this.hierarchyPropertyValues);
			stringBuilder.Append("]");
			stringBuilder.Append(" FolderProps=[");
			Util.AppendToString<PropertyValue>(stringBuilder, this.folderPropertyValues);
			stringBuilder.Append("]");
		}

		// Token: 0x0400092B RID: 2347
		private const RopId RopType = RopId.ImportHierarchyChange;

		// Token: 0x0400092C RID: 2348
		private static ImportHierarchyChangeResultFactory resultFactory = new ImportHierarchyChangeResultFactory();

		// Token: 0x0400092D RID: 2349
		private PropertyValue[] hierarchyPropertyValues;

		// Token: 0x0400092E RID: 2350
		private PropertyValue[] folderPropertyValues;
	}
}
