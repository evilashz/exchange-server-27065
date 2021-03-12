using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ProvisioningAgent;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x0200005A RID: 90
	internal class SingleProxySession : IDisposable
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000E984 File Offset: 0x0000CB84
		public ProxyAddressTemplate BaseAddress
		{
			get
			{
				return this.baseAddress;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000E98C File Offset: 0x0000CB8C
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000E994 File Offset: 0x0000CB94
		public ProxyDLL ProxyDll
		{
			get
			{
				return this.proxyDll;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000E99C File Offset: 0x0000CB9C
		public bool IsInitialized
		{
			get
			{
				return this.isInitialized;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		public SingleProxySession(ProxyDLL proxyDll, ProxyAddressTemplate baseAddress, string serverName)
		{
			this.proxyDll = proxyDll;
			this.serverName = serverName;
			this.baseAddress = baseAddress;
			this.hProxySession = IntPtr.Zero;
			this.isInitialized = false;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000E9D3 File Offset: 0x0000CBD3
		protected void Dispose(bool disposing)
		{
			if (disposing && this.isInitialized)
			{
				this.CloseProxies();
				this.isInitialized = false;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000E9ED File Offset: 0x0000CBED
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public void Initialize()
		{
			if (!this.isInitialized)
			{
				this.InitProxies();
				this.isInitialized = true;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000EA13 File Offset: 0x0000CC13
		public void UnInitialize()
		{
			if (this.isInitialized)
			{
				this.CloseProxies();
				this.isInitialized = false;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000EA2C File Offset: 0x0000CC2C
		public string GenerateProxy(RecipientInfo pRecipientInfo, int nRetries)
		{
			IntPtr zero = IntPtr.Zero;
			string result = null;
			try
			{
				this.GenerateProxy(pRecipientInfo, nRetries, out zero);
				if (zero != IntPtr.Zero)
				{
					result = Marshal.PtrToStringUni(zero);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					this.FreeProxy(zero);
				}
			}
			return result;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000EA88 File Offset: 0x0000CC88
		private void InitProxies()
		{
			ReturnCode rc = this.proxyDll.RcInitProxies(this.BaseAddress.ToString(), this.ServerName, out this.hProxySession);
			this.CheckReturnCode(rc);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
		private void GenerateProxy(RecipientInfo pRecipientInfo, int nRetries, out IntPtr ppszProxyAddr)
		{
			ReturnCode rc = this.proxyDll.RcGenerateProxy(this.hProxySession, ref pRecipientInfo, nRetries, out ppszProxyAddr);
			this.CheckReturnCode(rc);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000EAF9 File Offset: 0x0000CCF9
		private void FreeProxy(IntPtr pszProxy)
		{
			this.proxyDll.FreeProxy(pszProxy);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		public bool CheckProxy(RecipientInfo pRecipientInfo, string pwszProxyAddr)
		{
			bool result = false;
			if (this.proxyDll.RcCheckProxy != null)
			{
				ReturnCode rc = this.proxyDll.RcCheckProxy(this.hProxySession, ref pRecipientInfo, pwszProxyAddr, out result);
				this.CheckReturnCode(rc);
			}
			return result;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000EB50 File Offset: 0x0000CD50
		public bool ValidateBaseAddress(string pszBaseAddress)
		{
			StringBuilder pwstrSiteProxy = new StringBuilder(pszBaseAddress);
			bool result = false;
			ReturnCode rc = this.proxyDll.RcValidateSiteProxy(this.hProxySession, pwstrSiteProxy, out result);
			this.CheckReturnCode(rc);
			return result;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000EB8A File Offset: 0x0000CD8A
		private void CloseProxies()
		{
			this.proxyDll.CloseProxies(this.hProxySession);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		public bool ValidateProxy(string proxyAddress)
		{
			StringBuilder stringBuilder = new StringBuilder(proxyAddress);
			bool flag = false;
			ReturnCode rc = this.proxyDll.RcValidateProxy(this.hProxySession, stringBuilder, out flag);
			this.CheckReturnCode(rc);
			return flag && StringComparer.InvariantCultureIgnoreCase.Equals(stringBuilder.ToString(), proxyAddress);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		private void CheckReturnCode(ReturnCode rc)
		{
			if (rc == ReturnCode.RC_SUCCESS)
			{
				return;
			}
			LocalizedString value;
			switch (rc)
			{
			case ReturnCode.RC_ERROR:
				value = Strings.ProxyDLLError;
				break;
			case ReturnCode.RC_PROTOCOL:
				value = Strings.ProxyDLLProtocol;
				break;
			case ReturnCode.RC_SYNTAX:
				value = Strings.ProxyDLLSyntax;
				break;
			case ReturnCode.RC_EOF:
				value = Strings.ProxyDLLEOF;
				break;
			case ReturnCode.RC_IMPLEMENTATION:
				value = Strings.ProxyNotImplemented;
				break;
			case ReturnCode.RC_SOFTWARE:
				value = Strings.ProxyDLLSoftware;
				break;
			case ReturnCode.RC_CONFIG:
				value = Strings.ProxyDLLConfig;
				break;
			case ReturnCode.RC_MEMORY:
				value = Strings.ProxyDLLOOM;
				break;
			case ReturnCode.RC_CONTENTION:
				value = Strings.ProxyDLLContention;
				break;
			case ReturnCode.RC_NOTFOUND:
				value = Strings.ProxyDLLNotFound;
				break;
			case ReturnCode.RC_DISKSPACE:
				value = Strings.ProxyDLLDiskSpace;
				break;
			default:
				value = Strings.ProxyDLLDefault;
				break;
			}
			throw new RusException(Strings.ErrorProxyGeneration(value));
		}

		// Token: 0x0400012F RID: 303
		private ProxyDLL proxyDll;

		// Token: 0x04000130 RID: 304
		private IntPtr hProxySession;

		// Token: 0x04000131 RID: 305
		private bool isInitialized;

		// Token: 0x04000132 RID: 306
		private ProxyAddressTemplate baseAddress;

		// Token: 0x04000133 RID: 307
		private string serverName;
	}
}
