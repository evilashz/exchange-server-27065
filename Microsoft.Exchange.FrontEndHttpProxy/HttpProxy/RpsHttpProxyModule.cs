using System;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000CF RID: 207
	internal class RpsHttpProxyModule : ProxyModule
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x0002DA18 File Offset: 0x0002BC18
		static RpsHttpProxyModule()
		{
			string path;
			try
			{
				path = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\HttpProxy");
			}
			catch (SetupVersionInformationCorruptException)
			{
				path = "C:\\Program Files\\Microsoft\\Exchange Server\\V15";
			}
			string logFolderPath = Path.Combine(path, HttpProxyGlobals.ProtocolType.ToString(), "RequestMonitor");
			RequestMonitor.InitRequestMonitor(logFolderPath, 300000);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0002DA78 File Offset: 0x0002BC78
		protected override void OnBeginRequestInternal(HttpApplication httpApplication)
		{
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[RpsHttpProxyModule::OnBeginRequestInternal] Enter");
			HttpContext context = httpApplication.Context;
			RequestDetailsLogger current = RequestDetailsLoggerBase<RequestDetailsLogger>.GetCurrent(context);
			if (current != null)
			{
				RequestMonitor.Instance.RegisterRequest(current.ActivityId);
				string text = context.Request.Headers["Authorization"];
				byte[] bytes;
				byte[] array;
				if (!string.IsNullOrEmpty(text) && LiveIdBasicAuthModule.ParseCredentials(context, text, false, out bytes, out array))
				{
					string text2 = Encoding.UTF8.GetString(bytes).Trim();
					SmtpAddress smtpAddress = new SmtpAddress(text2);
					RequestMonitor.Instance.Log(current.ActivityId, RequestMonitorMetadata.AuthenticatedUser, text2);
					RequestMonitor.Instance.Log(current.ActivityId, RequestMonitorMetadata.Organization, smtpAddress.Domain);
					context.Items[Constants.WLIDMemberName] = text2;
					ExTraceGlobals.HttpModuleTracer.TraceDebug<string>((long)this.GetHashCode(), "[RpsHttpProxyModule::OnBeginRequestInternal] LiveIdMember={0}", text2);
				}
			}
			base.OnBeginRequestInternal(httpApplication);
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[RpsHttpProxyModule::OnBeginRequestInternal] Exit");
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0002DB7C File Offset: 0x0002BD7C
		protected override void OnEndRequestInternal(HttpApplication httpApplication)
		{
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[RpsHttpProxyModule::OnEndRequestInternal] Enter");
			HttpContext context = httpApplication.Context;
			RequestDetailsLogger current = RequestDetailsLoggerBase<RequestDetailsLogger>.GetCurrent(context);
			if (current != null)
			{
				RequestMonitor.Instance.UnRegisterRequest(current.ActivityId);
			}
			base.OnEndRequestInternal(httpApplication);
			ExTraceGlobals.HttpModuleTracer.TraceFunction((long)this.GetHashCode(), "[RpsHttpProxyModule::OnEndRequestInternal] Exit");
		}
	}
}
