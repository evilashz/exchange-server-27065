using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000200 RID: 512
	public class OWAServiceFactory : ServiceHostFactory
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x00047CB3 File Offset: 0x00045EB3
		public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
		{
			if (Globals.IsAnonymousCalendarApp)
			{
				return this.CreateOwaAnonymousServiceHost(baseAddresses);
			}
			return this.CreateOwaServiceHost(baseAddresses);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00047CE0 File Offset: 0x00045EE0
		private ServiceHost CreateOwaServiceHost(Uri[] baseAddresses)
		{
			baseAddresses = Array.FindAll<Uri>(baseAddresses, (Uri uri) => uri.Scheme == Uri.UriSchemeHttps);
			ServiceHost serviceHost = new ServiceHost(typeof(OWAService), baseAddresses);
			serviceHost.Authorization.ServiceAuthorizationManager = new OWAAuthorizationManager();
			foreach (Uri uri2 in baseAddresses)
			{
				ServiceEndpoint serviceEndpoint = serviceHost.AddServiceEndpoint(typeof(IOWAService), this.CreateBinding(uri2), uri2);
				serviceEndpoint.Behaviors.Add(new OWAWebHttpBehavior());
				serviceEndpoint.Behaviors.Add(new DispatcherSynchronizationBehavior
				{
					MaxPendingReceives = OWAServiceFactory.MaxPendingReceives
				});
				UriBuilder uriBuilder = new UriBuilder(uri2);
				UriBuilder uriBuilder2 = uriBuilder;
				uriBuilder2.Path += "/s";
				ServiceEndpoint serviceEndpoint2 = serviceHost.AddServiceEndpoint(typeof(IOWAStreamingService), this.CreateBinding(uriBuilder.Uri), uriBuilder.Uri);
				serviceEndpoint2.Behaviors.Add(new OWAWebHttpBehavior());
			}
			return serviceHost;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00047DF4 File Offset: 0x00045FF4
		private ServiceHost CreateOwaAnonymousServiceHost(Uri[] baseAddresses)
		{
			ServiceHost serviceHost = new ServiceHost(typeof(OWAAnonymousService), baseAddresses);
			foreach (Uri uri in baseAddresses)
			{
				ServiceEndpoint serviceEndpoint = serviceHost.AddServiceEndpoint(typeof(IOWAAnonymousCalendarService), this.CreateBinding(uri), uri);
				serviceEndpoint.Behaviors.Add(new OWAWebHttpBehavior());
			}
			return serviceHost;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00047E58 File Offset: 0x00046058
		private WebHttpBinding CreateBinding(Uri supportedUri)
		{
			string configurationName = string.Format("{0}Binding", supportedUri.Scheme);
			return new WebHttpBinding(configurationName)
			{
				Security = 
				{
					Transport = 
					{
						ClientCredentialType = Globals.ServiceAuthenticationType
					}
				}
			};
		}

		// Token: 0x04000AAD RID: 2733
		private const string MaxPendingReceivesKeyName = "MaxPendingReceives";

		// Token: 0x04000AAE RID: 2734
		private static readonly int DefaultMaxPendingReceives = 2 * Environment.ProcessorCount;

		// Token: 0x04000AAF RID: 2735
		private static readonly int MaxPendingReceives = Global.GetAppSettingAsInt("MaxPendingReceives", OWAServiceFactory.DefaultMaxPendingReceives);
	}
}
