using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001D4 RID: 468
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPMailbox
	{
		// Token: 0x06000E3D RID: 3645 RVA: 0x00025AC0 File Offset: 0x00023CC0
		internal IMAPMailbox(string nameOnTheWire)
		{
			this.nameOnTheWire = nameOnTheWire;
			this.name = nameOnTheWire;
			this.IsSelectable = true;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00025AF1 File Offset: 0x00023CF1
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00025AF9 File Offset: 0x00023CF9
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

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00025B02 File Offset: 0x00023D02
		internal string NameOnTheWire
		{
			get
			{
				return this.nameOnTheWire;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x00025B0A File Offset: 0x00023D0A
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x00025B12 File Offset: 0x00023D12
		internal char? Separator
		{
			get
			{
				return this.separator;
			}
			set
			{
				this.separator = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x00025B1B File Offset: 0x00023D1B
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x00025B23 File Offset: 0x00023D23
		internal bool IsSelectable
		{
			get
			{
				return this.selectable;
			}
			set
			{
				this.selectable = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00025B2C File Offset: 0x00023D2C
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x00025B34 File Offset: 0x00023D34
		internal bool? HasChildren
		{
			get
			{
				return this.hasChildren;
			}
			set
			{
				this.hasChildren = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00025B3D File Offset: 0x00023D3D
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00025B45 File Offset: 0x00023D45
		internal bool NoInferiors
		{
			get
			{
				return this.noInferiors;
			}
			set
			{
				this.noInferiors = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00025B4E File Offset: 0x00023D4E
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00025B56 File Offset: 0x00023D56
		internal bool IsWritable
		{
			get
			{
				return this.writable;
			}
			set
			{
				this.writable = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00025B5F File Offset: 0x00023D5F
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00025B67 File Offset: 0x00023D67
		internal IMAPMailFlags PermanentFlags
		{
			get
			{
				return this.permanentFlags;
			}
			set
			{
				this.permanentFlags = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00025B70 File Offset: 0x00023D70
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x00025B78 File Offset: 0x00023D78
		internal long? UidValidity
		{
			get
			{
				return this.uidValidity;
			}
			set
			{
				this.uidValidity = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00025B81 File Offset: 0x00023D81
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x00025B89 File Offset: 0x00023D89
		internal long? UidNext
		{
			get
			{
				return this.uidNext;
			}
			set
			{
				this.uidNext = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00025B92 File Offset: 0x00023D92
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00025B9A File Offset: 0x00023D9A
		internal int? NumberOfMessages
		{
			get
			{
				return this.numberOfMessages;
			}
			set
			{
				this.numberOfMessages = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00025BA4 File Offset: 0x00023DA4
		internal string ParentFolderPath
		{
			get
			{
				string text = null;
				int lastIndexOfSeparator = this.GetLastIndexOfSeparator();
				if (lastIndexOfSeparator >= 0)
				{
					text = this.Name.Substring(0, lastIndexOfSeparator);
				}
				IMAPUtils.FromModifiedUTF7(text, out text);
				return text;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00025BD8 File Offset: 0x00023DD8
		internal string ShortFolderName
		{
			get
			{
				string text = this.Name;
				int lastIndexOfSeparator = this.GetLastIndexOfSeparator();
				if (lastIndexOfSeparator >= 0)
				{
					text = this.Name.Substring(lastIndexOfSeparator + 1);
				}
				IMAPUtils.FromModifiedUTF7(text, out text);
				return text;
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00025C10 File Offset: 0x00023E10
		internal static void EnsureDefaultFolderMappingTable(SyncLogSession log)
		{
			if (IMAPMailbox.hasCreatedMappingTable)
			{
				return;
			}
			lock (IMAPMailbox.mappingTableLock)
			{
				if (IMAPMailbox.hasCreatedMappingTable)
				{
					return;
				}
				IMAPMailbox.BuildDefaultFolderMappings();
				IMAPMailbox.hasCreatedMappingTable = true;
			}
			if (log != null)
			{
				foreach (KeyValuePair<string, KeyedPair<string, DefaultFolderType>> keyValuePair in IMAPMailbox.preferredDefaultFolderMappings)
				{
					log.LogDebugging((TSLID)870UL, IMAPClient.Tracer, "Preferred Mapping: {0} => {1}", new object[]
					{
						keyValuePair.Key,
						keyValuePair.Value.Second
					});
				}
				foreach (KeyValuePair<string, DefaultFolderType> keyValuePair2 in IMAPMailbox.secondaryDefaultFolderMappings)
				{
					log.LogDebugging((TSLID)871UL, IMAPClient.Tracer, "Default Mapping: {0} => {1}", new object[]
					{
						keyValuePair2.Key,
						keyValuePair2.Value
					});
				}
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00025D6C File Offset: 0x00023F6C
		internal static DefaultFolderType GetDefaultFolderType(string mailboxName, out bool preferredMapping, out bool exactCaseSensitiveMatch)
		{
			DefaultFolderType result = DefaultFolderType.None;
			preferredMapping = false;
			exactCaseSensitiveMatch = false;
			KeyedPair<string, DefaultFolderType> keyedPair;
			if (IMAPMailbox.preferredDefaultFolderMappings.TryGetValue(mailboxName, out keyedPair))
			{
				preferredMapping = true;
				result = keyedPair.Second;
				exactCaseSensitiveMatch = (0 == string.Compare(mailboxName, keyedPair.First, StringComparison.Ordinal));
			}
			else
			{
				IMAPMailbox.secondaryDefaultFolderMappings.TryGetValue(mailboxName, out result);
			}
			return result;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00025DC0 File Offset: 0x00023FC0
		internal static DefaultFolderType GetDefaultFolderType(string mailboxName)
		{
			bool flag = false;
			bool flag2 = false;
			return IMAPMailbox.GetDefaultFolderType(mailboxName, out flag, out flag2);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00025DDB File Offset: 0x00023FDB
		internal void Rename(string newName)
		{
			this.name = newName;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00025DE4 File Offset: 0x00023FE4
		private static void BuildDefaultFolderMappings()
		{
			IMAPMailbox.preferredDefaultFolderMappings = new Dictionary<string, KeyedPair<string, DefaultFolderType>>(6, StringComparer.OrdinalIgnoreCase);
			IMAPMailbox.secondaryDefaultFolderMappings = new Dictionary<string, DefaultFolderType>(14, StringComparer.OrdinalIgnoreCase);
			IMAPMailbox.preferredDefaultFolderMappings.Add(IMAPMailbox.Inbox, new KeyedPair<string, DefaultFolderType>(IMAPMailbox.Inbox, DefaultFolderType.Inbox));
			IMAPMailbox.AddDefaultMappings();
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
					IMAPMailbox.AddDefaultMappings();
					goto IL_73;
				}
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
			}
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00025E9C File Offset: 0x0002409C
		private static void AddPreferredMapping(string folderNameUTF7, DefaultFolderType type)
		{
			IMAPMailbox.preferredDefaultFolderMappings[folderNameUTF7] = new KeyedPair<string, DefaultFolderType>(folderNameUTF7, type);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00025EB0 File Offset: 0x000240B0
		private static void AddDefaultMappings()
		{
			IMAPMailbox.AddPreferredMapping(IMAPUtils.ToModifiedUTF7(Strings.IMAPDeletedItems), DefaultFolderType.DeletedItems);
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPUtils.ToModifiedUTF7(Strings.IMAPDeletedMessages)] = DefaultFolderType.DeletedItems;
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPUtils.ToModifiedUTF7(Strings.IMAPTrash)] = DefaultFolderType.DeletedItems;
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPMailbox.InboxPrefix + IMAPUtils.ToModifiedUTF7(Strings.IMAPTrash)] = DefaultFolderType.DeletedItems;
			IMAPMailbox.secondaryDefaultFolderMappings["[Gmail]/" + IMAPUtils.ToModifiedUTF7(Strings.IMAPTrash)] = DefaultFolderType.DeletedItems;
			IMAPMailbox.AddPreferredMapping(IMAPUtils.ToModifiedUTF7(Strings.IMAPDrafts), DefaultFolderType.Drafts);
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPUtils.ToModifiedUTF7(Strings.IMAPDraft)] = DefaultFolderType.Drafts;
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPMailbox.InboxPrefix + IMAPUtils.ToModifiedUTF7(Strings.IMAPDrafts)] = DefaultFolderType.Drafts;
			IMAPMailbox.secondaryDefaultFolderMappings["[Gmail]/" + IMAPUtils.ToModifiedUTF7(Strings.IMAPDrafts)] = DefaultFolderType.Drafts;
			IMAPMailbox.AddPreferredMapping(IMAPUtils.ToModifiedUTF7(Strings.IMAPJunkEmail), DefaultFolderType.JunkEmail);
			IMAPMailbox.AddPreferredMapping("[Gmail]/" + IMAPUtils.ToModifiedUTF7(Strings.IMAPSpam), DefaultFolderType.JunkEmail);
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPUtils.ToModifiedUTF7(Strings.IMAPSpam)] = DefaultFolderType.JunkEmail;
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPUtils.ToModifiedUTF7(Strings.IMAPJunk)] = DefaultFolderType.JunkEmail;
			IMAPMailbox.secondaryDefaultFolderMappings["[Gmail]/" + IMAPUtils.ToModifiedUTF7(Strings.IMAPAllMail)] = DefaultFolderType.JunkEmail;
			IMAPMailbox.AddPreferredMapping(IMAPUtils.ToModifiedUTF7(Strings.IMAPSentItems), DefaultFolderType.SentItems);
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPUtils.ToModifiedUTF7(Strings.IMAPSentMessages)] = DefaultFolderType.SentItems;
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPUtils.ToModifiedUTF7(Strings.IMAPSent)] = DefaultFolderType.SentItems;
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPMailbox.InboxPrefix + IMAPUtils.ToModifiedUTF7(Strings.IMAPSentItems)] = DefaultFolderType.SentItems;
			IMAPMailbox.secondaryDefaultFolderMappings[IMAPMailbox.InboxPrefix + IMAPUtils.ToModifiedUTF7(Strings.IMAPSent)] = DefaultFolderType.SentItems;
			IMAPMailbox.secondaryDefaultFolderMappings["[Gmail]/" + IMAPUtils.ToModifiedUTF7(Strings.IMAPSentMail)] = DefaultFolderType.SentItems;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0002610C File Offset: 0x0002430C
		private int GetLastIndexOfSeparator()
		{
			int result = -1;
			if (!string.IsNullOrEmpty(this.Name))
			{
				char value = (this.Separator != null) ? this.Separator.Value : IMAPFolder.DefaultHierarchySeparator;
				result = this.Name.LastIndexOf(value);
			}
			return result;
		}

		// Token: 0x040007CA RID: 1994
		private const int NumPreferredMappings = 6;

		// Token: 0x040007CB RID: 1995
		private const int NumSecondaryMappings = 14;

		// Token: 0x040007CC RID: 1996
		private const string GmailPrefix = "[Gmail]/";

		// Token: 0x040007CD RID: 1997
		internal static readonly string Inbox = "INBOX";

		// Token: 0x040007CE RID: 1998
		private static readonly string InboxPrefix = IMAPMailbox.Inbox + ".";

		// Token: 0x040007CF RID: 1999
		private static Dictionary<string, KeyedPair<string, DefaultFolderType>> preferredDefaultFolderMappings;

		// Token: 0x040007D0 RID: 2000
		private static Dictionary<string, DefaultFolderType> secondaryDefaultFolderMappings;

		// Token: 0x040007D1 RID: 2001
		private static object mappingTableLock = new object();

		// Token: 0x040007D2 RID: 2002
		private static bool hasCreatedMappingTable = false;

		// Token: 0x040007D3 RID: 2003
		private readonly string nameOnTheWire;

		// Token: 0x040007D4 RID: 2004
		private string name;

		// Token: 0x040007D5 RID: 2005
		private char? separator;

		// Token: 0x040007D6 RID: 2006
		private bool selectable;

		// Token: 0x040007D7 RID: 2007
		private bool? hasChildren;

		// Token: 0x040007D8 RID: 2008
		private bool noInferiors;

		// Token: 0x040007D9 RID: 2009
		private bool writable = true;

		// Token: 0x040007DA RID: 2010
		private IMAPMailFlags permanentFlags;

		// Token: 0x040007DB RID: 2011
		private long? uidValidity;

		// Token: 0x040007DC RID: 2012
		private long? uidNext;

		// Token: 0x040007DD RID: 2013
		private int? numberOfMessages;
	}
}
