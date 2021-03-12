using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000007 RID: 7
	public class HTTPStatus200FaultBehavior : BehaviorExtensionElement, IEndpointBehavior
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003283 File Offset: 0x00001483
		public override Type BehaviorType
		{
			get
			{
				return typeof(HTTPStatus200FaultBehavior);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003290 File Offset: 0x00001490
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			HTTPStatus200FaultBehavior.HTTPStatus200FaultMessageInspector item = new HTTPStatus200FaultBehavior.HTTPStatus200FaultMessageInspector();
			endpointDispatcher.DispatchRuntime.MessageInspectors.Add(item);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000032B4 File Offset: 0x000014B4
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000032B6 File Offset: 0x000014B6
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000032B8 File Offset: 0x000014B8
		void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000032BA File Offset: 0x000014BA
		protected override object CreateBehavior()
		{
			return new HTTPStatus200FaultBehavior();
		}

		// Token: 0x02000008 RID: 8
		public class HTTPStatus200FaultMessageInspector : IDispatchMessageInspector
		{
			// Token: 0x0600002D RID: 45 RVA: 0x000032CC File Offset: 0x000014CC
			void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
			{
				if (reply.IsFault)
				{
					HttpResponseMessageProperty httpResponseMessageProperty = new HttpResponseMessageProperty();
					httpResponseMessageProperty.StatusCode = HttpStatusCode.OK;
					reply.Properties[HttpResponseMessageProperty.Name] = httpResponseMessageProperty;
				}
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00003305 File Offset: 0x00001505
			object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
			{
				return null;
			}
		}
	}
}
