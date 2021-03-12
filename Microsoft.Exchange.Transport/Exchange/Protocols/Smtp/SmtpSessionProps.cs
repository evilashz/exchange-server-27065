using System;
using System.Net;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000418 RID: 1048
	[Serializable]
	internal class SmtpSessionProps : ISmtpSession
	{
		// Token: 0x0600303B RID: 12347 RVA: 0x000C0729 File Offset: 0x000BE929
		private SmtpSessionProps()
		{
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000C0731 File Offset: 0x000BE931
		public SmtpSessionProps(ulong id)
		{
			this.sessionId = id;
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x000C0740 File Offset: 0x000BE940
		public ulong SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x000C0748 File Offset: 0x000BE948
		// (set) Token: 0x0600303F RID: 12351 RVA: 0x000C0750 File Offset: 0x000BE950
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
			internal set
			{
				this.remoteEndPoint = value;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x000C0759 File Offset: 0x000BE959
		// (set) Token: 0x06003041 RID: 12353 RVA: 0x000C0761 File Offset: 0x000BE961
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.localEndPoint;
			}
			internal set
			{
				this.localEndPoint = value;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x000C076A File Offset: 0x000BE96A
		// (set) Token: 0x06003043 RID: 12355 RVA: 0x000C0772 File Offset: 0x000BE972
		public string HelloDomain
		{
			get
			{
				return this.helloDomain;
			}
			set
			{
				this.helloDomain = value;
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000C077B File Offset: 0x000BE97B
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x000C0783 File Offset: 0x000BE983
		public SmtpResponse Banner
		{
			get
			{
				return this.banner;
			}
			set
			{
				this.banner = value;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x000C078C File Offset: 0x000BE98C
		// (set) Token: 0x06003047 RID: 12359 RVA: 0x000C0794 File Offset: 0x000BE994
		public IEhloOptions AdvertisedEhloOptions
		{
			get
			{
				return this.advertisedEhloOptions;
			}
			internal set
			{
				this.advertisedEhloOptions = value;
			}
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000C079D File Offset: 0x000BE99D
		public void Disconnect(DisconnectReason disconnectReason)
		{
			this.shouldDisconnect = true;
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x000C07A6 File Offset: 0x000BE9A6
		// (set) Token: 0x0600304A RID: 12362 RVA: 0x000C07AE File Offset: 0x000BE9AE
		internal bool ShouldDisconnect
		{
			get
			{
				return this.shouldDisconnect;
			}
			set
			{
				this.shouldDisconnect = value;
			}
		}

		// Token: 0x04001794 RID: 6036
		private readonly ulong sessionId;

		// Token: 0x04001795 RID: 6037
		private IPEndPoint remoteEndPoint;

		// Token: 0x04001796 RID: 6038
		private IPEndPoint localEndPoint;

		// Token: 0x04001797 RID: 6039
		private string helloDomain;

		// Token: 0x04001798 RID: 6040
		private SmtpResponse banner;

		// Token: 0x04001799 RID: 6041
		private IEhloOptions advertisedEhloOptions;

		// Token: 0x0400179A RID: 6042
		private bool shouldDisconnect;
	}
}
