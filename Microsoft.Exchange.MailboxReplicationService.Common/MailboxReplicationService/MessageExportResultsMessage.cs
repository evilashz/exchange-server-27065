using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200025B RID: 603
	internal class MessageExportResultsMessage : DataMessageBase
	{
		// Token: 0x06001ECE RID: 7886 RVA: 0x0003FC68 File Offset: 0x0003DE68
		public MessageExportResultsMessage(List<MessageRec> missingMessages, List<BadMessageRec> badMessages)
		{
			this.MissingMessages = missingMessages;
			this.BadMessages = badMessages;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x0003FC80 File Offset: 0x0003DE80
		private MessageExportResultsMessage(bool useCompression, byte[] blob)
		{
			this.BadMessages = new List<BadMessageRec>();
			this.MissingMessages = new List<MessageRec>();
			string serializedXML = CommonUtils.UnpackString(blob, useCompression);
			MessageExportResults messageExportResults = XMLSerializableBase.Deserialize<MessageExportResults>(serializedXML, false);
			if (messageExportResults != null)
			{
				this.MissingMessages = messageExportResults.GetMissingMessages();
				this.BadMessages.AddRange(messageExportResults.BadMessages);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x0003FCDC File Offset: 0x0003DEDC
		public static DataMessageOpcode[] SupportedOpcodes
		{
			get
			{
				return new DataMessageOpcode[]
				{
					DataMessageOpcode.MessageExportResults
				};
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x0003FCF9 File Offset: 0x0003DEF9
		// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x0003FD01 File Offset: 0x0003DF01
		public List<MessageRec> MissingMessages { get; private set; }

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0003FD0A File Offset: 0x0003DF0A
		// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x0003FD12 File Offset: 0x0003DF12
		public List<BadMessageRec> BadMessages { get; private set; }

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0003FD1B File Offset: 0x0003DF1B
		public static IDataMessage Deserialize(DataMessageOpcode opcode, byte[] data, bool useCompression)
		{
			return new MessageExportResultsMessage(useCompression, data);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0003FD24 File Offset: 0x0003DF24
		protected override int GetSizeInternal()
		{
			int num = 0;
			foreach (MessageRec messageRec in this.MissingMessages)
			{
				if (messageRec.EntryId != null && messageRec.FolderId != null)
				{
					num += messageRec.EntryId.Length + messageRec.FolderId.Length;
				}
			}
			foreach (BadMessageRec badMessageRec in this.BadMessages)
			{
				if (badMessageRec.EntryId != null && badMessageRec.XmlData != null)
				{
					num += badMessageRec.EntryId.Length + badMessageRec.XmlData.Length;
				}
			}
			return num;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0003FDFC File Offset: 0x0003DFFC
		protected override void SerializeInternal(bool useCompression, out DataMessageOpcode opcode, out byte[] data)
		{
			opcode = DataMessageOpcode.MessageExportResults;
			MessageExportResults messageExportResults = new MessageExportResults(this.MissingMessages, this.BadMessages);
			string data2 = messageExportResults.Serialize(false);
			data = CommonUtils.PackString(data2, useCompression);
		}
	}
}
