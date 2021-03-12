using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000019 RID: 25
	internal sealed class Pop3ResponseFactory : ResponseFactory
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004160 File Offset: 0x00002360
		internal Pop3ResponseFactory(ProtocolSession session) : base(session)
		{
			base.ProtocolUser = new Pop3ProtocolUser(session);
			base.SkipAuthOnCafeEnabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Pop.SkipAuthOnCafe.Enabled;
			base.UseSamAccountNameAsUsername = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Pop.UseSamAccountNameAsUsername.Enabled;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000041D6 File Offset: 0x000023D6
		protected override string ClientStringForMailboxSession
		{
			get
			{
				return "Client=POP3/IMAP4;Protocol=POP3";
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000041E0 File Offset: 0x000023E0
		public bool HasAllMessagesBeenRetrieved
		{
			get
			{
				if (this.hasAllMessagesBeenRetrieved)
				{
					return true;
				}
				if (!this.messagesLoaded)
				{
					return false;
				}
				foreach (Pop3Message pop3Message in this.Messages)
				{
					if (!pop3Message.HasSize)
					{
						return false;
					}
				}
				this.hasAllMessagesBeenRetrieved = true;
				return true;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004258 File Offset: 0x00002458
		public DataAccessView DataAccessView
		{
			get
			{
				return this.dataAccessView;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004260 File Offset: 0x00002460
		public override string FirstAuthenticateResponse
		{
			get
			{
				return "+ ";
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004267 File Offset: 0x00002467
		public override bool IsAuthenticated
		{
			get
			{
				return this.SessionState == Pop3State.Authenticated;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004272 File Offset: 0x00002472
		public override bool IsDisconnected
		{
			get
			{
				return this.sessionState == Pop3State.Disconnected;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000427E File Offset: 0x0000247E
		public override string TimeoutErrorString
		{
			get
			{
				return "-ERR Connection is closed. 12";
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004285 File Offset: 0x00002485
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000428D File Offset: 0x0000248D
		public Pop3State SessionState
		{
			get
			{
				return this.sessionState;
			}
			set
			{
				this.sessionState = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004296 File Offset: 0x00002496
		public List<Pop3Message> Messages
		{
			get
			{
				if (!this.messagesLoaded)
				{
					this.CreateListOfMessages();
				}
				return this.messages;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000042AC File Offset: 0x000024AC
		internal bool NeedToLoadLegacyUIDs
		{
			get
			{
				return this.needToLoadLegacyUIDs;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000042B4 File Offset: 0x000024B4
		protected override ExEventLog.EventTuple NoDefaultAcceptedDomainFoundEventTuple
		{
			get
			{
				return Pop3EventLogConstants.Tuple_NoDefaultAcceptedDomainFound;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000042BB File Offset: 0x000024BB
		protected override BudgetType BudgetType
		{
			get
			{
				return BudgetType.Pop;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000042BE File Offset: 0x000024BE
		public override string AuthenticationFailureString
		{
			get
			{
				return "Authentication failure: unknown user name or bad password.";
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000042C5 File Offset: 0x000024C5
		protected override string AccountInvalidatedString
		{
			get
			{
				return "-ERR Session invalidated - {0}";
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000042CC File Offset: 0x000024CC
		public override ProtocolRequest GenerateRequest(byte[] buf, int offset, int size)
		{
			int num;
			int nextToken = BaseSession.GetNextToken(buf, offset, size, out num);
			if (nextToken != offset)
			{
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.BeginCommand(Pop3ResponseFactory.InvalidBuf);
					base.Session.LightLogSession.Parameters = Encoding.ASCII.GetString(buf, offset, size);
				}
				return new Pop3RequestInvalid(this, "Protocol error. 19");
			}
			int num2 = size - (num - nextToken) - 1;
			if (BaseSession.CompareArg(Pop3Session.PassBuf, buf, nextToken, num - nextToken))
			{
				if (base.Session.LightLogSession != null)
				{
					base.Session.LightLogSession.BeginCommand(Pop3Session.PassBuf);
				}
				return new Pop3RequestPass(this, buf, num + 1, num2);
			}
			string text = null;
			if (num2 > 0)
			{
				text = Encoding.ASCII.GetString(buf, num + 1, num2);
			}
			else if (num2 < 0)
			{
				num = offset + size;
			}
			ProtocolRequest protocolRequest = null;
			char c = (char)BaseSession.LowerC[(int)buf[offset]];
			byte[] array = null;
			char c2 = c;
			switch (c2)
			{
			case 'a':
				if (BaseSession.CompareArg(Pop3Session.AuthBuf, buf, nextToken, num - nextToken))
				{
					array = Pop3Session.AuthBuf;
					protocolRequest = new Pop3RequestAuth(this, text);
				}
				break;
			case 'b':
				break;
			case 'c':
				if (BaseSession.CompareArg(Pop3Session.CapaBuf, buf, nextToken, num - nextToken))
				{
					array = Pop3Session.CapaBuf;
					protocolRequest = new Pop3RequestCapa(this, text);
				}
				break;
			case 'd':
				if (BaseSession.CompareArg(Pop3Session.DeleBuf, buf, nextToken, num - nextToken))
				{
					array = Pop3Session.DeleBuf;
					protocolRequest = new Pop3RequestDele(this, text);
				}
				break;
			default:
				switch (c2)
				{
				case 'l':
					if (BaseSession.CompareArg(Pop3Session.ListBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.ListBuf;
						protocolRequest = new Pop3RequestList(this, text);
					}
					break;
				case 'n':
					if (BaseSession.CompareArg(Pop3Session.NoopBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.NoopBuf;
						protocolRequest = new Pop3RequestNoop(this, text);
					}
					break;
				case 'q':
					if (BaseSession.CompareArg(Pop3Session.QuitBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.QuitBuf;
						protocolRequest = new Pop3RequestQuit(this, text);
					}
					break;
				case 'r':
					if (BaseSession.CompareArg(Pop3Session.RetrBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.RetrBuf;
						protocolRequest = new Pop3RequestRetr(this, text);
					}
					else if (BaseSession.CompareArg(Pop3Session.RsetBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.RsetBuf;
						protocolRequest = new Pop3RequestRset(this, text);
					}
					break;
				case 's':
					if (BaseSession.CompareArg(Pop3Session.StatBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.StatBuf;
						protocolRequest = new Pop3RequestStat(this, text);
					}
					else if (BaseSession.CompareArg(Pop3Session.StlsBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.StlsBuf;
						protocolRequest = new Pop3RequestStls(this, text);
					}
					break;
				case 't':
					if (BaseSession.CompareArg(Pop3Session.TopBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.TopBuf;
						protocolRequest = new Pop3RequestTop(this, text);
					}
					break;
				case 'u':
					if (BaseSession.CompareArg(Pop3Session.UserBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.UserBuf;
						protocolRequest = new Pop3RequestUser(this, text);
					}
					else if (BaseSession.CompareArg(Pop3Session.UidlBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.UidlBuf;
						protocolRequest = new Pop3RequestUidl(this, text);
					}
					break;
				case 'x':
					if (BaseSession.CompareArg(Pop3Session.XprxBuf, buf, nextToken, num - nextToken))
					{
						array = Pop3Session.XprxBuf;
						protocolRequest = new Pop3RequestXprx(this, text);
					}
					break;
				}
				break;
			}
			if (protocolRequest == null)
			{
				protocolRequest = new Pop3RequestInvalid(this, "Protocol error. 19");
			}
			if (protocolRequest is Pop3RequestInvalid)
			{
				base.CommandName = "InvalidCommand";
				base.Parameters = Encoding.ASCII.GetString(buf, offset, size);
			}
			else
			{
				base.CommandName = Encoding.ASCII.GetString(array);
				base.Parameters = text;
			}
			if (base.Session.LightLogSession != null)
			{
				if (protocolRequest is Pop3RequestInvalid)
				{
					base.Session.LightLogSession.BeginCommand(Pop3ResponseFactory.InvalidBuf);
					base.Session.LightLogSession.Parameters = Encoding.ASCII.GetString(buf, offset, size);
				}
				else
				{
					base.Session.LightLogSession.BeginCommand(array);
					base.Session.LightLogSession.Parameters = text;
				}
			}
			return protocolRequest;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000046EC File Offset: 0x000028EC
		public override ProtocolResponse ProcessInvalidState(ProtocolRequest request)
		{
			string text = (this.SessionState == Pop3State.AuthenticatedButFailed) ? "User is authenticated but not connected." : "Command is not valid in this state.";
			ProtocolResponse protocolResponse;
			if ((base.InvalidCommands += 1U) > 2U)
			{
				text += "\r\nConnection is closed. 21";
				protocolResponse = new Pop3Response(request, Pop3Response.Type.err, text);
				protocolResponse.IsDisconnectResponse = true;
			}
			else
			{
				protocolResponse = new Pop3Response(request, Pop3Response.Type.err, text);
			}
			return protocolResponse;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000474C File Offset: 0x0000294C
		public override ProtocolResponse ProcessParseError(ProtocolRequest request)
		{
			string text;
			switch (request.ParseResult)
			{
			case ParseResult.invalidArgument:
				text = "Protocol error. 14";
				break;
			case ParseResult.invalidNumberOfArguments:
				text = "Protocol error. 16";
				break;
			default:
				throw new InvalidOperationException("ProcessParseError is called when there is no error");
			}
			ProtocolResponse protocolResponse;
			if ((base.InvalidCommands += 1U) > 2U)
			{
				text += "\r\nConnection is closed. 21";
				protocolResponse = new Pop3Response(request, Pop3Response.Type.err, text);
				protocolResponse.IsDisconnectResponse = true;
			}
			else
			{
				protocolResponse = new Pop3Response(request, Pop3Response.Type.err, text);
			}
			return protocolResponse;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000047CC File Offset: 0x000029CC
		public override ProtocolResponse CommandIsNotAllASCII(byte[] buf, int offset, int size)
		{
			string text = "Protocol error. 17";
			ProtocolResponse protocolResponse;
			if ((base.InvalidCommands += 1U) > 2U)
			{
				text += "\r\nConnection is closed. 21";
				protocolResponse = new Pop3Response(Pop3Response.Type.err, text);
				protocolResponse.IsDisconnectResponse = true;
			}
			else
			{
				protocolResponse = new Pop3Response(Pop3Response.Type.err, text);
			}
			return protocolResponse;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004819 File Offset: 0x00002A19
		public override ProtocolResponse ProcessException(ProtocolRequest request, Exception exception)
		{
			return this.ProcessException(request, exception, "Server Unavailable. 21");
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004828 File Offset: 0x00002A28
		public override ProtocolResponse ProcessException(ProtocolRequest request, Exception exception, string responseString)
		{
			ProtocolBaseServices.SessionTracer.TraceError<Exception>(base.Session.SessionId, "Exception caught:{0}", exception);
			if (exception is StorageTransientException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(2);
			}
			else if (exception is StoragePermanentException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(3);
			}
			else if (exception is ADTransientException || exception is ServiceDiscoveryTransientException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(4);
			}
			else if (exception is ServiceDiscoveryPermanentException || exception is ADOperationException || exception is ADExternalException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(5);
			}
			else if (exception is ConnectionFailedTransientException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(0);
			}
			else if (exception is MailboxOfflineException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(1);
			}
			else if (exception is TransientException)
			{
				RatePerfCounters.IncrementExceptionPerfCounter(6);
			}
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.ExceptionCaught = exception;
			}
			ProtocolResponse protocolResponse;
			if ((base.InvalidCommands += 1U) > 2U)
			{
				responseString += "\r\nConnection is closed. 21";
				protocolResponse = new Pop3Response(request, Pop3Response.Type.err, responseString);
				protocolResponse.IsDisconnectResponse = true;
			}
			else if (exception is ConnectionFailedTransientException || exception is MailboxInSiteFailoverException || exception is MailboxCrossSiteFailoverException || exception is ConnectionFailedPermanentException)
			{
				protocolResponse = new Pop3Response(request, Pop3Response.Type.err, responseString + "\r\n-ERR Connection is closed. 22");
				protocolResponse.IsDisconnectResponse = true;
			}
			else
			{
				protocolResponse = new Pop3Response(request, Pop3Response.Type.err, responseString);
			}
			base.Session.SetDiagnosticValue(ConditionalHandlerSchema.Exception, exception.ToString());
			base.Session.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, (protocolResponse as Pop3Response).ResponseType);
			base.AddExceptionToCache(exception);
			return protocolResponse;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000049AC File Offset: 0x00002BAC
		public override ProtocolResponse AuthenticationDone(ProtocolRequest authenticateRequest, ResponseFactory.AuthenticationResult authenticationResult)
		{
			if (authenticateRequest == null)
			{
				return null;
			}
			if (authenticationResult == ResponseFactory.AuthenticationResult.success)
			{
				this.sessionState = Pop3State.Authenticated;
			}
			else if (authenticationResult == ResponseFactory.AuthenticationResult.authenticatedButFailed)
			{
				this.sessionState = Pop3State.AuthenticatedButFailed;
			}
			else
			{
				this.sessionState = Pop3State.Nonauthenticated;
			}
			return ((Pop3RequestAuth)authenticateRequest).AuthenticationDone(base.LoginAttempts += 1U, authenticationResult);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000049FC File Offset: 0x00002BFC
		public override bool DoProxyConnect(byte[] buf, int offset, int size, ProxySession proxySession)
		{
			if (base.Session.Disposed)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Incoming session is disposed, nothing to do.");
				proxySession.State = ProxySession.ProxyState.failed;
				return false;
			}
			if (proxySession.Disposed)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Outgoing proxy session is disposed, nothing to do.");
				proxySession.State = ProxySession.ProxyState.failed;
				return false;
			}
			IProxyLogin proxyLogin = (IProxyLogin)base.IncompleteRequest;
			if (proxyLogin == null)
			{
				proxySession.State = ProxySession.ProxyState.failed;
				return false;
			}
			if (buf != null)
			{
				base.TraceProxyResponse(buf, offset, size);
				if (proxySession.State == ProxySession.ProxyState.waitCapaOk)
				{
					string @string = Encoding.ASCII.GetString(buf, offset, size);
					if (@string != null)
					{
						if (@string.Contains("CLIENTACCESSRULES"))
						{
							this.clientAccessRulesSupportedByTargetServer = true;
						}
						if (@string.Equals("."))
						{
							proxySession.State = ProxySession.ProxyState.sendAuthPlain;
						}
					}
				}
				else if (proxySession.State == ProxySession.ProxyState.sendAuthBlob)
				{
					if (!BaseSession.CompareArg(Pop3ResponseFactory.PlusBuf, buf, offset, size))
					{
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
				}
				else
				{
					int num;
					int nextToken = BaseSession.GetNextToken(buf, offset, size, out num);
					if (nextToken != offset)
					{
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
					if (proxySession.State == ProxySession.ProxyState.authenticated && string.IsNullOrEmpty(proxyLogin.AuthenticationError))
					{
						string string2 = Encoding.ASCII.GetString(buf, offset, size);
						Match match = ResponseFactory.AuthErrorParser.Match(string2);
						if (match.Success)
						{
							if (match.Groups["authError"].Success)
							{
								proxyLogin.AuthenticationError = match.Groups["authError"].Value;
							}
							else
							{
								proxyLogin.AuthenticationError = null;
							}
							if (match.Groups["proxy"].Success)
							{
								IProxyLogin proxyLogin2 = proxyLogin;
								proxyLogin2.ProxyDestination = proxyLogin2.ProxyDestination + "," + match.Groups["proxy"].Value;
							}
						}
					}
					if (!BaseSession.CompareArg(Pop3Response.OkBuf, buf, nextToken, num - nextToken))
					{
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
				}
			}
			switch (proxySession.State)
			{
			case ProxySession.ProxyState.initTls:
			{
				string s = "STLS\r\n";
				base.Session.LogInformation("Sending STLS to the BE server.", new object[0]);
				if (!proxySession.SendToClient(new StringResponseItem(s)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.startTls;
				return false;
			}
			case ProxySession.ProxyState.startTls:
				base.Session.LogInformation("Negotiating TLS against BE server.", new object[0]);
				proxySession.Connection.BeginNegotiateTlsAsClient(new AsyncCallback(base.ProxyConnectionEncryptionComplete), proxySession);
				proxySession.State = ProxySession.ProxyState.initialization;
				return false;
			case ProxySession.ProxyState.initialization:
				if (base.Session.ProxyToLegacyServer)
				{
					string text = proxyLogin.UserName;
					if (!string.IsNullOrEmpty(base.Mailbox))
					{
						text = text + "/" + base.Mailbox;
					}
					string s2 = string.Format("USER {0}\r\n", text.Replace("\\", "\\\\"));
					base.Session.LogInformation("Sending USER {0} to the BE server.", new object[]
					{
						text
					});
					if (!proxySession.SendToClient(new StringResponseItem(s2)))
					{
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
					proxySession.State = ProxySession.ProxyState.user;
					return false;
				}
				else
				{
					base.Session.LogInformation("Sending CAPA to the BE server.", new object[0]);
					if (!proxySession.SendToClient(new StringResponseItem("CAPA\r\n")))
					{
						proxySession.State = ProxySession.ProxyState.failed;
						return false;
					}
					proxySession.State = ProxySession.ProxyState.waitCapaOk;
					return false;
				}
				break;
			case ProxySession.ProxyState.user:
			{
				base.Session.LogInformation("Sending PASS <password> to the BE server.", new object[0]);
				BufferBuilder bufferBuilder = new BufferBuilder(32);
				bufferBuilder.Append(Pop3Session.PassBuf);
				bufferBuilder.Append(32);
				try
				{
					bufferBuilder.Append(proxyLogin.Password);
				}
				catch (ArgumentException ex)
				{
					bufferBuilder.Reset();
					ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, ex.ToString());
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				bufferBuilder.Append(Strings.CRLFByteArray);
				bufferBuilder.RemoveUnusedBufferSpace();
				if (!proxySession.SendToClient(new SecureBufferResponseItem(bufferBuilder.GetBuffer())))
				{
					bufferBuilder.Reset();
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.authenticated;
				return false;
			}
			case ProxySession.ProxyState.waitCapaOk:
				return false;
			case ProxySession.ProxyState.sendAuthPlain:
				base.Session.LogInformation("Sending AUTH PLAIN to the BE server.", new object[0]);
				if (!proxySession.SendToClient(new BufferResponseItem(Pop3ResponseFactory.AuthPlainCommand)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.sendAuthBlob;
				return false;
			case ProxySession.ProxyState.sendAuthBlob:
			{
				byte[] plainAuthBlob = base.GetPlainAuthBlob();
				if (plainAuthBlob == null)
				{
					base.Session.LogInformation("No AUTH blob to send.", new object[0]);
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				base.Session.LogInformation("Sending AUTH blob to the BE server.", new object[0]);
				if (!proxySession.SendToClient(new SecureBufferResponseItem(plainAuthBlob)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				if (ProtocolBaseServices.GCCEnabledWithKeys)
				{
					proxySession.State = ProxySession.ProxyState.sendXproxy;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.authenticated;
				return false;
			}
			case ProxySession.ProxyState.authenticated:
				ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "Connection to BE server authenticated.");
				proxySession.State = ProxySession.ProxyState.completed;
				return true;
			case ProxySession.ProxyState.sendXproxy:
			{
				string text2 = string.Format("XPRX {0} {1} {2}\r\n", GccUtils.GetAuthStringForThisServer(), base.Session.RemoteEndPoint.Address, base.Session.LocalEndPoint.Address);
				base.Session.LogInformation("Sending {0} to the BE server.", new object[]
				{
					text2.Trim()
				});
				if (!proxySession.SendToClient(new StringResponseItem(text2)))
				{
					proxySession.State = ProxySession.ProxyState.failed;
					return false;
				}
				proxySession.State = ProxySession.ProxyState.authenticated;
				return false;
			}
			}
			ProtocolBaseServices.Assert(false, "This should never happen!", new object[0]);
			return false;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004FE8 File Offset: 0x000031E8
		public override void DoPostLoginTasks()
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004FEA File Offset: 0x000031EA
		public Pop3Message GetMessage(int idx)
		{
			if (this.Messages == null)
			{
				return null;
			}
			if (idx < 1 || idx > this.Messages.Count)
			{
				return null;
			}
			return this.Messages[idx - 1];
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005018 File Offset: 0x00003218
		internal ProtocolResponse DoConnect(Pop3RequestPass passRequest, SecureString password)
		{
			this.SessionState = Pop3State.Pass;
			ProtocolResponse protocolResponse = null;
			this.sessionState = Pop3State.Nonauthenticated;
			base.IncompleteRequest = passRequest;
			try
			{
				bool flag;
				if (base.TryToConnect(password, out flag))
				{
					this.SessionState = Pop3State.Authenticated;
					protocolResponse = new Pop3Response(passRequest, Pop3Response.Type.ok, "User successfully logged on.");
					base.DisconnectFromTheStore();
					base.Session.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, "OK");
				}
				else
				{
					this.sessionState = (flag ? Pop3State.AuthenticatedButFailed : Pop3State.Nonauthenticated);
					if (ResponseFactory.CheckOnlyAuthenticationStatusEnabled && flag && !ProtocolBaseServices.AuthErrorReportEnabled(base.UserName) && base.ProtocolUser.IsEnabled)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.Session.SessionId, "User {0} login succeeded but failed to create proxy connection.", base.UserName);
						protocolResponse = new Pop3Response(passRequest, Pop3Response.Type.ok, "User successfully logged on.");
					}
					else
					{
						ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.Session.SessionId, "Failed to login user {0}.", base.UserName);
						if ((ulong)(base.LoginAttempts += 1U) < (ulong)((long)base.Session.Server.MaxFailedLoginAttempts))
						{
							protocolResponse = new Pop3Response(passRequest, Pop3Response.Type.err, "Logon failure: unknown user name or bad password.");
						}
						else
						{
							protocolResponse = new Pop3Response(passRequest, Pop3Response.Type.err, "Logon failure: unknown user name or bad password.\r\nConnection is closed. 21");
							protocolResponse.IsDisconnectResponse = true;
						}
					}
					base.Session.SetDiagnosticValue(PopImapConditionalHandlerSchema.ResponseType, "Err");
					if (base.Session[ConditionalHandlerSchema.Exception] == null)
					{
						base.Session.SetDiagnosticValue(ConditionalHandlerSchema.Exception, "LoginFailed");
					}
				}
			}
			finally
			{
				base.IncompleteRequest = null;
			}
			return protocolResponse;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000051A8 File Offset: 0x000033A8
		internal int DeleteMarkedMessages()
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "DeleteMarkedMessages is called.");
			if (this.SessionState != Pop3State.Authenticated || this.DataAccessView == null)
			{
				return 0;
			}
			List<ProtocolMessage> list = new List<ProtocolMessage>(256);
			List<ProtocolMessage> list2 = new List<ProtocolMessage>(256);
			for (int i = 0; i < this.Messages.Count; i++)
			{
				if (this.Messages[i].IsRead)
				{
					list2.Add(this.Messages[i]);
				}
				if (this.Messages[i].IsDeleted)
				{
					list.Add(this.Messages[i]);
				}
			}
			StoreObjectId[] storeObjectIds = this.DataAccessView.GetStoreObjectIds(list);
			StoreObjectId[] storeObjectIds2 = this.DataAccessView.GetStoreObjectIds(list2);
			base.MarkAsRead(storeObjectIds2);
			base.DeleteMessages(storeObjectIds);
			return list.Count;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000528C File Offset: 0x0000348C
		internal void LoadLegacyUids()
		{
			if (!this.NeedToLoadLegacyUIDs)
			{
				return;
			}
			List<ProtocolMessage> list = new List<ProtocolMessage>(this.Messages.Count);
			if (!this.NeedToLoadLegacyUIDs)
			{
				return;
			}
			foreach (Pop3Message pop3Message in this.Messages)
			{
				if (pop3Message.NeedsLegacyUid)
				{
					list.Add(pop3Message);
				}
			}
			list.Sort();
			StoreObjectId[] storeObjectIds = this.DataAccessView.GetStoreObjectIds(list);
			if (storeObjectIds.Length != list.Count)
			{
				throw new LocalizedException(new LocalizedString("Unexpected length of storeObjectIds array"));
			}
			for (int i = 0; i < storeObjectIds.Length; i++)
			{
				this.AssignLegacyUid((Pop3Message)list[i], storeObjectIds[i]);
			}
			this.needToLoadLegacyUIDs = false;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005364 File Offset: 0x00003564
		internal void LoadLegacyUid(Pop3Message message)
		{
			if (!message.NeedsLegacyUid)
			{
				return;
			}
			this.AssignLegacyUid(message, message.Uid);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000537C File Offset: 0x0000357C
		protected override ProxySession NewProxySession(NetworkConnection connection)
		{
			return new Pop3ProxySession(this, connection);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005388 File Offset: 0x00003588
		protected override void ReloadStoreStates()
		{
			if (this.dataAccessView != null)
			{
				this.dataAccessView.Dispose();
				this.dataAccessView = null;
				using (Folder folder = Folder.Bind(base.Store, base.Store.GetDefaultFolderId(DefaultFolderType.Inbox), new PropertyDefinition[]
				{
					FolderSchema.PopImapConversionVersion
				}))
				{
					this.dataAccessView = new DataAccessView(this, folder);
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005400 File Offset: 0x00003600
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.dataAccessView != null)
				{
					this.dataAccessView.Dispose();
					this.dataAccessView = null;
				}
				if (this.fastQueryView != null)
				{
					this.fastQueryView.Dispose();
					this.fastQueryView = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005474 File Offset: 0x00003674
		protected override IEnumerable<EmailTransportService> GetProxyDestinations(ExchangePrincipal exchangePrincipal)
		{
			ServiceTopology serviceTopology = ProtocolBaseServices.IsMultiTenancyEnabled ? ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Pop3\\Pop3ResponseFactory.cs", "GetProxyDestinations", 1243) : ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Pop3\\Pop3ResponseFactory.cs", "GetProxyDestinations", 1243);
			return serviceTopology.FindAll<Pop3Service>(exchangePrincipal, ClientAccessType.Internal, (Pop3Service service) => ResponseFactory.CanProxyTo(service, exchangePrincipal), "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Pop3\\Pop3ResponseFactory.cs", "GetProxyDestinations", 1244).Cast<EmailTransportService>();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000054ED File Offset: 0x000036ED
		protected override int GetE15MbxProxyPort(string e15MbxFqdn)
		{
			return base.GetE15MbxProxyPort<Pop3AdConfiguration>(e15MbxFqdn, Pop3ResponseFactory.proxyPortForE15Mbx, false, "");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005501 File Offset: 0x00003701
		protected override int GetE15MbxProxyPort(string e15MbxFqdn, bool isCrossForest, string userDomain)
		{
			return base.GetE15MbxProxyPort<Pop3AdConfiguration>(e15MbxFqdn, Pop3ResponseFactory.proxyPortForE15Mbx, isCrossForest, userDomain);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005511 File Offset: 0x00003711
		private void AssignLegacyUid(Pop3Message message, StoreObjectId objectId)
		{
			if (objectId != null)
			{
				message.LegacyUid = base.Store.IdConverter.GetLegacyPopUid(objectId);
			}
			else
			{
				message.IsDeleted = true;
			}
			message.NeedsLegacyUid = false;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005540 File Offset: 0x00003740
		private void CreateListOfMessages()
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "CreateListOfMessages is called.");
			bool flag = false;
			try
			{
				if (!base.IsStoreConnected)
				{
					base.Store.Connect();
					flag = true;
				}
				this.needToLoadLegacyUIDs = false;
				bool flag2 = false;
				Pop3Server.FaultInjectionTracer.TraceTest<bool>(4163251517U, ref flag2);
				if (flag2)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(base.Session.SessionId, "ForceLegacyUid is set.");
				}
				using (Folder folder = Folder.Bind(base.Store, base.Store.GetDefaultFolderId(DefaultFolderType.Inbox), new PropertyDefinition[]
				{
					FolderSchema.PopImapConversionVersion
				}))
				{
					this.previousXsoVersion = ResponseFactory.GetPreviousXsoVersion(folder.TryGetProperty(FolderSchema.PopImapConversionVersion) as string);
					int itemCount = folder.ItemCount;
					if (itemCount > 0)
					{
						bool flag3 = ResponseFactory.IsXsoVersionChanged(this.previousXsoVersion);
						if (this.fastQueryView == null)
						{
							this.fastQueryView = new FastQueryView(this, folder, Pop3Message.GetFastQueryViewSortBys(this), Pop3Message.GetFastQueryViewProperties(this));
						}
						if (this.dataAccessView == null)
						{
							this.dataAccessView = new DataAccessView(this, folder);
						}
						this.messages = new List<Pop3Message>(itemCount);
						int num = 1;
						object[][] rows;
						do
						{
							rows = this.fastQueryView.TableView.GetRows(10000);
							for (int i = 0; i < rows.Length; i++)
							{
								object obj = rows[i][4];
								if ((obj is PropertyError || !(bool)obj) && !(rows[i][1] is PropertyError) && !(rows[i][0] is PropertyError))
								{
									object obj2 = rows[i][3];
									if (obj2 != null && !(obj2 is PropertyError) && (bool)obj2)
									{
										if (!flag3)
										{
											goto IL_1ED;
										}
										int imapId = (int)rows[i][0];
										this.dataAccessView.SetPoisonFlag(folder, imapId, false);
									}
									Pop3Message pop3Message = new Pop3Message(this, num++, rows[i]);
									this.messages.Add(pop3Message);
									if (flag2)
									{
										pop3Message.NeedsLegacyUid = true;
									}
									this.needToLoadLegacyUIDs |= pop3Message.NeedsLegacyUid;
								}
								IL_1ED:;
							}
						}
						while (rows.Length > 0);
						if (flag3)
						{
							ResponseFactory.RecordCurrentXsoVersion(folder);
						}
					}
					if (this.fastQueryView != null)
					{
						this.fastQueryView.Dispose();
						this.fastQueryView = null;
					}
					if (this.messages == null)
					{
						this.messages = new List<Pop3Message>();
					}
					this.messagesLoaded = true;
					ProtocolBaseServices.SessionTracer.TraceDebug<int>(base.Session.SessionId, "Found {0} items", this.messages.Count);
				}
			}
			finally
			{
				if (flag)
				{
					base.Store.Disconnect();
				}
			}
		}

		// Token: 0x04000053 RID: 83
		internal const string InvalidArgument = "Protocol error. 14";

		// Token: 0x04000054 RID: 84
		internal const string InvalidNumberOfArguments = "Protocol error. 16";

		// Token: 0x04000055 RID: 85
		internal const string CommandIsNotAllASCIIResponse = "Protocol error. 17";

		// Token: 0x04000056 RID: 86
		internal const string InvalidState = "Command is not valid in this state.";

		// Token: 0x04000057 RID: 87
		internal const string AuthenticatedButFailedState = "User is authenticated but not connected.";

		// Token: 0x04000058 RID: 88
		internal const string InvalidCommandResponse = "Protocol error. 19";

		// Token: 0x04000059 RID: 89
		internal const string InvalidCommandResponseLastTime = "Protocol error. 20";

		// Token: 0x0400005A RID: 90
		internal const string ResponseConnectionClosed = "\r\nConnection is closed. 21";

		// Token: 0x0400005B RID: 91
		internal const string TimeoutError = "-ERR Connection is closed. 12";

		// Token: 0x0400005C RID: 92
		internal const string ServerUnavailable = "Server Unavailable. 21";

		// Token: 0x0400005D RID: 93
		internal const string UnexpectedError = "Protocol error. 18";

		// Token: 0x0400005E RID: 94
		internal const string ResponseBye = "\r\n-ERR Connection is closed. 22";

		// Token: 0x0400005F RID: 95
		internal const string AccountInvalidatedMessage = "-ERR Session invalidated - {0}";

		// Token: 0x04000060 RID: 96
		private static readonly byte[] InvalidBuf = Encoding.ASCII.GetBytes("InvalidCommand");

		// Token: 0x04000061 RID: 97
		private static readonly byte[] AuthPlainCommand = Encoding.ASCII.GetBytes("AUTH PLAIN\r\n");

		// Token: 0x04000062 RID: 98
		private static readonly byte[] PlusBuf = Encoding.ASCII.GetBytes("+ ");

		// Token: 0x04000063 RID: 99
		private static MruDictionaryCache<string, int> proxyPortForE15Mbx = new MruDictionaryCache<string, int>(50, 5000, 1440);

		// Token: 0x04000064 RID: 100
		private bool messagesLoaded;

		// Token: 0x04000065 RID: 101
		private List<Pop3Message> messages;

		// Token: 0x04000066 RID: 102
		private Pop3State sessionState = Pop3State.Nonauthenticated;

		// Token: 0x04000067 RID: 103
		private bool hasAllMessagesBeenRetrieved;

		// Token: 0x04000068 RID: 104
		private int[] previousXsoVersion;

		// Token: 0x04000069 RID: 105
		private DataAccessView dataAccessView;

		// Token: 0x0400006A RID: 106
		private FastQueryView fastQueryView;

		// Token: 0x0400006B RID: 107
		private bool needToLoadLegacyUIDs = true;
	}
}
