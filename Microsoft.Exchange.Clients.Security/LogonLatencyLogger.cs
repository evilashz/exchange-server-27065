using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200002B RID: 43
	internal static class LogonLatencyLogger
	{
		// Token: 0x0600014E RID: 334 RVA: 0x0000A02C File Offset: 0x0000822C
		static LogonLatencyLogger()
		{
			try
			{
				bool.TryParse(ConfigurationManager.AppSettings["LogLiveLogonLatency"], out LogonLatencyLogger.loggingEnabled);
				if (LogonLatencyLogger.loggingEnabled)
				{
					ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceDebug(0L, "Logon latency key was set to true in web.config, logging will take place");
				}
				else
				{
					ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceDebug(0L, "Logon latency key was set to false or not found in web.config, logging will not take place");
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<string, string>(0L, "Unexpected exception in static constructor of LogonLatencyLogger. Exception message: {0}. Stack trace: {1}", ex.Message, ex.StackTrace);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A0B0 File Offset: 0x000082B0
		internal static void AddLatencyHeader(HttpContext httpContext, string header, long latency)
		{
			if (httpContext == null || httpContext.Response == null || !UserAgentUtilities.IsMonitoringRequest(httpContext.Request.UserAgent))
			{
				return;
			}
			try
			{
				httpContext.Response.AppendHeader(header, latency.ToString());
			}
			catch (HttpException arg)
			{
				ExTraceGlobals.PerformanceTracer.TraceDebug<HttpException>(0L, "Exception happened while trying to append latency headers. Exception will be ignored: {0}", arg);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A118 File Offset: 0x00008318
		internal static void CreateCookie(HttpContext httpContext, string logonStep)
		{
			try
			{
				if (LogonLatencyLogger.loggingEnabled)
				{
					LogonLatencyLogger.SetCookie(httpContext, "logonLatency", string.Format("{0}={1}", logonStep, DateTime.UtcNow.Ticks.ToString()), Utilities.GetOutlookDotComDomain(httpContext.Request.GetRequestUrlEvenIfProxied().Host));
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<string, string>(0L, "Unexpected exception while creating logon latency cookie. Exception message: {0}. Stack trace: {1}", ex.Message, ex.StackTrace);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A1A0 File Offset: 0x000083A0
		internal static void UpdateCookie(HttpContext httpContext, string action, DateTime checkPointDateTime)
		{
			try
			{
				if (LogonLatencyLogger.loggingEnabled)
				{
					DateTime utcNow = DateTime.UtcNow;
					long latency = Convert.ToInt64((utcNow - checkPointDateTime).TotalMilliseconds);
					if (action.Equals("ADL"))
					{
						LogonLatencyLogger.AddLatencyHeader(httpContext, "X-AuthDiagInfoLdapLatency", latency);
					}
					else
					{
						LogonLatencyLogger.AddLatencyHeader(httpContext, "X-AuthDiagInfoMservLookupLatency", latency);
					}
					string text = string.Empty;
					string cookieValue = string.Empty;
					if (httpContext.Request.Cookies["logonLatency"] != null && httpContext.Request.Cookies["logonLatency"].Value != null)
					{
						text = LogonLatencyLogger.GetLatestCookieValue(httpContext);
						if (action.Equals("ADL") || action.Equals("MSERVL"))
						{
							int num = 1;
							if (!string.IsNullOrEmpty(text))
							{
								while (text.Contains(action + num))
								{
									num++;
								}
							}
							action += num;
						}
						cookieValue = string.Format("{0}&{1}={2}", text, action, latency.ToString());
						LogonLatencyLogger.SetCookie(httpContext, "logonLatency", cookieValue, Utilities.GetOutlookDotComDomain(httpContext.Request.GetRequestUrlEvenIfProxied().Host));
					}
					else
					{
						string arg = "adlkupltncy";
						if (action.Equals("MSERVL"))
						{
							arg = "mservlkupltncy";
						}
						httpContext.Response.AppendToLog(string.Format("&{0}={1}", arg, latency.ToString()));
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<string, string>(0L, "Unexpected exception while updating logon latency cookie. Exception message: {0}. Stack trace: {1}", ex.Message, ex.StackTrace);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A34C File Offset: 0x0000854C
		internal static void UpdateCookie(HttpContext httpContext, string logonStep)
		{
			try
			{
				if (LogonLatencyLogger.loggingEnabled)
				{
					string arg = string.Empty;
					string cookieValue = string.Empty;
					if (httpContext.Request.Cookies["logonLatency"] != null && httpContext.Request.Cookies["logonLatency"].Value != null)
					{
						arg = LogonLatencyLogger.GetLatestCookieValue(httpContext);
						cookieValue = string.Format("{0}&{1}={2}", arg, logonStep, DateTime.UtcNow.Ticks.ToString());
						LogonLatencyLogger.SetCookie(httpContext, "logonLatency", cookieValue, Utilities.GetOutlookDotComDomain(httpContext.Request.GetRequestUrlEvenIfProxied().Host));
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<string, string>(0L, "Unexpected exception while updating logon latency cookie. Exception message: {0}. Stack trace: {1}", ex.Message, ex.StackTrace);
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A424 File Offset: 0x00008624
		internal static void WriteLatencyToIISLogAndDeleteCookie(HttpContext httpContext)
		{
			try
			{
				if (LogonLatencyLogger.loggingEnabled)
				{
					long num = -1L;
					long num2 = -1L;
					NameValueCollection nameValueCollection = new NameValueCollection();
					StringBuilder stringBuilder = new StringBuilder();
					if (httpContext.Request.Cookies["logonLatency"] != null && httpContext.Request.Cookies["logonLatency"].Value != null)
					{
						nameValueCollection = HttpUtility.ParseQueryString(LogonLatencyLogger.GetLatestCookieValue(httpContext));
						nameValueCollection["LGN04"] = DateTime.UtcNow.Ticks.ToString();
						LogonLatencyLogger.AssignLatency(nameValueCollection, "LGN02", "LGN01", ref num);
						LogonLatencyLogger.AssignLatency(nameValueCollection, "LGN04", "LGN02", ref num2);
						stringBuilder.AppendFormat("&livelgnltncy={0}&resubrpsltncy={1}", num.ToString(), num2.ToString());
						for (int i = 1; i <= 5; i++)
						{
							if (nameValueCollection["ADL" + i] != null)
							{
								stringBuilder.AppendFormat("&adlkupltncy{0}={1}", i, nameValueCollection["ADL" + i]);
							}
						}
						for (int j = 1; j <= 3; j++)
						{
							if (nameValueCollection["MSERVL" + j] != null)
							{
								stringBuilder.AppendFormat("&mservlkupltncy{0}={1}", j, nameValueCollection["MSERVL" + j]);
							}
						}
					}
					if (!stringBuilder.ToString().Contains("adlkupltncy1"))
					{
						stringBuilder.AppendFormat("&adlkupltncy1=-1", new object[0]);
					}
					httpContext.Response.AppendToLog(stringBuilder.ToString());
					httpContext.Items["logonLatency"] = stringBuilder.ToString();
					LogonLatencyLogger.DeleteCookie(httpContext);
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<string, string>(0L, "Unexpected exception while writing logon latency to IIS. Exception message: {0}. Stack trace: {1}", ex.Message, ex.StackTrace);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A62C File Offset: 0x0000882C
		internal static void LogProfileReadLatency(HttpContext httpContext, TimeSpan latency)
		{
			if (!LogonLatencyLogger.loggingEnabled)
			{
				return;
			}
			try
			{
				httpContext.Response.AppendToLog(string.Format("&lvidprflrd={0}", latency.TotalMilliseconds));
			}
			catch (Exception arg)
			{
				ExTraceGlobals.PerformanceTracer.TraceDebug<Exception>(0L, "Exception happened while trying to Log Time In ProfileService. Exception will be ignored: {0}", arg);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A68C File Offset: 0x0000888C
		internal static void LogTimeInLiveIdModule(HttpContext httpContext)
		{
			if (!LogonLatencyLogger.loggingEnabled)
			{
				return;
			}
			try
			{
				DateTime? dateTime = httpContext.Items["RequestStartTime"] as DateTime?;
				if (dateTime != null && dateTime != null)
				{
					DateTime utcNow = DateTime.UtcNow;
					long latency = Convert.ToInt64((utcNow - dateTime.Value).TotalMilliseconds);
					long latency2 = -1L;
					DateTime? dateTime2 = httpContext.Items["AdRequestStartTime"] as DateTime?;
					DateTime? dateTime3 = httpContext.Items["AdRequestEndTime"] as DateTime?;
					if (dateTime2 != null && dateTime2 != null && dateTime3 != null && dateTime3 != null)
					{
						latency2 = Convert.ToInt64((dateTime3.Value - dateTime2.Value).TotalMilliseconds);
					}
					httpContext.Response.AppendToLog(string.Format("&lvidmdlltncy={0}&lvidadlookupltncy={1}", latency.ToString(), latency2.ToString()));
					LogonLatencyLogger.AddLatencyHeader(httpContext, "X-AuthDiagInfoLiveIdModuleLatency", latency);
					LogonLatencyLogger.AddLatencyHeader(httpContext, "X-AuthDiagInfoLiveIdModuleAdLookupLatency", latency2);
					httpContext.Items["RequestStartTime"] = null;
				}
			}
			catch (Exception arg)
			{
				ExTraceGlobals.PerformanceTracer.TraceDebug<Exception>(0L, "Exception happened while trying to Log Time In LiveIdModule. Exception will be ignored: {0}", arg);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A7FC File Offset: 0x000089FC
		private static void AssignLatency(NameValueCollection values, string logonCurrentStepTicks, string logonPreviousStepTicks, ref long currentValue)
		{
			try
			{
				if (values[logonCurrentStepTicks] != null && values[logonPreviousStepTicks] != null)
				{
					TimeSpan timeSpan = new TimeSpan(Convert.ToInt64(values[logonCurrentStepTicks]) - Convert.ToInt64(values[logonPreviousStepTicks]));
					currentValue = Convert.ToInt64(timeSpan.TotalMilliseconds);
				}
				else
				{
					currentValue = 0L;
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<string, string>(0L, "Unexpected exception while computing logon latency. Exception message: {0}. Stack trace: {1}", ex.Message, ex.StackTrace);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A884 File Offset: 0x00008A84
		private static void SetCookie(HttpContext httpContext, string cookieName, string cookieValue, string cookieDomain)
		{
			HttpCookie httpCookie = httpContext.Response.Cookies.Get(cookieName);
			httpCookie.HttpOnly = true;
			httpCookie.Path = "/";
			httpCookie.Value = cookieValue;
			if (cookieDomain != null)
			{
				httpCookie.Domain = cookieDomain;
			}
			httpContext.Response.Cookies.Set(httpCookie);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A8D8 File Offset: 0x00008AD8
		private static string GetLatestCookieValue(HttpContext httpContext)
		{
			if (httpContext.Response.Cookies["logonLatency"] != null && httpContext.Response.Cookies["logonLatency"].Value != null)
			{
				return httpContext.Response.Cookies["logonLatency"].Value;
			}
			return httpContext.Request.Cookies["logonLatency"].Value;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A950 File Offset: 0x00008B50
		private static void DeleteCookie(HttpContext httpContext)
		{
			if (httpContext.Request.Cookies["logonLatency"] != null)
			{
				httpContext.Request.Cookies["logonLatency"].Expires = DateTime.UtcNow.AddDays(-100.0);
				httpContext.Request.Cookies["logonLatency"].Domain = Utilities.GetOutlookDotComDomain(httpContext.Request.GetRequestUrlEvenIfProxied().Host);
				httpContext.Response.Cookies.Set(httpContext.Request.Cookies["logonLatency"]);
			}
		}

		// Token: 0x04000157 RID: 343
		internal const string LogonLatencyCookieName = "logonLatency";

		// Token: 0x04000158 RID: 344
		internal const string LogLiveLogonLatencyKey = "LogLiveLogonLatency";

		// Token: 0x04000159 RID: 345
		internal const string RequestStartTimeKey = "RequestStartTime";

		// Token: 0x0400015A RID: 346
		internal const string AdRequestStartTimeKey = "AdRequestStartTime";

		// Token: 0x0400015B RID: 347
		internal const string AdRequestEndTimeKey = "AdRequestEndTime";

		// Token: 0x0400015C RID: 348
		internal const string BeforeLogon = "LGN01";

		// Token: 0x0400015D RID: 349
		internal const string RPSTicketReceived = "LGN02";

		// Token: 0x0400015E RID: 350
		internal const string LiveAuthenticationCompleted = "LGN04";

		// Token: 0x0400015F RID: 351
		internal const string ADLookupLatency = "ADL";

		// Token: 0x04000160 RID: 352
		internal const int ADLookupLatencyCount = 5;

		// Token: 0x04000161 RID: 353
		internal const string MservLookupLatency = "MSERVL";

		// Token: 0x04000162 RID: 354
		internal const int MservLookupLatencyCount = 3;

		// Token: 0x04000163 RID: 355
		internal const string ADLookupLatencyHeader = "X-AuthDiagInfoLdapLatency";

		// Token: 0x04000164 RID: 356
		internal const string MservLookupLatencyHeader = "X-AuthDiagInfoMservLookupLatency";

		// Token: 0x04000165 RID: 357
		private static bool loggingEnabled;
	}
}
