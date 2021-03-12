using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search;
using Microsoft.Exchange.Net.MonitoringWebClient;
using Microsoft.Exchange.Net.MonitoringWebClient.E4e;
using Microsoft.Exchange.Transport.RightsManagement;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200008C RID: 140
	public class E4ePortalProbe : ProbeWorkItem
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00016A4A File Offset: 0x00014C4A
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00016A52 File Offset: 0x00014C52
		public Uri EndpointUrl { get; set; }

		// Token: 0x060003CD RID: 973 RVA: 0x00016A5C File Offset: 0x00014C5C
		public override void PopulateDefinition<TDefinition>(TDefinition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition", "definition");
			}
			probeDefinition.TargetResource = propertyBag["TargetResource"];
			if (propertyBag.ContainsKey("Endpoint"))
			{
				probeDefinition.Endpoint = propertyBag["Endpoint"];
			}
			else if (ExEnvironment.IsSdfDomain)
			{
				probeDefinition.Endpoint = "sdfpilot.outlook.com";
			}
			else
			{
				probeDefinition.Endpoint = "outlook.office365.com";
			}
			if (propertyBag.ContainsKey("SecondaryEndpoint"))
			{
				probeDefinition.SecondaryEndpoint = propertyBag["SecondaryEndpoint"];
			}
			if (propertyBag.ContainsKey("SecondaryAccount"))
			{
				probeDefinition.SecondaryAccount = propertyBag["SecondaryAccount"];
			}
			if (propertyBag.ContainsKey("SecondaryAccountPassword"))
			{
				probeDefinition.SecondaryAccountPassword = propertyBag["SecondaryAccountPassword"];
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00016B34 File Offset: 0x00014D34
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (ExEnvironment.IsTest && string.IsNullOrEmpty(base.Definition.SecondaryEndpoint))
			{
				return;
			}
			this.EndpointUrl = new Uri(this.ReadAttribute("PortalServerUrl", string.Format("https://{0}/", base.Definition.Endpoint)));
			string messageSubject = "IPTDLPE_OME_Portal_Probe";
			string messageBody = "IPTDLP Encryption Team: OME Portal Probe.";
			string text;
			string text2;
			string accountPassword;
			this.FindMonitoringTenantAndAccountOnLocalServer(out text, out text2, out accountPassword);
			base.Result.StateAttribute21 = text;
			base.Result.StateAttribute4 = text2;
			this.CheckCancellation(cancellationToken);
			byte[] messageHtml = this.GenerateMessageHtml(new Guid(text), text2, messageSubject, messageBody);
			this.CheckCancellation(cancellationToken);
			this.OpenMessageHtml(cancellationToken, messageSubject, messageBody, messageHtml, text2, accountPassword);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00016BE4 File Offset: 0x00014DE4
		private void FindMonitoringTenantAndAccountOnLocalServer(out string monitoringTenantId, out string monitoringAccountUpn, out string monitoringAccountPassword)
		{
			this.TraceDebug("FindMonitoringTenantAndAccountOnLocalServer", new object[0]);
			if (!string.IsNullOrEmpty(base.Definition.SecondaryEndpoint))
			{
				this.TraceDebug("Use SecondaryEndpoint", new object[0]);
				monitoringTenantId = base.Definition.SecondaryEndpoint;
				monitoringAccountUpn = base.Definition.SecondaryAccount;
				monitoringAccountPassword = base.Definition.SecondaryAccountPassword;
				return;
			}
			MailboxProvider instance = MailboxProvider.GetInstance();
			ICollection<MailboxDatabaseInfo> collection;
			MailboxDatabaseSelectionResult allMailboxDatabaseInfo = instance.GetAllMailboxDatabaseInfo(out collection);
			if (allMailboxDatabaseInfo != MailboxDatabaseSelectionResult.Success)
			{
				this.SetErrorTypeAndThrowException("GetAllMailboxDatabaseInfo result is " + allMailboxDatabaseInfo.ToString(), null);
			}
			base.Result.StateAttribute16 = ((collection == null) ? double.NaN : ((double)collection.Count));
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in collection)
			{
				if (DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(mailboxDatabaseInfo.MailboxDatabaseGuid) && RmsClientManager.IRMConfig.IsInternalLicensingEnabledForTenant(mailboxDatabaseInfo.MonitoringAccountOrganizationId))
				{
					monitoringTenantId = mailboxDatabaseInfo.MonitoringAccountOrganizationId.ToExternalDirectoryOrganizationId();
					monitoringAccountUpn = mailboxDatabaseInfo.MonitoringAccountUserPrincipalName;
					monitoringAccountPassword = mailboxDatabaseInfo.MonitoringAccountPassword;
					return;
				}
			}
			this.SetErrorTypeAndThrowException("Cannot find monitoring tenant.", null);
			monitoringTenantId = null;
			monitoringAccountUpn = null;
			monitoringAccountPassword = null;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00016D64 File Offset: 0x00014F64
		private byte[] GenerateMessageHtml(Guid monitoringTenantId, string messageFromTo, string messageSubject, string messageBody)
		{
			this.TraceDebug("GenerateMessageHtml", new object[0]);
			List<string> list = new List<string>();
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					this.TraceDebug("Create SmtpProbeWorkDefinition", new object[0]);
					string text = "E4ePortalProbe";
					base.Definition.ExtensionAttributes = string.Format("<WorkContext>\r\n    <SendMail SmtpServerUri=\"localhost\">\r\n        <MailFrom Username=\"{0}\" />\r\n        <MailTo Username=\"{0}\" />\r\n        <Message Subject=\"{1}\" Body=\"{2}\" />\r\n    </SendMail>\r\n    <Match>\r\n        <!-- We don't need match element, this is to satisfy SmtpProbeWorkDefinition -->\r\n        <Notification Type=\"RECEIVE\" MatchType=\"SubString\" Value=\"*\" Mandatory=\"true\"/>\r\n    </Match>\r\n</WorkContext>", messageFromTo, messageSubject, messageBody);
					SmtpProbeWorkDefinition smtpProbeWorkDefinition = new SmtpProbeWorkDefinition(0, base.Definition);
					smtpProbeWorkDefinition.SendMail.Message.Headers.Add(new NameValuePair("Message-ID", Guid.NewGuid().ToString()));
					this.TraceDebug("Create CreateDefaultMessageStream", new object[0]);
					SendMailHelper.CreateDefaultMessageStream(text, smtpProbeWorkDefinition.SendMail, memoryStream, text);
					base.Result.StateAttribute8 = (double)memoryStream.Length;
					this.TraceDebug("EncryptProbeMail", new object[0]);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					E4eProbeHelper.EncryptProbeMail(monitoringTenantId, messageFromTo, messageFromTo, memoryStream, memoryStream2);
					base.Result.StateAttribute9 = (double)memoryStream2.Length;
					this.TraceDebug("EncryptProbeMail completed", new object[0]);
					memoryStream2.Seek(0L, SeekOrigin.Begin);
					using (StreamReader streamReader = new StreamReader(memoryStream2))
					{
						while (!streamReader.EndOfStream)
						{
							list.Add(streamReader.ReadLine());
						}
					}
				}
			}
			this.TraceDebug("Extract attachment", new object[0]);
			Pop3Message pop3Message = new Pop3Message(list);
			List<Pop3Attachment> attachments = pop3Message.Attachments;
			if (attachments == null || attachments.Count == 0)
			{
				this.SetErrorTypeAndThrowException("No attachment in the message.", null);
			}
			string attachmentNames = string.Empty;
			attachments.ForEach(delegate(Pop3Attachment a)
			{
				attachmentNames = attachmentNames + a.Name + ";";
			});
			base.Result.StateAttribute15 = attachmentNames;
			base.Result.StateAttribute6 = (double)attachments.Count;
			Pop3Attachment pop3Attachment = attachments.Find((Pop3Attachment a) => a.Name == "message.html");
			byte[] array = (pop3Attachment != null) ? pop3Attachment.Content : null;
			if (array == null || array.Length == 0)
			{
				this.SetErrorTypeAndThrowException("message.html is null or empty.", null);
			}
			return array;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00017068 File Offset: 0x00015268
		private void OpenMessageHtml(CancellationToken cancellationToken, string messageSubject, string messageBody, byte[] messageHtml, string accountUpn, string accountPassword)
		{
			this.TraceDebug("Open message.html", new object[0]);
			string text;
			Dictionary<string, string> dictionary;
			if (!this.TryParseMessageHtml(Encoding.ASCII.GetString(messageHtml), out text, out dictionary))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(text);
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					string key = keyValuePair.Key;
					string text2 = keyValuePair.Value;
					if (text2 == null)
					{
						text2 = "<null>";
					}
					else
					{
						text2 = text2.Length + " " + text2;
						if (text2.Length > 100)
						{
							text2 = text2.Substring(0, 100) + "...";
						}
					}
					stringBuilder.AppendLine(key);
					stringBuilder.AppendLine(text2);
				}
				base.Result.StateAttribute2 = stringBuilder.ToString();
				this.SetErrorTypeAndThrowException("Parse message.html failed.", null);
			}
			string text3;
			dictionary.TryGetValue("rpmsg", out text3);
			base.Result.StateAttribute7 = ((text3 == null) ? double.NaN : ((double)text3.Length));
			this.TraceDebug("Create HttpSession", new object[0]);
			IRequestAdapter requestAdapter = new RequestAdapter();
			IExceptionAnalyzer exceptionAnalyzer = new E4eExceptionAnalyzer(new Dictionary<string, RequestTarget>());
			IResponseTracker responseTracker = new ResponseTracker();
			HttpSession session = new HttpSession(requestAdapter, exceptionAnalyzer, responseTracker);
			TestFactory factory = new TestFactory();
			this.TraceDebug("Authenticate", new object[0]);
			AuthenticationParameters authenticationParameters = new AuthenticationParameters();
			authenticationParameters.ShouldDownloadStaticFileOnLogonPage = false;
			using (SecureString secureString = new SecureString())
			{
				foreach (char c in accountPassword)
				{
					secureString.AppendChar(c);
				}
				Exception exception = null;
				using (AutoResetEvent e = new AutoResetEvent(false))
				{
					Authenticate authenticate = new Authenticate(new Uri(this.EndpointUrl, "/owa/"), accountUpn, null, secureString, authenticationParameters, factory);
					authenticate.BeginExecute(session, delegate(IAsyncResult r)
					{
						try
						{
							authenticate.EndExecute(r);
						}
						catch (Exception exception)
						{
							exception = exception;
						}
						finally
						{
							e.Set();
						}
					}, authenticate);
					e.WaitOne();
					if (exception != null)
					{
						this.SetErrorTypeAndThrowException("OWA authenticate failed.", exception);
					}
				}
			}
			this.CheckCancellation(cancellationToken);
			string text4 = this.PostMessage(session, text, dictionary);
			base.Result.StateAttribute5 = text4;
			this.VerifyLoggedIn(session, cancellationToken, text4, accountUpn, messageSubject, messageBody);
			this.VerifyOTPFlow(cancellationToken, text, dictionary, messageSubject, messageBody, accountUpn);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00017360 File Offset: 0x00015560
		private void VerifyStringContains(string message, string s, params string[] substrings)
		{
			if (string.IsNullOrEmpty(s))
			{
				base.Result.StateAttribute2 = s;
				this.SetErrorTypeAndThrowException(message + " StateAttribute2 is empty", null);
			}
			foreach (string text in substrings)
			{
				if (s.IndexOf(text, StringComparison.OrdinalIgnoreCase) < 0)
				{
					base.Result.StateAttribute2 = s;
					base.Result.StateAttribute3 = text;
					this.SetErrorTypeAndThrowException(message + " StateAttribute2.Contains(StateAttribute3)", null);
				}
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000173DC File Offset: 0x000155DC
		private bool TryParseMessageHtml(string messageHtml, out string url, out Dictionary<string, string> inputs)
		{
			this.TraceDebug("TryParseMessageHtml", new object[0]);
			url = HttpUtility.HtmlDecode(E4eDecryptionHelper.GetValueOf(messageHtml, "form", "name", "form1", "action"));
			inputs = new Dictionary<string, string>();
			Regex regex = new Regex("\\<input\\s+type\\=[\\'\\\"]hidden[\\'\\\"]\\s+name\\=[\\'\\\"](?<name>\\w+)[\\'\\\"]\\s+", RegexOptions.IgnoreCase);
			MatchCollection matchCollection = regex.Matches(messageHtml);
			if (matchCollection == null)
			{
				return false;
			}
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				string value = match.Groups["name"].Value;
				string value2 = HttpUtility.HtmlDecode(E4eDecryptionHelper.GetValueOf(messageHtml, "input", "name", value, "value"));
				inputs.Add(value, value2);
			}
			return !string.IsNullOrEmpty(url) && inputs.Count >= 4;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00017708 File Offset: 0x00015908
		private string PostMessage(HttpSession session, string url, Dictionary<string, string> inputs)
		{
			this.TraceDebug("PostMessage", url);
			string itemId2;
			using (ManualResetEvent e = new ManualResetEvent(false))
			{
				string itemId = null;
				Exception exception = null;
				string text = Guid.NewGuid().ToString();
				RequestBody requestBodyForPost = this.GetRequestBodyForPost(inputs, text);
				this.TraceCookie(session);
				session.BeginPost(TestId.E4ePost, url, requestBodyForPost, "multipart/form-data; boundary=" + text, delegate(IAsyncResult r)
				{
					try
					{
						Exception exception;
						session.EndPost<bool>(r, new HttpStatusCode[]
						{
							HttpStatusCode.Found
						}, delegate(HttpWebResponseWrapper response)
						{
							this.Result.StateAttribute10 = (double)response.StatusCode;
							bool result;
							try
							{
								this.Result.StateAttribute11 = response.Headers.Get("X-CalculatedBETarget");
								this.Result.StateAttribute12 = response.Headers.Get("X-FEServer");
								this.Result.StateAttribute13 = response.Headers.Get("X-BEServer");
								this.Result.StateAttribute14 = response.Headers.Get("X-DiagInfo");
								string text2 = response.Headers.Get("Location");
								this.Result.StateAttribute25 = text2;
								itemId = HttpUtility.HtmlDecode(HttpUtility.ParseQueryString(text2.Split(new char[]
								{
									'?'
								})[1]).Get("itemid"));
								if (string.IsNullOrEmpty(itemId))
								{
									exception = new Exception("Failed to parse ItemId.");
									this.Result.StateAttribute24 = response.Body;
									result = false;
								}
								else
								{
									result = true;
								}
							}
							catch (Exception exception2)
							{
								exception = exception2;
								this.Result.StateAttribute24 = response.Body;
								result = false;
							}
							return result;
						});
					}
					catch (Exception exception)
					{
						Exception exception = exception;
					}
					finally
					{
						e.Set();
					}
				}, null);
				e.WaitOne();
				if (exception != null)
				{
					this.SetErrorTypeAndThrowException("PostMessage failed.", exception);
				}
				itemId2 = itemId;
			}
			return itemId2;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00017818 File Offset: 0x00015A18
		private void VerifyOTPFlow(CancellationToken cancellationToken, string url, Dictionary<string, string> inputs, string messageSubject, string messageBody, string accountUpn)
		{
			IRequestAdapter requestAdapter = new RequestAdapter();
			IExceptionAnalyzer exceptionAnalyzer = new E4eExceptionAnalyzer(new Dictionary<string, RequestTarget>());
			IResponseTracker responseTracker = new ResponseTracker();
			HttpSession session = new HttpSession(requestAdapter, exceptionAnalyzer, responseTracker);
			Uri uri = new Uri(url);
			string absoluteUri = new UriBuilder(uri)
			{
				Query = uri.Query + "&otp=true"
			}.Uri.AbsoluteUri;
			this.TraceDebug("VerifyOTPFlow", absoluteUri);
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			string text = Guid.NewGuid().ToString();
			RequestBody requestBody = this.GetRequestBodyForPost(inputs, text);
			string contentType = "multipart/form-data; boundary=" + text;
			string expectedPage = "OTPSigninPage.aspx";
			this.BeginPostAndFollowRedirections(session, absoluteUri, requestBody, contentType, expectedPage, out empty, out empty2, out empty3);
			base.Result.StateAttribute5 = string.Format("{0}::{1}::{2}", base.Result.StateAttribute5, empty2, empty3);
			string passcodeAndDeleteMessage = this.GetPasscodeAndDeleteMessage(accountUpn, empty3);
			if (string.IsNullOrWhiteSpace(passcodeAndDeleteMessage))
			{
				Exception innerException = new Exception(string.Format("Unable to get passcode. AccountUPN: {0}. OTPMessageId: {1}.", accountUpn, empty3));
				this.SetErrorTypeAndThrowException("VerifyOTPFlow failed", innerException);
			}
			requestBody = RequestBody.Format("__VIEWSTATE=&passcode=" + passcodeAndDeleteMessage, new object[0]);
			contentType = "application/x-www-form-urlencoded";
			expectedPage = "default.aspx";
			this.BeginPostAndFollowRedirections(session, empty, requestBody, contentType, expectedPage, out empty, out empty2, out empty3);
			this.VerifyLoggedIn(session, cancellationToken, empty2, accountUpn, messageSubject, messageBody);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00017A40 File Offset: 0x00015C40
		private string GetPage(HttpSession session, string url)
		{
			E4ePortalProbe.<>c__DisplayClass17 CS$<>8__locals1 = new E4ePortalProbe.<>c__DisplayClass17();
			CS$<>8__locals1.session = session;
			CS$<>8__locals1.<>4__this = this;
			this.TraceDebug("GetPage", url);
			CS$<>8__locals1.responseBody = null;
			CS$<>8__locals1.exception = null;
			string responseBody;
			using (ManualResetEvent e = new ManualResetEvent(false))
			{
				this.TraceCookie(CS$<>8__locals1.session);
				CS$<>8__locals1.session.BeginGet(TestId.E4ePageRequest, url, delegate(IAsyncResult r)
				{
					try
					{
						CS$<>8__locals1.session.EndGet<bool>(r, delegate(HttpWebResponseWrapper response)
						{
							CS$<>8__locals1.<>4__this.Result.StateAttribute10 = (double)response.StatusCode;
							CS$<>8__locals1.responseBody = response.Body;
							return true;
						});
					}
					catch (Exception exception)
					{
						CS$<>8__locals1.exception = exception;
					}
					finally
					{
						e.Set();
					}
				}, null);
				e.WaitOne();
				if (CS$<>8__locals1.exception != null)
				{
					this.SetErrorTypeAndThrowException("GetPage failed.", CS$<>8__locals1.exception);
				}
				responseBody = CS$<>8__locals1.responseBody;
			}
			return responseBody;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00017BCC File Offset: 0x00015DCC
		private string EwsGetItem(HttpSession session, string itemId)
		{
			this.TraceDebug("EwsGetItem", itemId);
			string text = "GetItem";
			RequestBody body = RequestBody.Format("{{\"__type\":\"GetItemJsonRequest:#Exchange\",\"Header\":{{\"__type\":\"JsonRequestHeaders:#Exchange\",\"RequestServerVersion\":\"Exchange2012\",\"TimeZoneContext\":{{\"__type\":\"TimeZoneContext:#Exchange\",\"TimeZoneDefinition\":{{\"__type\":\"TimeZoneDefinitionType:#Exchange\",\"Id\":\"UTC\"}}}}}},\"Body\":{{\"__type\":\"GetItemRequest:#Exchange\",\"ItemShape\":{{\"__type\":\"ItemResponseShape:#Exchange\",\"BaseShape\":\"IdOnly\",\"FilterHtmlContent\":false,\"BlockExternalImagesIfSenderUntrusted\":false,\"AddBlankTargetToLinks\":true,\"ClientSupportsIrm\":true,\"InlineImageUrlTemplate\":\"data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAEALAAAAAABAAEAAAIBTAA7\",\"MaximumBodySize\":2097152,\"InlineImageUrlOnLoadTemplate\":\"InlineImageLoader.GetLoader().Load(this)\",\"InlineImageCustomDataTemplate\":\"{{id}}\"}},\"ItemIds\":[{{\"__type\":\"ItemId:#Exchange\",\"Id\":\"{0}\"}}],\"ShapeName\":\"ItemNormalizedBody\"}}}}", new object[]
			{
				this.EscapeJsonString(itemId)
			});
			string responseBody2;
			using (ManualResetEvent e = new ManualResetEvent(false))
			{
				string responseBody = null;
				Exception exception = null;
				this.TraceCookie(session);
				session.BeginPost(TestId.E4eEwsCall, new Uri(this.EndpointUrl, "/encryption/service.svc?action=").ToString() + text, body, "application/json; charset=utf-8", new Dictionary<string, string>
				{
					{
						"Action",
						text
					},
					{
						"X-E4E-CANARY",
						session.CookieContainer.GetCookies(this.EndpointUrl)["X-E4E-CANARY"].Value
					}
				}, delegate(IAsyncResult r)
				{
					try
					{
						session.EndPost<bool>(r, delegate(HttpWebResponseWrapper response)
						{
							this.Result.StateAttribute10 = (double)response.StatusCode;
							responseBody = response.Body;
							return true;
						});
					}
					catch (Exception exception)
					{
						exception = exception;
					}
					finally
					{
						e.Set();
					}
				}, null);
				e.WaitOne();
				if (exception != null)
				{
					this.SetErrorTypeAndThrowException("EwsGetItem failed.", exception);
				}
				responseBody2 = responseBody;
			}
			return responseBody2;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00017D40 File Offset: 0x00015F40
		private string EscapeJsonString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder(s.Length);
			int i = 0;
			while (i < s.Length)
			{
				char c = s[i];
				char c2 = c;
				if (c2 <= '"')
				{
					switch (c2)
					{
					case '\b':
						stringBuilder.Append("\\b");
						break;
					case '\t':
						stringBuilder.Append("\\t");
						break;
					case '\n':
						stringBuilder.Append("\\n");
						break;
					case '\v':
						goto IL_D6;
					case '\f':
						stringBuilder.Append("\\f");
						break;
					case '\r':
						stringBuilder.Append("\\r");
						break;
					default:
						if (c2 != '"')
						{
							goto IL_D6;
						}
						stringBuilder.Append("\\\"");
						break;
					}
				}
				else if (c2 != '/')
				{
					if (c2 != '\\')
					{
						goto IL_D6;
					}
					stringBuilder.Append("\\\\");
				}
				else
				{
					stringBuilder.Append("\\/");
				}
				IL_106:
				i++;
				continue;
				IL_D6:
				if (c < ' ')
				{
					stringBuilder.Append("\\u" + string.Format("{0:X4}", (int)c));
					goto IL_106;
				}
				stringBuilder.Append(c);
				goto IL_106;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00017E6C File Offset: 0x0001606C
		private void TraceDebug(string format, params object[] args)
		{
			string arg = string.Format(format, args);
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("[{0}] ", arg);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00017EA2 File Offset: 0x000160A2
		private void TraceDebug(string message, string additionalMessage)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("[{0}] ", message);
			base.Result.StateAttribute1 = additionalMessage;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00017ED4 File Offset: 0x000160D4
		private void TraceCookie(HttpSession session)
		{
			string cookieHeader = session.CookieContainer.GetCookieHeader(this.EndpointUrl);
			base.Result.StateAttribute23 = cookieHeader;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00017EFF File Offset: 0x000160FF
		private void CheckCancellation(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				base.Result.Error = "Timeout";
				throw new OperationCanceledException();
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00017F20 File Offset: 0x00016120
		private void SetErrorTypeAndThrowException(string errorType, Exception innerException = null)
		{
			base.Result.Error = errorType;
			throw new Exception(errorType, innerException);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00017F38 File Offset: 0x00016138
		private void VerifyLoggedIn(HttpSession session, CancellationToken cancellationToken, string itemId, string accountUpn, string messageSubject, string messageBody)
		{
			this.CheckCancellation(cancellationToken);
			string page = this.GetPage(session, new Uri(this.EndpointUrl, "/encryption/default.aspx?wa=wsignin1.0&itemid=").ToString() + HttpUtility.UrlEncode(itemId));
			this.VerifyStringContains("Test default.aspx", page, new string[]
			{
				"e4eview.aspx",
				"_mailFrame",
				itemId
			});
			this.CheckCancellation(cancellationToken);
			string page2 = this.GetPage(session, new Uri(this.EndpointUrl, "/encryption/E4EView.aspx?loc=en-US&itemid=").ToString() + HttpUtility.UrlEncode(itemId));
			this.VerifyStringContains("Test e4eview.html", page2, new string[]
			{
				"startUpE4e()",
				itemId
			});
			this.CheckCancellation(cancellationToken);
			string s = this.EwsGetItem(session, itemId);
			this.VerifyStringContains("Test GetItem response", s, new string[]
			{
				accountUpn,
				messageSubject,
				messageBody
			});
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00018028 File Offset: 0x00016228
		private string GetPasscodeAndDeleteMessage(string accountUpn, string otpMessageId)
		{
			string result = string.Empty;
			using (MailboxSession mailboxSession = SearchStoreHelper.GetMailboxSession(accountUpn, false, "Monitoring"))
			{
				using (Folder inboxFolder = SearchStoreHelper.GetInboxFolder(mailboxSession))
				{
					int num = 0;
					VersionedId messageByInternetMessageId;
					do
					{
						ExDateTime exDateTime;
						messageByInternetMessageId = SearchStoreHelper.GetMessageByInternetMessageId(inboxFolder, otpMessageId, out exDateTime);
						num++;
						if (messageByInternetMessageId == null)
						{
							Thread.Sleep(TimeSpan.FromSeconds(15.0));
						}
					}
					while (messageByInternetMessageId == null && num < 20);
					if (messageByInternetMessageId != null)
					{
						StoreObjectId objectId = messageByInternetMessageId.ObjectId;
						MessageItem messageItem = Item.BindAsMessage(mailboxSession, objectId);
						PropertyDefinition[] properties = new PropertyDefinition[]
						{
							ItemSchema.TextBody
						};
						messageItem.Load(properties);
						result = this.GetPasscode(messageItem);
						mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
						{
							messageItem.Id
						});
					}
				}
			}
			return result;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00018118 File Offset: 0x00016318
		private string GetPasscode(MessageItem messageItem)
		{
			using (TextReader textReader = messageItem.Body.OpenTextReader(BodyFormat.TextHtml))
			{
				string text;
				while ((text = textReader.ReadLine()) != null)
				{
					int num = text.IndexOf("Passcode:", StringComparison.OrdinalIgnoreCase);
					if (num >= 0)
					{
						text = text.Substring(num);
						string text2 = new string((from c in text
						where char.IsDigit(c)
						select c).ToArray<char>());
						if (text2.Length > 0)
						{
							return text2;
						}
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000181B8 File Offset: 0x000163B8
		private RequestBody GetRequestBodyForPost(Dictionary<string, string> inputs, string boundary)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<object> list = new List<object>();
			foreach (KeyValuePair<string, string> keyValuePair in inputs)
			{
				stringBuilder.AppendFormat("--{0}", boundary);
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"", keyValuePair.Key);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("{" + list.Count + "}");
				list.Add(keyValuePair.Value);
			}
			stringBuilder.AppendFormat("--{0}--", boundary);
			stringBuilder.AppendLine();
			return RequestBody.Format(stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00018478 File Offset: 0x00016678
		private void BeginPostAndFollowRedirections(HttpSession session, string url, RequestBody requestBody, string contentType, string expectedPage, out string finalUrl, out string itemId, out string otpMessageId)
		{
			E4ePortalProbe.<>c__DisplayClass2b CS$<>8__locals1 = new E4ePortalProbe.<>c__DisplayClass2b();
			CS$<>8__locals1.session = session;
			CS$<>8__locals1.url = url;
			CS$<>8__locals1.expectedPage = expectedPage;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.exception = null;
			CS$<>8__locals1.finalUrlTemp = string.Empty;
			CS$<>8__locals1.itemIdTemp = string.Empty;
			CS$<>8__locals1.otpMessageIdTemp = string.Empty;
			using (ManualResetEvent e = new ManualResetEvent(false))
			{
				this.TraceCookie(CS$<>8__locals1.session);
				CS$<>8__locals1.session.BeginPostFollowingRedirections(TestId.E4ePost, CS$<>8__locals1.url, requestBody, contentType, null, RedirectionOptions.FollowUntilNo302, delegate(IAsyncResult r)
				{
					try
					{
						CS$<>8__locals1.session.EndPost<bool>(r, new HttpStatusCode[]
						{
							HttpStatusCode.OK
						}, delegate(HttpWebResponseWrapper response)
						{
							string value = string.Format("<meta name=\"e4ePage\" content=\"{0}\"/>", CS$<>8__locals1.expectedPage);
							if (response == null || string.IsNullOrEmpty(response.Body))
							{
								CS$<>8__locals1.exception = new Exception(string.Format("Response body is null for request: {0}", CS$<>8__locals1.url));
								return false;
							}
							CS$<>8__locals1.<>4__this.Result.StateAttribute22 = response.Body;
							if (!response.Body.Contains(value))
							{
								CS$<>8__locals1.exception = new Exception(string.Format("Response body doesn't contain expectedPage token: {0}", CS$<>8__locals1.expectedPage));
								return false;
							}
							CS$<>8__locals1.finalUrlTemp = response.Request.RequestUri.ToString();
							NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(CS$<>8__locals1.finalUrlTemp.Split(new char[]
							{
								'?'
							})[1]);
							CS$<>8__locals1.itemIdTemp = HttpUtility.HtmlDecode(nameValueCollection.Get("itemid"));
							CS$<>8__locals1.otpMessageIdTemp = nameValueCollection.Get("OTPMessageId");
							if (!string.IsNullOrWhiteSpace(CS$<>8__locals1.otpMessageIdTemp))
							{
								CS$<>8__locals1.otpMessageIdTemp = string.Format("<{0}>", HttpUtility.UrlDecode(CS$<>8__locals1.otpMessageIdTemp));
							}
							return true;
						});
					}
					catch (Exception exception)
					{
						CS$<>8__locals1.exception = exception;
					}
					finally
					{
						e.Set();
					}
				}, null);
				e.WaitOne();
				if (CS$<>8__locals1.exception != null)
				{
					this.SetErrorTypeAndThrowException("VerifyOTPFlow failed", CS$<>8__locals1.exception);
				}
			}
			finalUrl = CS$<>8__locals1.finalUrlTemp;
			itemId = CS$<>8__locals1.itemIdTemp;
			otpMessageId = CS$<>8__locals1.otpMessageIdTemp;
		}
	}
}
