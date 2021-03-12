using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP
{
	// Token: 0x020001DD RID: 477
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPFolder
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x00028B81 File Offset: 0x00026D81
		internal IMAPFolder(IMAPMailbox mailbox)
		{
			this.Mailbox = mailbox;
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00028B90 File Offset: 0x00026D90
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00028B98 File Offset: 0x00026D98
		internal long? NumberOfMessages
		{
			get
			{
				return this.numberOfMessages;
			}
			set
			{
				this.numberOfMessages = value;
				this.changed = true;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00028BA8 File Offset: 0x00026DA8
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x00028BB0 File Offset: 0x00026DB0
		internal long? NewNumberOfMessages
		{
			get
			{
				return this.newNumberOfMessages;
			}
			set
			{
				this.newNumberOfMessages = value;
				this.changed = true;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00028BC0 File Offset: 0x00026DC0
		// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x00028C26 File Offset: 0x00026E26
		internal long Uniqueness
		{
			get
			{
				if (this.uniqueness == null)
				{
					Random random = new Random(ExDateTime.Now.Millisecond);
					int num = random.Next();
					num ^= this.Name.GetHashCode();
					this.uniqueness = new long?((long)((ulong)Math.Abs(num)));
					this.changed = true;
				}
				return this.uniqueness.Value;
			}
			set
			{
				this.uniqueness = new long?(value);
				this.changed = true;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00028C3B File Offset: 0x00026E3B
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x00028C43 File Offset: 0x00026E43
		internal long? LowSyncValue
		{
			get
			{
				return this.lowSyncValue;
			}
			set
			{
				this.lowSyncValue = value;
				this.changed = true;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00028C53 File Offset: 0x00026E53
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x00028C70 File Offset: 0x00026E70
		internal long NewLowSyncValue
		{
			get
			{
				if (this.newLowSyncValue == null)
				{
					return (long)((ulong)-1);
				}
				return this.newLowSyncValue.Value;
			}
			set
			{
				this.newLowSyncValue = new long?(value);
				this.changed = true;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00028C85 File Offset: 0x00026E85
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00028C8D File Offset: 0x00026E8D
		internal long? HighSyncValue
		{
			get
			{
				return this.highSyncValue;
			}
			set
			{
				this.highSyncValue = value;
				this.changed = true;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00028C9D File Offset: 0x00026E9D
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x00028CBA File Offset: 0x00026EBA
		internal long NewHighSyncValue
		{
			get
			{
				if (this.newHighSyncValue == null)
				{
					return 0L;
				}
				return this.newHighSyncValue.Value;
			}
			set
			{
				this.newHighSyncValue = new long?(value);
				this.changed = true;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00028CCF File Offset: 0x00026ECF
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x00028CD8 File Offset: 0x00026ED8
		internal long? ValidityValue
		{
			get
			{
				return this.validityValue;
			}
			set
			{
				if (this.validityValue != null && value != this.validityValue)
				{
					this.lowSyncValue = null;
					this.highSyncValue = null;
					this.newLowSyncValue = null;
					this.newHighSyncValue = null;
				}
				this.validityValue = value;
				this.changed = true;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00028D61 File Offset: 0x00026F61
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00028D69 File Offset: 0x00026F69
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00028D71 File Offset: 0x00026F71
		internal IMAPMailbox Mailbox
		{
			get
			{
				return this.imapMailbox;
			}
			set
			{
				this.imapMailbox = value;
				this.name = this.imapMailbox.Name;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00028D8B File Offset: 0x00026F8B
		internal string CloudId
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00028D93 File Offset: 0x00026F93
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x00028D9B File Offset: 0x00026F9B
		internal bool Visited
		{
			get
			{
				return this.visited;
			}
			set
			{
				this.visited = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00028DA4 File Offset: 0x00026FA4
		internal bool HasCloudVersionChanged
		{
			get
			{
				return this.changed;
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00028DAC File Offset: 0x00026FAC
		internal static string GetShortFolderName(IMAPFolder folder, string fullFolderName)
		{
			string result;
			if (!string.IsNullOrEmpty(fullFolderName))
			{
				char? c = (folder == null) ? new char?(IMAPFolder.DefaultHierarchySeparator) : folder.Mailbox.Separator;
				if (c != null)
				{
					int num = fullFolderName.LastIndexOf(c.Value);
					if (num >= 0)
					{
						IMAPUtils.FromModifiedUTF7(fullFolderName.Substring(num + 1), out result);
						return result;
					}
				}
			}
			IMAPUtils.FromModifiedUTF7(fullFolderName, out result);
			return result;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00028E14 File Offset: 0x00027014
		internal static string CreateNewMailboxName(char separator, SyncFolder syncFolder, string parentCloudFolderId)
		{
			string result = null;
			if (syncFolder != null && syncFolder.DisplayName != null)
			{
				string text = IMAPUtils.ToModifiedUTF7(syncFolder.DisplayName);
				if (parentCloudFolderId == null || parentCloudFolderId.Equals(IMAPFolder.RootCloudFolderId))
				{
					if (text.LastIndexOf(separator) < 0)
					{
						result = text;
					}
				}
				else
				{
					result = parentCloudFolderId + separator + text;
				}
			}
			return result;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00028E6C File Offset: 0x0002706C
		internal bool TryGetParentCloudFolderId(char separator, string cloudId, out string parentFolderName)
		{
			if (cloudId == null)
			{
				cloudId = this.CloudId;
			}
			int num = cloudId.LastIndexOf(separator);
			if (num > 0)
			{
				parentFolderName = cloudId.Substring(0, num);
				return true;
			}
			parentFolderName = null;
			return false;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00028EA0 File Offset: 0x000270A0
		internal void RenameMailboxName(string newName)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newName", newName);
			this.Mailbox.Rename(newName);
			this.name = newName;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00028EC0 File Offset: 0x000270C0
		internal void ReparentMailboxName(string oldParent, string newParent)
		{
			string newName = newParent + this.Name.Substring(oldParent.Length);
			this.RenameMailboxName(newName);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00028EEC File Offset: 0x000270EC
		internal bool IsChildOfCloudFolder(string potentialParentCloudFolderId)
		{
			char? separator = this.Mailbox.Separator;
			char? c = separator;
			int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : null;
			return num != null && this.CloudId.StartsWith(potentialParentCloudFolderId + separator, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00028F4C File Offset: 0x0002714C
		internal bool UpdateFolderCloudVersion(bool transientFailures)
		{
			bool result = false;
			if (!transientFailures)
			{
				if (this.newLowSyncValue != null && this.newHighSyncValue != null)
				{
					if (this.lowSyncValue == null)
					{
						this.lowSyncValue = new long?(this.newLowSyncValue.Value);
					}
					if (this.highSyncValue == null)
					{
						this.highSyncValue = new long?(this.newHighSyncValue.Value);
					}
					if (this.newLowSyncValue.Value > this.highSyncValue.Value || this.newHighSyncValue.Value < this.lowSyncValue.Value)
					{
						this.highSyncValue = new long?(this.newHighSyncValue.Value);
						this.lowSyncValue = new long?(this.newLowSyncValue.Value);
						result = true;
					}
					else
					{
						this.highSyncValue = new long?(Math.Max(this.highSyncValue.Value, this.newHighSyncValue.Value));
						this.lowSyncValue = new long?(Math.Min(this.lowSyncValue.Value, this.newLowSyncValue.Value));
					}
				}
				if (this.lowSyncValue == 1L)
				{
					this.numberOfMessages = this.newNumberOfMessages;
				}
			}
			return result;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000290A0 File Offset: 0x000272A0
		internal string GenerateFolderCloudVersion()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} {3} {4}", new object[]
			{
				this.Uniqueness,
				(this.validityValue != null) ? this.validityValue.Value.ToString(CultureInfo.InvariantCulture) : "NIL",
				(this.lowSyncValue != null) ? this.lowSyncValue.Value.ToString(CultureInfo.InvariantCulture) : "NIL",
				(this.highSyncValue != null) ? this.highSyncValue.Value.ToString(CultureInfo.InvariantCulture) : "NIL",
				(this.numberOfMessages != null) ? this.numberOfMessages.Value.ToString(CultureInfo.InvariantCulture) : "NIL"
			});
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00029194 File Offset: 0x00027394
		internal void InitializeFromCloudFolder(string cloudId, string cloudVersion)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("cloudId", cloudId);
			if (this.name != null && this.name != cloudId)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid cloudId, does not match mailbox name: [CloudId={0}] [MailboxName={1}]", new object[]
				{
					cloudId,
					this.name
				}));
			}
			this.name = cloudId;
			this.uniqueness = null;
			this.validityValue = null;
			this.lowSyncValue = null;
			this.highSyncValue = null;
			this.numberOfMessages = null;
			if (cloudVersion != null && cloudVersion != "DeletedFolder")
			{
				string[] array = cloudVersion.Split(new char[]
				{
					' '
				});
				if (array.Length != 4 && array.Length != 5)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid cloudVersion, could not parse: {0}", new object[]
					{
						cloudVersion
					}));
				}
				if (array[0] != "NIL")
				{
					this.uniqueness = new long?((long)((ulong)uint.Parse(array[0], CultureInfo.InvariantCulture)));
				}
				if (array[1] != "NIL")
				{
					this.validityValue = new long?((long)((ulong)uint.Parse(array[1], CultureInfo.InvariantCulture)));
				}
				if (array[2] != "NIL")
				{
					this.lowSyncValue = new long?((long)((ulong)uint.Parse(array[2], CultureInfo.InvariantCulture)));
				}
				if (array[3] != "NIL")
				{
					this.highSyncValue = new long?((long)((ulong)uint.Parse(array[3], CultureInfo.InvariantCulture)));
				}
				if (array.Length == 5 && array[4] != "NIL")
				{
					this.numberOfMessages = new long?((long)((ulong)uint.Parse(array[4], CultureInfo.InvariantCulture)));
				}
			}
			this.changed = false;
		}

		// Token: 0x04000852 RID: 2130
		internal const string InboxCloudFolderId = "INBOX";

		// Token: 0x04000853 RID: 2131
		internal const string NilValue = "NIL";

		// Token: 0x04000854 RID: 2132
		internal const string DeletedCloudVersionSentinel = "DeletedFolder";

		// Token: 0x04000855 RID: 2133
		private const char TokenDelimiter = ' ';

		// Token: 0x04000856 RID: 2134
		internal static readonly char DefaultHierarchySeparator = '/';

		// Token: 0x04000857 RID: 2135
		internal static readonly string RootCloudFolderId = "&&ROOT";

		// Token: 0x04000858 RID: 2136
		private IMAPMailbox imapMailbox;

		// Token: 0x04000859 RID: 2137
		private long? uniqueness;

		// Token: 0x0400085A RID: 2138
		private long? lowSyncValue;

		// Token: 0x0400085B RID: 2139
		private long? highSyncValue;

		// Token: 0x0400085C RID: 2140
		private long? newLowSyncValue;

		// Token: 0x0400085D RID: 2141
		private long? newHighSyncValue;

		// Token: 0x0400085E RID: 2142
		private long? validityValue;

		// Token: 0x0400085F RID: 2143
		private string name;

		// Token: 0x04000860 RID: 2144
		private bool visited;

		// Token: 0x04000861 RID: 2145
		private bool changed;

		// Token: 0x04000862 RID: 2146
		private long? numberOfMessages;

		// Token: 0x04000863 RID: 2147
		private long? newNumberOfMessages;
	}
}
