using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Net;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.Probes
{
	// Token: 0x02000281 RID: 641
	public abstract class TcpProbe : ProbeWorkItem
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0007A41B File Offset: 0x0007861B
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x0007A423 File Offset: 0x00078623
		protected TcpConnection TcpCon { get; set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0007A42C File Offset: 0x0007862C
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x0007A434 File Offset: 0x00078634
		private protected string UserName { protected get; private set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0007A43D File Offset: 0x0007863D
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x0007A445 File Offset: 0x00078645
		private protected string Password { protected get; private set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0007A44E File Offset: 0x0007864E
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x0007A456 File Offset: 0x00078656
		protected bool IsSecure { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0007A45F File Offset: 0x0007865F
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x0007A467 File Offset: 0x00078667
		protected IPEndPoint EndPoint { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0007A470 File Offset: 0x00078670
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x0007A478 File Offset: 0x00078678
		private protected bool IsMbxProbe { protected get; private set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0007A481 File Offset: 0x00078681
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x0007A489 File Offset: 0x00078689
		private protected bool IsLocalProbe { protected get; private set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001210 RID: 4624
		protected abstract Trace Tracer { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001211 RID: 4625
		protected abstract string ProbeComponent { get; }

		// Token: 0x06001212 RID: 4626 RVA: 0x0007A494 File Offset: 0x00078694
		protected virtual void ParseDefinition()
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = base.Result;
			result.StateAttribute21 += "TPDS;";
			IPAddress address;
			if (!IPAddress.TryParse(base.Definition.Endpoint, out address))
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(base.Definition.Endpoint);
				if (hostEntry.AddressList.Length == 0)
				{
					throw new FormatException(string.Format("Unable to resolve ServerName '{0}' to an IpAddress", base.Definition.Endpoint));
				}
				address = hostEntry.AddressList[0];
			}
			int port;
			if (!int.TryParse(base.Definition.SecondaryEndpoint, out port))
			{
				ProbeResult result2 = base.Result;
				object stateAttribute = result2.StateAttribute21;
				result2.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute,
					"TPDE:",
					(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
					";"
				});
				throw new FormatException(string.Format("Unable to parse endpoint Port. Recieved: \"{0}\" Expected: int", base.Definition.SecondaryEndpoint));
			}
			if (base.Definition.Attributes.ContainsKey("MbxProbe"))
			{
				bool isMbxProbe;
				bool.TryParse(base.Definition.Attributes["MbxProbe"], out isMbxProbe);
				this.IsMbxProbe = isMbxProbe;
			}
			if (base.Definition.Attributes.ContainsKey("IsLocalProbe"))
			{
				bool isLocalProbe;
				bool.TryParse(base.Definition.Attributes["IsLocalProbe"], out isLocalProbe);
				this.IsLocalProbe = isLocalProbe;
			}
			this.EndPoint = new IPEndPoint(address, port);
			this.UserName = base.Definition.Account;
			this.Password = base.Definition.AccountPassword;
			ProbeResult result3 = base.Result;
			object stateAttribute2 = result3.StateAttribute21;
			result3.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute2,
				"TPDE:",
				(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
				";"
			});
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0007A69C File Offset: 0x0007889C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = base.Result;
			result.StateAttribute21 += "TDWS;";
			this.latencyMeasurementStart = DateTime.UtcNow;
			int num = 110000;
			if (base.Definition.Attributes.ContainsKey("ProbeTimeout"))
			{
				int.TryParse(base.Definition.Attributes["ProbeTimeout"], out num);
			}
			bool skipLogin = false;
			if (base.Definition.Attributes.ContainsKey("LightMode"))
			{
				bool.TryParse(base.Definition.Attributes["LightMode"], out skipLogin);
			}
			try
			{
				this.ParseDefinition();
				ProbeResult result2 = base.Result;
				result2.StateAttribute13 += "R1";
				if (!this.InvokeProbe(skipLogin) && !this.IsLocalProbe)
				{
					ProbeResult result3 = base.Result;
					result3.StateAttribute13 += ":R2";
					this.InvokeProbe(skipLogin);
				}
				if (this.IsMbxProbe && string.IsNullOrEmpty(base.Result.StateAttribute4))
				{
					base.Result.StateAttribute4 = this.EndPoint.Address.ToString();
				}
				if (!this.IsMbxProbe && string.IsNullOrEmpty(base.Result.StateAttribute3))
				{
					base.Result.StateAttribute3 = this.EndPoint.Address.ToString();
				}
			}
			catch (InvalidOperationException ex)
			{
				if (ex.ToString().IndexOf(MonitoringWebClientStrings.NameResolutionFailure(base.Definition.Endpoint), StringComparison.OrdinalIgnoreCase) > -1)
				{
					base.Result.FailureCategory = 3;
					base.Result.StateAttribute1 = PopImapProbeUtil.PopImapFailingComponent.Networking.ToString();
					base.Result.StateAttribute2 = "NameResolution";
				}
				throw;
			}
			finally
			{
				if (this.TcpCon != null)
				{
					this.TcpCon.Close();
				}
				base.Result.SampleValue = (double)((int)(DateTime.UtcNow - this.latencyMeasurementStart).TotalMilliseconds);
				ProbeResult result4 = base.Result;
				result4.StateAttribute21 = result4.StateAttribute21 + "TDWE:" + (int)(DateTime.UtcNow - utcNow).TotalMilliseconds;
			}
		}

		// Token: 0x06001214 RID: 4628
		protected abstract void TestProtocol();

		// Token: 0x06001215 RID: 4629
		protected abstract void InitializeConnection();

		// Token: 0x06001216 RID: 4630
		protected abstract void DetermineCapability();

		// Token: 0x06001217 RID: 4631 RVA: 0x0007A918 File Offset: 0x00078B18
		protected void VerifyResponse(TcpResponse response, string failureMessage)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = base.Result;
			result.StateAttribute21 += "TVRS;";
			string executionContext = string.Format("Probe executed for\r\n\r\nUser: {0}\r\nPassword: {1}\r\nTarget: {2}", base.Definition.Account, base.Definition.AccountPassword, base.Definition.Endpoint);
			base.Result.ExecutionContext = executionContext;
			if (response != null)
			{
				ProbeResult result2 = base.Result;
				result2.StateAttribute12 += response.ResponseString;
				Match match = TcpProbe.ProxyParserSuccessResponse.Match(response.ResponseString);
				if (match.Success && match.Groups["proxy"].Success)
				{
					base.Result.StateAttribute4 = Utils.ExtractServerNameFromFDQN(match.Groups["proxy"].Value);
				}
				if (response.ResponseType != ResponseType.success)
				{
					string error = response.ResponseString;
					Match match2 = TcpProbe.AuthErrorParser.Match(response.ResponseString);
					if (match2.Success && match2.Groups["authError"].Success)
					{
						error = match2.Groups["authError"].Value;
					}
					Match match3 = TcpProbe.ProxyParserFailedResponse.Match(response.ResponseString);
					if (match3.Success && match3.Groups["proxy"].Success)
					{
						base.Result.StateAttribute4 = Utils.ExtractServerNameFromFDQN(match3.Groups["proxy"].Value);
					}
					base.Result.StateAttribute2 = error;
					foreach (string text in this.acceptableErrors)
					{
						if (error.ToLower().Contains(text.ToLower()))
						{
							ProbeResult result3 = base.Result;
							object stateAttribute = result3.StateAttribute21;
							result3.StateAttribute21 = string.Concat(new object[]
							{
								stateAttribute,
								"TVRE:",
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
					base.Result.FailureCategory = (int)popImapFailingComponent;
					if (popImapFailingComponent == PopImapProbeUtil.PopImapFailingComponent.PopImap)
					{
						base.Result.StateAttribute1 = this.ProbeComponent;
					}
					else
					{
						base.Result.StateAttribute1 = popImapFailingComponent.ToString();
					}
					if (popImapFailingComponent == PopImapProbeUtil.PopImapFailingComponent.Auth && base.Definition.AccountPassword.StartsWith("%") && base.Definition.AccountPassword.EndsWith("%"))
					{
						base.Result.StateAttribute1 = PopImapProbeUtil.PopImapFailingComponent.Monitoring.ToString();
						base.Result.StateAttribute2 = " Bad password for Monitoring Account: " + base.Definition.AccountPassword;
					}
					string message = string.Format("Unexpected Server Response: \"{0}\"", response.ResponseString);
					WTFDiagnostics.TraceError(this.Tracer, base.TraceContext, message, null, "VerifyResponse", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\TcpProbe.cs", 457);
					ProbeResult result4 = base.Result;
					object stateAttribute2 = result4.StateAttribute21;
					result4.StateAttribute21 = string.Concat(new object[]
					{
						stateAttribute2,
						"TVRE:",
						(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
						";"
					});
					throw new InvalidOperationException(failureMessage);
				}
				ProbeResult result5 = base.Result;
				object stateAttribute3 = result5.StateAttribute21;
				result5.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute3,
					"TVRE:",
					(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
					";"
				});
				return;
			}
			base.Result.StateAttribute1 = PopImapProbeUtil.PopImapFailingComponent.PopImap.ToString();
			base.Result.StateAttribute2 = "Timedout";
			throw new InvalidOperationException(failureMessage);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0007ADDC File Offset: 0x00078FDC
		protected bool InvokeProbe(bool skipLogin)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = base.Result;
			result.StateAttribute21 += "TIPS;";
			bool result5;
			try
			{
				ProbeResult result2 = base.Result;
				result2.StateAttribute13 += ":C";
				this.InitializeConnection();
				if (!skipLogin)
				{
					this.TestProtocol();
				}
				ProbeResult result3 = base.Result;
				result3.StateAttribute13 += ":FIN";
				ProbeResult result4 = base.Result;
				object stateAttribute = result4.StateAttribute21;
				result4.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute,
					"TIPE:",
					(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
					";"
				});
				result5 = true;
			}
			catch (InvalidOperationException ex)
			{
				if (ex.Message.Contains("TcpConnection Errorcode was not null"))
				{
					this.HandleSocketException(ex);
					ProbeResult result6 = base.Result;
					object stateAttribute2 = result6.StateAttribute21;
					result6.StateAttribute21 = string.Concat(new object[]
					{
						stateAttribute2,
						"TIPE:",
						(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
						";"
					});
					result5 = true;
				}
				else
				{
					if (base.Result.StateAttribute13.Contains("R2") || this.IsLocalProbe || this.isVipProbe)
					{
						ProbeResult result7 = base.Result;
						result7.StateAttribute13 += ":FAIL";
						throw;
					}
					ProbeResult result8 = base.Result;
					object stateAttribute3 = result8.StateAttribute21;
					result8.StateAttribute21 = string.Concat(new object[]
					{
						stateAttribute3,
						"TIPE:",
						(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
						";"
					});
					result5 = false;
				}
			}
			catch (SocketException e)
			{
				this.HandleSocketException(e);
				ProbeResult result9 = base.Result;
				object stateAttribute4 = result9.StateAttribute21;
				result9.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute4,
					"TIPE:",
					(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
					";"
				});
				result5 = true;
			}
			catch (ApplicationException ex2)
			{
				if (!ex2.Message.Contains("EndRead() resulted in non-null error code"))
				{
					base.Result.FailureCategory = 9;
					base.Result.StateAttribute1 = this.ProbeComponent;
					base.Result.StateAttribute2 = ex2.Message;
					ProbeResult result10 = base.Result;
					object stateAttribute5 = result10.StateAttribute21;
					result10.StateAttribute21 = string.Concat(new object[]
					{
						stateAttribute5,
						"TIPE:",
						(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
						";"
					});
					throw;
				}
				this.HandleSocketException(ex2);
				ProbeResult result11 = base.Result;
				object stateAttribute6 = result11.StateAttribute21;
				result11.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute6,
					"TIPE:",
					(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
					";"
				});
				result5 = true;
			}
			catch (Exception ex3)
			{
				base.Result.FailureCategory = 9;
				base.Result.FailureContext = "Exception: " + ex3.ToString();
				base.Result.StateAttribute1 = this.ProbeComponent;
				base.Result.StateAttribute2 = "Unknown Reason: " + ex3.Message;
				ProbeResult result12 = base.Result;
				object stateAttribute7 = result12.StateAttribute21;
				result12.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute7,
					"TIPE:",
					(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
					";"
				});
				throw ex3;
			}
			return result5;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0007B254 File Offset: 0x00079454
		protected virtual void HandleSocketException(Exception e)
		{
			if (!this.probeTimedOut)
			{
				DateTime utcNow = DateTime.UtcNow;
				ProbeResult result = base.Result;
				result.StateAttribute21 += "THSS;";
				base.Result.FailureCategory = 9;
				base.Result.StateAttribute1 = this.ProbeComponent;
				base.Result.StateAttribute2 = "Infrastructure Failure";
				WTFDiagnostics.TraceError(this.Tracer, base.TraceContext, e.Message, null, "HandleSocketException", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\TcpProbe.cs", 569);
				ProbeResult result2 = base.Result;
				result2.StateAttribute13 += ":FAIL";
				ProbeResult result3 = base.Result;
				object stateAttribute = result3.StateAttribute21;
				result3.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute,
					"THSE:",
					(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
					";"
				});
				throw new InvalidOperationException("Unable to initialise TCP Network connection", e);
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0007B358 File Offset: 0x00079558
		internal string DecodeServerName(string banner)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(banner))
			{
				string[] array = banner.Split(new string[]
				{
					" "
				}, StringSplitOptions.None);
				string text2 = array[array.Length - 1];
				if (text2.StartsWith("[") && text2.EndsWith("]"))
				{
					text2 = text2.Substring(1, text2.Length - 2);
					try
					{
						text = Encoding.Unicode.GetString(Convert.FromBase64String(text2));
					}
					catch (FormatException)
					{
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "Unable to retrieve server name";
			}
			return text;
		}

		// Token: 0x04000DA8 RID: 3496
		private const string bannerServerDelimiter = " ";

		// Token: 0x04000DA9 RID: 3497
		public const string MaxCountOfIPs = "MaxCountOfIPs";

		// Token: 0x04000DAA RID: 3498
		public const string LightModeKey = "LightMode";

		// Token: 0x04000DAB RID: 3499
		public const string ProbeTimeout = "ProbeTimeout";

		// Token: 0x04000DAC RID: 3500
		public const string DatabaseGuid = "DatabaseGuid";

		// Token: 0x04000DAD RID: 3501
		public const string MbxProbeKey = "MbxProbe";

		// Token: 0x04000DAE RID: 3502
		public const string IsLocalKey = "IsLocalProbe";

		// Token: 0x04000DAF RID: 3503
		protected const string WrongServerError = "WrongServerException";

		// Token: 0x04000DB0 RID: 3504
		private static readonly Regex AuthErrorParser = new Regex("\\[Error=\"?(?<authError>[^\"]+)\"?( Proxy=(?<proxy>.+))?\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000DB1 RID: 3505
		private static readonly Regex ProxyParserSuccessResponse = new Regex("\\[Proxy=(?<proxy>.+)?\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000DB2 RID: 3506
		private static readonly Regex ProxyParserFailedResponse = new Regex("Proxy=(?<proxy>.+)?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000DB3 RID: 3507
		protected DateTime latencyMeasurementStart;

		// Token: 0x04000DB4 RID: 3508
		protected bool isVipProbe;

		// Token: 0x04000DB5 RID: 3509
		protected List<string> acceptableErrors = new List<string>();

		// Token: 0x04000DB6 RID: 3510
		protected bool probeTimedOut;
	}
}
