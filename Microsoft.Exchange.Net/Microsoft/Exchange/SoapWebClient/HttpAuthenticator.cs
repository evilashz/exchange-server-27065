using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006CB RID: 1739
	internal abstract class HttpAuthenticator
	{
		// Token: 0x0600208A RID: 8330 RVA: 0x000405EB File Offset: 0x0003E7EB
		public static HttpAuthenticator Create(ICredentials credentials)
		{
			return new HttpAuthenticator.GenericICredentialsHttpAuthenticator(credentials);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000405F3 File Offset: 0x0003E7F3
		public static HttpAuthenticator Create(CommonAccessToken commonAccessToken)
		{
			return new HttpAuthenticator.CommonAccessTokenHttpAuthenticator(commonAccessToken);
		}

		// Token: 0x0600208C RID: 8332
		public abstract T AuthenticateAndExecute<T>(CustomSoapHttpClientProtocol client, AuthenticateAndExecuteHandler<T> handler);

		// Token: 0x0600208D RID: 8333
		public abstract T AuthenticateAndExecute<T>(WebRequest request, AuthenticateAndExecuteHandler<T> handler);

		// Token: 0x04001F4A RID: 8010
		public static readonly HttpAuthenticator None = new HttpAuthenticator.NoHttpAuthenticator();

		// Token: 0x04001F4B RID: 8011
		public static readonly HttpAuthenticator NetworkService = new HttpAuthenticator.NetworkServiceHttpAuthenticator();

		// Token: 0x04001F4C RID: 8012
		private static readonly Trace Tracer = ExTraceGlobals.EwsClientTracer;

		// Token: 0x020006CC RID: 1740
		private sealed class NoHttpAuthenticator : HttpAuthenticator
		{
			// Token: 0x06002090 RID: 8336 RVA: 0x00040623 File Offset: 0x0003E823
			public override T AuthenticateAndExecute<T>(CustomSoapHttpClientProtocol client, AuthenticateAndExecuteHandler<T> handler)
			{
				return handler();
			}

			// Token: 0x06002091 RID: 8337 RVA: 0x0004062B File Offset: 0x0003E82B
			public override T AuthenticateAndExecute<T>(WebRequest request, AuthenticateAndExecuteHandler<T> handler)
			{
				return handler();
			}
		}

		// Token: 0x020006CD RID: 1741
		private sealed class GenericICredentialsHttpAuthenticator : HttpAuthenticator
		{
			// Token: 0x06002093 RID: 8339 RVA: 0x0004063B File Offset: 0x0003E83B
			public GenericICredentialsHttpAuthenticator(ICredentials credentials)
			{
				this.credentials = credentials;
			}

			// Token: 0x06002094 RID: 8340 RVA: 0x0004064A File Offset: 0x0003E84A
			public override T AuthenticateAndExecute<T>(CustomSoapHttpClientProtocol client, AuthenticateAndExecuteHandler<T> handler)
			{
				client.PreAuthenticate = true;
				client.Credentials = this.credentials;
				return handler();
			}

			// Token: 0x06002095 RID: 8341 RVA: 0x00040665 File Offset: 0x0003E865
			public override T AuthenticateAndExecute<T>(WebRequest request, AuthenticateAndExecuteHandler<T> handler)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04001F4D RID: 8013
			private ICredentials credentials;
		}

		// Token: 0x020006CE RID: 1742
		private sealed class NetworkServiceHttpAuthenticator : HttpAuthenticator
		{
			// Token: 0x06002096 RID: 8342 RVA: 0x0004066C File Offset: 0x0003E86C
			public override T AuthenticateAndExecute<T>(CustomSoapHttpClientProtocol client, AuthenticateAndExecuteHandler<T> handler)
			{
				client.PreAuthenticate = true;
				T result;
				try
				{
					using (NetworkServiceImpersonator.Impersonate())
					{
						client.Credentials = CredentialCache.DefaultCredentials;
						HttpAuthenticator.Tracer.TraceDebug((long)this.GetHashCode(), "Impersonated network service.");
						result = handler();
					}
				}
				catch
				{
					throw;
				}
				return result;
			}

			// Token: 0x06002097 RID: 8343 RVA: 0x000406DC File Offset: 0x0003E8DC
			public override T AuthenticateAndExecute<T>(WebRequest request, AuthenticateAndExecuteHandler<T> handler)
			{
				request.PreAuthenticate = true;
				T result;
				try
				{
					using (NetworkServiceImpersonator.Impersonate())
					{
						request.Credentials = CredentialCache.DefaultCredentials;
						HttpAuthenticator.Tracer.TraceDebug((long)this.GetHashCode(), "Impersonated network service.");
						result = handler();
					}
				}
				catch
				{
					throw;
				}
				return result;
			}
		}

		// Token: 0x020006CF RID: 1743
		private sealed class CommonAccessTokenHttpAuthenticator : HttpAuthenticator
		{
			// Token: 0x06002099 RID: 8345 RVA: 0x00040754 File Offset: 0x0003E954
			public CommonAccessTokenHttpAuthenticator(CommonAccessToken commonAccessToken)
			{
				this.token = commonAccessToken.Serialize();
			}

			// Token: 0x0600209A RID: 8346 RVA: 0x00040768 File Offset: 0x0003E968
			public override T AuthenticateAndExecute<T>(CustomSoapHttpClientProtocol client, AuthenticateAndExecuteHandler<T> handler)
			{
				T result;
				try
				{
					using (NetworkServiceImpersonator.Impersonate())
					{
						HttpAuthenticator.Tracer.TraceDebug((long)this.GetHashCode(), "Impersonated network service.");
						client.PreAuthenticate = true;
						client.Credentials = CredentialCache.DefaultCredentials;
						client.HttpHeaders.Add("X-CommonAccessToken", this.token);
						result = handler();
					}
				}
				catch
				{
					throw;
				}
				return result;
			}

			// Token: 0x0600209B RID: 8347 RVA: 0x000407EC File Offset: 0x0003E9EC
			public override T AuthenticateAndExecute<T>(WebRequest request, AuthenticateAndExecuteHandler<T> handler)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04001F4E RID: 8014
			private readonly string token;
		}
	}
}
