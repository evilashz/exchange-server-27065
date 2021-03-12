using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Management.AgentLog
{
	// Token: 0x02000003 RID: 3
	internal class AgentLogEntry
	{
		// Token: 0x0400000D RID: 13
		public DateTime Timestamp;

		// Token: 0x0400000E RID: 14
		public string SessionId;

		// Token: 0x0400000F RID: 15
		public IPAddress IPAddress;

		// Token: 0x04000010 RID: 16
		public string MessageId;

		// Token: 0x04000011 RID: 17
		public AgentLogEntry.RoutingAddressWrapper P1FromAddress;

		// Token: 0x04000012 RID: 18
		public List<AgentLogEntry.RoutingAddressWrapper> P2FromAddresses;

		// Token: 0x04000013 RID: 19
		public List<AgentLogEntry.RoutingAddressWrapper> Recipients;

		// Token: 0x04000014 RID: 20
		public string Agent;

		// Token: 0x04000015 RID: 21
		public string Event;

		// Token: 0x04000016 RID: 22
		public AgentAction Action;

		// Token: 0x04000017 RID: 23
		public SmtpResponse SmtpResponse;

		// Token: 0x04000018 RID: 24
		public string Reason;

		// Token: 0x04000019 RID: 25
		public string ReasonData;

		// Token: 0x0400001A RID: 26
		public string Diagnostics;

		// Token: 0x0400001B RID: 27
		public Guid NetworkMsgID;

		// Token: 0x0400001C RID: 28
		public Guid TenantID;

		// Token: 0x0400001D RID: 29
		public string Directionality;

		// Token: 0x02000004 RID: 4
		internal struct RoutingAddressWrapper
		{
			// Token: 0x06000019 RID: 25 RVA: 0x00002CE3 File Offset: 0x00000EE3
			public RoutingAddressWrapper(RoutingAddress routingAddress)
			{
				this.routingAddress = routingAddress;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600001A RID: 26 RVA: 0x00002CEC File Offset: 0x00000EEC
			public string LocalPart
			{
				get
				{
					return this.routingAddress.LocalPart;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600001B RID: 27 RVA: 0x00002D08 File Offset: 0x00000F08
			public string DomainPart
			{
				get
				{
					return this.routingAddress.DomainPart;
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600001C RID: 28 RVA: 0x00002D24 File Offset: 0x00000F24
			public bool IsValid
			{
				get
				{
					return this.routingAddress.IsValid;
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600001D RID: 29 RVA: 0x00002D3F File Offset: 0x00000F3F
			public string Name
			{
				get
				{
					return this.ToString();
				}
			}

			// Token: 0x0600001E RID: 30 RVA: 0x00002D50 File Offset: 0x00000F50
			public override string ToString()
			{
				return this.routingAddress.ToString();
			}

			// Token: 0x0400001E RID: 30
			private readonly RoutingAddress routingAddress;
		}
	}
}
