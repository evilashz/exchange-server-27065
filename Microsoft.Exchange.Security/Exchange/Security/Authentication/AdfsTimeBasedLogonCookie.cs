using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000021 RID: 33
	public class AdfsTimeBasedLogonCookie
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00007F46 File Offset: 0x00006146
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00007F4D File Offset: 0x0000614D
		private static ExactTimeoutCache<string, byte[]> KeyCache { get; set; } = new ExactTimeoutCache<string, byte[]>(delegate(string k, byte[] v, RemoveReason r)
		{
			AdfsTimeBasedLogonCookie.UpdateCacheSizeCounter();
		}, null, null, 25000, false);

		// Token: 0x060000C0 RID: 192 RVA: 0x00007F58 File Offset: 0x00006158
		private AdfsTimeBasedLogonCookie(HttpRequest request = null)
		{
			if (request != null)
			{
				this.InitializeFromHttpRequest(request);
				return;
			}
			this.LogonTime = (this.LastActivityTime = DateTime.UtcNow);
			using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
			{
				aesCryptoServiceProvider.GenerateKey();
				aesCryptoServiceProvider.GenerateIV();
				this.symKey = aesCryptoServiceProvider.Key;
				this.symIV = aesCryptoServiceProvider.IV;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00008008 File Offset: 0x00006208
		private static void UpdateCacheSizeCounter()
		{
			Utility.AdfsAuthCountersInstance.AdfsFedAuthModuleKeyCacheSize.RawValue = (long)AdfsTimeBasedLogonCookie.KeyCache.Count;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00008024 File Offset: 0x00006224
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000802C File Offset: 0x0000622C
		public DateTime LogonTime { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00008035 File Offset: 0x00006235
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000803D File Offset: 0x0000623D
		public DateTime LastActivityTime { get; private set; }

		// Token: 0x060000C7 RID: 199 RVA: 0x00008048 File Offset: 0x00006248
		public static bool TryCreateFromHttpRequest(HttpRequest request, out AdfsTimeBasedLogonCookie cookie)
		{
			cookie = null;
			bool flag = false;
			if (request != null)
			{
				try
				{
					cookie = new AdfsTimeBasedLogonCookie(request);
					flag = true;
				}
				catch (ArgumentException ex)
				{
					Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel.ExTraceGlobals.EventLogTracer.TraceInformation<string>(0, 0L, "Failed to create ADFS Logon Cookie Data: {0}.", ex.Message);
					Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError<string>(0L, "[AdfsTimeBasedLogonCookie::TryCreateFromHttpCookieCollection]: Failed to create ADFS Logon Cookie Data: {0}.", ex.Message);
				}
			}
			Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<bool>(0L, "[AdfsTimeBasedLogonCookie::TryCreateFromHttpCookieCollection]: Attempt to create ADFS Logon Cookie Data returned: {0}.", flag);
			return flag;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000080C0 File Offset: 0x000062C0
		public static AdfsTimeBasedLogonCookie CreateFromCurrentTime()
		{
			return new AdfsTimeBasedLogonCookie(null);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000080C8 File Offset: 0x000062C8
		public static void DeleteAdfsAuthCookies(HttpRequest request, HttpResponse response)
		{
			Utility.DeleteCookie(request, response, "TimeWindow", null, false);
			Utility.DeleteCookie(request, response, "TimeWindowKey", null, false);
			Utility.DeleteCookie(request, response, "TimeWindowIV", null, false);
			Utility.DeleteCookie(request, response, "TimeWindowSig", null, false);
			AdfsTimeBasedLogonCookie.InvalidateKeyCacheEntry(request);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00008108 File Offset: 0x00006308
		public static bool Validate(DateTime time, TimeSpan timeWindowSpan)
		{
			return AdfsTimeBasedLogonCookie.Validate(DateTime.UtcNow.Subtract(time), timeWindowSpan);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000812C File Offset: 0x0000632C
		public static bool Validate(TimeSpan timeSpan, TimeSpan timeWindowSpan)
		{
			long ticks = timeSpan.Ticks;
			bool flag = ticks < timeWindowSpan.Ticks;
			Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<bool>(0L, "[AdfsTimeBasedLogonCookie::Validate]: ADFS Logon Cookie is valid: {0}.", flag);
			return flag;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00008160 File Offset: 0x00006360
		public void AddToResponse(HttpRequest request, HttpResponse response)
		{
			using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
			{
				aesCryptoServiceProvider.Key = this.symKey;
				aesCryptoServiceProvider.IV = this.symIV;
				using (ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateEncryptor())
				{
					string s = this.ToTimeString();
					byte[] bytes = Encoding.Unicode.GetBytes(s);
					byte[] inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
					AdfsTimeBasedLogonCookie.CreateAndAddHttpCookieToResponse(request, response, "TimeWindow", Convert.ToBase64String(inArray));
				}
			}
			X509Certificate2[] certificates = Utility.GetCertificates();
			RSACryptoServiceProvider rsacryptoServiceProvider = certificates[0].PublicKey.Key as RSACryptoServiceProvider;
			byte[] inArray2 = rsacryptoServiceProvider.Encrypt(this.symKey, true);
			string text = Convert.ToBase64String(inArray2);
			byte[] inArray3 = rsacryptoServiceProvider.Encrypt(this.symIV, true);
			string text2 = Convert.ToBase64String(inArray3);
			AdfsTimeBasedLogonCookie.CreateAndAddHttpCookieToResponse(request, response, "TimeWindowKey", text);
			AdfsTimeBasedLogonCookie.CreateAndAddHttpCookieToResponse(request, response, "TimeWindowIV", text2);
			AdfsTimeBasedLogonCookie.KeyCache.TryInsertSliding(text, this.symKey, AdfsFederationAuthModule.TimeBasedAuthenticationTimeoutInterval);
			AdfsTimeBasedLogonCookie.KeyCache.TryInsertSliding(text2, this.symIV, AdfsFederationAuthModule.TimeBasedAuthenticationTimeoutInterval);
			AdfsTimeBasedLogonCookie.UpdateCacheSizeCounter();
			byte[] bytes2 = Encoding.Unicode.GetBytes("Adfs Logon Rocks!");
			byte[] inArray4 = rsacryptoServiceProvider.Encrypt(bytes2, true);
			AdfsTimeBasedLogonCookie.CreateAndAddHttpCookieToResponse(request, response, "TimeWindowSig", Convert.ToBase64String(inArray4));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000082C8 File Offset: 0x000064C8
		public void Renew()
		{
			Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug(0L, "[AdfsTimeBasedLogonCookie::Renew]: Renewing ADFS Logon cookie.");
			this.LastActivityTime = DateTime.UtcNow;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000082E8 File Offset: 0x000064E8
		private static void CreateAndAddHttpCookieToResponse(HttpRequest request, HttpResponse response, string name, string value)
		{
			HttpCookie httpCookie = new HttpCookie(name, value);
			httpCookie.HttpOnly = true;
			httpCookie.Secure = request.IsSecureConnection;
			response.Cookies.Add(httpCookie);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000831C File Offset: 0x0000651C
		private static void InvalidateKeyCacheEntry(HttpRequest request)
		{
			string text = null;
			if (request.Cookies["TimeWindowKey"] != null && request.Cookies["TimeWindowKey"].Value != null)
			{
				text = request.Cookies["TimeWindowKey"].Value;
			}
			if (!string.IsNullOrEmpty(text))
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<string>(0L, "[AdfsTimeBasedLogonCookie::Validate] Removing key cache entry with key {0}", text);
				AdfsTimeBasedLogonCookie.KeyCache.Remove(text);
			}
			string text2 = null;
			if (request.Cookies["TimeWindowIV"] != null && request.Cookies["TimeWindowIV"].Value != null)
			{
				text2 = request.Cookies["TimeWindowIV"].Value;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<string>(0L, "[AdfsTimeBasedLogonCookie::Validate] Removing key cache entry with key {0}", text2);
				AdfsTimeBasedLogonCookie.KeyCache.Remove(text2);
			}
			AdfsTimeBasedLogonCookie.UpdateCacheSizeCounter();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000083FC File Offset: 0x000065FC
		private void InitializeFromHttpRequest(HttpRequest request)
		{
			try
			{
				string text = null;
				if (request.Cookies["TimeWindow"] != null && request.Cookies["TimeWindow"].Value != null)
				{
					text = request.Cookies["TimeWindow"].Value;
				}
				string text2 = null;
				if (request.Cookies["TimeWindowKey"] != null && request.Cookies["TimeWindowKey"].Value != null)
				{
					text2 = request.Cookies["TimeWindowKey"].Value;
				}
				string text3 = null;
				if (request.Cookies["TimeWindowIV"] != null && request.Cookies["TimeWindowIV"].Value != null)
				{
					text3 = request.Cookies["TimeWindowIV"].Value;
				}
				string text4 = null;
				if (request.Cookies["TimeWindowSig"] != null && request.Cookies["TimeWindowSig"].Value != null)
				{
					text4 = request.Cookies["TimeWindowSig"].Value;
				}
				if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text3) || string.IsNullOrEmpty(text4))
				{
					Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError(0L, "[AdfsTimeBasedLogonCookie::InitializeFromHttpRequest]: Unable to find all the required AdfsLogonTime cookies in collection");
					throw new ArgumentException("Missing cookies in collection");
				}
				byte[] array = null;
				byte[] array2 = null;
				Utility.AdfsAuthCountersInstance.AdfsFedAuthModuleCacheHitsRateBase.Increment();
				AdfsTimeBasedLogonCookie.KeyCache.TryGetValue(text2, out array);
				AdfsTimeBasedLogonCookie.KeyCache.TryGetValue(text3, out array2);
				if (array != null && array2 != null)
				{
					Utility.AdfsAuthCountersInstance.AdfsFedAuthModuleKeyCacheHitsRate.Increment();
				}
				else
				{
					bool flag = true;
					RSACryptoServiceProvider rsacryptoServiceProvider = null;
					X509Certificate2[] certificates = Utility.GetCertificates();
					foreach (X509Certificate2 x509Certificate in certificates)
					{
						try
						{
							rsacryptoServiceProvider = (x509Certificate.PrivateKey as RSACryptoServiceProvider);
							byte[] rgb = Convert.FromBase64String(text4);
							byte[] bytes = rsacryptoServiceProvider.Decrypt(rgb, true);
							string @string = Encoding.Unicode.GetString(bytes);
							if (string.Compare(@string, "Adfs Logon Rocks!", StringComparison.Ordinal) == 0)
							{
								flag = false;
								break;
							}
						}
						catch (CryptographicException arg)
						{
							Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<CryptographicException>(0L, "[AdfsTimeBasedLogonCookie::InitializeFromHttpRequest] Received CryptographicException {0} decrypting AdfsLogonSig", arg);
						}
					}
					if (flag)
					{
						string text5 = "Error in validating ADFSLogon signature. This most likely indicates that the certifcate on the Cafe web-site on this server does not match the certificate on the Cafe web-site on another server in this Cafe array";
						Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError<string>(0L, "[AdfsTimeBasedLogonCookie:InitializeFromHttpRequest] - {0}", text5);
						throw new ArgumentException(text5);
					}
					byte[] rgb2 = Convert.FromBase64String(text2);
					byte[] rgb3 = Convert.FromBase64String(text3);
					try
					{
						array = rsacryptoServiceProvider.Decrypt(rgb2, true);
						array2 = rsacryptoServiceProvider.Decrypt(rgb3, true);
					}
					catch (CryptographicException ex)
					{
						Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<CryptographicException>(0L, "[AdfsTimeBasedLogonCookie::InitializeFromHttpRequest] Received CryptographicException {0} decrypting symKey/symIV", ex);
						throw new ArgumentException("Bad cookie value", ex);
					}
					AdfsTimeBasedLogonCookie.KeyCache.TryInsertSliding(text2, array, AdfsFederationAuthModule.TimeBasedAuthenticationTimeoutInterval);
					AdfsTimeBasedLogonCookie.KeyCache.TryInsertSliding(text3, array2, AdfsFederationAuthModule.TimeBasedAuthenticationTimeoutInterval);
					AdfsTimeBasedLogonCookie.UpdateCacheSizeCounter();
				}
				byte[] bytes2 = null;
				using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
				{
					aesCryptoServiceProvider.Key = array;
					aesCryptoServiceProvider.IV = array2;
					using (ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateDecryptor())
					{
						byte[] array4 = Convert.FromBase64String(text);
						try
						{
							bytes2 = cryptoTransform.TransformFinalBlock(array4, 0, array4.Length);
						}
						catch (CryptographicException ex2)
						{
							Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceDebug<CryptographicException>(0L, "[AdfsTimeBasedLogonCookie::InitializeFromHttpRequest] Received CryptographicException {0} transforming time string", ex2);
							throw new ArgumentException("Bad cookie value", ex2);
						}
					}
				}
				string string2 = Encoding.Unicode.GetString(bytes2);
				this.ParseTimes(string2);
				this.symKey = array;
				this.symIV = array2;
			}
			catch (InvalidOperationException ex3)
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError<InvalidOperationException>(0L, "[AdfsTimeBasedLogonCookie::ParseHttpCookies]: Unable to parse cookies: {0}.", ex3);
				throw new ArgumentException("Bad cookie value", ex3);
			}
			catch (FormatException ex4)
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError<FormatException>(0L, "[AdfsTimeBasedLogonCookie::ParseHttpCookies]: Unable to parse cookies: {0}.", ex4);
				throw new ArgumentException("Bad cookie value", ex4);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00008854 File Offset: 0x00006A54
		private void ParseTimes(string input)
		{
			string[] array = input.Split(new char[]
			{
				'|'
			});
			if (array.Length != 2)
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError<string>(0L, "[AdfsTimeBasedLogonCookie::ParseTimes]: {0}.", "input doesn't contain 2 segments");
				throw new ArgumentException("input doesn't contain 2 segments");
			}
			try
			{
				this.LogonTime = DateTime.FromBinary(long.Parse(array[0]));
				this.LastActivityTime = DateTime.FromBinary(long.Parse(array[1]));
			}
			catch (FormatException ex)
			{
				Microsoft.Exchange.Diagnostics.Components.Clients.ExTraceGlobals.AdfsAuthModuleTracer.TraceError<string, FormatException>(0L, "[AdfsTimeBasedLogonCookie::ParseTimes]: {0}: {1}.", "Invalid time format", ex);
				throw new ArgumentException("Invalid time format", ex);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000088F8 File Offset: 0x00006AF8
		private string ToTimeString()
		{
			return string.Format("{1}{0}{2}", '|', this.LogonTime.ToBinary(), this.LastActivityTime.ToBinary());
		}

		// Token: 0x0400014D RID: 333
		private const string AdfsLogonSig = "Adfs Logon Rocks!";

		// Token: 0x0400014E RID: 334
		public const string AdfsLogonTimeCookieName = "TimeWindow";

		// Token: 0x0400014F RID: 335
		public const string AdfsLogonKeyCookieName = "TimeWindowKey";

		// Token: 0x04000150 RID: 336
		public const string AdfsLogonIVCookieName = "TimeWindowIV";

		// Token: 0x04000151 RID: 337
		public const string AdfsLogonSigCookieName = "TimeWindowSig";

		// Token: 0x04000152 RID: 338
		public const char SplitChar = '|';

		// Token: 0x04000153 RID: 339
		public const int AdfsLogonCookieKeyCacheSizeLimit = 25000;

		// Token: 0x04000154 RID: 340
		private byte[] symKey;

		// Token: 0x04000155 RID: 341
		private byte[] symIV;
	}
}
