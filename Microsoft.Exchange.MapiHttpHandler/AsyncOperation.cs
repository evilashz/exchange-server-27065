using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MapiHttpHandler;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AsyncOperation : BaseObject
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003078 File Offset: 0x00001278
		protected AsyncOperation(HttpContextBase context, string cookieVdirPath, AsyncOperationCookieFlags cookieFlags)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.context = context;
				this.cookieVdirPath = cookieVdirPath;
				this.cookieFlags = cookieFlags;
				string strA = context.GetContentType();
				if (string.Compare(strA, "application/mapi-http", true) == 0)
				{
					this.contentType = "application/mapi-http";
				}
				else
				{
					if (string.Compare(strA, "application/octet-stream", true) != 0)
					{
						throw ProtocolException.FromResponseCode((LID)46876, string.Format("Invalid Content-Type header; expecting {0}", "application/mapi-http"), ResponseCode.InvalidHeader, null);
					}
					this.contentType = "application/octet-stream";
				}
				AsyncOperation.ValidateAcceptTypes(context.Request, ref this.contentType);
				this.requestId = context.GetRequestId();
				disposeGuard.Success();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003150 File Offset: 0x00001350
		public string ContentType
		{
			get
			{
				base.CheckDisposed();
				return this.contentType;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000315E File Offset: 0x0000135E
		public string RequestId
		{
			get
			{
				base.CheckDisposed();
				return this.requestId;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000039 RID: 57
		public abstract string RequestType { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000316C File Offset: 0x0000136C
		public virtual TimeSpan InitialFlushPeriod
		{
			get
			{
				return AsyncOperation.initialFlushPeriod;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003173 File Offset: 0x00001373
		public AsyncOperationInfo AsyncOperationInfo
		{
			get
			{
				base.CheckDisposed();
				return this.asyncOperationInfo;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003181 File Offset: 0x00001381
		public virtual object DroppedConnectionContextHandle
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000318A File Offset: 0x0000138A
		public SessionContext SessionContext
		{
			get
			{
				base.CheckDisposed();
				if (this.sessionContextActivity == null)
				{
					return null;
				}
				return this.sessionContextActivity.SessionContext;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000031A7 File Offset: 0x000013A7
		protected HttpContextBase Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000031AF File Offset: 0x000013AF
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000031C6 File Offset: 0x000013C6
		protected object ContextHandle
		{
			get
			{
				if (this.SessionContext == null)
				{
					return null;
				}
				return this.SessionContext.ContextHandle;
			}
			set
			{
				if (this.SessionContext == null)
				{
					if (value != null)
					{
						throw new InvalidOperationException("Unable to set context handle without a session context");
					}
				}
				else
				{
					this.SessionContext.ContextHandle = value;
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000031EA File Offset: 0x000013EA
		protected string Cookie
		{
			get
			{
				if (this.SessionContext == null)
				{
					return null;
				}
				return this.SessionContext.Cookie;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003201 File Offset: 0x00001401
		protected string SequenceCookie
		{
			get
			{
				return this.currentSequenceCookie;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003209 File Offset: 0x00001409
		protected AsyncOperation.MapiHttpProtocolRequestInfo ProtocolRequestInfo
		{
			get
			{
				return this.protocolRequestInfo;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003211 File Offset: 0x00001411
		public virtual string GetTraceBeginParameters(MapiHttpRequest mapiHttpRequest)
		{
			base.CheckDisposed();
			return string.Empty;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000321E File Offset: 0x0000141E
		public virtual string GetTraceEndParameters(MapiHttpRequest mapiHttpRequest, MapiHttpResponse mapiHttpResponse)
		{
			base.CheckDisposed();
			return string.Empty;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000322C File Offset: 0x0000142C
		public virtual void ParseRequest(WorkBuffer requestBuffer)
		{
			base.CheckDisposed();
			using (Reader reader = Reader.CreateBufferReader(requestBuffer.ArraySegment))
			{
				this.request = this.InternalParseRequest(reader);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000034CC File Offset: 0x000016CC
		public virtual async Task ExecuteAsync()
		{
			base.CheckDisposed();
			this.TraceBegin();
			TimeSpan requestDelay;
			TimeSpan responseDelay;
			this.context.GetTimingOverrides(out requestDelay, out responseDelay);
			if (requestDelay != TimeSpan.Zero)
			{
				await Task.Delay(requestDelay);
			}
			this.response = await this.InternalExecuteAsync(this.request);
			if (responseDelay != TimeSpan.Zero)
			{
				await Task.Delay(responseDelay);
			}
			this.TraceEnd();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003514 File Offset: 0x00001714
		public virtual void SerializeResponse(out WorkBuffer[] responseBuffers)
		{
			base.CheckDisposed();
			int maxSizeOfBuffer = (int)this.response.GetSerializedSize();
			WorkBuffer workBuffer = new WorkBuffer(maxSizeOfBuffer);
			try
			{
				using (BufferWriter bufferWriter = new BufferWriter(workBuffer.ArraySegment))
				{
					this.response.Serialize(bufferWriter);
					workBuffer.Count = (int)bufferWriter.Position;
				}
				responseBuffers = new WorkBuffer[]
				{
					workBuffer
				};
				workBuffer = null;
			}
			finally
			{
				Util.DisposeIfPresent(workBuffer);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000035A4 File Offset: 0x000017A4
		public virtual void Prepare()
		{
			base.CheckDisposed();
			this.sessionContextActivity = null;
			string userAuthIdentifier;
			if (!this.context.TryGetUserAuthIdentifier(out userAuthIdentifier))
			{
				throw ProtocolException.FromResponseCode((LID)43100, "Unable to determine user authentication identifier.", ResponseCode.AnonymousNotAllowed, null);
			}
			string empty;
			if (!this.context.TryGetMailboxIdParameter(out empty))
			{
				empty = string.Empty;
			}
			TimeSpan sessionContextIdleTimeout;
			if (!this.context.TryGetExpirationInfo(out sessionContextIdleTimeout))
			{
				sessionContextIdleTimeout = Constants.SessionContextIdleTimeout;
			}
			string cookie;
			Exception ex2;
			if ((this.cookieFlags & AsyncOperationCookieFlags.RequireSession) == AsyncOperationCookieFlags.RequireSession)
			{
				string contextCookie = this.context.GetContextCookie();
				if ((this.cookieFlags & AsyncOperationCookieFlags.AllowInvalidSession) == AsyncOperationCookieFlags.AllowInvalidSession)
				{
					Exception ex;
					SessionContextManager.TryGetSessionContextActivity(contextCookie, userAuthIdentifier, sessionContextIdleTimeout, out this.sessionContextActivity, out ex);
				}
				else
				{
					this.sessionContextActivity = SessionContextManager.GetSessionContextActivity(contextCookie, userAuthIdentifier, sessionContextIdleTimeout);
				}
				if (this.SessionContext != null && (this.cookieFlags & AsyncOperationCookieFlags.RequireSequence) == AsyncOperationCookieFlags.RequireSequence)
				{
					string sequenceCookie = this.context.GetSequenceCookie();
					string sequenceCookie2 = this.SessionContext.Identifier.BeginSequenceOperation(sequenceCookie);
					this.context.SetSequenceCookie(sequenceCookie2, this.cookieVdirPath);
					this.currentSequenceCookie = sequenceCookie;
				}
			}
			else if ((this.cookieFlags & AsyncOperationCookieFlags.AllowSession) == AsyncOperationCookieFlags.AllowSession && this.context.TryGetContextCookie(out cookie) && !SessionContextManager.TryGetSessionContextActivity(cookie, userAuthIdentifier, sessionContextIdleTimeout, out this.sessionContextActivity, out ex2) && (this.cookieFlags & AsyncOperationCookieFlags.AllowInvalidSession) == AsyncOperationCookieFlags.None)
			{
				throw ex2;
			}
			if ((this.cookieFlags & AsyncOperationCookieFlags.CreateSession) == AsyncOperationCookieFlags.CreateSession)
			{
				if (this.SessionContext != null)
				{
					this.SessionContext.MarkForRundown(SessionRundownReason.ClientRecreate);
					Util.DisposeIfPresent(this.sessionContextActivity);
					this.sessionContextActivity = null;
				}
				string text;
				string text2;
				string text3;
				string text4;
				string text5;
				if (!this.context.TryGetUserIdentityInfo(out text, out text2, out text3, out text4, out text5))
				{
					throw ProtocolException.FromResponseCode((LID)57852, "Unable to determine user identity information.", ResponseCode.AnonymousNotAllowed, null);
				}
				if (string.IsNullOrEmpty(text3))
				{
					throw ProtocolException.FromResponseCode((LID)33276, "Unable to determine user security information.", ResponseCode.AnonymousNotAllowed, null);
				}
				this.sessionContextActivity = SessionContextManager.CreateSessionContextActivity(new SessionContextIdentifier(), sessionContextIdleTimeout, userAuthIdentifier, string.IsNullOrEmpty(text) ? string.Empty : text, string.IsNullOrEmpty(text2) ? string.Empty : text2, text3, string.IsNullOrEmpty(text5) ? string.Empty : text5, string.IsNullOrEmpty(empty) ? string.Empty : empty);
			}
			if (this.SessionContext != null)
			{
				this.context.SetContextCookie(this.SessionContext.Identifier.Cookie, this.cookieVdirPath);
				this.context.SetExpirationInfo(this.SessionContext.ExpirationInfo);
				if ((this.cookieFlags & AsyncOperationCookieFlags.CreateSession) == AsyncOperationCookieFlags.CreateSession)
				{
					this.context.SetSequenceCookie(this.SessionContext.Identifier.NextSequenceCookie, this.cookieVdirPath);
				}
			}
			string empty2;
			if (!this.context.TryGetCafeActivityId(out empty2))
			{
				empty2 = string.Empty;
			}
			string empty3;
			string serverAddress;
			if (!this.context.TryGetClientIPEndpoints(out empty3, out serverAddress))
			{
				empty3 = string.Empty;
			}
			string empty4 = string.Empty;
			this.context.TryGetSourceCafeServer(out empty4);
			this.protocolRequestInfo = new AsyncOperation.MapiHttpProtocolRequestInfo(this.requestId, empty2, empty4, this.Cookie, this.SequenceCookie, empty3, serverAddress, empty);
			if (this.SessionContext != null)
			{
				this.asyncOperationInfo = new AsyncOperationInfo(this.RequestType, this.RequestId, this.SequenceCookie, empty4, empty2, empty3);
				this.SessionContext.AsyncOperationTracker.Register(this.asyncOperationInfo);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000038C8 File Offset: 0x00001AC8
		public void Cleanup()
		{
			base.CheckDisposed();
			if (this.SessionContext != null)
			{
				if ((this.cookieFlags & AsyncOperationCookieFlags.DestroySession) == AsyncOperationCookieFlags.DestroySession)
				{
					this.SessionContext.MarkForRundown(SessionRundownReason.ClientRundown);
				}
				if (this.currentSequenceCookie != null)
				{
					this.SessionContext.Identifier.EndSequenceOperation();
					this.currentSequenceCookie = null;
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000391C File Offset: 0x00001B1C
		public void OnComplete(Exception failureException)
		{
			base.CheckDisposed();
			if (this.SessionContext != null)
			{
				if ((this.cookieFlags & AsyncOperationCookieFlags.CreateSession) != AsyncOperationCookieFlags.None)
				{
					if (failureException != null)
					{
						this.SessionContext.MarkForRundown(SessionRundownReason.ProtocolFault);
					}
					else if (this.ContextHandle == null)
					{
						this.SessionContext.MarkForRundown(SessionRundownReason.ContextHandleCleared);
					}
				}
				if (this.currentSequenceCookie != null)
				{
					this.SessionContext.Identifier.EndSequenceOperation();
					this.currentSequenceCookie = null;
				}
			}
			if (this.asyncOperationInfo != null)
			{
				this.asyncOperationInfo.OnComplete(failureException);
				if (this.SessionContext != null)
				{
					this.SessionContext.AsyncOperationTracker.Complete(this.asyncOperationInfo);
				}
				this.asyncOperationInfo = null;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000039BE File Offset: 0x00001BBE
		public void AppendLogString(StringBuilder stringBuilder)
		{
			if (this.response != null)
			{
				this.response.AppendLogString(stringBuilder);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000039D4 File Offset: 0x00001BD4
		protected static string CombineTraceParameters(string trace1, string trace2)
		{
			if (string.IsNullOrEmpty(trace2))
			{
				return trace1;
			}
			if (string.IsNullOrEmpty(trace1))
			{
				return trace2;
			}
			return string.Format("{0}; {1}", trace1, trace2);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000039F6 File Offset: 0x00001BF6
		protected static string CreateCookieVdirPath(string vdirPath)
		{
			if (!string.IsNullOrEmpty(vdirPath))
			{
				return vdirPath.TrimEnd(AsyncOperation.slashDelimiter);
			}
			return "/";
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003A11 File Offset: 0x00001C11
		protected virtual MapiHttpRequest InternalParseRequest(Reader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003A18 File Offset: 0x00001C18
		protected virtual Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003A20 File Offset: 0x00001C20
		protected ArraySegment<byte> AllocateBuffer(int requestedSize, int maxAllowedSize)
		{
			if (requestedSize > maxAllowedSize)
			{
				throw ProtocolException.FromResponseCode((LID)33500, string.Format("Requested output buffer size({0}) bigger than the maximum allowed({1})", requestedSize, maxAllowedSize), ResponseCode.InvalidPayload, null);
			}
			WorkBuffer workBuffer = new WorkBuffer(requestedSize);
			this.allocatedBuffers.Add(workBuffer);
			return workBuffer.ArraySegment;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003A6E File Offset: 0x00001C6E
		protected ArraySegment<byte> AllocateErrorBuffer()
		{
			return this.AllocateBuffer(4104, 4104);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003A80 File Offset: 0x00001C80
		protected MapiHttpClientBinding GetMapiHttpClientBinding(Func<ClientSecurityContext> clientSecurityContextGetter)
		{
			return new MapiHttpClientBinding(this.ProtocolRequestInfo.MailboxIdentifier, this.ProtocolRequestInfo.ClientAddress, this.ProtocolRequestInfo.ServerAddress, this.Context.Request.IsSecureConnection, this.Context.User.Identity, clientSecurityContextGetter);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003AD4 File Offset: 0x00001CD4
		protected ClientSecurityContext GetInitialCachedClientSecurityContext()
		{
			ClientSecurityContext result;
			try
			{
				result = this.context.User.Identity.CreateClientSecurityContext(false);
			}
			catch (AuthzException innerException)
			{
				throw ProtocolException.FromHttpStatusCode((LID)37900, "Could not create a client security context from a user identity.", string.Empty, HttpStatusCode.ServiceUnavailable, HttpStatusCode.ServiceUnavailable.ToString(), null, null, innerException);
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003B38 File Offset: 0x00001D38
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.sessionContextActivity);
			this.DisposeAllocatedBuffers();
			base.InternalDispose();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003B51 File Offset: 0x00001D51
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AsyncOperation>(this);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003B5C File Offset: 0x00001D5C
		private static void ValidateAcceptTypes(HttpRequestBase request, ref string contentType)
		{
			string[] acceptTypes = request.AcceptTypes;
			if (acceptTypes == null || acceptTypes.Length == 0)
			{
				return;
			}
			string[] array = acceptTypes;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				string text2 = text;
				int num = text2.IndexOf(';');
				if (num == -1)
				{
					goto IL_3F;
				}
				if (num > 0)
				{
					text2 = text2.Substring(0, num).Trim();
					goto IL_3F;
				}
				IL_8B:
				i++;
				continue;
				IL_3F:
				if (string.Equals(text2, "application/mapi-http", StringComparison.InvariantCultureIgnoreCase))
				{
					contentType = "application/mapi-http";
				}
				else if (string.Equals(text2, "application/octet-stream", StringComparison.InvariantCultureIgnoreCase))
				{
					contentType = "application/octet-stream";
				}
				else if (!string.Equals(text2, "application/*", StringComparison.InvariantCultureIgnoreCase) && !string.Equals(text2, "*/*", StringComparison.InvariantCultureIgnoreCase))
				{
					goto IL_8B;
				}
				return;
			}
			throw ProtocolException.FromResponseCode((LID)62716, string.Format("Invalid Accept header; expecting {0}", "application/mapi-http"), ResponseCode.InvalidHeader, null);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003C24 File Offset: 0x00001E24
		private void TraceOperation(string action, string parameters)
		{
			ExTraceGlobals.AsyncOperationTracer.TraceInformation(34208, 0L, "[{0}] {1}: {2}{3}", new object[]
			{
				this.RequestId,
				this.RequestType,
				action,
				string.IsNullOrEmpty(parameters) ? string.Empty : string.Format("; {0}", parameters)
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003C82 File Offset: 0x00001E82
		private void TraceBegin()
		{
			if (ExTraceGlobals.AsyncOperationTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				this.TraceOperation("Begin", this.GetTraceBeginParameters(this.request));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003CA8 File Offset: 0x00001EA8
		private void TraceEnd()
		{
			if (ExTraceGlobals.AsyncOperationTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				this.TraceOperation("End", this.GetTraceEndParameters(this.request, this.response));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003CD4 File Offset: 0x00001ED4
		private void DisposeAllocatedBuffers()
		{
			foreach (WorkBuffer workBuffer in this.allocatedBuffers)
			{
				workBuffer.Dispose();
			}
			this.allocatedBuffers.Clear();
		}

		// Token: 0x04000049 RID: 73
		private static readonly char[] slashDelimiter = new char[]
		{
			'/'
		};

		// Token: 0x0400004A RID: 74
		private static readonly TimeSpan initialFlushPeriod = TimeSpan.FromSeconds(2.0);

		// Token: 0x0400004B RID: 75
		private readonly IList<WorkBuffer> allocatedBuffers = new List<WorkBuffer>();

		// Token: 0x0400004C RID: 76
		private readonly HttpContextBase context;

		// Token: 0x0400004D RID: 77
		private readonly string requestId;

		// Token: 0x0400004E RID: 78
		private readonly string cookieVdirPath;

		// Token: 0x0400004F RID: 79
		private readonly string contentType;

		// Token: 0x04000050 RID: 80
		private readonly AsyncOperationCookieFlags cookieFlags;

		// Token: 0x04000051 RID: 81
		private MapiHttpRequest request;

		// Token: 0x04000052 RID: 82
		private MapiHttpResponse response;

		// Token: 0x04000053 RID: 83
		private SessionContextActivity sessionContextActivity;

		// Token: 0x04000054 RID: 84
		private AsyncOperationInfo asyncOperationInfo;

		// Token: 0x04000055 RID: 85
		private string currentSequenceCookie;

		// Token: 0x04000056 RID: 86
		private AsyncOperation.MapiHttpProtocolRequestInfo protocolRequestInfo;

		// Token: 0x02000007 RID: 7
		protected class MapiHttpProtocolRequestInfo : ProtocolRequestInfo
		{
			// Token: 0x0600005D RID: 93 RVA: 0x00003D60 File Offset: 0x00001F60
			public MapiHttpProtocolRequestInfo(string requestId, string cafeActivityId, string sourceCafeServer, string contextCookie, string sequenceCookie, string clientAddress, string serverAddress, string mailboxIdentifier)
			{
				List<string> list = new List<string>(3);
				if (!string.IsNullOrEmpty(requestId))
				{
					list.Add("R:" + requestId);
				}
				if (!string.IsNullOrEmpty(cafeActivityId))
				{
					list.Add("A:" + cafeActivityId);
				}
				if (!string.IsNullOrEmpty(sourceCafeServer))
				{
					list.Add("FE:" + sourceCafeServer);
				}
				if (list.Count > 0)
				{
					this.requestIds = list.ToArray();
				}
				list.Clear();
				if (!string.IsNullOrEmpty(contextCookie))
				{
					list.Add("C:" + contextCookie);
				}
				if (!string.IsNullOrEmpty(sequenceCookie))
				{
					list.Add("S:" + sequenceCookie);
				}
				if (list.Count > 0)
				{
					this.cookies = list.ToArray();
				}
				this.clientAddress = clientAddress;
				this.serverAddress = serverAddress;
				this.mailboxIdentifier = mailboxIdentifier;
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x0600005E RID: 94 RVA: 0x00003E43 File Offset: 0x00002043
			public override string[] RequestIds
			{
				get
				{
					return this.requestIds;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00003E4B File Offset: 0x0000204B
			public override string[] Cookies
			{
				get
				{
					return this.cookies;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000060 RID: 96 RVA: 0x00003E53 File Offset: 0x00002053
			public override string ClientAddress
			{
				get
				{
					return this.clientAddress;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000061 RID: 97 RVA: 0x00003E5B File Offset: 0x0000205B
			public string ServerAddress
			{
				get
				{
					return this.serverAddress;
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x06000062 RID: 98 RVA: 0x00003E63 File Offset: 0x00002063
			public string MailboxIdentifier
			{
				get
				{
					return this.mailboxIdentifier;
				}
			}

			// Token: 0x04000057 RID: 87
			private readonly string[] requestIds;

			// Token: 0x04000058 RID: 88
			private readonly string[] cookies;

			// Token: 0x04000059 RID: 89
			private readonly string clientAddress;

			// Token: 0x0400005A RID: 90
			private readonly string serverAddress;

			// Token: 0x0400005B RID: 91
			private readonly string mailboxIdentifier;
		}
	}
}
