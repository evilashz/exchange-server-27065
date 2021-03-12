using System;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Office.Outlook;
using Microsoft.Office.Outlook.Network;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OutlookService.Probes
{
	// Token: 0x0200025E RID: 606
	public abstract class OutlookServiceSocketProbeBase : ProbeWorkItem
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00071BAD File Offset: 0x0006FDAD
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x00071BB5 File Offset: 0x0006FDB5
		protected SocketType Type { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00071BBE File Offset: 0x0006FDBE
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x00071BC6 File Offset: 0x0006FDC6
		protected TimeSpan Timeout { get; set; }

		// Token: 0x06001114 RID: 4372 RVA: 0x00071BD0 File Offset: 0x0006FDD0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceSocketBaseProbe.DoWork : Endpoint={0}, Account={1}, Password={2}", base.Definition.Endpoint, base.Definition.Account, base.Definition.AccountPassword, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceSocketProbeBase.cs", 65);
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceSocketBaseProbe.DoWork : Setting up connection to {0}", base.Definition.Endpoint, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceSocketProbeBase.cs", 73);
			ServiceApiClient serviceApiClient = new ServiceApiClient();
			SocketClient socketClient = null;
			string str = string.Empty;
			if (!string.IsNullOrWhiteSpace(base.Definition.Account))
			{
				str = base.Definition.Account.Substring(0, base.Definition.Account.IndexOf('@'));
			}
			string text = "HxServiceMon-" + str;
			if (text.Length > 90)
			{
				text = text.Substring(0, 90);
			}
			DeviceInfo deviceInfo = new DeviceInfo
			{
				deviceId = text,
				deviceType = "Probe",
				deviceOS = "Windows"
			};
			try
			{
				NetworkCredential cred = new NetworkCredential(base.Definition.Account, base.Definition.AccountPassword);
				CredentialCache credentialCache = new CredentialCache();
				Uri uri = new Uri(base.Definition.Endpoint);
				credentialCache.Add(uri, "Basic", cred);
				ICredentials credentials = credentialCache;
				string text2 = "monitoring-" + Guid.NewGuid().ToString();
				serviceApiClient.Initialize(true, uri.Authority, deviceInfo, text2, credentials, "Monitoring", this.Type);
				WTFDiagnostics.TraceDebug(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceSocketBaseProbe.DoWork : Initializing the socket client", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceSocketProbeBase.cs", 114);
				socketClient = new SocketClient(serviceApiClient);
				WTFDiagnostics.TraceDebug(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceSocketBaseProbe.DoWork : About to execute the request", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceSocketProbeBase.cs", 121);
				this.ExecuteRequest(socketClient);
			}
			catch (Exception ex)
			{
				if (!OutlookServiceSocketProbeBase.IsKnownServerException(ex))
				{
					base.Result.ResultType = ResultType.Failed;
					WTFDiagnostics.TraceDebug<string, Exception, string>(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceSocketBaseProbe.DoWork : Probe Failed with Exception Message = {0} InnerException = {1} StackTrace = {2}", ex.Message, ex.InnerException, ex.StackTrace, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceSocketProbeBase.cs", 142);
					throw ex;
				}
				base.Result.ResultType = ResultType.Succeeded;
				base.Result.StateAttribute13 = "Received a Response. Probe is running on passive MDB. Known exception encountered";
				WTFDiagnostics.TraceDebug(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceSocketBaseProbe.DoWork : Known Exception Encountered. Passing the probe in this case", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceSocketProbeBase.cs", 134);
			}
			finally
			{
				try
				{
					if (socketClient != null)
					{
						socketClient.Client.CloseAsync();
					}
					WTFDiagnostics.TraceDebug(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceSocketBaseProbe.DoWork : serviceapi client closed successfully", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceSocketProbeBase.cs", 161);
				}
				catch (Exception ex2)
				{
					WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.HTTPTracer, base.TraceContext, "OutlookServiceSocketBaseProbe.DoWork : Exception occured while closing the client Message = {0} StackTrace = {1}", ex2.Message, ex2.StackTrace, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\OutlookServiceSocketProbeBase.cs", 168);
				}
			}
		}

		// Token: 0x06001115 RID: 4373
		protected abstract void ExecuteRequest(SocketClient client);

		// Token: 0x06001116 RID: 4374 RVA: 0x00071F10 File Offset: 0x00070110
		private static bool IsKnownServerException(Exception ex)
		{
			return OutlookServiceSocketProbeBase.SearchExceptionString(ex, OutlookServiceSocketProbeBase.KnownServerExceptions);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00071F34 File Offset: 0x00070134
		protected static bool SearchExceptionString(Exception exception, string[] expectedExceptionsNames)
		{
			string exceptionString = exception.ToString();
			return expectedExceptionsNames.Any((string exceptionName) => exceptionString.Contains(exceptionName));
		}

		// Token: 0x04000CD9 RID: 3289
		private const int AllowedDeviceIdLength = 90;

		// Token: 0x04000CDA RID: 3290
		private static readonly string[] KnownServerExceptions = new string[]
		{
			typeof(MapiExceptionIllegalCrossServerConnection).Name,
			typeof(WrongServerException).Name
		};
	}
}
