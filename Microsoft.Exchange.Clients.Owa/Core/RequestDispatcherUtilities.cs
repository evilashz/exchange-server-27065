using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000238 RID: 568
	internal static class RequestDispatcherUtilities
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x000770CC File Offset: 0x000752CC
		internal static ApplicationElement GetApplicationElement(HttpRequest httpRequest)
		{
			ApplicationElement result = ApplicationElement.StartPage;
			string queryStringParameter = Utilities.GetQueryStringParameter(httpRequest, "ae", false);
			if (!string.IsNullOrEmpty(queryStringParameter))
			{
				object obj = FormsRegistry.ApplicationElementParser.Parse(httpRequest.QueryString["ae"]);
				if (obj == null)
				{
					throw new OwaInvalidRequestException("Invalid application element");
				}
				result = (ApplicationElement)obj;
			}
			return result;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00077121 File Offset: 0x00075321
		internal static FormValue DoFormsRegistryLookup(ISessionContext sessionContext, ApplicationElement applicationElement, string type, string action, string state)
		{
			return RequestDispatcherUtilities.DoFormsRegistryLookup(sessionContext.Experiences, sessionContext.SegmentationFlags, applicationElement, type, action, state);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0007713C File Offset: 0x0007533C
		internal static FormValue DoFormsRegistryLookup(Experience[] experiences, ulong segmentationFlags, ApplicationElement applicationElement, string type, string action, string state)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "RequestDispatcher.DoFormsRegistryLookup");
			if (type == null)
			{
				type = string.Empty;
			}
			if (state == null)
			{
				state = string.Empty;
			}
			if (action == null)
			{
				action = string.Empty;
			}
			FormKey formKey = new FormKey(applicationElement, type, action, state);
			FormValue formValue = FormsRegistryManager.LookupForm(formKey, experiences);
			if (formValue == null)
			{
				return null;
			}
			if ((formValue.SegmentationFlags & segmentationFlags) != formValue.SegmentationFlags)
			{
				formKey.Action = action;
				formKey.State = state;
				formKey.Class = "Disabled";
				formValue = FormsRegistryManager.LookupForm(formKey, experiences);
			}
			if (formValue == null)
			{
				return null;
			}
			return formValue;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000771CC File Offset: 0x000753CC
		internal static void LookupExperiencesForRequest(OwaContext owaContext, bool isOptimizedForAccessibility, bool isRichClientFeatureEnabled, out BrowserType browserType, out UserAgentParser.UserAgentVersion browserVersion, out Experience[] experiences)
		{
			string application = string.Empty;
			string empty = string.Empty;
			browserType = BrowserType.Other;
			browserVersion = default(UserAgentParser.UserAgentVersion);
			if (isOptimizedForAccessibility || RequestDispatcherUtilities.ShouldDoBasicRegistryLookup(owaContext))
			{
				UserAgentParser.Parse(string.Empty, out application, out browserVersion, out empty);
			}
			else
			{
				UserAgentParser.Parse(owaContext.HttpContext.Request.UserAgent, out application, out browserVersion, out empty);
			}
			browserType = Utilities.GetBrowserType(owaContext.HttpContext.Request.UserAgent);
			if (browserType == BrowserType.Other)
			{
				application = "Safari";
				browserVersion = new UserAgentParser.UserAgentVersion(3, 0, 0);
				empty = string.Empty;
				browserType = BrowserType.Safari;
			}
			experiences = FormsRegistryManager.LookupExperiences(application, browserVersion, empty, ClientControl.None, isRichClientFeatureEnabled);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00077275 File Offset: 0x00075475
		internal static bool ShouldDoBasicRegistryLookup(OwaContext owaContext)
		{
			return (RequestDispatcherUtilities.IsDownLevelClient(owaContext.HttpContext, false) && owaContext.RequestType != OwaRequestType.Attachment && owaContext.RequestType != OwaRequestType.WebReadyRequest && owaContext.RequestType != OwaRequestType.Aspx) || owaContext.RequestType == OwaRequestType.WebPart;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000772B0 File Offset: 0x000754B0
		internal static void SetProxyRequestUrl(OwaContext owaContext)
		{
			UriBuilder uriBuilder = new UriBuilder(owaContext.HttpContext.Request.Url);
			Uri uri = uriBuilder.Uri;
			uriBuilder = new UriBuilder(owaContext.SecondCasUri.Uri);
			uriBuilder.Path = uri.AbsolutePath;
			string text = uri.Query;
			if (text.StartsWith("?", StringComparison.Ordinal))
			{
				text = text.Substring(1, text.Length - 1);
			}
			uriBuilder.Query = text;
			Uri uri2 = uriBuilder.Uri;
			OwaDiagnostics.TracePfd(23177, "The request will be proxied to \"{0}\"", new object[]
			{
				uri2
			});
			owaContext.SetInternalHandlerParameter("pru", uri2);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00077358 File Offset: 0x00075558
		internal static void RespondProxyPing(OwaContext owaContext)
		{
			owaContext.HttpContext.Response.AppendHeader("X-OWA-Version", Globals.ApplicationVersion);
			owaContext.HttpStatusCode = (HttpStatusCode)242;
		}

		// Token: 0x04000D34 RID: 3380
		internal const string DisabledItemType = "Disabled";
	}
}
