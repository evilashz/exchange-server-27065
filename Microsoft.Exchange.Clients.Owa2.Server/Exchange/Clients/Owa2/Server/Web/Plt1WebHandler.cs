﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000484 RID: 1156
	public class Plt1WebHandler : IHttpHandler
	{
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x0008D8D8 File Offset: 0x0008BAD8
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0008D8DC File Offset: 0x0008BADC
		public void ProcessRequest(HttpContext context)
		{
			string userContextId = Plt1WebHandler.GetUserContextId(context);
			bool isMowa = OfflineClientRequestUtilities.IsRequestFromMOWAClient(context.Request, context.Request.UserAgent);
			HttpRequest request = context.Request;
			string clientAddressWithoutPII = this.GetClientAddressWithoutPII(request);
			UserAgent userAgent = OwaUserAgentUtilities.CreateUserAgentWithLayoutOverride(context);
			string userName = string.Empty;
			string cookieValueAndSetIfNull = ClientIdCookie.GetCookieValueAndSetIfNull(context);
			UserContext userContext = UserContextManager.GetUserContext(context, false);
			if (userContext != null && userContext.LogonIdentity != null)
			{
				SmtpAddress primarySmtpAddress = userContext.LogonIdentity.PrimarySmtpAddress;
				userName = userContext.LogonIdentity.PrimarySmtpAddress.ToString();
			}
			if (Plt1WebHandler.IsPlt1PerformanceRequest(request))
			{
				string text = "";
				if (context.Request.HttpMethod == "POST")
				{
					using (StreamReader streamReader = new StreamReader(context.Request.InputStream))
					{
						text = streamReader.ReadToEnd();
						context.Response.AppendToLog(text);
					}
				}
				string clientVersion = context.Request.QueryString.Get("cver") ?? string.Empty;
				Uri uri;
				string refererQueryString = request.TryParseUrlReferrer(out uri) ? uri.Query : "noRefUrl";
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
				Plt1WebHandler.GetPlt1PerformanceEventData(userAgent, refererQueryString, request.QueryString, text, dictionary, dictionary2);
				Plt1WebHandler.AddClientLoadTimeDataPoint(dictionary, dictionary2, userContextId, clientAddressWithoutPII, clientVersion, isMowa, userName, userContext, cookieValueAndSetIfNull);
				if (dictionary2.Count > 0 && dictionary.ContainsKey("msg") && dictionary["msg"].Contains("success"))
				{
					Plt1WebHandler.AddCalculatedClientLoadTimeDataPoint(dictionary, dictionary2, userContextId, clientAddressWithoutPII, clientVersion, isMowa, userName, cookieValueAndSetIfNull);
				}
				if (userContext != null && userContext.FeaturesManager != null && userContext.FeaturesManager.ServerSettings.OwaServerLogonActivityLogging.Enabled)
				{
					this.AddtoActivityLog(userContext, dictionary, userName, clientAddressWithoutPII, userAgent.RawString);
				}
			}
			else
			{
				string clientVersion2 = context.Request.QueryString.Get("v") ?? string.Empty;
				ClientLogEvent plt1AccessEvent = Plt1WebHandler.GetPlt1AccessEvent(userContextId, request.UserAgent, clientAddressWithoutPII, clientVersion2, isMowa, cookieValueAndSetIfNull);
				OwaClientLogger.AppendToLog(plt1AccessEvent);
			}
			Plt1WebHandler.SetResponseHeaders(context.Response);
			Plt1WebHandler.WriteImage(context.Response);
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0008DB43 File Offset: 0x0008BD43
		internal static bool IsPlt1PerformanceRequest(HttpRequest httpRequest)
		{
			return httpRequest.QueryString.AllKeys.Any((string k) => k != null && (k.Equals("PLT", StringComparison.OrdinalIgnoreCase) || k.Equals("ALT", StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x0008DB74 File Offset: 0x0008BD74
		internal static string GetUserContextId(HttpContext context)
		{
			UserContextCookie userContextCookie = UserContextCookie.GetUserContextCookie(context);
			if (userContextCookie == null || userContextCookie.CookieValue == null)
			{
				return string.Empty;
			}
			return userContextCookie.CookieValue;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0008DBA0 File Offset: 0x0008BDA0
		internal static void SetResponseHeaders(HttpResponse response)
		{
			DateTime expires = DateTime.UtcNow.Add(TimeSpan.FromDays(30.0));
			response.Cache.SetCacheability(HttpCacheability.Private);
			response.Cache.SetExpires(expires);
			response.ContentType = "image/gif";
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x0008DBEC File Offset: 0x0008BDEC
		internal static ClientLogEvent GetPlt1AccessEvent(string contextId, string userAgent, string ipAddress, string clientVersion, bool isMowa, string clientIdCookieValue = null)
		{
			string[] keys = new string[]
			{
				"UA"
			};
			string[] values = new string[]
			{
				userAgent
			};
			Datapoint datapoint = new Datapoint(DatapointConsumer.Analytics, "PLT1Access", DateTime.UtcNow.ToString("o"), keys, values);
			return new ClientLogEvent(datapoint, contextId, ipAddress, string.Empty, clientVersion, Globals.ApplicationVersion ?? string.Empty, isMowa, clientIdCookieValue);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x0008DC60 File Offset: 0x0008BE60
		internal static void GetPlt1PerformanceEventData(UserAgent userAgent, string refererQueryString, NameValueCollection queryString, string postData, Dictionary<string, string> rawKeyValuePairs, Dictionary<string, string> calculatedKeyValuePairs)
		{
			Plt1WebHandler.UpdateDeviceInfo(rawKeyValuePairs, userAgent);
			Plt1WebHandler.AddKeyValuePair(rawKeyValuePairs, "UA", userAgent.RawString);
			if (!string.IsNullOrEmpty(refererQueryString))
			{
				Plt1WebHandler.AddKeyValuePair(rawKeyValuePairs, "urlQuery", refererQueryString);
			}
			for (int i = 0; i < queryString.Keys.Count; i++)
			{
				string text = queryString.Keys[i];
				string text2 = queryString[text];
				if (text != null && text2 != null)
				{
					if (text.ToUpper() == "ALT" || text.ToUpper() == "PLT")
					{
						Plt1WebHandler.AddKeyValuePair(rawKeyValuePairs, "type", text.ToUpper());
						string[] array = text2.Split(new char[]
						{
							','
						});
						for (int j = 0; j < array.Length - 1; j += 2)
						{
							Plt1WebHandler.AddKeyValuePair(rawKeyValuePairs, array[j], array[j + 1]);
						}
						string text3 = "-1";
						if (rawKeyValuePairs.ContainsKey("fS") && rawKeyValuePairs.ContainsKey("now") && rawKeyValuePairs.ContainsKey("type"))
						{
							text3 = Plt1WebHandler.AddStringAsIntValue(rawKeyValuePairs, "fS", "now");
						}
						Plt1WebHandler.AddKeyValuePair(calculatedKeyValuePairs, rawKeyValuePairs["type"], string.IsNullOrEmpty(text3) ? "-1" : text3);
						if (rawKeyValuePairs.ContainsKey("rSt") && rawKeyValuePairs.ContainsKey("now"))
						{
							string value = Plt1WebHandler.SubtractStringAsIntValue(rawKeyValuePairs, "rSt", "now");
							Plt1WebHandler.AddKeyValuePair(calculatedKeyValuePairs, "RDT", value);
						}
						if (rawKeyValuePairs.ContainsKey("rStNoTim") && rawKeyValuePairs.ContainsKey("nowNoTim"))
						{
							string value2 = Plt1WebHandler.SubtractStringAsIntValue(rawKeyValuePairs, "rStNoTim", "nowNoTim");
							Plt1WebHandler.AddKeyValuePair(calculatedKeyValuePairs, "NTRDT", value2);
						}
						Plt1WebHandler.AddTimingMetrics(string.Empty, string.Empty, rawKeyValuePairs, calculatedKeyValuePairs);
					}
					else if (text == "msg")
					{
						string[] array2 = text2.Split(new char[]
						{
							';'
						});
						if (array2.Length > 0)
						{
							Plt1WebHandler.AddKeyValuePair(rawKeyValuePairs, "msg", array2[0]);
						}
						if (array2.Length > 1)
						{
							Plt1WebHandler.AddKeyValuePair(rawKeyValuePairs, "msgc", array2[1]);
						}
					}
					else
					{
						Plt1WebHandler.AddKeyValuePair(rawKeyValuePairs, text, text2);
					}
				}
			}
			if (!string.IsNullOrEmpty(postData))
			{
				string[] array3 = postData.Split(new char[]
				{
					'&'
				});
				foreach (string text4 in array3)
				{
					if (text4.StartsWith("Res="))
					{
						int num = text4.IndexOf(',');
						if (num >= 0)
						{
							int length = "Res=".Length;
							string text5 = text4.Substring(length, num - length);
							if (text5.Contains("?"))
							{
								text5 = text5.Substring(0, text5.IndexOf('?'));
							}
							string text6 = Plt1WebHandler.EncodeValidKeyString(text5);
							string text7 = text4.Substring(num + 1 + "tim=".Length);
							string[] array5 = text7.Split(new char[]
							{
								','
							});
							for (int l = 0; l < array5.Length - 1; l += 2)
							{
								Plt1WebHandler.AddKeyValuePair(rawKeyValuePairs, array5[l] + "[" + text6 + "]", array5[l + 1]);
							}
							if (text5.Equals("sessiondata.ashx", StringComparison.InvariantCultureIgnoreCase))
							{
								Plt1WebHandler.AddTimingMetrics("S", "[" + text6 + "]", rawKeyValuePairs, calculatedKeyValuePairs);
							}
							else if (Regex.IsMatch(text5, "^preboot\\.", RegexOptions.IgnoreCase))
							{
								Plt1WebHandler.AddTimingMetrics("R1", "[" + text6 + "]", rawKeyValuePairs, calculatedKeyValuePairs);
							}
							else if (Regex.IsMatch(text5, "^boot\\.([^\\.]+\\.)?0", RegexOptions.IgnoreCase))
							{
								Plt1WebHandler.AddTimingMetrics("R2", "[" + text6 + "]", rawKeyValuePairs, calculatedKeyValuePairs);
							}
							else if (text5.StartsWith("userspecificresourceinjector.ashx", StringComparison.InvariantCultureIgnoreCase))
							{
								Plt1WebHandler.AddTimingMetrics("U", "[" + text6 + "]", rawKeyValuePairs, calculatedKeyValuePairs);
							}
						}
					}
					else if (!string.IsNullOrEmpty(text4))
					{
						string[] array6 = text4.Split(new char[]
						{
							'='
						});
						if (array6.Length >= 2)
						{
							string key = Plt1WebHandler.EncodeValidKeyString(array6[0]);
							Plt1WebHandler.AddKeyValuePair(rawKeyValuePairs, key, array6[1]);
						}
					}
				}
			}
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0008E0C6 File Offset: 0x0008C2C6
		private static void WriteImage(HttpResponse response)
		{
			response.BinaryWrite(Plt1WebHandler.ClearGif);
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x0008E0D4 File Offset: 0x0008C2D4
		private string GetClientAddressWithoutPII(HttpRequest request)
		{
			string text = request.Headers["X-Forwarded-For"];
			if (string.IsNullOrEmpty(text))
			{
				return request.UserHostAddress;
			}
			return text;
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x0008E104 File Offset: 0x0008C304
		private static void AddTimingMetrics(string prefix, string resourceName, Dictionary<string, string> rawKeyValuePairs, Dictionary<string, string> calculatedKevValuePairs)
		{
			if (rawKeyValuePairs.ContainsKey("reds" + resourceName) && rawKeyValuePairs.ContainsKey("redE" + resourceName))
			{
				string value = Plt1WebHandler.SubtractStringAsIntValue(rawKeyValuePairs, "reds" + resourceName, "redE" + resourceName);
				Plt1WebHandler.AddKeyValuePair(calculatedKevValuePairs, prefix + "RT", value);
			}
			if (rawKeyValuePairs.ContainsKey("dLS" + resourceName) && rawKeyValuePairs.ContainsKey("dLE" + resourceName))
			{
				string value2 = Plt1WebHandler.SubtractStringAsIntValue(rawKeyValuePairs, "dLS" + resourceName, "dLE" + resourceName);
				Plt1WebHandler.AddKeyValuePair(calculatedKevValuePairs, prefix + "DN", value2);
			}
			if (rawKeyValuePairs.ContainsKey("cS" + resourceName) && rawKeyValuePairs.ContainsKey("cE" + resourceName))
			{
				string value3 = Plt1WebHandler.SubtractStringAsIntValue(rawKeyValuePairs, "cS" + resourceName, "cE" + resourceName);
				Plt1WebHandler.AddKeyValuePair(calculatedKevValuePairs, prefix + "CT", value3);
			}
			if (rawKeyValuePairs.ContainsKey("sCS" + resourceName) && rawKeyValuePairs.ContainsKey("cE" + resourceName))
			{
				string value4 = Plt1WebHandler.SubtractStringAsIntValue(rawKeyValuePairs, "sCS" + resourceName, "cE" + resourceName);
				Plt1WebHandler.AddKeyValuePair(calculatedKevValuePairs, prefix + "ST", value4);
			}
			if (rawKeyValuePairs.ContainsKey("reqS" + resourceName) && rawKeyValuePairs.ContainsKey("resS" + resourceName))
			{
				string value5 = Plt1WebHandler.SubtractStringAsIntValue(rawKeyValuePairs, "reqS" + resourceName, "resS" + resourceName);
				Plt1WebHandler.AddKeyValuePair(calculatedKevValuePairs, prefix + "RQ", value5);
			}
			if (rawKeyValuePairs.ContainsKey("resS" + resourceName) && rawKeyValuePairs.ContainsKey("resE" + resourceName))
			{
				string value6 = Plt1WebHandler.SubtractStringAsIntValue(rawKeyValuePairs, "resS" + resourceName, "resE" + resourceName);
				Plt1WebHandler.AddKeyValuePair(calculatedKevValuePairs, prefix + "RS", value6);
			}
			if (rawKeyValuePairs.ContainsKey("resE" + resourceName))
			{
				string value7 = rawKeyValuePairs["resE" + resourceName];
				Plt1WebHandler.AddKeyValuePair(calculatedKevValuePairs, prefix + "TR", value7);
			}
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0008E34C File Offset: 0x0008C54C
		private static string AddStringAsIntValue(Dictionary<string, string> keyValuePairs, string key1, string key2)
		{
			try
			{
				return (Convert.ToInt64(keyValuePairs[key1]) + Convert.ToInt64(keyValuePairs[key2])).ToString();
			}
			catch (FormatException)
			{
				keyValuePairs["ELMSG"] = key1 + "." + key2 + ".FormatException";
			}
			catch (OverflowException)
			{
				keyValuePairs["ELMSG"] = key1 + "." + key2 + ".OverflowException";
			}
			return null;
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x0008E3DC File Offset: 0x0008C5DC
		private static string SubtractStringAsIntValue(Dictionary<string, string> keyValuePairs, string key1, string key2)
		{
			try
			{
				return (Convert.ToInt64(keyValuePairs[key2]) - Convert.ToInt64(keyValuePairs[key1])).ToString();
			}
			catch (FormatException)
			{
				keyValuePairs["ELMSG"] = key1 + "." + key2 + ".FormatException";
			}
			catch (OverflowException)
			{
				keyValuePairs["ELMSG"] = key1 + "." + key2 + ".OverflowException";
			}
			return null;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0008E46C File Offset: 0x0008C66C
		private static void AddKeyValuePair(Dictionary<string, string> keyValuePairs, string key, string value)
		{
			if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
			{
				if (!SpecialCharacters.IsValidKey(key))
				{
					return;
				}
				if (keyValuePairs.ContainsKey(key))
				{
					keyValuePairs["DupKey"] = key;
					return;
				}
				keyValuePairs[key] = Plt1WebHandler.EncodeValidValueString(value);
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x0008E4AC File Offset: 0x0008C6AC
		private static string EncodeValidKeyString(string keyString)
		{
			StringBuilder stringBuilder = new StringBuilder(keyString.Length);
			foreach (char c in keyString)
			{
				if (!char.IsLetterOrDigit(c))
				{
					stringBuilder.Append('.');
					StringBuilder stringBuilder2 = stringBuilder;
					int num = (int)c;
					stringBuilder2.Append(num.ToString("X"));
					stringBuilder.Append('.');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x0008E51D File Offset: 0x0008C71D
		private static string EncodeValidValueString(string valueString)
		{
			if (valueString == null)
			{
				return null;
			}
			return valueString.Replace(";", "$!").Replace('\'', '#').Replace('"', '#');
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0008E548 File Offset: 0x0008C748
		private static void UpdateDeviceInfo(Dictionary<string, string> keyValuePairs, UserAgent userAgent)
		{
			if (!string.IsNullOrEmpty(userAgent.Browser))
			{
				Plt1WebHandler.AddKeyValuePair(keyValuePairs, "brn", userAgent.Browser);
			}
			if (userAgent.BrowserVersion != null)
			{
				Plt1WebHandler.AddKeyValuePair(keyValuePairs, "brv", userAgent.BrowserVersion.Build.ToString());
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0008E59C File Offset: 0x0008C79C
		private static void AddClientLoadTimeDataPoint(Dictionary<string, string> rawKeyValuePairs, Dictionary<string, string> calculatedKeyValuePairs, string contextId, string ipAddress, string clientVersion, bool isMowa, string userName, UserContext userContext, string clientIdCookieValue = null)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			foreach (string text in rawKeyValuePairs.Keys)
			{
				if (!Plt1WebHandler.ShouldIgnoreKey(text))
				{
					list.Add(text);
					list2.Add(rawKeyValuePairs[text]);
				}
			}
			foreach (string text2 in calculatedKeyValuePairs.Keys)
			{
				if (!Plt1WebHandler.ShouldIgnoreKey(text2))
				{
					list.Add(text2);
					list2.Add(calculatedKeyValuePairs[text2]);
				}
			}
			Datapoint datapoint = new Datapoint(DatapointConsumer.Analytics, "ClientLoadTime", DateTime.UtcNow.ToString("o"), list.ToArray(), list2.ToArray());
			ClientLogEvent logEvent = new ClientLogEvent(datapoint, contextId, ipAddress, userName, clientVersion, Globals.ApplicationVersion ?? string.Empty, isMowa, clientIdCookieValue);
			Plt1WebHandler.UpdateLogEventCommonData(logEvent, userContext);
			OwaClientLogger.AppendToLog(logEvent);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0008E6C8 File Offset: 0x0008C8C8
		private static void AddCalculatedClientLoadTimeDataPoint(Dictionary<string, string> rawKeyValuePairs, Dictionary<string, string> calculatedKeyValuePairs, string contextId, string ipAddress, string clientVersion, bool isMowa, string userName, string clientIdCookieValue = null)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			if (rawKeyValuePairs.ContainsKey("brn"))
			{
				list.Add("brn");
				list2.Add(rawKeyValuePairs["brn"]);
			}
			if (rawKeyValuePairs.ContainsKey("brv"))
			{
				list.Add("brv");
				list2.Add(rawKeyValuePairs["brv"]);
			}
			if (rawKeyValuePairs.ContainsKey("tg"))
			{
				list.Add("tg");
				list2.Add(rawKeyValuePairs["tg"]);
			}
			if (rawKeyValuePairs.ContainsKey("domL"))
			{
				list.Add("domL");
				list2.Add(rawKeyValuePairs["domL"]);
			}
			if (rawKeyValuePairs.ContainsKey("pE"))
			{
				list.Add("DPT");
				list2.Add(rawKeyValuePairs["pE"]);
			}
			if (rawKeyValuePairs.ContainsKey("rpo"))
			{
				list.Add("RPO");
				list2.Add(rawKeyValuePairs["rpo"]);
			}
			if (rawKeyValuePairs.ContainsKey("te"))
			{
				list.Add("te");
				list2.Add(rawKeyValuePairs["te"]);
			}
			foreach (string text in calculatedKeyValuePairs.Keys)
			{
				if (!Plt1WebHandler.ShouldIgnoreKey(text))
				{
					list.Add(text);
					list2.Add(calculatedKeyValuePairs[text]);
				}
			}
			Datapoint datapoint = new Datapoint(DatapointConsumer.Analytics, "CalculatedClientLoadTime", DateTime.UtcNow.ToString("o"), list.ToArray(), list2.ToArray());
			ClientLogEvent logEvent = new ClientLogEvent(datapoint, contextId, ipAddress, userName, clientVersion, Globals.ApplicationVersion ?? string.Empty, isMowa, clientIdCookieValue);
			OwaClientLogger.AppendToLog(logEvent);
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x0008E8B0 File Offset: 0x0008CAB0
		private void AddtoActivityLog(UserContext userContext, Dictionary<string, string> rawKeyValuePairs, string userName, string ipAddressWithoutPII, string userAgent)
		{
			string exceptionInfo = string.Empty;
			string result = string.Empty;
			string text;
			if (!rawKeyValuePairs.TryGetValue("msg", out text) || string.IsNullOrEmpty(text))
			{
				return;
			}
			if (text.Contains("success"))
			{
				result = "Success";
			}
			else
			{
				if (text.Contains("AuthRedirect"))
				{
					return;
				}
				result = "Failure";
				string text2;
				if (rawKeyValuePairs.TryGetValue("Err", out text2) && !string.IsNullOrEmpty(text2))
				{
					int length = (text2.Length > 100) ? 100 : text2.Length;
					exceptionInfo = text2.Substring(0, length);
				}
			}
			if (userContext != null)
			{
				try
				{
					userContext.LockAndReconnectMailboxSession();
					if (userContext.MailboxSession != null && userContext.MailboxSession.ActivitySession != null)
					{
						userContext.MailboxSession.ActivitySession.CaptureServerLogonActivity(result, exceptionInfo, userName, ipAddressWithoutPII, userAgent);
					}
				}
				catch (Exception arg)
				{
					ExTraceGlobals.UserContextCallTracer.TraceError<Exception>(0L, "Could not acquire lock for UserContext to capture user dataset activity. Exception: {0}", arg);
				}
				finally
				{
					if (userContext.MailboxSessionLockedByCurrentThread())
					{
						userContext.UnlockAndDisconnectMailboxSession();
					}
				}
			}
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0008E9C0 File Offset: 0x0008CBC0
		private static bool ShouldIgnoreKey(string key)
		{
			switch (key)
			{
			case "ts":
			case "ds":
			case "DC":
			case "Mowa":
			case "ip":
			case "user":
			case "cbld":
			case "Bld":
			case "UC":
				return true;
			}
			return false;
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0008EA98 File Offset: 0x0008CC98
		private static void UpdateLogEventCommonData(ClientLogEvent logEvent, UserContext userContext)
		{
			if (userContext != null)
			{
				if (userContext.IsExplicitLogon && userContext.MailboxIdentity != null)
				{
					SmtpAddress primarySmtpAddress = userContext.MailboxIdentity.PrimarySmtpAddress;
					logEvent.DatapointProperties.Add("PSA", userContext.MailboxIdentity.PrimarySmtpAddress.ToString());
				}
				logEvent.UpdateNetid(userContext);
				logEvent.UpdateMailboxGuid(userContext.ExchangePrincipal);
				logEvent.UpdateTenantInfo(userContext);
				if (userContext.LogEventCommonData != null)
				{
					logEvent.UpdateFlightInfo(userContext.LogEventCommonData);
					logEvent.UpdateClientInfo(userContext.LogEventCommonData);
				}
			}
		}

		// Token: 0x040016CC RID: 5836
		private const int ExpirationDays = 30;

		// Token: 0x040016CD RID: 5837
		private const string CDNFirestResourceNamePattern = "^preboot\\.";

		// Token: 0x040016CE RID: 5838
		private const string CDNLargeResourceNamePattern = "^boot\\.([^\\.]+\\.)?0";

		// Token: 0x040016CF RID: 5839
		private const string SessionDataRequestName = "sessiondata.ashx";

		// Token: 0x040016D0 RID: 5840
		private const string USRIRequestName = "userspecificresourceinjector.ashx";

		// Token: 0x040016D1 RID: 5841
		private const string PLTTypeKeyName = "type";

		// Token: 0x040016D2 RID: 5842
		private const string PLTEndTimeKeyName = "now";

		// Token: 0x040016D3 RID: 5843
		private const string LogDataErrorKey = "ELMSG";

		// Token: 0x040016D4 RID: 5844
		private const string MailboxPrincipalKey = "PSA";

		// Token: 0x040016D5 RID: 5845
		private const string Success = "Success";

		// Token: 0x040016D6 RID: 5846
		private const string Failure = "Failure";

		// Token: 0x040016D7 RID: 5847
		internal static readonly byte[] ClearGif = new byte[]
		{
			71,
			73,
			70,
			56,
			57,
			97,
			1,
			0,
			1,
			0,
			240,
			0,
			0,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			33,
			249,
			4,
			1,
			0,
			0,
			1,
			0,
			44,
			0,
			0,
			0,
			0,
			1,
			0,
			1,
			0,
			0,
			2,
			2,
			76,
			1,
			0,
			59
		};
	}
}
