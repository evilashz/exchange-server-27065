using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ProvisioningAgent;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200002E RID: 46
	internal class ProfileService
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0000B130 File Offset: 0x00009330
		private ProfileService()
		{
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000B143 File Offset: 0x00009343
		internal static ProfileService Instance
		{
			get
			{
				if (ProfileService.instance == null)
				{
					Interlocked.CompareExchange<ProfileService>(ref ProfileService.instance, new ProfileService(), null);
				}
				return ProfileService.instance;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000B176 File Offset: 0x00009376
		internal LiveIdManager LiveIdManager
		{
			get
			{
				if (this.liveIdManager == null)
				{
					this.liveIdManager = PassportLiveIdManager.CreateLiveIdManager(LiveIdInstanceType.Business, delegate(LocalizedException exception, ExchangeErrorCategory category)
					{
						ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError(0L, exception.ToString());
					}, null, null, false);
				}
				return this.liveIdManager;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B1B4 File Offset: 0x000093B4
		internal DateTime GetAccountCreationTime(string memberName)
		{
			DateTime result;
			lock (this.lockObject)
			{
				LiveIdManager liveIdManager = this.LiveIdManager;
				DateTime accountCreationTime = liveIdManager.GetAccountCreationTime(new SmtpAddress(memberName));
				result = accountCreationTime;
			}
			return result;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B208 File Offset: 0x00009408
		private void DisposeLiveIdManager()
		{
			if (this.liveIdManager != null)
			{
				this.liveIdManager.Dispose();
				this.liveIdManager = null;
			}
		}

		// Token: 0x04000173 RID: 371
		private static ProfileService instance;

		// Token: 0x04000174 RID: 372
		private object lockObject = new object();

		// Token: 0x04000175 RID: 373
		private LiveIdManager liveIdManager;
	}
}
