using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveSync.Probes
{
	// Token: 0x02000007 RID: 7
	public abstract class ActiveSyncProbeBase : ProbeWorkItem
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000261C File Offset: 0x0000081C
		protected void TrustAllCerts()
		{
			RemoteCertificateValidationCallback callback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
			CertificateValidationManager.RegisterCallback("ActiveSyncAMProbe", callback);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002654 File Offset: 0x00000854
		protected override void DoWork(CancellationToken cancellationToken)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = this.probeTrackingObject.Result;
			result.StateAttribute21 += "BDWS;";
			this.probeTrackingObject.ResetEvent = new ManualResetEvent(true);
			while (this.probeTrackingObject.State != ProbeState.Finish)
			{
				this.probeTrackingObject.ResetEvent.Reset();
				DateTime utcNow2 = DateTime.UtcNow;
				ProbeResult result2 = this.probeTrackingObject.Result;
				object stateAttribute = result2.StateAttribute21;
				result2.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute,
					"CurrentNow:",
					utcNow2.TimeOfDay,
					";"
				});
				try
				{
					this.probeTrackingObject.WebRequest.BeginGetResponse(new AsyncCallback(this.CommandCallback), this.probeTrackingObject);
				}
				catch (WebException ex)
				{
					if (ex.Response != null)
					{
						this.probeTrackingObject.WebResponses.Add(new ActiveSyncWebResponse((HttpWebResponse)ex.Response));
						this.ParseResponseSetNextState(this.probeTrackingObject);
					}
					else
					{
						this.probeTrackingObject.Result.FailureContext = "Exception: " + ex.ToString();
						this.probeTrackingObject.State = ProbeState.Failure;
					}
				}
				if (!this.probeTrackingObject.ResetEvent.WaitOne(this.probeTrackingObject.TimeoutLimit.Subtract(utcNow2)) || cancellationToken.IsCancellationRequested)
				{
					this.probeTrackingObject.WebRequest.Abort();
					ProbeResult result3 = this.probeTrackingObject.Result;
					object stateAttribute2 = result3.StateAttribute21;
					result3.StateAttribute21 = string.Concat(new object[]
					{
						stateAttribute2,
						"BDWE:",
						DateTime.UtcNow.TimeOfDay,
						";"
					});
					this.ReportFailure(true);
				}
				if (this.probeTrackingObject.State == ProbeState.Failure)
				{
					break;
				}
			}
			if (this.probeTrackingObject.State == ProbeState.Failure)
			{
				ProbeResult result4 = this.probeTrackingObject.Result;
				object stateAttribute3 = result4.StateAttribute21;
				result4.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute3,
					"BDWE:",
					DateTime.UtcNow.TimeOfDay,
					";"
				});
				this.ReportFailure(false);
				return;
			}
			ProbeResult result5 = this.probeTrackingObject.Result;
			object stateAttribute4 = result5.StateAttribute21;
			result5.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute4,
				"BDWE:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
			this.ReportSuccess();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002920 File Offset: 0x00000B20
		private void CommandCallback(IAsyncResult result)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result2 = this.probeTrackingObject.Result;
			result2.StateAttribute21 += "BCBS;";
			ActiveSyncProbeStateObject activeSyncProbeStateObject = (ActiveSyncProbeStateObject)result.AsyncState;
			HttpWebRequest webRequest = activeSyncProbeStateObject.WebRequest;
			try
			{
				activeSyncProbeStateObject.WebResponses.Add(new ActiveSyncWebResponse((HttpWebResponse)webRequest.EndGetResponse(result)));
				this.ParseResponseSetNextState(activeSyncProbeStateObject);
			}
			catch (WebException ex)
			{
				if (ex.Response != null)
				{
					activeSyncProbeStateObject.WebResponses.Add(new ActiveSyncWebResponse((HttpWebResponse)ex.Response));
					this.ParseResponseSetNextState(activeSyncProbeStateObject);
				}
				else
				{
					this.probeTrackingObject.Result.FailureContext = "Exception: " + ex.ToString();
					activeSyncProbeStateObject.State = ProbeState.Failure;
				}
			}
			catch (Exception ex2)
			{
				this.probeTrackingObject.Result.FailureContext = "Exception: " + ex2.ToString();
				activeSyncProbeStateObject.State = ProbeState.Failure;
			}
			finally
			{
				ProbeResult result3 = activeSyncProbeStateObject.Result;
				object stateAttribute = result3.StateAttribute21;
				result3.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute,
					"BCBE:",
					DateTime.UtcNow.TimeOfDay,
					";"
				});
				activeSyncProbeStateObject.ResetEvent.Set();
			}
		}

		// Token: 0x06000012 RID: 18
		protected abstract void ParseResponseSetNextState(ActiveSyncProbeStateObject probeStateObject);

		// Token: 0x06000013 RID: 19
		protected abstract void HandleSocketError(ActiveSyncProbeStateObject probeStateObject);

		// Token: 0x06000014 RID: 20 RVA: 0x00002A98 File Offset: 0x00000C98
		protected void ReportFailure(bool isAborted)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = this.probeTrackingObject.Result;
			result.StateAttribute21 += "BRFS;";
			this.probeTrackingObject.Result.StateAttribute23 = ActiveSyncProbeBase.GetForestName(base.Definition.Account);
			base.Result.SampleValue = (double)((int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds);
			ProbeResult result2 = base.Result;
			result2.StateAttribute13 += "FAIL";
			int num = 0;
			string stateAttribute = string.Empty;
			string fdqn = string.Empty;
			RequestFailureContext requestFailureContext = null;
			if (this.probeTrackingObject.LastResponseIndex != -1)
			{
				num = this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].HttpStatus;
				stateAttribute = this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].RespondingCasServer;
				fdqn = this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].ProxiedMbxServer;
				base.Result.StateAttribute14 = this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].DiagnosticsValue;
				requestFailureContext = this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].CafeErrorValues;
			}
			base.Result.StateAttribute6 = (double)num;
			string text = string.Format("{0}", num);
			if (string.IsNullOrEmpty(base.Result.StateAttribute14))
			{
				base.Result.StateAttribute14 = "NoDiagnosticsHeaderReturned";
				if (base.Result.StateAttribute6 != 401.0)
				{
					if (requestFailureContext != null)
					{
						text = ActiveSyncProbeBase.CategorizeCafeError(requestFailureContext);
						base.Result.StateAttribute14 = "CAFE:" + this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].CafeErrorHeader;
					}
					else
					{
						base.Result.StateAttribute14 = "NoCafeErrorOrActiveSyncDiagnosticsHeaderReturned";
					}
				}
			}
			else
			{
				this.ParseDiagnosticHeader();
			}
			if (string.IsNullOrEmpty(base.Result.StateAttribute3))
			{
				base.Result.StateAttribute3 = stateAttribute;
			}
			if (string.IsNullOrEmpty(base.Result.StateAttribute4))
			{
				base.Result.StateAttribute4 = Utils.ExtractServerNameFromFDQN(fdqn);
				base.Result.StateAttribute15 = ActiveSyncProbeUtil.PopulateSiteName(base.Result.StateAttribute4);
			}
			string[] array = base.Result.StateAttribute14.Split(new string[]
			{
				"Error:"
			}, StringSplitOptions.None);
			if (array.Length >= 2)
			{
				string text2 = array[1];
				text = text2.Split(new char[]
				{
					'_'
				})[0];
			}
			else if (!string.IsNullOrEmpty(this.probeTrackingObject.ProbeErrorResponse))
			{
				if (this.probeTrackingObject.ProbeErrorResponse.IndexOf(MonitoringWebClientStrings.NameResolutionFailure(new Uri(base.Definition.Endpoint).Host), StringComparison.OrdinalIgnoreCase) > -1)
				{
					text = "NameResolution";
					base.Result.Exception = this.probeTrackingObject.ProbeErrorResponse;
				}
				else
				{
					text = this.probeTrackingObject.ProbeErrorResponse;
				}
			}
			if (text.StartsWith("Storage Transient"))
			{
				text = "Storage Transient";
			}
			else if (text.StartsWith("Storage Permanent"))
			{
				text = "Storage Permanent";
			}
			else if (text.StartsWith("WrongServerException"))
			{
				text = "WrongServerException";
			}
			else if (text.StartsWith("ADOperationException"))
			{
				text = "ADOperationException";
			}
			if (isAborted)
			{
				base.Result.StateAttribute1 = ActiveSyncProbeUtil.EasFailingComponent.EAS.ToString();
				base.Result.StateAttribute2 = "Aborted";
			}
			else
			{
				if (ActiveSyncProbeUtil.KnownErrors.ContainsKey(text))
				{
					base.Result.FailureCategory = (int)ActiveSyncProbeUtil.KnownErrors[text];
					base.Result.StateAttribute1 = ActiveSyncProbeUtil.KnownErrors[text].ToString();
				}
				else if (ActiveSyncProbeUtil.KnownErrors.ContainsKey(num.ToString()))
				{
					base.Result.FailureCategory = (int)ActiveSyncProbeUtil.KnownErrors[num.ToString()];
					base.Result.StateAttribute1 = ActiveSyncProbeUtil.KnownErrors[num.ToString()].ToString();
				}
				else
				{
					base.Result.FailureCategory = 7;
					base.Result.StateAttribute1 = ActiveSyncProbeUtil.EasFailingComponent.EAS.ToString();
					text = "Unknown Reason: " + text;
				}
				base.Result.StateAttribute2 = text;
			}
			int num2 = 0;
			XmlDocument xmlDocument = null;
			if (this.probeTrackingObject.LastResponseIndex != -1 && this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].ResponseBody != null)
			{
				xmlDocument = this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].ResponseBody;
				using (XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Status"))
				{
					int.TryParse(elementsByTagName[0].InnerXml, out num2);
				}
			}
			ProbeResult result3 = base.Result;
			string stateAttribute2 = result3.StateAttribute13;
			result3.StateAttribute13 = string.Concat(new string[]
			{
				stateAttribute2,
				"/HTTP:",
				num.ToString(),
				"/EASStatus:",
				num2.ToString()
			});
			string text3 = string.Format("Error occurred:\r\n\r\nUser: {0}\r\nPassword: {1}\r\nTarget: {2}\r\nResponse: {3}\r\nDiagnostics header:{4}", new object[]
			{
				base.Definition.Account,
				this.ReadAttribute("HidePasswordInLog", true) ? "******" : base.Definition.AccountPassword,
				base.Definition.Endpoint,
				(xmlDocument == null) ? "No body" : xmlDocument.OuterXml,
				base.Result.StateAttribute14
			});
			base.Result.ExecutionContext = text3;
			WTFDiagnostics.TraceError(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.ActiveSyncTracer, base.TraceContext, text3, null, "ReportFailure", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncProbeBase.cs", 451);
			ProbeResult result4 = this.probeTrackingObject.Result;
			object stateAttribute3 = result4.StateAttribute21;
			result4.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute3,
				"BRFE:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
			throw new WebException(text3);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003120 File Offset: 0x00001320
		protected void ReportSuccess()
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = this.probeTrackingObject.Result;
			result.StateAttribute21 += "BRSS;";
			WTFDiagnostics.TraceInformation(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.ActiveSyncTracer, base.TraceContext, "Parsing Settings positive response.", null, "ReportSuccess", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncProbeBase.cs", 465);
			base.Result.SampleValue = (double)((int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds);
			int num = 0;
			if (this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].ResponseBody != null)
			{
				XmlDocument responseBody = this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].ResponseBody;
				using (XmlNodeList elementsByTagName = responseBody.GetElementsByTagName("Status"))
				{
					int.TryParse(elementsByTagName[0].InnerXml, out num);
				}
			}
			if (this.probeTrackingObject.LastResponseIndex != -1)
			{
				ProbeResult result2 = base.Result;
				object stateAttribute = result2.StateAttribute13;
				result2.StateAttribute13 = string.Concat(new object[]
				{
					stateAttribute,
					"FIN/HTTP:",
					this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].HttpStatus.ToString(),
					"/EASStatus:",
					num
				});
				base.Result.StateAttribute14 = this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].DiagnosticsValue;
			}
			base.Result.StateAttribute1 = ActiveSyncProbeUtil.EasFailingComponent.EAS.ToString();
			base.Result.StateAttribute2 = "Success";
			if (string.IsNullOrEmpty(base.Result.StateAttribute14))
			{
				base.Result.StateAttribute14 = "NoDiagnosticsHeaderReturned";
			}
			else
			{
				this.ParseDiagnosticHeader();
				if (string.IsNullOrEmpty(base.Result.StateAttribute3))
				{
					base.Result.StateAttribute3 = this.probeTrackingObject.WebResponses[this.probeTrackingObject.LastResponseIndex].RespondingCasServer;
				}
			}
			WTFDiagnostics.TraceInformation<string, string, string>(Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring.ExTraceGlobals.ActiveSyncTracer, base.TraceContext, "ActiveSync worked for user: {0} with Password: {1} and target: {2}", base.Definition.Account, this.ReadAttribute("HidePasswordInLog", true) ? "******" : base.Definition.AccountPassword, base.Definition.Endpoint, null, "ReportSuccess", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncProbeBase.cs", 507);
			base.Result.ExecutionContext = base.Result.StateAttribute14;
			ProbeResult result3 = this.probeTrackingObject.Result;
			object stateAttribute2 = result3.StateAttribute21;
			result3.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute2,
				"BRSE:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003420 File Offset: 0x00001620
		protected void ParseDiagnosticHeader()
		{
			string[] array = base.Result.StateAttribute14.Split(new string[]
			{
				"_"
			}, StringSplitOptions.None);
			foreach (string text in array)
			{
				if (!text.ToLower().Contains("budget:"))
				{
					if (text.ToLower().Contains("cas:"))
					{
						this.probeTrackingObject.Result.StateAttribute3 = text.Split(new char[]
						{
							':'
						})[1];
					}
					if (text.ToLower().Contains("dc:"))
					{
						this.probeTrackingObject.Result.StateAttribute5 = text.Split(new char[]
						{
							':'
						})[1];
					}
					if (text.ToLower().Contains("mbx:"))
					{
						this.probeTrackingObject.Result.StateAttribute4 = Utils.ExtractServerNameFromFDQN(text.Split(new char[]
						{
							':'
						})[1]);
						if (!string.IsNullOrEmpty(this.probeTrackingObject.Result.StateAttribute4))
						{
							base.Result.StateAttribute15 = ActiveSyncProbeUtil.PopulateSiteName(base.Result.StateAttribute4);
						}
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003568 File Offset: 0x00001768
		protected bool AcceptableError(string diagnosticsHeader)
		{
			if (diagnosticsHeader != null)
			{
				string[] array = diagnosticsHeader.Split(new string[]
				{
					"Error:"
				}, StringSplitOptions.None);
				if (array.Length >= 2)
				{
					string text = array[1].ToLowerInvariant();
					foreach (string value in this.acceptableErrors)
					{
						if (text.StartsWith(value))
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000035F4 File Offset: 0x000017F4
		private static string CategorizeCafeError(RequestFailureContext cafeError)
		{
			if (cafeError.LiveIdAuthResult != null)
			{
				switch (cafeError.LiveIdAuthResult.Value)
				{
				case LiveIdAuthResult.UserNotFoundInAD:
				case LiveIdAuthResult.AuthFailure:
				case LiveIdAuthResult.ExpiredCreds:
				case LiveIdAuthResult.InvalidCreds:
				case LiveIdAuthResult.RecoverableAuthFailure:
				case LiveIdAuthResult.AmbigiousMailboxFoundFailure:
					return "MonitoringAccountFailure";
				case LiveIdAuthResult.LiveServerUnreachable:
				case LiveIdAuthResult.FederatedStsUnreachable:
				case LiveIdAuthResult.OperationTimedOut:
				case LiveIdAuthResult.CommunicationFailure:
					return "LiveIdFailure";
				}
			}
			if (cafeError.HttpProxySubErrorCode != null)
			{
				HttpProxySubErrorCode value = cafeError.HttpProxySubErrorCode.Value;
				if (value <= HttpProxySubErrorCode.BackEndRequestTimedOut)
				{
					switch (value)
					{
					case HttpProxySubErrorCode.DirectoryOperationError:
					case HttpProxySubErrorCode.MServOperationError:
					case HttpProxySubErrorCode.ServerDiscoveryError:
						break;
					case HttpProxySubErrorCode.ServerLocatorError:
						goto IL_E3;
					default:
						if (value != HttpProxySubErrorCode.BackEndRequestTimedOut)
						{
							goto IL_EF;
						}
						return "CafeTimeOutContactingBackEnd";
					}
				}
				else
				{
					switch (value)
					{
					case HttpProxySubErrorCode.DatabaseNameNotFound:
					case HttpProxySubErrorCode.DatabaseGuidNotFound:
					case HttpProxySubErrorCode.OrganizationMailboxNotFound:
						goto IL_E3;
					default:
						if (value != HttpProxySubErrorCode.BadSamlToken)
						{
							goto IL_EF;
						}
						break;
					}
				}
				return "CafeActiveDirectoryFailure";
				IL_E3:
				return "CafeHighAvailabilityFailure";
			}
			IL_EF:
			return "CafeFailure";
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000036F8 File Offset: 0x000018F8
		private static string GetForestName(string account)
		{
			string text = "NoValidForest";
			if (!string.IsNullOrEmpty(account) && account.Contains("@") && account.Contains("."))
			{
				int num = account.IndexOf("@") + 1;
				int num2 = account.IndexOf(".", num);
				text = account.Substring(num, num2 - num).ToLower();
				if (text.Contains("e15"))
				{
					text = text.Substring(0, text.IndexOf("e15"));
				}
			}
			return text;
		}

		// Token: 0x04000014 RID: 20
		public const string MaxCountOfIPs = "MaxCountOfIPs";

		// Token: 0x04000015 RID: 21
		public const string DCProbeKey = "DCProbe";

		// Token: 0x04000016 RID: 22
		public const string InvokeNowExecutionProbeKey = "InvokeNowExecution";

		// Token: 0x04000017 RID: 23
		public const string AcceptableErrors = "KnownFailure";

		// Token: 0x04000018 RID: 24
		public const string LegacyEndpoint = "LegacyEndpoint";

		// Token: 0x04000019 RID: 25
		private const string StorageTransientError = "Storage Transient";

		// Token: 0x0400001A RID: 26
		private const string StoragePermanentError = "Storage Permanent";

		// Token: 0x0400001B RID: 27
		private const string WrongServerError = "WrongServerException";

		// Token: 0x0400001C RID: 28
		private const string ADOperationExceptionError = "ADOperationException";

		// Token: 0x0400001D RID: 29
		protected ActiveSyncProbeStateObject probeTrackingObject;

		// Token: 0x0400001E RID: 30
		protected DateTime latencyMeasurementStart;

		// Token: 0x0400001F RID: 31
		protected int timeout;

		// Token: 0x04000020 RID: 32
		protected bool isDcProbe;

		// Token: 0x04000021 RID: 33
		protected bool isInvokeNowExecution;

		// Token: 0x04000022 RID: 34
		protected List<string> acceptableErrors = new List<string>(new string[]
		{
			"WrongServerException".ToLowerInvariant()
		});

		// Token: 0x02000008 RID: 8
		protected enum StateResult
		{
			// Token: 0x04000025 RID: 37
			Success,
			// Token: 0x04000026 RID: 38
			Retry,
			// Token: 0x04000027 RID: 39
			Redirect,
			// Token: 0x04000028 RID: 40
			Fail
		}
	}
}
