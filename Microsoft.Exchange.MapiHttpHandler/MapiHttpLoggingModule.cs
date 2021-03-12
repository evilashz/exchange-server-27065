using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.MapiHttp;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001D RID: 29
	public sealed class MapiHttpLoggingModule : MapiHttpModule
	{
		// Token: 0x06000146 RID: 326 RVA: 0x000077E4 File Offset: 0x000059E4
		public MapiHttpLoggingModule()
		{
			this.perfDateTime = new PerfDateTime();
			this.logLine = new StringBuilder();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000787C File Offset: 0x00005A7C
		internal override void InitializeModule(HttpApplication application)
		{
			application.BeginRequest += delegate(object sender, EventArgs args)
			{
				this.OnBeginRequest(MapiHttpContextWrapper.GetWrapper(((HttpApplication)sender).Context));
			};
			application.PostAuthorizeRequest += delegate(object sender, EventArgs args)
			{
				this.OnPostAuthorizeRequest(MapiHttpContextWrapper.GetWrapper(((HttpApplication)sender).Context));
			};
			application.PreRequestHandlerExecute += delegate(object sender, EventArgs args)
			{
				this.OnPreRequestHandlerExecute(MapiHttpContextWrapper.GetWrapper(((HttpApplication)sender).Context));
			};
			application.PostRequestHandlerExecute += delegate(object sender, EventArgs args)
			{
				this.OnPostRequestHandlerExecute(MapiHttpContextWrapper.GetWrapper(((HttpApplication)sender).Context));
			};
			application.EndRequest += delegate(object sender, EventArgs args)
			{
				this.OnEndRequest(MapiHttpContextWrapper.GetWrapper(((HttpApplication)sender).Context));
			};
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000078E3 File Offset: 0x00005AE3
		internal override void OnBeginRequest(HttpContextBase context)
		{
			this.beginRequestTime = new DateTime?(this.perfDateTime.UtcNow);
			context.Items["MapiHttpLoggingModuleLogger"] = this.logLine;
			this.SetDefaultResponseHeaders(context);
			this.AppendLogRequestInfo(context);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000791F File Offset: 0x00005B1F
		internal override void OnPostAuthorizeRequest(HttpContextBase context)
		{
			this.AppendAuthInfo(context);
			this.postAuthorizeRequestTime = new DateTime?(this.perfDateTime.UtcNow);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000793E File Offset: 0x00005B3E
		internal override void OnPreRequestHandlerExecute(HttpContextBase context)
		{
			this.preRequestHandlerExecuteTime = new DateTime?(this.perfDateTime.UtcNow);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007956 File Offset: 0x00005B56
		internal override void OnPostRequestHandlerExecute(HttpContextBase context)
		{
			this.postRequestHandlerExecuteTime = new DateTime?(this.perfDateTime.UtcNow);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007970 File Offset: 0x00005B70
		internal override void OnEndRequest(HttpContextBase context)
		{
			if (this.beginRequestTime != null)
			{
				this.logLine.AppendFormat("&Stage=BeginRequest:{0}", this.beginRequestTime.Value.ToString("o"));
				if (this.postAuthorizeRequestTime != null)
				{
					this.logLine.AppendFormat(";PostAuthorizeRequest:{0}", this.postAuthorizeRequestTime.Value.ToString("o"));
				}
				if (this.preRequestHandlerExecuteTime != null)
				{
					this.logLine.AppendFormat(";PreRequestHandlerExecute:{0}", this.preRequestHandlerExecuteTime.Value.ToString("o"));
				}
				if (this.postRequestHandlerExecuteTime != null)
				{
					this.logLine.AppendFormat(";PostRequestHandlerExecute:{0}", this.postRequestHandlerExecuteTime.Value.ToString("o"));
				}
				this.logLine.AppendFormat(";EndRequest:{0}", this.perfDateTime.UtcNow.ToString("o"));
			}
			else
			{
				this.logLine.AppendFormat("&Stage=EndRequest:{0}", this.perfDateTime.UtcNow.ToString("o"));
			}
			context.Response.AppendToLog(this.logLine.ToString());
			this.logLine.Clear();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007AD0 File Offset: 0x00005CD0
		private void AppendLogRequestInfo(HttpContextBase context)
		{
			string text;
			context.TryGetSourceCafeServer(out text);
			string text2;
			context.TryGetCafeActivityId(out text2);
			string clientRequestInfo = MapiHttpEndpoints.GetClientRequestInfo(context.Request.Headers);
			this.logLine.Append("&FrontEnd=");
			this.logLine.Append(string.IsNullOrEmpty(text) ? "<null>" : text);
			this.logLine.Append("&RequestId=");
			this.logLine.Append(string.IsNullOrEmpty(text2) ? "<null>" : text2);
			this.logLine.Append("&ClientRequestInfo=");
			this.logLine.Append(clientRequestInfo);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007B78 File Offset: 0x00005D78
		private void AppendAuthInfo(HttpContextBase context)
		{
			Dictionary<Enum, string> authValues = context.GetAuthValues();
			if (authValues.Count > 0)
			{
				this.logLine.Append("&AuthInfo=");
				foreach (KeyValuePair<Enum, string> keyValuePair in authValues)
				{
					this.logLine.Append(keyValuePair.Key);
					this.logLine.Append(":");
					this.logLine.Append(keyValuePair.Value);
					this.logLine.Append(";");
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007C2C File Offset: 0x00005E2C
		private void SetDefaultResponseHeaders(HttpContextBase context)
		{
			context.SetServerVersion();
			string text;
			if (context.TryGetRequestId(out text) && !string.IsNullOrEmpty(text))
			{
				context.SetRequestId(text);
			}
			string text2;
			if (context.TryGetClientInfo(out text2) && !string.IsNullOrEmpty(text2))
			{
				context.SetClientInfo(text2);
			}
			string text3;
			if (context.TryGetRequestType(out text3) && !string.IsNullOrEmpty(text3))
			{
				context.SetRequestType(text3);
			}
		}

		// Token: 0x040000A0 RID: 160
		public const string AdditionalInfoLogger = "MapiHttpLoggingModuleLogger";

		// Token: 0x040000A1 RID: 161
		private const string ClientRequestInfoLogParameter = "&ClientRequestInfo=";

		// Token: 0x040000A2 RID: 162
		private const string FrontEndLogParameter = "&FrontEnd=";

		// Token: 0x040000A3 RID: 163
		private const string RequestIdLogParameter = "&RequestId=";

		// Token: 0x040000A4 RID: 164
		private const string AuthInfoLogParameter = "&AuthInfo=";

		// Token: 0x040000A5 RID: 165
		private readonly PerfDateTime perfDateTime;

		// Token: 0x040000A6 RID: 166
		private DateTime? beginRequestTime;

		// Token: 0x040000A7 RID: 167
		private DateTime? postAuthorizeRequestTime;

		// Token: 0x040000A8 RID: 168
		private DateTime? preRequestHandlerExecuteTime;

		// Token: 0x040000A9 RID: 169
		private DateTime? postRequestHandlerExecuteTime;

		// Token: 0x040000AA RID: 170
		private StringBuilder logLine;
	}
}
