using System;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200001F RID: 31
	internal sealed class Pop3Session : ProtocolSession
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00005F50 File Offset: 0x00004150
		public Pop3Session(NetworkConnection connection, VirtualServer virtualServer) : base(connection, virtualServer)
		{
			base.ResponseFactory = new Pop3ResponseFactory(this);
			base.WorkloadSettings = Pop3Session.DefaultWorkloadSettings;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005F74 File Offset: 0x00004174
		public override string BannerString()
		{
			string result;
			if (base.VirtualServer.Banner != null)
			{
				result = string.Format("+OK {0}", base.VirtualServer.Banner);
			}
			else
			{
				result = string.Format("+OK {0} Microsoft POP3 MAIL Service, ready at {1}", ProtocolBaseServices.ServerName, Rfc822Date.Format(ResponseFactory.CurrentExTimeZone.ConvertDateTime(ExDateTime.UtcNow)));
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005FCB File Offset: 0x000041CB
		public override string GetUserConfigurationName()
		{
			return "POP3.UserConfiguration";
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005FD2 File Offset: 0x000041D2
		internal override void HandleCommandTooLongError(NetworkConnection nc, byte[] buf, int offset, int size)
		{
			base.SendToClient(new BufferResponseItem(Pop3Session.CommandTooLong, new BaseSession.SendCompleteDelegate(base.EndShutdown)));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005FF4 File Offset: 0x000041F4
		internal override void HandleIdleTimeout()
		{
			ProtocolBaseServices.SessionTracer.TraceDebug<Pop3Session>(base.SessionId, "Idle timeout reached. Attempting to end session: {0}.", this);
			if (base.NegotiatingTls)
			{
				base.Connection.Shutdown();
				return;
			}
			lock (this.LockObject)
			{
				Pop3ResponseFactory pop3ResponseFactory = (Pop3ResponseFactory)base.ResponseFactory;
				if (pop3ResponseFactory != null)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug<Pop3Session>(base.SessionId, "Ending session due to idle timeout: {0}.", this);
					base.BeginShutdown(pop3ResponseFactory.TimeoutErrorString);
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000608C File Offset: 0x0000428C
		protected override void HandleCommand(NetworkConnection nc, byte[] buf, int offset, int size)
		{
			ProtocolResponse protocolResponse = null;
			try
			{
				ResponseFactory responseFactory = base.ResponseFactory;
				if (responseFactory == null || responseFactory.Disposed)
				{
					ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "ResponseFactory is disposed.");
					return;
				}
				if (base.ProxySession != null)
				{
					this.HandleProxyCommand(nc, buf, offset, size);
					return;
				}
				IStandardBudget perCallBudget = null;
				try
				{
					responseFactory.RecordCommandStart();
					perCallBudget = responseFactory.AcquirePerCommandBudget();
					if (ProtocolBaseServices.SessionTracer.IsTraceEnabled(TraceType.InfoTrace) || base.VerifyMailboxLogEnabled() || base.LightLogSession != null)
					{
						if (responseFactory.IsInAuthenticationMode)
						{
							base.LogReceive(Pop3Session.AuthBlob, 0, Pop3Session.AuthBlob.Length);
						}
						else
						{
							int num;
							int nextToken = BaseSession.GetNextToken(buf, offset, size, out num);
							if (nextToken == offset && num < offset + size && BaseSession.CompareArg(Pop3Session.PassBuf, buf, nextToken, num - nextToken))
							{
								base.LogReceive(Pop3Session.PassLogBuf, 0, Pop3Session.PassLogBuf.Length);
							}
							else
							{
								base.LogReceive(buf, offset, size);
							}
						}
					}
					if (responseFactory.IsInAuthenticationMode)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug(base.SessionId, "Authentication mode.");
						protocolResponse = responseFactory.ProcessAuthentication(buf, offset, size);
					}
					else if (this.Is7BitCommand(buf, offset, size))
					{
						protocolResponse = responseFactory.ProcessCommand(buf, offset, size);
					}
					else
					{
						protocolResponse = responseFactory.CommandIsNotAllASCII(buf, offset, size);
					}
					if (!base.OkToIssueRead)
					{
						base.OkToIssueRead = true;
						return;
					}
				}
				catch (Exception exception)
				{
					if (!this.CheckNonCriticalException(exception))
					{
						throw;
					}
					protocolResponse = responseFactory.ProcessException(null, exception);
				}
				finally
				{
					base.EnforceMicroDelayAndDisposeCostHandles(perCallBudget);
				}
				if (protocolResponse != null)
				{
					if (protocolResponse.IsDisconnectResponse)
					{
						base.BeginShutdown(protocolResponse.DataToSend);
						return;
					}
					if (!base.SendToClient(new StringResponseItem(protocolResponse.DataToSend)))
					{
						return;
					}
				}
			}
			finally
			{
				if (protocolResponse != null)
				{
					base.SetDiagnosticValue(PopImapConditionalHandlerSchema.Message, protocolResponse.MessageString);
					base.SetDiagnosticValue(PopImapConditionalHandlerSchema.Response, protocolResponse.DataToSend);
					this.LogCommand(protocolResponse, protocolResponse.DataToSend);
					protocolResponse.Dispose();
				}
			}
			base.SendToClient(new EndResponseItem(new BaseSession.SendCompleteDelegate(base.EndCommandProcess)));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000062CC File Offset: 0x000044CC
		private void HandleProxyCommand(NetworkConnection nc, byte[] buf, int offset, int size)
		{
			if (base.LightLogSession != null)
			{
				base.LightLogSession.RequestSize += (long)size;
				base.LightLogSession.FlushProxyTraffic();
			}
			ProxySession proxySession = base.ProxySession;
			if (proxySession != null)
			{
				proxySession.SendBufferAsCommand(buf, offset, size);
				proxySession.SendToClient(base.BeginReadResponseItem);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006324 File Offset: 0x00004524
		private void LogCommand(ProtocolResponse response, string message)
		{
			LightWeightLogSession lightLogSession = base.LightLogSession;
			if (lightLogSession != null)
			{
				if (response.IsCommandFailedResponse)
				{
					lightLogSession.Result = message;
					return;
				}
				Pop3Response pop3Response = response as Pop3Response;
				if (pop3Response != null)
				{
					if (pop3Response.ResponseType == Pop3Response.Type.unknown)
					{
						lightLogSession.Result = Pop3Response.Type.ok.ToString();
						return;
					}
					lightLogSession.Result = pop3Response.ResponseType.ToString();
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006388 File Offset: 0x00004588
		private bool Is7BitCommand(byte[] buf, int offset, int size)
		{
			int num;
			int nextToken = BaseSession.GetNextToken(buf, offset, size, out num);
			return BaseSession.CompareArg(Pop3Session.PassBuf, buf, nextToken, num - nextToken) || base.Is7BitString(buf, offset, size);
		}

		// Token: 0x04000089 RID: 137
		public const string UserConfigurationName = "POP3.UserConfiguration";

		// Token: 0x0400008A RID: 138
		internal const string BannerDefault = "+OK {0}";

		// Token: 0x0400008B RID: 139
		internal const string Banner = "+OK {0} Microsoft POP3 MAIL Service, ready at {1}";

		// Token: 0x0400008C RID: 140
		internal const string BannerDebug = "+OK {0} Microsoft POP3 MAIL Service, Version: {1} ready at {2}";

		// Token: 0x0400008D RID: 141
		private const string PassLog = "PASS *****";

		// Token: 0x0400008E RID: 142
		internal static readonly byte[] PassBuf = Encoding.ASCII.GetBytes("pass");

		// Token: 0x0400008F RID: 143
		internal static readonly byte[] AuthBuf = Encoding.ASCII.GetBytes("auth");

		// Token: 0x04000090 RID: 144
		internal static readonly byte[] UserBuf = Encoding.ASCII.GetBytes("user");

		// Token: 0x04000091 RID: 145
		internal static readonly byte[] QuitBuf = Encoding.ASCII.GetBytes("quit");

		// Token: 0x04000092 RID: 146
		internal static readonly byte[] StatBuf = Encoding.ASCII.GetBytes("stat");

		// Token: 0x04000093 RID: 147
		internal static readonly byte[] StlsBuf = Encoding.ASCII.GetBytes("stls");

		// Token: 0x04000094 RID: 148
		internal static readonly byte[] ListBuf = Encoding.ASCII.GetBytes("list");

		// Token: 0x04000095 RID: 149
		internal static readonly byte[] RetrBuf = Encoding.ASCII.GetBytes("retr");

		// Token: 0x04000096 RID: 150
		internal static readonly byte[] RsetBuf = Encoding.ASCII.GetBytes("rset");

		// Token: 0x04000097 RID: 151
		internal static readonly byte[] DeleBuf = Encoding.ASCII.GetBytes("dele");

		// Token: 0x04000098 RID: 152
		internal static readonly byte[] NoopBuf = Encoding.ASCII.GetBytes("noop");

		// Token: 0x04000099 RID: 153
		internal static readonly byte[] TopBuf = Encoding.ASCII.GetBytes("top");

		// Token: 0x0400009A RID: 154
		internal static readonly byte[] UidlBuf = Encoding.ASCII.GetBytes("uidl");

		// Token: 0x0400009B RID: 155
		internal static readonly byte[] CapaBuf = Encoding.ASCII.GetBytes("capa");

		// Token: 0x0400009C RID: 156
		internal static readonly byte[] XprxBuf = Encoding.ASCII.GetBytes("xprx");

		// Token: 0x0400009D RID: 157
		private static readonly byte[] CommandTooLong = Encoding.ASCII.GetBytes("-ERR Protocol error. Connection is closed. 10\r\n");

		// Token: 0x0400009E RID: 158
		private static readonly byte[] PassLogBuf = Encoding.ASCII.GetBytes("PASS *****");

		// Token: 0x0400009F RID: 159
		private static readonly byte[] AuthBlob = Encoding.ASCII.GetBytes("<auth blob>");

		// Token: 0x040000A0 RID: 160
		private static readonly WorkloadSettings DefaultWorkloadSettings = new WorkloadSettings(WorkloadType.Pop, false);
	}
}
