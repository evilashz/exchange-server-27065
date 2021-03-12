using System;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000332 RID: 818
	internal sealed class GetStreamingEvents : NotificationCommandBase<GetStreamingEventsRequest, XmlNode>, IAsyncServiceCommand
	{
		// Token: 0x060016FE RID: 5886 RVA: 0x0007A518 File Offset: 0x00078718
		public GetStreamingEvents(CallContext callContext, GetStreamingEventsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0007A524 File Offset: 0x00078724
		internal override IExchangeWebMethodResponse GetResponse()
		{
			if (this.CompleteRequestAsyncCallback == null)
			{
				using (EwsResponseWireWriter ewsResponseWireWriter = EwsResponseWireWriter.Create(CallContext.Current))
				{
					try
					{
						ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "[GetStreamingEvents::GetResponse] Writing the response.");
						GetStreamingEventsSoapResponse getStreamingEventsSoapResponse = new GetStreamingEventsSoapResponse();
						getStreamingEventsSoapResponse.Body = StreamingConnection.CreateErrorResponse(base.Result.Error, base.Request.SubscriptionIds);
						ewsResponseWireWriter.WriteResponseToWire(getStreamingEventsSoapResponse, false);
						getStreamingEventsSoapResponse.Body = StreamingConnection.CreateConnectionResponse(ConnectionStatus.Closed);
						ewsResponseWireWriter.WriteResponseToWire(getStreamingEventsSoapResponse, true);
						ewsResponseWireWriter.FinishWritesAndCompleteResponse(null);
					}
					catch (HttpException arg)
					{
						ExTraceGlobals.SubscriptionsTracer.TraceDebug<HttpException>((long)this.GetHashCode(), "[GetStreamingEvents::GetResponse] Exception occurred while writing the response: {0}", arg);
					}
				}
			}
			return null;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0007A5EC File Offset: 0x000787EC
		internal override ServiceResult<XmlNode> Execute()
		{
			ExTraceGlobals.GetEventsCallTracer.TraceDebug((long)this.GetHashCode(), "GetStreamingEvents.Execute called");
			ServiceCommandBase.ThrowIfNullOrEmpty<string>(base.Request.SubscriptionIds, "SubscriptionIds", "GetStreamingEvents:Execute");
			TimeSpan connectionLifetime = TimeSpan.FromMinutes((double)base.Request.ConnectionTimeout);
			StreamingConnection.CreateConnection(CallContext.Current, base.Request.SubscriptionIds, connectionLifetime, this.CompleteRequestAsyncCallback);
			return new ServiceResult<XmlNode>(null);
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0007A65D File Offset: 0x0007885D
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x0007A665 File Offset: 0x00078865
		public CompleteRequestAsyncCallback CompleteRequestAsyncCallback { get; set; }
	}
}
