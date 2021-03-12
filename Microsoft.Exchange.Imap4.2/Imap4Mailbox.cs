using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Imap4;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000016 RID: 22
	internal sealed class Imap4Mailbox : DisposeTrackableBase
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00008174 File Offset: 0x00006374
		private Imap4Mailbox(Imap4ResponseFactory factory, Imap4Mailbox parentMailbox, object[] data)
		{
			this.factory = factory;
			this.messages = new List<Imap4Message>(100);
			this.LoadMailboxProperties(data);
			this.listFlags = this.GetFoldersListFlags(data);
			this.previousXsoVersion = ResponseFactory.GetPreviousXsoVersion(data[11] as string);
			this.ParentMailbox = parentMailbox;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000081E1 File Offset: 0x000063E1
		internal int[] PreviousXsoVersion
		{
			get
			{
				return this.previousXsoVersion;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000081E9 File Offset: 0x000063E9
		internal StoreObjectId ParentMailboxUid
		{
			get
			{
				return this.parentMailboxUid;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000081F1 File Offset: 0x000063F1
		internal Imap4ResponseFactory Factory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000081F9 File Offset: 0x000063F9
		internal Imap4DataAccessViewHandler DataAccessView
		{
			get
			{
				return this.dataAccessView;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00008201 File Offset: 0x00006401
		internal Imap4FastQueryViewHandler FastQueryView
		{
			get
			{
				return this.fastQueryView;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00008209 File Offset: 0x00006409
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00008211 File Offset: 0x00006411
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000821A File Offset: 0x0000641A
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00008222 File Offset: 0x00006422
		internal string FullPath
		{
			get
			{
				return this.fullPath;
			}
			set
			{
				this.fullPath = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000822B File Offset: 0x0000642B
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00008234 File Offset: 0x00006434
		internal Imap4Mailbox ParentMailbox
		{
			get
			{
				return this.parentMailbox;
			}
			set
			{
				this.parentMailbox = value;
				if (this.parentMailbox != null)
				{
					if (this.parentMailbox.IsRoot)
					{
						this.fullPath = this.name;
					}
					else
					{
						this.fullPath = this.parentMailbox.FullPath + '/' + this.name;
					}
					this.parentMailboxUid = this.parentMailbox.Uid;
					return;
				}
				this.fullPath = this.name;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000082AC File Offset: 0x000064AC
		internal StoreObjectId Uid
		{
			get
			{
				return this.uid;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000082B4 File Offset: 0x000064B4
		internal bool HasChildren
		{
			get
			{
				return this.hasChildren;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000082BC File Offset: 0x000064BC
		internal string ListFlags
		{
			get
			{
				return this.listFlags;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000082C4 File Offset: 0x000064C4
		internal Imap4Flags Flags
		{
			get
			{
				return Imap4Mailbox.flags;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000082CB File Offset: 0x000064CB
		internal Imap4Flags PermanentFlags
		{
			get
			{
				return this.permanentFlags;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000082D3 File Offset: 0x000064D3
		internal bool IsHidden
		{
			get
			{
				return this.isHidden || this.name == null;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000082E8 File Offset: 0x000064E8
		internal int Recent
		{
			get
			{
				return this.recent;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000082F0 File Offset: 0x000064F0
		internal int Exists
		{
			get
			{
				return this.messages.Count - this.deleted;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00008304 File Offset: 0x00006504
		internal List<Imap4Message> Messages
		{
			get
			{
				return this.messages;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000830C File Offset: 0x0000650C
		internal int MessagesTotal
		{
			get
			{
				return this.messages.Count;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00008319 File Offset: 0x00006519
		internal int FirstUnseenIndex
		{
			get
			{
				return this.firstUnseenIndex;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00008321 File Offset: 0x00006521
		internal int LastSeenArticleId
		{
			get
			{
				return this.lastSeenArticleId;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00008329 File Offset: 0x00006529
		internal int Unseen
		{
			get
			{
				return this.unseen;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00008331 File Offset: 0x00006531
		internal int UidValidity
		{
			get
			{
				return this.uidValidity;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00008339 File Offset: 0x00006539
		internal int UidNext
		{
			get
			{
				return this.uidNext;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00008341 File Offset: 0x00006541
		internal bool HasBeenUpdated
		{
			get
			{
				return this.hasBeenUpdated;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00008349 File Offset: 0x00006549
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00008351 File Offset: 0x00006551
		internal bool MailboxDoesNotExist
		{
			get
			{
				return this.mailboxDoesNotExist;
			}
			set
			{
				this.mailboxDoesNotExist = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000835A File Offset: 0x0000655A
		internal MessageRights Rights
		{
			get
			{
				return this.rights;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00008362 File Offset: 0x00006562
		internal bool ExamineMode
		{
			get
			{
				return this.examineMode;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000836A File Offset: 0x0000656A
		internal bool IsReadOnly
		{
			get
			{
				return (this.rights & (MessageRights.EditOwned | MessageRights.EditAny)) == MessageRights.None;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00008378 File Offset: 0x00006578
		internal bool IsNonselect
		{
			get
			{
				return this.listFlags.IndexOf("\\Noselect", StringComparison.Ordinal) != -1;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00008391 File Offset: 0x00006591
		internal bool IsInbox
		{
			get
			{
				return this.type == Imap4Mailbox.Type.inbox;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000839C File Offset: 0x0000659C
		internal bool IsRoot
		{
			get
			{
				return this.type == Imap4Mailbox.Type.root;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000083A8 File Offset: 0x000065A8
		public void UpdateMessageCounters()
		{
			int num = 0;
			int num2 = 0;
			this.deleted = 0;
			this.firstUnseenIndex = -1;
			foreach (Imap4Message imap4Message in this.messages)
			{
				if (imap4Message.IsDeleted)
				{
					this.messageTotalHasBeenUpdated = true;
					this.deleted++;
				}
				else
				{
					if ((imap4Message.Flags & Imap4Flags.Recent) != Imap4Flags.None)
					{
						num++;
					}
					if ((imap4Message.Flags & Imap4Flags.Seen) == Imap4Flags.None)
					{
						num2++;
					}
					if (this.firstUnseenIndex == -1 && (imap4Message.Flags & Imap4Flags.Seen) == Imap4Flags.None)
					{
						this.firstUnseenIndex = imap4Message.Index;
					}
				}
			}
			if (num != this.recent)
			{
				this.recentHasBeenUpdated = true;
				this.recent = num;
			}
			if (num2 != this.unseen)
			{
				this.unseen = num2;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008488 File Offset: 0x00006688
		public string GetNotifications()
		{
			return this.GetNotifications(true);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008494 File Offset: 0x00006694
		public string GetNotifications(bool consumeExpunge)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			if (this.HasBeenUpdated && !this.MailboxDoesNotExist)
			{
				if (this.recentHasBeenUpdated)
				{
					stringBuilder.AppendFormat("* {1} {0}\r\n", "RECENT", this.recent);
				}
				if (consumeExpunge)
				{
					for (int i = this.messages.Count - 1; i >= 0; i--)
					{
						Imap4Message imap4Message = this.messages[i];
						if (imap4Message.IsDeleted)
						{
							stringBuilder.AppendFormat("* {1} {0}\r\n", "EXPUNGE", i + 1);
						}
					}
					if (this.messageTotalHasBeenUpdated)
					{
						stringBuilder.AppendFormat("* {1} {0}\r\n", "EXISTS", this.Exists);
					}
				}
				for (int j = 0; j < this.messages.Count; j++)
				{
					Imap4Message imap4Message = this.messages[j];
					if (imap4Message.FlagsHaveBeenChanged)
					{
						Imap4Flags imap4Flags;
						if (this.originalMessageFlags.TryGetValue(imap4Message.Id, out imap4Flags) && imap4Message.Flags == imap4Flags)
						{
							imap4Message.FlagsHaveBeenChanged = false;
						}
						else if (!this.addedMessages.Contains(imap4Message.Id))
						{
							stringBuilder.AppendFormat("* {0} FETCH (FLAGS {1})\r\n", imap4Message.Index, Imap4FlagsHelper.ToString(imap4Message.Flags));
						}
					}
				}
				this.originalMessageFlags.Clear();
				this.addedMessages.Clear();
				this.ResetNotifications(consumeExpunge);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008601 File Offset: 0x00006801
		public override string ToString()
		{
			return Imap4Mailbox.PathToString(this.fullPath);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000860E File Offset: 0x0000680E
		public string GetEncodedPath()
		{
			return Imap4UTF7Encoding.Encode(this.fullPath);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000861C File Offset: 0x0000681C
		public override bool Equals(object obj)
		{
			Imap4Mailbox imap4Mailbox = obj as Imap4Mailbox;
			if (imap4Mailbox != null)
			{
				return this.uid.Equals(imap4Mailbox.Uid);
			}
			string text = obj as string;
			return text != null && string.Compare(this.fullPath, text, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008661 File Offset: 0x00006861
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008669 File Offset: 0x00006869
		public void SetNotifications()
		{
			this.hasBeenUpdated = true;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008674 File Offset: 0x00006874
		internal static bool TryNormalizeMailboxPath(string path, out string normalizedPath)
		{
			if (string.IsNullOrEmpty(path))
			{
				normalizedPath = string.Empty;
				return true;
			}
			if (!Imap4UTF7Encoding.TryDecode(path, out normalizedPath))
			{
				normalizedPath = string.Empty;
				return false;
			}
			if (normalizedPath.Length > 1 && normalizedPath[normalizedPath.Length - 1] == '/')
			{
				normalizedPath = normalizedPath.Substring(0, normalizedPath.Length - 1);
			}
			if (normalizedPath[0] == '/')
			{
				normalizedPath = normalizedPath.Substring(1, normalizedPath.Length - 1);
			}
			if (normalizedPath.Length > 0 && (normalizedPath[0] == '/' || normalizedPath[normalizedPath.Length - 1] == '/'))
			{
				normalizedPath = string.Empty;
				return false;
			}
			int num = 0;
			int num2 = normalizedPath.IndexOf('/', num);
			int num3 = 0;
			while (num2 != -1)
			{
				if (num2 - num > 250)
				{
					return false;
				}
				num3++;
				num = num2 + 1;
				num2 = normalizedPath.IndexOf('/', num);
			}
			if (normalizedPath.Length - num > 250)
			{
				return false;
			}
			if (num3 >= 32)
			{
				return false;
			}
			for (int i = 0; i < normalizedPath.Length; i++)
			{
				if (char.IsControl(normalizedPath, i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008793 File Offset: 0x00006993
		internal static string PathToString(string fullPath)
		{
			if (string.IsNullOrEmpty(fullPath) || fullPath.IndexOf(' ') > -1)
			{
				return "\"" + fullPath + "\"";
			}
			return fullPath;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000087BC File Offset: 0x000069BC
		internal static Imap4Mailbox GetRootMailbox(Imap4ResponseFactory factory)
		{
			object[] array = new object[Imap4Mailbox.FolderProperties.Length];
			array[0] = factory.Store.GetDefaultFolderId(DefaultFolderType.Root);
			array[1] = 0;
			array[2] = MessageRights.None;
			array[3] = -1;
			array[4] = 0;
			array[5] = -1;
			array[6] = true;
			array[7] = 1;
			array[8] = false;
			array[9] = string.Empty;
			array[10] = null;
			return new Imap4Mailbox(factory, null, array);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008844 File Offset: 0x00006A44
		internal static void SetParentMailbox(Dictionary<StoreObjectId, Imap4Mailbox> mailboxes, Imap4Mailbox mailbox)
		{
			if (!mailboxes.ContainsKey(mailbox.ParentMailboxUid))
			{
				mailbox.mailboxDoesNotExist = true;
				return;
			}
			Imap4Mailbox imap4Mailbox = mailboxes[mailbox.ParentMailboxUid];
			if (!imap4Mailbox.IsRoot && imap4Mailbox.ParentMailbox == null)
			{
				Imap4Mailbox.SetParentMailbox(mailboxes, imap4Mailbox);
			}
			mailbox.ParentMailbox = imap4Mailbox;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008894 File Offset: 0x00006A94
		internal static ICollection<Imap4Mailbox> GetMailboxTree(Imap4ResponseFactory factory)
		{
			string value = null;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2397449533U, ref value);
			ICollection<Imap4Mailbox> values;
			using (Folder folder = Folder.Bind(factory.Store, DefaultFolderType.Root))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, Imap4Mailbox.FolderProperties))
				{
					int estimatedRowCount = queryResult.EstimatedRowCount;
					Dictionary<StoreObjectId, Imap4Mailbox> dictionary = new Dictionary<StoreObjectId, Imap4Mailbox>(estimatedRowCount + 1);
					Dictionary<string, StoreObjectId> dictionary2 = new Dictionary<string, StoreObjectId>(estimatedRowCount + 1, StringComparer.OrdinalIgnoreCase);
					List<StoreObjectId> list = new List<StoreObjectId>();
					StringBuilder stringBuilder = null;
					StringBuilder stringBuilder2 = null;
					Imap4Mailbox rootMailbox = Imap4Mailbox.GetRootMailbox(factory);
					dictionary.Add(rootMailbox.Uid, rootMailbox);
					Imap4Mailbox imap4Mailbox = null;
					bool showHiddenFolders = ((Imap4Server)factory.Session.Server).ShowHiddenFolders;
					object[][] rows;
					do
					{
						rows = queryResult.GetRows(10000);
						for (int i = 0; i < rows.Length; i++)
						{
							object obj = rows[i][10];
							if (obj is PropertyError)
							{
								list.Add(((VersionedId)rows[i][0]).ObjectId);
							}
							else if (list.Contains((StoreObjectId)obj))
							{
								list.Add(((VersionedId)rows[i][0]).ObjectId);
							}
							else
							{
								if (!showHiddenFolders)
								{
									obj = rows[i][8];
									if (obj is PropertyError || (bool)obj)
									{
										list.Add(((VersionedId)rows[i][0]).ObjectId);
										goto IL_392;
									}
								}
								obj = rows[i][9];
								if (obj is PropertyError)
								{
									list.Add(((VersionedId)rows[i][0]).ObjectId);
								}
								else
								{
									try
									{
										Imap4Mailbox imap4Mailbox2 = new Imap4Mailbox(factory, null, rows[i]);
										imap4Mailbox = imap4Mailbox2;
										if (!imap4Mailbox2.IsRoot && imap4Mailbox2.ParentMailbox == null)
										{
											Imap4Mailbox.SetParentMailbox(dictionary, imap4Mailbox2);
										}
										if (imap4Mailbox2.Name.IndexOf('/') > -1 || imap4Mailbox2.Name.Length > 250)
										{
											list.Add(imap4Mailbox2.Uid);
											if (stringBuilder == null)
											{
												stringBuilder = new StringBuilder();
											}
											stringBuilder.AppendFormat("<li>{0} ({1})</li>", imap4Mailbox2.Name, imap4Mailbox2.FullPath);
										}
										else
										{
											if (imap4Mailbox2.FullPath.Equals(value, StringComparison.OrdinalIgnoreCase))
											{
												dictionary.Add(imap4Mailbox2.Uid, imap4Mailbox2);
												imap4Mailbox = null;
												dictionary2.Add(imap4Mailbox2.FullPath, imap4Mailbox2.Uid);
											}
											StoreObjectId storeObjectId;
											if (dictionary2.TryGetValue(imap4Mailbox2.FullPath, out storeObjectId))
											{
												if (stringBuilder2 == null)
												{
													stringBuilder2 = new StringBuilder();
												}
												else
												{
													stringBuilder2.Append(", ");
												}
												stringBuilder2.Append(imap4Mailbox2.ToString());
												list.Add(imap4Mailbox2.Uid);
												if (storeObjectId != null && dictionary.ContainsKey(storeObjectId))
												{
													if (dictionary[storeObjectId] != null)
													{
														dictionary[storeObjectId].Dispose();
													}
													dictionary.Remove(storeObjectId);
													list.Add(storeObjectId);
													Imap4Mailbox[] array = new Imap4Mailbox[dictionary.Count];
													dictionary.Values.CopyTo(array, 0);
													foreach (Imap4Mailbox imap4Mailbox3 in array)
													{
														if (list.Contains(imap4Mailbox3.ParentMailboxUid))
														{
															if (dictionary.ContainsKey(imap4Mailbox3.Uid) && dictionary[imap4Mailbox3.Uid] != null)
															{
																dictionary[imap4Mailbox3.Uid].Dispose();
															}
															dictionary.Remove(imap4Mailbox3.Uid);
															list.Add(imap4Mailbox3.Uid);
														}
													}
												}
											}
											else
											{
												dictionary.Add(imap4Mailbox2.Uid, imap4Mailbox2);
												dictionary2.Add(imap4Mailbox2.FullPath, imap4Mailbox2.Uid);
												imap4Mailbox = null;
											}
										}
									}
									finally
									{
										if (imap4Mailbox != null)
										{
											imap4Mailbox.Dispose();
											imap4Mailbox = null;
										}
									}
								}
							}
							IL_392:;
						}
					}
					while (rows.Length > 0);
					if (stringBuilder != null)
					{
						SystemMessageHelper.PostUniqueMessage(factory.Store, ProtocolBaseStrings.InvalidNamesSubject, ProtocolBaseStrings.InvalidNamesBody(stringBuilder.ToString()), ProtocolBaseServices.ServiceName + "{A0795A4A-FB27-4637-B13D-F329C8614BB2}");
					}
					if (stringBuilder2 != null)
					{
						SystemMessageHelper.PostUniqueMessage(factory.Store, ProtocolBaseStrings.DuplicateFoldersSubject, ProtocolBaseStrings.DuplicateFoldersBody(stringBuilder2.ToString()), ProtocolBaseServices.ServiceName + "{FDEC7803-34A2-4eff-9847-739FB157FCB7}");
					}
					values = dictionary.Values;
				}
			}
			return values;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008D1C File Offset: 0x00006F1C
		internal void ResetNotifications()
		{
			this.ResetNotifications(true);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008D28 File Offset: 0x00006F28
		internal void ResetNotifications(bool consumeExpunge)
		{
			if (consumeExpunge)
			{
				this.hasBeenUpdated = false;
				this.messageTotalHasBeenUpdated = false;
				this.deleted = 0;
			}
			this.recentHasBeenUpdated = false;
			if (!this.examineMode)
			{
				bool flag = false;
				lock (this.factory.Store)
				{
					try
					{
						if (!this.factory.IsStoreConnected)
						{
							this.factory.ConnectToTheStore();
							flag = true;
						}
						this.UpdateLastSeenArticleId();
					}
					catch (StoragePermanentException ex)
					{
						ProtocolBaseServices.SessionTracer.TraceError(this.factory.Session.SessionId, ex.Message);
						this.factory.Session.BeginShutdown();
					}
					catch (StorageTransientException ex2)
					{
						ProtocolBaseServices.SessionTracer.TraceError(this.factory.Session.SessionId, ex2.Message);
						this.factory.Session.BeginShutdown();
					}
					finally
					{
						if (flag)
						{
							this.factory.DisconnectFromTheStore();
						}
					}
				}
			}
			int num = 1;
			for (int i = 0; i < this.messages.Count; i++)
			{
				if (this.messages[i].FlagsHaveBeenChanged)
				{
					this.messages[i].FlagsHaveBeenChanged = false;
				}
				if (consumeExpunge)
				{
					if (this.messages[i].IsDeleted)
					{
						this.messages.Remove(this.messages[i--]);
					}
					else
					{
						this.messages[i].Index = num++;
					}
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008EEC File Offset: 0x000070EC
		internal List<ProtocolMessage> GetMessagesToExpunge(List<ProtocolMessage> messages)
		{
			List<ProtocolMessage> list = new List<ProtocolMessage>(256);
			foreach (ProtocolMessage protocolMessage in messages)
			{
				if (((Imap4Message)protocolMessage).IsMarkedForDeletion && !protocolMessage.IsDeleted)
				{
					list.Add(protocolMessage);
				}
			}
			return list;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00008F5C File Offset: 0x0000715C
		internal List<ProtocolMessage> GetMessagesToExpunge()
		{
			List<ProtocolMessage> list = new List<ProtocolMessage>(256);
			foreach (Imap4Message imap4Message in this.messages)
			{
				if (imap4Message.IsMarkedForDeletion && !imap4Message.IsDeleted)
				{
					list.Add(imap4Message);
				}
			}
			return list;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008FCC File Offset: 0x000071CC
		internal Imap4Message GetMessage(int index, bool useUid)
		{
			if (useUid)
			{
				for (int i = 0; i < this.messages.Count; i++)
				{
					if (this.messages[i].Id == index)
					{
						return this.messages[i];
					}
				}
				return null;
			}
			if (index >= 1 && index <= this.MessagesTotal)
			{
				return this.messages[index - 1];
			}
			return null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009034 File Offset: 0x00007234
		internal Imap4Message GetMessage(int docId)
		{
			for (int i = this.messages.Count - 1; i >= 0; i--)
			{
				if (this.messages[i].DocumentId.Equals(docId))
				{
					return this.messages[i];
				}
			}
			return null;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009083 File Offset: 0x00007283
		internal void ExploreMailbox(bool isSelect)
		{
			this.ExploreMailbox(this.examineMode, isSelect);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009092 File Offset: 0x00007292
		internal void ExploreMailbox(bool isExamine, bool isSelect)
		{
			this.ExploreMailbox(isExamine, isSelect, false);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000090A0 File Offset: 0x000072A0
		internal void ExploreMailbox(bool isExamine, bool isSelect, bool isViewReload)
		{
			this.examineMode = isExamine;
			using (Folder folder = Folder.Bind(this.factory.Store, this.uid, Imap4Mailbox.FolderProperties))
			{
				this.ReloadMailboxProperties(folder);
				this.LoadMessages(folder, isSelect, isViewReload);
			}
			if (isExamine)
			{
				this.permanentFlags = Imap4Flags.None;
				return;
			}
			if (this.IsReadOnly || (this.Rights & MessageRights.DeleteAny) == MessageRights.None)
			{
				this.permanentFlags = (Imap4Flags.Seen | Imap4Flags.Answered | Imap4Flags.Draft | Imap4Flags.Flagged | Imap4Flags.MdnSent);
				return;
			}
			this.permanentFlags = (Imap4Flags.Seen | Imap4Flags.Deleted | Imap4Flags.Answered | Imap4Flags.Draft | Imap4Flags.Flagged | Imap4Flags.MdnSent);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000912C File Offset: 0x0000732C
		internal void ReloadMailboxProperties()
		{
			using (Folder folder = Folder.Bind(this.factory.Store, this.uid, Imap4Mailbox.FolderProperties))
			{
				this.ReloadMailboxProperties(folder);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009178 File Offset: 0x00007378
		internal Imap4DataAccessViewHandler OpenDataAccessView(Folder folder)
		{
			return new Imap4DataAccessViewHandler(this.Factory, folder);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009188 File Offset: 0x00007388
		internal void DeleteMessage(int docId)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(this.factory.Session.SessionId, "DeleteMessage is called");
			Imap4Message message = this.GetMessage(docId);
			if (message == null)
			{
				return;
			}
			message.IsDeleted = true;
			this.UpdateMessageCounters();
			this.recentHasBeenUpdated = false;
			this.SetNotifications();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000091DC File Offset: 0x000073DC
		internal void AddMessage(int imapId, int docId, Imap4Flags flags, int size)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(this.factory.Session.SessionId, "AddMessage is called");
			Imap4Message message = this.GetMessage(docId);
			if (message != null)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(this.factory.Session.SessionId, "Message is already added");
				return;
			}
			this.AppendOrInsertMessage(new Imap4Message(this, this.messages.Count + 1, imapId, docId, flags, size));
			this.addedMessages.Add(imapId);
			this.messageTotalHasBeenUpdated = true;
			this.UpdateMessageCounters();
			this.SetNotifications();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00009274 File Offset: 0x00007474
		internal bool ModifyMessage(int imapId, int docId, Imap4Flags flags, int size)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(this.factory.Session.SessionId, "ModifyMessage is called");
			Imap4Message message = this.GetMessage(docId);
			if (message == null)
			{
				return false;
			}
			if (message.Id != imapId)
			{
				message.IsDeleted = true;
				this.AppendOrInsertMessage(new Imap4Message(this, this.messages.Count + 1, imapId, docId, flags, size));
				this.addedMessages.Add(imapId);
				this.messageTotalHasBeenUpdated = true;
				this.UpdateMessageCounters();
				this.SetNotifications();
				return true;
			}
			if (message.Reload(flags, this.originalMessageFlags))
			{
				this.UpdateMessageCounters();
				this.SetNotifications();
			}
			return false;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009319 File Offset: 0x00007519
		internal void DisposeViews()
		{
			if (this.fastQueryView != null)
			{
				this.fastQueryView.Dispose();
				this.fastQueryView = null;
			}
			if (this.dataAccessView != null)
			{
				this.dataAccessView.Dispose();
				this.dataAccessView = null;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000934F File Offset: 0x0000754F
		protected override void InternalDispose(bool isDisposing)
		{
			this.DisposeViews();
			this.factory = null;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000935E File Offset: 0x0000755E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Imap4Mailbox>(this);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009368 File Offset: 0x00007568
		private static int CompareImapIdsAndModificationTimes(Imap4Mailbox.UidFixItemInfo x, Imap4Mailbox.UidFixItemInfo y)
		{
			if (x.ImapId > y.ImapId)
			{
				return 1;
			}
			if (x.ImapId < y.ImapId)
			{
				return -1;
			}
			if (x.LastModificationTime > y.LastModificationTime)
			{
				return 1;
			}
			if (x.LastModificationTime < y.LastModificationTime)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000093C0 File Offset: 0x000075C0
		private static void GetItemsInFolder(Folder folder, List<Imap4Mailbox.UidFixItemInfo> itemsInFolder)
		{
			itemsInFolder.Clear();
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, new PropertyDefinition[]
			{
				ItemSchema.Id,
				ItemSchema.ImapId,
				StoreObjectSchema.LastModifiedTime
			}))
			{
				object[][] rows;
				do
				{
					rows = queryResult.GetRows(10000);
					for (int i = 0; i < rows.Length; i++)
					{
						if (!(rows[i][0] is PropertyError) && !(rows[i][1] is PropertyError) && !(rows[i][2] is PropertyError))
						{
							itemsInFolder.Add(new Imap4Mailbox.UidFixItemInfo((int)rows[i][1], (ExDateTime)rows[i][2], ((VersionedId)rows[i][0]).ObjectId));
						}
					}
				}
				while (rows.Length > 0);
			}
			itemsInFolder.Sort(new Comparison<Imap4Mailbox.UidFixItemInfo>(Imap4Mailbox.CompareImapIdsAndModificationTimes));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000094A0 File Offset: 0x000076A0
		private void ReloadMailboxProperties(Folder folder)
		{
			object[] array = new object[Imap4Mailbox.FolderProperties.Length];
			for (int i = 0; i < Imap4Mailbox.FolderProperties.Length; i++)
			{
				array[i] = folder.TryGetProperty(Imap4Mailbox.FolderProperties[i]);
			}
			this.LoadMailboxProperties(array);
			this.UpdateMessageCounters();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000094EC File Offset: 0x000076EC
		private string GetFoldersListFlags(object[] data)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder(64);
			object obj = data[3];
			object obj2 = data[5];
			object obj3 = data[7];
			if (obj is PropertyError && obj2 is PropertyError && obj3 is PropertyError)
			{
				flag = true;
			}
			if (this.IsRoot || flag)
			{
				stringBuilder.Append("\\Noselect");
			}
			if (this.IsInbox)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append("\\Marked");
			}
			else if (!(obj is PropertyError) && !(obj2 is PropertyError) && (int)obj2 > (int)obj + 1)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append("\\Marked");
			}
			if (!(obj3 is PropertyError))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				if ((int)obj3 > 0)
				{
					stringBuilder.Append("\\HasChildren");
				}
				else
				{
					stringBuilder.Append("\\HasNoChildren");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000095F0 File Offset: 0x000077F0
		private void LoadMailboxProperties(object[] data)
		{
			object obj = data[0];
			if (obj is PropertyError)
			{
				throw new ArgumentException("Id is invalid");
			}
			if (obj is VersionedId)
			{
				this.uid = ((VersionedId)obj).ObjectId;
			}
			else
			{
				if (!(obj is StoreObjectId))
				{
					throw new ArgumentException("Id is invalid");
				}
				this.uid = (StoreObjectId)obj;
			}
			obj = data[1];
			if (!(obj is PropertyError))
			{
				this.uidValidity = (int)obj;
			}
			else
			{
				this.uidValidity = 0;
			}
			obj = data[2];
			if (!(obj is PropertyError))
			{
				this.rights = (MessageRights)obj;
			}
			else
			{
				this.rights = MessageRights.None;
			}
			obj = data[3];
			if (!(obj is PropertyError))
			{
				this.lastSeenArticleId = (int)obj;
			}
			else
			{
				this.lastSeenArticleId = -1;
			}
			obj = data[5];
			if (!(obj is PropertyError))
			{
				this.uidNext = (int)obj;
			}
			else
			{
				this.uidNext = 0;
			}
			obj = data[6];
			if (!(obj is PropertyError))
			{
				this.hasChildren = (bool)obj;
			}
			else
			{
				this.hasChildren = false;
			}
			obj = data[8];
			if (!(obj is PropertyError))
			{
				this.isHidden = (bool)obj;
			}
			else
			{
				this.isHidden = false;
			}
			DefaultFolderType defaultFolderType = this.factory.Store.IsDefaultFolderType(this.uid);
			DefaultFolderType defaultFolderType2 = defaultFolderType;
			if (defaultFolderType2 != DefaultFolderType.Inbox)
			{
				if (defaultFolderType2 == DefaultFolderType.Root)
				{
					this.type = Imap4Mailbox.Type.root;
					this.name = string.Empty;
				}
				else
				{
					this.type = Imap4Mailbox.Type.normal;
					obj = data[9];
					if (!(obj is PropertyError))
					{
						this.name = (string)obj;
					}
					else
					{
						this.name = null;
					}
				}
			}
			else
			{
				this.type = Imap4Mailbox.Type.inbox;
				this.name = "INBOX";
			}
			obj = data[10];
			if (!(obj is PropertyError))
			{
				this.parentMailboxUid = (StoreObjectId)obj;
			}
			else
			{
				this.parentMailboxUid = null;
			}
			obj = data[4];
			if (!(obj is PropertyError))
			{
				this.itemCount = (int)obj;
				return;
			}
			this.itemCount = 0;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000097CC File Offset: 0x000079CC
		private void UpdateLastSeenArticleId()
		{
			if (this.IsReadOnly)
			{
				return;
			}
			using (Folder folder = Folder.Bind(this.factory.Store, this.uid, Imap4Mailbox.FolderProperties))
			{
				if (!(folder.TryGetProperty(FolderSchema.NextArticleId) is PropertyError))
				{
					int num = (int)folder.TryGetProperty(FolderSchema.NextArticleId) - 1;
					ProtocolBaseServices.SessionTracer.TraceDebug<int>(this.factory.Session.SessionId, "Writing ImapLastSeenArticleId {0}", num);
					folder[FolderSchema.ImapLastSeenArticleId] = num;
					folder.Save();
				}
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009878 File Offset: 0x00007A78
		private void LoadMessages(Folder folder, bool isSelect, bool isViewReload)
		{
			if (isViewReload)
			{
				for (int i = 0; i < this.messages.Count; i++)
				{
					this.messages[i].IsDeleted = true;
				}
				this.SetNotifications();
			}
			else
			{
				this.messages.Clear();
			}
			try
			{
				this.DisposeViews();
			}
			catch (LocalizedException)
			{
			}
			bool flag = ResponseFactory.IsXsoVersionChanged(this.PreviousXsoVersion);
			this.fastQueryView = new Imap4FastQueryViewHandler(this.Factory, folder);
			int num = 0;
			HashSet<int> hashSet = new HashSet<int>();
			int num2 = 0;
			int num3 = -1;
			object[][] rows;
			do
			{
				rows = this.fastQueryView.TableView.GetRows(10000);
				for (int j = 0; j < rows.Length; j++)
				{
					object obj = rows[j][9];
					if (!(obj is PropertyError) && (bool)obj)
					{
						num++;
					}
					else if (rows[j][1] is PropertyError || rows[j][0] is PropertyError)
					{
						num++;
					}
					else
					{
						Imap4Message imap4Message = new Imap4Message(this, this.messages.Count + 1, rows[j]);
						if (imap4Message.IsNotRfc822Renderable && flag)
						{
							if (this.dataAccessView == null)
							{
								this.dataAccessView = this.OpenDataAccessView(folder);
							}
							this.dataAccessView.SetPoisonFlag(folder, imap4Message.Id, false);
							imap4Message.IsNotRfc822Renderable = false;
						}
						this.AppendOrInsertMessage(imap4Message);
						if (hashSet.Contains(imap4Message.Id))
						{
							num2++;
						}
						else
						{
							hashSet.Add(imap4Message.Id);
						}
						num3 = Math.Max(num3, imap4Message.Id);
					}
				}
			}
			while (rows.Length > 0);
			if (flag)
			{
				ResponseFactory.RecordCurrentXsoVersion(folder);
			}
			if (isSelect && this.dataAccessView == null)
			{
				this.dataAccessView = this.OpenDataAccessView(folder);
			}
			this.DetectUidCorruption(folder, num3, num2, num, true);
			this.UpdateMessageCounters();
			this.ResetNotifications(!isViewReload);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009A5C File Offset: 0x00007C5C
		private void DetectUidCorruption(Folder folder, int maxExistingUid, int numberOfDuplicateUids, int rejectedItems, bool tryToReloadFolderProperties)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(this.factory.Session.SessionId, "ImapId corruption detection:\r\nUidNext ({0}) <= maxExistingUid ({1}) is {2}\r\nthis.itemCount ({3}) > this.MessagesTotal ({4}) + rejectedItems ({5}) is {6}\r\nnumberOfDuplicateUids ({7}) > 0 is {8}", new object[]
			{
				this.UidNext,
				maxExistingUid,
				this.UidNext <= maxExistingUid,
				this.itemCount,
				this.MessagesTotal,
				rejectedItems,
				this.itemCount > this.MessagesTotal + rejectedItems,
				numberOfDuplicateUids,
				numberOfDuplicateUids > 0
			});
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UidCorruptionDetected");
			bool flag = false;
			if (this.UidNext <= maxExistingUid)
			{
				flag = true;
				stringBuilder.Append('1');
			}
			if (this.itemCount > this.MessagesTotal + rejectedItems)
			{
				flag = true;
				stringBuilder.Append('2');
			}
			if (numberOfDuplicateUids > 0)
			{
				flag = true;
				stringBuilder.Append('3');
			}
			if (flag)
			{
				if (tryToReloadFolderProperties)
				{
					folder.Load(Imap4Mailbox.FolderProperties);
					this.ReloadMailboxProperties(folder);
					this.DetectUidCorruption(folder, maxExistingUid, numberOfDuplicateUids, rejectedItems, false);
					return;
				}
				this.Factory.Session.LightLogSession.Message = stringBuilder.ToString();
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009BA4 File Offset: 0x00007DA4
		private void AppendOrInsertMessage(Imap4Message newMessage)
		{
			int num = this.messages.Count - 1;
			if (num == -1 || this.messages[num].Id < newMessage.Id)
			{
				this.messages.Add(newMessage);
				return;
			}
			while (num >= 0 && this.messages[num].Id > newMessage.Id)
			{
				num--;
			}
			if (num >= 0 && this.messages[num].Id == newMessage.Id)
			{
				this.messages[num].Flags = newMessage.Flags;
				this.messages[num].IsDeleted = false;
				return;
			}
			num++;
			this.messages.Insert(num, newMessage);
		}

		// Token: 0x040000B4 RID: 180
		internal const string InboxName = "INBOX";

		// Token: 0x040000B5 RID: 181
		internal const char FolderSeparator = '/';

		// Token: 0x040000B6 RID: 182
		internal const string InvalidNamesMessageId = "{A0795A4A-FB27-4637-B13D-F329C8614BB2}";

		// Token: 0x040000B7 RID: 183
		internal const string DuplicateFoldersMessageId = "{FDEC7803-34A2-4eff-9847-739FB157FCB7}";

		// Token: 0x040000B8 RID: 184
		internal const string Notification = "* {1} {0}\r\n";

		// Token: 0x040000B9 RID: 185
		internal const string NotificationExpunge = "EXPUNGE";

		// Token: 0x040000BA RID: 186
		private const string NotificationExists = "EXISTS";

		// Token: 0x040000BB RID: 187
		private const string FlagNoselect = "\\Noselect";

		// Token: 0x040000BC RID: 188
		private const string FlagMarked = "\\Marked";

		// Token: 0x040000BD RID: 189
		private const string FlagHasNoChildren = "\\HasNoChildren";

		// Token: 0x040000BE RID: 190
		private const string FlagHasChildren = "\\HasChildren";

		// Token: 0x040000BF RID: 191
		private const string NotificationRecent = "RECENT";

		// Token: 0x040000C0 RID: 192
		private const string NotificationUnseen = "UNSEEN";

		// Token: 0x040000C1 RID: 193
		private const string NotificationFlags = "FLAGS";

		// Token: 0x040000C2 RID: 194
		private const string NotificationFetchFlags = "* {0} FETCH (FLAGS {1})\r\n";

		// Token: 0x040000C3 RID: 195
		internal static readonly SortBy[] SortBy = new SortBy[]
		{
			new SortBy(ItemSchema.ImapId, SortOrder.Ascending)
		};

		// Token: 0x040000C4 RID: 196
		internal static readonly PropertyDefinition[] FolderProperties = new PropertyDefinition[]
		{
			FolderSchema.Id,
			ItemSchema.ArticleId,
			FolderSchema.AccessRights,
			FolderSchema.ImapLastSeenArticleId,
			FolderSchema.ItemCount,
			FolderSchema.NextArticleId,
			FolderSchema.HasChildren,
			FolderSchema.ChildCount,
			FolderSchema.IsHidden,
			FolderSchema.DisplayName,
			StoreObjectSchema.ParentItemId,
			FolderSchema.PopImapConversionVersion
		};

		// Token: 0x040000C5 RID: 197
		private static readonly PropertyDefinition[] imapIdProperty = new PropertyDefinition[]
		{
			ItemSchema.ImapId
		};

		// Token: 0x040000C6 RID: 198
		private static readonly PropertyDefinition[] folderPropertyList = new PropertyDefinition[]
		{
			FolderSchema.NextArticleId,
			FolderSchema.ImapLastUidFixTime
		};

		// Token: 0x040000C7 RID: 199
		private static string rootKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x040000C8 RID: 200
		private static string imapKeyName = Imap4Mailbox.rootKeyName + "\\IMAP";

		// Token: 0x040000C9 RID: 201
		private static Imap4Flags flags = Imap4Flags.Seen | Imap4Flags.Deleted | Imap4Flags.Answered | Imap4Flags.Draft | Imap4Flags.Flagged | Imap4Flags.MdnSent;

		// Token: 0x040000CA RID: 202
		private Imap4ResponseFactory factory;

		// Token: 0x040000CB RID: 203
		private string name;

		// Token: 0x040000CC RID: 204
		private Imap4Mailbox.Type type;

		// Token: 0x040000CD RID: 205
		private string fullPath;

		// Token: 0x040000CE RID: 206
		private StoreObjectId uid;

		// Token: 0x040000CF RID: 207
		private StoreObjectId parentMailboxUid;

		// Token: 0x040000D0 RID: 208
		private Imap4Mailbox parentMailbox;

		// Token: 0x040000D1 RID: 209
		private bool hasChildren;

		// Token: 0x040000D2 RID: 210
		private string listFlags;

		// Token: 0x040000D3 RID: 211
		private Imap4Flags permanentFlags;

		// Token: 0x040000D4 RID: 212
		private bool isHidden;

		// Token: 0x040000D5 RID: 213
		private int recent;

		// Token: 0x040000D6 RID: 214
		private int lastSeenArticleId;

		// Token: 0x040000D7 RID: 215
		private int firstUnseenIndex;

		// Token: 0x040000D8 RID: 216
		private int unseen;

		// Token: 0x040000D9 RID: 217
		private int deleted;

		// Token: 0x040000DA RID: 218
		private int uidValidity;

		// Token: 0x040000DB RID: 219
		private int uidNext;

		// Token: 0x040000DC RID: 220
		private MessageRights rights;

		// Token: 0x040000DD RID: 221
		private bool examineMode;

		// Token: 0x040000DE RID: 222
		private List<Imap4Message> messages;

		// Token: 0x040000DF RID: 223
		private int itemCount;

		// Token: 0x040000E0 RID: 224
		private bool hasBeenUpdated;

		// Token: 0x040000E1 RID: 225
		private bool messageTotalHasBeenUpdated;

		// Token: 0x040000E2 RID: 226
		private bool recentHasBeenUpdated;

		// Token: 0x040000E3 RID: 227
		private bool mailboxDoesNotExist;

		// Token: 0x040000E4 RID: 228
		private int[] previousXsoVersion;

		// Token: 0x040000E5 RID: 229
		private Imap4DataAccessViewHandler dataAccessView;

		// Token: 0x040000E6 RID: 230
		private Imap4FastQueryViewHandler fastQueryView;

		// Token: 0x040000E7 RID: 231
		private Dictionary<int, Imap4Flags> originalMessageFlags = new Dictionary<int, Imap4Flags>(5);

		// Token: 0x040000E8 RID: 232
		private HashSet<int> addedMessages = new HashSet<int>();

		// Token: 0x02000017 RID: 23
		private enum Type
		{
			// Token: 0x040000EA RID: 234
			normal,
			// Token: 0x040000EB RID: 235
			root,
			// Token: 0x040000EC RID: 236
			inbox
		}

		// Token: 0x02000018 RID: 24
		private struct PropertyIndex
		{
			// Token: 0x040000ED RID: 237
			public const int Id = 0;

			// Token: 0x040000EE RID: 238
			public const int ArticleId = 1;

			// Token: 0x040000EF RID: 239
			public const int AccessRight = 2;

			// Token: 0x040000F0 RID: 240
			public const int ImapLastSeenArticleId = 3;

			// Token: 0x040000F1 RID: 241
			public const int ItemCount = 4;

			// Token: 0x040000F2 RID: 242
			public const int NextArticleId = 5;

			// Token: 0x040000F3 RID: 243
			public const int HasChildren = 6;

			// Token: 0x040000F4 RID: 244
			public const int ChildCount = 7;

			// Token: 0x040000F5 RID: 245
			public const int IsHidden = 8;

			// Token: 0x040000F6 RID: 246
			public const int DisplayName = 9;

			// Token: 0x040000F7 RID: 247
			public const int ParentId = 10;

			// Token: 0x040000F8 RID: 248
			public const int ConversionVersion = 11;
		}

		// Token: 0x02000019 RID: 25
		internal class UidFixItemInfo
		{
			// Token: 0x06000145 RID: 325 RVA: 0x00009D54 File Offset: 0x00007F54
			internal UidFixItemInfo(int imapId, ExDateTime lastModificationTime, StoreObjectId objectId)
			{
				this.ImapId = imapId;
				this.LastModificationTime = lastModificationTime;
				this.ObjectId = objectId;
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000146 RID: 326 RVA: 0x00009D71 File Offset: 0x00007F71
			// (set) Token: 0x06000147 RID: 327 RVA: 0x00009D79 File Offset: 0x00007F79
			public int ImapId { get; set; }

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000148 RID: 328 RVA: 0x00009D82 File Offset: 0x00007F82
			// (set) Token: 0x06000149 RID: 329 RVA: 0x00009D8A File Offset: 0x00007F8A
			public ExDateTime LastModificationTime { get; private set; }

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600014A RID: 330 RVA: 0x00009D93 File Offset: 0x00007F93
			// (set) Token: 0x0600014B RID: 331 RVA: 0x00009D9B File Offset: 0x00007F9B
			public StoreObjectId ObjectId { get; private set; }
		}

		// Token: 0x0200001A RID: 26
		internal class ItemsDeleteError : ValidationError
		{
			// Token: 0x0600014C RID: 332 RVA: 0x00009DA4 File Offset: 0x00007FA4
			internal ItemsDeleteError(string folderName) : base(new LocalizedString(string.Format("Failed to delete messages within folder {0}", folderName)))
			{
			}

			// Token: 0x040000FC RID: 252
			private const string ErrorMessage = "Failed to delete messages within folder {0}";
		}
	}
}
