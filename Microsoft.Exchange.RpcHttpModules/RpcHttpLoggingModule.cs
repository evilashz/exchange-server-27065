using System;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication.FederatedAuthService;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000015 RID: 21
	public class RpcHttpLoggingModule : RpcHttpModule
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003265 File Offset: 0x00001465
		public RpcHttpLoggingModule() : this(RpcHttpLoggingModule.GetDefaultLogger())
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003272 File Offset: 0x00001472
		internal RpcHttpLoggingModule(IExtensibleLogger logger)
		{
			this.logger = logger;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000032C9 File Offset: 0x000014C9
		internal override void InitializeModule(HttpApplication application)
		{
			application.BeginRequest += delegate(object sender, EventArgs args)
			{
				this.OnBeginRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
			application.PostAuthorizeRequest += delegate(object sender, EventArgs args)
			{
				this.OnPostAuthorizeRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
			application.EndRequest += delegate(object sender, EventArgs args)
			{
				this.OnEndRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003304 File Offset: 0x00001504
		internal override void OnBeginRequest(HttpContextBase context)
		{
			context.Response.AppendToLog("&RequestId=" + base.GetRequestId(context).ToString());
			this.LogRequest(context, RpcHttpLoggingModule.HttpPipelineStage.BeginRequest);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003343 File Offset: 0x00001543
		internal override void OnPostAuthorizeRequest(HttpContextBase context)
		{
			this.LogRequest(context, RpcHttpLoggingModule.HttpPipelineStage.PostAuthorizeRequest);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000334D File Offset: 0x0000154D
		internal override void OnEndRequest(HttpContextBase context)
		{
			this.LogRequest(context, RpcHttpLoggingModule.HttpPipelineStage.EndRequest);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003358 File Offset: 0x00001558
		private static IExtensibleLogger GetDefaultLogger()
		{
			if (RpcHttpLoggingModule.defaultLogger == null)
			{
				lock (RpcHttpLoggingModule.defaultLoggerLock)
				{
					if (RpcHttpLoggingModule.defaultLogger == null)
					{
						RpcHttpLoggingModule.defaultLogger = new RpcHttpLogger();
						AppDomain.CurrentDomain.DomainUnload += RpcHttpLoggingModule.DisposeDefaultLogger;
					}
				}
			}
			return RpcHttpLoggingModule.defaultLogger;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000033C4 File Offset: 0x000015C4
		private static void DisposeDefaultLogger(object sender, EventArgs e)
		{
			lock (RpcHttpLoggingModule.defaultLoggerLock)
			{
				if (RpcHttpLoggingModule.defaultLogger != null)
				{
					RpcHttpLoggingModule.defaultLogger.Dispose();
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003410 File Offset: 0x00001610
		private void LogRequest(HttpContextBase context, RpcHttpLoggingModule.HttpPipelineStage stage)
		{
			RpcHttpLogEvent rpcHttpLogEvent = new RpcHttpLogEvent(stage.ToString());
			rpcHttpLogEvent.UserName = this.GetUserName(context);
			rpcHttpLogEvent.OutlookSessionId = base.GetOutlookSessionId(context);
			if (stage == RpcHttpLoggingModule.HttpPipelineStage.EndRequest)
			{
				if (context.Items.Contains("ExtendedStatus"))
				{
					rpcHttpLogEvent.Status = string.Format("{0} ({1})", this.GetResponseStatusInfo(context.Response), context.Items["ExtendedStatus"]);
				}
				else
				{
					rpcHttpLogEvent.Status = this.GetResponseStatusInfo(context.Response);
				}
			}
			rpcHttpLogEvent.HttpVerb = context.Request.HttpMethod;
			rpcHttpLogEvent.UriQueryString = context.Request.Url.Query;
			rpcHttpLogEvent.ClientIp = context.Request.UserHostAddress;
			rpcHttpLogEvent.AuthType = this.GetAuthType(context);
			rpcHttpLogEvent.RpcHttpUserName = this.GetLogonUserName(context);
			rpcHttpLogEvent.ServerTarget = context.Request.Headers[WellKnownHeader.RpcHttpProxyServerTarget];
			rpcHttpLogEvent.FEServer = context.Request.Headers[WellKnownHeader.XFEServer];
			rpcHttpLogEvent.RequestId = base.GetRequestId(context).ToString();
			rpcHttpLogEvent.AssociationGuid = RpcHttpConnectionRegistrationModule.ExtractAssociationGuid(context.Request);
			this.logger.LogEvent(rpcHttpLogEvent);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003560 File Offset: 0x00001760
		private string GetUserName(HttpContextBase context)
		{
			string text = null;
			if (context.User != null)
			{
				text = (context.Items["WLID-MemberName"] as string);
				if (string.IsNullOrEmpty(text))
				{
					text = context.Request.ServerVariables["LOGON_USER"];
				}
			}
			return text;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000035AC File Offset: 0x000017AC
		private string GetLogonUserName(HttpContextBase context)
		{
			string result = null;
			try
			{
				string text = context.Request.Headers[WellKnownHeader.RpcHttpProxyLogonUserName];
				if (!string.IsNullOrEmpty(text))
				{
					result = Encoding.UTF8.GetString(Convert.FromBase64String(text));
				}
			}
			catch (FormatException)
			{
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003600 File Offset: 0x00001800
		private string GetAuthType(HttpContextBase context)
		{
			string text = context.Items["AuthType"] as string;
			if (string.IsNullOrEmpty(text))
			{
				text = AuthServiceHelper.GetAuthType(context.Request.Headers["Authorization"]);
				context.Items["AuthType"] = text;
			}
			return text;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003658 File Offset: 0x00001858
		private string GetResponseStatusInfo(HttpResponseBase response)
		{
			return string.Format("{0}.{1}.{2}", response.StatusCode, response.SubStatusCode, response.StatusDescription);
		}

		// Token: 0x0400003A RID: 58
		internal const string WlidMemberItemName = "WLID-MemberName";

		// Token: 0x0400003B RID: 59
		internal const string LogonUserVariableName = "LOGON_USER";

		// Token: 0x0400003C RID: 60
		internal const string AuthTypeItemName = "AuthType";

		// Token: 0x0400003D RID: 61
		private static readonly object defaultLoggerLock = new object();

		// Token: 0x0400003E RID: 62
		private static RpcHttpLogger defaultLogger = null;

		// Token: 0x0400003F RID: 63
		private readonly IExtensibleLogger logger;

		// Token: 0x02000016 RID: 22
		internal enum HttpPipelineStage
		{
			// Token: 0x04000041 RID: 65
			BeginRequest,
			// Token: 0x04000042 RID: 66
			PostAuthorizeRequest,
			// Token: 0x04000043 RID: 67
			EndRequest
		}
	}
}
