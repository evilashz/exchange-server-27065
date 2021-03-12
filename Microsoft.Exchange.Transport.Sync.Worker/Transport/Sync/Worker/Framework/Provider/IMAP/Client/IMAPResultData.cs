using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001D9 RID: 473
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPResultData
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x00028000 File Offset: 0x00026200
		internal IMAPResultData()
		{
			this.capabilities = new List<string>(10);
			this.messageUids = new List<string>(10);
			this.messageSizes = new List<long>(10);
			this.messageIds = new List<string>(10);
			this.messageFlags = new List<IMAPMailFlags>(10);
			this.messageInternalDates = new List<ExDateTime?>(10);
			this.mailboxes = new List<IMAPMailbox>(10);
			this.messageSeqNumsHashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			this.messageSeqNums = new List<int>(10);
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0002808B File Offset: 0x0002628B
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x00028093 File Offset: 0x00026293
		internal int? LowestSequenceNumber
		{
			get
			{
				return this.lowestSequenceNumber;
			}
			set
			{
				this.lowestSequenceNumber = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0002809C File Offset: 0x0002629C
		internal IList<string> MessageUids
		{
			get
			{
				return this.messageUids;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x000280A4 File Offset: 0x000262A4
		internal IList<ExDateTime?> MessageInternalDates
		{
			get
			{
				return this.messageInternalDates;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x000280AC File Offset: 0x000262AC
		internal IList<long> MessageSizes
		{
			get
			{
				return this.messageSizes;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000280B4 File Offset: 0x000262B4
		internal IList<string> MessageIds
		{
			get
			{
				return this.messageIds;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000280BC File Offset: 0x000262BC
		internal IList<IMAPMailFlags> MessageFlags
		{
			get
			{
				return this.messageFlags;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000280C4 File Offset: 0x000262C4
		internal IList<IMAPMailbox> Mailboxes
		{
			get
			{
				return this.mailboxes;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x000280CC File Offset: 0x000262CC
		internal HashSet<string> MessageSeqNumsHashSet
		{
			get
			{
				return this.messageSeqNumsHashSet;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x000280D4 File Offset: 0x000262D4
		internal List<int> MessageSeqNums
		{
			get
			{
				return this.messageSeqNums;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x000280DC File Offset: 0x000262DC
		internal IList<string> Capabilities
		{
			get
			{
				return this.capabilities;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x000280E4 File Offset: 0x000262E4
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x000280EC File Offset: 0x000262EC
		internal Stream MessageStream
		{
			get
			{
				return this.messageStream;
			}
			set
			{
				this.messageStream = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x000280F5 File Offset: 0x000262F5
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x000280FD File Offset: 0x000262FD
		internal IMAPStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00028106 File Offset: 0x00026306
		// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x0002810E File Offset: 0x0002630E
		internal Exception FailureException
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00028117 File Offset: 0x00026317
		internal bool IsParseSuccessful
		{
			get
			{
				return null == this.exception;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00028122 File Offset: 0x00026322
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x0002812A File Offset: 0x0002632A
		internal bool UidAlreadySeen
		{
			get
			{
				return this.uidAlreadySeen;
			}
			set
			{
				this.uidAlreadySeen = value;
			}
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00028134 File Offset: 0x00026334
		internal void Clear()
		{
			this.messageUids.Clear();
			this.messageSizes.Clear();
			this.messageIds.Clear();
			this.messageFlags.Clear();
			this.messageInternalDates.Clear();
			this.mailboxes.Clear();
			this.messageSeqNums.Clear();
			this.messageSeqNumsHashSet.Clear();
			this.capabilities.Clear();
			this.messageStream = null;
			this.exception = null;
			this.status = IMAPStatus.Unknown;
			this.lowestSequenceNumber = null;
		}

		// Token: 0x04000826 RID: 2086
		private const int DefaultCollectionSize = 10;

		// Token: 0x04000827 RID: 2087
		private List<string> capabilities;

		// Token: 0x04000828 RID: 2088
		private int? lowestSequenceNumber;

		// Token: 0x04000829 RID: 2089
		private List<string> messageUids;

		// Token: 0x0400082A RID: 2090
		private List<long> messageSizes;

		// Token: 0x0400082B RID: 2091
		private List<string> messageIds;

		// Token: 0x0400082C RID: 2092
		private HashSet<string> messageSeqNumsHashSet;

		// Token: 0x0400082D RID: 2093
		private List<ExDateTime?> messageInternalDates;

		// Token: 0x0400082E RID: 2094
		private List<int> messageSeqNums;

		// Token: 0x0400082F RID: 2095
		private List<IMAPMailFlags> messageFlags;

		// Token: 0x04000830 RID: 2096
		private bool uidAlreadySeen;

		// Token: 0x04000831 RID: 2097
		private List<IMAPMailbox> mailboxes;

		// Token: 0x04000832 RID: 2098
		private Stream messageStream;

		// Token: 0x04000833 RID: 2099
		private IMAPStatus status;

		// Token: 0x04000834 RID: 2100
		private Exception exception;
	}
}
