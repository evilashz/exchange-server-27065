using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A6 RID: 422
	internal class PlayOnPhoneStateProvider : DisposeTrackableBase, IPlayOnPhoneStateProvider, IDisposable
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x0003ADA3 File Offset: 0x00038FA3
		public PlayOnPhoneStateProvider(UserContext userContext)
		{
			this.client = new UMClientCommon(userContext.ExchangePrincipal);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003ADBC File Offset: 0x00038FBC
		public virtual UMCallState GetCallState(string callId)
		{
			UMCallState result = UMCallState.Disconnected;
			try
			{
				result = this.client.GetCallInfo(callId).CallState;
			}
			catch (InvalidCallIdException)
			{
			}
			return result;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003ADF4 File Offset: 0x00038FF4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.client != null)
			{
				this.client.Dispose();
				this.client = null;
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003AE13 File Offset: 0x00039013
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PlayOnPhoneStateProvider>(this);
		}

		// Token: 0x0400092B RID: 2347
		private UMClientCommon client;
	}
}
