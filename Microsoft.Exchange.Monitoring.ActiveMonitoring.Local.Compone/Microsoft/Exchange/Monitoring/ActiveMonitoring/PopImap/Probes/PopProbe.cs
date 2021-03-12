using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.Probes
{
	// Token: 0x0200028A RID: 650
	public abstract class PopProbe : TcpProbe
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0007E3DB File Offset: 0x0007C5DB
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.POP3Tracer;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0007E3E2 File Offset: 0x0007C5E2
		protected override string ProbeComponent
		{
			get
			{
				return "POP3";
			}
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0007E3EC File Offset: 0x0007C5EC
		protected override void TestProtocol()
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = base.Result;
			result.StateAttribute21 += "BTPS;";
			Pop3Connection pop3Connection = base.TcpCon as Pop3Connection;
			string request = string.Format("user {0}", base.UserName);
			string request2 = string.Format("pass {0}", base.Password);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "Sending USER Command.", null, "TestProtocol", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopProbe.cs", 85);
			ProbeResult result2 = base.Result;
			result2.StateAttribute13 += ":U";
			base.VerifyResponse(pop3Connection.SendRequest(request, false), string.Format("Unexpected response to the POP USER command for user: {0} on port: {1} {2} SSL encryption", base.UserName, base.EndPoint.Port, base.IsSecure ? "with" : "without"));
			WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "Sending PASS Command.", null, "TestProtocol", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopProbe.cs", 93);
			ProbeResult result3 = base.Result;
			result3.StateAttribute13 += ":P";
			TcpResponse tcpResponse = pop3Connection.SendRequest(request2, false);
			if (tcpResponse.ResponseType != ResponseType.success && base.IsLocalProbe)
			{
				ProbeResult result4 = base.Result;
				result4.StateAttribute13 += ":U";
				base.VerifyResponse(pop3Connection.SendRequest(request, false), string.Format("Unexpected response to the POP USER command for user: {0} on port: {1} {2} SSL encryption", base.UserName, base.EndPoint.Port, base.IsSecure ? "with" : "without"));
				ProbeResult result5 = base.Result;
				result5.StateAttribute13 += ":P";
				tcpResponse = pop3Connection.SendRequest(request2, false);
			}
			base.VerifyResponse(tcpResponse, string.Format("Could not login to user: {0} with POP on port: {1} {2} SSL encryption", base.UserName, base.EndPoint.Port, base.IsSecure ? "with" : "without"));
			ProbeResult result6 = base.Result;
			object stateAttribute = result6.StateAttribute21;
			result6.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute,
				"BTPE:",
				(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
				";"
			});
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0007E63C File Offset: 0x0007C83C
		protected override void DetermineCapability()
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = base.Result;
			result.StateAttribute21 += "BDCS;";
			WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "Determining Login capability.", null, "DetermineCapability", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopProbe.cs", 128);
			Pop3Connection pop3Connection = base.TcpCon as Pop3Connection;
			TcpResponse tcpResponse = pop3Connection.SendRequest("CAPA", true);
			if (tcpResponse.ResponseType != ResponseType.success)
			{
				string message = string.Format("Unexpected Server Response while Determining Login Capability: \"{0}\"", tcpResponse.ResponseString);
				WTFDiagnostics.TraceError(ExTraceGlobals.POP3Tracer, base.TraceContext, tcpResponse.ResponseString, null, "DetermineCapability", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopProbe.cs", 139);
				ProbeResult result2 = base.Result;
				object stateAttribute = result2.StateAttribute21;
				result2.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute,
					"BDCE:",
					(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
					";"
				});
				throw new InvalidOperationException(message);
			}
			ProbeResult result3 = base.Result;
			object stateAttribute2 = result3.StateAttribute21;
			result3.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute2,
				"BDCE:",
				(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
				";"
			});
		}

		// Token: 0x04000DE1 RID: 3553
		private const string UserFormat = "user {0}";

		// Token: 0x04000DE2 RID: 3554
		private const string PassFormat = "pass {0}";

		// Token: 0x04000DE3 RID: 3555
		private const int PopUnsecuredPort = 110;
	}
}
