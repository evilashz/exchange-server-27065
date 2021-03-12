using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000009 RID: 9
	public sealed class ImapFolderState : XMLSerializableBase
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002FB8 File Offset: 0x000011B8
		public ImapFolderState()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002FC0 File Offset: 0x000011C0
		internal ImapFolderState(int seqNumCrawl, uint uidNext, uint uidValidity, GlobCountSet uidReadSet, GlobCountSet uidDeletedSet)
		{
			this.SeqNumCrawl = seqNumCrawl;
			this.UidNext = uidNext;
			this.UidValidity = uidValidity;
			this.uidReadSet = uidReadSet;
			this.uidDeletedSet = uidDeletedSet;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002FED File Offset: 0x000011ED
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002FF5 File Offset: 0x000011F5
		[XmlElement(ElementName = "SeqNumCrawl")]
		public int SeqNumCrawl { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002FFE File Offset: 0x000011FE
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003006 File Offset: 0x00001206
		[XmlElement(ElementName = "UidNext")]
		public uint UidNext { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000300F File Offset: 0x0000120F
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003017 File Offset: 0x00001217
		[XmlElement(ElementName = "UidValidity")]
		public uint UidValidity { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003020 File Offset: 0x00001220
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000302D File Offset: 0x0000122D
		[XmlElement(ElementName = "UidSetRead")]
		public byte[] UidSetRead
		{
			get
			{
				return ImapFolderState.SerializeUidSet(this.uidReadSet);
			}
			set
			{
				this.uidReadSet = ImapFolderState.ParseUidSet(value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000303B File Offset: 0x0000123B
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003048 File Offset: 0x00001248
		[XmlElement(ElementName = "UidSetDeleted")]
		public byte[] UidSetDeleted
		{
			get
			{
				return ImapFolderState.SerializeUidSet(this.uidDeletedSet);
			}
			set
			{
				this.uidDeletedSet = ImapFolderState.ParseUidSet(value);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003058 File Offset: 0x00001258
		public static ImapFolderState Deserialize(byte[] compressedXml)
		{
			byte[] bytes = CommonUtils.DecompressData(compressedXml);
			string @string = Encoding.UTF7.GetString(bytes);
			if (string.IsNullOrEmpty(@string))
			{
				throw new CorruptSyncStateException(new ArgumentNullException("data", "Cannot deserialize null or empty data"));
			}
			return XMLSerializableBase.Deserialize<ImapFolderState>(@string, true);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000309C File Offset: 0x0000129C
		public byte[] Serialize()
		{
			return CommonUtils.CompressData(Encoding.UTF7.GetBytes(base.Serialize(false)));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000030B4 File Offset: 0x000012B4
		internal static ImapFolderState CreateNew(ImapClientFolder folder)
		{
			return new ImapFolderState
			{
				SeqNumCrawl = int.MaxValue,
				UidNext = folder.UidNext,
				UidValidity = folder.UidValidity
			};
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000030EC File Offset: 0x000012EC
		internal static ImapFolderState Create(List<MessageRec> messages, int seqNumCrawl, uint uidNext, uint uidValidity)
		{
			if (messages.Count == 0)
			{
				return new ImapFolderState
				{
					SeqNumCrawl = seqNumCrawl,
					UidNext = uidNext,
					UidValidity = uidValidity
				};
			}
			Dictionary<uint, MessageRec> dictionary = new Dictionary<uint, MessageRec>();
			foreach (MessageRec messageRec in messages)
			{
				uint key = ImapEntryId.ParseUid(messageRec.EntryId);
				dictionary.Add(key, messageRec);
			}
			GlobCountSet globCountSet = new GlobCountSet();
			GlobCountSet globCountSet2 = new GlobCountSet();
			for (uint num = uidNext - 1U; num > 0U; num -= 1U)
			{
				MessageRec messageRec2 = null;
				if (!dictionary.TryGetValue(num, out messageRec2))
				{
					globCountSet2.Insert((ulong)num);
				}
				else
				{
					MessageFlags messageFlags = (MessageFlags)((int)messageRec2[PropTag.MessageFlags]);
					if (messageFlags.HasFlag(MessageFlags.Read))
					{
						globCountSet.Insert((ulong)num);
					}
				}
			}
			return new ImapFolderState(seqNumCrawl, uidNext, uidValidity, globCountSet, globCountSet2);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000031F0 File Offset: 0x000013F0
		internal static void EnumerateReadUnreadFlagChanges(ImapFolderState sourceState, ImapFolderState syncState, Action<uint> uidInclusionAction, Action<uint> uidExclusionAction)
		{
			ImapFolderState.EnumerateChanges(sourceState.uidReadSet, syncState.uidReadSet, syncState.UidNext, uidInclusionAction, uidExclusionAction);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000320B File Offset: 0x0000140B
		internal static void EnumerateMessageDeletes(ImapFolderState sourceState, ImapFolderState syncState, Action<uint> uidInclusionAction, Action<uint> uidExclusionAction)
		{
			ImapFolderState.EnumerateChanges(sourceState.uidDeletedSet, syncState.uidDeletedSet, syncState.UidNext, uidInclusionAction, uidExclusionAction);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003228 File Offset: 0x00001428
		private static void EnumerateChanges(GlobCountSet sourceUidSet, GlobCountSet targetUidSet, uint uidNext, Action<uint> uidInclusionAction, Action<uint> uidExclusionAction)
		{
			uint highestUid = uidNext - 1U;
			if (sourceUidSet == null || targetUidSet == null)
			{
				return;
			}
			if (sourceUidSet.IsEmpty && targetUidSet.IsEmpty)
			{
				return;
			}
			if (sourceUidSet.IsEmpty)
			{
				ImapFolderState.PerformAction(targetUidSet, highestUid, uidExclusionAction);
				return;
			}
			if (targetUidSet.IsEmpty)
			{
				ImapFolderState.PerformAction(sourceUidSet, highestUid, uidInclusionAction);
				return;
			}
			GlobCountSet globCountSet = GlobCountSet.Subtract(sourceUidSet, targetUidSet);
			GlobCountSet globCountSet2 = GlobCountSet.Subtract(targetUidSet, sourceUidSet);
			if (globCountSet != null && !globCountSet.IsEmpty)
			{
				ImapFolderState.PerformAction(globCountSet, highestUid, uidInclusionAction);
			}
			if (globCountSet2 != null && !globCountSet2.IsEmpty)
			{
				ImapFolderState.PerformAction(globCountSet2, highestUid, uidExclusionAction);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000032AC File Offset: 0x000014AC
		private static void PerformAction(GlobCountSet uidSet, uint highestUid, Action<uint> action)
		{
			foreach (GlobCountRange globCountRange in uidSet)
			{
				uint num = (uint)globCountRange.LowBound;
				while (num <= (uint)globCountRange.HighBound && num <= highestUid)
				{
					action(num);
					num += 1U;
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003314 File Offset: 0x00001514
		private static GlobCountSet ParseUidSet(byte[] input)
		{
			if (input == null)
			{
				return null;
			}
			GlobCountSet result;
			using (BufferReader bufferReader = Reader.CreateBufferReader(input))
			{
				try
				{
					result = GlobCountSet.Parse(bufferReader);
				}
				catch (BufferParseException innerException)
				{
					throw new CorruptSyncStateException(innerException);
				}
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003368 File Offset: 0x00001568
		private static byte[] SerializeUidSet(GlobCountSet uidSet)
		{
			if (uidSet == null)
			{
				return null;
			}
			return BufferWriter.Serialize(new BufferWriter.SerializeDelegate(uidSet.Serialize));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003380 File Offset: 0x00001580
		[Conditional("DEBUG")]
		private static void ValidateEnumerationInputs(ImapFolderState sourceState, ImapFolderState syncState, Action<uint> uidInclusionAction, Action<uint> uidExclusionAction)
		{
		}

		// Token: 0x04000022 RID: 34
		internal const int Recrawl = 2147483647;

		// Token: 0x04000023 RID: 35
		internal const int CrawlCompleted = 0;

		// Token: 0x04000024 RID: 36
		private GlobCountSet uidReadSet;

		// Token: 0x04000025 RID: 37
		private GlobCountSet uidDeletedSet;
	}
}
