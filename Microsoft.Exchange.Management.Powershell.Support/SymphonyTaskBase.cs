using System;
using System.Linq;
using System.Management.Automation;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000036 RID: 54
	public class SymphonyTaskBase : Task
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000D148 File Offset: 0x0000B348
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000D150 File Offset: 0x0000B350
		[Parameter(Mandatory = false)]
		public string Endpoint { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000D159 File Offset: 0x0000B359
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000D161 File Offset: 0x0000B361
		[Parameter(Mandatory = false)]
		public string CertSubject { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000D16A File Offset: 0x0000B36A
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000D172 File Offset: 0x0000B372
		internal Uri WorkloadUri { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000D17B File Offset: 0x0000B37B
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000D183 File Offset: 0x0000B383
		internal X509Certificate2 Certificate { get; set; }

		// Token: 0x060002CE RID: 718 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		internal WorkItemInfo GetWorkItemById(string workItemId)
		{
			WorkItemStatus[] array = (WorkItemStatus[])Enum.GetValues(typeof(WorkItemStatus));
			using (ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler> workloadClient = new ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler>(this.WorkloadUri, this.Certificate))
			{
				WorkItemStatus[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					SymphonyTaskBase.<>c__DisplayClass7 CS$<>8__locals3 = new SymphonyTaskBase.<>c__DisplayClass7();
					CS$<>8__locals3.status = array2[i];
					WorkItemQueryResult result = new WorkItemQueryResult();
					WorkItemInfo workItemInfo;
					do
					{
						workloadClient.CallSymphony(delegate
						{
							result = workloadClient.Proxy.QueryWorkItems(null, null, null, CS$<>8__locals3.status, 1000, result.Bookmark);
						}, this.WorkloadUri.ToString());
						workItemInfo = result.WorkItems.SingleOrDefault((WorkItemInfo w) => string.Equals(w.WorkItemId, workItemId));
					}
					while (workItemInfo == null && result.HasMoreResults);
					if (workItemInfo != null)
					{
						return workItemInfo;
					}
				}
				throw new WorkItemNotFoundException(workItemId);
			}
			WorkItemInfo result2;
			return result2;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		internal WorkItemInfo GetWorkitemByIdAndTenantId(string workItemId, Guid tenantId)
		{
			WorkItemInfo retrieved2;
			using (ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler> workloadClient = new ProxyWrapper<UpgradeHandlerClient, IUpgradeHandler>(this.WorkloadUri, this.Certificate))
			{
				WorkItemInfo retrieved = null;
				workloadClient.CallSymphony(delegate
				{
					retrieved = workloadClient.Proxy.QueryTenantWorkItems(tenantId).SingleOrDefault((WorkItemInfo w) => string.Equals(w.WorkItemId, workItemId));
				}, this.WorkloadUri.ToString());
				if (retrieved == null)
				{
					throw new WorkItemNotFoundException(workItemId);
				}
				retrieved2 = retrieved;
			}
			return retrieved2;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D474 File Offset: 0x0000B674
		protected override void InternalBeginProcessing()
		{
			Uri baseUri = string.IsNullOrEmpty(this.Endpoint) ? UpgradeHandlerContext.AnchorConfig.GetConfig<Uri>("WebServiceUri") : new Uri(this.Endpoint);
			this.WorkloadUri = new Uri(baseUri, "WorkloadService.svc");
			string subject = string.IsNullOrEmpty(this.CertSubject) ? UpgradeHandlerContext.AnchorConfig.GetConfig<string>("CertificateSubject") : this.CertSubject;
			this.Certificate = CertificateHelper.GetExchangeCertificate(subject);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D4ED File Offset: 0x0000B6ED
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D4F6 File Offset: 0x0000B6F6
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is LocalizedException;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000D528 File Offset: 0x0000B728
		protected object GetPropertyValue(PSMemberInfoCollection<PSPropertyInfo> properties, string propertyName)
		{
			object result;
			try
			{
				result = properties.Single((PSPropertyInfo PropertyInfo) => PropertyInfo.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)).Value;
			}
			catch (InvalidOperationException exception)
			{
				base.ThrowTerminatingError(exception, ErrorCategory.InvalidData, properties);
				result = null;
			}
			return result;
		}
	}
}
