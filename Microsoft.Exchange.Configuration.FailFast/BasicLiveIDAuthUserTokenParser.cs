using System;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.FailFast;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Configuration.FailFast
{
	// Token: 0x02000003 RID: 3
	internal class BasicLiveIDAuthUserTokenParser : IUserTokenParser
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D0 File Offset: 0x000002D0
		private BasicLiveIDAuthUserTokenParser()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020D8 File Offset: 0x000002D8
		internal static BasicLiveIDAuthUserTokenParser Instance
		{
			get
			{
				return BasicLiveIDAuthUserTokenParser.instance;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E0 File Offset: 0x000002E0
		public bool TryParseUserToken(HttpContext context, out string userToken)
		{
			Logger.EnterFunction(ExTraceGlobals.FailFastModuleTracer, "BasicLiveIDAuthUserTokenParser.TryParseUserToken");
			userToken = null;
			if (context == null || context.Request == null)
			{
				Logger.TraceDebug(ExTraceGlobals.FailFastModuleTracer, "Context or Context.Request is null.", new object[0]);
				return false;
			}
			string text = context.Request.Headers["X-WLID-MemberName"];
			if (!string.IsNullOrEmpty(text))
			{
				userToken = text;
			}
			else
			{
				string text2 = context.Request.Headers["Authorization"];
				byte[] bytes;
				byte[] array;
				if (!LiveIdBasicAuthModule.ParseCredentials(context, text2, false, out bytes, out array))
				{
					Logger.TraceDebug(ExTraceGlobals.FailFastModuleTracer, "Auth header \"{0}\" is not Basic LiveID Auth format. Ignore this request in BasicLiveIDAuthUserTokenParser.", new object[]
					{
						text2
					});
					return false;
				}
				userToken = Encoding.ASCII.GetString(bytes);
			}
			Logger.TraceDebug(ExTraceGlobals.FailFastModuleTracer, "Parse auth header and get user token {0}.", new object[]
			{
				userToken
			});
			Logger.ExitFunction(ExTraceGlobals.FailFastModuleTracer, "BasicLiveIDAuthUserTokenParser.TryParseUserToken");
			return true;
		}

		// Token: 0x04000001 RID: 1
		private static BasicLiveIDAuthUserTokenParser instance = new BasicLiveIDAuthUserTokenParser();
	}
}
