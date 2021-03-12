using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MapiHttp;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class RpcHelper
	{
		// Token: 0x06000108 RID: 264 RVA: 0x0000433C File Offset: 0x0000253C
		public static bool IsRealServerName(string serverName)
		{
			return !serverName.Contains("@");
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000434C File Offset: 0x0000254C
		public static RpcBindingInfo BuildRpcProxyOnlyBindingInfo(IPropertyBag propertyBag)
		{
			return RpcHelper.BuildRpcBindingInfo(propertyBag, true, false, 6001);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000435B File Offset: 0x0000255B
		public static RpcBindingInfo BuildCompleteBindingInfo(IPropertyBag propertyBag, int legacyEndpoint)
		{
			return RpcHelper.BuildRpcBindingInfo(propertyBag, propertyBag.IsPropertyFound(ContextPropertySchema.RpcProxyServer), true, legacyEndpoint);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004370 File Offset: 0x00002570
		public static MapiHttpBindingInfo BuildCompleteMapiHttpBindingInfo(IPropertyBag propertyBag)
		{
			return RpcHelper.BuildMapiHttpBindingInfo(propertyBag);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004378 File Offset: 0x00002578
		public static TaskResult FigureOutErrorInformation(IPropertyBag outputPropertyBag, VerifyRpcProxyResult verifyRpcProxyResult)
		{
			if (!verifyRpcProxyResult.IsSuccessful)
			{
				outputPropertyBag.Set(ContextPropertySchema.Exception, verifyRpcProxyResult.Exception);
				outputPropertyBag.Set(ContextPropertySchema.ErrorDetails, string.Format("Status: {0}\nHttpStatusCode: {1}\nHttpStatusDescription: {2}\nProcessedBody: {3}", new object[]
				{
					(verifyRpcProxyResult.Exception != null) ? verifyRpcProxyResult.Exception.Status.ToString() : "OK",
					verifyRpcProxyResult.ResponseStatusCode,
					verifyRpcProxyResult.ResponseStatusDescription,
					verifyRpcProxyResult.ResponseBody
				}));
				return TaskResult.Failed;
			}
			return TaskResult.Success;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004408 File Offset: 0x00002608
		public static bool DetectShouldUseSsl(RpcProxyPort rpcProxyPort)
		{
			int num = (int)rpcProxyPort;
			return !num.ToString().Contains("8");
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000442C File Offset: 0x0000262C
		private static RpcBindingInfo BuildRpcBindingInfo(IPropertyBag propertyBag, bool useHttp, bool addRpcAuthentication, int legacyEndpoint)
		{
			RpcBindingInfo rpcBindingInfo = new RpcBindingInfo();
			rpcBindingInfo.RpcServer = propertyBag.Get(ContextPropertySchema.RpcServer);
			rpcBindingInfo.Timeout = propertyBag.Get(ContextPropertySchema.Timeout);
			rpcBindingInfo.UseUniqueBinding = true;
			string authType;
			if (useHttp)
			{
				RpcHelper.AddRpcHttpInfo(rpcBindingInfo, propertyBag, legacyEndpoint);
				authType = RpcHelper.HttpAuthenticationSchemeMapping.Get(propertyBag.Get(ContextPropertySchema.RpcProxyAuthenticationType));
			}
			else
			{
				rpcBindingInfo.UseTcp();
				authType = RpcHelper.AuthenticationServiceMapping.Get(propertyBag.Get(ContextPropertySchema.RpcAuthenticationType));
			}
			ICredentials credentials = propertyBag.Get(ContextPropertySchema.Credentials);
			rpcBindingInfo.Credential = ((credentials != null) ? credentials.GetCredential(rpcBindingInfo.Uri, authType) : null);
			if (addRpcAuthentication)
			{
				RpcHelper.AddRpcAuthenticationInfo(rpcBindingInfo, propertyBag.Get(ContextPropertySchema.RpcAuthenticationType));
			}
			int num = Convert.ToInt32(rpcBindingInfo.Timeout.TotalSeconds);
			int num2 = (num >= 10) ? (num - 10) : num;
			int num3 = num2 / 2;
			rpcBindingInfo.RpcHttpHeaders.Add(WellKnownHeader.FrontEndToBackEndTimeout, num3.ToString());
			return rpcBindingInfo;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004524 File Offset: 0x00002724
		private static MapiHttpBindingInfo BuildMapiHttpBindingInfo(IPropertyBag propertyBag)
		{
			string text = propertyBag.Get(ContextPropertySchema.MapiHttpPersonalizedServerName);
			string serverFqdn = propertyBag.Get(ContextPropertySchema.MapiHttpServer);
			bool ignoreCertificateErrors = propertyBag.Get(ContextPropertySchema.IgnoreInvalidServerCertificateSubject);
			RpcProxyPort rpcProxyPort = propertyBag.Get(ContextPropertySchema.RpcProxyPort);
			bool useSsl = RpcHelper.DetectShouldUseSsl(rpcProxyPort);
			MapiHttpBindingInfo mapiHttpBindingInfo = new MapiHttpBindingInfo(serverFqdn, (int)rpcProxyPort, useSsl, propertyBag.Get(ContextPropertySchema.Credentials), ignoreCertificateErrors, RpcHelper.LooksLikePersonalizedServerName(text) ? text : null);
			WebHeaderCollection webHeaderCollection = propertyBag.Get(ContextPropertySchema.AdditionalHttpHeaders);
			if (webHeaderCollection != null && webHeaderCollection.Count > 0)
			{
				mapiHttpBindingInfo.AdditionalHttpHeaders = webHeaderCollection;
			}
			int val = Convert.ToInt32(propertyBag.Get(ContextPropertySchema.Timeout).TotalSeconds);
			int val2 = Convert.ToInt32(Constants.HttpRequestTimeout.TotalSeconds);
			int num = Math.Min(val, val2);
			int num2 = (num >= 10) ? (num - 10) : num;
			mapiHttpBindingInfo.AdditionalHttpHeaders.Add(WellKnownHeader.FrontEndToBackEndTimeout, num2.ToString());
			return mapiHttpBindingInfo;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004618 File Offset: 0x00002818
		private static bool LooksLikePersonalizedServerName(string mailboxId)
		{
			int num = mailboxId.IndexOf('@');
			return num > 0 && num != mailboxId.Length - 1;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004640 File Offset: 0x00002840
		private static void AddRpcHttpInfo(RpcBindingInfo bindingInfo, IPropertyBag propertyBag, int legacyEndpoint)
		{
			bindingInfo.UseRpcProxy((propertyBag.Get(ContextPropertySchema.RpcEndpoint) == OutlookEndpointSelection.Consolidated) ? 6001 : legacyEndpoint, propertyBag.Get(ContextPropertySchema.RpcProxyServer), propertyBag.Get(ContextPropertySchema.RpcProxyPort));
			bindingInfo.RpcProxyAuthentication = propertyBag.Get(ContextPropertySchema.RpcProxyAuthenticationType);
			string text = propertyBag.Get(ContextPropertySchema.OutlookSessionCookieValue);
			if (!string.IsNullOrEmpty(text))
			{
				string value = "\"" + text + " Client=ACTIVEMONITORING" + "\"";
				bindingInfo.RpcHttpCookies.Add(new Cookie("OutlookSession", value));
			}
			bindingInfo.RpcHttpHeaders.Add(propertyBag.Get(ContextPropertySchema.AdditionalHttpHeaders));
			bindingInfo.WebProxyServer = propertyBag.Get(ContextPropertySchema.WebProxyServer);
			bindingInfo.UseSsl = RpcHelper.DetectShouldUseSsl(bindingInfo.RpcProxyPort);
			bindingInfo.IgnoreInvalidRpcProxyServerCertificateSubject = propertyBag.Get(ContextPropertySchema.IgnoreInvalidServerCertificateSubject);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000471C File Offset: 0x0000291C
		private static void AddRpcAuthenticationInfo(RpcBindingInfo bindingInfo, AuthenticationService rpcAuthenticationType)
		{
			bindingInfo.RpcAuthentication = rpcAuthenticationType;
			if (!RpcHelper.IsRealServerName(bindingInfo.RpcServer) && bindingInfo.RpcAuthentication != AuthenticationService.None && bindingInfo.RpcAuthentication != AuthenticationService.Ntlm && bindingInfo.RpcAuthentication != AuthenticationService.Negotiate)
			{
				throw new WrongPropertyValueCombinationException(Strings.WrongAuthForPersonalizedServer);
			}
			bindingInfo.UseRpcEncryption = (bindingInfo.RpcAuthentication != AuthenticationService.None);
		}

		// Token: 0x04000054 RID: 84
		public static readonly ContextProperty[] DependenciesOfBuildRpcProxyOnlyBindingInfo = new ContextProperty[]
		{
			ContextPropertySchema.Credentials.GetOnly(),
			ContextPropertySchema.RpcServer.GetOnly(),
			ContextPropertySchema.RpcEndpoint.GetOnly(),
			ContextPropertySchema.AdditionalHttpHeaders.GetOnly(),
			ContextPropertySchema.RpcProxyServer.GetOnly(),
			ContextPropertySchema.RpcProxyPort.GetOnly(),
			ContextPropertySchema.RpcProxyAuthenticationType.GetOnly(),
			ContextPropertySchema.IgnoreInvalidServerCertificateSubject.GetOnly(),
			ContextPropertySchema.OutlookSessionCookieValue.GetOnly(),
			ContextPropertySchema.WebProxyServer.GetOnly(),
			ContextPropertySchema.Timeout.GetOnly()
		};

		// Token: 0x04000055 RID: 85
		public static readonly ContextProperty[] DependenciesOfBuildCompleteBindingInfo = RpcHelper.DependenciesOfBuildRpcProxyOnlyBindingInfo.Concat(new ContextProperty[]
		{
			ContextPropertySchema.RpcAuthenticationType.GetOnly()
		});

		// Token: 0x04000056 RID: 86
		public static readonly ContextProperty[] DependenciesOfBuildMapiHttpBindingInfo = new ContextProperty[]
		{
			ContextPropertySchema.Credentials.GetOnly(),
			ContextPropertySchema.MapiHttpPersonalizedServerName.GetOnly(),
			ContextPropertySchema.MapiHttpServer.GetOnly(),
			ContextPropertySchema.AdditionalHttpHeaders.GetOnly(),
			ContextPropertySchema.RpcProxyPort.GetOnly(),
			ContextPropertySchema.IgnoreInvalidServerCertificateSubject.GetOnly(),
			ContextPropertySchema.Timeout.GetOnly()
		};

		// Token: 0x04000057 RID: 87
		public static readonly RpcHelper.Mapping<AuthenticationService, string> AuthenticationServiceMapping = new RpcHelper.Mapping<AuthenticationService, string>(null, StringComparer.OrdinalIgnoreCase)
		{
			{
				AuthenticationService.None,
				"Anonymous"
			},
			{
				AuthenticationService.None,
				string.Empty
			},
			{
				AuthenticationService.None,
				null
			},
			{
				AuthenticationService.Negotiate,
				"Negotiate"
			},
			{
				AuthenticationService.Kerberos,
				"Kerberos"
			},
			{
				AuthenticationService.Ntlm,
				"Ntlm"
			}
		};

		// Token: 0x04000058 RID: 88
		public static readonly RpcHelper.Mapping<HttpAuthenticationScheme, string> HttpAuthenticationSchemeMapping = new RpcHelper.Mapping<HttpAuthenticationScheme, string>(null, StringComparer.OrdinalIgnoreCase)
		{
			{
				HttpAuthenticationScheme.Basic,
				"Basic"
			},
			{
				HttpAuthenticationScheme.Negotiate,
				"Negotiate"
			},
			{
				HttpAuthenticationScheme.Ntlm,
				"Ntlm"
			}
		};

		// Token: 0x0200002A RID: 42
		public class Mapping<X, Y> : IEnumerable<KeyValuePair<X, Y>>, IEnumerable
		{
			// Token: 0x06000114 RID: 276 RVA: 0x00004949 File Offset: 0x00002B49
			public Mapping(IEqualityComparer<X> comparerX = null, IEqualityComparer<Y> comparerY = null)
			{
				this.comparerX = (comparerX ?? EqualityComparer<X>.Default);
				this.comparerY = (comparerY ?? EqualityComparer<Y>.Default);
			}

			// Token: 0x06000115 RID: 277 RVA: 0x0000497C File Offset: 0x00002B7C
			public void Add(X x, Y y)
			{
				this.inner.Add(new KeyValuePair<X, Y>(x, y));
			}

			// Token: 0x06000116 RID: 278 RVA: 0x00004990 File Offset: 0x00002B90
			public X Get(Y y)
			{
				foreach (KeyValuePair<X, Y> keyValuePair in this.inner)
				{
					if (this.comparerY.Equals(keyValuePair.Value, y))
					{
						return keyValuePair.Key;
					}
				}
				throw RpcHelper.Mapping<X, Y>.BadValueException<Y>(y);
			}

			// Token: 0x06000117 RID: 279 RVA: 0x00004A04 File Offset: 0x00002C04
			public Y Get(X x)
			{
				foreach (KeyValuePair<X, Y> keyValuePair in this.inner)
				{
					if (this.comparerX.Equals(keyValuePair.Key, x))
					{
						return keyValuePair.Value;
					}
				}
				throw RpcHelper.Mapping<X, Y>.BadValueException<X>(x);
			}

			// Token: 0x06000118 RID: 280 RVA: 0x00004A78 File Offset: 0x00002C78
			public IEnumerator<KeyValuePair<X, Y>> GetEnumerator()
			{
				return this.inner.GetEnumerator();
			}

			// Token: 0x06000119 RID: 281 RVA: 0x00004A8A File Offset: 0x00002C8A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600011A RID: 282 RVA: 0x00004A92 File Offset: 0x00002C92
			private static Exception BadValueException<T>(T value)
			{
				return new ArgumentException(string.Format("{0} {1} is not supported", typeof(T).Name, value));
			}

			// Token: 0x04000059 RID: 89
			private readonly List<KeyValuePair<X, Y>> inner = new List<KeyValuePair<X, Y>>();

			// Token: 0x0400005A RID: 90
			private readonly IEqualityComparer<X> comparerX;

			// Token: 0x0400005B RID: 91
			private readonly IEqualityComparer<Y> comparerY;
		}
	}
}
