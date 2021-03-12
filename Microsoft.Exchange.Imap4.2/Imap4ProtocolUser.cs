using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200001B RID: 27
	internal class Imap4ProtocolUser : ProtocolUser
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00009DBC File Offset: 0x00007FBC
		internal Imap4ProtocolUser(ProtocolSession session) : base(session)
		{
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00009DC5 File Offset: 0x00007FC5
		public override ExEventLog.EventTuple UserExceededNumberOfConnectionsEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_UserExceededNumberOfConnections;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00009DCC File Offset: 0x00007FCC
		public override ExEventLog.EventTuple OwaServerNotFoundEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_OwaServerNotFound;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00009DD3 File Offset: 0x00007FD3
		public override ExEventLog.EventTuple OwaServerInvalidEventTuple
		{
			get
			{
				return Imap4EventLogConstants.Tuple_OwaServerInvalid;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00009DDA File Offset: 0x00007FDA
		public Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				return this.maxReceiveSize;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009DE4 File Offset: 0x00007FE4
		protected internal override void Configure(ADUser adUser)
		{
			base.Configure(adUser, CASMailboxSchema.ImapEnabled, CASMailboxSchema.ImapUseProtocolDefaults, CASMailboxSchema.ImapMessagesRetrievalMimeFormat, CASMailboxSchema.ImapEnableExactRFC822Size, CASMailboxSchema.ImapProtocolLoggingEnabled, CASMailboxSchema.ImapSuppressReadReceipt, CASMailboxSchema.ImapForceICalForCalendarRetrievalOption);
			this.maxReceiveSize = adUser.MaxReceiveSize;
		}

		// Token: 0x040000FD RID: 253
		private Unlimited<ByteQuantifiedSize> maxReceiveSize;
	}
}
