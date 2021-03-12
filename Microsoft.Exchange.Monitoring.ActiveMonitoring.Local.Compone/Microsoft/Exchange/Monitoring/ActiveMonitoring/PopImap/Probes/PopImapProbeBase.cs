using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.Probes
{
	// Token: 0x0200027F RID: 639
	public abstract class PopImapProbeBase : ProbeWorkItem
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060011F6 RID: 4598
		protected abstract string ProbeComponent { get; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060011F7 RID: 4599
		protected abstract bool ProbeMbxScope { get; }

		// Token: 0x060011F8 RID: 4600 RVA: 0x000796E0 File Offset: 0x000778E0
		protected void DoWork(PopImapProbeStateObject probeTrackingObject, CancellationToken cancellationToken)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = probeTrackingObject.Result;
			result.StateAttribute21 += "BDWS;";
			while (probeTrackingObject.State != ProbeState.Finish)
			{
				DateTime utcNow2 = DateTime.UtcNow;
				ProbeResult result2 = probeTrackingObject.Result;
				object stateAttribute = result2.StateAttribute21;
				result2.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute,
					"CurrentNow:",
					utcNow2.TimeOfDay,
					";"
				});
				StringBuilder stringBuilder = new StringBuilder();
				probeTrackingObject.Connection.SendData(probeTrackingObject.Command);
				do
				{
					IAsyncResult asyncResult = probeTrackingObject.Connection.BeginRead();
					if (!asyncResult.AsyncWaitHandle.WaitOne(probeTrackingObject.TimeoutLimit.Subtract(utcNow2)) || cancellationToken.IsCancellationRequested)
					{
						probeTrackingObject.Connection.Dispose();
						ProbeResult result3 = probeTrackingObject.Result;
						object stateAttribute2 = result3.StateAttribute21;
						result3.StateAttribute21 = string.Concat(new object[]
						{
							stateAttribute2,
							"BDWE:",
							DateTime.UtcNow.TimeOfDay,
							";"
						});
						this.ReportFailure(true, probeTrackingObject);
					}
					stringBuilder.Append(probeTrackingObject.Connection.EndRead(asyncResult));
				}
				while (!probeTrackingObject.Connection.LastLineReceived(stringBuilder.ToString(), probeTrackingObject.ExpectedTag, probeTrackingObject.MultiLine));
				probeTrackingObject.TcpResponses.Add(probeTrackingObject.Connection.CreateResponse(stringBuilder.ToString()));
				this.ParseResponseSetNextState(probeTrackingObject);
				if (probeTrackingObject.State == ProbeState.Failure)
				{
					break;
				}
			}
			if (probeTrackingObject.State == ProbeState.Failure)
			{
				ProbeResult result4 = probeTrackingObject.Result;
				object stateAttribute3 = result4.StateAttribute21;
				result4.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute3,
					"BDWE:",
					DateTime.UtcNow.TimeOfDay,
					";"
				});
				this.ReportFailure(false, probeTrackingObject);
				return;
			}
			ProbeResult result5 = probeTrackingObject.Result;
			object stateAttribute4 = result5.StateAttribute21;
			result5.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute4,
				"BDWE:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
			this.ReportSuccess(probeTrackingObject);
		}

		// Token: 0x060011F9 RID: 4601
		protected abstract void ParseResponseSetNextState(PopImapProbeStateObject probeTrackingObject);

		// Token: 0x060011FA RID: 4602
		protected abstract void HandleSocketError(Exception e);

		// Token: 0x060011FB RID: 4603 RVA: 0x00079950 File Offset: 0x00077B50
		protected void ReportFailure(bool isAborted, PopImapProbeStateObject probeTrackingObject)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = base.Result;
			result.StateAttribute21 += "BRFS;";
			base.Result.SampleValue = (double)((int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds);
			TcpResponse tcpResponse = probeTrackingObject.TcpResponses[probeTrackingObject.LastResponseIndex];
			ProbeResult result2 = probeTrackingObject.Result;
			result2.StateAttribute13 += ":FAIL";
			string executionContext = string.Format("Probe executed for\r\n\r\nUser: {0}\r\nPassword: {1}\r\nTarget: {2}", base.Definition.Account, base.Definition.AccountPassword, base.Definition.Endpoint);
			base.Result.ExecutionContext = executionContext;
			ProbeResult result3 = base.Result;
			result3.StateAttribute12 += tcpResponse.ResponseString;
			Match match = PopImapProbeBase.ProxyParserSuccessResponse.Match(tcpResponse.ResponseString);
			if (match.Success && match.Groups["proxy"].Success)
			{
				probeTrackingObject.Result.StateAttribute4 = Utils.ExtractServerNameFromFDQN(match.Groups["proxy"].Value);
			}
			if (tcpResponse.ResponseType != ResponseType.success)
			{
				string error;
				if (string.IsNullOrEmpty(probeTrackingObject.FailingReason))
				{
					error = probeTrackingObject.TcpResponses[probeTrackingObject.LastResponseIndex].ResponseString;
				}
				else
				{
					error = probeTrackingObject.FailingReason;
				}
				Match match2 = PopImapProbeBase.AuthErrorParser.Match(tcpResponse.ResponseString);
				if (match2.Success && match2.Groups["authError"].Success)
				{
					error = match2.Groups["authError"].Value;
				}
				Match match3 = PopImapProbeBase.ProxyParserFailedResponse.Match(tcpResponse.ResponseString);
				if (match3.Success && match3.Groups["proxy"].Success)
				{
					base.Result.StateAttribute4 = Utils.ExtractServerNameFromFDQN(match3.Groups["proxy"].Value);
				}
				probeTrackingObject.Result.StateAttribute2 = error;
				foreach (string text in this.acceptableErrors)
				{
					if (error.ToLower().Contains(text.ToLower()))
					{
						ProbeResult result4 = base.Result;
						object stateAttribute = result4.StateAttribute21;
						result4.StateAttribute21 = string.Concat(new object[]
						{
							stateAttribute,
							"BRFE:",
							(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
							";"
						});
						return;
					}
				}
				PopImapProbeUtil.PopImapFailingComponent popImapFailingComponent = PopImapProbeUtil.PopImapFailingComponent.PopImap;
				if (PopImapProbeUtil.KnownErrors.ContainsKey(error))
				{
					popImapFailingComponent = PopImapProbeUtil.KnownErrors[error];
				}
				else
				{
					List<string> list = (from key in PopImapProbeUtil.KnownErrors.Keys
					where error.Contains(key)
					select key).ToList<string>();
					if (list.Count == 1)
					{
						popImapFailingComponent = PopImapProbeUtil.KnownErrors[list.First<string>()];
					}
					else
					{
						error = "Unknown Reason: " + error;
					}
				}
				probeTrackingObject.Result.FailureCategory = (int)popImapFailingComponent;
				if (popImapFailingComponent == PopImapProbeUtil.PopImapFailingComponent.PopImap)
				{
					probeTrackingObject.Result.StateAttribute1 = this.ProbeComponent;
				}
				else
				{
					probeTrackingObject.Result.StateAttribute1 = popImapFailingComponent.ToString();
				}
				if (popImapFailingComponent == PopImapProbeUtil.PopImapFailingComponent.Auth && base.Definition.AccountPassword.StartsWith("%") && base.Definition.AccountPassword.EndsWith("%"))
				{
					probeTrackingObject.Result.StateAttribute1 = PopImapProbeUtil.PopImapFailingComponent.Monitoring.ToString();
					probeTrackingObject.Result.StateAttribute2 = " Bad password for Monitoring Account: " + base.Definition.AccountPassword;
				}
				ProbeResult result5 = probeTrackingObject.Result;
				object stateAttribute2 = result5.StateAttribute21;
				result5.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute2,
					"BRFE:",
					(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
					";"
				});
				this.WriteAllResponses(probeTrackingObject);
				throw new InvalidOperationException("Failed probe instance, please check reasons for failure.");
			}
			if (isAborted)
			{
				probeTrackingObject.Result.StateAttribute1 = PopImapProbeUtil.PopImapFailingComponent.PopImap.ToString();
				probeTrackingObject.Result.StateAttribute2 = "Timedout";
				this.WriteAllResponses(probeTrackingObject);
				throw new InvalidOperationException("Aborted probe due to timeout");
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00079E1C File Offset: 0x0007801C
		protected void ReportSuccess(PopImapProbeStateObject probeTrackingObject)
		{
			DateTime utcNow = DateTime.UtcNow;
			base.Result.SampleValue = (double)((int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds);
			ProbeResult result = probeTrackingObject.Result;
			result.StateAttribute21 += "BRSS;";
			probeTrackingObject.Result.StateAttribute1 = this.ProbeComponent;
			probeTrackingObject.Result.StateAttribute2 = probeTrackingObject.TcpResponses[probeTrackingObject.LastResponseIndex].ResponseType.ToString();
			ProbeResult result2 = probeTrackingObject.Result;
			result2.StateAttribute13 += ":FIN";
			this.WriteAllResponses(probeTrackingObject);
			ProbeResult result3 = base.Result;
			object stateAttribute = result3.StateAttribute21;
			result3.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute,
				"BRSE:",
				(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
				";"
			});
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00079F20 File Offset: 0x00078120
		protected bool AcceptableError(PopImapProbeStateObject probeTrackingObject)
		{
			string responseString = probeTrackingObject.TcpResponses[probeTrackingObject.LastResponseIndex].ResponseString;
			foreach (string text in this.acceptableErrors)
			{
				if (responseString.ToLower().Contains(text.ToLower()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00079FA0 File Offset: 0x000781A0
		protected void HandleConnectionExceptions(PopImapProbeStateObject probeTrackingObject)
		{
			if (probeTrackingObject.ConnectionException == null)
			{
				return;
			}
			if (probeTrackingObject.ConnectionException.GetType() == typeof(InvalidOperationException))
			{
				ProbeResult result = base.Result;
				object stateAttribute = result.StateAttribute21;
				result.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute,
					"PDWE:",
					(int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds,
					";"
				});
				probeTrackingObject.Result.StateAttribute1 = PopImapProbeUtil.PopImapFailingComponent.Networking.ToString();
				probeTrackingObject.Result.StateAttribute2 = "NetworkConnectionFailed - NegotiateSSLError";
				this.HandleSocketError(probeTrackingObject.ConnectionException);
				return;
			}
			if (probeTrackingObject.ConnectionException.GetType() == typeof(SocketException))
			{
				ProbeResult result2 = base.Result;
				object stateAttribute2 = result2.StateAttribute21;
				result2.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute2,
					"PDWE:",
					(int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds,
					";"
				});
				probeTrackingObject.Result.StateAttribute1 = PopImapProbeUtil.PopImapFailingComponent.Networking.ToString();
				probeTrackingObject.Result.StateAttribute2 = "NetworkConnectionFailed - SocketError";
				this.HandleSocketError(probeTrackingObject.ConnectionException);
				return;
			}
			if (!(probeTrackingObject.ConnectionException.GetType() == typeof(ApplicationException)))
			{
				probeTrackingObject.Result.FailureCategory = 9;
				probeTrackingObject.Result.FailureContext = "Exception: " + probeTrackingObject.ConnectionException.ToString();
				probeTrackingObject.Result.StateAttribute1 = this.ProbeComponent;
				probeTrackingObject.Result.StateAttribute2 = "Unknown Reason: " + probeTrackingObject.ConnectionException.Message;
				ProbeResult result3 = probeTrackingObject.Result;
				object stateAttribute3 = result3.StateAttribute21;
				result3.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute3,
					"PDWE:",
					(int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds,
					";"
				});
				throw probeTrackingObject.ConnectionException;
			}
			if (probeTrackingObject.ConnectionException.Message.Contains("EndRead() resulted in non-null error code"))
			{
				ProbeResult result4 = probeTrackingObject.Result;
				object stateAttribute4 = result4.StateAttribute21;
				result4.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute4,
					"PDWE:",
					(int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds,
					";"
				});
				probeTrackingObject.Result.FailureCategory = 9;
				probeTrackingObject.Result.StateAttribute1 = this.ProbeComponent;
				probeTrackingObject.Result.StateAttribute2 = "EndReadFailure";
				this.HandleSocketError(probeTrackingObject.ConnectionException);
				return;
			}
			ApplicationException ex = probeTrackingObject.ConnectionException as ApplicationException;
			probeTrackingObject.Result.FailureCategory = 9;
			probeTrackingObject.Result.StateAttribute1 = this.ProbeComponent;
			probeTrackingObject.Result.StateAttribute2 = ex.Message;
			ProbeResult result5 = probeTrackingObject.Result;
			object stateAttribute5 = result5.StateAttribute21;
			result5.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute5,
				"PDWE:",
				(int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds,
				";"
			});
			throw ex;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0007A33C File Offset: 0x0007853C
		protected void WriteAllResponses(PopImapProbeStateObject probeTrackingObject)
		{
			if (probeTrackingObject.LastResponseIndex > -1)
			{
				foreach (TcpResponse tcpResponse in probeTrackingObject.TcpResponses)
				{
					ProbeResult result = probeTrackingObject.Result;
					result.StateAttribute12 += tcpResponse.ResponseString;
				}
			}
		}

		// Token: 0x04000D95 RID: 3477
		public const string MaxCountOfIPs = "MaxCountOfIPs";

		// Token: 0x04000D96 RID: 3478
		public const string InvokeNowExecutionProbeKey = "InvokeNowExecution";

		// Token: 0x04000D97 RID: 3479
		public const string AcceptableErrors = "KnownFailure";

		// Token: 0x04000D98 RID: 3480
		private const string StorageTransientError = "Storage Transient";

		// Token: 0x04000D99 RID: 3481
		private const string StoragePermanentError = "Storage Permanent";

		// Token: 0x04000D9A RID: 3482
		private const string WrongServerError = "WrongServerException";

		// Token: 0x04000D9B RID: 3483
		private const string ADOperationExceptionError = "ADOperationException";

		// Token: 0x04000D9C RID: 3484
		protected const int MaxRetryAttempts = 3;

		// Token: 0x04000D9D RID: 3485
		private static readonly Regex AuthErrorParser = new Regex("\\[Error=\"?(?<authError>[^\"]+)\"?( Proxy=(?<proxy>.+))?\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000D9E RID: 3486
		private static readonly Regex ProxyParserSuccessResponse = new Regex("\\[Proxy=(?<proxy>.+)?\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000D9F RID: 3487
		private static readonly Regex ProxyParserFailedResponse = new Regex("Proxy=(?<proxy>.+)?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000DA0 RID: 3488
		protected DateTime latencyMeasurementStart;

		// Token: 0x04000DA1 RID: 3489
		protected int timeout;

		// Token: 0x04000DA2 RID: 3490
		protected bool isInvokeNowExecution;

		// Token: 0x04000DA3 RID: 3491
		protected List<string> acceptableErrors = new List<string>(new string[]
		{
			"WrongServerException".ToLowerInvariant()
		});

		// Token: 0x02000280 RID: 640
		protected enum StateResult
		{
			// Token: 0x04000DA5 RID: 3493
			Success,
			// Token: 0x04000DA6 RID: 3494
			Retry,
			// Token: 0x04000DA7 RID: 3495
			Fail
		}
	}
}
