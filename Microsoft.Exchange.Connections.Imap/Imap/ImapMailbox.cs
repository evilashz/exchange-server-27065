using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class ImapMailbox
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00006388 File Offset: 0x00004588
		public ImapMailbox(string nameOnTheWire)
		{
			this.IsWritable = true;
			this.NameOnTheWire = nameOnTheWire;
			this.Name = nameOnTheWire;
			this.IsSelectable = true;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000063B9 File Offset: 0x000045B9
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000063C1 File Offset: 0x000045C1
		public string Name { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000063CA File Offset: 0x000045CA
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000063D2 File Offset: 0x000045D2
		public string NameOnTheWire { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000063DB File Offset: 0x000045DB
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000063E3 File Offset: 0x000045E3
		public char? Separator { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000063EC File Offset: 0x000045EC
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000063F4 File Offset: 0x000045F4
		public bool IsSelectable { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000063FD File Offset: 0x000045FD
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00006405 File Offset: 0x00004605
		public bool? HasChildren { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000640E File Offset: 0x0000460E
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00006416 File Offset: 0x00004616
		public bool NoInferiors { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000641F File Offset: 0x0000461F
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00006427 File Offset: 0x00004627
		public bool IsWritable { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006430 File Offset: 0x00004630
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00006438 File Offset: 0x00004638
		public ImapMailFlags PermanentFlags { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006441 File Offset: 0x00004641
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00006449 File Offset: 0x00004649
		public long? UidValidity { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006452 File Offset: 0x00004652
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000645A File Offset: 0x0000465A
		public long? UidNext { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006463 File Offset: 0x00004663
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000646B File Offset: 0x0000466B
		public int? NumberOfMessages { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006474 File Offset: 0x00004674
		public string ParentFolderPath
		{
			get
			{
				string text = null;
				int lastIndexOfSeparator = this.GetLastIndexOfSeparator();
				if (lastIndexOfSeparator >= 0)
				{
					text = this.Name.Substring(0, lastIndexOfSeparator);
				}
				ImapUtilities.FromModifiedUTF7(text, out text);
				return text;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000064A8 File Offset: 0x000046A8
		public string ShortFolderName
		{
			get
			{
				string text = this.Name;
				int lastIndexOfSeparator = this.GetLastIndexOfSeparator();
				if (lastIndexOfSeparator >= 0)
				{
					text = this.Name.Substring(lastIndexOfSeparator + 1);
				}
				ImapUtilities.FromModifiedUTF7(text, out text);
				return text;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000064E0 File Offset: 0x000046E0
		internal static void EnsureDefaultFolderMappingTable(ILog log = null)
		{
			if (ImapMailbox.hasCreatedMappingTable)
			{
				return;
			}
			lock (ImapMailbox.MappingTableLock)
			{
				if (ImapMailbox.hasCreatedMappingTable)
				{
					return;
				}
				ImapMailbox.BuildDefaultFolderMappings();
				ImapMailbox.hasCreatedMappingTable = true;
			}
			if (log != null)
			{
				foreach (KeyValuePair<string, KeyedPair<string, ImapDefaultFolderType>> keyValuePair in ImapMailbox.preferredDefaultFolderMappings)
				{
					log.Debug("Preferred Mapping: {0} => {1}", new object[]
					{
						keyValuePair.Key,
						keyValuePair.Value.Second
					});
				}
				foreach (KeyValuePair<string, ImapDefaultFolderType> keyValuePair2 in ImapMailbox.secondaryDefaultFolderMappings)
				{
					log.Debug("Default Mapping: {0} => {1}", new object[]
					{
						keyValuePair2.Key,
						keyValuePair2.Value
					});
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000661C File Offset: 0x0000481C
		internal static ImapDefaultFolderType GetDefaultFolderType(string mailboxName, out bool preferredMapping, out bool exactCaseSensitiveMatch)
		{
			ImapDefaultFolderType result = ImapDefaultFolderType.None;
			preferredMapping = false;
			exactCaseSensitiveMatch = false;
			KeyedPair<string, ImapDefaultFolderType> keyedPair;
			if (ImapMailbox.preferredDefaultFolderMappings.TryGetValue(mailboxName, out keyedPair))
			{
				preferredMapping = true;
				result = keyedPair.Second;
				exactCaseSensitiveMatch = (0 == string.Compare(mailboxName, keyedPair.First, StringComparison.Ordinal));
			}
			else
			{
				ImapMailbox.secondaryDefaultFolderMappings.TryGetValue(mailboxName, out result);
			}
			return result;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006670 File Offset: 0x00004870
		internal static ImapDefaultFolderType GetDefaultFolderType(string mailboxName)
		{
			bool flag = false;
			bool flag2 = false;
			return ImapMailbox.GetDefaultFolderType(mailboxName, out flag, out flag2);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000668B File Offset: 0x0000488B
		internal void Rename(string newName)
		{
			this.Name = newName;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006694 File Offset: 0x00004894
		private static void BuildDefaultFolderMappings()
		{
			ImapMailbox.preferredDefaultFolderMappings = new Dictionary<string, KeyedPair<string, ImapDefaultFolderType>>(6, StringComparer.OrdinalIgnoreCase);
			ImapMailbox.secondaryDefaultFolderMappings = new Dictionary<string, ImapDefaultFolderType>(14, StringComparer.OrdinalIgnoreCase);
			ImapMailbox.preferredDefaultFolderMappings.Add(ImapMailbox.Inbox, new KeyedPair<string, ImapDefaultFolderType>(ImapMailbox.Inbox, ImapDefaultFolderType.Inbox));
			ImapMailbox.AddDefaultMappings();
			CultureInfo[] installedLanguagePackCultures = LanguagePackInfo.GetInstalledLanguagePackCultures(LanguagePackType.Server);
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			try
			{
				CultureInfo[] array = installedLanguagePackCultures;
				int i = 0;
				while (i < array.Length)
				{
					CultureInfo currentUICulture = array[i];
					try
					{
						Thread.CurrentThread.CurrentUICulture = currentUICulture;
					}
					catch (NotSupportedException)
					{
						goto IL_73;
					}
					goto IL_6E;
					IL_73:
					i++;
					continue;
					IL_6E:
					ImapMailbox.AddDefaultMappings();
					goto IL_73;
				}
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000674C File Offset: 0x0000494C
		private static void AddPreferredMapping(string folderNameUTF7, ImapDefaultFolderType type)
		{
			ImapMailbox.preferredDefaultFolderMappings[folderNameUTF7] = new KeyedPair<string, ImapDefaultFolderType>(folderNameUTF7, type);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006760 File Offset: 0x00004960
		private static void AddDefaultMappings()
		{
			ImapMailbox.AddPreferredMapping(ImapUtilities.ToModifiedUTF7(CXStrings.ImapDeletedItems), ImapDefaultFolderType.DeletedItems);
			ImapMailbox.secondaryDefaultFolderMappings[ImapUtilities.ToModifiedUTF7(CXStrings.ImapDeletedMessages)] = ImapDefaultFolderType.DeletedItems;
			ImapMailbox.secondaryDefaultFolderMappings[ImapUtilities.ToModifiedUTF7(CXStrings.ImapTrash)] = ImapDefaultFolderType.DeletedItems;
			ImapMailbox.secondaryDefaultFolderMappings[ImapMailbox.InboxPrefix + ImapUtilities.ToModifiedUTF7(CXStrings.ImapTrash)] = ImapDefaultFolderType.DeletedItems;
			ImapMailbox.secondaryDefaultFolderMappings["[Gmail]/" + ImapUtilities.ToModifiedUTF7(CXStrings.ImapTrash)] = ImapDefaultFolderType.DeletedItems;
			ImapMailbox.AddPreferredMapping(ImapUtilities.ToModifiedUTF7(CXStrings.ImapDrafts), ImapDefaultFolderType.Drafts);
			ImapMailbox.secondaryDefaultFolderMappings[ImapUtilities.ToModifiedUTF7(CXStrings.ImapDraft)] = ImapDefaultFolderType.Drafts;
			ImapMailbox.secondaryDefaultFolderMappings[ImapMailbox.InboxPrefix + ImapUtilities.ToModifiedUTF7(CXStrings.ImapDrafts)] = ImapDefaultFolderType.Drafts;
			ImapMailbox.secondaryDefaultFolderMappings["[Gmail]/" + ImapUtilities.ToModifiedUTF7(CXStrings.ImapDrafts)] = ImapDefaultFolderType.Drafts;
			ImapMailbox.AddPreferredMapping(ImapUtilities.ToModifiedUTF7(CXStrings.ImapJunkEmail), ImapDefaultFolderType.JunkEmail);
			ImapMailbox.AddPreferredMapping("[Gmail]/" + ImapUtilities.ToModifiedUTF7(CXStrings.ImapSpam), ImapDefaultFolderType.JunkEmail);
			ImapMailbox.secondaryDefaultFolderMappings[ImapUtilities.ToModifiedUTF7(CXStrings.ImapSpam)] = ImapDefaultFolderType.JunkEmail;
			ImapMailbox.secondaryDefaultFolderMappings[ImapUtilities.ToModifiedUTF7(CXStrings.ImapJunk)] = ImapDefaultFolderType.JunkEmail;
			ImapMailbox.secondaryDefaultFolderMappings["[Gmail]/" + ImapUtilities.ToModifiedUTF7(CXStrings.ImapAllMail)] = ImapDefaultFolderType.JunkEmail;
			ImapMailbox.AddPreferredMapping(ImapUtilities.ToModifiedUTF7(CXStrings.ImapSentItems), ImapDefaultFolderType.SentItems);
			ImapMailbox.secondaryDefaultFolderMappings[ImapUtilities.ToModifiedUTF7(CXStrings.ImapSentMessages)] = ImapDefaultFolderType.SentItems;
			ImapMailbox.secondaryDefaultFolderMappings[ImapUtilities.ToModifiedUTF7(CXStrings.ImapSent)] = ImapDefaultFolderType.SentItems;
			ImapMailbox.secondaryDefaultFolderMappings[ImapMailbox.InboxPrefix + ImapUtilities.ToModifiedUTF7(CXStrings.ImapSentItems)] = ImapDefaultFolderType.SentItems;
			ImapMailbox.secondaryDefaultFolderMappings[ImapMailbox.InboxPrefix + ImapUtilities.ToModifiedUTF7(CXStrings.ImapSent)] = ImapDefaultFolderType.SentItems;
			ImapMailbox.secondaryDefaultFolderMappings["[Gmail]/" + ImapUtilities.ToModifiedUTF7(CXStrings.ImapSentMail)] = ImapDefaultFolderType.SentItems;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000069B8 File Offset: 0x00004BB8
		private int GetLastIndexOfSeparator()
		{
			int result = -1;
			if (!string.IsNullOrEmpty(this.Name))
			{
				char value = (this.Separator != null) ? this.Separator.Value : '/';
				result = this.Name.LastIndexOf(value);
			}
			return result;
		}

		// Token: 0x04000078 RID: 120
		private const int NumPreferredMappings = 6;

		// Token: 0x04000079 RID: 121
		private const int NumSecondaryMappings = 14;

		// Token: 0x0400007A RID: 122
		private const string GmailPrefix = "[Gmail]/";

		// Token: 0x0400007B RID: 123
		internal static readonly string Inbox = "INBOX";

		// Token: 0x0400007C RID: 124
		private static readonly string InboxPrefix = ImapMailbox.Inbox + ".";

		// Token: 0x0400007D RID: 125
		private static readonly object MappingTableLock = new object();

		// Token: 0x0400007E RID: 126
		private static Dictionary<string, KeyedPair<string, ImapDefaultFolderType>> preferredDefaultFolderMappings;

		// Token: 0x0400007F RID: 127
		private static Dictionary<string, ImapDefaultFolderType> secondaryDefaultFolderMappings;

		// Token: 0x04000080 RID: 128
		private static bool hasCreatedMappingTable = false;
	}
}
