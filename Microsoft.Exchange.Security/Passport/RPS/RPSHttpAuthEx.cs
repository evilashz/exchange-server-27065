using System;
using System.Runtime.InteropServices;
using System.Web;
using Microsoft.Passport.RPS.Native;

namespace Microsoft.Passport.RPS
{
	// Token: 0x02000114 RID: 276
	public sealed class RPSHttpAuthEx : IDisposable
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x0003B58B File Offset: 0x0003978B
		public RPSHttpAuthEx(RPS rps)
		{
			if (rps == null)
			{
				throw new ArgumentException("RPS object cannot be null");
			}
			this.m_rps = rps;
			this.m_IRPSHttpAuth = (IRPSHttpAuth)rps.GetObjectInternal("rps.http.auth");
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0003B5C0 File Offset: 0x000397C0
		~RPSHttpAuthEx()
		{
			this.Cleanup(false);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0003B5F0 File Offset: 0x000397F0
		public void Dispose()
		{
			this.Cleanup(true);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0003B5F9 File Offset: 0x000397F9
		private void Cleanup(bool bSuppress)
		{
			if (!this.m_bHasDisposed)
			{
				this.m_bHasDisposed = true;
				if (this.m_IRPSHttpAuth != null)
				{
					Marshal.ReleaseComObject(this.m_IRPSHttpAuth);
					this.m_IRPSHttpAuth = null;
				}
				if (bSuppress)
				{
					GC.SuppressFinalize(this);
				}
				return;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0003B631 File Offset: 0x00039831
		public IRPSHttpAuth NativeInterface
		{
			get
			{
				return this.m_IRPSHttpAuth;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0003B63C File Offset: 0x0003983C
		public RPSTicket Authenticate(string siteName, string headers, string path, string method, string querystring, RPSPropBag propBag, out bool needBody)
		{
			needBody = false;
			bool flag = true;
			IRPSPropBag irpspropBag;
			if (propBag != null)
			{
				irpspropBag = propBag.NativeInterface;
			}
			else
			{
				irpspropBag = null;
			}
			try
			{
				IRPSTicket irpsticket = (IRPSTicket)this.m_IRPSHttpAuth.AuthenticateRawHttp(siteName, method, path, querystring, null, flag, headers, null, irpspropBag);
				if (irpsticket != null)
				{
					return new RPSTicket(this.m_rps, irpsticket);
				}
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode != -2147184099)
				{
					throw ex;
				}
				needBody = true;
			}
			return null;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0003B6BC File Offset: 0x000398BC
		public RPSTicket Authenticate(string siteName, string headers, string path, string method, string querystring, string body, RPSPropBag propBag)
		{
			bool flag = true;
			IRPSPropBag irpspropBag;
			if (propBag != null)
			{
				irpspropBag = propBag.NativeInterface;
			}
			else
			{
				irpspropBag = null;
			}
			IRPSTicket irpsticket = (IRPSTicket)this.m_IRPSHttpAuth.AuthenticateRawHttp(siteName, method, path, querystring, null, flag, headers, body, irpspropBag);
			if (irpsticket != null)
			{
				return new RPSTicket(this.m_rps, irpsticket);
			}
			return null;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0003B70C File Offset: 0x0003990C
		public string GetLogoutHeaders(string siteName)
		{
			string logoutHeaders = this.m_IRPSHttpAuth.GetLogoutHeaders(siteName);
			GC.KeepAlive(this);
			return logoutHeaders;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0003B730 File Offset: 0x00039930
		public string GetTweenerChallengeHeader(string siteName, RPSPropBag propBag)
		{
			IRPSPropBag irpspropBag;
			if (propBag != null)
			{
				irpspropBag = propBag.NativeInterface;
			}
			else
			{
				irpspropBag = null;
			}
			string tweenerChallengeHeader = this.m_IRPSHttpAuth.GetTweenerChallengeHeader(siteName, irpspropBag);
			GC.KeepAlive(this);
			return tweenerChallengeHeader;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0003B760 File Offset: 0x00039960
		public string GetLiveIDChallengeHeader(string siteName, RPSPropBag propBag)
		{
			IRPSPropBag irpspropBag;
			if (propBag != null)
			{
				irpspropBag = propBag.NativeInterface;
			}
			else
			{
				irpspropBag = null;
			}
			string liveIDChallengeHeader = this.m_IRPSHttpAuth.GetLiveIDChallengeHeader(siteName, irpspropBag);
			GC.KeepAlive(this);
			return liveIDChallengeHeader;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0003B790 File Offset: 0x00039990
		public string LogoTag(bool bLogin, bool bSecure, string urlName, string domainName, string siteName, RPSPropBag propBag)
		{
			IRPSPropBag irpspropBag;
			if (propBag != null)
			{
				irpspropBag = propBag.NativeInterface;
			}
			else
			{
				irpspropBag = null;
			}
			string result = this.m_IRPSHttpAuth.LogoTag(bLogin, bSecure, urlName, domainName, siteName, irpspropBag);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0003B7C8 File Offset: 0x000399C8
		public string TextTag(bool bLogin, bool bSecure, string urlName, string domainName, string siteName, RPSPropBag propBag)
		{
			IRPSPropBag irpspropBag;
			if (propBag != null)
			{
				irpspropBag = propBag.NativeInterface;
			}
			else
			{
				irpspropBag = null;
			}
			string result = this.m_IRPSHttpAuth.TextTag(bLogin, bSecure, urlName, domainName, siteName, irpspropBag);
			GC.KeepAlive(this);
			return result;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0003B800 File Offset: 0x00039A00
		internal static void InternalWriteHeaders(HttpResponse response, string headers)
		{
			if (response == null || headers == null)
			{
				throw new ArgumentException("response and headers cannot be null.");
			}
			int num;
			for (int i = 0; i < headers.Length; i = num + 2)
			{
				num = headers.IndexOf('\r', i);
				if (num < 0)
				{
					num = headers.Length;
				}
				string text = headers.Substring(i, num - i);
				int num2 = text.IndexOf(':');
				if (num2 > 0)
				{
					string name = text.Substring(0, num2);
					string value = text.Substring(num2 + 1);
					response.AddHeader(name, value);
				}
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0003B87B File Offset: 0x00039A7B
		public void WriteHeaders(HttpResponse response, string headers)
		{
			RPSHttpAuthEx.InternalWriteHeaders(response, headers);
		}

		// Token: 0x04000831 RID: 2097
		private RPS m_rps;

		// Token: 0x04000832 RID: 2098
		private bool m_bHasDisposed;

		// Token: 0x04000833 RID: 2099
		private IRPSHttpAuth m_IRPSHttpAuth;
	}
}
