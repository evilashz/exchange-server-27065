using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000210 RID: 528
	public class SmtpProbe : ProbeWorkItem
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x0002BCCE File Offset: 0x00029ECE
		public SmtpProbe() : this(null, new BasicSmtpClientFactory())
		{
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0002BCDC File Offset: 0x00029EDC
		public SmtpProbe(IPop3Client popClient, IMinimalSmtpClientFactory smtpClientFactory)
		{
			this.popClient = popClient;
			this.smtpClientFactory = smtpClientFactory;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0002BD08 File Offset: 0x00029F08
		internal string MailInfo
		{
			get
			{
				SmtpProbeWorkDefinition.SendMailDefinition sendMail = this.WorkDefinition.SendMail;
				Message message = sendMail.Message;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[SLA:{0}] ", TimeSpan.FromMinutes(sendMail.Sla).ToString());
				stringBuilder.AppendFormat("[From:{0}] ", sendMail.SenderUsername);
				stringBuilder.AppendFormat("[To:{0}] ", sendMail.RecipientUsername);
				stringBuilder.AppendFormat("[{0}]", message.ToString());
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0002BD8F File Offset: 0x00029F8F
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x0002BD97 File Offset: 0x00029F97
		private protected SmtpProbeWorkDefinition WorkDefinition { protected get; private set; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x0002BDA8 File Offset: 0x00029FA8
		protected IPop3Client PopClient
		{
			get
			{
				return this.popClient;
			}
			set
			{
				this.popClient = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x0002BDB1 File Offset: 0x00029FB1
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x0002BDB9 File Offset: 0x00029FB9
		private protected CancellationToken CancellationToken { protected get; private set; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0002BDC2 File Offset: 0x00029FC2
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x0002BDCA File Offset: 0x00029FCA
		protected List<ProbeStatus> AllPreviousSendResults { get; set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0002BDD3 File Offset: 0x00029FD3
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x0002BDDB File Offset: 0x00029FDB
		protected List<ProbeStatus> PreviousSuccessSendResults { get; set; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x0002BDE4 File Offset: 0x00029FE4
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x0002BDEC File Offset: 0x00029FEC
		protected List<ProbeStatus> PreviousDeliverResults { get; set; }

		// Token: 0x0600101F RID: 4127 RVA: 0x0002BDF5 File Offset: 0x00029FF5
		public static string GetProbeErrorType(ProbeResult result)
		{
			return result.StateAttribute1;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0002BE00 File Offset: 0x0002A000
		public static void SetProbeErrorType(ProbeResult result, MailErrorType value)
		{
			result.StateAttribute1 = value.ToString();
			SmtpFailureCategory failureCategory;
			SmtpProbe.failureComponentMapping.TryGetValue(value, out failureCategory);
			result.FailureCategory = (int)failureCategory;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0002BE33 File Offset: 0x0002A033
		public static bool GetAuthOnly(ProbeResult result)
		{
			return result.StateAttribute6 == 14.0;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0002BE46 File Offset: 0x0002A046
		public static void SetAuthOnly(ProbeResult result, bool value)
		{
			result.StateAttribute6 = (double)(value ? 14 : 15);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0002BE58 File Offset: 0x0002A058
		public static string GetSendMailExecutionId(ProbeResult result)
		{
			if (SmtpProbe.GetProbeRecordType(result) == RecordType.SendMail.ToString())
			{
				return result.ExecutionId.ToString();
			}
			if (SmtpProbe.GetProbeRecordType(result) == RecordType.DeliverMail.ToString())
			{
				return result.StateAttribute11;
			}
			return "N/A";
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		public static void SetSendMailExecutionId(ProbeResult result, string value)
		{
			result.StateAttribute11 = value;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0002BEB9 File Offset: 0x0002A0B9
		public static bool GetDeliveryExpected(ProbeResult result)
		{
			return result.StateAttribute16 == 1.0;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0002BECC File Offset: 0x0002A0CC
		public static void SetDeliveryExpected(ProbeResult result, bool value)
		{
			result.StateAttribute16 = (double)(value ? 1 : 0);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0002BEDC File Offset: 0x0002A0DC
		public static double GetSentTime(ProbeResult result)
		{
			return result.StateAttribute17;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0002BEE4 File Offset: 0x0002A0E4
		public static void SetSentTime(ProbeResult result, double value)
		{
			result.StateAttribute17 = value;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0002BEED File Offset: 0x0002A0ED
		public static double GetExpectedDeliverTime(ProbeResult result)
		{
			return result.StateAttribute18;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0002BEF5 File Offset: 0x0002A0F5
		public static void SetExpectedDeliverTime(ProbeResult result, double value)
		{
			result.StateAttribute18 = value;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0002BEFE File Offset: 0x0002A0FE
		public static double GetActualDeliverTime(ProbeResult result)
		{
			return result.StateAttribute19;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0002BF06 File Offset: 0x0002A106
		public static void SetActualDeliverTime(ProbeResult result, double value)
		{
			result.StateAttribute19 = value;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0002BF0F File Offset: 0x0002A10F
		public static AddressFamily GetIpVersion(ProbeResult result)
		{
			if (result.StateAttribute7 == 4.0)
			{
				return AddressFamily.InterNetwork;
			}
			if (result.StateAttribute7 == 6.0)
			{
				return AddressFamily.InterNetworkV6;
			}
			return AddressFamily.Unspecified;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0002BF39 File Offset: 0x0002A139
		public static void SetIpVersion(ProbeResult result, AddressFamily value)
		{
			if (value == AddressFamily.InterNetwork)
			{
				result.StateAttribute7 = 4.0;
				return;
			}
			if (value == AddressFamily.InterNetworkV6)
			{
				result.StateAttribute7 = 6.0;
				return;
			}
			result.StateAttribute7 = 0.0;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0002BF73 File Offset: 0x0002A173
		public static double GetPort(ProbeResult result)
		{
			return result.StateAttribute8;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0002BF7B File Offset: 0x0002A17B
		public static void SetPort(ProbeResult result, double value)
		{
			result.StateAttribute8 = value;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0002BF84 File Offset: 0x0002A184
		public static string GetInternalProbeId(ProbeResult result)
		{
			return result.StateAttribute22;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0002BF8C File Offset: 0x0002A18C
		public static void SetInternalProbeId(ProbeResult result, string value)
		{
			result.StateAttribute22 = value;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0002BF95 File Offset: 0x0002A195
		public static string GetProbeMailInfo(ProbeResult result)
		{
			return result.StateAttribute24;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0002BF9D File Offset: 0x0002A19D
		public static void SetProbeMailInfo(ProbeResult result, string value)
		{
			result.StateAttribute24 = value;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0002BFA6 File Offset: 0x0002A1A6
		public static string GetProbeRecordType(ProbeResult result)
		{
			return result.StateAttribute12;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0002BFAE File Offset: 0x0002A1AE
		public static void SetProbeRecordType(ProbeResult result, RecordType value)
		{
			result.StateAttribute12 = value.ToString();
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.TraceDebug("Started", new object[0]);
			this.SetDefaultAttributeValues();
			this.CancellationToken = cancellationToken;
			try
			{
				this.GetExtendedWorkDefinition();
			}
			catch (Exception e)
			{
				this.TraceError(e, "GetWorkDefinition error", new object[0]);
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.ConfigurationError);
				throw;
			}
			try
			{
				this.LoadAdditionalData();
			}
			catch (Exception e2)
			{
				this.TraceError(e2, "LoadAdditionalData error", new object[0]);
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.ConfigurationError);
				throw;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				this.FindProbeResults();
				this.CheckCancellation();
			}
			catch (OperationCanceledException e3)
			{
				this.TraceError(e3, "SmtpProbe finished with cancellation", new object[0]);
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.FindProbeResultsTimeOut);
				throw;
			}
			finally
			{
				this.TraceDebug("FPR Time:{0} seconds", new object[]
				{
					stopwatch.Elapsed.TotalSeconds
				});
			}
			Pop3Exception ex = null;
			try
			{
				stopwatch = Stopwatch.StartNew();
				this.CheckMail();
				this.CheckCancellation();
			}
			catch (Pop3Exception ex2)
			{
				this.TraceError(ex2, "Pop3Exception", new object[0]);
				ex = ex2;
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.CheckMailException);
			}
			catch (OperationCanceledException e4)
			{
				this.TraceError(e4, "SmtpProbe finished with cancellation", new object[0]);
				this.HandleCheckMailTimeOutException(stopwatch.Elapsed);
				throw;
			}
			catch (Exception e5)
			{
				this.TraceError(e5, "CheckMail error", new object[0]);
			}
			finally
			{
				this.TraceDebug("CM Time:{0} seconds", new object[]
				{
					stopwatch.Elapsed.TotalSeconds
				});
			}
			try
			{
				stopwatch = Stopwatch.StartNew();
				this.UpdateUndeliveredMessages(ex);
				this.CheckCancellation();
			}
			catch (OperationCanceledException e6)
			{
				this.TraceError(e6, "SmtpProbe finished with cancellation", new object[0]);
				this.HandleUumTimeOutException(stopwatch.Elapsed);
				throw;
			}
			catch (Exception e7)
			{
				this.TraceError(e7, "UUM error", new object[0]);
			}
			finally
			{
				this.TraceDebug("UUM Time:{0} seconds", new object[]
				{
					stopwatch.Elapsed.TotalSeconds
				});
			}
			try
			{
				stopwatch = Stopwatch.StartNew();
				this.SendMail();
				if (!this.MailErrorAlreadySet())
				{
					base.Result.ResultType = ResultType.Succeeded;
				}
			}
			catch (OperationCanceledException e8)
			{
				this.TraceError(e8, "SmtpProbe finished with cancellation", new object[0]);
				this.HandleSmtpSendTimeOutException(stopwatch.Elapsed);
				throw;
			}
			catch (Exception ex3)
			{
				if (this.WorkDefinition.SendMail.IgnoreSendMailFailure)
				{
					this.TraceDebug("SendMail exception ignored: {0}", new object[]
					{
						ex3.ToString()
					});
					SmtpProbe.SetDeliveryExpected(base.Result, false);
					return;
				}
				this.LogSendMailException(ex3);
				this.TraceError("SmtpProbe finished with failure.", new object[0]);
				if (!this.MailErrorAlreadySet())
				{
					SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SendMailException);
				}
				throw;
			}
			finally
			{
				this.TraceDebug("SM Time:{0} seconds", new object[]
				{
					stopwatch.Elapsed.TotalSeconds
				});
				this.HandleResultPublished(base.Result);
			}
			this.TraceDebug("SmtpProbe finished successfully", new object[0]);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0002C384 File Offset: 0x0002A584
		protected virtual void HandleResultPublished(ProbeResult result)
		{
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0002C386 File Offset: 0x0002A586
		protected virtual void HandleCheckMailTimeOutException(TimeSpan checkMailTime)
		{
			if (checkMailTime.TotalSeconds > (double)base.Definition.TimeoutSeconds / 2.0)
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.CheckMailTimeOut);
				return;
			}
			SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.ProbeTimeOut);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0002C3C1 File Offset: 0x0002A5C1
		protected virtual void HandleUumTimeOutException(TimeSpan uumTime)
		{
			if (uumTime.TotalSeconds > (double)base.Definition.TimeoutSeconds / 2.0)
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.UpdateUndeliveredTimeOut);
				return;
			}
			SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.ProbeTimeOut);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002C3FC File Offset: 0x0002A5FC
		protected virtual void HandleSmtpSendTimeOutException(TimeSpan sendMailTime)
		{
			if (sendMailTime.TotalSeconds > (double)base.Definition.TimeoutSeconds / 2.0)
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SendMailTimeOut);
				return;
			}
			SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.ProbeTimeOut);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002C438 File Offset: 0x0002A638
		protected void FindProbeResults()
		{
			if (!this.WorkDefinition.CheckMail.Enabled)
			{
				return;
			}
			this.CheckCancellation();
			try
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPTracer, new TracingContext(), "FindProbeResults started.", null, "FindProbeResults", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 836);
				this.FindProbeResultsInternal();
			}
			catch (AggregateException ex)
			{
				this.TraceError(ex.Flatten(), "FindProbeResults error", new object[0]);
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SqlQueryFailure);
			}
			catch (Exception e)
			{
				this.TraceError(e, "FindProbeResults error", new object[0]);
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.SqlQueryFailure);
			}
			this.TraceDebug("Found {0} previous send results", new object[]
			{
				this.PreviousSuccessSendResults.Count
			});
			this.TraceDebug("Found {0} previous deliver results", new object[]
			{
				this.PreviousDeliverResults.Count
			});
			this.CheckCancellation();
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0002C5C4 File Offset: 0x0002A7C4
		protected virtual void FindProbeResultsInternal()
		{
			this.AllPreviousSendResults = new List<ProbeStatus>();
			this.PreviousSuccessSendResults = new List<ProbeStatus>();
			this.PreviousDeliverResults = new List<ProbeStatus>();
			int queryTimeWindow = this.WorkDefinition.CheckMail.QueryTimeWindow;
			DateTime executionStartTime = base.Result.ExecutionStartTime;
			DateTime startTime = base.Result.ExecutionStartTime.AddSeconds((double)(-2 * queryTimeWindow)).AddMinutes(-this.WorkDefinition.SendMail.Sla * 2.0);
			IDataAccessQuery<ProbeResult> probeResults = base.Broker.GetProbeResults(base.Definition, startTime);
			Task<int> task = probeResults.ExecuteAsync(delegate(ProbeResult r)
			{
				ProbeStatus probeStatus = new ProbeStatus(r);
				if (probeStatus.RecordType == RecordType.SendMail)
				{
					this.AllPreviousSendResults.Add(probeStatus);
					if (probeStatus.ResultType == ResultType.Succeeded)
					{
						this.PreviousSuccessSendResults.Add(probeStatus);
						return;
					}
				}
				else
				{
					if (probeStatus.RecordType == RecordType.DeliverMail)
					{
						this.PreviousDeliverResults.Add(probeStatus);
						return;
					}
					this.TraceError("Unknown record type={0}, ID={1}", new object[]
					{
						probeStatus.RecordType,
						probeStatus.InternalProbeId
					});
				}
			}, this.CancellationToken, base.TraceContext);
			task.Wait(this.CancellationToken);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0002C68C File Offset: 0x0002A88C
		protected void CheckMail()
		{
			SmtpProbeWorkDefinition.CheckMailDefinition checkMail = this.WorkDefinition.CheckMail;
			if (!checkMail.Enabled)
			{
				return;
			}
			using (IPop3Client pop3Client = this.popClient ?? new Pop3Client(this.CancellationToken))
			{
				this.TraceDebug("Connecting to POP. Server={0}, Port={1}, User={2}, EnableSsl={3}, ReadTimeout={4}s", new object[]
				{
					checkMail.PopServer,
					checkMail.Port,
					checkMail.Username,
					checkMail.EnableSsl,
					checkMail.ReadTimeout
				});
				try
				{
					pop3Client.Connect(checkMail.PopServer, checkMail.Port, checkMail.Username, checkMail.Password, checkMail.EnableSsl, checkMail.ReadTimeout);
				}
				catch (Pop3Exception e)
				{
					this.HandleLoginException(e);
					throw;
				}
				catch (OperationCanceledException)
				{
					SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.MailboxLoginFailure);
					throw;
				}
				this.TraceDebug("Connected", new object[0]);
				List<Pop3Message> list = pop3Client.List();
				list.Reverse(0, list.Count);
				this.TraceDebug("MsgCount={0}", new object[]
				{
					list.Count
				});
				TimeSpan inboxQueryWindow = this.GetInboxQueryWindow();
				DateTime dateTime = DateTime.UtcNow - inboxQueryWindow;
				this.TraceDebug("QueryLimit={0}", new object[]
				{
					dateTime
				});
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				List<long> list2 = new List<long>();
				try
				{
					foreach (Pop3Message pop3Message in list)
					{
						this.CheckCancellation();
						num2++;
						pop3Client.RetrieveHeader(pop3Message);
						if (pop3Message.ReceivedDate < dateTime)
						{
							this.TraceDebug("MsgOutsideQueryLimit={0}", new object[]
							{
								pop3Message.ReceivedDate.ToString()
							});
							break;
						}
						string text;
						if (checkMail.DisableCheckMailByMessageId)
						{
							if (this.VerifyMessage(pop3Client, pop3Message))
							{
								this.PublishMailWithNoMessageId(pop3Message);
								list2.Add(pop3Message.Number);
								pop3Client.Delete(pop3Message);
								num++;
							}
						}
						else if (this.MatchMessage(pop3Message, out text))
						{
							this.TraceDebug("Found: ID={0}", new object[]
							{
								text
							});
							bool flag = this.VerifyMessage(pop3Client, pop3Message);
							this.TraceDebug("Verified={0}", new object[]
							{
								flag
							});
							this.LogMessage(pop3Message, text);
							bool flag2 = false;
							bool flag3 = this.UpdateDeliveryStatus(pop3Message, text, flag, inboxQueryWindow, out flag2);
							if (flag3)
							{
								num++;
							}
							if (flag2)
							{
								list2.Add(pop3Message.Number);
								pop3Client.Delete(pop3Message);
								num3++;
							}
						}
					}
				}
				finally
				{
					this.TraceDebug("MsgScanned={0}", new object[]
					{
						num2
					});
					this.TraceDebug("MsgMatched={0}", new object[]
					{
						num
					});
					this.TraceDebug("MsgDeleted={0}", new object[]
					{
						num3
					});
				}
				if (!checkMail.DisableCheckMailByMessageId)
				{
					this.CleanUpMailbox(pop3Client, list, list2);
				}
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0002CA54 File Offset: 0x0002AC54
		protected virtual void HandleLoginException(Pop3Exception e)
		{
			SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.MailboxLoginFailure);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0002CA64 File Offset: 0x0002AC64
		protected void SendMail()
		{
			SmtpProbeWorkDefinition.SendMailDefinition sendMail = this.WorkDefinition.SendMail;
			Message message = sendMail.Message;
			MailMessage mail = message.Mail;
			this.TraceDebug("SmtpServer={0}, Port={1}, From={2}, To={3}, Timeout={4}s", new object[]
			{
				sendMail.SmtpServer,
				sendMail.Port,
				sendMail.SenderUsername,
				sendMail.RecipientUsername,
				sendMail.Timeout
			});
			try
			{
				this.CheckCancellation();
				if (!sendMail.Enabled)
				{
					this.TraceDebug("SendMail is DISABLED", new object[0]);
					SmtpProbe.SetProbeRecordType(base.Result, RecordType.None);
				}
				else
				{
					SmtpProbe.SetInternalProbeId(base.Result, message.MessageId);
					SmtpProbe.SetProbeMailInfo(base.Result, this.MailInfo);
					mail.From = new MailAddress(sendMail.SenderUsername);
					mail.To.Add(sendMail.RecipientUsername);
					foreach (NameValuePair nameValuePair in message.Headers)
					{
						mail.Headers.Add(nameValuePair.Name, nameValuePair.Value);
					}
					mail.Subject = message.Subject;
					mail.Body = message.Body;
					foreach (KeyValuePair<string, object> entry in message.Attachments)
					{
						mail.Attachments.Add(Message.CreateMailAttachment(entry));
					}
					if (sendMail.IgnoreCertificateNameMismatchPolicyError)
					{
						ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.ValidateCertificate);
					}
					this.TraceDebug("Calling Send: ID={0}", new object[]
					{
						message.MessageId
					});
					DateTime utcNow = DateTime.UtcNow;
					this.SendMail(mail);
					DateTime utcNow2 = DateTime.UtcNow;
					base.Result.SampleValue = (utcNow2 - utcNow).TotalMilliseconds;
					this.TraceDebug("Send done", new object[0]);
					DateTime dateTime = utcNow2.AddMinutes(this.WorkDefinition.SendMail.Sla);
					SmtpProbe.SetSentTime(base.Result, (double)utcNow2.Ticks);
					SmtpProbe.SetDeliveryExpected(base.Result, message.ExpectDelivery);
					if (!sendMail.AuthOnly)
					{
						SmtpProbe.SetExpectedDeliverTime(base.Result, (double)dateTime.Ticks);
					}
				}
			}
			finally
			{
				message.CleanupAttachment();
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0002CD20 File Offset: 0x0002AF20
		protected virtual void SendMail(MailMessage mail)
		{
			IMinimalSmtpClient minimalSmtpClient = null;
			try
			{
				string host = this.WorkDefinition.SendMail.ResolveEndPoint ? ResolverHelper.ResolveEndPoint(this.WorkDefinition.SendMail.SmtpServer, this.WorkDefinition.SendMail.RecordResolveType, new DelTraceDebug(this.TraceDebug), this.WorkDefinition.SendMail.SimpleResolution).ToString() : this.WorkDefinition.SendMail.SmtpServer;
				minimalSmtpClient = this.smtpClientFactory.CreateSmtpClient(host, this.WorkDefinition, new DelTraceDebug(this.TraceDebug));
				minimalSmtpClient.CancellationToken = this.CancellationToken;
				minimalSmtpClient.Send(mail);
			}
			catch (ResolverHelper.UnableToResolveException e)
			{
				this.HandleDnsFailure(e);
				throw;
			}
			finally
			{
				if (minimalSmtpClient != null)
				{
					this.UpdateProbeExecutionData(minimalSmtpClient);
					try
					{
						minimalSmtpClient.Dispose();
					}
					catch (Exception e2)
					{
						this.TraceError(e2, "SmtpClient Dispose exception ignored", new object[0]);
					}
				}
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0002CE2C File Offset: 0x0002B02C
		protected void GetExtendedWorkDefinition()
		{
			if (this.WorkDefinition == null)
			{
				this.WorkDefinition = new SmtpProbeWorkDefinition(base.Id, base.Definition, new DelTraceDebug(this.TraceDebug));
				this.TraceDebug("Work definition processed", new object[0]);
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002CE6A File Offset: 0x0002B06A
		protected virtual void UpdateProbeExecutionData(IMinimalSmtpClient smtpClient)
		{
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0002CE6C File Offset: 0x0002B06C
		protected virtual void CopySendDataToDeliver(ProbeStatus sendStatus, ProbeResult deliverResult)
		{
			SmtpProbe.SetInternalProbeId(deliverResult, sendStatus.InternalProbeId);
			SmtpProbe.SetProbeMailInfo(deliverResult, sendStatus.ProbeMailInfo);
			SmtpProbe.SetDeliveryExpected(deliverResult, sendStatus.DeliveryExpected);
			SmtpProbe.SetSentTime(deliverResult, (double)sendStatus.SentTime.Ticks);
			SmtpProbe.SetSendMailExecutionId(deliverResult, sendStatus.SendMailExecutionId);
			SmtpProbe.SetExpectedDeliverTime(deliverResult, (double)(sendStatus.SentTime + TimeSpan.FromMinutes(this.WorkDefinition.SendMail.Sla)).Ticks);
			SmtpProbe.SetIpVersion(deliverResult, this.WorkDefinition.SendMail.IpVersion);
			SmtpProbe.SetAuthOnly(deliverResult, this.WorkDefinition.SendMail.AuthOnly);
			SmtpProbe.SetPort(deliverResult, (double)this.WorkDefinition.SendMail.Port);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002CF30 File Offset: 0x0002B130
		protected virtual void HandleSlaMiss(ProbeResult deliverResult)
		{
			this.SetDeliverResultError(deliverResult, "Mail delivery exceeded the SLA ({0}s)", new object[]
			{
				TimeSpan.FromMinutes(this.WorkDefinition.SendMail.Sla).TotalSeconds
			});
			SmtpProbe.SetProbeErrorType(deliverResult, MailErrorType.SlaExceeded);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002CF80 File Offset: 0x0002B180
		protected virtual void HandleDeliveryFailure(ProbeResult deliverResult, ProbeStatus previousSendStatus, Exception ex)
		{
			if (ex != null)
			{
				this.SetDeliverResultError(deliverResult, ex);
			}
			if (SmtpProbe.GetProbeErrorType(base.Result) == MailErrorType.MailboxLoginFailure.ToString() || (previousSendStatus != null && previousSendStatus.ProbeErrorType == MailErrorType.MailboxLoginFailure))
			{
				SmtpProbe.SetProbeErrorType(deliverResult, MailErrorType.MailboxLoginFailure);
			}
			else if (SmtpProbe.GetProbeErrorType(base.Result) == MailErrorType.PopProxyFailure.ToString() || (previousSendStatus != null && previousSendStatus.ProbeErrorType == MailErrorType.PopProxyFailure))
			{
				SmtpProbe.SetProbeErrorType(deliverResult, MailErrorType.PopProxyFailure);
			}
			else if (SmtpProbe.GetProbeErrorType(base.Result) == MailErrorType.CheckMailException.ToString() || (previousSendStatus != null && previousSendStatus.ProbeErrorType == MailErrorType.CheckMailException))
			{
				SmtpProbe.SetProbeErrorType(deliverResult, MailErrorType.CheckMailException);
			}
			else if (previousSendStatus != null && previousSendStatus.ProbeErrorType == MailErrorType.FindProbeResultsTimeOut)
			{
				SmtpProbe.SetProbeErrorType(deliverResult, MailErrorType.FindProbeResultsTimeOut);
			}
			else if (previousSendStatus != null && previousSendStatus.ProbeErrorType == MailErrorType.SqlQueryFailure)
			{
				SmtpProbe.SetProbeErrorType(deliverResult, MailErrorType.SqlQueryFailure);
			}
			else if (ex != null)
			{
				SmtpProbe.SetProbeErrorType(deliverResult, MailErrorType.CheckMailException);
			}
			else
			{
				this.SetDeliverResultError(deliverResult, "Mail was not delivered after {0}s and is considered a delivery failure", new object[]
				{
					this.WorkDefinition.CheckMail.QueryTimeWindow
				});
				SmtpProbe.SetProbeErrorType(deliverResult, MailErrorType.NoDelivery);
			}
			deliverResult.SetCompleted(ResultType.Failed);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0002D0B0 File Offset: 0x0002B2B0
		protected virtual void HandleDnsFailure(ResolverHelper.UnableToResolveException e)
		{
			this.TraceError(e, "{0} Resolution error", new object[]
			{
				e.QueryType
			});
			SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.AzureDnsFailure);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		protected virtual void HandleVerificationFailure(ProbeResult deliverResult)
		{
			this.SetDeliverResultError(deliverResult, "Message verification failed.", new object[0]);
			SmtpProbe.SetProbeErrorType(deliverResult, MailErrorType.VerificationFailure);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0002D108 File Offset: 0x0002B308
		protected virtual void SetDefaultAttributeValues()
		{
			SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.Success);
			SmtpProbe.SetProbeRecordType(base.Result, RecordType.SendMail);
			SmtpProbe.SetInternalProbeId(base.Result, "None. Probe exited before creating subject");
			SmtpProbe.SetProbeMailInfo(base.Result, "None. Probe exited before creating mail info");
			SmtpProbe.SetPort(base.Result, 0.0);
			SmtpProbe.SetSendMailExecutionId(base.Result, "None");
			SmtpProbe.SetDeliveryExpected(base.Result, false);
			SmtpProbe.SetSentTime(base.Result, 0.0);
			SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.Success);
			base.Result.ResultType = ResultType.Failed;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002D1AC File Offset: 0x0002B3AC
		protected virtual void LoadAdditionalData()
		{
			SmtpProbe.SetAuthOnly(base.Result, this.WorkDefinition.SendMail.AuthOnly);
			SmtpProbe.SetIpVersion(base.Result, this.WorkDefinition.SendMail.IpVersion);
			SmtpProbe.SetPort(base.Result, (double)this.WorkDefinition.SendMail.Port);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002D20C File Offset: 0x0002B40C
		protected void TraceDebug(string format, params object[] args)
		{
			string text = string.Format(format, args);
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("[{0}] ", text);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPTracer, new TracingContext(), text, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 1336);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0002D264 File Offset: 0x0002B464
		protected void TraceDebug(StringBuilder log, string format, params object[] args)
		{
			string text = string.Format(format, args);
			if (log != null)
			{
				log.AppendFormat("[{0}] ", text);
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPTracer, new TracingContext(), text, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 1354);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		protected void TraceError(string format, params object[] args)
		{
			string text = string.Format(format, args);
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("[{0}] ", text);
			WTFDiagnostics.TraceError(ExTraceGlobals.SMTPTracer, new TracingContext(), text, null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 1366);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0002D304 File Offset: 0x0002B504
		protected void TraceError(Exception e, string format, params object[] args)
		{
			string arg = string.Format(format, args);
			if (e.InnerException == null)
			{
				ProbeResult result = base.Result;
				result.ExecutionContext += string.Format("[{0}. {1}] ", arg, e.Message);
			}
			else
			{
				ProbeResult result2 = base.Result;
				result2.ExecutionContext += string.Format("[{0}. {1} Inner: {2}] ", arg, e.Message, e.InnerException.Message);
			}
			WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.SMTPTracer, new TracingContext(), "{0}. {1}", arg, e.ToString(), null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 1388);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0002D3A8 File Offset: 0x0002B5A8
		private bool MailErrorAlreadySet()
		{
			string probeErrorType = SmtpProbe.GetProbeErrorType(base.Result);
			return !string.IsNullOrWhiteSpace(probeErrorType) && !string.Equals(probeErrorType, MailErrorType.Success.ToString());
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0002D3E4 File Offset: 0x0002B5E4
		private bool MatchMessage(Pop3Message message, out string messageId)
		{
			messageId = string.Empty;
			Match match = null;
			string subjectOverride = this.WorkDefinition.SendMail.Message.SubjectOverride;
			if (!this.WorkDefinition.SendMail.Message.UseSubjectVerbatim)
			{
				string pattern = "^" + subjectOverride + "-[0-9]{20}(\\w\\W)*";
				match = Regex.Match(message.Subject, pattern);
			}
			else
			{
				string value = string.Format("{0}:", Message.ProbeMessageIDHeaderTag);
				string pattern2 = "(\\w\\W)*" + subjectOverride + "-[0-9]{20}(\\w\\W)*";
				foreach (string text in message.HeaderComponents)
				{
					if (text.Trim().StartsWith(value))
					{
						match = Regex.Match(text, pattern2);
						if (match.Success)
						{
							break;
						}
					}
				}
			}
			if (match != null && match.Success)
			{
				messageId = match.ToString();
				return true;
			}
			return false;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0002D4E4 File Offset: 0x0002B6E4
		private void CleanUpMailbox(IPop3Client pop3Client, List<Pop3Message> messages, IEnumerable<long> messagesDeleted)
		{
			SmtpProbeWorkDefinition.CheckMailDefinition checkMail = this.WorkDefinition.CheckMail;
			int num = base.Definition.TimeoutSeconds - 120;
			if (checkMail.DeleteMessageMinutes == 0 || num <= 0)
			{
				this.TraceDebug("Cleanup skipped", new object[0]);
				return;
			}
			messages.Reverse(0, messages.Count);
			DateTime t = DateTime.UtcNow.AddMinutes((double)(-1 * checkMail.DeleteMessageMinutes));
			string pattern = "^[0-9]{10}-[0-9]{20}(\\w\\W)*";
			int num2 = 0;
			int num3 = 0;
			DateTime t2 = DateTime.UtcNow.AddSeconds((double)num);
			this.TraceDebug("Starting cleanup ({0}s)", new object[]
			{
				num
			});
			foreach (Pop3Message pop3Message in messages)
			{
				if (DateTime.UtcNow >= t2)
				{
					break;
				}
				if (!messagesDeleted.Contains(pop3Message.Number))
				{
					pop3Client.RetrieveHeader(pop3Message);
					if (!(pop3Message.ReceivedDate < t))
					{
						break;
					}
					if (Regex.Match(pop3Message.Subject, pattern).Success)
					{
						try
						{
							pop3Client.Delete(pop3Message);
							num2++;
						}
						catch (Exception e)
						{
							this.TraceError(e, "Mailbox cleanup error", new object[0]);
						}
					}
					num3++;
					this.CheckCancellation();
				}
			}
			this.TraceDebug("Checked={0}, Deleted={1}", new object[]
			{
				num3,
				num2
			});
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0002D680 File Offset: 0x0002B880
		private bool VerifyMessage(IPop3Client pop3Client, Pop3Message message)
		{
			if (this.WorkDefinition.CheckMail.ExpectedMessage == null)
			{
				return true;
			}
			this.mailVerificationLog.TryAdd(message.Number, new StringBuilder());
			return ((this.WorkDefinition.CheckMail.ExpectedMessage.Body == null && this.WorkDefinition.CheckMail.ExpectedMessage.Attachment == null) || this.RetrieveMessageBody(pop3Client, message)) && (this.VerifySubject(message) && this.VerifyHeader(message) && this.VerifyMessageBody(message)) && this.VerifyAttachment(message);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0002D7A4 File Offset: 0x0002B9A4
		private bool RetrieveMessageBody(IPop3Client pop3Client, Pop3Message message)
		{
			if (message.Components == null)
			{
				pop3Client.Retrieve(message);
			}
			int num = message.HeaderComponents.Count;
			int count = message.Components.Count;
			if (count > num)
			{
				message.BodyComponents = (from i in Enumerable.Range(num, count - num)
				select message.Components[i]).ToList<string>();
			}
			if (message.BodyComponents == null)
			{
				this.TraceError("Failed to retrieve message body components.", new object[0]);
				return false;
			}
			num = message.HeaderComponents.FindIndex((string s) => s.Contains("boundary="));
			StringBuilder log = this.GetMailVerificationLog(message.Number);
			if (num < 0)
			{
				this.TraceDebug(log, "Not a multipart message.", new object[0]);
				message.BodyText = string.Join(Environment.NewLine, message.BodyComponents.ToList<string>()).Trim();
				message.AttachmentCount = 0;
			}
			else
			{
				string boundary = message.HeaderComponents.ToList<string>()[num].Trim().Substring("boundary=".Length).Trim(new char[]
				{
					'"'
				});
				this.TraceDebug(log, "Multipart message", new object[]
				{
					boundary
				});
				IEnumerable<int> source = from i in Enumerable.Range(0, message.BodyComponents.Count)
				where message.BodyComponents[i].Contains(boundary)
				select i;
				int num2 = source.Count<int>() - 1;
				if (num2 <= 1)
				{
					this.TraceError("NumberOfBodyParts must > 1", new object[0]);
					return false;
				}
				this.TraceDebug(log, "NumberOfBodyParts={0}", new object[]
				{
					num2
				});
				int num3 = source.First<int>();
				int num4 = source.ToList<int>()[1] - 1;
				source = from i in Enumerable.Range(num3, num4 - num3)
				where string.IsNullOrWhiteSpace(message.BodyComponents[i])
				select i;
				num3 = ((source.Count<int>() == 0) ? num4 : (source.ToList<int>().First<int>() + 1));
				if (num3 < num4)
				{
					IEnumerable<string> source2 = from i in Enumerable.Range(num3, num4 - num3)
					select message.BodyComponents[i];
					message.BodyText = string.Join(Environment.NewLine, source2.ToList<string>()).TrimEnd(new char[]
					{
						'='
					}).TrimStart(new char[0]);
				}
				else
				{
					message.BodyText = string.Empty;
				}
				IEnumerable<string> source3 = message.BodyComponents.FindAll((string s) => s.StartsWith("Content-Disposition: attachment"));
				message.AttachmentCount = source3.Count<string>();
			}
			return true;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0002DAF8 File Offset: 0x0002BCF8
		private bool VerifySubject(Pop3Message message)
		{
			Notification subject = this.WorkDefinition.CheckMail.ExpectedMessage.Subject;
			if (subject == null)
			{
				return true;
			}
			string text = string.Copy(message.Subject).Trim();
			if (!this.WorkDefinition.SendMail.Message.UseSubjectVerbatim)
			{
				int num = text.IndexOf(' ');
				text = ((num > 0) ? text.Substring(num + 1) : string.Empty);
			}
			StringBuilder log = this.GetMailVerificationLog(message.Number);
			this.TraceDebug(log, "Subject='{0}'", new object[]
			{
				text
			});
			bool flag = this.Verify(subject, "Subject", text);
			bool flag2 = true;
			if (subject.Mandatory && ((flag && !subject.MatchExpected) || (!flag && subject.MatchExpected)))
			{
				flag2 = false;
			}
			this.TraceDebug(log, "Type:{0}, Value:'{1}', MatchType:{2}, MatchExpected:{3}, Match:{4}, Verified:{5} ", new object[]
			{
				subject.Type,
				subject.Value,
				subject.Method.ToString(),
				subject.MatchExpected,
				flag,
				flag2
			});
			return flag2;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0002DCE4 File Offset: 0x0002BEE4
		private bool VerifyHeader(Pop3Message message)
		{
			IEnumerable<Notification> headers = this.WorkDefinition.CheckMail.ExpectedMessage.Headers;
			if (headers == null || headers.Count<Notification>() == 0)
			{
				return true;
			}
			IEnumerable<string> headerComponents = message.HeaderComponents;
			using (IEnumerator<Notification> enumerator = headers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SmtpProbe.<>c__DisplayClass14 CS$<>8__locals1 = new SmtpProbe.<>c__DisplayClass14();
					CS$<>8__locals1.headerToMatch = enumerator.Current;
					string pattern = (!string.IsNullOrEmpty(CS$<>8__locals1.headerToMatch.Value)) ? string.Format("{0}: ", CS$<>8__locals1.headerToMatch.Type) : string.Format("{0}:", CS$<>8__locals1.headerToMatch.Type);
					int index = pattern.Length;
					bool flag = true;
					StringBuilder log = this.GetMailVerificationLog(message.Number);
					foreach (string text in from h in headerComponents
					where h.StartsWith(pattern)
					select h)
					{
						this.TraceDebug(log, "Header='{0}'", new object[]
						{
							text
						});
					}
					if (CS$<>8__locals1.headerToMatch.Method == MatchType.String)
					{
						flag = headerComponents.Any((string header) => header.StartsWith(pattern) && string.Compare(header.Substring(index), CS$<>8__locals1.headerToMatch.Value, true) == 0);
					}
					else if (CS$<>8__locals1.headerToMatch.Method == MatchType.SubString)
					{
						flag = headerComponents.Any((string header) => header.StartsWith(pattern) && header.Substring(index).Contains(CS$<>8__locals1.headerToMatch.Value));
					}
					else if (CS$<>8__locals1.headerToMatch.Method == MatchType.Regex)
					{
						flag = headerComponents.Any((string header) => header.StartsWith(pattern) && Regex.IsMatch(header.Substring(index), CS$<>8__locals1.headerToMatch.Value, RegexOptions.IgnoreCase));
					}
					bool flag2 = true;
					if (CS$<>8__locals1.headerToMatch.Mandatory && ((flag && !CS$<>8__locals1.headerToMatch.MatchExpected) || (!flag && CS$<>8__locals1.headerToMatch.MatchExpected)))
					{
						flag2 = false;
					}
					this.TraceDebug(log, "Tag:{0}, Value:'{1}', MatchType:{2}, MatchExpected:{3}, Match:{4}, Verified:{5}", new object[]
					{
						CS$<>8__locals1.headerToMatch.Type,
						CS$<>8__locals1.headerToMatch.Value,
						CS$<>8__locals1.headerToMatch.Method.ToString(),
						CS$<>8__locals1.headerToMatch.MatchExpected,
						flag,
						flag2
					});
					if (!flag2)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0002DFA8 File Offset: 0x0002C1A8
		private bool VerifyMessageBody(Pop3Message message)
		{
			Notification body = this.WorkDefinition.CheckMail.ExpectedMessage.Body;
			if (body == null)
			{
				return true;
			}
			StringBuilder log = this.GetMailVerificationLog(message.Number);
			this.TraceDebug(log, "BodyText='{0}'", new object[]
			{
				message.BodyText
			});
			bool flag = this.Verify(body, "Body", message.BodyText);
			bool flag2 = true;
			if (body.Mandatory && ((flag && !body.MatchExpected) || (!flag && body.MatchExpected)))
			{
				flag2 = false;
			}
			this.TraceDebug(log, "Type:{0}, Value:'{1}', MatchType:{2}, MatchExpected:{3}, Match:{4}, Verified:{5}", new object[]
			{
				body.Type,
				body.Value,
				body.Method.ToString(),
				body.MatchExpected,
				flag,
				flag2
			});
			return flag2;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0002E094 File Offset: 0x0002C294
		private bool VerifyAttachment(Pop3Message message)
		{
			Notification attachment = this.WorkDefinition.CheckMail.ExpectedMessage.Attachment;
			if (attachment == null)
			{
				return true;
			}
			StringBuilder log = this.GetMailVerificationLog(message.Number);
			this.TraceDebug(log, "Attachment#={0}", new object[]
			{
				message.AttachmentCount
			});
			bool flag = message.AttachmentCount == (int)Convert.ChangeType(attachment.Value, typeof(int));
			bool flag2 = true;
			if (attachment.Mandatory && ((flag && !attachment.MatchExpected) || (!flag && attachment.MatchExpected)))
			{
				flag2 = false;
			}
			this.TraceDebug(log, "Type:{0}, Value:'{1}', MatchExpected:{2}, Match:{3}, Verified:{4}", new object[]
			{
				attachment.Type,
				attachment.Value,
				attachment.MatchExpected,
				flag,
				flag2
			});
			return flag2;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0002E184 File Offset: 0x0002C384
		private bool Verify(Notification matchItem, string typeToVerify, string valueToVerify)
		{
			if (string.Compare(typeToVerify, matchItem.Type, true) != 0)
			{
				return false;
			}
			if (string.IsNullOrEmpty(valueToVerify) && string.IsNullOrEmpty(matchItem.Value))
			{
				return true;
			}
			if (matchItem.Method == MatchType.SubString)
			{
				return valueToVerify.Contains(matchItem.Value);
			}
			if (matchItem.Method == MatchType.Regex)
			{
				return Regex.IsMatch(valueToVerify, matchItem.Value, RegexOptions.IgnoreCase);
			}
			return string.Compare(valueToVerify.Trim(), matchItem.Value, true) == 0;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		private void LogMessage(Pop3Message message, string messageId)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (message.Components != null)
			{
				stringBuilder.AppendLine(string.Join(Environment.NewLine, message.Components.ToArray()));
			}
			else if (message.HeaderComponents != null)
			{
				stringBuilder.AppendLine(string.Join(Environment.NewLine, message.HeaderComponents.ToArray()));
			}
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.SMTPTracer, new TracingContext(), "Content of Message '{0}': {1}", messageId, stringBuilder.ToString(), null, "LogMessage", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 1890);
			if (!string.IsNullOrWhiteSpace(this.WorkDefinition.CheckMail.LogFileLocation))
			{
				string path = string.Format("{0}\\{1}.txt", this.WorkDefinition.CheckMail.LogFileLocation, messageId);
				using (StreamWriter streamWriter = new StreamWriter(path))
				{
					streamWriter.WriteLine(stringBuilder.ToString());
					streamWriter.Close();
				}
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0002E31C File Offset: 0x0002C51C
		private bool UpdateDeliveryStatus(Pop3Message message, string messageId, bool verificationSuccess, TimeSpan queryWindow, out bool shouldDelete)
		{
			this.CheckCancellation();
			shouldDelete = false;
			bool flag = false;
			IEnumerable<ProbeStatus> source = from s in this.PreviousSuccessSendResults
			where s.InternalProbeId == messageId
			select s;
			IEnumerable<ProbeStatus> source2 = from s in this.PreviousDeliverResults
			where s.InternalProbeId == messageId
			select s;
			int num = source.Count<ProbeStatus>();
			int num2 = source2.Count<ProbeStatus>();
			if (num == 1 && num2 == 0)
			{
				ProbeStatus probeStatus = source.First<ProbeStatus>();
				ProbeResult probeResult = new ProbeResult(base.Definition);
				SmtpProbe.SetProbeErrorType(probeResult, MailErrorType.Success);
				probeResult.ExecutionStartTime = DateTime.UtcNow;
				probeResult.ExecutionEndTime = DateTime.UtcNow;
				SmtpProbe.SetProbeRecordType(probeResult, RecordType.DeliverMail);
				this.CopySendDataToDeliver(probeStatus, probeResult);
				DateTime t = probeStatus.SentTime + TimeSpan.FromMinutes(this.WorkDefinition.SendMail.Sla);
				DateTime receivedDate = message.ReceivedDate;
				SmtpProbe.SetActualDeliverTime(probeResult, (double)receivedDate.Ticks);
				probeResult.SampleValue = (receivedDate - probeStatus.SentTime).TotalMilliseconds;
				StringBuilder stringBuilder = this.GetMailVerificationLog(message.Number);
				if (stringBuilder != null)
				{
					probeResult.ExecutionContext = stringBuilder.ToString();
				}
				ResultType resultType;
				if (probeStatus.DeliveryExpected)
				{
					if (!verificationSuccess)
					{
						resultType = ResultType.Failed;
						this.HandleVerificationFailure(probeResult);
					}
					else if (DateTime.Compare(receivedDate, t) <= 0)
					{
						resultType = ResultType.Succeeded;
					}
					else
					{
						resultType = ResultType.Failed;
						this.HandleSlaMiss(probeResult);
					}
				}
				else
				{
					resultType = ResultType.Failed;
					this.SetDeliverResultError(probeResult, "Unexpected mail delivery, it was not supposed to be delivered.", new object[0]);
					SmtpProbe.SetProbeErrorType(probeResult, MailErrorType.UnexpectedDelivery);
				}
				probeResult.SetCompleted(resultType);
				if (this.mailDeliveryStatus.TryAdd(SmtpProbe.GetInternalProbeId(probeResult), resultType == ResultType.Succeeded))
				{
					ProbeResult probeResult2 = probeResult;
					probeResult2.ExecutionContext += "[COPIED SEND RESULT EXECUTION CONTEXT START]";
					ProbeResult probeResult3 = probeResult;
					probeResult3.ExecutionContext += probeStatus.ExecutionContext;
					base.Broker.PublishResult(probeResult);
					this.HandleResultPublished(probeResult);
					this.TraceDebug("DeliverMailStatus={0}", new object[]
					{
						(resultType == ResultType.Succeeded) ? "Succeeded" : "Failed"
					});
					flag = true;
				}
			}
			else
			{
				if (num2 >= 1 && num >= 1)
				{
					ProbeStatus probeStatus2 = source.First<ProbeStatus>();
					shouldDelete = true;
					this.TraceDebug("OK to delete", new object[]
					{
						probeStatus2.InternalProbeId
					});
				}
				if (num > 1)
				{
					this.TraceError("Unexpected # of SendMail results={0}, ID={1}", new object[]
					{
						num,
						messageId
					});
				}
				if (num2 > 1)
				{
					this.TraceError("Unexpected # of DeliverMail results={0}, ID={1}", new object[]
					{
						num2,
						messageId
					});
				}
			}
			if (!this.WorkDefinition.CheckMail.VerifyDeliverResultBeforeDelete)
			{
				shouldDelete = flag;
			}
			this.CheckCancellation();
			return flag;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0002E620 File Offset: 0x0002C820
		private void UpdateUndeliveredMessages(Exception ex)
		{
			if (!this.WorkDefinition.CheckMail.Enabled)
			{
				return;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPTracer, new TracingContext(), "UpdateUndeliveredMessages started.", null, "UpdateUndeliveredMessages", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 2043);
			int queryTimeWindow = this.WorkDefinition.CheckMail.QueryTimeWindow;
			DateTime dateTime = base.Result.ExecutionStartTime.AddSeconds((double)(-1 * queryTimeWindow));
			ProbeStatus previousSendStatus = null;
			if (this.AllPreviousSendResults.Any<ProbeStatus>())
			{
				previousSendStatus = (from s in this.AllPreviousSendResults
				orderby s.SentTime
				select s).Last<ProbeStatus>();
			}
			using (List<ProbeStatus>.Enumerator enumerator = this.PreviousSuccessSendResults.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ProbeStatus sendStatus = enumerator.Current;
					this.CheckCancellation();
					if ((sendStatus.SentTime + TimeSpan.FromMinutes(this.WorkDefinition.SendMail.Sla)).Ticks < dateTime.Ticks)
					{
						IEnumerable<ProbeStatus> enumerable = from d in this.PreviousDeliverResults
						where sendStatus.InternalProbeId == d.InternalProbeId
						select d;
						if (enumerable == null || enumerable.Count<ProbeStatus>() == 0)
						{
							string internalProbeId = sendStatus.InternalProbeId;
							ProbeResult probeResult = new ProbeResult(base.Definition);
							SmtpProbe.SetProbeErrorType(probeResult, MailErrorType.Success);
							probeResult.ExecutionStartTime = DateTime.UtcNow;
							probeResult.ExecutionEndTime = DateTime.UtcNow;
							SmtpProbe.SetProbeRecordType(probeResult, RecordType.DeliverMail);
							this.CopySendDataToDeliver(sendStatus, probeResult);
							if (sendStatus.DeliveryExpected)
							{
								this.HandleDeliveryFailure(probeResult, previousSendStatus, ex);
							}
							else
							{
								this.TraceDebug("Mail was not delivered after {0}s, which was expected, and is considered a success", new object[]
								{
									queryTimeWindow
								});
								probeResult.SetCompleted(ResultType.Succeeded);
							}
							probeResult.SampleValue = TimeSpan.FromSeconds((double)(queryTimeWindow + 1)).TotalMilliseconds;
							if (this.mailDeliveryStatus.TryAdd(internalProbeId, probeResult.ResultType == ResultType.Succeeded))
							{
								ProbeResult probeResult2 = probeResult;
								probeResult2.ExecutionContext += "[COPIED SEND RESULT EXECUTION CONTEXT START]";
								ProbeResult probeResult3 = probeResult;
								probeResult3.ExecutionContext += sendStatus.ExecutionContext;
								base.Broker.PublishResult(probeResult);
								this.HandleResultPublished(probeResult);
								this.TraceError("DeliverMail ID={0}, Status=Failed", new object[]
								{
									internalProbeId
								});
							}
						}
						else if (enumerable.Count<ProbeStatus>() > 1)
						{
							this.TraceError("Unexpected # of DeliverMail results={0}, ID={1}", new object[]
							{
								this.PreviousDeliverResults.Count,
								sendStatus.InternalProbeId
							});
						}
					}
				}
			}
			this.CheckCancellation();
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPTracer, new TracingContext(), "UpdateUndeliveredMessages finished.", null, "UpdateUndeliveredMessages", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 2115);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0002E93C File Offset: 0x0002CB3C
		private void SetDeliverResultError(ProbeResult deliverResult, string format, params object[] args)
		{
			string text = string.Format(format, args);
			string text2 = string.Format("[{0}] [Latency: {1}s] [Sent Time: {2}] [Expected Delivery Time: {3}] [ActualDeliveryTime : {4}] [DeilveryExpected: {3}]", new object[]
			{
				text,
				TimeSpan.FromMilliseconds(deliverResult.SampleValue).TotalSeconds,
				new DateTime((long)SmtpProbe.GetSentTime(deliverResult), DateTimeKind.Utc),
				new DateTime((long)SmtpProbe.GetExpectedDeliverTime(deliverResult), DateTimeKind.Utc),
				new DateTime((long)SmtpProbe.GetActualDeliverTime(deliverResult), DateTimeKind.Utc),
				SmtpProbe.GetDeliveryExpected(deliverResult) ? "true" : "false"
			});
			deliverResult.FailureContext = text2;
			deliverResult.Error = string.Format("{0} {1}", text2, SmtpProbe.GetProbeMailInfo(deliverResult));
			WTFDiagnostics.TraceError(ExTraceGlobals.SMTPTracer, new TracingContext(), deliverResult.Error, null, "SetDeliverResultError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 2140);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0002EA1C File Offset: 0x0002CC1C
		private void SetDeliverResultError(ProbeResult deliverResult, Exception e)
		{
			string text = e.Message + " " + ((e.InnerException == null) ? string.Empty : e.InnerException.Message);
			deliverResult.FailureContext = text;
			deliverResult.ExecutionContext += string.Format("[{0}] ", text);
			deliverResult.Error = string.Format("{0} {1}", text, SmtpProbe.GetProbeMailInfo(deliverResult));
			deliverResult.Exception = e.ToString();
			WTFDiagnostics.TraceError(ExTraceGlobals.SMTPTracer, new TracingContext(), e.ToString(), null, "SetDeliverResultError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 2157);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0002EAC0 File Offset: 0x0002CCC0
		private void LogSendMailException(Exception e)
		{
			string text = string.Format("SendMail failed. {0}", e.Message);
			if (e is SmtpException)
			{
				text = text + " SmtpException StatusCode: " + ((SmtpException)e).StatusCode;
			}
			base.Result.FailureContext = text;
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("[{0}] ", text);
			WTFDiagnostics.TraceError<string>(ExTraceGlobals.SMTPTracer, new TracingContext(), "SendMail failed. {0}", e.ToString(), null, "LogSendMailException", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbe.cs", 2175);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0002EB5C File Offset: 0x0002CD5C
		private void CheckCancellation()
		{
			if (this.CancellationToken.IsCancellationRequested)
			{
				SmtpProbe.SetProbeErrorType(base.Result, MailErrorType.ProbeTimeOut);
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0002EB8C File Offset: 0x0002CD8C
		private void PublishMailWithNoMessageId(Pop3Message message)
		{
			string text = string.Format("{0}-{1}", base.Id, CombGuidGenerator.NewGuid().ToString());
			this.TraceDebug("Found a matching message and assigned ID: {0}", new object[]
			{
				text
			});
			this.LogMessage(message, text);
			ProbeResult probeResult = new ProbeResult(base.Definition);
			probeResult.ExecutionStartTime = DateTime.UtcNow;
			probeResult.ExecutionEndTime = DateTime.UtcNow;
			SmtpProbe.SetInternalProbeId(probeResult, text);
			SmtpProbe.SetProbeRecordType(probeResult, RecordType.DeliverMail);
			SmtpProbe.SetActualDeliverTime(probeResult, (double)message.ReceivedDate.Ticks);
			probeResult.StateAttribute4 = message.Subject.TrimEnd(Environment.NewLine.ToCharArray());
			probeResult.StateAttribute5 = message.BodyText;
			StringBuilder stringBuilder = this.GetMailVerificationLog(message.Number);
			if (stringBuilder != null)
			{
				probeResult.ExecutionContext = stringBuilder.ToString();
			}
			probeResult.SetCompleted(ResultType.Succeeded);
			base.Broker.PublishResult(probeResult);
			this.TraceDebug("DeliverMail result inserted. ID:{0}", new object[]
			{
				text
			});
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0002EC9C File Offset: 0x0002CE9C
		private StringBuilder GetMailVerificationLog(long number)
		{
			StringBuilder result = null;
			this.mailVerificationLog.TryGetValue(number, out result);
			return result;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0002ECBC File Offset: 0x0002CEBC
		private TimeSpan GetInboxQueryWindow()
		{
			return TimeSpan.FromSeconds((double)this.WorkDefinition.CheckMail.QueryTimeWindow) + TimeSpan.FromSeconds((double)base.Definition.RecurrenceIntervalSeconds) + TimeSpan.FromMinutes(5.0);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0002ED08 File Offset: 0x0002CF08
		private bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			if (this.WorkDefinition.SendMail.IgnoreCertificateNameMismatchPolicyError)
			{
				policyErrors &= ~SslPolicyErrors.RemoteCertificateNameMismatch;
			}
			return policyErrors == SslPolicyErrors.None;
		}

		// Token: 0x040007B7 RID: 1975
		internal static readonly string ProbeErrorTypeAttribute = "StateAttribute1";

		// Token: 0x040007B8 RID: 1976
		internal static readonly string AuthOnlyAttribute = "StateAttribute6";

		// Token: 0x040007B9 RID: 1977
		internal static readonly string IpVersionAttribute = "StateAttribute7";

		// Token: 0x040007BA RID: 1978
		internal static readonly string PortAttribute = "StateAttribute8";

		// Token: 0x040007BB RID: 1979
		internal static readonly string SendMailExecutionIdAttribute = "StateAttribute11";

		// Token: 0x040007BC RID: 1980
		internal static readonly string ProbeRecordTypeAttribute = "StateAttribute12";

		// Token: 0x040007BD RID: 1981
		internal static readonly string DeliveryExceptedAttribute = "StateAttribute16";

		// Token: 0x040007BE RID: 1982
		internal static readonly string SentTimeAttribute = "StateAttribute17";

		// Token: 0x040007BF RID: 1983
		internal static readonly string ExpectedDeliverTimeAttribute = "StateAttribute18";

		// Token: 0x040007C0 RID: 1984
		internal static readonly string ActualDeliverTimeAttribute = "StateAttribute19";

		// Token: 0x040007C1 RID: 1985
		internal static readonly string InternalProbeIdAttribute = "StateAttribute22";

		// Token: 0x040007C2 RID: 1986
		internal static readonly string ProbeMailInfoAttribute = "StateAttribute24";

		// Token: 0x040007C3 RID: 1987
		private static Dictionary<MailErrorType, SmtpFailureCategory> failureComponentMapping = new Dictionary<MailErrorType, SmtpFailureCategory>
		{
			{
				MailErrorType.CheckMailException,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.FfoAntispamFailure,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.MailboxLoginFailure,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.AzureDnsFailure,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.SqlQueryFailure,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.PopProxyFailure,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.FindProbeResultsTimeOut,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.ConnectorConfigurationError,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.CheckMailTimeOut,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.UpdateUndeliveredTimeOut,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.SlaExceeded,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.NoDelivery,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.SaveStatusTimeout,
				SmtpFailureCategory.MonitoringComponent
			},
			{
				MailErrorType.FfoGlsFailure,
				SmtpFailureCategory.DependentComponent
			},
			{
				MailErrorType.ServiceLocatorFailure,
				SmtpFailureCategory.DependentComponent
			},
			{
				MailErrorType.NoDestinationsFailure,
				SmtpFailureCategory.DependentComponent
			},
			{
				MailErrorType.SmtpAuthFailure,
				SmtpFailureCategory.DependentComponent
			},
			{
				MailErrorType.SmtpSendAuthTimeOut,
				SmtpFailureCategory.DependentComponent
			},
			{
				MailErrorType.NetworkingConfigurationFailure,
				SmtpFailureCategory.DependentComponent
			},
			{
				MailErrorType.ActiveDirectoryFailure,
				SmtpFailureCategory.DependentComponent
			},
			{
				MailErrorType.UnableToConnect,
				SmtpFailureCategory.DependentCoveredComponent
			},
			{
				MailErrorType.DnsFailure,
				SmtpFailureCategory.DnsFailure
			}
		};

		// Token: 0x040007C4 RID: 1988
		private ConcurrentDictionary<string, bool> mailDeliveryStatus = new ConcurrentDictionary<string, bool>();

		// Token: 0x040007C5 RID: 1989
		private ConcurrentDictionary<long, StringBuilder> mailVerificationLog = new ConcurrentDictionary<long, StringBuilder>();

		// Token: 0x040007C6 RID: 1990
		private IPop3Client popClient;

		// Token: 0x040007C7 RID: 1991
		private IMinimalSmtpClientFactory smtpClientFactory;

		// Token: 0x02000211 RID: 529
		private enum IpVersion
		{
			// Token: 0x040007D1 RID: 2001
			Unspecified,
			// Token: 0x040007D2 RID: 2002
			Four = 4,
			// Token: 0x040007D3 RID: 2003
			Six = 6
		}
	}
}
