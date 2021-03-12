using System;
using System.Text;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000141 RID: 321
	internal class InstantMessagePayload : IPendingRequestNotifier
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000AE2 RID: 2786 RVA: 0x0004D404 File Offset: 0x0004B604
		// (remove) Token: 0x06000AE3 RID: 2787 RVA: 0x0004D43C File Offset: 0x0004B63C
		public event EventHandler<EventArgs> ChangeUserPresenceAfterInactivity;

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0004D471 File Offset: 0x0004B671
		public bool ShouldThrottle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0004D474 File Offset: 0x0004B674
		internal InstantMessagePayload(UserContext userContext)
		{
			this.payloadString = new StringBuilder(256);
			this.userContext = userContext;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0004D493 File Offset: 0x0004B693
		internal void RegisterWithPendingRequestNotifier()
		{
			if (this.userContext != null && this.userContext.PendingRequestManager != null)
			{
				this.userContext.PendingRequestManager.AddPendingRequestNotifier(this);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000AE7 RID: 2791 RVA: 0x0004D4BC File Offset: 0x0004B6BC
		// (remove) Token: 0x06000AE8 RID: 2792 RVA: 0x0004D4F4 File Offset: 0x0004B6F4
		public event DataAvailableEventHandler DataAvailable;

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0004D529 File Offset: 0x0004B729
		public int Length
		{
			get
			{
				return this.payloadString.Length;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0004D538 File Offset: 0x0004B738
		public virtual string ReadDataAndResetState()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug<string>((long)this.GetHashCode(), "InstantMessagePayload.ReadDataAndResetState. SIP Uri: {0}", this.GetUriForUser());
			string result;
			lock (this)
			{
				result = this.payloadString.ToString();
				this.Clear();
			}
			return result;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0004D59C File Offset: 0x0004B79C
		public void Append(string value)
		{
			if (this.payloadString.Length < 1000000)
			{
				if (!this.overMaxSize)
				{
					this.payloadString.Append(value);
					return;
				}
			}
			else
			{
				this.LogPayloadNotPickedEvent();
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0004D5CE File Offset: 0x0004B7CE
		public void Append(int value)
		{
			if (this.payloadString.Length < 1000000)
			{
				if (!this.overMaxSize)
				{
					this.payloadString.Append(value);
					return;
				}
			}
			else
			{
				this.LogPayloadNotPickedEvent();
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0004D600 File Offset: 0x0004B800
		public void Append(StringBuilder value)
		{
			if (this.payloadString.Length < 1000000)
			{
				if (!this.overMaxSize)
				{
					this.payloadString.Append(value);
					return;
				}
			}
			else
			{
				this.LogPayloadNotPickedEvent();
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0004D632 File Offset: 0x0004B832
		public void Clear()
		{
			this.payloadString.Remove(0, this.payloadString.Length);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0004D64C File Offset: 0x0004B84C
		public void PickupData(int length)
		{
			if (length == 0 && this.payloadString.Length > 0)
			{
				this.DataAvailable(this, new EventArgs());
				ExTraceGlobals.InstantMessagingTracer.TraceDebug<string>((long)this.GetHashCode(), "InstantMessagePayload.PickupData. DataAvailable method called. SIP Uri: {0}", this.GetUriForUser());
			}
			else
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug<string>((long)this.GetHashCode(), "InstantMessagePayload.PickupData. No need to call DataAvailable method. SIP Uri: {0}", this.GetUriForUser());
			}
			if (this.overMaxSize)
			{
				lock (this)
				{
					this.overMaxSize = false;
				}
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0004D6F0 File Offset: 0x0004B8F0
		public void ConnectionAliveTimer()
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug<string>((long)this.GetHashCode(), "InstantMessagePayload.ConnectionAliveTimer. User: {0}", this.GetUriForUser());
			long num = Globals.ApplicationTime - this.userContext.LastUserRequestTime;
			if (num > (long)Globals.ActivityBasedPresenceDuration)
			{
				EventArgs e = new EventArgs();
				EventHandler<EventArgs> changeUserPresenceAfterInactivity = this.ChangeUserPresenceAfterInactivity;
				if (changeUserPresenceAfterInactivity != null)
				{
					changeUserPresenceAfterInactivity(this, e);
				}
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0004D74C File Offset: 0x0004B94C
		protected virtual void Cancel()
		{
			this.Clear();
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0004D754 File Offset: 0x0004B954
		private void LogPayloadNotPickedEvent()
		{
			if (!this.overMaxSize)
			{
				this.overMaxSize = true;
				string uriForUser = this.GetUriForUser();
				ExTraceGlobals.InstantMessagingTracer.TraceError<string>((long)this.GetHashCode(), "InstantMessagePayload.LogPayloadNotPickedEvent. Payload has grown too large without being picked up. User: {0}", uriForUser);
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_PayloadNotBeingPickedup, string.Empty, new object[]
				{
					this.GetUriForUser()
				});
				PendingRequestManager pendingRequestManager = this.userContext.PendingRequestManager;
				if (pendingRequestManager != null)
				{
					ChunkedHttpResponse chunkedHttpResponse = pendingRequestManager.ChunkedHttpResponse;
					if (chunkedHttpResponse != null && chunkedHttpResponse.IsClientConnected)
					{
						InstantMessageUtilities.SendWatsonReport("InstantMessagePayload.LogPayloadNotPickedEvent", this.userContext, new OverflowException(string.Format("Payload has grown too large without being picked up. User: {0}", uriForUser)));
					}
				}
				this.Cancel();
				this.payloadString.Append(InstantMessagePayload.overMaxSizeUnavailablePayload);
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0004D80E File Offset: 0x0004BA0E
		private string GetUriForUser()
		{
			if (this.userContext.InstantMessagingType != InstantMessagingTypeOptions.Ocs)
			{
				return string.Empty;
			}
			return this.userContext.SipUri;
		}

		// Token: 0x040007D6 RID: 2006
		private const int MaxPayloadSize = 1000000;

		// Token: 0x040007D7 RID: 2007
		private const int DefaultPayloadStringSize = 256;

		// Token: 0x040007D8 RID: 2008
		private static string overMaxSizeUnavailablePayload = "UN(" + 2004 + ");";

		// Token: 0x040007D9 RID: 2009
		private volatile bool overMaxSize;

		// Token: 0x040007DA RID: 2010
		private StringBuilder payloadString;

		// Token: 0x040007DB RID: 2011
		protected UserContext userContext;
	}
}
