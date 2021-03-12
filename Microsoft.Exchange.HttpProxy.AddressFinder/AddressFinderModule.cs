using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.Routing;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x02000007 RID: 7
	internal class AddressFinderModule : IHttpModule
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002461 File Offset: 0x00000661
		public AddressFinderModule() : this(AddressFinderFactory.GetInstance(), HttpProxySettings.AddressFinderEnabled.Value, null)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002479 File Offset: 0x00000679
		internal AddressFinderModule(IAddressFinderFactory addressFinderFactory, bool isEnabled, IAddressFinderDiagnostics diagnostics)
		{
			if (addressFinderFactory == null)
			{
				throw new ArgumentNullException("addressFinderFactory");
			}
			this.addressFinderFactory = addressFinderFactory;
			this.isEnabled = isEnabled;
			this.diagnostics = diagnostics;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024BC File Offset: 0x000006BC
		public void Init(HttpApplication application)
		{
			application.PostAuthorizeRequest += delegate(object sender, EventArgs args)
			{
				this.OnPostAuthorizeRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024D0 File Offset: 0x000006D0
		public void Dispose()
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002620 File Offset: 0x00000820
		internal void OnPostAuthorizeRequest(HttpContextBase context)
		{
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				if (!this.isEnabled)
				{
					return;
				}
				IAddressFinderDiagnostics addressFinderDiagnostics = this.GetDiagnostics(context);
				IAddressFinder addressFinder = this.addressFinderFactory.CreateAddressFinder(HttpProxyGlobals.ProtocolType, context.Request.Url.AbsolutePath);
				if (addressFinder != null)
				{
					AddressFinderSource source = new AddressFinderSource(context.Items, context.Request.Headers, context.Request.QueryString, context.Request.Url, context.Request.ApplicationPath, context.Request.FilePath, context.Request.Cookies);
					IRoutingKey[] value = addressFinder.Find(source, addressFinderDiagnostics);
					context.Items["RoutingKeys"] = value;
				}
				else
				{
					ExTraceGlobals.VerboseTracer.TraceDebug<string, Uri, ProtocolType>((long)this.GetHashCode(), "[AddressFinderModule::OnPostAuthorizeRequest]: addressFinder is null: Method {0}; Url {1}; Protocol {2};", context.Request.HttpMethod, context.Request.Url, HttpProxyGlobals.ProtocolType);
					addressFinderDiagnostics.AddErrorInfo("addressFinder is null");
				}
				addressFinderDiagnostics.LogRoutingKeys();
			}, new Diagnostics.LastChanceExceptionHandler(this.LastChanceExceptionHandler));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002660 File Offset: 0x00000860
		private IAddressFinderDiagnostics GetDiagnostics(HttpContextBase context)
		{
			IAddressFinderDiagnostics result;
			if (this.diagnostics == null)
			{
				result = new AddressFinderDiagnostics(context);
			}
			else
			{
				result = this.diagnostics;
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002688 File Offset: 0x00000888
		private void LastChanceExceptionHandler(Exception ex)
		{
			IAddressFinderDiagnostics addressFinderDiagnostics = this.GetDiagnostics(new HttpContextWrapper(HttpContext.Current));
			if (addressFinderDiagnostics != null)
			{
				addressFinderDiagnostics.LogUnhandledException(ex);
			}
		}

		// Token: 0x04000010 RID: 16
		private readonly bool isEnabled;

		// Token: 0x04000011 RID: 17
		private readonly IAddressFinderDiagnostics diagnostics;

		// Token: 0x04000012 RID: 18
		private readonly IAddressFinderFactory addressFinderFactory;
	}
}
