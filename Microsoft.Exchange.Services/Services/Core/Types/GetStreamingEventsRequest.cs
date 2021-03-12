using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000451 RID: 1105
	[XmlType("GetStreamingEventsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetStreamingEventsRequest : BaseRequest
	{
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x000A1B9D File Offset: 0x0009FD9D
		// (set) Token: 0x06002074 RID: 8308 RVA: 0x000A1BA5 File Offset: 0x0009FDA5
		[XmlArrayItem(ElementName = "SubscriptionId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[XmlArray(ElementName = "SubscriptionIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string[] SubscriptionIds { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x000A1BAE File Offset: 0x0009FDAE
		// (set) Token: 0x06002076 RID: 8310 RVA: 0x000A1BB6 File Offset: 0x0009FDB6
		[XmlElement("ConnectionTimeout", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public int ConnectionTimeout { get; set; }

		// Token: 0x06002077 RID: 8311 RVA: 0x000A1BBF File Offset: 0x0009FDBF
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetStreamingEvents(callContext, this);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000A1BC8 File Offset: 0x0009FDC8
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000A1BCB File Offset: 0x0009FDCB
		internal override ITask CreateServiceTask<T>(ServiceAsyncResult<T> serviceAsyncResult)
		{
			return new AsyncServiceTask<T>(this, CallContext.Current, serviceAsyncResult);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000A1BD9 File Offset: 0x0009FDD9
		internal override ProxyServiceTask<T> CreateProxyServiceTask<T>(ServiceAsyncResult<T> serviceAsyncResult, CallContext callContext, WebServicesInfo[] services)
		{
			return new HangingProxyServiceTask<T>(this, callContext, serviceAsyncResult, services, StreamingConnection.PeriodicConnectionCheckInterval * 2, new Func<BaseSoapResponse>(this.GetCloseClientConnectionResponse));
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000A1BF7 File Offset: 0x0009FDF7
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000A1BFA File Offset: 0x0009FDFA
		internal override WebServicesInfo[] PerformServiceDiscovery(CallContext callContext)
		{
			if (this.SubscriptionIds.Length != 1)
			{
				return null;
			}
			return BasePullRequest.PerformServiceDiscoveryForSubscriptionId(this.SubscriptionIds[0], callContext, this);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000A1C18 File Offset: 0x0009FE18
		private BaseSoapResponse GetCloseClientConnectionResponse()
		{
			GetStreamingEventsSoapResponse getStreamingEventsSoapResponse = new GetStreamingEventsSoapResponse();
			GetStreamingEventsResponse getStreamingEventsResponse = new GetStreamingEventsResponse();
			GetStreamingEventsResponseMessage getStreamingEventsResponseMessage = new GetStreamingEventsResponseMessage(ServiceResultCode.Success, null);
			getStreamingEventsResponseMessage.SetConnectionStatus(ConnectionStatus.Closed);
			getStreamingEventsResponse.AddResponse(getStreamingEventsResponseMessage);
			getStreamingEventsSoapResponse.Body = getStreamingEventsResponse;
			return getStreamingEventsSoapResponse;
		}
	}
}
