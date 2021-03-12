using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000095 RID: 149
	internal class TargetCalculationCallbackBeacon
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x00019DF1 File Offset: 0x00017FF1
		public TargetCalculationCallbackBeacon(AnchoredRoutingTarget anchoredRoutingTarget)
		{
			if (anchoredRoutingTarget == null)
			{
				throw new ArgumentNullException("anchoredRoutingTarget");
			}
			this.AnchoredRoutingTarget = anchoredRoutingTarget;
			this.AnchorMailbox = this.AnchoredRoutingTarget.AnchorMailbox;
			this.State = TargetCalculationCallbackState.TargetResolved;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00019E26 File Offset: 0x00018026
		public TargetCalculationCallbackBeacon(AnchorMailbox anchorMailbox, BackEndServer mailboxServer)
		{
			if (anchorMailbox == null)
			{
				throw new ArgumentNullException("anchorMailbox");
			}
			if (mailboxServer == null)
			{
				throw new ArgumentNullException("mailboxServer");
			}
			this.AnchorMailbox = anchorMailbox;
			this.MailboxServer = mailboxServer;
			this.State = TargetCalculationCallbackState.MailboxServerResolved;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00019E60 File Offset: 0x00018060
		public TargetCalculationCallbackBeacon(MailboxServerLocatorAsyncState mailboxServerLocatorAsyncState, IAsyncResult mailboxServerLocatorAsyncResult)
		{
			if (mailboxServerLocatorAsyncState == null)
			{
				throw new ArgumentNullException("mailboxServerLocatorAsyncState");
			}
			if (mailboxServerLocatorAsyncResult == null)
			{
				throw new ArgumentNullException("mailboxServerLocatorAsyncResult");
			}
			this.MailboxServerLocatorAsyncState = mailboxServerLocatorAsyncState;
			this.MailboxServerLocatorAsyncResult = mailboxServerLocatorAsyncResult;
			this.AnchorMailbox = this.MailboxServerLocatorAsyncState.AnchorMailbox;
			this.State = TargetCalculationCallbackState.LocatorCallback;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00019EB5 File Offset: 0x000180B5
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00019EBD File Offset: 0x000180BD
		public TargetCalculationCallbackState State { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00019EC6 File Offset: 0x000180C6
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00019ECE File Offset: 0x000180CE
		public AnchoredRoutingTarget AnchoredRoutingTarget { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00019ED7 File Offset: 0x000180D7
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00019EDF File Offset: 0x000180DF
		public AnchorMailbox AnchorMailbox { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00019EE8 File Offset: 0x000180E8
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00019EF0 File Offset: 0x000180F0
		public BackEndServer MailboxServer { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00019EF9 File Offset: 0x000180F9
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00019F01 File Offset: 0x00018101
		public MailboxServerLocatorAsyncState MailboxServerLocatorAsyncState { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00019F0A File Offset: 0x0001810A
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00019F12 File Offset: 0x00018112
		public IAsyncResult MailboxServerLocatorAsyncResult { get; private set; }
	}
}
