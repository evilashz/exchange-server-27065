using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200002E RID: 46
	public class SessionInfo
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000A57A File Offset: 0x0000877A
		internal SessionInfo()
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A584 File Offset: 0x00008784
		internal SessionInfo(ProtocolSession protocolSession)
		{
			lock (protocolSession)
			{
				if (protocolSession.LightLogSession != null)
				{
					this.ClientIp = protocolSession.LightLogSession.ClientIp;
					this.ProxyDestination = protocolSession.LightLogSession.ProxyDestination;
				}
				if (protocolSession.Connection != null)
				{
					this.LocalEndPoint = protocolSession.LocalEndPoint.ToString();
					this.RemoteEndPoint = protocolSession.RemoteEndPoint.ToString();
				}
				this.SessionId = protocolSession.SessionId;
				this.Disconnected = protocolSession.Disconnected;
				this.Disposed = protocolSession.Disposed;
				this.NegotiatingTls = protocolSession.NegotiatingTls;
				this.IsTls = protocolSession.IsTls;
				this.Started = protocolSession.Started.UniversalTime;
				this.LastActivityTime = protocolSession.LastActivityTime.UniversalTime;
				if (protocolSession.ResponseFactory != null)
				{
					this.User = protocolSession.ResponseFactory.UserName;
					this.Mailbox = protocolSession.ResponseFactory.Mailbox;
					if (protocolSession.ResponseFactory.ProtocolUser != null)
					{
						this.ConnectionId = protocolSession.ResponseFactory.ProtocolUser.ConnectionIdentity;
					}
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A6D0 File Offset: 0x000088D0
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000A6D8 File Offset: 0x000088D8
		public string ClientIp { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000A6E1 File Offset: 0x000088E1
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000A6E9 File Offset: 0x000088E9
		public string LocalEndPoint { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000A6F2 File Offset: 0x000088F2
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000A6FA File Offset: 0x000088FA
		public string RemoteEndPoint { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000A703 File Offset: 0x00008903
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000A70B File Offset: 0x0000890B
		public long SessionId { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000A714 File Offset: 0x00008914
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000A71C File Offset: 0x0000891C
		public bool Disconnected { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000A725 File Offset: 0x00008925
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000A72D File Offset: 0x0000892D
		public bool Disposed { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000A736 File Offset: 0x00008936
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000A73E File Offset: 0x0000893E
		public bool NegotiatingTls { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000A747 File Offset: 0x00008947
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000A74F File Offset: 0x0000894F
		public bool IsTls { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000A758 File Offset: 0x00008958
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000A760 File Offset: 0x00008960
		public DateTime Started { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000A769 File Offset: 0x00008969
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000A771 File Offset: 0x00008971
		public DateTime LastActivityTime { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000A77A File Offset: 0x0000897A
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000A782 File Offset: 0x00008982
		public string User { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000A78B File Offset: 0x0000898B
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000A793 File Offset: 0x00008993
		public string ConnectionId { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000A79C File Offset: 0x0000899C
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000A7A4 File Offset: 0x000089A4
		public string Mailbox { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000A7AD File Offset: 0x000089AD
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000A7B5 File Offset: 0x000089B5
		public string ProxyDestination { get; set; }
	}
}
