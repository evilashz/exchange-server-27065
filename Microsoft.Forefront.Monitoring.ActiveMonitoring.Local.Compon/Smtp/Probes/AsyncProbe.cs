using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x020001FA RID: 506
	public class AsyncProbe : ProbeWorkItem
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x00027AA1 File Offset: 0x00025CA1
		public AsyncProbe()
		{
			AsyncProbe.random = new Random();
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00027AC0 File Offset: 0x00025CC0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext += "AsyncProbe started. ";
			bool flag;
			this.seqNumber = ProbeRunSequence.GetProbeRunSequenceNumber(base.Id.ToString(), out flag);
			ProbeResult result2 = base.Result;
			result2.ExecutionContext += string.Format("Sequence # = {0}. first = {1}. ", this.seqNumber, flag);
			base.Result.StateAttribute2 = RecordType.SendMail.ToString();
			base.Result.StateAttribute5 = this.seqNumber.ToString();
			this.GetFailureRate();
			try
			{
				if (!flag)
				{
					if (!this.LastSendMailFailed(cancellationToken))
					{
						ProbeResult result3 = base.Result;
						result3.ExecutionContext += "Last SendMail successful. ";
						bool flag2 = this.VerifyPreviousResults(cancellationToken);
						this.UpdateCheckMailResult(flag2);
						base.Broker.PublishResult(this.checkMailResult);
						ProbeResult result4 = base.Result;
						result4.ExecutionContext += string.Format("CheckMail {0}. ", flag2 ? "succeeded" : "failed");
					}
					else
					{
						ProbeResult result5 = base.Result;
						result5.ExecutionContext += "Last SendMail failed. No CheckMail result will be published. ";
					}
				}
			}
			catch (Exception ex)
			{
				ProbeResult result6 = base.Result;
				result6.ExecutionContext += string.Format("CheckMail failed. [Exception: {0}] ", ex.Message);
			}
			if (this.IsSendMailSuccess())
			{
				try
				{
					this.SendMail(cancellationToken);
					goto IL_1BB;
				}
				catch (Exception ex2)
				{
					ProbeResult result7 = base.Result;
					result7.ExecutionContext += string.Format("AsyncProbe finished with SendMail failure. [Exception: {0}] ", ex2.Message);
					throw;
				}
				goto IL_195;
				IL_1BB:
				ProbeHelper.ModifyResultName(base.Result);
				ProbeResult result8 = base.Result;
				result8.ExecutionContext += "AsyncProbe finished with SendMail success.";
				return;
			}
			IL_195:
			ProbeResult result9 = base.Result;
			result9.ExecutionContext += "AsyncProbe finished with simulated SendMail failure";
			throw new Exception("SendMail failed (simulation).");
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00027CCC File Offset: 0x00025ECC
		private static string GenerateLamNotificationId(int probeId, long sequence)
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

		// Token: 0x06000F71 RID: 3953 RVA: 0x00027D2C File Offset: 0x00025F2C
		private void SendMail(CancellationToken cancellationToken)
		{
			string text = AsyncProbe.GenerateLamNotificationId(base.Id, this.seqNumber);
			SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition = new SmtpProbeWorkDefinition.SendMailDefinition();
			sendMailDefinition.Enabled = true;
			sendMailDefinition.SmtpServer = "127.0.0.1";
			sendMailDefinition.ResolveEndPoint = false;
			sendMailDefinition.Port = 25;
			sendMailDefinition.EnableSsl = true;
			sendMailDefinition.SenderUsername = "rui@live.com";
			sendMailDefinition.RecipientUsername = "admin@support.msol-test.com";
			sendMailDefinition.SenderTenantID = "2652fdda-b016-45fb-bb6e-ce0aacfa6d0e";
			sendMailDefinition.RecipientTenantID = "2652fdda-b016-45fb-bb6e-ce0aacfa6d0e";
			sendMailDefinition.Direction = Directionality.Incoming;
			sendMailDefinition.Message = new Message();
			sendMailDefinition.Message.MessageId = "12345678";
			sendMailDefinition.Message.Subject = "test mail";
			sendMailDefinition.Message.Body = "this is a test";
			sendMailDefinition.Message.Mail = new MailMessage();
			SendMailHelper.SendMail(base.Definition.Name, sendMailDefinition, text, null);
			string[] array = new string[]
			{
				"Antimalware",
				"Antispam",
				"TransportRule",
				"Delivery"
			};
			string[] array2 = new string[]
			{
				"RECEIVED",
				"DELIVERED",
				"FAILED",
				"EXPANDED"
			};
			foreach (string arg in array)
			{
				string value = string.Format("Agent={0};Action={1}", arg, "testAction");
				EventNotificationItem eventNotificationItem = new EventNotificationItem("Transport", "MessageTracking", base.Definition.Name, ResultSeverityLevel.Verbose);
				eventNotificationItem.AddCustomProperty("StateAttribute1", "some messageID");
				eventNotificationItem.AddCustomProperty("StateAttribute2", "AGENTINFO");
				eventNotificationItem.AddCustomProperty("StateAttribute3", value);
				eventNotificationItem.StateAttribute4 = text;
				eventNotificationItem.Publish(false);
			}
			foreach (string value2 in array2)
			{
				EventNotificationItem eventNotificationItem2 = new EventNotificationItem("Transport", "MessageTracking", base.Definition.Name, ResultSeverityLevel.Verbose);
				eventNotificationItem2.AddCustomProperty("StateAttribute1", "some messageID");
				eventNotificationItem2.AddCustomProperty("StateAttribute2", "MTRT");
				eventNotificationItem2.AddCustomProperty("StateAttribute3", value2);
				eventNotificationItem2.StateAttribute4 = text;
				eventNotificationItem2.Publish(false);
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00027FE8 File Offset: 0x000261E8
		private List<ProbeResult> GetPreviousResults(CancellationToken cancellationToken)
		{
			string previousResultId = AsyncProbe.GenerateLamNotificationId(base.Id, this.seqNumber - 1L);
			string previousRunResultName = string.Format("Transport/MessageTracking/{0}", base.Definition.Name);
			DateTime startTime = DateTime.UtcNow.ToLocalTime().AddMinutes(-60.0);
			List<ProbeResult> results = new List<ProbeResult>();
			LocalDataAccess localDataAccess = new LocalDataAccess();
			IOrderedEnumerable<ProbeResult> query = from r in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime(previousRunResultName, startTime))
			where r.DeploymentId == Settings.DeploymentId && r.ResultName.StartsWith(previousRunResultName) && r.StateAttribute4.StartsWith(previousResultId) && r.ExecutionEndTime >= startTime
			orderby r.ExecutionStartTime
			select r;
			IDataAccessQuery<ProbeResult> dataAccessQuery = localDataAccess.AsDataAccessQuery<ProbeResult>(query);
			dataAccessQuery.ExecuteAsync(delegate(ProbeResult r)
			{
				results.Add(r);
			}, cancellationToken, AsyncProbe.traceContext);
			return results;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00028198 File Offset: 0x00026398
		private bool LastSendMailFailed(CancellationToken cancellationToken)
		{
			long previousSeqNumber = this.seqNumber - 1L;
			DateTime startTime = DateTime.UtcNow.ToLocalTime().AddMinutes(-60.0);
			List<ProbeResult> results = new List<ProbeResult>();
			LocalDataAccess localDataAccess = new LocalDataAccess();
			IOrderedEnumerable<ProbeResult> query = from r in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime(base.Result.ResultName, startTime))
			where r.DeploymentId == Settings.DeploymentId && r.ResultName.StartsWith(this.Result.ResultName) && r.ExecutionEndTime >= startTime && r.WorkItemId == this.Id && r.StateAttribute1 == previousSeqNumber.ToString()
			orderby r.ExecutionStartTime
			select r;
			IDataAccessQuery<ProbeResult> dataAccessQuery = localDataAccess.AsDataAccessQuery<ProbeResult>(query);
			dataAccessQuery.ExecuteAsync(delegate(ProbeResult r)
			{
				if (r.StateAttribute2 == RecordType.SendMail.ToString() && r.ResultType != ResultType.Succeeded)
				{
					results.Add(r);
				}
			}, cancellationToken, AsyncProbe.traceContext);
			return results.Count != 0;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x000282E0 File Offset: 0x000264E0
		private bool VerifyPreviousResults(CancellationToken cancellationToken)
		{
			List<ProbeResult> previousResults = this.GetPreviousResults(cancellationToken);
			ProbeResult probeResult = this.checkMailResult;
			probeResult.ExecutionContext += string.Format("# of previous results: {0}. ", previousResults.Count);
			bool flag = true;
			List<string[]> propertiesInExtensionXml = this.GetPropertiesInExtensionXml(previousResults);
			List<string[]> resultsToMach = this.GetResultsToMach();
			using (List<string[]>.Enumerator enumerator = resultsToMach.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string[] resultToMatch = enumerator.Current;
					string[] array;
					if (string.Compare(resultToMatch[3], "Regex", true) == 0)
					{
						array = propertiesInExtensionXml.Find((string[] item) => string.Compare(item[0], resultToMatch[0], true) == 0 && Regex.IsMatch(item[1], resultToMatch[1], RegexOptions.IgnoreCase));
					}
					else
					{
						array = propertiesInExtensionXml.Find((string[] item) => string.Compare(item[0], resultToMatch[0], true) == 0 && string.Compare(item[1], resultToMatch[1], true) == 0);
					}
					bool flag2 = array != null;
					ProbeResult probeResult2 = this.checkMailResult;
					probeResult2.ExecutionContext += string.Format("[Type:{0}, Value:{1}, MatchType:{2}, Verified:{3}] ", new object[]
					{
						resultToMatch[0],
						resultToMatch[1],
						resultToMatch[3],
						flag2
					});
					if (!flag2 && string.Compare("true", resultToMatch[2], true) == 0)
					{
						flag = false;
					}
				}
			}
			return flag && this.IsCheckMailSuccess();
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00028474 File Offset: 0x00026674
		private List<string[]> GetResultsToMach()
		{
			string[] array = new string[]
			{
				"Antimalware",
				"Antispam",
				"TransportRule",
				"Delivery"
			};
			string[] array2 = new string[]
			{
				"RECEIVED",
				"DELIVERED",
				"FAILED",
				"EXPANDED"
			};
			List<string[]> list = new List<string[]>();
			foreach (string arg in array)
			{
				string[] item = new string[]
				{
					"AGENTINFO",
					string.Format("Agent={0};Action={1}", arg, "testAction"),
					"true",
					"Regex"
				};
				list.Add(item);
			}
			foreach (string text in array2)
			{
				string[] item2 = new string[]
				{
					"MTRT",
					text,
					"true",
					"String"
				};
				list.Add(item2);
			}
			return list;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00028598 File Offset: 0x00026798
		private List<string[]> GetPropertiesInExtensionXml(List<ProbeResult> results)
		{
			List<string[]> list = new List<string[]>();
			foreach (ProbeResult probeResult in results)
			{
				if (!string.IsNullOrWhiteSpace(probeResult.ExtensionXml))
				{
					XmlNode propertiesNode = this.GetPropertiesNode(probeResult.ExtensionXml);
					if (propertiesNode != null)
					{
						string[] item = new string[]
						{
							this.GetProperty(propertiesNode, "StateAttribute2", string.Empty),
							this.GetProperty(propertiesNode, "StateAttribute3", string.Empty)
						};
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00028640 File Offset: 0x00026840
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

		// Token: 0x06000F78 RID: 3960 RVA: 0x000286AC File Offset: 0x000268AC
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

		// Token: 0x06000F79 RID: 3961 RVA: 0x000286D4 File Offset: 0x000268D4
		private void UpdateCheckMailResult(bool success)
		{
			this.checkMailResult.StateAttribute2 = RecordType.DeliverMail.ToString();
			this.checkMailResult.StateAttribute5 = this.seqNumber.ToString();
			this.checkMailResult.DeploymentId = base.Result.DeploymentId;
			this.checkMailResult.ResultName = base.Result.ResultName;
			this.checkMailResult.MachineName = base.Result.MachineName;
			this.checkMailResult.ServiceName = base.Result.ServiceName;
			this.checkMailResult.WorkItemId = base.Result.WorkItemId;
			this.checkMailResult.ExecutionStartTime = base.Result.ExecutionStartTime;
			this.checkMailResult.ExecutionEndTime = base.Result.ExecutionEndTime;
			this.checkMailResult.Version = base.Result.Version;
			if (!success)
			{
				this.checkMailResult.FailureContext = "CheckMail failed.";
			}
			this.checkMailResult.SetCompleted(success ? ResultType.Succeeded : ResultType.Failed);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000287E2 File Offset: 0x000269E2
		private bool IsSendMailSuccess()
		{
			return this.sendFailureRate == 0 || (this.sendFailureRate != 100 && AsyncProbe.random.Next(0, 101) >= this.sendFailureRate);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00028812 File Offset: 0x00026A12
		private bool IsCheckMailSuccess()
		{
			return this.checkFailureRate == 0 || (this.checkFailureRate != 100 && AsyncProbe.random.Next(0, 101) >= this.checkFailureRate);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00028844 File Offset: 0x00026A44
		private void GetFailureRate()
		{
			if (base.Definition != null && base.Definition.Attributes != null)
			{
				string s;
				int num;
				if (base.Definition.Attributes.TryGetValue("SendFailureRate", out s) && int.TryParse(s, out num) && num >= 0 && num <= 100)
				{
					this.sendFailureRate = num;
				}
				else
				{
					this.sendFailureRate = 30;
				}
				if (base.Definition.Attributes.TryGetValue("CheckFailureRate", out s) && int.TryParse(s, out num) && num >= 0 && num <= 100)
				{
					this.checkFailureRate = num;
				}
				else
				{
					this.checkFailureRate = 20;
				}
			}
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("SendMail target failure rate = {0}. CheckMail target failure rate = {1}. ", this.sendFailureRate, this.checkFailureRate);
		}

		// Token: 0x0400075B RID: 1883
		private static Random random;

		// Token: 0x0400075C RID: 1884
		private static TracingContext traceContext = new TracingContext();

		// Token: 0x0400075D RID: 1885
		private int sendFailureRate;

		// Token: 0x0400075E RID: 1886
		private int checkFailureRate;

		// Token: 0x0400075F RID: 1887
		private long seqNumber;

		// Token: 0x04000760 RID: 1888
		private ProbeResult checkMailResult = new ProbeResult();
	}
}
