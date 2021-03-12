using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000139 RID: 313
	internal sealed class InstantMessageManager : DisposeTrackableBase
	{
		// Token: 0x06000A36 RID: 2614 RVA: 0x00046184 File Offset: 0x00044384
		internal InstantMessageManager(UserContext userContext)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager Constructor.");
			this.userContext = userContext;
			if (userContext.InstantMessagingType == InstantMessagingTypeOptions.Ocs)
			{
				this.payload = new InstantMessageOCSPayload(userContext);
			}
			else
			{
				this.payload = new InstantMessagePayload(userContext);
			}
			this.payload.RegisterWithPendingRequestNotifier();
			if (userContext.PendingRequestManager != null)
			{
				userContext.PendingRequestManager.ClientDisconnected += this.ClientDisconnected;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00046201 File Offset: 0x00044401
		private void ClientDisconnected(object sender, EventArgs e)
		{
			this.TerminateProvider("ClientDisconnect");
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0004620E File Offset: 0x0004440E
		public InstantMessageProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00046216 File Offset: 0x00044416
		public InstantMessagePayload Payload
		{
			get
			{
				return this.payload;
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00046220 File Offset: 0x00044420
		public void TerminateProvider(string reason)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager.TerminateProvider.");
			InstantMessageProvider instantMessageProvider = Interlocked.Exchange<InstantMessageProvider>(ref this.provider, null);
			if (instantMessageProvider != null)
			{
				if (this.payload != null)
				{
					InstantMessagePayloadUtilities.GenerateUnavailablePayload(this.payload, null, "Disconnected from IM by server due to timeout: " + reason, InstantMessageFailure.ServerTimeout, false);
				}
				instantMessageProvider.Dispose();
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00046280 File Offset: 0x00044480
		public void SignOut()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager.SignOut.");
			InstantMessageProvider instantMessageProvider = Interlocked.Exchange<InstantMessageProvider>(ref this.provider, null);
			if (instantMessageProvider != null)
			{
				if (this.payload != null)
				{
					InstantMessagePayloadUtilities.GenerateUnavailablePayload(this.payload, null, "Signed out manually.", InstantMessageFailure.ClientSignOut, false);
				}
				instantMessageProvider.Dispose();
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000462D8 File Offset: 0x000444D8
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

		// Token: 0x06000A3D RID: 2621 RVA: 0x00046333 File Offset: 0x00044533
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<InstantMessageManager>(this);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0004633C File Offset: 0x0004453C
		public void StartProvider()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager.StartProvider.");
			if (this.userContext.IsSignedOutOfIM())
			{
				if (this.payload != null)
				{
					InstantMessagePayloadUtilities.GenerateUnavailablePayload(this.payload, null, "Not signed in because IsSignedOutOfIM was true.", InstantMessageFailure.ClientSignOut, false);
				}
			}
			else if ((this.provider == null || this.provider.IsDisposed) && this.userContext.InstantMessagingType == InstantMessagingTypeOptions.Ocs)
			{
				this.StartOcsProvider();
			}
			if (this.provider != null)
			{
				this.Provider.EstablishSession();
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000463CC File Offset: 0x000445CC
		public void ResetPresence()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageManager.ResetPresence");
			if (this.Provider != null)
			{
				this.Provider.MakeEndpointMostActive();
				if (this.Provider.IsActivityBasedPresenceSet)
				{
					this.Provider.ResetPresence();
				}
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0004641A File Offset: 0x0004461A
		private void StartOcsProvider()
		{
			this.provider = InstantMessageOCSProvider.Create(this.userContext, this.payload);
		}

		// Token: 0x040007AC RID: 1964
		private InstantMessageProvider provider;

		// Token: 0x040007AD RID: 1965
		private InstantMessagePayload payload;

		// Token: 0x040007AE RID: 1966
		private UserContext userContext;
	}
}
