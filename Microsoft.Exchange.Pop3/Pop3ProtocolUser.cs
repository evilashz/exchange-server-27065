using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000004 RID: 4
	internal class Pop3ProtocolUser : ProtocolUser
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002A58 File Offset: 0x00000C58
		internal Pop3ProtocolUser(ProtocolSession session) : base(session)
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002A61 File Offset: 0x00000C61
		public override ExEventLog.EventTuple UserExceededNumberOfConnectionsEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_UserExceededNumberOfConnections;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002A68 File Offset: 0x00000C68
		public override ExEventLog.EventTuple OwaServerNotFoundEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_OwaServerNotFound;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002A6F File Offset: 0x00000C6F
		public override ExEventLog.EventTuple OwaServerInvalidEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_OwaServerInvalid;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A78 File Offset: 0x00000C78
		protected internal override void Configure(ADUser adUser)
		{
			base.Configure(adUser, CASMailboxSchema.PopEnabled, CASMailboxSchema.PopUseProtocolDefaults, CASMailboxSchema.PopMessagesRetrievalMimeFormat, CASMailboxSchema.PopEnableExactRFC822Size, CASMailboxSchema.PopProtocolLoggingEnabled, CASMailboxSchema.PopSuppressReadReceipt, CASMailboxSchema.PopForceICalForCalendarRetrievalOption);
		}
	}
}
