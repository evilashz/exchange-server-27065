using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Connections.Imap;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000004 RID: 4
	internal sealed class ImapClientFolder
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002122 File Offset: 0x00000322
		public ImapClientFolder(ImapMailbox folder)
		{
			this.folder = folder;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002131 File Offset: 0x00000331
		public ImapClientFolder(string folderName)
		{
			this.folder = new ImapMailbox(folderName);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002145 File Offset: 0x00000345
		public string Name
		{
			get
			{
				return this.folder.Name;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002152 File Offset: 0x00000352
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000215F File Offset: 0x0000035F
		public bool IsSelectable
		{
			get
			{
				return this.folder.IsSelectable;
			}
			set
			{
				this.folder.IsSelectable = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000216D File Offset: 0x0000036D
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002175 File Offset: 0x00000375
		public ImapDefaultFolderType DefaultFolderType { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000217E File Offset: 0x0000037E
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002186 File Offset: 0x00000386
		public WellKnownFolderType WellKnownFolderType { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000218F File Offset: 0x0000038F
		public char? Separator
		{
			get
			{
				return this.folder.Separator;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000219C File Offset: 0x0000039C
		public uint UidNext
		{
			get
			{
				return (uint)this.folder.UidNext.Value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021C0 File Offset: 0x000003C0
		public uint UidValidity
		{
			get
			{
				return (uint)this.folder.UidValidity.Value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021E1 File Offset: 0x000003E1
		public int? NumberOfMessages
		{
			get
			{
				return this.folder.NumberOfMessages;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021EE File Offset: 0x000003EE
		public string ParentFolderPath
		{
			get
			{
				return this.folder.ParentFolderPath;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021FB File Offset: 0x000003FB
		public string ShortFolderName
		{
			get
			{
				return this.folder.ShortFolderName;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002208 File Offset: 0x00000408
		public ImapMailFlags SupportedFlags
		{
			get
			{
				return this.folder.PermanentFlags;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002218 File Offset: 0x00000418
		public static void FindWellKnownFolders(List<ImapClientFolder> folders)
		{
			Dictionary<ImapDefaultFolderType, ImapClientFolder> dictionary = new Dictionary<ImapDefaultFolderType, ImapClientFolder>();
			foreach (ImapClientFolder imapClientFolder in folders)
			{
				bool flag;
				bool flag2;
				imapClientFolder.DefaultFolderType = ImapClientFolder.GetDefaultFolderType(imapClientFolder.Name, out flag, out flag2);
				if (imapClientFolder.DefaultFolderType != ImapDefaultFolderType.None && (flag2 || flag) && !dictionary.ContainsKey(imapClientFolder.DefaultFolderType))
				{
					dictionary.Add(imapClientFolder.DefaultFolderType, imapClientFolder);
				}
			}
			foreach (ImapClientFolder imapClientFolder2 in folders)
			{
				imapClientFolder2.WellKnownFolderType = WellKnownFolderType.None;
				if (imapClientFolder2.DefaultFolderType != ImapDefaultFolderType.None && !dictionary.ContainsKey(imapClientFolder2.DefaultFolderType))
				{
					dictionary.Add(imapClientFolder2.DefaultFolderType, imapClientFolder2);
				}
			}
			foreach (KeyValuePair<ImapDefaultFolderType, ImapClientFolder> keyValuePair in dictionary)
			{
				WellKnownFolderType wellKnownFolderType;
				if (ImapClientFolder.DefaultToWellKnownFolderMapping.TryGetValue(keyValuePair.Key, out wellKnownFolderType))
				{
					ImapClientFolder value = keyValuePair.Value;
					value.WellKnownFolderType = wellKnownFolderType;
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000236C File Offset: 0x0000056C
		public void SelectImapFolder(ImapConnection imapConnection)
		{
			imapConnection.SelectImapMailbox(this.folder);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000237C File Offset: 0x0000057C
		public List<ImapMessageRec> LookupMessages(ImapConnection imapConnection, List<uint> uidList)
		{
			this.SelectImapFolder(imapConnection);
			LookupMessagesParams lookupParams = new LookupMessagesParams(uidList);
			IEnumerable<ImapMessageRec> collection = this.LookupMessagesInfoFromImapServer(imapConnection, lookupParams);
			List<ImapMessageRec> list = new List<ImapMessageRec>(collection);
			list.Sort();
			return list;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B0 File Offset: 0x000005B0
		public List<ImapMessageRec> EnumerateMessages(ImapConnection imapConnection, FetchMessagesFlags flags, int? highFetchValue = null, int? lowFetchValue = null)
		{
			ImapMailbox imapMailbox = imapConnection.SelectImapMailbox(this.folder);
			int? numberOfMessages = imapMailbox.NumberOfMessages;
			if (numberOfMessages == null || numberOfMessages.Value == 0)
			{
				MrsTracer.Provider.Debug("Imap folder {0} does not have any messages to be enumerated", new object[]
				{
					this.Name
				});
				return new List<ImapMessageRec>(0);
			}
			int highFetchValue2 = highFetchValue ?? numberOfMessages.Value;
			int lowFetchValue2 = lowFetchValue ?? 1;
			EnumerateMessagesParams enumerateParams = new EnumerateMessagesParams(highFetchValue2, lowFetchValue2, flags);
			IEnumerable<ImapMessageRec> collection = this.EnumerateMessagesInfoFromImapServer(imapConnection, enumerateParams);
			List<ImapMessageRec> list = new List<ImapMessageRec>(collection);
			list.Sort();
			return list;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002469 File Offset: 0x00000669
		private static ImapDefaultFolderType GetDefaultFolderType(string folderName, out bool preferredMapping, out bool exactCaseSensitiveMatch)
		{
			return ImapMailbox.GetDefaultFolderType(folderName, out preferredMapping, out exactCaseSensitiveMatch);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002474 File Offset: 0x00000674
		private IEnumerable<ImapMessageRec> EnumerateMessagesInfoFromImapServer(ImapConnection imapConnection, EnumerateMessagesParams enumerateParams)
		{
			if (enumerateParams.LowFetchValue > enumerateParams.HighFetchValue)
			{
				return new List<ImapMessageRec>(0);
			}
			ImapResultData messageInfoByRange = imapConnection.GetMessageInfoByRange(enumerateParams.LowFetchValue.ToString(CultureInfo.InvariantCulture), enumerateParams.HighFetchValue.ToString(CultureInfo.InvariantCulture), enumerateParams.FetchMessagesFlags.HasFlag(FetchMessagesFlags.FetchByUid), enumerateParams.FetchMessagesFlags.HasFlag(FetchMessagesFlags.IncludeExtendedData) ? ImapConnection.MessageInfoDataItemsForNewMessages : ImapConnection.MessageInfoDataItemsForChangesOnly);
			return this.GetImapMessageRecsFromResultData(messageInfoByRange, enumerateParams.FetchMessagesFlags);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000250C File Offset: 0x0000070C
		private IEnumerable<ImapMessageRec> LookupMessagesInfoFromImapServer(ImapConnection imapConnection, LookupMessagesParams lookupParams)
		{
			ImapResultData messageInfoByRange = imapConnection.GetMessageInfoByRange(lookupParams.GetUidFetchString(), null, lookupParams.FetchMessagesFlags.HasFlag(FetchMessagesFlags.FetchByUid), lookupParams.FetchMessagesFlags.HasFlag(FetchMessagesFlags.IncludeExtendedData) ? ImapConnection.MessageInfoDataItemsForNewMessages : ImapConnection.MessageInfoDataItemsForChangesOnly);
			return this.GetImapMessageRecsFromResultData(messageInfoByRange, lookupParams.FetchMessagesFlags);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002570 File Offset: 0x00000770
		private IEnumerable<ImapMessageRec> GetImapMessageRecsFromResultData(ImapResultData resultData, FetchMessagesFlags fetchFlags)
		{
			IList<string> messageUids = resultData.MessageUids;
			IList<ImapMailFlags> messageFlags = resultData.MessageFlags;
			bool flag = messageUids != null && messageUids.Count > 0 && messageFlags != null && messageFlags.Count > 0;
			if (!flag)
			{
				return new List<ImapMessageRec>(0);
			}
			if (fetchFlags.HasFlag(FetchMessagesFlags.IncludeExtendedData))
			{
				return ImapExtendedMessageRec.FromImapResultData(this.folder, resultData);
			}
			return ImapMessageRec.FromImapResultData(resultData);
		}

		// Token: 0x0400000A RID: 10
		public static readonly Dictionary<ImapDefaultFolderType, WellKnownFolderType> DefaultToWellKnownFolderMapping = new Dictionary<ImapDefaultFolderType, WellKnownFolderType>
		{
			{
				ImapDefaultFolderType.Inbox,
				WellKnownFolderType.Inbox
			},
			{
				ImapDefaultFolderType.DeletedItems,
				WellKnownFolderType.DeletedItems
			},
			{
				ImapDefaultFolderType.Drafts,
				WellKnownFolderType.Drafts
			},
			{
				ImapDefaultFolderType.SentItems,
				WellKnownFolderType.SentItems
			},
			{
				ImapDefaultFolderType.JunkEmail,
				WellKnownFolderType.JunkEmail
			}
		};

		// Token: 0x0400000B RID: 11
		private readonly ImapMailbox folder;
	}
}
