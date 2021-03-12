using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Ews.Probes
{
	// Token: 0x02000176 RID: 374
	public abstract class EWSGenericProbeCommon : EWSCommon
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00044A66 File Offset: 0x00042C66
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00044A6E File Offset: 0x00042C6E
		protected string OperationName { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00044A77 File Offset: 0x00042C77
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x00044A7F File Offset: 0x00042C7F
		protected bool IsVerifyCookieAffinity { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00044A88 File Offset: 0x00042C88
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x00044A90 File Offset: 0x00042C90
		protected bool VerifyXropEndPoint { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00044A99 File Offset: 0x00042C99
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00044AA1 File Offset: 0x00042CA1
		protected string SecondaryOperationName { get; set; }

		// Token: 0x06000AEF RID: 2799 RVA: 0x00044AAC File Offset: 0x00042CAC
		protected override void Configure()
		{
			this.OperationName = this.ReadAttribute("OperationName", "ConvertId");
			this.IsVerifyCookieAffinity = this.ReadAttribute("VerifyCookieAffinity", false);
			this.VerifyXropEndPoint = this.ReadAttribute("VerifyXropEndpoint", false);
			this.SecondaryOperationName = this.ReadAttribute("SecondaryOperationName", "ConvertId");
			base.Configure();
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00044B2C File Offset: 0x00042D2C
		protected void RunEWSGenericProbe(CancellationToken cancellationToken)
		{
			base.Initialize(ExTraceGlobals.EWSTracer);
			if (15 == base.MailboxVersion && base.IsOutsideInMonitoring && base.CafeVipModeWhenPossible)
			{
				this.componentIdForRequest = "UnifiedNamespaceAMProbe";
				CafeUtils.DoWorkWithUnifiedNamespace(new CafeUtils.DoWork(this.DoWorkInternal), cancellationToken, false, base.Definition, base.Broker, base.UnifiedNamespace);
				return;
			}
			if (base.TrustAnySSLCertificate)
			{
				string componentId = "Ews_AM_Probe";
				RemoteCertificateValidationCallback callback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
				{
					if (base.SslValidationDelaySeconds != 0)
					{
						Thread.Sleep(base.SslValidationDelaySeconds * 1000);
					}
					return true;
				};
				CertificateValidationManager.RegisterCallback(componentId, callback);
				this.componentIdForRequest = componentId;
			}
			this.DoWorkInternal(cancellationToken);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00044BC8 File Offset: 0x00042DC8
		private void ExecuteEWSCall(string endPoint, string operation, bool verifyAffinity, CancellationToken cancellationToken, bool trackLatency = false)
		{
			base.LogTrace(string.Format("Starting EwsGenericProbe with Username: {0} ", base.Definition.Account));
			ExchangeService exchangeService = base.GetExchangeService(base.Definition.Account, base.Definition.AccountPassword, new EWSCommon.TraceListener(base.TraceContext, ExTraceGlobals.EWSTracer), base.ConstructEwsUrl(endPoint), 2);
			if (!string.IsNullOrEmpty(this.componentIdForRequest))
			{
				exchangeService.SetComponentId(this.componentIdForRequest);
			}
			this.PerformEWSOperation(exchangeService, operation, trackLatency, cancellationToken);
			if (verifyAffinity)
			{
				base.DecorateLogSection("COOKIE AFFINITY VERIFICATION");
				this.VerifyCookieAffinity(exchangeService, this.OperationName, cancellationToken);
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00044C68 File Offset: 0x00042E68
		private void DoWorkInternal(CancellationToken cancellationToken)
		{
			try
			{
				base.LogTrace(string.Format("Executing EWS Generic probe with {0}", base.EffectiveAuthN));
				base.LatencyMeasurement.Start();
				base.DecorateLogSection("PRIMARY ENDPOINT VERIFICATION");
				if (base.IsOutsideInMonitoring)
				{
					if (base.MailboxVersion == 14)
					{
						this.ExecuteEWSCall(base.Definition.Endpoint, this.SecondaryOperationName, this.IsVerifyCookieAffinity, cancellationToken, false);
					}
					else
					{
						this.ExecuteEWSCall(base.Definition.Endpoint, this.OperationName, this.IsVerifyCookieAffinity, cancellationToken, true);
					}
				}
				else
				{
					this.ExecuteEWSCall(base.Definition.Endpoint, this.OperationName, this.IsVerifyCookieAffinity, cancellationToken, false);
				}
				if (this.VerifyXropEndPoint)
				{
					base.LogTrace(string.Format("Executing EWS Generic probe against xrop endpoint: {0}", this.SecondaryOperationName));
					base.DecorateLogSection("XROP ENDPOINT VERIFICATION");
					this.ExecuteEWSCall(base.Definition.SecondaryEndpoint, this.SecondaryOperationName, false, cancellationToken, false);
				}
				base.LatencyMeasurement.Stop();
			}
			catch (Exception e)
			{
				this.RecordEWSProbeError(e);
			}
			finally
			{
				base.LatencyMeasurement.Stop();
				base.Result.StateAttribute20 = (double)base.LatencyMeasurement.ElapsedMilliseconds;
			}
			this.RecordProbeResult();
			base.WriteVitalInfoToExecutionContext();
			base.LogTrace("EwsGenericProbe succeeded");
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00044DE4 File Offset: 0x00042FE4
		private void RecordProbeResult()
		{
			if (!string.IsNullOrEmpty(this.probeErrorResult))
			{
				base.ThrowError("EWSGenericProbeError", new Exception(this.probeErrorResult), "");
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00044E0E File Offset: 0x0004300E
		private void RecordEWSProbeError(Exception e)
		{
			base.LogTrace(string.Format("EWS Generic Probe failed while using {0}", base.EffectiveAuthN));
			this.probeErrorResult += e.ToString();
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00045004 File Offset: 0x00043204
		private void PerformEWSOperation(ExchangeService service, string operation, bool trackLatency, CancellationToken cancellationToken)
		{
			if (operation != null)
			{
				if (operation == "GetFolder")
				{
					base.RetrySoapActionAndThrow(delegate()
					{
						PropertySet propertySet = new PropertySet(0);
						Folder.Bind(service, 4, propertySet);
					}, operation, service, cancellationToken, trackLatency);
					return;
				}
				if (operation == "SendEmail")
				{
					base.RetrySoapActionAndThrow(delegate()
					{
						string text = "OBD Test EWS MessageSent ";
						SearchFilter searchFilter = new SearchFilter.ContainsSubstring(ItemSchema.Subject, text);
						ItemView itemView = new ItemView(10);
						itemView.PropertySet = new PropertySet(0);
						FindItemsResults<Item> findItemsResults = service.FindItems(4, searchFilter, itemView);
						foreach (Item item in findItemsResults.Items)
						{
							item.Delete(0);
						}
						EmailMessage emailMessage = new EmailMessage(service);
						string subject = text + Guid.NewGuid().ToString();
						emailMessage.Subject = subject;
						emailMessage.Body = text;
						emailMessage.ToRecipients.Add(this.Definition.Account);
						emailMessage.Save(3);
						emailMessage.Load();
						AlternateId alternateId = new AlternateId(1, emailMessage.Id.ToString(), this.Definition.Account);
						AlternateId alternateId2 = service.ConvertId(alternateId, 2) as AlternateId;
						this.Result.StateAttribute22 = alternateId2.UniqueId.ToString();
						this.Result.StateAttribute23 = emailMessage.InternetMessageId;
						emailMessage.Send();
					}, operation, service, cancellationToken, trackLatency);
					return;
				}
				if (!(operation == "ConvertId"))
				{
				}
			}
			base.RetrySoapActionAndThrow(delegate()
			{
				AlternateId alternateId = new AlternateId(3, "00000000EEC1BD786111D011917B00000000000107002FBF98FC7852CF11912A000000000001000002AD4860000052FA68AB0CD61C4681AA421824823451000057E89BBD0000", "sarrusaphone@bombastadron.com");
				service.ConvertId(alternateId, 4);
			}, operation, service, cancellationToken);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000450C4 File Offset: 0x000432C4
		private void VerifyCookieAffinity(ExchangeService service, string operationName, CancellationToken cancellationToken)
		{
			string text = string.Empty;
			if (base.MailboxVersion == 14)
			{
				text = service.HttpResponseHeaders["X-DiagInfo"];
				if (string.IsNullOrEmpty(text))
				{
					this.AddTracesAndThrow("The CAS name we expected to find in the X-DiagInfo header after the first request was either null or empty.", service);
				}
			}
			this.PerformEWSOperation(service, operationName, true, cancellationToken);
			if (base.MailboxVersion == 14)
			{
				string text2 = service.HttpResponseHeaders["X-DiagInfo"];
				if (string.IsNullOrEmpty(text2))
				{
					this.AddTracesAndThrow("The CAS name we expected to find in the X-DiagInfo header after the second request was either null or empty.", service);
				}
				if (!text.Equals(text2))
				{
					this.AddTracesAndThrow("The CAS names from the X-DiagInfoHeader from consecutive requests using the same service object dont match. Affinity may not be working correctly. CAS from 1st request: " + text + ". CAS from second request: " + text2, service);
				}
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00045160 File Offset: 0x00043360
		private void AddTracesAndThrow(string errorMessage, ExchangeService service)
		{
			base.TraceBuilder.AppendLine(((EWSCommon.TraceListener)service.TraceListener).RequestLog);
			throw new Exception(errorMessage);
		}

		// Token: 0x0400082C RID: 2092
		protected const string DefaultOperationName = "ConvertId";

		// Token: 0x0400082D RID: 2093
		private const string XDiagInfoHeaderName = "X-DiagInfo";

		// Token: 0x0400082E RID: 2094
		private string componentIdForRequest;

		// Token: 0x0400082F RID: 2095
		private string probeErrorResult = string.Empty;
	}
}
