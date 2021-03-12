using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000029 RID: 41
	internal class ProtocolResponse : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00009087 File Offset: 0x00007287
		public ProtocolResponse(ProtocolRequest request) : this(request, null)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009091 File Offset: 0x00007291
		public ProtocolResponse(string input) : this(null, input)
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000909B File Offset: 0x0000729B
		public ProtocolResponse(ProtocolRequest request, string input)
		{
			this.responseStringBuilder = new StringBuilder(256);
			this.responseStringBuilder.Append(input);
			this.request = request;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000090D3 File Offset: 0x000072D3
		public ProtocolRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000090DB File Offset: 0x000072DB
		// (set) Token: 0x06000249 RID: 585 RVA: 0x000090E3 File Offset: 0x000072E3
		public bool IsDisconnectResponse
		{
			get
			{
				return this.disconnectAfterSend;
			}
			set
			{
				this.disconnectAfterSend = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000090EC File Offset: 0x000072EC
		public bool NeedToDelayStoreAction
		{
			get
			{
				return this.request != null && this.request.NeedToDelayStoreAction;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00009103 File Offset: 0x00007303
		public virtual bool IsCommandFailedResponse
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00009106 File Offset: 0x00007306
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000910E File Offset: 0x0000730E
		public bool StartTls
		{
			get
			{
				return this.startTlsAfterSend;
			}
			set
			{
				this.startTlsAfterSend = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00009117 File Offset: 0x00007317
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00009124 File Offset: 0x00007324
		public string MessageString
		{
			get
			{
				return this.responseStringBuilder.ToString();
			}
			set
			{
				this.responseStringBuilder.Length = 0;
				this.responseStringBuilder.Append(value);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000913F File Offset: 0x0000733F
		public virtual string DataToSend
		{
			get
			{
				return this.responseStringBuilder.ToString();
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000914C File Offset: 0x0000734C
		public void Append(char value)
		{
			this.responseStringBuilder.Append(value);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000915B File Offset: 0x0000735B
		public void Append(string value)
		{
			this.responseStringBuilder.Append(value);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000916A File Offset: 0x0000736A
		public void AppendFormat(string format, params object[] args)
		{
			this.responseStringBuilder.AppendFormat(format, args);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000917A File Offset: 0x0000737A
		public void AppendFormat(string format, object arg0)
		{
			this.responseStringBuilder.AppendFormat(format, arg0);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000918A File Offset: 0x0000738A
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ProtocolResponse>(this);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009192 File Offset: 0x00007392
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000091A7 File Offset: 0x000073A7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000091B8 File Offset: 0x000073B8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.request != null)
				{
					IDisposable disposable = this.request as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					this.request = null;
				}
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009208 File Offset: 0x00007408
		protected StringBuilder GetAuthError()
		{
			IProxyLogin proxyLogin = this.Request as IProxyLogin;
			if (proxyLogin != null && (!string.IsNullOrEmpty(proxyLogin.ClientIp) || ProtocolBaseServices.AuthErrorReportEnabled(proxyLogin.UserName)))
			{
				StringBuilder stringBuilder = new StringBuilder(128);
				if (!string.IsNullOrEmpty(proxyLogin.AuthenticationError))
				{
					if (proxyLogin.AuthenticationError.IndexOf('"') > -1)
					{
						throw new ApplicationException("Error string contains quotes:" + proxyLogin.AuthenticationError);
					}
					if (proxyLogin.AuthenticationError.IndexOfAny(ProtocolResponse.QuotableChars) > -1)
					{
						stringBuilder.AppendFormat("Error=\"{0}\"", proxyLogin.AuthenticationError);
					}
					else
					{
						stringBuilder.AppendFormat("Error={0}", proxyLogin.AuthenticationError);
					}
				}
				if (!string.IsNullOrEmpty(proxyLogin.ProxyDestination))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.AppendFormat("Proxy={0}", proxyLogin.ProxyDestination);
				}
				if (stringBuilder.Length > 0)
				{
					return stringBuilder;
				}
			}
			return null;
		}

		// Token: 0x0400014C RID: 332
		protected static readonly char[] QuotableChars = new char[]
		{
			' ',
			'='
		};

		// Token: 0x0400014D RID: 333
		private bool disconnectAfterSend;

		// Token: 0x0400014E RID: 334
		private bool startTlsAfterSend;

		// Token: 0x0400014F RID: 335
		private StringBuilder responseStringBuilder;

		// Token: 0x04000150 RID: 336
		private ProtocolRequest request;

		// Token: 0x04000151 RID: 337
		private DisposeTracker disposeTracker;
	}
}
