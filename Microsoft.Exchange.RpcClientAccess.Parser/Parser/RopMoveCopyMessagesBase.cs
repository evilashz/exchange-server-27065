using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000306 RID: 774
	internal abstract class RopMoveCopyMessagesBase : DualInputRop
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x00031A8D File Offset: 0x0002FC8D
		protected bool ReportProgress
		{
			get
			{
				return this.reportProgress;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00031A95 File Offset: 0x0002FC95
		protected bool IsCopy
		{
			get
			{
				return this.isCopy;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00031A9D File Offset: 0x0002FC9D
		protected StoreId[] MessageIds
		{
			get
			{
				return this.messageIds;
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00031AA5 File Offset: 0x0002FCA5
		internal void SetInput(byte logonIndex, byte sourceHandleTableIndex, byte destinationHandleTableIndex, StoreId[] messageIds, bool reportProgress, bool isCopy)
		{
			base.SetCommonInput(logonIndex, sourceHandleTableIndex, destinationHandleTableIndex);
			this.messageIds = messageIds;
			this.reportProgress = reportProgress;
			this.isCopy = isCopy;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00031AC8 File Offset: 0x0002FCC8
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteCountedStoreIds(this.messageIds);
			writer.WriteBool(this.reportProgress);
			writer.WriteBool(this.isCopy);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00031AF6 File Offset: 0x0002FCF6
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messageIds = reader.ReadSizeAndStoreIdArray();
			this.reportProgress = reader.ReadBool();
			this.isCopy = reader.ReadBool();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00031B24 File Offset: 0x0002FD24
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" IsCopy=").Append(this.IsCopy);
			stringBuilder.Append(" Progress=").Append(this.ReportProgress);
			stringBuilder.Append(" Mids=[");
			Util.AppendToString<StoreId>(stringBuilder, this.messageIds);
			stringBuilder.Append("]");
		}

		// Token: 0x040009CD RID: 2509
		private StoreId[] messageIds;

		// Token: 0x040009CE RID: 2510
		private bool reportProgress;

		// Token: 0x040009CF RID: 2511
		private bool isCopy;
	}
}
