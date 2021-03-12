using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ImapFolder
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00005BAA File Offset: 0x00003DAA
		internal ImapFolder(ImapMailbox mailbox)
		{
			this.Mailbox = mailbox;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005BB9 File Offset: 0x00003DB9
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00005BC1 File Offset: 0x00003DC1
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005BD1 File Offset: 0x00003DD1
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00005BD9 File Offset: 0x00003DD9
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005BEC File Offset: 0x00003DEC
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00005C52 File Offset: 0x00003E52
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005C67 File Offset: 0x00003E67
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00005C6F File Offset: 0x00003E6F
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005C7F File Offset: 0x00003E7F
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00005C9C File Offset: 0x00003E9C
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005CB1 File Offset: 0x00003EB1
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00005CB9 File Offset: 0x00003EB9
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005CC9 File Offset: 0x00003EC9
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00005CE6 File Offset: 0x00003EE6
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005CFB File Offset: 0x00003EFB
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00005D04 File Offset: 0x00003F04
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005D8D File Offset: 0x00003F8D
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005D95 File Offset: 0x00003F95
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005D9D File Offset: 0x00003F9D
		internal ImapMailbox Mailbox
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005DB7 File Offset: 0x00003FB7
		internal string CloudId
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005DBF File Offset: 0x00003FBF
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00005DC7 File Offset: 0x00003FC7
		internal bool Visited { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005DD0 File Offset: 0x00003FD0
		internal bool HasCloudVersionChanged
		{
			get
			{
				return this.changed;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005DD8 File Offset: 0x00003FD8
		internal static string GetShortFolderName(ImapFolder folder, string fullFolderName)
		{
			string result;
			if (!string.IsNullOrEmpty(fullFolderName))
			{
				char? c = (folder == null) ? new char?('/') : folder.Mailbox.Separator;
				if (c != null)
				{
					int num = fullFolderName.LastIndexOf(c.Value);
					if (num >= 0)
					{
						ImapUtilities.FromModifiedUTF7(fullFolderName.Substring(num + 1), out result);
						return result;
					}
				}
			}
			ImapUtilities.FromModifiedUTF7(fullFolderName, out result);
			return result;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005E40 File Offset: 0x00004040
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

		// Token: 0x060000EE RID: 238 RVA: 0x00005E74 File Offset: 0x00004074
		internal void RenameMailboxName(string newName)
		{
			this.Mailbox.Rename(newName);
			this.name = newName;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005E8C File Offset: 0x0000408C
		internal void ReparentMailboxName(string oldParent, string newParent)
		{
			string newName = newParent + this.Name.Substring(oldParent.Length);
			this.RenameMailboxName(newName);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005EB8 File Offset: 0x000040B8
		internal bool IsChildOfCloudFolder(string potentialParentCloudFolderId)
		{
			char? separator = this.Mailbox.Separator;
			char? c = separator;
			int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : null;
			return num != null && this.CloudId.StartsWith(potentialParentCloudFolderId + separator, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005F18 File Offset: 0x00004118
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

		// Token: 0x060000F2 RID: 242 RVA: 0x0000606C File Offset: 0x0000426C
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

		// Token: 0x060000F3 RID: 243 RVA: 0x00006160 File Offset: 0x00004360
		internal void InitializeFromCloudFolder(string cloudId, string cloudVersion)
		{
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

		// Token: 0x04000069 RID: 105
		internal const string DeletedCloudVersionSentinel = "DeletedFolder";

		// Token: 0x0400006A RID: 106
		private const char TokenDelimiter = ' ';

		// Token: 0x0400006B RID: 107
		internal static readonly string RootCloudFolderId = "&&ROOT";

		// Token: 0x0400006C RID: 108
		private ImapMailbox imapMailbox;

		// Token: 0x0400006D RID: 109
		private long? uniqueness;

		// Token: 0x0400006E RID: 110
		private long? lowSyncValue;

		// Token: 0x0400006F RID: 111
		private long? highSyncValue;

		// Token: 0x04000070 RID: 112
		private long? newLowSyncValue;

		// Token: 0x04000071 RID: 113
		private long? newHighSyncValue;

		// Token: 0x04000072 RID: 114
		private long? validityValue;

		// Token: 0x04000073 RID: 115
		private string name;

		// Token: 0x04000074 RID: 116
		private bool changed;

		// Token: 0x04000075 RID: 117
		private long? numberOfMessages;

		// Token: 0x04000076 RID: 118
		private long? newNumberOfMessages;
	}
}
