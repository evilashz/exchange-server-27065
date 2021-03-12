using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000299 RID: 665
	internal abstract class RopDeleteMessagesBase : InputRop
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0002B70A File Offset: 0x0002990A
		protected bool ReportProgress
		{
			get
			{
				return this.reportProgress;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0002B712 File Offset: 0x00029912
		protected bool IsOkToSendNonReadNotification
		{
			get
			{
				return this.isOkToSendNonReadNotification;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002B71A File Offset: 0x0002991A
		protected StoreId[] MessageIds
		{
			get
			{
				return this.messageIds;
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0002B722 File Offset: 0x00029922
		internal void SetInput(byte logonIndex, byte handleTableIndex, bool reportProgress, bool isOkToSendNonReadNotification, StoreId[] messageIds)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.reportProgress = reportProgress;
			this.isOkToSendNonReadNotification = isOkToSendNonReadNotification;
			this.messageIds = messageIds;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0002B743 File Offset: 0x00029943
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool(this.reportProgress);
			writer.WriteBool(this.isOkToSendNonReadNotification);
			writer.WriteCountedStoreIds(this.messageIds);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0002B771 File Offset: 0x00029971
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.reportProgress = reader.ReadBool();
			this.isOkToSendNonReadNotification = reader.ReadBool();
			this.messageIds = reader.ReadSizeAndStoreIdArray();
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002B7A0 File Offset: 0x000299A0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Progress=").Append(this.ReportProgress);
			stringBuilder.Append(" SendNRN=").Append(this.IsOkToSendNonReadNotification);
			stringBuilder.Append(" MIDs=[");
			Util.AppendToString<StoreId>(stringBuilder, this.MessageIds);
			stringBuilder.Append("]");
		}

		// Token: 0x04000786 RID: 1926
		private bool reportProgress;

		// Token: 0x04000787 RID: 1927
		private bool isOkToSendNonReadNotification;

		// Token: 0x04000788 RID: 1928
		private StoreId[] messageIds;
	}
}
