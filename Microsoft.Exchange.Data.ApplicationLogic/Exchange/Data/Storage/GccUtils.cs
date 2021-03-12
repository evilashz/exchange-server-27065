using System;
using System.Globalization;
using System.Net;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000138 RID: 312
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class GccUtils
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00034BDB File Offset: 0x00032DDB
		public static DatacenterServerAuthentication DatacenterServerAuthentication
		{
			get
			{
				return GccUtils.DatacenterServerAuth;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00034BE2 File Offset: 0x00032DE2
		public static string CurrentProxyKey
		{
			get
			{
				if (!GccUtils.IsGlobalCriminalComplianceEnabled)
				{
					return string.Empty;
				}
				return Convert.ToBase64String(GccUtils.DatacenterServerAuth.CurrentSecretKey);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00034C00 File Offset: 0x00032E00
		public static string PreviousProxyKey
		{
			get
			{
				if (!GccUtils.IsGlobalCriminalComplianceEnabled)
				{
					return string.Empty;
				}
				return Convert.ToBase64String(GccUtils.DatacenterServerAuth.PreviousSecretKey);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00034C1E File Offset: 0x00032E1E
		internal static bool IsGlobalCriminalComplianceEnabled
		{
			get
			{
				return GccUtils.isGlobalCriminalComplianceEnabled.Member;
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00034C2C File Offset: 0x00032E2C
		public static bool AreStoredSecretKeysValid()
		{
			if (!GccUtils.IsGlobalCriminalComplianceEnabled)
			{
				return false;
			}
			bool result;
			try
			{
				GccUtils.RefreshProxySecretKeys();
				result = true;
			}
			catch (InvalidDatacenterProxyKeyException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00034C64 File Offset: 0x00032E64
		public static bool SetStoreSessionClientIPEndpointsFromHttpRequest(StoreSession session, HttpRequest httpRequest, bool useServerToServerHeaders)
		{
			bool result = true;
			HttpContext httpContext = null;
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (httpRequest == null)
			{
				httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					httpRequest = httpContext.Request;
				}
			}
			IPAddress ipv6None;
			IPAddress ipv6Loopback;
			if (httpRequest != null)
			{
				result = GccUtils.InternalGetClientIPEndpointsFromHttpRequest((httpContext != null) ? new HttpContextWrapper(httpContext) : null, new HttpRequestWrapper(httpRequest), out ipv6None, out ipv6Loopback, useServerToServerHeaders, false);
			}
			else
			{
				ipv6None = IPAddress.IPv6None;
				ipv6Loopback = IPAddress.IPv6Loopback;
			}
			session.SetClientIPEndpoints(ipv6None, ipv6Loopback);
			return result;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00034CCF File Offset: 0x00032ECF
		public static bool SetStoreSessionClientIPEndpointsFromHttpRequest(StoreSession session, HttpRequest httpRequest)
		{
			return GccUtils.SetStoreSessionClientIPEndpointsFromHttpRequest(session, httpRequest, false);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00034CDC File Offset: 0x00032EDC
		public static bool SetStoreSessionClientIPEndpointsFromXproxy(StoreSession session, string authString, string clientIp, string serverIp, NetworkConnection connection)
		{
			if (!GccUtils.IsGlobalCriminalComplianceEnabled)
			{
				return false;
			}
			bool result = true;
			IPAddress address;
			IPAddress address2;
			if (GccUtils.IsValidAuthString(authString))
			{
				if (!IPAddress.TryParse(clientIp, out address))
				{
					address = connection.RemoteEndPoint.Address;
					result = false;
				}
				if (!IPAddress.TryParse(serverIp, out address2))
				{
					address2 = connection.LocalEndPoint.Address;
					result = false;
				}
			}
			else
			{
				result = false;
				address = connection.RemoteEndPoint.Address;
				address2 = connection.LocalEndPoint.Address;
			}
			session.SetClientIPEndpoints(address, address2);
			return result;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00034D58 File Offset: 0x00032F58
		public static void CopyClientIPEndpointsForServerToServerProxy(HttpContext originalContext, HttpWebRequest targetRequest)
		{
			if (originalContext == null)
			{
				throw new ArgumentNullException("originalContext");
			}
			if (targetRequest == null)
			{
				throw new ArgumentNullException("targetRequest");
			}
			IPAddress ipaddress;
			IPAddress ipaddress2;
			GccUtils.InternalGetClientIPEndpointsFromHttpRequest(new HttpContextWrapper(originalContext), null, out ipaddress, out ipaddress2, false, false);
			targetRequest.Headers["x-ServerToServer-OriginalClient"] = ipaddress.ToString();
			targetRequest.Headers["x-ServerToServer-OriginalServer"] = ipaddress2.ToString();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00034DC0 File Offset: 0x00032FC0
		public static string GetAuthStringForThisServer()
		{
			if (!GccUtils.IsGlobalCriminalComplianceEnabled)
			{
				return string.Empty;
			}
			GccUtils.EnsureProxyKeysAreLoaded();
			return GccUtils.DatacenterServerAuth.GetAuthenticationString();
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00034DDE File Offset: 0x00032FDE
		public static bool IsValidAuthString(string authString)
		{
			if (!GccUtils.IsGlobalCriminalComplianceEnabled)
			{
				return false;
			}
			if (string.IsNullOrEmpty(authString))
			{
				return false;
			}
			GccUtils.EnsureProxyKeysAreLoaded();
			return GccUtils.DatacenterServerAuth.ValidateAuthenticationString(authString);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00034E04 File Offset: 0x00033004
		public static void RefreshProxySecretKeys()
		{
			if (!GccUtils.IsGlobalCriminalComplianceEnabled)
			{
				return;
			}
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs");
			if (registryKey == null)
			{
				throw new InvalidDatacenterProxyKeyException();
			}
			string text = (string)registryKey.GetValue("CurrentProxyKey", null);
			string previousKeyBase = (string)registryKey.GetValue("PreviousProxyKey", null);
			string currentIVBase = (string)registryKey.GetValue("CurrentIVProxyKey", null);
			string previousIVBase = (string)registryKey.GetValue("PreviousIVProxyKey", null);
			registryKey.Close();
			if (string.IsNullOrEmpty(text) || !GccUtils.DatacenterServerAuth.TrySetCurrentAndPreviousSecretKeys(text, previousKeyBase, currentIVBase, previousIVBase))
			{
				throw new InvalidDatacenterProxyKeyException();
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00034EA0 File Offset: 0x000330A0
		public static bool GetClientIPEndpointsFromHttpRequest(HttpContext httpContext, out IPAddress clientIPAddress, out IPAddress serverIPAddress, bool useServerToServerHeaders, bool trustEntireForwardedForHeader)
		{
			return GccUtils.InternalGetClientIPEndpointsFromHttpRequest(new HttpContextWrapper(httpContext), null, out clientIPAddress, out serverIPAddress, useServerToServerHeaders, trustEntireForwardedForHeader);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00034EB3 File Offset: 0x000330B3
		public static bool GetClientIPEndpointsFromHttpRequest(HttpContextBase httpContext, out IPAddress clientIPAddress, out IPAddress serverIPAddress, bool useServerToServerHeaders, bool trustEntireForwardedForHeader)
		{
			return GccUtils.InternalGetClientIPEndpointsFromHttpRequest(httpContext, null, out clientIPAddress, out serverIPAddress, useServerToServerHeaders, trustEntireForwardedForHeader);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00034EC1 File Offset: 0x000330C1
		public static string GetServerAddress(HttpContext httpContext)
		{
			return GccUtils.InternalGetServerAddress(new HttpContextWrapper(httpContext), null);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00034ECF File Offset: 0x000330CF
		public static string GetClientAddress(HttpContext httpContext)
		{
			return GccUtils.InternalGetClientAddress(new HttpContextWrapper(httpContext), null);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00034EDD File Offset: 0x000330DD
		public static string GetClientAddress(HttpContextBase httpContext)
		{
			return GccUtils.InternalGetClientAddress(httpContext, null);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00034EE6 File Offset: 0x000330E6
		public static string GetClientPort(HttpContext httpContext)
		{
			return GccUtils.InternalGetClientPort(new HttpContextWrapper(httpContext), null);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00034EF4 File Offset: 0x000330F4
		public static bool TryGetGccProxyInfo(HttpContext httpContext, out string gccProxyInfo)
		{
			gccProxyInfo = null;
			bool flag;
			string text;
			string text2;
			if (!GccUtils.InternalTryGetGccProxyInfo(new HttpContextWrapper(httpContext), null, out gccProxyInfo, out flag, out text, out text2) || !flag)
			{
				gccProxyInfo = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00034F22 File Offset: 0x00033122
		public static bool TryCreateGccProxyInfo(HttpContext httpContext, out string gccProxyInfo)
		{
			return GccUtils.InternalTryCreateGccProxyInfo(new HttpContextWrapper(httpContext), null, out gccProxyInfo);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00034F34 File Offset: 0x00033134
		private static bool InternalGetClientIPEndpointsFromHttpRequest(HttpContextBase httpContext, HttpRequestBase httpRequest, out IPAddress clientIPAddress, out IPAddress serverIPAddress, bool useServerToServerHeaders, bool trustEntireForwardedForHeader)
		{
			if (httpContext != null)
			{
				if (httpRequest == null)
				{
					httpRequest = httpContext.Request;
				}
			}
			else if (httpRequest == null)
			{
				throw new ArgumentException("HttpContext and HttpRequest cannot both be null");
			}
			bool flag = false;
			bool flag2 = true;
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = null;
			if (useServerToServerHeaders)
			{
				text = httpRequest.Headers["x-ServerToServer-OriginalClient"];
				text2 = httpRequest.Headers["x-ServerToServer-OriginalServer"];
			}
			if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
			{
				string text4;
				if (GccUtils.InternalTryGetGccProxyInfo(httpContext, httpRequest, out text4, out flag, out text, out text2))
				{
					flag2 = false;
				}
				else
				{
					text = httpRequest.Headers["X-Forwarded-For"];
					if (!string.IsNullOrEmpty(text))
					{
						text3 = GccUtils.InternalGetServerAddress(httpContext, httpRequest);
						text2 = text3;
						if (trustEntireForwardedForHeader)
						{
							int num = text.IndexOf(',');
							if (num > -1)
							{
								text = text.Substring(0, num);
							}
						}
						else
						{
							int num2 = text.LastIndexOf(',');
							if (num2 > -1)
							{
								text = text.Substring(num2 + 1);
							}
						}
						flag2 = false;
						flag = true;
					}
				}
				if (flag2 || !flag)
				{
					text = GccUtils.InternalGetClientAddress(httpContext, httpRequest);
					text2 = (text3 ?? GccUtils.InternalGetServerAddress(httpContext, httpRequest));
				}
			}
			if (string.IsNullOrEmpty(text) || !IPAddress.TryParse(text, out clientIPAddress))
			{
				clientIPAddress = IPAddress.IPv6None;
			}
			if (string.IsNullOrEmpty(text2) || !IPAddress.TryParse(text2, out serverIPAddress))
			{
				serverIPAddress = IPAddress.IPv6Loopback;
			}
			return flag2 || flag;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00035078 File Offset: 0x00033278
		private static TReturn InternalExecuteWorkerRequest<TReturn>(HttpContextBase httpContext, HttpRequestBase httpRequest, Func<HttpWorkerRequest, TReturn> httpWorkerDelegate, Func<HttpRequestBase, TReturn> httpRequestDelegate)
		{
			if (httpContext != null)
			{
				HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)httpContext.GetService(typeof(HttpWorkerRequest));
				if (httpWorkerRequest != null)
				{
					return httpWorkerDelegate(httpWorkerRequest);
				}
				if (httpRequest == null)
				{
					httpRequest = httpContext.Request;
				}
			}
			else if (httpRequest == null)
			{
				throw new ArgumentException("HttpContext and HttpRequest cannot both be null");
			}
			return httpRequestDelegate(httpRequest);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000350E8 File Offset: 0x000332E8
		private static string InternalGetServerAddress(HttpContextBase httpContext, HttpRequestBase httpRequest)
		{
			return GccUtils.InternalExecuteWorkerRequest<string>(httpContext, httpRequest, (HttpWorkerRequest x) => x.GetLocalAddress(), (HttpRequestBase x) => x.ServerVariables["LOCAL_ADDR"]);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00035150 File Offset: 0x00033350
		private static string InternalGetClientAddress(HttpContextBase httpContext, HttpRequestBase httpRequest)
		{
			return GccUtils.InternalExecuteWorkerRequest<string>(httpContext, httpRequest, (HttpWorkerRequest x) => x.GetRemoteAddress(), (HttpRequestBase x) => x.ServerVariables["REMOTE_ADDR"]);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000351D0 File Offset: 0x000333D0
		private static string InternalGetClientPort(HttpContextBase httpContext, HttpRequestBase httpRequest)
		{
			return GccUtils.InternalExecuteWorkerRequest<string>(httpContext, httpRequest, (HttpWorkerRequest x) => x.GetRemotePort().ToString(), (HttpRequestBase x) => x.ServerVariables["REMOTE_PORT"]);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00035288 File Offset: 0x00033488
		private static bool InternalTryGetGccProxyInfo(HttpContextBase httpContext, HttpRequestBase httpRequest, out string gccProxyInfo, out bool trustedProxy, out string clientIpRaw, out string serverIpRaw)
		{
			trustedProxy = false;
			clientIpRaw = string.Empty;
			serverIpRaw = string.Empty;
			gccProxyInfo = GccUtils.InternalExecuteWorkerRequest<string>(httpContext, httpRequest, delegate(HttpWorkerRequest request)
			{
				if (GccUtils.gccKnownRequestHeaderIndex == null)
				{
					GccUtils.gccKnownRequestHeaderIndex = new int?(HttpWorkerRequest.GetKnownRequestHeaderIndex("X-GCC-PROXYINFO"));
				}
				if (GccUtils.gccKnownRequestHeaderIndex.Value != -1)
				{
					return request.GetKnownRequestHeader(GccUtils.gccKnownRequestHeaderIndex.Value);
				}
				return request.GetUnknownRequestHeader("X-GCC-PROXYINFO");
			}, (HttpRequestBase request) => request.Headers["X-GCC-PROXYINFO"]);
			if (string.IsNullOrEmpty(gccProxyInfo))
			{
				gccProxyInfo = null;
				return false;
			}
			int num = gccProxyInfo.IndexOf(',');
			if (num > 0 && num < gccProxyInfo.Length - 1)
			{
				string authString = gccProxyInfo.Substring(0, num);
				bool flag = false;
				try
				{
					flag = GccUtils.IsValidAuthString(authString);
				}
				catch (InvalidDatacenterProxyKeyException)
				{
				}
				if (flag)
				{
					clientIpRaw = gccProxyInfo.Substring(num + 1);
					num = clientIpRaw.IndexOf(',');
					if (num > 0 && num < clientIpRaw.Length - 1)
					{
						serverIpRaw = clientIpRaw.Substring(num + 1);
						clientIpRaw = clientIpRaw.Substring(0, num);
						trustedProxy = true;
					}
				}
			}
			return true;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00035388 File Offset: 0x00033588
		private static bool InternalTryCreateGccProxyInfo(HttpContextBase httpContext, HttpRequestBase httpRequest, out string gccProxyInfo)
		{
			gccProxyInfo = null;
			string text = GccUtils.InternalGetClientAddress(httpContext, httpRequest);
			string text2 = GccUtils.InternalGetServerAddress(httpContext, httpRequest);
			IPAddress ipv6None;
			if (string.IsNullOrEmpty(text) || !IPAddress.TryParse(text, out ipv6None))
			{
				ipv6None = IPAddress.IPv6None;
			}
			IPAddress ipv6Loopback;
			if (string.IsNullOrEmpty(text2) || !IPAddress.TryParse(text2, out ipv6Loopback))
			{
				ipv6Loopback = IPAddress.IPv6Loopback;
			}
			string text3 = null;
			try
			{
				text3 = GccUtils.GetAuthStringForThisServer();
			}
			catch (InvalidDatacenterProxyKeyException)
			{
			}
			if (!string.IsNullOrEmpty(text3))
			{
				gccProxyInfo = string.Format(CultureInfo.InvariantCulture, "{0},{1},{2}", new object[]
				{
					text3,
					ipv6None,
					ipv6Loopback
				});
				return true;
			}
			return false;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00035430 File Offset: 0x00033630
		private static void EnsureProxyKeysAreLoaded()
		{
			lock (GccUtils.RegistryLock)
			{
				if (GccUtils.DatacenterServerAuth.CurrentSecretKey == null)
				{
					GccUtils.RefreshProxySecretKeys();
				}
			}
		}

		// Token: 0x040006C3 RID: 1731
		public const string GccProxyInfoHeader = "X-GCC-PROXYINFO";

		// Token: 0x040006C4 RID: 1732
		private const string E14RegistryRoot = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x040006C5 RID: 1733
		private const string CurrentProxyKeyName = "CurrentProxyKey";

		// Token: 0x040006C6 RID: 1734
		private const string CurrentIVKeyName = "CurrentIVProxyKey";

		// Token: 0x040006C7 RID: 1735
		private const string PreviousProxyKeyName = "PreviousProxyKey";

		// Token: 0x040006C8 RID: 1736
		private const string PreviousIVKeyName = "PreviousIVProxyKey";

		// Token: 0x040006C9 RID: 1737
		private const string ServerToServerOriginalClientHeader = "x-ServerToServer-OriginalClient";

		// Token: 0x040006CA RID: 1738
		private const string ServerToServerOriginalServerHeader = "x-ServerToServer-OriginalServer";

		// Token: 0x040006CB RID: 1739
		private const string XForwardedForHeader = "X-Forwarded-For";

		// Token: 0x040006CC RID: 1740
		private static readonly DatacenterServerAuthentication DatacenterServerAuth = new DatacenterServerAuthentication();

		// Token: 0x040006CD RID: 1741
		private static readonly object RegistryLock = new object();

		// Token: 0x040006CE RID: 1742
		private static int? gccKnownRequestHeaderIndex = null;

		// Token: 0x040006CF RID: 1743
		private static LazyMember<bool> isGlobalCriminalComplianceEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.Global.GlobalCriminalCompliance.Enabled);
	}
}
