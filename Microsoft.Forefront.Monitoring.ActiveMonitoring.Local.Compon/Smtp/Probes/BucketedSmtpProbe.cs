using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000212 RID: 530
	public class BucketedSmtpProbe : SmtpProbe
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x0002EE7C File Offset: 0x0002D07C
		public BucketedSmtpProbe() : this(null, new BasicSmtpClientFactory())
		{
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0002EE8A File Offset: 0x0002D08A
		public BucketedSmtpProbe(IPop3Client popClient, IMinimalSmtpClientFactory smtpClientFactory) : base(popClient, smtpClientFactory)
		{
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0002EE94 File Offset: 0x0002D094
		public static string GetSmtpResponseReceived(ProbeResult result)
		{
			return result.StateAttribute2;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0002EE9C File Offset: 0x0002D09C
		public static void SetSmtpResponseReceived(ProbeResult result, string value)
		{
			result.StateAttribute2 = value;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0002EEA5 File Offset: 0x0002D0A5
		public static string GetFDServerEncountered(ProbeResult result)
		{
			return result.StateAttribute3;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0002EEAD File Offset: 0x0002D0AD
		public static void SetFDServerEncountered(ProbeResult result, string value)
		{
			result.StateAttribute3 = value;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0002EEB6 File Offset: 0x0002D0B6
		public static string GetMbxDatabase(ProbeResult result)
		{
			return result.StateAttribute4;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0002EEBE File Offset: 0x0002D0BE
		public static void SetMbxDatabase(ProbeResult result, string value)
		{
			result.StateAttribute4 = value;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0002EEC7 File Offset: 0x0002D0C7
		public static string GetMbxDatabaseVersion(ProbeResult result)
		{
			return result.StateAttribute5;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0002EECF File Offset: 0x0002D0CF
		public static void SetMbxDatabaseVersion(ProbeResult result, string value)
		{
			result.StateAttribute5 = value;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		public static string GetHubServer(ProbeResult result)
		{
			return result.StateAttribute13;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
		public static void SetHubServer(ProbeResult result, string value)
		{
			result.StateAttribute13 = value;
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0002EEE9 File Offset: 0x0002D0E9
		public static string GetTargetFQDN(ProbeResult result)
		{
			return result.StateAttribute14;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0002EEF1 File Offset: 0x0002D0F1
		public static void SetTargetFQDN(ProbeResult result, string value)
		{
			result.StateAttribute14 = value;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0002EEFA File Offset: 0x0002D0FA
		public static string GetTargetVIP(ProbeResult result)
		{
			return result.StateAttribute15;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0002EF02 File Offset: 0x0002D102
		public static void SetTargetVIP(ProbeResult result, string value)
		{
			result.StateAttribute15 = value;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0002EF0B File Offset: 0x0002D10B
		public static string GetEhloIssued(ProbeResult result)
		{
			return result.StateAttribute21;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0002EF13 File Offset: 0x0002D113
		public static void SetEhloIssued(ProbeResult result, string value)
		{
			result.StateAttribute21 = value;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0002EF1C File Offset: 0x0002D11C
		public static string GetExchangeMessageID(ProbeResult result)
		{
			return result.StateAttribute23;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0002EF24 File Offset: 0x0002D124
		public static void SetExchangeMessageID(ProbeResult result, string value)
		{
			result.StateAttribute23 = value;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0002EF30 File Offset: 0x0002D130
		protected override void SetDefaultAttributeValues()
		{
			base.SetDefaultAttributeValues();
			BucketedSmtpProbe.SetEhloIssued(base.Result, "None. Probe exited before EHLO");
			BucketedSmtpProbe.SetExchangeMessageID(base.Result, "None. Probe exited before BDAT response");
			BucketedSmtpProbe.SetFDServerEncountered(base.Result, "None. Probe exited before EHLO response");
			BucketedSmtpProbe.SetMbxDatabase(base.Result, "None. Probe exited before database discovered");
			BucketedSmtpProbe.SetMbxDatabaseVersion(base.Result, "None. Probe exited before database discovered");
			BucketedSmtpProbe.SetSmtpResponseReceived(base.Result, "None. Probe exited before any smtp response received");
			BucketedSmtpProbe.SetTargetFQDN(base.Result, "None. Probe exited before discovering FQDN");
			BucketedSmtpProbe.SetTargetVIP(base.Result, "None. Probe exited before resolving IP");
			BucketedSmtpProbe.SetHubServer(base.Result, "None. Probe exited before BDAT response");
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002EFD4 File Offset: 0x0002D1D4
		protected override void LoadAdditionalData()
		{
			base.LoadAdditionalData();
			if (base.WorkDefinition != null && base.WorkDefinition.SendMail != null && !string.IsNullOrWhiteSpace(base.WorkDefinition.SendMail.OriginalFQDN))
			{
				BucketedSmtpProbe.SetTargetFQDN(base.Result, base.WorkDefinition.SendMail.OriginalFQDN);
			}
			if (base.WorkDefinition != null && base.WorkDefinition.TargetData != null)
			{
				if (!string.IsNullOrWhiteSpace(base.WorkDefinition.TargetData.MailboxDatabase))
				{
					BucketedSmtpProbe.SetMbxDatabase(base.Result, base.WorkDefinition.TargetData.MailboxDatabase);
				}
				if (!string.IsNullOrWhiteSpace(base.WorkDefinition.TargetData.MailboxDatabaseVersion))
				{
					BucketedSmtpProbe.SetMbxDatabaseVersion(base.Result, base.WorkDefinition.TargetData.MailboxDatabaseVersion);
				}
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002F0A8 File Offset: 0x0002D2A8
		protected override void CopySendDataToDeliver(ProbeStatus sendStatus, ProbeResult deliverResult)
		{
			base.CopySendDataToDeliver(sendStatus, deliverResult);
			BucketedSmtpProbe.SetEhloIssued(deliverResult, sendStatus.EhloIssued);
			BucketedSmtpProbe.SetExchangeMessageID(deliverResult, sendStatus.ExchangeMessageId);
			BucketedSmtpProbe.SetFDServerEncountered(deliverResult, sendStatus.FDServerEncountered);
			BucketedSmtpProbe.SetSmtpResponseReceived(deliverResult, sendStatus.SmtpResponseReceived);
			BucketedSmtpProbe.SetTargetVIP(deliverResult, sendStatus.TargetVIP);
			BucketedSmtpProbe.SetHubServer(deliverResult, sendStatus.HubServer);
			BucketedSmtpProbe.SetTargetFQDN(deliverResult, base.WorkDefinition.SendMail.OriginalFQDN);
			BucketedSmtpProbe.SetMbxDatabase(deliverResult, base.WorkDefinition.TargetData.MailboxDatabase);
			BucketedSmtpProbe.SetMbxDatabaseVersion(deliverResult, base.WorkDefinition.TargetData.MailboxDatabaseVersion);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0002F148 File Offset: 0x0002D348
		protected override void UpdateProbeExecutionData(IMinimalSmtpClient client)
		{
			base.UpdateProbeExecutionData(client);
			RawSmtpClientWrapper rawSmtpClientWrapper = client as RawSmtpClientWrapper;
			if (rawSmtpClientWrapper == null)
			{
				return;
			}
			IPAddress ipaddress = null;
			if (!string.IsNullOrEmpty(rawSmtpClientWrapper.Host) && base.WorkDefinition.SendMail.ResolveEndPoint)
			{
				BucketedSmtpProbe.SetTargetVIP(base.Result, rawSmtpClientWrapper.Host);
			}
			else if (!string.IsNullOrEmpty(rawSmtpClientWrapper.Host) && IPAddress.TryParse(rawSmtpClientWrapper.Host, out ipaddress))
			{
				BucketedSmtpProbe.SetTargetVIP(base.Result, rawSmtpClientWrapper.Host);
			}
			if (!string.IsNullOrWhiteSpace(rawSmtpClientWrapper.FDContacted))
			{
				BucketedSmtpProbe.SetFDServerEncountered(base.Result, rawSmtpClientWrapper.FDContacted);
			}
			if (!string.IsNullOrWhiteSpace(rawSmtpClientWrapper.EhloSent))
			{
				BucketedSmtpProbe.SetEhloIssued(base.Result, rawSmtpClientWrapper.EhloSent);
			}
			if (!string.IsNullOrWhiteSpace(rawSmtpClientWrapper.ExchangeMessageId))
			{
				BucketedSmtpProbe.SetExchangeMessageID(base.Result, rawSmtpClientWrapper.ExchangeMessageId.Trim(new char[]
				{
					'<',
					'>'
				}));
			}
			if (!string.IsNullOrWhiteSpace(rawSmtpClientWrapper.HubServer))
			{
				BucketedSmtpProbe.SetHubServer(base.Result, rawSmtpClientWrapper.HubServer);
			}
			if (!string.IsNullOrWhiteSpace(rawSmtpClientWrapper.LastResponse))
			{
				if (rawSmtpClientWrapper.SuccessfullySentLastMail)
				{
					BucketedSmtpProbe.SetSmtpResponseReceived(base.Result, "250 2.6.0 <ID Omitted> Queued mail for delivery");
				}
				else if (rawSmtpClientWrapper.LastResponseCode == SimpleSmtpClient.SmtpResponseCode.OK && rawSmtpClientWrapper.LastCommand.IndexOf("EHLO", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					string value = rawSmtpClientWrapper.LastResponse.Substring(rawSmtpClientWrapper.LastResponse.IndexOf(Environment.NewLine, StringComparison.Ordinal));
					BucketedSmtpProbe.SetSmtpResponseReceived(base.Result, value);
				}
				else
				{
					BucketedSmtpProbe.SetSmtpResponseReceived(base.Result, rawSmtpClientWrapper.LastResponse);
				}
			}
			if (rawSmtpClientWrapper.LastEncounteredException != null)
			{
				this.ClassifyError(rawSmtpClientWrapper.LastResponse, rawSmtpClientWrapper.LastEncounteredException, rawSmtpClientWrapper.LastCommand);
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0002F2FC File Offset: 0x0002D4FC
		protected override void HandleCheckMailTimeOutException(TimeSpan checkMailTime)
		{
			string probeErrorType = SmtpProbe.GetProbeErrorType(base.Result);
			if (probeErrorType == MailErrorType.MailboxLoginFailure.ToString() || probeErrorType == MailErrorType.SqlQueryFailure.ToString() || probeErrorType == MailErrorType.SaveStatusTimeout.ToString())
			{
				return;
			}
			base.HandleCheckMailTimeOutException(checkMailTime);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0002F35C File Offset: 0x0002D55C
		protected override void HandleUumTimeOutException(TimeSpan uumTime)
		{
			string probeErrorType = SmtpProbe.GetProbeErrorType(base.Result);
			if (probeErrorType == MailErrorType.SqlQueryFailure.ToString() || probeErrorType == MailErrorType.SaveStatusTimeout.ToString())
			{
				return;
			}
			base.HandleUumTimeOutException(uumTime);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0002F3A8 File Offset: 0x0002D5A8
		protected override void HandleSmtpSendTimeOutException(TimeSpan sendMailTime)
		{
			if (SmtpProbe.GetProbeErrorType(base.Result) == MailErrorType.SmtpSendAuthTimeOut.ToString() || SmtpProbe.GetProbeErrorType(base.Result) == MailErrorType.SendMailConnectTimeOut.ToString())
			{
				return;
			}
			base.HandleSmtpSendTimeOutException(sendMailTime);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0002F3F9 File Offset: 0x0002D5F9
		protected override void HandleDnsFailure(ResolverHelper.UnableToResolveException e)
		{
			base.HandleDnsFailure(e);
			if (!string.IsNullOrWhiteSpace(e.Domain))
			{
				BucketedSmtpProbe.SetTargetFQDN(base.Result, e.Domain);
			}
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0002F43C File Offset: 0x0002D63C
		protected override void HandleLoginException(Pop3Exception e)
		{
			if (string.IsNullOrWhiteSpace(e.LastResponse))
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.MailboxLoginFailure);
				return;
			}
			List<string> list = (from errorPattern in BucketedSmtpProbe.popErrorMapping.Keys
			where TransportProbeCommon.ErrorMatches(e.LastResponse, errorPattern)
			select errorPattern).ToList<string>();
			if (list.Count<string>() > 1)
			{
				throw new Exception(string.Format("The BucketedSmtpProbe encountered a POP error that matches multiple known error patterns. The last response was {1}{0}  The following known error patterns match {2}", Environment.NewLine, e.LastResponse, string.Join("," + Environment.NewLine, list)));
			}
			if (list.Count<string>() == 1)
			{
				SmtpProbe.SetProbeErrorType(base.Result, BucketedSmtpProbe.popErrorMapping[list.First<string>()]);
				return;
			}
			SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.MailboxLoginFailure);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0002F514 File Offset: 0x0002D714
		protected override void FindProbeResultsInternal()
		{
			if (!base.WorkDefinition.IsCortex)
			{
				base.FindProbeResultsInternal();
				return;
			}
			base.AllPreviousSendResults = new List<ProbeStatus>();
			base.PreviousSuccessSendResults = new List<ProbeStatus>();
			base.PreviousDeliverResults = new List<ProbeStatus>();
			int queryTimeWindow = base.WorkDefinition.CheckMail.QueryTimeWindow;
			DateTime executionStartTime = base.Result.ExecutionStartTime;
			DateTime t = base.Result.ExecutionStartTime.AddSeconds((double)(-2 * queryTimeWindow)).AddMinutes(-base.WorkDefinition.SendMail.Sla * 2.0);
			string key = base.Definition.ConstructWorkItemResultName();
			Task<StatusEntryCollection> statusEntries = base.Broker.GetStatusEntries(key, base.CancellationToken, base.TraceContext);
			statusEntries.Wait(base.CancellationToken);
			this.collection = statusEntries.Result;
			bool flag = false;
			foreach (StatusEntry entry in this.collection)
			{
				ProbeStatus probeStatus = new ProbeStatus(entry);
				if (probeStatus.SentTime < t)
				{
					this.collection.Remove(entry);
					flag = true;
				}
				else if (probeStatus.InternalProbeId.Contains(base.WorkDefinition.SendMail.Message.SubjectOverride))
				{
					if (probeStatus.RecordType == RecordType.SendMail)
					{
						base.AllPreviousSendResults.Add(probeStatus);
						if (probeStatus.ResultType == ResultType.Succeeded)
						{
							base.PreviousSuccessSendResults.Add(probeStatus);
						}
					}
					else if (probeStatus.RecordType == RecordType.DeliverMail)
					{
						base.PreviousDeliverResults.Add(probeStatus);
					}
					else
					{
						base.TraceError("Unknown record type={0}, ID={1}", new object[]
						{
							probeStatus.RecordType,
							probeStatus.InternalProbeId
						});
					}
				}
			}
			if (flag)
			{
				base.Broker.SaveStatusEntries(this.collection, base.CancellationToken, base.TraceContext);
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0002F720 File Offset: 0x0002D920
		protected override void HandleResultPublished(ProbeResult result)
		{
			if (!base.WorkDefinition.IsCortex)
			{
				base.HandleResultPublished(result);
				return;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			if (this.collection == null)
			{
				try
				{
					string key = base.Definition.ConstructWorkItemResultName();
					Task<StatusEntryCollection> statusEntries = base.Broker.GetStatusEntries(key, base.CancellationToken, base.TraceContext);
					statusEntries.Wait(base.CancellationToken);
					this.collection = statusEntries.Result;
				}
				catch (AggregateException ex)
				{
					base.TraceError(ex.Flatten(), "HandleResultPublished error", new object[0]);
					SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SqlQueryFailure);
				}
				catch (Exception e)
				{
					base.TraceError(e, "HandleResultPublished error", new object[0]);
					SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SqlQueryFailure);
				}
				finally
				{
					if (base.CancellationToken.IsCancellationRequested)
					{
						base.TraceDebug("Save Time:{0} seconds", new object[]
						{
							stopwatch.Elapsed.TotalSeconds
						});
						SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SaveStatusTimeout);
						throw new OperationCanceledException();
					}
				}
			}
			if (this.collection != null)
			{
				ProbeStatus probeStatus = new ProbeStatus(result);
				probeStatus.CreateStatusEntry(this.collection);
				Task task = base.Broker.SaveStatusEntries(this.collection, base.CancellationToken, base.TraceContext);
				task.Wait(base.CancellationToken);
				base.TraceDebug("Save Time:{0} seconds", new object[]
				{
					stopwatch.Elapsed.TotalSeconds
				});
				if (base.CancellationToken.IsCancellationRequested)
				{
					SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SaveStatusTimeout);
					throw new OperationCanceledException();
				}
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0002F910 File Offset: 0x0002DB10
		private void ClassifyError(string lastResponse, Exception lastException, string lastCommand)
		{
			if (lastException is OperationCanceledException && string.Equals(lastCommand, "AUTH LOGIN", StringComparison.InvariantCultureIgnoreCase))
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SmtpSendAuthTimeOut);
				return;
			}
			if (lastException is OperationCanceledException && string.IsNullOrWhiteSpace(lastCommand))
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SendMailConnectTimeOut);
				return;
			}
			if (lastException is OperationCanceledException)
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.ProbeTimeOut);
				return;
			}
			if (lastException is RawSmtpClientWrapper.InvalidCertificateException)
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.CertificateExpiredFailure);
				return;
			}
			if (lastException is RawSmtpClientWrapper.StartTlsNotAdvertisedException)
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.StartTlsNotAdvertisedFailure);
				return;
			}
			if (lastException is RawSmtpClientWrapper.MiscellaneousConnectionException)
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.UnableToConnect);
				return;
			}
			if (lastException is IOException)
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.ConnectionDroppedFailure);
			}
			List<string> list = (from errorPattern in BucketedSmtpProbe.smtpErrorMapping.Keys
			where TransportProbeCommon.ErrorMatches(lastResponse, errorPattern)
			select errorPattern).ToList<string>();
			if (list.Count<string>() > 1)
			{
				throw new Exception(string.Format("The BucketedSmtpProbe encountered an SMTP error that matches multiple known error patterns. The last response was {1}{0}  The following known error patterns match {2}", Environment.NewLine, lastResponse, string.Join("," + Environment.NewLine, list)));
			}
			if (list.Count<string>() == 1)
			{
				SmtpProbe.SetProbeErrorType(base.Result, BucketedSmtpProbe.smtpErrorMapping[list.First<string>()]);
				return;
			}
			SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.MiscAckFailure);
		}

		// Token: 0x040007D4 RID: 2004
		internal static readonly string SmtpResponseReceivedAttribute = "StateAttribute2";

		// Token: 0x040007D5 RID: 2005
		internal static readonly string FdServerEncounteredAttribute = "StateAttribute3";

		// Token: 0x040007D6 RID: 2006
		internal static readonly string MbxDatabaseAttribute = "StateAttribute4";

		// Token: 0x040007D7 RID: 2007
		internal static readonly string MbxDatabaseVersionAttribute = "StateAttribute5";

		// Token: 0x040007D8 RID: 2008
		internal static readonly string HubServerAttribute = "StateAttribute13";

		// Token: 0x040007D9 RID: 2009
		internal static readonly string TargetFQDNAttribute = "StateAttribute14";

		// Token: 0x040007DA RID: 2010
		internal static readonly string TargetVipAttribute = "StateAttribute15";

		// Token: 0x040007DB RID: 2011
		internal static readonly string EhloIssuedAttribute = "StateAttribute21";

		// Token: 0x040007DC RID: 2012
		internal static readonly string ExchangeMessageIdAttribute = "StateAttribute23";

		// Token: 0x040007DD RID: 2013
		private static readonly Dictionary<string, MailErrorType> popErrorMapping = new Dictionary<string, MailErrorType>
		{
			{
				"Error=ProxyFailed",
				MailErrorType.PopProxyFailure
			},
			{
				"Error=ProxyNotAuthenticated",
				MailErrorType.PopProxyFailure
			}
		};

		// Token: 0x040007DE RID: 2014
		private static readonly Dictionary<string, MailErrorType> smtpErrorMapping = new Dictionary<string, MailErrorType>
		{
			{
				"451 Temporary local problem - please retry later",
				MailErrorType.FfoAntispamFailure
			},
			{
				SmtpResponse.EnvelopeFilterNotReady.ToString(),
				MailErrorType.FfoAntispamFailure
			},
			{
				"550 5.7.7 Access Denied, Bad HELO",
				MailErrorType.FfoAntispamFailure
			},
			{
				"550 5.1.8 Access Denied, Bad outbound sender",
				MailErrorType.FfoAntispamFailure
			},
			{
				"550 5.1.8 Access Denied, Bad sender",
				MailErrorType.FfoAntispamFailure
			},
			{
				"550 5.1.2 Access Denied, Bad recipient",
				MailErrorType.FfoAntispamFailure
			},
			{
				"550 5.4.1 .*@.*: Recipient Address Rejected: Access Denied",
				MailErrorType.FfoAntispamFailure
			},
			{
				"550 5.7.1 Access Denied; the sending domain .* is not authorized to relay outbound mail through this host",
				MailErrorType.FfoAntispamFailure
			},
			{
				"550 5.7.1 Service unavailable; Client host .* blocked",
				MailErrorType.FfoAntispamFailure
			},
			{
				"550 5.7.1 Service unavailable; Client host .* rejected by recipient domain",
				MailErrorType.FfoAntispamFailure
			},
			{
				"550 5.7.1 Service unavailable, sending IPv6 address .* must have reverse DNS record",
				MailErrorType.FfoAntispamFailure
			},
			{
				"554 5.7.1 Service unavailable, message sent over IPv6 .* must pass SPF or DKIM validation (message not signed)",
				MailErrorType.FfoAntispamFailure
			},
			{
				SmtpResponse.TenantAttribution.UnattributableMailRejectSmtpResponse.ToString(),
				MailErrorType.FfoAttributionFailure
			},
			{
				SmtpResponse.TenantAttribution.RelayNotAllowedRejectSmtpResponse.ToString(),
				MailErrorType.FfoAttributionFailure
			},
			{
				SmtpResponse.TenantAttribution.AppConfigFfoHubMissingSmtpResponse.ToString(),
				MailErrorType.FfoAttributionFailure
			},
			{
				SmtpResponse.TenantAttribution.UnknownCustomerTypeSmtpResponse.ToString(),
				MailErrorType.FfoAttributionFailure
			},
			{
				SmtpResponse.TenantAttribution.MissingAttributionHeaderSmtpResponse.ToString(),
				MailErrorType.FfoAttributionFailure
			},
			{
				SmtpResponse.TenantAttribution.RecipientBelongsToDifferentDomainThanPreviouslyAttributedRejectSmtpResponse.ToString(),
				MailErrorType.FfoAttributionFailure
			},
			{
				"454 4.7.0 Failed to establish appropriate TLS channel: Access Denied",
				MailErrorType.FfoAttributionFailure
			},
			{
				SmtpResponse.EncodedProxyFailureResponseConnectionFailure.ToString(),
				MailErrorType.FfoConnectionFailure
			},
			{
				SmtpResponse.TenantAttribution.ExoSmtpNextHopDomainMissingForHostedCustomerSmtpResponse.ToString(),
				MailErrorType.FfoGlsFailure
			},
			{
				SmtpResponse.TenantAttribution.GlsMissingTenantPropertiesSmtpResponse.ToString(),
				MailErrorType.FfoGlsFailure
			},
			{
				SmtpResponse.TenantAttribution.DirectoryRequestOverThresholdSmtpResponse.ToString(),
				MailErrorType.FfoGlsFailure
			},
			{
				SmtpResponse.TenantAttribution.GlsRequestOverThresholdSmtpResponse.ToString(),
				MailErrorType.FfoGlsFailure
			},
			{
				SmtpResponse.TenantAttribution.DirectoryRequestFailureSmtpResponse.ToString(),
				MailErrorType.FfoGlsFailure
			},
			{
				SmtpResponse.TenantAttribution.GlsRequestErrorSmtpResponse.ToString(),
				MailErrorType.FfoGlsFailure
			},
			{
				SmtpResponse.EncodedProxyFailureResponseProtocolError.ToString(),
				MailErrorType.FfoProxyFailure
			},
			{
				SmtpResponse.EncodedProxyFailureResponseShutdown.ToString(),
				MailErrorType.FfoProxyFailure
			},
			{
				SmtpResponse.EncodedProxyFailureResponseUserLookupFailure.ToString(),
				MailErrorType.FfoProxyFailure
			},
			{
				SmtpResponse.MessageNotProxiedResponse.ToString(),
				MailErrorType.FfoProxyFailure
			},
			{
				SmtpResponse.EncodedProxyFailureResponseSocketError.ToString(),
				MailErrorType.ProxySocketFailure
			},
			{
				SmtpResponse.EncodedProxyFailureResponseBackEndLocatorFailure.ToString(),
				MailErrorType.ServiceLocatorFailure
			},
			{
				SmtpResponse.EncodedProxyFailureResponseDnsError.ToString(),
				MailErrorType.DnsFailure
			},
			{
				SmtpResponse.ShadowRedundancyFailed.ToString(),
				MailErrorType.ShadowFailure
			},
			{
				SmtpResponse.InsufficientResource.ToString(),
				MailErrorType.ServerInBackpressure
			},
			{
				SmtpResponse.CTSParseError.ToString(),
				MailErrorType.ContentConversionFailure
			},
			{
				SmtpResponse.AuthUnsuccessful.ToString(),
				MailErrorType.SmtpAuthFailure
			},
			{
				SmtpResponse.AuthUnrecognized.ToString(),
				MailErrorType.SmtpAuthFailure
			},
			{
				SmtpResponse.TenantAttribution.UnAuthorizedMessageOverIPv6.ToString(),
				MailErrorType.ConnectorConfigurationError
			},
			{
				SmtpResponse.TooManyConnections.ToString(),
				MailErrorType.MaxConcurrentConnectionFailure
			},
			{
				SmtpResponse.TooManyConnectionsPerSource.ToString(),
				MailErrorType.MaxConcurrentConnectionFailure
			},
			{
				SmtpResponse.ServiceUnavailable.ToString(),
				MailErrorType.ServiceNotAvailableFailure
			},
			{
				SmtpResponse.EncodedProxyFailureResponseNoDestinations.ToString(),
				MailErrorType.NoDestinationsFailure
			},
			{
				SmtpResponse.HubRecipientCacheCreationFailureInEOH.ToString(),
				MailErrorType.ActiveDirectoryFailure
			},
			{
				SmtpResponse.HubRecipientCacheCreationTransientFailureInEOH.ToString(),
				MailErrorType.ActiveDirectoryFailure
			},
			{
				SmtpResponse.HubAttributionFailureInCreateTmi.ToString(),
				MailErrorType.ActiveDirectoryFailure
			},
			{
				SmtpResponse.HubAttributionTransientFailureInCreateTmi.ToString(),
				MailErrorType.ActiveDirectoryFailure
			},
			{
				"421 4.3.2 Temporary Server Network Configuration error detected: Expected=.*, actual=.*",
				MailErrorType.NetworkingConfigurationFailure
			}
		};

		// Token: 0x040007DF RID: 2015
		private StatusEntryCollection collection;
	}
}
