using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Pop;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000004 RID: 4
	public sealed class PopFolderState : XMLSerializableBase
	{
		// Token: 0x0600001F RID: 31 RVA: 0x0000246E File Offset: 0x0000066E
		public PopFolderState()
		{
			this.messageList = new PopBookmark(string.Empty);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002486 File Offset: 0x00000686
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000248E File Offset: 0x0000068E
		[XmlElement(ElementName = "MessageListString")]
		public string MessageListString { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002497 File Offset: 0x00000697
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000249F File Offset: 0x0000069F
		[XmlIgnore]
		internal PopBookmark MessageList
		{
			get
			{
				return this.messageList;
			}
			private set
			{
				this.messageList = value;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024A8 File Offset: 0x000006A8
		public static PopFolderState Deserialize(byte[] compressedXml)
		{
			byte[] bytes = CommonUtils.DecompressData(compressedXml);
			string @string = Encoding.UTF7.GetString(bytes);
			if (string.IsNullOrEmpty(@string))
			{
				throw new CorruptSyncStateException(new ArgumentNullException("data", "Cannot deserialize null or empty data"));
			}
			PopFolderState popFolderState = XMLSerializableBase.Deserialize<PopFolderState>(@string, true);
			popFolderState.MessageList = PopBookmark.Parse(popFolderState.MessageListString);
			return popFolderState;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024FF File Offset: 0x000006FF
		public byte[] Serialize()
		{
			this.MessageListString = this.MessageList.ToString();
			return CommonUtils.CompressData(Encoding.UTF7.GetBytes(base.Serialize(false)));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002528 File Offset: 0x00000728
		internal static PopFolderState CreateNew()
		{
			return new PopFolderState();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002530 File Offset: 0x00000730
		internal static PopFolderState Create(List<MessageRec> messages)
		{
			PopFolderState popFolderState = new PopFolderState();
			foreach (MessageRec messageRec in messages)
			{
				popFolderState.MessageList.Add(Encoding.UTF8.GetString(messageRec.EntryId));
			}
			return popFolderState;
		}

		// Token: 0x0400000B RID: 11
		private PopBookmark messageList;
	}
}
