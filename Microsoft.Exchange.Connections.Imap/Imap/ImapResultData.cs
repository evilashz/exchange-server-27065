using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class ImapResultData
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00009F5C File Offset: 0x0000815C
		internal ImapResultData()
		{
			this.MessageUids = new List<string>(10);
			this.MessageSizes = new List<long>(10);
			this.MessageIds = new List<string>(10);
			this.MessageFlags = new List<ImapMailFlags>(10);
			this.MessageInternalDates = new List<ExDateTime?>(10);
			this.Mailboxes = new List<ImapMailbox>(10);
			this.MessageSeqNumsHashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			this.MessageSeqNums = new List<int>(10);
			this.Capabilities = new ImapServerCapabilities();
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009FE5 File Offset: 0x000081E5
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00009FED File Offset: 0x000081ED
		internal int? LowestSequenceNumber { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009FF6 File Offset: 0x000081F6
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00009FFE File Offset: 0x000081FE
		internal List<string> MessageUids { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000A007 File Offset: 0x00008207
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000A00F File Offset: 0x0000820F
		internal List<ExDateTime?> MessageInternalDates { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000A018 File Offset: 0x00008218
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000A020 File Offset: 0x00008220
		internal List<long> MessageSizes { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000A029 File Offset: 0x00008229
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000A031 File Offset: 0x00008231
		internal List<string> MessageIds { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000A03A File Offset: 0x0000823A
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000A042 File Offset: 0x00008242
		internal List<ImapMailFlags> MessageFlags { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000A04B File Offset: 0x0000824B
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000A053 File Offset: 0x00008253
		internal List<ImapMailbox> Mailboxes { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000A05C File Offset: 0x0000825C
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000A064 File Offset: 0x00008264
		internal HashSet<string> MessageSeqNumsHashSet { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000A06D File Offset: 0x0000826D
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000A075 File Offset: 0x00008275
		internal List<int> MessageSeqNums { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000A07E File Offset: 0x0000827E
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000A086 File Offset: 0x00008286
		internal ImapServerCapabilities Capabilities { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000A08F File Offset: 0x0000828F
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000A097 File Offset: 0x00008297
		internal Stream MessageStream { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000A0A0 File Offset: 0x000082A0
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000A0A8 File Offset: 0x000082A8
		internal ImapStatus Status { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000A0B1 File Offset: 0x000082B1
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000A0B9 File Offset: 0x000082B9
		internal Exception FailureException { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000A0C2 File Offset: 0x000082C2
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000A0CA File Offset: 0x000082CA
		internal bool UidAlreadySeen { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000A0D3 File Offset: 0x000082D3
		internal bool IsParseSuccessful
		{
			get
			{
				return null == this.FailureException;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000A0E0 File Offset: 0x000082E0
		internal void Clear()
		{
			this.MessageUids.Clear();
			this.MessageSizes.Clear();
			this.MessageIds.Clear();
			this.MessageFlags.Clear();
			this.MessageInternalDates.Clear();
			this.Mailboxes.Clear();
			this.MessageSeqNums.Clear();
			this.MessageSeqNumsHashSet.Clear();
			this.MessageStream = null;
			this.FailureException = null;
			this.Status = ImapStatus.Unknown;
			this.LowestSequenceNumber = null;
		}

		// Token: 0x040000E9 RID: 233
		private const int DefaultCollectionSize = 10;
	}
}
