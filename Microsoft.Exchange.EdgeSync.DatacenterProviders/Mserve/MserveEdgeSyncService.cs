using System;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.Net.Mserve;
using Microsoft.Exchange.Net.Mserve.ProvisionRequest;
using Microsoft.Exchange.Net.Mserve.ProvisionResponse;

namespace Microsoft.Exchange.EdgeSync.Mserve
{
	// Token: 0x02000034 RID: 52
	internal class MserveEdgeSyncService : MserveWebService
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000F1A2 File Offset: 0x0000D3A2
		public MserveEdgeSyncService(string provisionUrl, string settingsUrl, string remoteCertSubject, string clientToken, EdgeSyncLogSession logSession, bool batchMode, bool trackDuplicatedAddEntries) : base(provisionUrl, settingsUrl, remoteCertSubject, clientToken, batchMode)
		{
			base.TrackDuplicatedAddEntries = trackDuplicatedAddEntries;
			this.logSession = logSession;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		protected override void LogProvisionAccountDetail(string type, Microsoft.Exchange.Net.Mserve.ProvisionRequest.AccountType account)
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.AppendFormat("Type:{0} Name:{1} PartnerId:{2}", type, account.Name, account.PartnerID);
			this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, stringBuilder.ToString(), "ProvisionRequest");
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000F210 File Offset: 0x0000D410
		protected override void LogResponseAccountDetail(OperationType type, Microsoft.Exchange.Net.Mserve.ProvisionResponse.AccountType account)
		{
			StringBuilder stringBuilder;
			if (account.Fault != null)
			{
				stringBuilder = new StringBuilder(300);
				stringBuilder.AppendFormat("Type:{0} Name:{1} PartnerId:{2} Status:{3} Fault:{4} FaultCode:{5} FaultDetail:{6}", new object[]
				{
					type,
					account.Name,
					account.PartnerID,
					account.Status,
					account.Fault.Faultstring,
					account.Fault.Faultcode,
					account.Fault.Detail
				});
			}
			else
			{
				stringBuilder = new StringBuilder(200);
				stringBuilder.AppendFormat("Type:{0} Name:{1} PartnerId:{2} Status:{3}", new object[]
				{
					type,
					account.Name,
					account.PartnerID,
					account.Status
				});
			}
			this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, stringBuilder.ToString(), "ProvisionResponse");
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		protected override void LogProvisionResponse(Microsoft.Exchange.Net.Mserve.ProvisionResponse.Provision response)
		{
			StringBuilder stringBuilder;
			if (response.Fault != null)
			{
				stringBuilder = new StringBuilder(200);
				stringBuilder.AppendFormat("Status:{0} Fault:{1} FaultCode:{2} FaultDetail:{3}", new object[]
				{
					response.Status,
					response.Fault.Faultstring,
					response.Fault.Faultcode,
					response.Fault.Detail
				});
			}
			else
			{
				stringBuilder = new StringBuilder(20);
				stringBuilder.AppendFormat("Status:{0}", response.Status);
			}
			this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, stringBuilder.ToString(), "ProvisionRootResponse");
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000F3A1 File Offset: 0x0000D5A1
		protected override void LogProvisionRequest(string url)
		{
			this.logSession.LogEvent(EdgeSyncLoggingLevel.Medium, EdgeSyncEvent.TargetConnection, url, "Sending a new batch of provision requests");
		}

		// Token: 0x040000F0 RID: 240
		private readonly EdgeSyncLogSession logSession;
	}
}
