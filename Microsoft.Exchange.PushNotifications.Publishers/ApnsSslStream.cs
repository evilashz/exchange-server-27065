using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200003D RID: 61
	internal class ApnsSslStream : IDisposable
	{
		// Token: 0x06000249 RID: 585 RVA: 0x000089A0 File Offset: 0x00006BA0
		public ApnsSslStream(SslStream sslStream)
		{
			if (sslStream == null)
			{
				throw new ArgumentNullException("sslStream");
			}
			this.SslStream = sslStream;
		}

		// Token: 0x17000097 RID: 151
		// (set) Token: 0x0600024A RID: 586 RVA: 0x000089BD File Offset: 0x00006BBD
		public virtual int ReadTimeout
		{
			set
			{
				this.SslStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (set) Token: 0x0600024B RID: 587 RVA: 0x000089CB File Offset: 0x00006BCB
		public virtual int WriteTimeout
		{
			set
			{
				this.SslStream.WriteTimeout = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600024C RID: 588 RVA: 0x000089D9 File Offset: 0x00006BD9
		public virtual bool CanRead
		{
			get
			{
				return this.SslStream.CanRead;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000089E6 File Offset: 0x00006BE6
		public virtual bool CanWrite
		{
			get
			{
				return this.SslStream.CanWrite;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000089F3 File Offset: 0x00006BF3
		public virtual bool IsMutuallyAuthenticated
		{
			get
			{
				return this.SslStream.IsMutuallyAuthenticated;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00008A00 File Offset: 0x00006C00
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00008A08 File Offset: 0x00006C08
		private SslStream SslStream { get; set; }

		// Token: 0x06000251 RID: 593 RVA: 0x00008A11 File Offset: 0x00006C11
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return this.SslStream.BeginAuthenticateAsClient(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation, asyncCallback, asyncState);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008A27 File Offset: 0x00006C27
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this.SslStream.EndAuthenticateAsClient(asyncResult);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008A35 File Offset: 0x00006C35
		public virtual void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this.SslStream.AuthenticateAsClient(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008A47 File Offset: 0x00006C47
		public virtual int Read(byte[] buffer, int offset, int count)
		{
			return this.SslStream.Read(buffer, offset, count);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008A57 File Offset: 0x00006C57
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return this.SslStream.BeginRead(buffer, offset, count, asyncCallback, asyncState);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008A6B File Offset: 0x00006C6B
		public virtual int EndRead(IAsyncResult ar)
		{
			return this.SslStream.EndRead(ar);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008A79 File Offset: 0x00006C79
		public virtual void Write(byte[] buffer)
		{
			this.SslStream.Write(buffer);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008A87 File Offset: 0x00006C87
		public virtual void Dispose()
		{
			this.SslStream.Close();
		}
	}
}
