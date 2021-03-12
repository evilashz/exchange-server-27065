using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Local.Components;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000053 RID: 83
	public class TransportSmtpProbe : ProbeWorkItem
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000D3F6 File Offset: 0x0000B5F6
		internal ProbeResult CheckMailResult
		{
			get
			{
				if (this.checkMailResult == null)
				{
					this.checkMailResult = new ProbeResult(base.Definition);
					this.checkMailResult.StateAttribute2 = RecordType.DeliverMail.ToString();
				}
				return this.checkMailResult;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000D42D File Offset: 0x0000B62D
		internal SmtpProbeWorkDefinition WorkDefinition
		{
			get
			{
				return this.workDefinition;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000D435 File Offset: 0x0000B635
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000D43D File Offset: 0x0000B63D
		internal long SeqNumber { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000D446 File Offset: 0x0000B646
		internal List<Notification> ResultsFromPreviousRun
		{
			get
			{
				return this.resultsFromPreviousRun;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000D450 File Offset: 0x0000B650
		internal string MailInfo
		{
			get
			{
				SmtpProbeWorkDefinition.SendMailDefinition sendMail = this.workDefinition.SendMail;
				Message message = sendMail.Message;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[SLA: {0}] ", TimeSpan.FromMinutes(sendMail.Sla).ToString());
				stringBuilder.AppendFormat("[From: {0}] ", sendMail.SenderUsername);
				stringBuilder.AppendFormat("[To: {0}] ", sendMail.RecipientUsername);
				stringBuilder.AppendFormat("[SenderTenantID: {0}] ", sendMail.SenderTenantID);
				stringBuilder.AppendFormat("[RecipientTenantID: {0}] ", sendMail.RecipientTenantID);
				stringBuilder.Append(message.ToString());
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		protected static string GenerateLamNotificationId(int probeId, long sequence)
		{
			return Guid.Parse(string.Format("{0:X8}-{1:X4}-{2:X4}-{3:X4}-{4:X12}", new object[]
			{
				probeId,
				0,
				0,
				0,
				(int)sequence
			})).ToString();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000D558 File Offset: 0x0000B758
		protected static string GenerateClientMessageId(int probeId, long sequence)
		{
			string text = TransportSmtpProbe.GenerateLamNotificationId(probeId, sequence);
			return text.Replace("-", string.Empty) + "@" + Environment.MachineName;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000D58C File Offset: 0x0000B78C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.TraceDebug("TransportSmtpProbe started.", new object[0]);
			if (!this.ShouldRun())
			{
				this.TraceDebug("TransportSmtpProbe skipped because ShouldRun returned false", new object[0]);
				return;
			}
			bool flag;
			this.SeqNumber = ProbeRunSequence.GetProbeRunSequenceNumber(base.Id.ToString(), out flag);
			this.TraceDebug("SeqNumber={0}. First={1}.", new object[]
			{
				this.SeqNumber,
				flag
			});
			base.Result.StateAttribute2 = RecordType.SendMail.ToString();
			base.Result.StateAttribute5 = this.SeqNumber.ToString();
			try
			{
				this.GetExtendedWorkDefinition();
			}
			catch (Exception e)
			{
				this.LogException("GetWorkDefinition failed.", e);
				throw;
			}
			try
			{
				if (!flag)
				{
					if (!this.LastSendMailFailed(cancellationToken))
					{
						this.TraceDebug("Last SendMail successful.", new object[0]);
						this.SaveLastSuccessfulSendMailResult(cancellationToken);
						bool flag2 = this.VerifyPreviousResults(cancellationToken);
						this.UpdateCommonProbeResultAttributes(flag2);
						base.Broker.PublishResult(this.CheckMailResult);
						this.TraceDebug("CheckMail {0}.", new object[]
						{
							flag2 ? "succeeded" : "failed"
						});
					}
					else
					{
						this.TraceDebug("Last SendMail failed. No CheckMail result will be published.", new object[0]);
					}
				}
			}
			catch (OperationCanceledException ex)
			{
				this.LogException("Checkmail failed.", ex);
				base.Result.Error = this.GetDebugInfo(ex.Message);
				this.TraceError("TransportSmtpProbe finished with CheckMail cancellation.", new object[0]);
				throw;
			}
			catch (MessageTracingProbeException ex2)
			{
				this.LogException("Checkmail failed.", ex2);
				base.Result.Error = this.GetDebugInfo(ex2.Message);
				throw;
			}
			catch (Exception e2)
			{
				this.LogException("Checkmail failed.", e2);
				throw;
			}
			try
			{
				this.SendMail(cancellationToken);
			}
			catch (Exception ex3)
			{
				if (this.WorkDefinition.SendMail.IgnoreSendMailFailure)
				{
					this.TraceDebug("SendMail exception ignored: {0}", new object[]
					{
						ex3.ToString()
					});
					return;
				}
				this.LogSendMailException(ex3);
				base.Result.Error = this.GetDebugInfo(ex3.Message);
				this.TraceError("TransportSmtpProbe finished with SendMail failure.", new object[0]);
				throw;
			}
			ProbeHelper.ModifyResultName(base.Result);
			this.TraceDebug("TransportSmtpProbe finished with SendMail success.", new object[0]);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000D818 File Offset: 0x0000BA18
		protected virtual bool ShouldRun()
		{
			if (!TransportProbeCommon.IsProbeExecutionEnabled())
			{
				return false;
			}
			ServerComponentEnum serverComponentEnum = ServerComponentEnum.HubTransport;
			if (base.Definition.ServiceName != serverComponentEnum.ToString())
			{
				return true;
			}
			ServiceState effectiveState = ServerComponentStateManager.GetEffectiveState(serverComponentEnum);
			return effectiveState != ServiceState.Draining;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000D860 File Offset: 0x0000BA60
		protected void SendMail(CancellationToken cancellationToken)
		{
			SmtpProbeWorkDefinition.SendMailDefinition sendMail = this.workDefinition.SendMail;
			Message message = sendMail.Message;
			try
			{
				this.CheckCancellation(cancellationToken);
				base.Result.StateAttribute1 = message.MessageId;
				base.Result.StateAttribute3 = this.MailInfo;
				string text = TransportSmtpProbe.GenerateLamNotificationId(base.Id, this.SeqNumber);
				base.Result.StateAttribute11 = text;
				string text2 = TransportSmtpProbe.GenerateClientMessageId(base.Id, this.SeqNumber);
				message.Headers.Add(new NameValuePair("Message-ID", text2));
				base.Result.StateAttribute12 = text2;
				this.TraceDebug("Calling SendMail: InternetMessageId={0}, From={1}, To={2}, SenderTenantID={3}, RecipientTenantID={4}, MessageId={5}.", new object[]
				{
					text2,
					sendMail.SenderUsername,
					sendMail.RecipientUsername,
					sendMail.SenderTenantID,
					sendMail.RecipientTenantID,
					message.MessageId
				});
				DateTime utcNow = DateTime.UtcNow;
				this.SendMail(sendMail, text);
				DateTime utcNow2 = DateTime.UtcNow;
				this.TraceDebug(string.Format("SendMail finished at {0}.", utcNow2), new object[0]);
				base.Result.SampleValue = (utcNow2 - utcNow).TotalMilliseconds;
				DateTime dateTime = utcNow2.AddMinutes(this.workDefinition.SendMail.Sla);
				base.Result.StateAttribute4 = string.Format("[MailSentTime: {0}] [MailResultsExpectedTime: {1}]", utcNow2.ToString("o"), dateTime.ToString("o"));
				base.Result.StateAttribute6 = (double)utcNow2.Ticks;
				base.Result.StateAttribute7 = (double)dateTime.Ticks;
			}
			finally
			{
				message.CleanupAttachment();
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000DB08 File Offset: 0x0000BD08
		protected virtual bool VerifyPreviousResults(CancellationToken cancellationToken)
		{
			List<ProbeResult> previousResults = this.GetPreviousResults(cancellationToken);
			List<Notification> propertiesInExtensionXml = this.GetPropertiesInExtensionXml(previousResults);
			string format = "NumberOfPrevousResults={0}. {1}";
			object[] array = new object[2];
			array[0] = previousResults.Count;
			object[] array2 = array;
			int num = 1;
			string text;
			if (previousResults.Count != 0)
			{
				text = string.Join(" ", from r in propertiesInExtensionXml
				select string.Format("[Type={0}, Value={1}]", r.Type, r.Value));
			}
			else
			{
				text = string.Empty;
			}
			array2[num] = text;
			this.TraceDebugCheckMail(format, array);
			this.resultsFromPreviousRun = propertiesInExtensionXml;
			List<Notification> expectedNotifications = this.workDefinition.ExpectedNotifications;
			bool flag = true;
			this.TraceDebugCheckMail("Expected results: ", new object[0]);
			using (List<Notification>.Enumerator enumerator = expectedNotifications.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Notification resultToMatch = enumerator.Current;
					bool flag2 = propertiesInExtensionXml.Any((Notification item) => string.Compare(item.Type, resultToMatch.Type, true) == 0);
					bool flag3;
					if (resultToMatch.Method == MatchType.SubString)
					{
						flag3 = propertiesInExtensionXml.Any((Notification item) => string.Compare(item.Type, resultToMatch.Type, true) == 0 && item.Value.Contains(resultToMatch.Value));
					}
					else if (resultToMatch.Method == MatchType.Regex)
					{
						flag3 = propertiesInExtensionXml.Any((Notification item) => string.Compare(item.Type, resultToMatch.Type, true) == 0 && Regex.IsMatch(item.Value, resultToMatch.Value, RegexOptions.IgnoreCase));
					}
					else
					{
						flag3 = propertiesInExtensionXml.Any((Notification item) => string.Compare(item.Type, resultToMatch.Type, true) == 0 && string.Compare(item.Value, resultToMatch.Value, true) == 0);
					}
					this.TraceDebugCheckMail("[Type:{0}, Value:{1}, MatchType:{2}, MatchExpected:{3}, MatchFound:{4}] ", new object[]
					{
						resultToMatch.Type,
						resultToMatch.Value,
						resultToMatch.Method.ToString(),
						resultToMatch.MatchExpected,
						flag3
					});
					if (resultToMatch.MatchExpected)
					{
						flag = (flag3 || (!flag2 && !resultToMatch.Mandatory));
					}
					else if (flag3)
					{
						flag = false;
					}
				}
			}
			this.TraceDebugCheckMail("CheckMail {0}. ", new object[]
			{
				flag ? "successful" : "failed"
			});
			this.CheckCancellation(cancellationToken);
			return flag;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000DD80 File Offset: 0x0000BF80
		protected virtual void UpdateCheckMailResult(bool success, ProbeResult probeResult)
		{
			probeResult.StateAttribute2 = RecordType.DeliverMail.ToString();
			this.UpdateCommonProbeResultAttributes(success);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000DD9C File Offset: 0x0000BF9C
		protected void UpdateCommonProbeResultAttributes(bool success)
		{
			this.CheckMailResult.StateAttribute5 = this.SeqNumber.ToString();
			this.CheckMailResult.MachineName = base.Result.MachineName;
			this.CheckMailResult.ExecutionStartTime = base.Result.ExecutionStartTime;
			this.CheckMailResult.ExecutionEndTime = DateTime.UtcNow;
			this.CheckMailResult.Version = base.Result.Version;
			if (!success)
			{
				string text;
				if (this.ResultsFromPreviousRun.Count<Notification>() == 0)
				{
					text = string.Format("CheckMail results verification failed. No results were found for this message within the SLA ({0}) from the time it was sent.", TimeSpan.FromMinutes(this.WorkDefinition.SendMail.Sla).ToString());
				}
				else
				{
					text = "CheckMail results verification failed.";
				}
				this.CheckMailResult.FailureContext = text;
				this.CheckMailResult.Error = this.GetDebugInfo(text);
			}
			this.CheckMailResult.SetCompleted(success ? ResultType.Succeeded : ResultType.Failed);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000DE8C File Offset: 0x0000C08C
		protected virtual void SendMail(SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition, string lamNotificationID)
		{
			SendMailHelper.SendMail(base.Definition.Name, sendMailDefinition, lamNotificationID, null);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000DEA1 File Offset: 0x0000C0A1
		protected virtual string GetProbeResultComponent()
		{
			return "MessageTracking";
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		protected void GetExtendedWorkDefinition()
		{
			if (this.workDefinition == null)
			{
				this.workDefinition = new SmtpProbeWorkDefinition(base.Id, base.Definition, new DelTraceDebug(this.TraceDebug));
				this.workDefinition.SendMail.Sla = Math.Min(this.workDefinition.SendMail.Sla, (double)((int)TimeSpan.FromSeconds((double)base.Definition.RecurrenceIntervalSeconds).TotalMinutes));
				this.TraceDebug("Work definition processed.", new object[0]);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000DF34 File Offset: 0x0000C134
		protected virtual void TraceDebug(string format, params object[] args)
		{
			string text = string.Format(format, args);
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + text + " ";
			WTFDiagnostics.TraceDebug(ExTraceGlobals.SMTPTracer, new TracingContext(), text, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\TransportSmtpProbe.cs", 522);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000DF88 File Offset: 0x0000C188
		protected virtual void TraceError(string format, params object[] args)
		{
			string text = string.Format(format, args);
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + text + " ";
			WTFDiagnostics.TraceError(ExTraceGlobals.SMTPTracer, new TracingContext(), text, null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\TransportSmtpProbe.cs", 534);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		protected void LogException(string message, Exception e)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("[{0} Exception: {1}] ", message, e.Message);
			WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.SMTPTracer, new TracingContext(), "{0} Exception: {1}", message, e.ToString(), null, "LogException", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\TransportSmtpProbe.cs", 545);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000E03C File Offset: 0x0000C23C
		protected void LogSendMailException(Exception e)
		{
			string text;
			if (e is SmtpException)
			{
				text = string.Format("SmtpException - StatusCode: {0}, {1}", ((SmtpException)e).StatusCode, e.Message);
			}
			else
			{
				text = string.Format("Exception - {0}", e.Message);
			}
			base.Result.FailureContext = text;
			this.TraceError(text, new object[0]);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		protected void TraceDebugCheckMail(string format, params object[] args)
		{
			string text = string.Format(format, args);
			ProbeResult probeResult = this.CheckMailResult;
			probeResult.ExecutionContext = probeResult.ExecutionContext + text + " ";
			WTFDiagnostics.TraceDebug(ExTraceGlobals.SMTPTracer, new TracingContext(), text, null, "TraceDebugCheckMail", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\TransportSmtpProbe.cs", 578);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000E168 File Offset: 0x0000C368
		private List<ProbeResult> GetPreviousResults(CancellationToken cancellationToken)
		{
			string previousResultId = TransportSmtpProbe.GenerateLamNotificationId(base.Id, this.SeqNumber - 1L);
			string previousRunResultName = string.Format("Transport/{1}/{0}", base.Definition.Name, this.GetProbeResultComponent());
			DateTime d = DateTime.UtcNow.ToLocalTime();
			int num = Math.Max(base.Definition.RecurrenceIntervalSeconds * 10, 3600);
			DateTime startTime = d - TimeSpan.FromSeconds((double)num);
			List<ProbeResult> results = new List<ProbeResult>();
			LocalDataAccess localDataAccess = new LocalDataAccess();
			IOrderedEnumerable<ProbeResult> query = from r in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime(previousRunResultName, startTime))
			where r.DeploymentId == Settings.DeploymentId && r.ResultName.StartsWith(previousRunResultName) && r.StateAttribute4.StartsWith(previousResultId) && r.ExecutionEndTime >= startTime
			orderby r.ExecutionStartTime
			select r;
			IDataAccessQuery<ProbeResult> dataAccessQuery = localDataAccess.AsDataAccessQuery<ProbeResult>(query);
			Task<int> task = dataAccessQuery.ExecuteAsync(delegate(ProbeResult r)
			{
				results.Add(r);
			}, cancellationToken, TransportSmtpProbe.traceContext);
			task.Wait(cancellationToken);
			this.CheckCancellation(cancellationToken);
			return results;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000E340 File Offset: 0x0000C540
		private bool LastSendMailFailed(CancellationToken cancellationToken)
		{
			long previousSeqNumber = this.SeqNumber - 1L;
			DateTime d = DateTime.UtcNow.ToLocalTime();
			int num = Math.Max(base.Definition.RecurrenceIntervalSeconds * 10, 3600);
			DateTime startTime = d - TimeSpan.FromSeconds((double)num);
			List<ProbeResult> results = new List<ProbeResult>();
			LocalDataAccess localDataAccess = new LocalDataAccess();
			IOrderedEnumerable<ProbeResult> query = from r in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime(base.Result.ResultName, startTime))
			where r.DeploymentId == Settings.DeploymentId && r.ResultName.StartsWith(this.Result.ResultName) && r.ExecutionEndTime >= startTime && r.WorkItemId == this.Id
			orderby r.ExecutionStartTime
			select r;
			IDataAccessQuery<ProbeResult> dataAccessQuery = localDataAccess.AsDataAccessQuery<ProbeResult>(query);
			Task<int> task = dataAccessQuery.ExecuteAsync(delegate(ProbeResult r)
			{
				if (r.StateAttribute2 == RecordType.SendMail.ToString() && r.StateAttribute5 == previousSeqNumber.ToString())
				{
					results.Add(r);
				}
			}, cancellationToken, TransportSmtpProbe.traceContext);
			task.Wait(cancellationToken);
			this.CheckCancellation(cancellationToken);
			return results.Count != 0;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		private void SaveLastSuccessfulSendMailResult(CancellationToken cancellationToken)
		{
			long previousSeqNumber = this.SeqNumber - 1L;
			DateTime d = DateTime.UtcNow.ToLocalTime();
			int num = Math.Max(base.Definition.RecurrenceIntervalSeconds * 10, 3600);
			DateTime startTime = d - TimeSpan.FromSeconds((double)num);
			List<ProbeResult> results = new List<ProbeResult>();
			string resultName = ProbeHelper.ModifyResultName(base.Result.ResultName);
			LocalDataAccess localDataAccess = new LocalDataAccess();
			IOrderedEnumerable<ProbeResult> query = from r in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime(resultName, startTime))
			where r.DeploymentId == Settings.DeploymentId && r.ResultName.StartsWith(resultName) && r.ExecutionEndTime >= startTime && r.WorkItemId == this.Id
			orderby r.ExecutionStartTime
			select r;
			IDataAccessQuery<ProbeResult> dataAccessQuery = localDataAccess.AsDataAccessQuery<ProbeResult>(query);
			Task<int> task = dataAccessQuery.ExecuteAsync(delegate(ProbeResult r)
			{
				if (r.StateAttribute2 == RecordType.SendMail.ToString() && r.StateAttribute5 == previousSeqNumber.ToString())
				{
					results.Add(r);
				}
			}, cancellationToken, TransportSmtpProbe.traceContext);
			task.Wait(cancellationToken);
			this.CheckCancellation(cancellationToken);
			if (results.Count > 0)
			{
				ProbeResult probeResult = results[0];
				this.CheckMailResult.StateAttribute1 = probeResult.StateAttribute1;
				this.CheckMailResult.StateAttribute3 = probeResult.StateAttribute3;
				this.CheckMailResult.StateAttribute4 = probeResult.StateAttribute4;
				this.CheckMailResult.StateAttribute6 = probeResult.StateAttribute6;
				this.CheckMailResult.StateAttribute7 = probeResult.StateAttribute7;
				this.CheckMailResult.StateAttribute11 = probeResult.StateAttribute11;
				this.CheckMailResult.StateAttribute12 = probeResult.StateAttribute12;
				this.CheckMailResult.StateAttribute13 = probeResult.ExecutionContext;
				this.CheckMailResult.StateAttribute14 = probeResult.StateAttribute5;
				this.CheckMailResult.StateAttribute15 = probeResult.ExtensionXml;
				this.CheckMailResult.StateAttribute21 = probeResult.ExecutionStartTime.ToString();
				this.CheckMailResult.StateAttribute22 = probeResult.ExecutionEndTime.ToString();
				this.TraceDebug("Last SendMail result saved.", new object[0]);
			}
			else
			{
				this.CheckMailResult.StateAttribute12 = TransportSmtpProbe.GenerateClientMessageId(base.Id, previousSeqNumber);
			}
			this.TraceDebug("NumberOfSuccessResultsFound={0}.", new object[]
			{
				results.Count
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000E77C File Offset: 0x0000C97C
		private List<Notification> GetPropertiesInExtensionXml(IEnumerable<ProbeResult> results)
		{
			List<Notification> list = new List<Notification>();
			foreach (ProbeResult probeResult in results)
			{
				if (!string.IsNullOrWhiteSpace(probeResult.ExtensionXml))
				{
					XmlNode propertiesNode = this.GetPropertiesNode(probeResult.ExtensionXml);
					if (propertiesNode != null)
					{
						Notification item = new Notification
						{
							Type = this.GetProperty(propertiesNode, "StateAttribute2", string.Empty),
							Value = this.GetProperty(propertiesNode, "StateAttribute3", string.Empty)
						};
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000E828 File Offset: 0x0000CA28
		private XmlNode GetPropertiesNode(string extensionXml)
		{
			if (string.IsNullOrWhiteSpace(extensionXml))
			{
				return null;
			}
			byte[] bytes = Encoding.Default.GetBytes(extensionXml);
			XmlNode result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
				safeXmlDocument.Load(memoryStream);
				XmlElement documentElement = safeXmlDocument.DocumentElement;
				result = documentElement.SelectSingleNode("/Properties");
			}
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000E894 File Offset: 0x0000CA94
		private string GetProperty(XmlNode properties, string propertyName, string defaultValue)
		{
			if (properties == null)
			{
				return defaultValue;
			}
			XmlNode xmlNode = properties.SelectSingleNode(propertyName);
			if (xmlNode == null)
			{
				return defaultValue;
			}
			return xmlNode.InnerText;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000E8BC File Offset: 0x0000CABC
		private string GetDebugInfo(string info)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(info);
			if (!string.IsNullOrWhiteSpace(this.CheckMailResult.StateAttribute1))
			{
				stringBuilder.AppendFormat("Last MessageId={0}, ", this.CheckMailResult.StateAttribute1);
			}
			if (!string.IsNullOrWhiteSpace(base.Result.StateAttribute1))
			{
				stringBuilder.AppendFormat("Current MessageId={0}, ", base.Result.StateAttribute1);
			}
			if (!string.IsNullOrWhiteSpace(this.CheckMailResult.StateAttribute12))
			{
				stringBuilder.AppendFormat("Last InternetMessageId={0}, ", this.CheckMailResult.StateAttribute12);
			}
			if (!string.IsNullOrWhiteSpace(base.Result.StateAttribute12))
			{
				stringBuilder.AppendFormat("Current InternetMessageId={0}, ", base.Result.StateAttribute12);
			}
			if (!string.IsNullOrWhiteSpace(base.Result.StateAttribute5))
			{
				stringBuilder.AppendFormat("RunSeqNumber={0} ", base.Result.StateAttribute5);
			}
			if (!string.IsNullOrWhiteSpace(this.CheckMailResult.StateAttribute13))
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat("Last SendMail ExecutionContext: {0}", this.CheckMailResult.StateAttribute13);
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000E9E1 File Offset: 0x0000CBE1
		private void CheckCancellation(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}
		}

		// Token: 0x04000155 RID: 341
		private static readonly string ServerName = Environment.MachineName;

		// Token: 0x04000156 RID: 342
		private static readonly TracingContext traceContext = new TracingContext();

		// Token: 0x04000157 RID: 343
		private ProbeResult checkMailResult;

		// Token: 0x04000158 RID: 344
		private List<Notification> resultsFromPreviousRun = new List<Notification>();

		// Token: 0x04000159 RID: 345
		private SmtpProbeWorkDefinition workDefinition;
	}
}
