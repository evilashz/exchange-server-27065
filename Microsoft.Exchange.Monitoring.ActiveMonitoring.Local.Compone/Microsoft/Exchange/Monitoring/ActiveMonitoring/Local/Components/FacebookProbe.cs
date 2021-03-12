using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Script.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components
{
	// Token: 0x02000274 RID: 628
	internal sealed class FacebookProbe : ProbeWorkItem
	{
		// Token: 0x060011C1 RID: 4545 RVA: 0x00077864 File Offset: 0x00075A64
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!PeopleConnectMaintenance.ShouldRun(base.TraceContext))
			{
				base.Result.StateAttribute1 = "Probe not run, since this server is not primary active manager of the DAG";
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, base.TraceContext, "FacebookProbe.DoWork(): Not run because local server is not PAM of the DAG.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\facebookprobe.cs", 46);
				return;
			}
			if (this.IsGallatin())
			{
				base.Result.StateAttribute1 = "Probe not run, since this server is Gallatin";
				WTFDiagnostics.TraceInformation(ExTraceGlobals.PeopleConnectTracer, base.TraceContext, "FacebookProbe.DoWork(): Not run because local server is Gallatin.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\facebookprobe.cs", 53);
				return;
			}
			HttpWebResponse httpWebResponse = null;
			try
			{
				IPeopleConnectApplicationConfig peopleConnectApplicationConfig = CachedPeopleConnectApplicationConfig.Instance.ReadFacebook();
				PeopleConnectMaintenance.LogApplicationConfig(base.Result, peopleConnectApplicationConfig);
				string endpoint = base.Definition.Endpoint;
				base.Result.StateAttribute2 = "Redirect Url = " + endpoint;
				string text = string.Format("{0}oauth/authorize?client_id={1}&redirect_uri={2}&type=web_server", peopleConnectApplicationConfig.GraphApiEndpoint, peopleConnectApplicationConfig.AppId, endpoint);
				base.Result.StateAttribute3 = "Authorization url = " + text;
				HttpWebRequest httpWebRequest = WebRequest.Create(text) as HttpWebRequest;
				httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; Trident/5.0)";
				httpWebRequest.ServicePoint.Expect100Continue = false;
				httpWebResponse = (httpWebRequest.GetResponse() as HttpWebResponse);
			}
			catch (ExchangeConfigurationException ex)
			{
				Exception ex2 = ex.InnerException ?? ex;
				base.Result.StateAttribute1 = ex2.GetType().Name;
				base.Result.Exception = ex2.GetType().Name;
				base.Result.FailureContext = ex2.StackTrace;
				base.Result.Error = ex2.Message;
				throw;
			}
			catch (WebException ex3)
			{
				httpWebResponse = (ex3.Response as HttpWebResponse);
				base.Result.StateAttribute1 = ex3.GetType().Name;
				throw;
			}
			finally
			{
				if (httpWebResponse != null)
				{
					string text2;
					using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
					{
						text2 = streamReader.ReadToEnd();
					}
					base.Result.ExecutionContext = text2;
					base.Result.StateAttribute4 = httpWebResponse.StatusCode.ToString();
					if (httpWebResponse.StatusCode == HttpStatusCode.BadRequest && text2.Contains("OAuthException"))
					{
						FacebookProbe.OAuthException ex4 = new JavaScriptSerializer().Deserialize<FacebookProbe.OAuthException>(text2);
						base.Result.StateAttribute1 = ex4.Error.Message;
						base.Result.Error = ex4.Error.Code;
						base.Result.Exception = ex4.Error.Type;
					}
				}
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00077B44 File Offset: 0x00075D44
		private bool IsGallatin()
		{
			bool result = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
			{
				if (registryKey == null || registryKey.GetValue("ServiceName") == null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.SecurityTracer, base.TraceContext, "Registry does not have ServiceName key.", null, "IsGallatin", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\peopleconnect\\facebookprobe.cs", 175);
				}
				else if (string.Equals(registryKey.GetValue("ServiceName").ToString(), "Gallatin", StringComparison.CurrentCultureIgnoreCase))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x02000275 RID: 629
		private class OAuthException
		{
			// Token: 0x1700035A RID: 858
			// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00077BE0 File Offset: 0x00075DE0
			// (set) Token: 0x060011C5 RID: 4549 RVA: 0x00077BE8 File Offset: 0x00075DE8
			public FacebookProbe.OAuthInnerException Error { get; set; }
		}

		// Token: 0x02000276 RID: 630
		private class OAuthInnerException
		{
			// Token: 0x1700035B RID: 859
			// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00077BF9 File Offset: 0x00075DF9
			// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00077C01 File Offset: 0x00075E01
			public string Message { get; set; }

			// Token: 0x1700035C RID: 860
			// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00077C0A File Offset: 0x00075E0A
			// (set) Token: 0x060011CA RID: 4554 RVA: 0x00077C12 File Offset: 0x00075E12
			public string Type { get; set; }

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x060011CB RID: 4555 RVA: 0x00077C1B File Offset: 0x00075E1B
			// (set) Token: 0x060011CC RID: 4556 RVA: 0x00077C23 File Offset: 0x00075E23
			public string Code { get; set; }
		}
	}
}
