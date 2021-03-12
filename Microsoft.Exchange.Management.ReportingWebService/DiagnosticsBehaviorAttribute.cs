using System;
using System.Collections.ObjectModel;
using System.Data.Services;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200001D RID: 29
	[AttributeUsage(AttributeTargets.Class)]
	public class DiagnosticsBehaviorAttribute : Attribute, IServiceBehavior, IErrorHandler
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000356E File Offset: 0x0000176E
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003570 File Offset: 0x00001770
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = (ChannelDispatcher)channelDispatcherBase;
				foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
				{
					endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(this);
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003604 File Offset: 0x00001804
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003606 File Offset: 0x00001806
		bool IErrorHandler.HandleError(Exception error)
		{
			return false;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000360C File Offset: 0x0000180C
		void IErrorHandler.ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			ServiceDiagnostics.ReportUnhandledException(error, HttpContext.Current);
			string text = HttpContext.Current.Request.QueryString["$format"];
			string text2 = HttpContext.Current.Request.Headers["Accept"];
			XmlObjectSerializer serializer;
			WebBodyFormatMessageProperty property;
			if ((text != null && text.Equals("json", StringComparison.InvariantCultureIgnoreCase)) || (text2 != null && text2.Equals("application/json", StringComparison.InvariantCultureIgnoreCase)))
			{
				serializer = new DataContractJsonSerializer(typeof(ServiceFault));
				property = new WebBodyFormatMessageProperty(WebContentFormat.Json);
			}
			else
			{
				serializer = new DataContractSerializer(typeof(ServiceFault));
				property = new WebBodyFormatMessageProperty(WebContentFormat.Xml);
			}
			fault = Message.CreateMessage(version, string.Empty, new ServiceFault(string.Empty, error), serializer);
			fault.Properties.Add("WebBodyFormatMessageProperty", property);
			HttpResponseMessageProperty httpResponseMessageProperty = new HttpResponseMessageProperty();
			DataServiceException ex = error as DataServiceException;
			if (ex != null)
			{
				httpResponseMessageProperty.StatusCode = (HttpStatusCode)ex.StatusCode;
			}
			else
			{
				httpResponseMessageProperty.StatusCode = HttpStatusCode.InternalServerError;
			}
			fault.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
		}
	}
}
