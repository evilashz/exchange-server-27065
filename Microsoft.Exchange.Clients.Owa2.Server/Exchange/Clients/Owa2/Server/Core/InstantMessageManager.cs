using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000135 RID: 309
	internal sealed class InstantMessageManager : DisposeTrackableBase
	{
		// Token: 0x06000A62 RID: 2658 RVA: 0x00023BA0 File Offset: 0x00021DA0
		internal InstantMessageManager(UserContext userContext)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager Constructor.");
			this.userContext = userContext;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00023BC5 File Offset: 0x00021DC5
		public InstantMessageProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00023BCD File Offset: 0x00021DCD
		public InstantMessageNotifier Notifier
		{
			get
			{
				return this.notifier;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00023BD8 File Offset: 0x00021DD8
		public void Subscribe(string subscriptionId)
		{
			if (this.notifier == null)
			{
				if (this.userContext.InstantMessageType == InstantMessagingTypeOptions.Ocs)
				{
					this.notifier = new InstantMessageOCSNotifier(this.userContext);
				}
				else
				{
					this.notifier = new InstantMessageNotifier(this.userContext);
				}
			}
			if (!this.notifier.IsRegistered)
			{
				this.notifier.SubscriptionId = subscriptionId;
				this.notifier.RegisterWithPendingRequestNotifier();
				if (this.userContext.PendingRequestManager != null)
				{
					this.userContext.PendingRequestManager.ClientDisconnected += this.ClientDisconnected;
				}
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00023C6C File Offset: 0x00021E6C
		public void TerminateProvider(string reason)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager.TerminateProvider.");
			InstantMessageProvider instantMessageProvider = Interlocked.Exchange<InstantMessageProvider>(ref this.provider, null);
			if (instantMessageProvider != null)
			{
				if (this.notifier != null)
				{
					InstantMessagePayloadUtilities.GenerateUnavailablePayload(this.notifier, null, "Disconnected from IM by server due to timeout: " + reason, InstantMessageServiceError.ServerTimeout, false);
				}
				instantMessageProvider.Dispose();
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00023CCC File Offset: 0x00021ECC
		public void SignOut()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager.SignOut.");
			InstantMessageProvider instantMessageProvider = Interlocked.Exchange<InstantMessageProvider>(ref this.provider, null);
			if (instantMessageProvider != null)
			{
				if (this.notifier != null)
				{
					InstantMessagePayloadUtilities.GenerateUnavailablePayload(this.notifier, null, "Signed out manually.", InstantMessageServiceError.ClientSignOut, false);
				}
				instantMessageProvider.Dispose();
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00023D24 File Offset: 0x00021F24
		public InstantMessageOperationError StartProvider(MailboxSession session)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager.StartProvider.");
			if ((this.provider == null || this.provider.IsDisposed) && this.userContext.InstantMessageType == InstantMessagingTypeOptions.Ocs)
			{
				this.Subscribe("InstantMessageNotification");
				Stopwatch stopwatch = Stopwatch.StartNew();
				this.provider = InstantMessageOCSProvider.Create(this.userContext, this.notifier);
				stopwatch.Stop();
				OwaApplication.GetRequestDetailsLogger.Set(InstantMessageSignIn.LogMetadata.CreateProvider, stopwatch.ElapsedMilliseconds);
			}
			if (this.provider == null)
			{
				return InstantMessageOperationError.UnableToCreateProvider;
			}
			Stopwatch stopwatch2 = Stopwatch.StartNew();
			this.Provider.EstablishSession();
			stopwatch2.Stop();
			OwaApplication.GetRequestDetailsLogger.Set(InstantMessageSignIn.LogMetadata.EstablishSession, stopwatch2.ElapsedMilliseconds);
			Stopwatch stopwatch3 = Stopwatch.StartNew();
			this.Provider.GetExpandedGroups(session);
			stopwatch3.Stop();
			OwaApplication.GetRequestDetailsLogger.Set(InstantMessageSignIn.LogMetadata.GetExpandedGroups, stopwatch3.ElapsedMilliseconds);
			Stopwatch stopwatch4 = Stopwatch.StartNew();
			this.ResetPresence(true);
			stopwatch4.Stop();
			OwaApplication.GetRequestDetailsLogger.Set(InstantMessageSignIn.LogMetadata.ResetPresence, stopwatch4.ElapsedMilliseconds);
			return InstantMessageOperationError.Success;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00023E5A File Offset: 0x0002205A
		public void ResetPresence()
		{
			this.ResetPresence(false);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00023E64 File Offset: 0x00022064
		protected override void InternalDispose(bool isDisposing)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug<bool>((long)this.GetHashCode(), "InstantMessageManager.Dispose. IsDisposing: {0}", isDisposing);
			if (isDisposing)
			{
				if (this.userContext.PendingRequestManager != null)
				{
					this.userContext.PendingRequestManager.ClientDisconnected -= this.ClientDisconnected;
				}
				this.TerminateProvider("Dispose");
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00023EBF File Offset: 0x000220BF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<InstantMessageManager>(this);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00023EC7 File Offset: 0x000220C7
		private void ResetPresence(bool forceReset)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager.ResetPresence");
			if (this.Provider != null && (forceReset || this.Provider.IsActivityBasedPresenceSet))
			{
				this.Provider.ResetPresence();
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00023F03 File Offset: 0x00022103
		private void ClientDisconnected(object sender, EventArgs e)
		{
			this.TerminateProvider("ClientDisconnect");
		}

		// Token: 0x040006D6 RID: 1750
		private InstantMessageProvider provider;

		// Token: 0x040006D7 RID: 1751
		private InstantMessageNotifier notifier;

		// Token: 0x040006D8 RID: 1752
		private UserContext userContext;
	}
}
