using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Pickup
{
	// Token: 0x02000523 RID: 1315
	internal sealed class PickupComponent : IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x06003D52 RID: 15698 RVA: 0x000FFE80 File Offset: 0x000FE080
		public PickupComponent(IPickupSubmitHandler submitHandler)
		{
			if (submitHandler == null)
			{
				throw new ArgumentException("Submission handler is not provided");
			}
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Creating new Pickup Component");
			this.simpleDirectory = new PickupDirectory(PickupType.Pickup, submitHandler);
			this.replayDirectory = new PickupDirectory(PickupType.Replay, submitHandler);
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000FFED1 File Offset: 0x000FE0D1
		public void Load()
		{
			this.RegisterConfigurationChangeHandlers();
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x000FFED9 File Offset: 0x000FE0D9
		public void Unload()
		{
			this.UnregisterConfigurationChangeHandlers();
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000FFEE1 File Offset: 0x000FE0E1
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000FFEE4 File Offset: 0x000FE0E4
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Pickup starting.");
			this.targetRunningState = targetRunningState;
			this.paused = (initiallyPaused || !this.ShouldExecute());
			if (!this.paused)
			{
				this.simpleDirectory.Start();
				this.replayDirectory.Start();
			}
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Pickup started.");
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000FFF58 File Offset: 0x000FE158
		public void Stop()
		{
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Pickup stopping.");
			this.simpleDirectory.Stop();
			this.replayDirectory.Stop();
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Pickup stopped.");
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000FFFA8 File Offset: 0x000FE1A8
		public void Pause()
		{
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Pickup pausing.");
			if (!this.paused)
			{
				this.paused = true;
				this.simpleDirectory.Stop();
				this.replayDirectory.Stop();
			}
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Pickup paused.");
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x00100008 File Offset: 0x000FE208
		public void Continue()
		{
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Pickup resuming.");
			if (this.paused && this.ShouldExecute())
			{
				this.paused = false;
				this.simpleDirectory.Start();
				this.replayDirectory.Start();
			}
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Pickup resumed.");
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06003D5A RID: 15706 RVA: 0x0010006E File Offset: 0x000FE26E
		public string CurrentState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x00100071 File Offset: 0x000FE271
		public void ConfigUpdate(object source, EventArgs args)
		{
			this.TransportServerConfigUpdate(null);
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x0010007A File Offset: 0x000FE27A
		private void RegisterConfigurationChangeHandlers()
		{
			Components.ConfigChanged += this.ConfigUpdate;
			Components.Configuration.LocalServerChanged += this.TransportServerConfigUpdate;
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x001000A3 File Offset: 0x000FE2A3
		private void UnregisterConfigurationChangeHandlers()
		{
			Components.ConfigChanged -= this.ConfigUpdate;
			Components.Configuration.LocalServerChanged -= this.TransportServerConfigUpdate;
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x001000CC File Offset: 0x000FE2CC
		private void Configure()
		{
			this.simpleDirectory.Reconfigure();
			this.replayDirectory.Reconfigure();
			ExTraceGlobals.PickupTracer.TraceDebug((long)this.GetHashCode(), "Pickup Reconfigured.");
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x001000FA File Offset: 0x000FE2FA
		private void TransportServerConfigUpdate(TransportServerConfiguration args)
		{
			if (!this.paused)
			{
				this.Configure();
			}
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x0010010A File Offset: 0x000FE30A
		private bool ShouldExecute()
		{
			return this.targetRunningState == ServiceState.Active;
		}

		// Token: 0x04001F39 RID: 7993
		private PickupDirectory simpleDirectory;

		// Token: 0x04001F3A RID: 7994
		private PickupDirectory replayDirectory;

		// Token: 0x04001F3B RID: 7995
		private bool paused;

		// Token: 0x04001F3C RID: 7996
		private ServiceState targetRunningState;
	}
}
