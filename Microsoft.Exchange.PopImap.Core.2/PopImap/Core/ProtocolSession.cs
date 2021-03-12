using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000026 RID: 38
	internal abstract class ProtocolSession : BaseSession, IReadOnlyPropertyBag
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x000073A0 File Offset: 0x000055A0
		public ProtocolSession(NetworkConnection connection, VirtualServer virtualServer) : base(connection, virtualServer.Server.PreAuthConnectionTimeout, virtualServer.Server.MaxCommandLength)
		{
			this.virtualServer = virtualServer;
			if (this.Server.LightLog != null)
			{
				this.lightLogSession = this.Server.LightLog.OpenSession((ulong)connection.ConnectionId, connection.RemoteEndPoint, connection.LocalEndPoint, ProtocolLoggingLevel.Verbose);
			}
			this.mailboxLogTimeout = ExDateTime.MinValue;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007424 File Offset: 0x00005624
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000742C File Offset: 0x0000562C
		public WorkloadSettings WorkloadSettings { get; protected set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00007435 File Offset: 0x00005635
		public VirtualServer VirtualServer
		{
			get
			{
				return this.virtualServer;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000743D File Offset: 0x0000563D
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00007445 File Offset: 0x00005645
		public ResponseFactory ResponseFactory
		{
			get
			{
				return this.responseFactory;
			}
			set
			{
				this.responseFactory = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000744E File Offset: 0x0000564E
		public ProtocolBaseServices Server
		{
			get
			{
				return this.virtualServer.Server;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000745B File Offset: 0x0000565B
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00007463 File Offset: 0x00005663
		public ProxySession ProxySession
		{
			get
			{
				return this.proxySession;
			}
			set
			{
				this.proxySession = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000746C File Offset: 0x0000566C
		public bool IsTls
		{
			get
			{
				return this.isTls;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007474 File Offset: 0x00005674
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000747C File Offset: 0x0000567C
		public bool OkToIssueRead
		{
			get
			{
				return this.okToIssueRead;
			}
			set
			{
				this.okToIssueRead = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007485 File Offset: 0x00005685
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000748D File Offset: 0x0000568D
		public bool ProxyToLegacyServer { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00007496 File Offset: 0x00005696
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000749E File Offset: 0x0000569E
		public IStandardBudget Budget { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000074A7 File Offset: 0x000056A7
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x000074AF File Offset: 0x000056AF
		public ActivityScope ActivityScope { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000074B8 File Offset: 0x000056B8
		public LightWeightLogSession LightLogSession
		{
			get
			{
				return this.lightLogSession;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000074C0 File Offset: 0x000056C0
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000074C8 File Offset: 0x000056C8
		public LrsSession LrsSession { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000074D1 File Offset: 0x000056D1
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000074D9 File Offset: 0x000056D9
		protected internal IPAddress ProxyRemoteAddress { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000074E2 File Offset: 0x000056E2
		protected internal bool MailboxLogEnabled
		{
			get
			{
				return this.mailboxLogTimeout > ExDateTime.UtcNow;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000074F4 File Offset: 0x000056F4
		public void StartSession(bool startSSL)
		{
			base.EnterCommandProcessing();
			try
			{
				if (this.LightLogSession != null)
				{
					this.LightLogSession.BeginCommand(ProtocolSession.OpenBuf);
				}
				this.nonAuthSessionDisconnectTime = ExDateTime.UtcNow.AddSeconds((double)this.Server.PreAuthConnectionTimeout);
				if (startSSL)
				{
					this.StartSsl();
				}
				else if (this.AddToUnauthenticatedConnectionsPerIp())
				{
					if (!base.SendToClient(new StringResponseItem(this.BannerString())))
					{
						return;
					}
					this.hasShownBanner = true;
					base.SendToClient(new EndResponseItem(new BaseSession.SendCompleteDelegate(this.EndCommandProcess)));
				}
				ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "ProtocolSession has started.");
			}
			finally
			{
				base.LeaveCommandProcessing();
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000075B4 File Offset: 0x000057B4
		public bool StartSsl()
		{
			bool result;
			try
			{
				base.NegotiatingTls = true;
				base.Connection.BeginNegotiateTlsAsServer(this.virtualServer.Certificate, new AsyncCallback(this.TlsNegotiationCompleteCallback), base.Connection);
				result = true;
			}
			catch (Win32Exception ex)
			{
				base.NegotiatingTls = false;
				if (ex.NativeErrorCode != -2146885628)
				{
					throw;
				}
				ProtocolBaseServices.SessionTracer.TraceError(base.SessionId, "Unable to find certificate.");
				ProtocolBaseServices.LogEvent(this.Server.SslCertificateNotFoundEventTuple, null, new string[0]);
				result = false;
			}
			return result;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007650 File Offset: 0x00005850
		public void BeginShutdown()
		{
			this.BeginShutdown(null);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007659 File Offset: 0x00005859
		public void BeginShutdown(string message)
		{
			this.BeginShutdown(message, null);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007664 File Offset: 0x00005864
		public void BeginShutdown(string message, BaseSession.ConnectionShutdownDelegate connectionShutdown)
		{
			base.ConnectionShutdown = connectionShutdown;
			if (!string.IsNullOrEmpty(message))
			{
				base.SendToClient(new StringResponseItem(message, new BaseSession.SendCompleteDelegate(this.EndShutdown)));
				return;
			}
			base.SendToClient(new BufferResponseItem(ProtocolSession.EmptyBuffer, 0, 0, new BaseSession.SendCompleteDelegate(this.EndShutdown)));
		}

		// Token: 0x06000201 RID: 513
		public abstract string BannerString();

		// Token: 0x06000202 RID: 514 RVA: 0x000076BC File Offset: 0x000058BC
		public override bool IsUserTraceEnabled()
		{
			ResponseFactory responseFactory = this.responseFactory;
			if (responseFactory != null)
			{
				ProtocolUser protocolUser = responseFactory.ProtocolUser;
				if (protocolUser != null)
				{
					string uniqueName = protocolUser.UniqueName;
					if (!string.IsNullOrEmpty(uniqueName))
					{
						return ExUserTracingAdaptor.Instance.IsTracingEnabledUser(uniqueName);
					}
				}
			}
			return false;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000076FC File Offset: 0x000058FC
		public override string GetUserNameForLogging()
		{
			ResponseFactory responseFactory = this.ResponseFactory;
			if (responseFactory != null)
			{
				string userName = responseFactory.UserName;
				if (!string.IsNullOrEmpty(userName))
				{
					return userName;
				}
			}
			return "(Unauthorized user)";
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000772C File Offset: 0x0000592C
		public override void EnterReadLoop(NetworkConnection networkConnection)
		{
			lock (this.LockObject)
			{
				if (this.responseFactory != null && !this.responseFactory.IsAuthenticated && !this.responseFactory.IsDisconnected)
				{
					int num = (int)(this.nonAuthSessionDisconnectTime - ExDateTime.UtcNow).TotalSeconds;
					if (num <= 0)
					{
						ProtocolBaseServices.SessionTracer.Information(base.SessionId, "ProtocolSession.EnterReadLoop. Preauthenticated timeout");
						if (this.LightLogSession != null)
						{
							this.LightLogSession.ErrorMessage = "PreAuthTimeout";
						}
						this.BeginShutdown();
						return;
					}
					base.Connection.Timeout = num;
				}
			}
			base.EnterReadLoop(networkConnection);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000077F0 File Offset: 0x000059F0
		public override string ToString()
		{
			if (this.ProxyRemoteAddress != null)
			{
				return string.Format("{0} proxy from {1}", base.ToString(), this.ProxyRemoteAddress);
			}
			return base.ToString();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007817 File Offset: 0x00005A17
		public override bool CheckNonCriticalException(Exception exception)
		{
			if (this.lightLogSession != null)
			{
				this.lightLogSession.ExceptionCaught = exception;
			}
			return base.CheckNonCriticalException(exception);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007834 File Offset: 0x00005A34
		internal void SetMailboxLogTimeout(ExDateTime timeout)
		{
			this.mailboxLogTimeout = timeout;
			if (this.VerifyMailboxLogEnabled())
			{
				if (this.mailboxLogger != null)
				{
					throw new InvalidOperationException("this.mailboxLogger is not null.");
				}
				this.mailboxLogger = new MailboxLogger(this.ResponseFactory.Store, ProtocolBaseServices.ServiceName);
				if (this.mailboxLogger.LastError != null)
				{
					ProtocolBaseServices.LogEvent(this.Server.CreateMailboxLoggerFailedEventTuple, this.ResponseFactory.UserName, new string[]
					{
						this.ResponseFactory.UserName,
						this.mailboxLogger.LastError.ToString()
					});
				}
				this.ClearOldMailboxLogs();
				this.mailboxLogger.WriteLog(Encoding.ASCII.GetBytes(string.Format("<session id = \"{0}\" user = \"{1}\" from = \"{2}\" to = \"{3}\" time = \"{4}\" secure = \"{5}\">", new object[]
				{
					base.SessionId,
					this.ResponseFactory.UserName,
					base.RemoteEndPoint,
					base.LocalEndPoint,
					ExDateTime.Now,
					this.IsTls
				})));
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000794C File Offset: 0x00005B4C
		internal void LogReceive(byte[] buf, int offset, int size)
		{
			if (this.LightLogSession != null)
			{
				this.LightLogSession.RequestSize += (long)size;
			}
			bool flag = ProtocolBaseServices.SessionTracer.IsTraceEnabled(TraceType.InfoTrace);
			bool flag2 = this.VerifyMailboxLogEnabled();
			if (!flag && !flag2)
			{
				return;
			}
			if (flag)
			{
				string @string = Encoding.ASCII.GetString(buf, offset, size);
				ProtocolBaseServices.SessionTracer.Information<string>(base.SessionId, ">>> CommandReceived: {0}", @string);
			}
			if (flag2)
			{
				this.mailboxLogger.AppendLog(string.Format("<receive time = \"{0}\">", ExDateTime.Now));
				this.mailboxLogger.AppendLog(buf, offset, size);
				this.mailboxLogger.AppendLog("</receive>\r\n");
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000079F8 File Offset: 0x00005BF8
		internal void LogSend(byte[] buf, int offset, int size)
		{
			if (this.LightLogSession != null)
			{
				this.LightLogSession.ResponseSize += (long)size;
			}
			bool flag = ProtocolBaseServices.SessionTracer.IsTraceEnabled(TraceType.InfoTrace);
			bool flag2 = this.VerifyMailboxLogEnabled();
			if (!flag && !flag2)
			{
				return;
			}
			if (flag)
			{
				string arg = Encoding.ASCII.GetString(buf, offset, size).Trim();
				ProtocolBaseServices.SessionTracer.Information<string>(base.SessionId, "<<< Response sent: {0}", arg);
			}
			if (flag2)
			{
				this.mailboxLogger.AppendLog("<d>");
				this.mailboxLogger.AppendLog(buf, offset, size);
				this.mailboxLogger.AppendLog("</d>");
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007A98 File Offset: 0x00005C98
		internal void LogSend(string format, int size)
		{
			if (this.LightLogSession != null)
			{
				this.LightLogSession.ResponseSize += (long)size;
			}
			bool flag = ProtocolBaseServices.SessionTracer.IsTraceEnabled(TraceType.InfoTrace);
			bool flag2 = this.VerifyMailboxLogEnabled();
			if (!flag && !flag2)
			{
				return;
			}
			string text = string.Format(format, size);
			if (flag)
			{
				ProtocolBaseServices.SessionTracer.Information<string>(base.SessionId, "<<< Response sent: {0}", text.Trim());
			}
			if (flag2)
			{
				this.mailboxLogger.AppendLog("<d>");
				this.mailboxLogger.AppendLog(text);
				this.mailboxLogger.AppendLog("</d>");
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007B38 File Offset: 0x00005D38
		internal void LogInformation(string template, params object[] args)
		{
			ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, template, args);
			if (this.VerifyMailboxLogEnabled())
			{
				this.mailboxLogger.AppendLog(string.Format("<information time = \"{0}\">", ExDateTime.Now));
				this.mailboxLogger.AppendLog(string.Format(template, args));
				this.mailboxLogger.AppendLog("</information>\r\n");
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007BA0 File Offset: 0x00005DA0
		internal object GetThrottlingPolicyValue(Func<IThrottlingPolicy, object> func)
		{
			if (this.Budget == null)
			{
				return null;
			}
			return func(this.Budget.ThrottlingPolicy);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007BC0 File Offset: 0x00005DC0
		internal void EndShutdown()
		{
			lock (this.LockObject)
			{
				if (base.ProcessingCommandRefCounter > 0)
				{
					base.Disconnected = true;
				}
				else
				{
					base.Dispose();
				}
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007C14 File Offset: 0x00005E14
		internal void EnforceMicroDelayAndDisposeCostHandles(IStandardBudget perCallBudget)
		{
			try
			{
				if (perCallBudget != null)
				{
					bool flag = false;
					ResourceKey[] resourcesToAccess = null;
					lock (this.LockObject)
					{
						if (this.ResponseFactory != null)
						{
							flag = true;
							resourcesToAccess = this.ResponseFactory.ResourceKeys;
						}
					}
					try
					{
						if (flag)
						{
							ResourceLoadDelayInfo.EnforceDelay(perCallBudget, this.WorkloadSettings, resourcesToAccess, TimeSpan.MaxValue, null);
						}
					}
					finally
					{
						if (perCallBudget != null)
						{
							perCallBudget.Dispose();
						}
					}
				}
			}
			finally
			{
				ActivityContext.ClearThreadScope();
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007CB8 File Offset: 0x00005EB8
		protected internal bool AddToUnauthenticatedConnectionsPerIp()
		{
			if (ProtocolSession.unauthenticatedConnectionsPerIp.Add(base.RemoteEndPoint.Address) > this.Server.MaxConcurrentConnectionsFromSingleIp)
			{
				this.LogInformation("Number of unauthenticated connections from single IP {0} exceeded {1}, session disconnected.", new object[]
				{
					base.RemoteEndPoint.Address,
					this.Server.MaxConcurrentConnectionsFromSingleIp
				});
				ProtocolBaseServices.LogEvent(this.Server.MaxConnectionsFromSingleIpExceededEventTuple, base.RemoteEndPoint.Address.ToString(), new string[]
				{
					base.RemoteEndPoint.Address.ToString(),
					this.Server.MaxConcurrentConnectionsFromSingleIp.ToString()
				});
				this.virtualServer.Connections_Rejected.Increment();
				this.BeginShutdown(this.Server.MaxConnectionsError);
				return false;
			}
			this.VirtualServer.UnAuth_Connections.Increment();
			return true;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007DA8 File Offset: 0x00005FA8
		protected internal void RemoveFromUnauthenticatedConnectionsPerIp()
		{
			lock (this.LockObject)
			{
				if (!this.removedFromNonAuth)
				{
					ProtocolSession.unauthenticatedConnectionsPerIp.Remove(base.RemoteEndPoint.Address);
					this.VirtualServer.UnAuth_Connections.Decrement();
					this.removedFromNonAuth = true;
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007E18 File Offset: 0x00006018
		protected internal bool VerifyMailboxLogEnabled()
		{
			bool mailboxLogEnabled = this.MailboxLogEnabled;
			if (!mailboxLogEnabled && this.mailboxLogger != null)
			{
				this.mailboxLogger.AppendLog(string.Format("<stopped time = \"{0}\" /></session>\r\n", ExDateTime.Now));
				this.mailboxLogger.Flush();
				this.mailboxLogger.Dispose();
				this.mailboxLogger = null;
			}
			return mailboxLogEnabled;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007E74 File Offset: 0x00006074
		protected override void ReadLineCompletePostProcessing()
		{
			lock (this.LockObject)
			{
				if (this.responseFactory != null && !this.responseFactory.IsAuthenticated && !this.responseFactory.IsDisconnected)
				{
					int num = (int)(this.nonAuthSessionDisconnectTime - ExDateTime.UtcNow).TotalSeconds;
					if (num <= 0)
					{
						ProtocolBaseServices.SessionTracer.Information(base.SessionId, "ProtocolSession.ReadLineCompletePostProcessing. Preauthenticated timeout");
						if (this.LightLogSession != null)
						{
							this.LightLogSession.ErrorMessage = "PreAuthTimeout";
						}
						this.BeginShutdown();
					}
					else
					{
						base.Connection.Timeout = num;
					}
				}
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007F30 File Offset: 0x00006130
		protected void EndCommandProcess()
		{
			this.ResponseFactory.RecordCommandEnd();
			string commandName = string.Empty;
			if (this.LightLogSession != null && !this.ResponseFactory.IsInAuthenticationMode)
			{
				this.SetDiagnosticValue(PopImapConditionalHandlerSchema.LightLogContext, string.Concat(new object[]
				{
					this.LightLogSession.Result,
					this.LightLogSession.RowsProcessed,
					this.LightLogSession.Recent,
					this.LightLogSession.TotalSize,
					this.LightLogSession.SearchType,
					this.LightLogSession.ClientIp,
					this.LightLogSession.Message,
					this.LightLogSession.ErrorMessage,
					this.LightLogSession.LiveIdAuthResult,
					this.LightLogSession.FolderCount,
					this.LightLogSession.ItemsDeleted,
					(this.LightLogSession.ExceptionCaught == null) ? "<no exception>" : this.LightLogSession.ExceptionCaught.ToString(),
					this.LightLogSession.CafeActivityId,
					(this.LightLogSession.Budget == null) ? "<null budget>" : this.LightLogSession.Budget.ToString()
				}));
				if (this.LightLogSession.Command != null)
				{
					commandName = Encoding.Default.GetString(this.LightLogSession.Command);
				}
				this.LightLogSession.CompleteCommand();
			}
			if (this.MailboxLogEnabled && this.mailboxLogger != null)
			{
				this.mailboxLogger.AppendLog(string.Format("<commandFinished time = \"{0}\" />\r\n", ExDateTime.Now));
				this.mailboxLogger.Flush();
			}
			string text = this[PopImapConditionalHandlerSchema.RequestId] as string;
			Guid key;
			if (!string.IsNullOrEmpty(text) && Guid.TryParse(text, out key))
			{
				PopImapRequestData popImapRequestData = PopImapRequestCache.Instance.Get(key);
				popImapRequestData.Message = (this[PopImapConditionalHandlerSchema.Message] as string);
				popImapRequestData.LightLogContext = (this[PopImapConditionalHandlerSchema.LightLogContext] as string);
				popImapRequestData.Response = (this[PopImapConditionalHandlerSchema.Response] as string);
				popImapRequestData.RequestTime = ((this[ConditionalHandlerSchema.ElapsedTime] != null) ? TimeSpan.Parse(this[ConditionalHandlerSchema.ElapsedTime].ToString()).TotalMilliseconds : 0.0);
				popImapRequestData.ResponseType = (this[PopImapConditionalHandlerSchema.ResponseType] as string);
				if (popImapRequestData.CommandName == null)
				{
					popImapRequestData.CommandName = commandName;
				}
				if ((popImapRequestData.ErrorDetails == null || popImapRequestData.ErrorDetails.Count == 0) && this[ConditionalHandlerSchema.Exception] != null)
				{
					popImapRequestData.ErrorDetails = new List<ErrorDetail>();
					popImapRequestData.ErrorDetails.Add(new ErrorDetail
					{
						ErrorMessage = this[ConditionalHandlerSchema.Exception].ToString(),
						UserEmail = (this[ConditionalHandlerSchema.SmtpAddress] as string)
					});
				}
			}
			List<ConditionalResults> list = ConditionalRegistrationCache.Singleton.Evaluate(this);
			if (list != null && list.Count > 0)
			{
				foreach (ConditionalResults hit in list)
				{
					ConditionalRegistrationLog.Save(hit);
				}
			}
			base.BeginRead();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000082C0 File Offset: 0x000064C0
		protected override int SendNextChunk(NetworkConnection nc)
		{
			if (this.VerifyMailboxLogEnabled() && this.mailboxLogger != null)
			{
				this.mailboxLogger.AppendLog(string.Format("<sendStart time = \"{0}\" />\r\n", ExDateTime.Now));
			}
			int num = base.SendNextChunk(nc);
			if (num > 0 && this.VerifyMailboxLogEnabled() && this.mailboxLogger != null)
			{
				this.mailboxLogger.AppendLog(string.Format("<sendEnd time = \"{0}\" bytes=\"{1}\"/>\r\n", ExDateTime.Now, num));
			}
			return num;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008340 File Offset: 0x00006540
		protected override void InternalDispose()
		{
			lock (this.LockObject)
			{
				try
				{
					try
					{
						if (this.responseFactory != null)
						{
							this.responseFactory.RecordCommandEnd();
						}
						if (!this.removedFromNonAuth)
						{
							this.RemoveFromUnauthenticatedConnectionsPerIp();
						}
					}
					finally
					{
						if (this.virtualServer != null)
						{
							this.virtualServer.RemoveSession(this);
							this.virtualServer = null;
						}
					}
					if (this.LightLogSession != null)
					{
						if (this.LightLogSession.Command == null)
						{
							this.LightLogSession.BeginCommand(ProtocolSession.CloseBuf);
						}
						this.LightLogSession.CompleteCommand();
						this.lightLogSession = null;
					}
					if (this.LrsSession != null)
					{
						this.LrsSession = null;
					}
					if (this.mailboxLogger != null)
					{
						this.mailboxLogger.AppendLog(string.Format("<disconnected time = \"{0}\"/></session>\r\n", ExDateTime.Now));
						this.mailboxLogger.Flush();
						this.mailboxLogger.Dispose();
						this.mailboxLogger = null;
					}
					if (this.proxySession != null)
					{
						((IDisposable)this.proxySession).Dispose();
						this.proxySession = null;
					}
				}
				finally
				{
					try
					{
						if (this.responseFactory != null)
						{
							this.responseFactory.Dispose();
							this.responseFactory = null;
						}
					}
					finally
					{
						base.InternalDispose();
					}
				}
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000084D8 File Offset: 0x000066D8
		protected bool Is7BitString(byte[] buf, int offset, int size)
		{
			for (int i = 0; i < size; i++)
			{
				if (buf[offset + i] != 9 && buf[offset + i] != 10 && buf[offset + i] != 13 && (buf[offset + i] > 126 || buf[offset + i] < 32))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008524 File Offset: 0x00006724
		protected void ClearOldMailboxLogs()
		{
			MailboxLogger mailboxLogger = this.mailboxLogger;
			if (!this.VerifyMailboxLogEnabled() || mailboxLogger == null || mailboxLogger.LastError != null || this.ResponseFactory == null || !this.ResponseFactory.IsStoreConnected)
			{
				return;
			}
			bool flag = false;
			ProtocolBaseServices.FaultInjectionTracer.TraceTest<bool>(2816879933U, ref flag);
			if (flag)
			{
				mailboxLogger.ClearOldLogs(5000, 10485760L);
				return;
			}
			string uniqueName = this.ResponseFactory.ProtocolUser.UniqueName;
			ExDateTime exDateTime = ProtocolSession.GetNextTimeToClearMailboxLogs(uniqueName);
			if (exDateTime < ExDateTime.UtcNow)
			{
				object perUserClearMailboxLogsLock = ProtocolSession.GetPerUserClearMailboxLogsLock(uniqueName);
				lock (perUserClearMailboxLogsLock)
				{
					exDateTime = ProtocolSession.GetNextTimeToClearMailboxLogs(uniqueName);
					UserConfiguration userConfiguration = null;
					try
					{
						IDictionary dictionary = null;
						if (exDateTime == ExDateTime.MinValue)
						{
							userConfiguration = this.GetUserConfiguration();
							dictionary = userConfiguration.GetDictionary();
							if (dictionary.Contains("nextTimeToClearMailboxLogs"))
							{
								exDateTime = (ExDateTime)dictionary["nextTimeToClearMailboxLogs"];
							}
							else
							{
								exDateTime = ExDateTime.MinValue;
							}
							ProtocolSession.SetNextTimeToClearMailboxLogs(uniqueName, exDateTime);
						}
						if (exDateTime < ExDateTime.UtcNow)
						{
							mailboxLogger.ClearOldLogs(5000, 10485760L);
							exDateTime = ExDateTime.UtcNow.AddHours(6.0);
							ProtocolSession.SetNextTimeToClearMailboxLogs(uniqueName, exDateTime);
							if (userConfiguration == null)
							{
								userConfiguration = this.GetUserConfiguration();
								dictionary = userConfiguration.GetDictionary();
							}
							dictionary["nextTimeToClearMailboxLogs"] = exDateTime;
							userConfiguration.Save();
						}
					}
					catch (StorageTransientException ex)
					{
						ProtocolBaseServices.SessionTracer.TraceError<string, string>(base.SessionId, "Error when trying to get, create, or save UserConfiguration object: {0}\nStack trace: {1}", ex.Message, ex.StackTrace);
					}
					catch (StoragePermanentException ex2)
					{
						ProtocolBaseServices.SessionTracer.TraceError<string, string>(base.SessionId, "Error when trying to get, create, or save UserConfiguration object: {0}\nStack trace: {1}", ex2.Message, ex2.StackTrace);
					}
					finally
					{
						if (userConfiguration != null)
						{
							userConfiguration.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008760 File Offset: 0x00006960
		private static ExDateTime GetNextTimeToClearMailboxLogs(string uniqueName)
		{
			ExDateTime minValue;
			lock (ProtocolSession.nextTimeToClearMailboxLogs)
			{
				if (!ProtocolSession.nextTimeToClearMailboxLogs.TryGetValue(uniqueName, out minValue))
				{
					minValue = ExDateTime.MinValue;
					ProtocolSession.nextTimeToClearMailboxLogs[uniqueName] = minValue;
				}
			}
			return minValue;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000087BC File Offset: 0x000069BC
		private static void SetNextTimeToClearMailboxLogs(string uniqueName, ExDateTime time)
		{
			lock (ProtocolSession.nextTimeToClearMailboxLogs)
			{
				ProtocolSession.nextTimeToClearMailboxLogs[uniqueName] = time;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008804 File Offset: 0x00006A04
		private static object GetPerUserClearMailboxLogsLock(string uniqueName)
		{
			object obj2;
			lock (ProtocolSession.nextTimeToClearMailboxLogsLock)
			{
				if (!ProtocolSession.nextTimeToClearMailboxLogsLock.TryGetValue(uniqueName, out obj2))
				{
					obj2 = new object();
					ProtocolSession.nextTimeToClearMailboxLogsLock[uniqueName] = obj2;
				}
			}
			return obj2;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008860 File Offset: 0x00006A60
		private UserConfiguration GetUserConfiguration()
		{
			UserConfiguration userConfiguration;
			try
			{
				userConfiguration = this.ResponseFactory.Store.UserConfigurationManager.GetMailboxConfiguration(this.GetUserConfigurationName(), UserConfigurationTypes.Dictionary);
			}
			catch (ObjectNotFoundException)
			{
				try
				{
					userConfiguration = this.ResponseFactory.Store.UserConfigurationManager.CreateMailboxConfiguration(this.GetUserConfigurationName(), UserConfigurationTypes.Dictionary);
					userConfiguration.Save();
				}
				catch (ObjectExistedException)
				{
					userConfiguration = this.ResponseFactory.Store.UserConfigurationManager.GetMailboxConfiguration(this.GetUserConfigurationName(), UserConfigurationTypes.Dictionary);
				}
			}
			return userConfiguration;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000088F4 File Offset: 0x00006AF4
		private void TlsNegotiationCompleteCallback(IAsyncResult iar)
		{
			base.EnterCommandProcessing();
			try
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.SessionId, "User {0} entering ProtocolSession.TlsNegotiationCompleteCallback.", this.GetUserNameForLogging());
				ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "SSLHandshakeCompleted");
				NetworkConnection networkConnection = (NetworkConnection)iar.AsyncState;
				object obj;
				networkConnection.EndNegotiateTlsAsServer(iar, out obj);
				if (obj != null)
				{
					ProtocolBaseServices.SessionTracer.TraceError(base.SessionId, "TLS negotiation failed: {0}.", new object[]
					{
						obj
					});
					base.Dispose();
				}
				else
				{
					base.NegotiatingTls = false;
					this.isTls = base.Connection.IsTls;
					this.virtualServer.SSLConnections_Total.Increment();
					this.virtualServer.SSLConnections_Current.Increment();
					if (!this.hasShownBanner)
					{
						if (this.AddToUnauthenticatedConnectionsPerIp() && !base.SendToClient(new StringResponseItem(this.BannerString())))
						{
							return;
						}
					}
					else if (!base.SendToClient(new BufferResponseItem(ProtocolSession.EmptyBuffer, 0, 0)))
					{
						return;
					}
					base.SendToClient(new EndResponseItem(new BaseSession.SendCompleteDelegate(this.EndCommandProcess)));
				}
			}
			finally
			{
				base.LeaveCommandProcessing();
				ProtocolBaseServices.InMemoryTraceOperationCompleted(base.SessionId);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008A38 File Offset: 0x00006C38
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			object[] array = new object[propertyDefinitionArray.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitionArray)
			{
				array[num++] = this[propertyDefinition];
			}
			return array;
		}

		// Token: 0x170000BD RID: 189
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				object result = null;
				if (this.diagnosticsProperties.TryGetValue(propertyDefinition, out result))
				{
					return result;
				}
				if (propertyDefinition == ConditionalHandlerSchema.SmtpAddress)
				{
					if (this.ResponseFactory != null)
					{
						return this.ResponseFactory.PrimarySmtpAddress;
					}
					return null;
				}
				else if (propertyDefinition == ConditionalHandlerSchema.DisplayName)
				{
					if (this.ResponseFactory != null)
					{
						return this.ResponseFactory.UserName;
					}
					return null;
				}
				else if (propertyDefinition == ConditionalHandlerSchema.TenantName)
				{
					if (this.ResponseFactory != null && this.ResponseFactory.ProtocolUser != null)
					{
						return this.ResponseFactory.ProtocolUser.AcceptedDomain;
					}
					return null;
				}
				else if (propertyDefinition == ConditionalHandlerSchema.Cmd)
				{
					if (this.ResponseFactory != null)
					{
						return this.ResponseFactory.CommandName;
					}
					return null;
				}
				else if (propertyDefinition == PopImapConditionalHandlerSchema.Parameters)
				{
					if (this.ResponseFactory != null)
					{
						return this.ResponseFactory.Parameters;
					}
					return null;
				}
				else
				{
					if (propertyDefinition == ConditionalHandlerSchema.ThrottlingPolicyName)
					{
						return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.GetShortIdentityString());
					}
					if (propertyDefinition == ConditionalHandlerSchema.MaxConcurrency)
					{
						if (ProtocolBaseServices.ServiceName == "POP3")
						{
							return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.PopMaxConcurrency);
						}
						return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.ImapMaxConcurrency);
					}
					else if (propertyDefinition == ConditionalHandlerSchema.MaxBurst)
					{
						if (ProtocolBaseServices.ServiceName == "POP3")
						{
							return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.PopMaxBurst);
						}
						return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.ImapMaxBurst);
					}
					else if (propertyDefinition == ConditionalHandlerSchema.RechargeRate)
					{
						if (ProtocolBaseServices.ServiceName == "POP3")
						{
							return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.PopRechargeRate);
						}
						return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.ImapRechargeRate);
					}
					else if (propertyDefinition == ConditionalHandlerSchema.CutoffBalance)
					{
						if (ProtocolBaseServices.ServiceName == "POP3")
						{
							return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.PopCutoffBalance);
						}
						return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.ImapCutoffBalance);
					}
					else
					{
						if (propertyDefinition == ConditionalHandlerSchema.ThrottlingPolicyScope)
						{
							return this.GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.ThrottlingPolicyScope);
						}
						if (propertyDefinition == ConditionalHandlerSchema.BudgetLockedOut)
						{
							ITokenBucket budgetTokenBucket = this.GetBudgetTokenBucket();
							return budgetTokenBucket != null && budgetTokenBucket.Locked;
						}
						if (propertyDefinition == ConditionalHandlerSchema.BudgetLockedUntil)
						{
							ITokenBucket budgetTokenBucket2 = this.GetBudgetTokenBucket();
							return (budgetTokenBucket2 != null) ? budgetTokenBucket2.LockedUntilUtc : new DateTime?(DateTime.MinValue);
						}
						return null;
					}
				}
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008E09 File Offset: 0x00007009
		public void SetDiagnosticValue(PropertyDefinition propDef, object value)
		{
			this.diagnosticsProperties[propDef] = value;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008E18 File Offset: 0x00007018
		public void ClearDiagnosticValue(PropertyDefinition propDef)
		{
			object obj;
			this.diagnosticsProperties.TryRemove(propDef, out obj);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008E34 File Offset: 0x00007034
		internal ITokenBucket GetBudgetTokenBucket()
		{
			StandardBudgetWrapper standardBudgetWrapper = this.Budget as StandardBudgetWrapper;
			if (standardBudgetWrapper != null)
			{
				return standardBudgetWrapper.GetInnerBudget().CasTokenBucket;
			}
			return null;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008E60 File Offset: 0x00007060
		internal void SetBudgetDiagnosticValues(bool start)
		{
			ITokenBucket budgetTokenBucket = this.GetBudgetTokenBucket();
			if (budgetTokenBucket != null)
			{
				float balance = budgetTokenBucket.GetBalance();
				this.SetDiagnosticValue(start ? ConditionalHandlerSchema.BudgetBalanceStart : ConditionalHandlerSchema.BudgetBalanceEnd, balance);
				this.SetDiagnosticValue(start ? ConditionalHandlerSchema.IsOverBudgetAtStart : ConditionalHandlerSchema.IsOverBudgetAtEnd, balance < 0f);
			}
			StandardBudgetWrapper standardBudgetWrapper = this.Budget as StandardBudgetWrapper;
			if (standardBudgetWrapper != null)
			{
				this.SetDiagnosticValue(start ? ConditionalHandlerSchema.ConcurrencyStart : ConditionalHandlerSchema.ConcurrencyEnd, standardBudgetWrapper.GetInnerBudget().Connections);
			}
		}

		// Token: 0x0400011B RID: 283
		private const string NextTimeToClearMailboxLogsProperty = "nextTimeToClearMailboxLogs";

		// Token: 0x0400011C RID: 284
		private const int MaxNumberOfMailboxLogs = 5000;

		// Token: 0x0400011D RID: 285
		private const long MaxSizeOfMailboxLogs = 10485760L;

		// Token: 0x0400011E RID: 286
		protected static readonly byte[] EmptyBuffer = new byte[0];

		// Token: 0x0400011F RID: 287
		private static readonly byte[] OpenBuf = Encoding.ASCII.GetBytes("OpenSession");

		// Token: 0x04000120 RID: 288
		private static readonly byte[] CloseBuf = Encoding.ASCII.GetBytes("CloseSession");

		// Token: 0x04000121 RID: 289
		private static RefCountTable<IPAddress> unauthenticatedConnectionsPerIp = new RefCountTable<IPAddress>();

		// Token: 0x04000122 RID: 290
		private static Dictionary<string, ExDateTime> nextTimeToClearMailboxLogs = new Dictionary<string, ExDateTime>();

		// Token: 0x04000123 RID: 291
		private static Dictionary<string, object> nextTimeToClearMailboxLogsLock = new Dictionary<string, object>();

		// Token: 0x04000124 RID: 292
		private ConcurrentDictionary<PropertyDefinition, object> diagnosticsProperties = new ConcurrentDictionary<PropertyDefinition, object>();

		// Token: 0x04000125 RID: 293
		private ResponseFactory responseFactory;

		// Token: 0x04000126 RID: 294
		private ProxySession proxySession;

		// Token: 0x04000127 RID: 295
		private bool okToIssueRead = true;

		// Token: 0x04000128 RID: 296
		private bool isTls;

		// Token: 0x04000129 RID: 297
		private VirtualServer virtualServer;

		// Token: 0x0400012A RID: 298
		private bool hasShownBanner;

		// Token: 0x0400012B RID: 299
		private LightWeightLogSession lightLogSession;

		// Token: 0x0400012C RID: 300
		private ExDateTime mailboxLogTimeout;

		// Token: 0x0400012D RID: 301
		private MailboxLogger mailboxLogger;

		// Token: 0x0400012E RID: 302
		private bool removedFromNonAuth;

		// Token: 0x0400012F RID: 303
		private ExDateTime nonAuthSessionDisconnectTime;
	}
}
