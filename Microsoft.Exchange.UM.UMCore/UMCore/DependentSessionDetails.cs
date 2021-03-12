using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000112 RID: 274
	internal class DependentSessionDetails
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x0001F5A0 File Offset: 0x0001D7A0
		internal DependentSessionDetails(UMCallSessionHandler<OutboundCallDetailsEventArgs> args, UMSubscriber caller, string callerId, UMSipPeer remotePeer, PhoneNumber numberToCall, BaseUMCallSession parentUMCallSession)
		{
			this.caller = caller;
			this.callerId = callerId;
			this.numberToCall = numberToCall;
			this.remotePeerToUse = remotePeer;
			this.onoutboundCallRequestCompleted = args;
			this.parentUMCallSession = parentUMCallSession;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001F5D5 File Offset: 0x0001D7D5
		internal UMSubscriber Caller
		{
			get
			{
				return this.caller;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001F5DD File Offset: 0x0001D7DD
		internal string CallerID
		{
			get
			{
				return this.callerId;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001F5E5 File Offset: 0x0001D7E5
		internal BaseUMCallSession ParentUMCallSession
		{
			get
			{
				return this.parentUMCallSession;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001F5ED File Offset: 0x0001D7ED
		internal UMSipPeer RemotePeerToUse
		{
			get
			{
				return this.remotePeerToUse;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001F5F5 File Offset: 0x0001D7F5
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0001F5FD File Offset: 0x0001D7FD
		internal PhoneNumber NumberToCall
		{
			get
			{
				return this.numberToCall;
			}
			set
			{
				this.numberToCall = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001F606 File Offset: 0x0001D806
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0001F60E File Offset: 0x0001D80E
		internal BaseUMCallSession DependentUMCallSession
		{
			get
			{
				return this.dependentUMCallSession;
			}
			set
			{
				this.dependentUMCallSession = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001F617 File Offset: 0x0001D817
		internal UMCallSessionHandler<OutboundCallDetailsEventArgs> OutBoundCallConnectedHandler
		{
			get
			{
				return this.onoutboundCallRequestCompleted;
			}
		}

		// Token: 0x0400084A RID: 2122
		private UMCallSessionHandler<OutboundCallDetailsEventArgs> onoutboundCallRequestCompleted;

		// Token: 0x0400084B RID: 2123
		private UMSubscriber caller;

		// Token: 0x0400084C RID: 2124
		private string callerId;

		// Token: 0x0400084D RID: 2125
		private PhoneNumber numberToCall;

		// Token: 0x0400084E RID: 2126
		private UMSipPeer remotePeerToUse;

		// Token: 0x0400084F RID: 2127
		private BaseUMCallSession parentUMCallSession;

		// Token: 0x04000850 RID: 2128
		private BaseUMCallSession dependentUMCallSession;
	}
}
