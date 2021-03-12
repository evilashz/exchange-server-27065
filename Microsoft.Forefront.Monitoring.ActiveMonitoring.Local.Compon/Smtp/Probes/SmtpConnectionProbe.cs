using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000219 RID: 537
	public class SmtpConnectionProbe : ProbeWorkItem
	{
		// Token: 0x0600110F RID: 4367 RVA: 0x00031474 File Offset: 0x0002F674
		public SmtpConnectionProbe()
		{
			ChainEnginePool pool = new ChainEnginePool();
			this.cache = new CertificateCache(pool);
			this.cache.Open(OpenFlags.ReadOnly);
			this.CancelProbeWithSuccess = false;
			this.LatencyAnalytics = new SmtpConnectionProbeAnalytics();
			this.UseXmlConfiguration = true;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x000314BE File Offset: 0x0002F6BE
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x000314C6 File Offset: 0x0002F6C6
		internal CancellationToken CancellationToken { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x000314CF File Offset: 0x0002F6CF
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x000314D7 File Offset: 0x0002F6D7
		internal bool CancelProbeWithSuccess { get; set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x000314E0 File Offset: 0x0002F6E0
		protected SmtpConnectionProbeWorkDefinition WorkDefinition
		{
			get
			{
				if (this.workDefinition == null)
				{
					this.workDefinition = new SmtpConnectionProbeWorkDefinition(base.Definition.ExtensionAttributes, this.UseXmlConfiguration);
				}
				return this.workDefinition;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0003150C File Offset: 0x0002F70C
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x00031514 File Offset: 0x0002F714
		private protected virtual ISimpleSmtpClient Client
		{
			protected get
			{
				return this.client;
			}
			private set
			{
				this.client = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0003151D File Offset: 0x0002F71D
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x00031525 File Offset: 0x0002F725
		protected int TestCount
		{
			get
			{
				return this.testCount;
			}
			set
			{
				this.testCount = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0003152E File Offset: 0x0002F72E
		protected virtual bool IsSmtpConnectionExpectedToFail
		{
			get
			{
				return base.Broker.IsLocal() && base.Definition.ServiceName == "HubTransport" && this.IsHubTransportInDrainingState();
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0003155C File Offset: 0x0002F75C
		protected virtual bool DisconnectBetweenSessions
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0003155F File Offset: 0x0002F75F
		protected virtual int Identifier
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00031567 File Offset: 0x0002F767
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x0003156F File Offset: 0x0002F76F
		protected SmtpConnectionProbeAnalytics LatencyAnalytics { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00031578 File Offset: 0x0002F778
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x00031580 File Offset: 0x0002F780
		protected bool UseXmlConfiguration { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0003158C File Offset: 0x0002F78C
		private string ResolvedEndPoint
		{
			get
			{
				if (string.IsNullOrEmpty(this.resolvedEndPoint))
				{
					this.resolvedEndPoint = this.WorkDefinition.SmtpServer;
					if (this.WorkDefinition.ResolveEndPoint)
					{
						DnsUtils.DnsResponse mxendPointForDomain = DnsUtils.GetMXEndPointForDomain(this.WorkDefinition.SmtpServer);
						WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "Resolved SMTP server {0} as {1}.", this.WorkDefinition.SmtpServer, this.resolvedEndPoint, null, "ResolvedEndPoint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 211);
						if (!mxendPointForDomain.DnsResolvedSuccessfuly)
						{
							throw new Exception(string.Format("Unable to resolve MX records for {0}.", this.WorkDefinition.SmtpServer));
						}
						this.resolvedEndPoint = mxendPointForDomain.IPAddress.ToString();
					}
				}
				return this.resolvedEndPoint;
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00031647 File Offset: 0x0002F847
		public static void SetResolvedEndpoint(ProbeResult result, string resolvedEndpoint)
		{
			result.StateAttribute11 = resolvedEndpoint;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00031650 File Offset: 0x0002F850
		public static string GetResolvedEndpiont(ProbeResult result)
		{
			return result.StateAttribute11;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00031658 File Offset: 0x0002F858
		public static void SetLatencyAnalysis(ProbeResult result, string analysis)
		{
			result.StateAttribute12 = analysis;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00031661 File Offset: 0x0002F861
		public static string GetLatencyAnalysis(ProbeResult result)
		{
			return result.StateAttribute12;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00031669 File Offset: 0x0002F869
		public static void SetLatencySummary(ProbeResult result, string summary)
		{
			result.StateAttribute13 = summary;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00031672 File Offset: 0x0002F872
		public static string GetLatencySummary(ProbeResult result)
		{
			return result.StateAttribute13;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0003167A File Offset: 0x0002F87A
		public static void SetAdvertisedServerName(ProbeResult result, string serverName)
		{
			result.StateAttribute14 = serverName;
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00031683 File Offset: 0x0002F883
		public static string GetAdvertisedServerName(ProbeResult result)
		{
			return result.StateAttribute14;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0003168B File Offset: 0x0002F88B
		public static void SetProtocolConversation(ProbeResult result, string protocol)
		{
			result.StateAttribute15 = protocol;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00031694 File Offset: 0x0002F894
		public static string GetProtocolConversation(ProbeResult result)
		{
			return result.StateAttribute15;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0003169C File Offset: 0x0002F89C
		public static void SetPortNumber(ProbeResult result, int portNumber)
		{
			result.StateAttribute16 = (double)portNumber;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000316A6 File Offset: 0x0002F8A6
		public static int GetPortNumber(ProbeResult result)
		{
			return (int)result.StateAttribute16;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x000316AF File Offset: 0x0002F8AF
		public static void SetHighestLatency(ProbeResult result, long latency)
		{
			result.StateAttribute17 = (double)latency;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x000316B9 File Offset: 0x0002F8B9
		public static long GetHighestLatency(ProbeResult result)
		{
			return (long)result.StateAttribute17;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x000316C2 File Offset: 0x0002F8C2
		public static void SetAverageLatency(ProbeResult result, long average)
		{
			result.StateAttribute18 = (double)average;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x000316CC File Offset: 0x0002F8CC
		public static long GetAverageLatency(ProbeResult result)
		{
			return (long)result.StateAttribute18;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000316D5 File Offset: 0x0002F8D5
		public static void SetProbeExecutionTestStateDisabled(ProbeResult result)
		{
			result.StateAttribute19 = 1.0;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000316E6 File Offset: 0x0002F8E6
		public static bool GetProbeExecutionTestStateDisabled(ProbeResult result)
		{
			return (int)result.StateAttribute19 == 1;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000316F5 File Offset: 0x0002F8F5
		public static void SetExceptionType(ProbeResult result, string exceptionType)
		{
			result.StateAttribute21 = exceptionType;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000316FE File Offset: 0x0002F8FE
		public static string GetExceptionType(ProbeResult result)
		{
			return result.StateAttribute21;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00031706 File Offset: 0x0002F906
		public static void SetLastSmtpResponse(ProbeResult result, string smtpResponse)
		{
			result.StateAttribute22 = smtpResponse;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0003170F File Offset: 0x0002F90F
		public static string GetLastSmtpResponse(ProbeResult result)
		{
			return result.StateAttribute22;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000319EC File Offset: 0x0002FBEC
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition definition, Dictionary<string, string> propertyBag)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(definition.ExtensionAttributes);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("//WorkContext");
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "SmtpServer"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ResolveEndPoint"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "Port"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "UseSsl"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "UseSsl", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "IgnoreCertificateNameMismatchPolicyError"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "UseSsl", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "IgnoreCertificateChainPolicyErrorForSelfSigned"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "AuthenticationType"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedConnectionLostPoint"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "HeloDomain"));
			SmtpConnectionProbe.UpdateNodeAttribute(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "SenderTenantID"));
			SmtpConnectionProbe.UpdateNodeAttribute(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "RecipientTenantID"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "Data"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "Data", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "AddAttributions"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "Data", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "Direction"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedResponseOnConnect"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedResponseOnHelo"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedResponseOnStartTls"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedResponseOnHeloAfterStartTls"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedResponseOnAuthenticate"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedResponseOnMailFrom"));
			SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedResponseOnData"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "AuthenticationAccount", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "AuthenticationAccountUsername"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "AuthenticationAccount", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "AuthenticationAccountUsername"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "AuthenticationAccount", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "AuthenticationAccountPassword"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "MailFrom", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "MailFromUsername"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "MailTo", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "MailToUsername"));
			SmtpConnectionProbe.UpdateChildNodeAttribute(xmlNode, "MailTo", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "MailToExpectedResponse"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ClientCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ClientCertificateStoreLocation"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ClientCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ClientCertificateStoreName"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ClientCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ClientCertificateFindType"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ClientCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ClientCertificateFindValue"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ClientCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ClientCertificateTransportCertificateName"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ClientCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ClientCertificateTransportCertificateFqdn"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ClientCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ClientCertificateTransportWildcardMatchType"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ExpectedServerCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedServerCertificateSubject"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ExpectedServerCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedServerCertificateIssuer"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ExpectedServerCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedServerCertificateValidFrom"));
			SmtpConnectionProbe.UpdateChildNodeInnerText(xmlNode, "ExpectedServerCertificate", propertyBag.FirstOrDefault((KeyValuePair<string, string> p) => p.Key == "ExpectedServerCertificateValidTo"));
			definition.ExtensionAttributes = xmlDocument.InnerXml;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00031E2C File Offset: 0x0003002C
		internal bool ShouldCancelProbe()
		{
			if (this.CancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}
			return this.CancelProbeWithSuccess;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00031E58 File Offset: 0x00030058
		internal void MeasureLatency(string reason, Action cmd)
		{
			SmtpConnectionProbeLatency smtpConnectionProbeLatency = new SmtpConnectionProbeLatency(reason, true);
			try
			{
				cmd();
			}
			finally
			{
				smtpConnectionProbeLatency.StopRecording();
				this.LatencyAnalytics.AddLatency(smtpConnectionProbeLatency);
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00031E98 File Offset: 0x00030098
		internal void MeasureLatency(string reason, Action cmd, ConnectionLostPoint connectionLostPoint)
		{
			try
			{
				this.MeasureLatency(reason, cmd);
			}
			catch
			{
				if (!this.AssertIsConnected(connectionLostPoint))
				{
					throw;
				}
			}
			finally
			{
				this.AssertIsConnected(connectionLostPoint);
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00031EE4 File Offset: 0x000300E4
		internal bool MeasureLatency(string reason, SmtpConnectionProbe.ActionWithReturn<bool> cmd)
		{
			SmtpConnectionProbeLatency smtpConnectionProbeLatency = new SmtpConnectionProbeLatency(reason, true);
			bool result;
			try
			{
				result = cmd();
			}
			finally
			{
				smtpConnectionProbeLatency.StopRecording();
				this.LatencyAnalytics.AddLatency(smtpConnectionProbeLatency);
			}
			return result;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00031F28 File Offset: 0x00030128
		internal bool MeasureLatency(string reason, SmtpConnectionProbe.ActionWithReturn<bool> cmd, ConnectionLostPoint connectionLostPoint)
		{
			bool result;
			try
			{
				result = this.MeasureLatency(reason, cmd);
			}
			catch
			{
				if (!this.AssertIsConnected(connectionLostPoint))
				{
					throw;
				}
				return false;
			}
			finally
			{
				this.AssertIsConnected(connectionLostPoint);
			}
			return result;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00031F84 File Offset: 0x00030184
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.CancellationToken = cancellationToken;
			SmtpConnectionProbe.SetResolvedEndpoint(base.Result, "The connection has not been established");
			DateTime dateTime = DateTime.MinValue;
			if (!TransportProbeCommon.IsProbeExecutionEnabled())
			{
				ProbeResult result = base.Result;
				result.ExecutionContext += "SmtpConnectionProbe skipped because probe execution disabled.";
				WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "SmtpConnectionProbe skipped because probe execution disabled.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 723);
				SmtpConnectionProbe.SetProbeExecutionTestStateDisabled(base.Result);
				return;
			}
			if (!this.IsSmtpConnectionExpectedToFail)
			{
				try
				{
					this.Client = this.GetSmtpClient();
					this.Client.IgnoreCertificateNameMismatchPolicyError = this.WorkDefinition.IgnoreCertificateNameMismatchPolicyError;
					this.Client.IgnoreCertificateChainPolicyErrorForSelfSigned = this.WorkDefinition.IgnoreCertificateChainPolicyErrorForSelfSigned;
					this.testCount = 1;
					this.MeasureLatency("BeforeConnect", delegate()
					{
						this.BeforeConnect();
					});
					dateTime = DateTime.UtcNow;
					for (int i = 1; i <= this.testCount; i++)
					{
						if (this.ShouldCancelProbe())
						{
							return;
						}
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "Running test {0}.", i.ToString(), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 779);
						this.TestConnection();
					}
					WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "The SMTP server has been successfully evaluated and validated.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 783);
				}
				catch (EndpointManagerEndpointUninitializedException)
				{
					ProbeResult result2 = base.Result;
					result2.ExecutionContext += " Probe ended due to EndpointManagerEndpointUninitializedException, ignoring exception and treating as transient";
				}
				catch (OperationCanceledException)
				{
					SmtpConnectionProbe.SetExceptionType(base.Result, typeof(OperationCanceledException).FullName);
					throw;
				}
				catch (Exception ex)
				{
					base.Result.FailureContext = ex.Message;
					SmtpConnectionProbe.SetExceptionType(base.Result, ex.GetType().FullName);
					WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "{0}\r\n{1}", ex.ToString(), (this.Client != null) ? this.Client.SessionText : string.Empty, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 800);
					throw;
				}
				finally
				{
					base.Result.SampleValue = ((dateTime == DateTime.MinValue) ? 0.0 : Convert.ToDouble((DateTime.UtcNow - dateTime).TotalMilliseconds));
					SmtpConnectionProbe.SetLatencyAnalysis(base.Result, this.LatencyAnalytics.GenerateLatencyAnalysis());
					SmtpConnectionProbe.SetLatencySummary(base.Result, this.LatencyAnalytics.LatencySummary());
					SmtpConnectionProbeLatency highestLatencyValue = this.LatencyAnalytics.GetHighestLatencyValue();
					SmtpConnectionProbe.SetHighestLatency(base.Result, (highestLatencyValue == null) ? 0L : highestLatencyValue.Latency);
					SmtpConnectionProbe.SetAverageLatency(base.Result, this.LatencyAnalytics.Mean);
					if (this.Client != null)
					{
						SmtpConnectionProbe.SetProtocolConversation(base.Result, this.Client.SessionText.Replace(Environment.NewLine, " "));
						if (!string.IsNullOrEmpty(this.Client.LastResponse))
						{
							SmtpConnectionProbe.SetLastSmtpResponse(base.Result, this.Client.LastResponse);
						}
						this.Client.Dispose();
						this.Client = null;
					}
				}
				return;
			}
			if (this.CanConnectToEndPoint())
			{
				ProbeResult result3 = base.Result;
				result3.ExecutionContext += "Smtp connection succeeded when expected to fail.";
				base.Result.SetCompleted(ResultType.Failed);
				SmtpConnectionProbe.SetExceptionType(base.Result, "Smtp connection succeeded when expected to fail.");
				WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "SmtpConnectionProbe failed because connection succeeded which was not expected", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 741);
				return;
			}
			ProbeResult result4 = base.Result;
			result4.ExecutionContext += "Smtp connection failed as expected.";
			base.Result.SetCompleted(ResultType.Succeeded);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "SmtpConnectionProbe succeeded. Connection failed as expected", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 750);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000323BC File Offset: 0x000305BC
		protected void AssertExpectedResponse(SmtpExpectedResponse expectedResponse)
		{
			SimpleSmtpClient.SmtpResponseCode lastResponseCode = this.Client.LastResponseCode;
			string text = "Server {0} on port {1} did not respond with expected response ({2}). The actual response was: {3}.";
			bool flag;
			switch (expectedResponse.Type)
			{
			case ExpectedResponseType.ResponseText:
			{
				Regex regex = new Regex(expectedResponse.ResponseText, RegexOptions.IgnoreCase);
				flag = regex.IsMatch(this.Client.LastResponse);
				text = string.Format(text, new object[]
				{
					this.ResolvedEndPoint,
					this.WorkDefinition.Port,
					expectedResponse.ResponseText,
					this.Client.LastResponse
				});
				goto IL_ED;
			}
			}
			flag = (lastResponseCode == expectedResponse.ResponseCode);
			text = string.Format(text, new object[]
			{
				this.ResolvedEndPoint,
				this.WorkDefinition.Port,
				expectedResponse.ResponseCode,
				this.Client.LastResponse
			});
			IL_ED:
			if (!flag)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), text, null, "AssertExpectedResponse", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 857);
				throw new Exception(text);
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00032500 File Offset: 0x00030700
		protected bool SendCommand(string command, string expectedResponse, ref StringBuilder errors)
		{
			if (this.ShouldCancelProbe() || command == null)
			{
				return false;
			}
			this.MeasureLatency(command.Split(new char[]
			{
				' '
			}).First<string>(), delegate()
			{
				this.Client.Send(command);
			});
			if (!this.VerifyExpectedResponse(expectedResponse))
			{
				if (errors == null)
				{
					errors = new StringBuilder();
				}
				errors.AppendFormat(string.Format("Response to '{0}' not as expected. Actual: {1}", command, this.Client.LastResponse), new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000325A4 File Offset: 0x000307A4
		protected bool VerifyExpectedResponse(string response)
		{
			string text = this.Client.LastResponse;
			if (this.Client.LastResponse.EndsWith("\r\n"))
			{
				text = text.Substring(0, this.Client.LastResponse.Length - 2);
			}
			return string.Equals(text, response);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000325F8 File Offset: 0x000307F8
		protected string GetProbeId()
		{
			bool flag;
			long probeRunSequenceNumber = ProbeRunSequence.GetProbeRunSequenceNumber(this.Identifier.ToString(), out flag);
			return string.Format("{0:X8}-0000-0000-0000-{1:X12}", this.Identifier, (int)probeRunSequenceNumber);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00032674 File Offset: 0x00030874
		protected virtual bool CanConnectToEndPoint()
		{
			bool result = false;
			try
			{
				using (ISimpleSmtpClient client = this.GetSmtpClient())
				{
					this.MeasureLatency("Connect", () => client.Connect(this.ResolvedEndPoint, this.WorkDefinition.Port, this.DisconnectBetweenSessions));
					result = client.IsConnected;
				}
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceInformation<string, int, Exception>(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "Unable to connect to server {0} on port {1} due to the following exception: {2}", this.ResolvedEndPoint, this.WorkDefinition.Port, arg, null, "CanConnectToEndPoint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 947);
				result = false;
			}
			finally
			{
				IDisposable disposable = this.Client;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00032768 File Offset: 0x00030968
		protected virtual ISimpleSmtpClient GetSmtpClient()
		{
			return new SimpleSmtpClient(this.CancellationToken);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00032775 File Offset: 0x00030975
		protected virtual void BeforeConnect()
		{
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00032777 File Offset: 0x00030977
		protected virtual void AfterConnect()
		{
			this.RunCustomCommands(CustomCommandRunPoint.AfterConnect);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00032780 File Offset: 0x00030980
		protected virtual void AfterHelo()
		{
			this.RunCustomCommands(CustomCommandRunPoint.AfterHelo);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00032789 File Offset: 0x00030989
		protected virtual void AfterStartTls()
		{
			this.RunCustomCommands(CustomCommandRunPoint.AfterStartTls);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00032792 File Offset: 0x00030992
		protected virtual void AfterHeloAfterStartTls()
		{
			this.RunCustomCommands(CustomCommandRunPoint.AfterHeloAfterStartTls);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0003279B File Offset: 0x0003099B
		protected virtual void AfterAuthenticate()
		{
			this.RunCustomCommands(CustomCommandRunPoint.AfterAuthenticate);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000327A4 File Offset: 0x000309A4
		protected virtual void AfterMailFrom()
		{
			this.RunCustomCommands(CustomCommandRunPoint.AfterMailFrom);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000327AD File Offset: 0x000309AD
		protected virtual void BeforeRcptTo()
		{
			this.RunCustomCommands(CustomCommandRunPoint.BeforeRcptTo);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000327B6 File Offset: 0x000309B6
		protected virtual void AfterRcptTo()
		{
			this.RunCustomCommands(CustomCommandRunPoint.AfterRcptTo);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000327BF File Offset: 0x000309BF
		protected virtual void BeforeData()
		{
			this.RunCustomCommands(CustomCommandRunPoint.BeforeData);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000327C9 File Offset: 0x000309C9
		protected virtual void AfterData()
		{
			this.RunCustomCommands(CustomCommandRunPoint.AfterData);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000327D4 File Offset: 0x000309D4
		private static void UpdateNodeInnerText(XmlNode root, KeyValuePair<string, string> kvp)
		{
			if (kvp.Key != null)
			{
				XmlNode xmlNode = root.SelectSingleNode(kvp.Key);
				if (xmlNode == null)
				{
					xmlNode = root.AppendChild(root.OwnerDocument.CreateElement(kvp.Key));
				}
				xmlNode.InnerText = kvp.Value;
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00032824 File Offset: 0x00030A24
		private static void UpdateChildNodeInnerText(XmlNode root, string childNodeName, KeyValuePair<string, string> kvp)
		{
			if (kvp.Key != null)
			{
				XmlNode xmlNode = root.SelectSingleNode(childNodeName);
				if (xmlNode == null)
				{
					xmlNode = root.AppendChild(root.OwnerDocument.CreateElement(childNodeName));
				}
				if (kvp.Key.StartsWith(childNodeName))
				{
					string key = kvp.Key.Replace(childNodeName, string.Empty);
					kvp = new KeyValuePair<string, string>(key, kvp.Value);
				}
				SmtpConnectionProbe.UpdateNodeInnerText(xmlNode, kvp);
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00032890 File Offset: 0x00030A90
		private static void UpdateNodeAttribute(XmlNode node, KeyValuePair<string, string> kvp)
		{
			if (node != null && kvp.Key != null)
			{
				XmlAttribute xmlAttribute = node.Attributes[kvp.Key];
				if (xmlAttribute == null)
				{
					xmlAttribute = node.Attributes.Append(node.OwnerDocument.CreateAttribute(kvp.Key));
				}
				xmlAttribute.Value = kvp.Value;
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000328EC File Offset: 0x00030AEC
		private static void UpdateChildNodeAttribute(XmlNode root, string childNodeName, KeyValuePair<string, string> kvp)
		{
			if (root != null && kvp.Key != null)
			{
				XmlNode xmlNode = root.SelectSingleNode(childNodeName);
				if (xmlNode == null)
				{
					xmlNode = root.AppendChild(root.OwnerDocument.CreateElement(childNodeName));
				}
				if (kvp.Key.StartsWith(childNodeName))
				{
					string key = kvp.Key.Replace(childNodeName, string.Empty);
					kvp = new KeyValuePair<string, string>(key, kvp.Value);
				}
				SmtpConnectionProbe.UpdateNodeAttribute(xmlNode, kvp);
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003295C File Offset: 0x00030B5C
		private bool AssertIsConnected(ConnectionLostPoint connectionLostPoint)
		{
			if (this.client.IsConnected)
			{
				if (this.WorkDefinition.ExpectedConnectionLostPoint != ConnectionLostPoint.None && this.WorkDefinition.ExpectedConnectionLostPoint == connectionLostPoint)
				{
					string text = string.Format("Still connected to server {0} but expected to be dropped: {1}. Treating as failure.", this.ResolvedEndPoint, connectionLostPoint);
					WTFDiagnostics.TraceError(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), text, null, "AssertIsConnected", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 1198);
					throw new Exception(text);
				}
			}
			else
			{
				string text;
				if (connectionLostPoint == ConnectionLostPoint.OnConnect)
				{
					text = "Cannot establish connection to server {0} on port {1}.";
				}
				else
				{
					text = "Connection to server {0} on port {1} was lost.";
				}
				text = string.Format(text + " Lost: {2}. Expected: {3}. Treating as {4}.", new object[]
				{
					this.ResolvedEndPoint,
					this.WorkDefinition.Port,
					connectionLostPoint,
					this.WorkDefinition.ExpectedConnectionLostPoint,
					(this.WorkDefinition.ExpectedConnectionLostPoint == connectionLostPoint) ? "success" : "failure"
				});
				WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), text, null, "AssertIsConnected", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 1224);
				if (this.WorkDefinition.ExpectedConnectionLostPoint != connectionLostPoint)
				{
					return false;
				}
				this.CancelProbeWithSuccess = true;
			}
			return true;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00032A90 File Offset: 0x00030C90
		private void AssertExpectedServerCertificate()
		{
			string format = "The server certificate did not match the expected {0} value of \"{1}\". The actual value was \"{2}\".";
			if (this.WorkDefinition.ExpectedServerCertificateValid)
			{
				if (this.client.RemoteCertificate == null)
				{
					throw new Exception("No server certificate existed.");
				}
				if (this.WorkDefinition.ExpectedServerCertificate.Subject != null)
				{
					Regex regex = new Regex(this.WorkDefinition.ExpectedServerCertificate.Subject, RegexOptions.IgnoreCase);
					if (!regex.IsMatch(this.client.RemoteCertificate.Subject))
					{
						throw new Exception(string.Format(format, "subject", this.WorkDefinition.ExpectedServerCertificate.Subject, this.client.RemoteCertificate.Subject));
					}
				}
				if (this.WorkDefinition.ExpectedServerCertificate.Issuer != null)
				{
					Regex regex2 = new Regex(this.WorkDefinition.ExpectedServerCertificate.Issuer, RegexOptions.IgnoreCase);
					if (!regex2.IsMatch(this.client.RemoteCertificate.Issuer))
					{
						throw new Exception(string.Format(format, "issuer", this.WorkDefinition.ExpectedServerCertificate.Issuer, this.client.RemoteCertificate.Issuer));
					}
				}
				if (this.WorkDefinition.ExpectedServerCertificate.ValidFrom != null && !(this.client.RemoteCertificate.ValidFrom <= this.WorkDefinition.ExpectedServerCertificate.ValidFrom))
				{
					throw new Exception(string.Format(format, "effective date", this.WorkDefinition.ExpectedServerCertificate.ValidFrom, this.client.RemoteCertificate.ValidFrom));
				}
				if (this.WorkDefinition.ExpectedServerCertificate.ValidTo != null && !(this.client.RemoteCertificate.ValidTo >= this.WorkDefinition.ExpectedServerCertificate.ValidTo))
				{
					throw new Exception(string.Format(format, "expiration date", this.WorkDefinition.ExpectedServerCertificate.ValidTo, this.client.RemoteCertificate.ValidTo));
				}
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00032E14 File Offset: 0x00031014
		private void TestConnection()
		{
			try
			{
				SmtpConnectionProbe.SetResolvedEndpoint(base.Result, this.ResolvedEndPoint);
				SmtpConnectionProbe.SetPortNumber(base.Result, this.WorkDefinition.Port);
				this.MeasureLatency("Connect", () => this.Client.Connect(this.ResolvedEndPoint, this.WorkDefinition.Port, this.DisconnectBetweenSessions), ConnectionLostPoint.OnConnect);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceInformation<string, int, Exception>(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "Unable to connect to server {0} on port {1} due to the following exception: {2}", this.ResolvedEndPoint, this.WorkDefinition.Port, arg, null, "TestConnection", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 1321);
				throw;
			}
			if (this.ShouldCancelProbe())
			{
				return;
			}
			this.AssertExpectedResponse(this.WorkDefinition.ExpectedResponseOnConnect);
			this.AfterConnect();
			this.MeasureLatency("EHLO", delegate()
			{
				this.Client.Ehlo(this.WorkDefinition.HeloDomain);
			}, ConnectionLostPoint.OnHelo);
			SmtpConnectionProbe.SetAdvertisedServerName(base.Result, this.Client.AdvertisedServerName);
			if (this.ShouldCancelProbe())
			{
				return;
			}
			this.AssertExpectedResponse(this.WorkDefinition.ExpectedResponseOnHelo);
			this.AfterHelo();
			if (this.WorkDefinition.UseSsl || this.WorkDefinition.AuthenticationType == AuthenticationType.Exchange)
			{
				if (this.WorkDefinition.ClientCertificate != null && this.WorkDefinition.AuthenticationType != AuthenticationType.Exchange)
				{
					this.AddClientCertificatesToSmtp();
				}
				this.MeasureLatency("STARTTLS", delegate()
				{
					this.Client.StartTls(this.WorkDefinition.AuthenticationType == AuthenticationType.Exchange);
				});
				if (this.ShouldCancelProbe())
				{
					return;
				}
				this.AssertExpectedResponse(this.WorkDefinition.ExpectedResponseOnStartTls);
				this.AssertExpectedServerCertificate();
				this.AfterStartTls();
				this.MeasureLatency("EHLO", delegate()
				{
					this.Client.Ehlo(this.WorkDefinition.HeloDomain);
				}, ConnectionLostPoint.OnHeloAfterStartTls);
				if (this.ShouldCancelProbe())
				{
					return;
				}
				this.AssertExpectedResponse(this.WorkDefinition.ExpectedResponseOnHeloAfterStartTls);
				this.AfterHeloAfterStartTls();
			}
			if (this.WorkDefinition.AuthenticationType != AuthenticationType.Anonymous)
			{
				switch (this.WorkDefinition.AuthenticationType)
				{
				case AuthenticationType.AuthLogin:
					this.MeasureLatency("AuthLogin", delegate()
					{
						this.Client.AuthLogin(this.WorkDefinition.AuthenticationAccount.Username, this.WorkDefinition.AuthenticationAccount.Password);
					}, ConnectionLostPoint.OnAuthenticate);
					break;
				case AuthenticationType.Exchange:
					this.MeasureLatency("ExchangeAuth", delegate()
					{
						this.Client.ExchangeAuth();
					}, ConnectionLostPoint.OnAuthenticate);
					break;
				default:
					throw new ArgumentException("Unexpected Authentication type specified in the work definition.");
				}
				if (this.ShouldCancelProbe())
				{
					return;
				}
				this.AssertExpectedResponse(this.WorkDefinition.ExpectedResponseOnAuthenticate);
				this.AfterAuthenticate();
			}
			if (!string.IsNullOrEmpty(this.WorkDefinition.MailFrom))
			{
				this.MeasureLatency("MAILFROM", delegate()
				{
					this.Client.MailFrom(this.GetMailFromArguments());
				}, ConnectionLostPoint.OnMailFrom);
				if (this.ShouldCancelProbe())
				{
					return;
				}
				this.AssertExpectedResponse(this.WorkDefinition.ExpectedResponseOnMailFrom);
				this.AfterMailFrom();
			}
			this.BeforeRcptTo();
			if (this.WorkDefinition.MailTo != null)
			{
				using (IEnumerator<SmtpRecipient> enumerator = this.WorkDefinition.MailTo.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SmtpRecipient recipient = enumerator.Current;
						this.MeasureLatency("RCPTTO", delegate()
						{
							this.Client.RcptTo(recipient.Username);
						}, ConnectionLostPoint.OnRcptTo);
						if (this.ShouldCancelProbe())
						{
							return;
						}
						this.AssertExpectedResponse(recipient.ExpectedResponse);
					}
				}
				this.AfterRcptTo();
			}
			this.BeforeData();
			if (!string.IsNullOrEmpty(this.WorkDefinition.Data))
			{
				this.MeasureLatency("DATA", delegate()
				{
					this.Client.Data(string.Format("{0}:{1}\r\n{2}", "X-MS-Exchange-ActiveMonitoringProbeName", base.Definition.Name, this.WorkDefinition.Data));
				}, ConnectionLostPoint.OnData);
				if (this.ShouldCancelProbe())
				{
					return;
				}
				this.AssertExpectedResponse(this.WorkDefinition.ExpectedResponseOnData);
				this.AfterData();
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00033250 File Offset: 0x00031450
		private void RunCustomCommands(CustomCommandRunPoint runPoint)
		{
			IEnumerable<SmtpCustomCommand> enumerable = from p in this.WorkDefinition.CustomCommands
			where p.CustomCommandRunPoint == runPoint
			select p;
			using (IEnumerator<SmtpCustomCommand> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SmtpCustomCommand customCommand = enumerator.Current;
					this.MeasureLatency(customCommand.Name, delegate()
					{
						this.Client.Send(customCommand.Name + " " + customCommand.Arguments);
					});
					if (this.ShouldCancelProbe())
					{
						break;
					}
					this.AssertExpectedResponse(customCommand.ExpectedResponse);
				}
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00033318 File Offset: 0x00031518
		private void AddClientCertificatesToSmtp()
		{
			this.Client.ClientCertificates = new X509CertificateCollection();
			if (!string.IsNullOrEmpty(this.WorkDefinition.ClientCertificate.FindValue))
			{
				X509Store x509Store = new X509Store(this.WorkDefinition.ClientCertificate.StoreName, this.WorkDefinition.ClientCertificate.StoreLocation);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				try
				{
					this.Client.ClientCertificates.AddRange(x509Store.Certificates.Find(this.WorkDefinition.ClientCertificate.FindType, this.WorkDefinition.ClientCertificate.FindValue, true));
					WTFDiagnostics.TraceInformation<int>(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "Found {0} client certificate(s) matching specified criteria.", this.Client.ClientCertificates.Count, null, "AddClientCertificatesToSmtp", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 1534);
					goto IL_19D;
				}
				finally
				{
					x509Store.Close();
				}
			}
			if (!string.IsNullOrEmpty(this.WorkDefinition.ClientCertificate.TransportCertificateName))
			{
				this.Client.ClientCertificates.Add(this.cache.Find(this.WorkDefinition.ClientCertificate.TransportCertificateName));
			}
			else if (!string.IsNullOrEmpty(this.WorkDefinition.ClientCertificate.TransportCertificateFqdn))
			{
				string item = string.IsNullOrEmpty(this.WorkDefinition.ClientCertificate.TransportCertificateFqdn) ? ComputerInformation.DnsPhysicalFullyQualifiedDomainName : this.WorkDefinition.ClientCertificate.TransportCertificateFqdn;
				List<string> list = new List<string>(1);
				list.Add(item);
				this.Client.ClientCertificates.Add(this.cache.Find(list, true, this.WorkDefinition.ClientCertificate.TransportWildcardMatchType));
			}
			IL_19D:
			foreach (X509Certificate x509Certificate in this.Client.ClientCertificates)
			{
				WTFDiagnostics.TraceDebug<string, string, string, string>(ExTraceGlobals.SMTPConnectionTracer, new TracingContext(), "Subject: {0}, Issuer: {1}, Effective: {2} to {3}.\r\n", x509Certificate.Subject, x509Certificate.Issuer, x509Certificate.GetEffectiveDateString(), x509Certificate.GetExpirationDateString(), null, "AddClientCertificatesToSmtp", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpConnectionProbe.cs", 1566);
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00033558 File Offset: 0x00031758
		private bool IsHubTransportInDrainingState()
		{
			Assembly assembly = Assembly.Load("Microsoft.Exchange.Data.Directory");
			if (assembly == null)
			{
				return false;
			}
			Type type = assembly.GetType("Microsoft.Exchange.Data.Directory.ServerComponentStateManager");
			if (type == null)
			{
				return false;
			}
			object[] parameters = new object[]
			{
				ServerComponentEnum.HubTransport,
				false
			};
			Type[] types = new Type[]
			{
				typeof(ServerComponentEnum),
				typeof(bool)
			};
			object obj = type.GetMethod("GetEffectiveState", types).Invoke(null, parameters);
			return obj.ToString() == "Draining";
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00033600 File Offset: 0x00031800
		private string GetMailFromArguments()
		{
			bool flag = base.Broker.IsLocal() && Datacenter.IsMicrosoftHostedOnly(true) && !string.IsNullOrEmpty(this.WorkDefinition.Data) && this.WorkDefinition.AddAttributions;
			string text2;
			if (flag)
			{
				string text = (this.WorkDefinition.Direction == Directionality.Incoming) ? this.WorkDefinition.RecipientTenantID : this.WorkDefinition.SenderTenantID;
				if (string.IsNullOrWhiteSpace(text))
				{
					text2 = string.Format("{0} XATTRDIRECT={1}", this.WorkDefinition.MailFrom, SmtpUtils.ToXtextString(this.WorkDefinition.Direction.ToString(), false));
				}
				else
				{
					text2 = string.Format("{0} XATTRDIRECT={1} XATTRORGID=xorgid:{2}", this.WorkDefinition.MailFrom, SmtpUtils.ToXtextString(this.WorkDefinition.Direction.ToString(), false), SmtpUtils.ToXtextString(text, false));
				}
			}
			else
			{
				text2 = this.WorkDefinition.MailFrom;
			}
			if (this.Client.IsXSysProbeAdvertised)
			{
				text2 += string.Format(" XSYSPROBEID={0}", this.GetProbeId());
			}
			return text2;
		}

		// Token: 0x04000821 RID: 2081
		internal const string ResolvedEndpointBeforeConnect = "The connection has not been established";

		// Token: 0x04000822 RID: 2082
		private SmtpConnectionProbeWorkDefinition workDefinition;

		// Token: 0x04000823 RID: 2083
		private string resolvedEndPoint;

		// Token: 0x04000824 RID: 2084
		private ISimpleSmtpClient client;

		// Token: 0x04000825 RID: 2085
		private int testCount;

		// Token: 0x04000826 RID: 2086
		private CertificateCache cache;

		// Token: 0x0200021A RID: 538
		// (Invoke) Token: 0x0600118A RID: 4490
		internal delegate T ActionWithReturn<T>();
	}
}
