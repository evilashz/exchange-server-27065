using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000156 RID: 342
	public static class ServiceCommonMetadataPublisher
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x000246F7 File Offset: 0x000228F7
		public static void PublishMetadata()
		{
			ServiceCommonMetadataPublisher.PublishMetadata(HttpContext.Current);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00024704 File Offset: 0x00022904
		public static void PublishMetadata(HttpContext context)
		{
			if (!ServiceCommonMetadataPublisher.isServiceCommonMetadataRegistered)
			{
				ActivityContext.RegisterMetadata(typeof(ServiceCommonMetadata));
				ServiceCommonMetadataPublisher.isServiceCommonMetadataRegistered = true;
			}
			HttpContextBase context2 = (context != null) ? new HttpContextWrapper(context) : null;
			ServiceCommonMetadataPublisher.PublishGeneric(context2);
			ServiceCommonMetadataPublisher.PublishAuthData(context2);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00024748 File Offset: 0x00022948
		internal static void SetResponseHeader(this HttpContext context, string source, string propertyName, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					string name = string.Format("{0}_{1}_{2}", "X-DEBUG", source, propertyName);
					context.Response.Headers[name] = value;
				}
				catch (SystemException ex)
				{
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					if (currentActivityScope != null)
					{
						currentActivityScope.AppendToProperty(ServiceCommonMetadata.GenericErrors, ex.Message);
					}
				}
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000247B4 File Offset: 0x000229B4
		internal static string GetContextItem(this HttpContext context, string key)
		{
			if (context == null)
			{
				return null;
			}
			return context.Items.GetContextItem(key);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000247C8 File Offset: 0x000229C8
		internal static void PublishServerInfo(IActivityScope activityScope)
		{
			if (activityScope == null)
			{
				return;
			}
			activityScope.SetProperty(ServiceCommonMetadata.ServerVersionMajor, ServiceCommonMetadataPublisher.VersionMajor);
			activityScope.SetProperty(ServiceCommonMetadata.ServerVersionMinor, ServiceCommonMetadataPublisher.VersionMinor);
			activityScope.SetProperty(ServiceCommonMetadata.ServerVersionBuild, ServiceCommonMetadataPublisher.VersionBuild);
			activityScope.SetProperty(ServiceCommonMetadata.ServerVersionRevision, ServiceCommonMetadataPublisher.VersionRevision);
			activityScope.SetProperty(ServiceCommonMetadata.ServerHostName, ServiceCommonMetadataPublisher.MachineName);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00024834 File Offset: 0x00022A34
		internal static Dictionary<Enum, string> GetAuthValues(this HttpContextBase context)
		{
			Dictionary<Enum, string> dictionary = new Dictionary<Enum, string>(9);
			if (context != null)
			{
				IDictionary items = context.Items;
				HttpRequestBase request = context.Request;
				dictionary.SetNonNullValue(ServiceCommonMetadata.IsAuthenticated, request.IsAuthenticated ? "true" : "false");
				string text = items.GetContextItem("AuthType");
				string contextItem = items.GetContextItem("AuthModuleLatency");
				string text2 = items.GetContextItem("WLID-MemberName");
				string contextItem2 = items.GetContextItem("AuthenticatedUserOrganization");
				if (string.IsNullOrEmpty(text) || text == "Unknown")
				{
					text = context.GetAuthType();
				}
				if (string.IsNullOrEmpty(text))
				{
					text = request.GetRequestHeader("Authorization");
					if (!string.IsNullOrEmpty(text))
					{
						text = text.Split(new char[]
						{
							' '
						})[0];
					}
				}
				if (string.IsNullOrEmpty(text2))
				{
					if (!string.IsNullOrEmpty(text2 = items.GetContextItem("LiveIdNegotiateMemberName")))
					{
						text = text + ";" + items.GetContextItem("NegoCap");
					}
					else if (string.IsNullOrEmpty(text2 = items.GetContextItem("RPSMemberName")))
					{
						if (!string.IsNullOrEmpty(text2 = items.GetContextItem("AuthenticatedUser")))
						{
							if (text2.Contains("\\"))
							{
								text2 = text2.Split(new char[]
								{
									'\\'
								})[1];
							}
						}
						else
						{
							text2 = context.GetAuthUser();
						}
					}
				}
				dictionary.SetNonNullValue(ActivityStandardMetadata.AuthenticationType, text);
				dictionary.SetNonNullValue(ServiceLatencyMetadata.AuthModuleLatency, contextItem);
				dictionary.SetNonNullValue(ServiceCommonMetadata.LiveIdBasicLog, items.GetContextItem("LiveIdBasicLog"));
				dictionary.SetNonNullValue(ServiceCommonMetadata.LiveIdBasicError, items.GetContextItem("LiveIdBasicError"));
				dictionary.SetNonNullValue(ServiceCommonMetadata.LiveIdNegotiateError, items.GetContextItem("LiveIdNegotiateError"));
				dictionary.SetNonNullValue(ServiceCommonMetadata.OAuthToken, items.GetContextItem("OAuthToken"));
				dictionary.SetNonNullValue(ServiceCommonMetadata.OAuthError, items.GetContextItem("OAuthError"));
				dictionary.SetNonNullValue(ServiceCommonMetadata.OAuthErrorCategory, items.GetContextItem("OAuthErrorCategory"));
				dictionary.SetNonNullValue(ServiceCommonMetadata.OAuthExtraInfo, items.GetContextItem("OAuthExtraInfo"));
				dictionary.SetNonNullValue(ServiceCommonMetadata.AuthenticatedUser, text2);
				dictionary.SetNonNullValue(ActivityStandardMetadata.TenantId, contextItem2);
				dictionary.SetNonNullValue(ActivityStandardMetadata.Puid, items.GetContextItem("PassportUniqueId"));
			}
			return dictionary;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00024A90 File Offset: 0x00022C90
		private static string GetAuthType(this HttpContextBase context)
		{
			if (context != null)
			{
				try
				{
					if (context.User != null && context.User.Identity.IsAuthenticated)
					{
						string authenticationType = context.User.Identity.AuthenticationType;
						if (!string.IsNullOrEmpty(authenticationType))
						{
							return authenticationType;
						}
					}
				}
				catch (SystemException ex)
				{
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					if (currentActivityScope != null)
					{
						currentActivityScope.AppendToProperty(ServiceCommonMetadata.GenericErrors, ex.Message);
					}
				}
			}
			return null;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00024B0C File Offset: 0x00022D0C
		private static string GetAuthUser(this HttpContextBase context)
		{
			if (context != null)
			{
				try
				{
					if (context.User != null && context.User.Identity.IsAuthenticated)
					{
						string name = context.User.Identity.Name;
						if (!string.IsNullOrEmpty(name))
						{
							return name;
						}
					}
				}
				catch (SystemException ex)
				{
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					if (currentActivityScope != null)
					{
						currentActivityScope.AppendToProperty(ServiceCommonMetadata.GenericErrors, ex.Message);
					}
				}
			}
			return null;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00024B88 File Offset: 0x00022D88
		private static void PublishGeneric(HttpContextBase context)
		{
			IActivityScope currentActivityScope = ServiceCommonMetadataPublisher.GetCurrentActivityScope(context);
			if (currentActivityScope == null)
			{
				return;
			}
			ServiceCommonMetadataPublisher.PublishServerInfo(currentActivityScope);
			if (context == null)
			{
				return;
			}
			HttpRequestBase request = context.Request;
			HttpResponseBase response = context.Response;
			string value = request.GetRequestHeader("X-Forwarded-For");
			if (string.IsNullOrEmpty(value))
			{
				value = request.UserHostAddress;
			}
			currentActivityScope.SetProperty(ServiceCommonMetadata.ClientIpAddress, value);
			currentActivityScope.SetProperty(ActivityStandardMetadata.ClientInfo, request.UserAgent);
			currentActivityScope.SetProperty(ServiceCommonMetadata.RequestSize, request.ContentLength.ToString());
			if (currentActivityScope.GetProperty(ServiceCommonMetadata.HttpStatus) == null)
			{
				currentActivityScope.SetProperty(ServiceCommonMetadata.HttpStatus, response.StatusCode.ToString());
			}
			if (request.Cookies.Count > 0)
			{
				for (int i = 0; i < request.Cookies.Count; i++)
				{
					if (string.Equals(request.Cookies[i].Name, "exchangecookie", StringComparison.OrdinalIgnoreCase))
					{
						currentActivityScope.SetProperty(ServiceCommonMetadata.Cookie, request.Cookies[i].Value);
						return;
					}
				}
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00024CA0 File Offset: 0x00022EA0
		private static void PublishAuthData(HttpContextBase context)
		{
			if (context == null)
			{
				return;
			}
			IActivityScope currentActivityScope = ServiceCommonMetadataPublisher.GetCurrentActivityScope(context);
			if (currentActivityScope == null)
			{
				return;
			}
			Dictionary<Enum, string> authValues = context.GetAuthValues();
			foreach (KeyValuePair<Enum, string> keyValuePair in authValues)
			{
				currentActivityScope.SetProperty(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00024D14 File Offset: 0x00022F14
		private static IActivityScope GetCurrentActivityScope(HttpContextBase context)
		{
			IActivityScope activityScope = null;
			if (context != null)
			{
				activityScope = (context.Items[typeof(ActivityScope)] as IActivityScope);
			}
			if (activityScope == null)
			{
				activityScope = ActivityContext.GetCurrentActivityScope();
			}
			return activityScope;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00024D4C File Offset: 0x00022F4C
		private static string GetContextItem(this IDictionary items, string key)
		{
			if (items != null)
			{
				object obj = items[key];
				if (obj != null)
				{
					return obj.ToString();
				}
			}
			return null;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00024D70 File Offset: 0x00022F70
		private static string GetRequestHeader(this HttpRequestBase request, string key)
		{
			if (request != null)
			{
				try
				{
					return request.Headers[key];
				}
				catch (SystemException ex)
				{
					IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
					if (currentActivityScope != null)
					{
						currentActivityScope.AppendToProperty(ServiceCommonMetadata.GenericErrors, ex.Message);
					}
				}
			}
			return null;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00024DC4 File Offset: 0x00022FC4
		private static void SetNonNullValue(this Dictionary<Enum, string> dictionary, Enum key, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				dictionary.Add(key, value);
			}
		}

		// Token: 0x040006B0 RID: 1712
		public const string DebugHeaderPrefix = "X-DEBUG";

		// Token: 0x040006B1 RID: 1713
		private const string TrackingCookieName = "exchangecookie";

		// Token: 0x040006B2 RID: 1714
		private const string OriginatingClientIpHeader = "X-Forwarded-For";

		// Token: 0x040006B3 RID: 1715
		private static readonly string VersionMajor = 15.ToString();

		// Token: 0x040006B4 RID: 1716
		private static readonly string VersionMinor = 0.ToString();

		// Token: 0x040006B5 RID: 1717
		private static readonly string VersionBuild = 1497.ToString();

		// Token: 0x040006B6 RID: 1718
		private static readonly string VersionRevision = 10.ToString();

		// Token: 0x040006B7 RID: 1719
		private static readonly string MachineName = Environment.MachineName;

		// Token: 0x040006B8 RID: 1720
		private static bool isServiceCommonMetadataRegistered = false;
	}
}
