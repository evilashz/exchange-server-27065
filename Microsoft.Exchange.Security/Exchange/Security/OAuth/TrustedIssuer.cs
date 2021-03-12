using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000FE RID: 254
	internal sealed class TrustedIssuer
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x00037E20 File Offset: 0x00036020
		private TrustedIssuer(IssuerMetadata metadata, Dictionary<string, X509Certificate2> x5tCertMap, TrustedIssuer.OnlineCertificateProvider onlineCertificateProvider)
		{
			OAuthCommon.VerifyNonNullArgument("metadata", metadata);
			OAuthCommon.VerifyNonNullArgument("x5tCertMap", x5tCertMap);
			this.metadata = metadata;
			this.x5tCertMap = x5tCertMap;
			this.onlineCertificateProvider = onlineCertificateProvider;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00037E55 File Offset: 0x00036055
		public static TrustedIssuer CreateFromExchangeCallback(LocalConfiguration localConfiguration, string realm)
		{
			return new TrustedIssuer(new IssuerMetadata(IssuerKind.PartnerApp, localConfiguration.ApplicationId, realm), TrustedIssuer.GetOfflineCertMap("self", localConfiguration.Certificates), null);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00037E7C File Offset: 0x0003607C
		public static bool TryCreateFromAuthServer(AuthServer authServer, out TrustedIssuer trustedIssuer)
		{
			trustedIssuer = null;
			IssuerKind kind;
			switch (authServer.Type)
			{
			case AuthServerType.MicrosoftACS:
				kind = IssuerKind.ACS;
				goto IL_3C;
			case AuthServerType.ADFS:
				kind = IssuerKind.ADFS;
				goto IL_3C;
			case AuthServerType.AzureAD:
				kind = IssuerKind.AzureAD;
				goto IL_3C;
			}
			throw new InvalidOperationException();
			IL_3C:
			IssuerMetadata issuerMetadata = new IssuerMetadata(kind, authServer.IssuerIdentifier, authServer.Realm);
			TrustedIssuer.OnlineCertificateProvider onlineCertificateProvider = TrustedIssuer.GetOnlineCertificateProvider(authServer.AuthMetadataUrl);
			Dictionary<string, X509Certificate2> dictionary = TrustedIssuer.CombineCertificates(authServer.Name, authServer.CertificateBytes, onlineCertificateProvider);
			if (dictionary.Count == 0)
			{
				trustedIssuer = null;
				OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthAuthServerMissingCertificates, authServer.Name, new object[]
				{
					authServer.Name,
					authServer.ApplicationIdentifier
				});
				return false;
			}
			trustedIssuer = new TrustedIssuer(issuerMetadata, dictionary, onlineCertificateProvider);
			return true;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00037F44 File Offset: 0x00036144
		public static bool TryCreateFromPartnerApplication(PartnerApplication partnerApp, out TrustedIssuer trustedIssuer)
		{
			trustedIssuer = null;
			IssuerMetadata issuerMetadata = new IssuerMetadata(IssuerKind.PartnerApp, string.IsNullOrEmpty(partnerApp.IssuerIdentifier) ? partnerApp.ApplicationIdentifier : partnerApp.IssuerIdentifier, partnerApp.Realm);
			TrustedIssuer.OnlineCertificateProvider onlineCertificateProvider = TrustedIssuer.GetOnlineCertificateProvider(partnerApp.AuthMetadataUrl);
			Dictionary<string, X509Certificate2> dictionary = TrustedIssuer.CombineCertificates(partnerApp.Name, partnerApp.CertificateBytes, onlineCertificateProvider);
			if (dictionary.Count == 0)
			{
				OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthPartnerApplicationMissingCertificates, partnerApp.Name, new object[]
				{
					partnerApp.Name,
					partnerApp.ApplicationIdentifier
				});
				return false;
			}
			trustedIssuer = new TrustedIssuer(issuerMetadata, dictionary, onlineCertificateProvider);
			trustedIssuer.application = partnerApp;
			return true;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00038008 File Offset: 0x00036208
		public static TrustedIssuer.OnlineCertificateProvider GetOnlineCertificateProvider(string metadataUrlString)
		{
			if (Uri.IsWellFormedUriString(metadataUrlString, UriKind.Absolute))
			{
				switch (AuthMetadataParser.DecideMetadataDocumentType(metadataUrlString))
				{
				case AuthMetadataParser.MetadataDocType.OAuthS2SV1Metadata:
					return TrustedIssuer.onlineCertProviders.GetOrAdd(metadataUrlString, (string url) => new TrustedIssuer.JsonOnlineCertificateProvider(url));
				case AuthMetadataParser.MetadataDocType.WSFedMetadata:
					return TrustedIssuer.onlineCertProviders.GetOrAdd(metadataUrlString, (string url) => new TrustedIssuer.XmlOnlineCertificateProvider(url));
				case AuthMetadataParser.MetadataDocType.OAuthOpenIdConnectMetadata:
					return TrustedIssuer.onlineCertProviders.GetOrAdd(metadataUrlString, (string url) => new TrustedIssuer.OpenIdConnectCertificateProvider(url));
				}
				return TrustedIssuer.onlineCertProviders.GetOrAdd(metadataUrlString, (string url) => new TrustedIssuer.JsonOnlineCertificateProvider(url));
			}
			return TrustedIssuer.OnlineCertificateProvider.NullProvider;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000380EC File Offset: 0x000362EC
		private static Dictionary<string, X509Certificate2> CombineCertificates(string name, MultiValuedProperty<byte[]> offlineCertBytes, TrustedIssuer.OnlineCertificateProvider onlineCertificateProvider)
		{
			Dictionary<string, X509Certificate2> offlineCertMap = TrustedIssuer.GetOfflineCertMap(name, offlineCertBytes);
			X509Certificate2[] certificates = onlineCertificateProvider.GetCertificates();
			if (certificates != null)
			{
				bool flag = false;
				foreach (X509Certificate2 x509Certificate in certificates)
				{
					string text = OAuthCommon.Base64UrlEncoder.EncodeBytes(x509Certificate.GetCertHash());
					if (!offlineCertMap.ContainsKey(text))
					{
						ExTraceGlobals.OAuthTracer.TraceDebug<string, string, string>(0L, "[TrustedIssuer:CombineCertificates] found new online certificates with x5t '{0}' for {1} from {2}", text, name, onlineCertificateProvider.MetadataUrl);
						offlineCertMap.Add(text, x509Certificate);
						flag = true;
					}
				}
				if (flag)
				{
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthNewCertificatesFromMetadataUrl, onlineCertificateProvider.MetadataUrl, new object[]
					{
						onlineCertificateProvider.MetadataUrl
					});
				}
			}
			return offlineCertMap;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0003819E File Offset: 0x0003639E
		private static Dictionary<string, X509Certificate2> GetOfflineCertMap(string name, MultiValuedProperty<byte[]> offlineCertBytes)
		{
			return TrustedIssuer.GetOfflineCertMap(name, (from certByte in offlineCertBytes
			select new X509Certificate2(certByte)).ToArray<X509Certificate2>());
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000381D0 File Offset: 0x000363D0
		private static Dictionary<string, X509Certificate2> GetOfflineCertMap(string name, X509Certificate2[] offlineCerts)
		{
			Dictionary<string, X509Certificate2> dictionary = new Dictionary<string, X509Certificate2>(4);
			foreach (X509Certificate2 x509Certificate in offlineCerts)
			{
				string text = OAuthCommon.Base64UrlEncoder.EncodeBytes(x509Certificate.GetCertHash());
				if (dictionary.ContainsKey(text))
				{
					ExTraceGlobals.OAuthTracer.TraceWarning<string, string>(0L, "[TrustedIssuer:CombineCertificates] duplicate certificates with x5t '{0}' were found for issuer {1}", text, name);
				}
				else
				{
					dictionary.Add(text, x509Certificate);
				}
			}
			return dictionary;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00038230 File Offset: 0x00036430
		public IssuerMetadata IssuerMetadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00038238 File Offset: 0x00036438
		public PartnerApplication PartnerApplication
		{
			get
			{
				if (this.application == null)
				{
					throw new InvalidOperationException();
				}
				return this.application;
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00038258 File Offset: 0x00036458
		public SecurityKeyIdentifier GetSecurityKeyIdentifier(string x5tHint)
		{
			X509Certificate2 x509Certificate = null;
			if (!string.IsNullOrEmpty(x5tHint) && this.x5tCertMap.TryGetValue(x5tHint, out x509Certificate))
			{
				ExTraceGlobals.OAuthTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[TrustedIssuer:GetSecurityKeyIdentifier] found cert based on x5t value '{0}': {1}", x5tHint, x509Certificate.Subject);
				return new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					new X509RawDataKeyIdentifierClause(x509Certificate)
				});
			}
			ExTraceGlobals.OAuthTracer.TraceDebug<string>((long)this.GetHashCode(), "[TrustedIssuer:GetSecurityKeyIdentifier] did not find cert based on x5t value '{0}', returning all certs", x5tHint);
			return new SecurityKeyIdentifier((from cert in this.x5tCertMap.Values
			select new X509RawDataKeyIdentifierClause(cert)).ToArray<X509RawDataKeyIdentifierClause>());
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00038304 File Offset: 0x00036504
		public void SetSigningTokens(string x5tHint, TokenValidationParameters tokenValidationParameters)
		{
			X509Certificate2 x509Certificate = null;
			if (!string.IsNullOrEmpty(x5tHint) && this.x5tCertMap.TryGetValue(x5tHint, out x509Certificate))
			{
				ExTraceGlobals.OAuthTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[TrustedIssuer:GetSecurityKeyIdentifier] found cert based on x5t value '{0}': {1}", x5tHint, x509Certificate.Subject);
				tokenValidationParameters.SigningToken = new X509SecurityToken(x509Certificate);
				return;
			}
			ExTraceGlobals.OAuthTracer.TraceDebug<string>((long)this.GetHashCode(), "[TrustedIssuer:GetSecurityKeyIdentifier] did not find cert based on x5t value '{0}', returning all certs", x5tHint);
			List<SecurityToken> list = new List<SecurityToken>();
			foreach (X509Certificate2 certificate in this.x5tCertMap.Values)
			{
				list.Add(new X509SecurityToken(certificate));
			}
			tokenValidationParameters.SigningTokens = list;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000383CC File Offset: 0x000365CC
		public void PokeOnlineCertificateProvider()
		{
			this.onlineCertificateProvider.NotifyValidationFailure();
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000383DC File Offset: 0x000365DC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "TrustedIssuer ({0})", new object[]
			{
				this.IssuerMetadata
			});
		}

		// Token: 0x040007CC RID: 1996
		private static ConcurrentDictionary<string, TrustedIssuer.OnlineCertificateProvider> onlineCertProviders = new ConcurrentDictionary<string, TrustedIssuer.OnlineCertificateProvider>();

		// Token: 0x040007CD RID: 1997
		private readonly Dictionary<string, X509Certificate2> x5tCertMap;

		// Token: 0x040007CE RID: 1998
		private readonly IssuerMetadata metadata;

		// Token: 0x040007CF RID: 1999
		private readonly TrustedIssuer.OnlineCertificateProvider onlineCertificateProvider;

		// Token: 0x040007D0 RID: 2000
		private PartnerApplication application;

		// Token: 0x020000FF RID: 255
		internal abstract class OnlineCertificateProvider
		{
			// Token: 0x0600087F RID: 2175 RVA: 0x00038415 File Offset: 0x00036615
			protected OnlineCertificateProvider()
			{
			}

			// Token: 0x06000880 RID: 2176 RVA: 0x0003841D File Offset: 0x0003661D
			protected OnlineCertificateProvider(string metadataUrl) : this(metadataUrl, TrustedIssuer.OnlineCertificateProvider.DefaultRefreshInterval)
			{
			}

			// Token: 0x06000881 RID: 2177 RVA: 0x0003842B File Offset: 0x0003662B
			protected OnlineCertificateProvider(string metadataUrl, TimeSpan refreshInterval)
			{
				this.metadataUrl = metadataUrl;
				this.refreshInterval = refreshInterval;
				this.signatureValidateFailed = false;
				this.fetchCount = 0;
				this.successfulFetchTimes = new DateTime[TrustedIssuer.OnlineCertificateProvider.MaxSuccessfulFetchCountPerInterval];
				this.oldestSuccessfulFetchTimeIndex = 0;
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000882 RID: 2178 RVA: 0x00038466 File Offset: 0x00036666
			public static TrustedIssuer.OnlineCertificateProvider NullProvider
			{
				get
				{
					return TrustedIssuer.OnlineCertificateProvider.nullProvider;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000883 RID: 2179 RVA: 0x0003846D File Offset: 0x0003666D
			public string MetadataUrl
			{
				get
				{
					return this.metadataUrl;
				}
			}

			// Token: 0x06000884 RID: 2180 RVA: 0x00038478 File Offset: 0x00036678
			public virtual X509Certificate2[] GetCertificates()
			{
				if (this.NeedFetchNow())
				{
					ExTraceGlobals.OAuthTracer.TraceDebug((long)this.GetHashCode(), "[OCP:GetCertificates] start fetching");
					X509Certificate2[] array = this.FetchCertsHelper();
					bool flag = array != null;
					if (flag)
					{
						ExTraceGlobals.OAuthTracer.TraceDebug((long)this.GetHashCode(), "[OCP:GetCertificates] new certificates are fetched");
						this.certificates = array;
					}
					this.PostFetch(flag);
				}
				else
				{
					ExTraceGlobals.OAuthTracer.TraceDebug((long)this.GetHashCode(), "[OCP:GetCertificates] skip fetching and going to reuse the saved certs");
				}
				this.ClearSignatureValidationFlag();
				return this.certificates;
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x000384FD File Offset: 0x000366FD
			internal void ClearSignatureValidationFlag()
			{
				this.signatureValidateFailed = false;
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x00038506 File Offset: 0x00036706
			public virtual void NotifyValidationFailure()
			{
				this.signatureValidateFailed = true;
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x00038510 File Offset: 0x00036710
			internal bool NeedFetchNow()
			{
				DateTime utcNow = DateTime.UtcNow;
				if (this.nextFetchTime < utcNow)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<DateTime, DateTime>((long)this.GetHashCode(), "[OCP:NeedFetchNow] return true since scheduled fetch time was {0} and now is {1}", this.nextFetchTime, utcNow);
					return true;
				}
				DateTime arg = this.successfulFetchTimes[this.oldestSuccessfulFetchTimeIndex];
				ExTraceGlobals.OAuthTracer.TraceDebug<DateTime, int>((long)this.GetHashCode(), "[OCP:NeedFetchNow] the last 3 successful fetching happened at {0}, current retry count: {1}", arg, this.fetchCount);
				if (this.signatureValidateFailed && arg.Add(this.refreshInterval) < utcNow)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug((long)this.GetHashCode(), "[OCP:NeedFetchNow] return true since we were told the signature validation failed, and we did not fetch too many times. last 24 hours");
					return true;
				}
				if (0 < this.fetchCount && this.fetchCount <= 3)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug((long)this.GetHashCode(), "[OCP:NeedFetchNow] return true since we failed to fetch last time");
					return true;
				}
				return false;
			}

			// Token: 0x06000888 RID: 2184 RVA: 0x000385E4 File Offset: 0x000367E4
			internal void PostFetch(bool succeed)
			{
				this.fetchCount++;
				DateTime utcNow = DateTime.UtcNow;
				if (!succeed)
				{
					if (this.fetchCount > 3)
					{
						this.nextFetchTime = utcNow.Add(this.refreshInterval);
						return;
					}
				}
				else
				{
					this.successfulFetchTimes[this.oldestSuccessfulFetchTimeIndex] = utcNow;
					this.oldestSuccessfulFetchTimeIndex = (this.oldestSuccessfulFetchTimeIndex + 1) % TrustedIssuer.OnlineCertificateProvider.MaxSuccessfulFetchCountPerInterval;
					this.nextFetchTime = utcNow.Add(this.refreshInterval);
					this.fetchCount = 0;
				}
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x0003866C File Offset: 0x0003686C
			private X509Certificate2[] FetchCertsHelper()
			{
				AuthMetadataClient authMetadataClient = new AuthMetadataClient(this.metadataUrl, true);
				string content = null;
				try
				{
					content = authMetadataClient.Acquire(false);
				}
				catch (WebException ex)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<WebException>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during acquiring: {0}", ex);
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
					{
						this.metadataUrl,
						ex
					});
					return null;
				}
				IList<X509Certificate2> list = this.ExtractCertificates(content);
				if (list.Count == 0)
				{
					return null;
				}
				return list.ToArray<X509Certificate2>();
			}

			// Token: 0x0600088A RID: 2186
			protected abstract IList<X509Certificate2> ExtractCertificates(string content);

			// Token: 0x0600088B RID: 2187 RVA: 0x0003870C File Offset: 0x0003690C
			public override string ToString()
			{
				return string.Format("NextFetch @ {0}, Retry# {1}, SucceedFetch @{2}", this.nextFetchTime, this.fetchCount, this.successfulFetchTimes[this.oldestSuccessfulFetchTimeIndex]);
			}

			// Token: 0x040007D7 RID: 2007
			private static readonly TimeSpan DefaultRefreshInterval = TimeSpan.FromHours(24.0);

			// Token: 0x040007D8 RID: 2008
			private static readonly TrustedIssuer.OnlineCertificateProvider nullProvider = new TrustedIssuer.NullOnlineCertificateProvider();

			// Token: 0x040007D9 RID: 2009
			private static readonly int MaxSuccessfulFetchCountPerInterval = 3;

			// Token: 0x040007DA RID: 2010
			protected readonly string metadataUrl;

			// Token: 0x040007DB RID: 2011
			private readonly TimeSpan refreshInterval;

			// Token: 0x040007DC RID: 2012
			private X509Certificate2[] certificates;

			// Token: 0x040007DD RID: 2013
			private DateTime nextFetchTime;

			// Token: 0x040007DE RID: 2014
			private DateTime[] successfulFetchTimes;

			// Token: 0x040007DF RID: 2015
			private int oldestSuccessfulFetchTimeIndex;

			// Token: 0x040007E0 RID: 2016
			private int fetchCount;

			// Token: 0x040007E1 RID: 2017
			private bool signatureValidateFailed;
		}

		// Token: 0x02000100 RID: 256
		internal sealed class XmlOnlineCertificateProvider : TrustedIssuer.OnlineCertificateProvider
		{
			// Token: 0x0600088D RID: 2189 RVA: 0x0003876E File Offset: 0x0003696E
			public XmlOnlineCertificateProvider(string metadataUrl) : base(metadataUrl)
			{
			}

			// Token: 0x0600088E RID: 2190 RVA: 0x00038777 File Offset: 0x00036977
			public XmlOnlineCertificateProvider(string metadataUrl, TimeSpan refreshInterval) : base(metadataUrl, refreshInterval)
			{
			}

			// Token: 0x0600088F RID: 2191 RVA: 0x0003878C File Offset: 0x0003698C
			protected override IList<X509Certificate2> ExtractCertificates(string content)
			{
				List<X509Certificate2> list = new List<X509Certificate2>();
				Exception ex = null;
				try
				{
					using (TextReader textReader = new StringReader(content))
					{
						using (XmlReader xmlReader = XmlReader.Create(textReader))
						{
							MetadataSerializer metadataSerializer = new MetadataSerializer
							{
								CertificateValidationMode = X509CertificateValidationMode.None
							};
							EntityDescriptor entityDescriptor = metadataSerializer.ReadMetadata(xmlReader) as EntityDescriptor;
							SecurityTokenServiceDescriptor securityTokenServiceDescriptor = entityDescriptor.RoleDescriptors.OfType<SecurityTokenServiceDescriptor>().First<SecurityTokenServiceDescriptor>();
							foreach (KeyDescriptor keyDescriptor in from k in securityTokenServiceDescriptor.Keys
							where k.Use == KeyType.Signing
							select k)
							{
								foreach (SecurityKeyIdentifierClause securityKeyIdentifierClause in keyDescriptor.KeyInfo)
								{
									if (securityKeyIdentifierClause is X509RawDataKeyIdentifierClause)
									{
										list.Add(new X509Certificate2((securityKeyIdentifierClause as X509RawDataKeyIdentifierClause).GetX509RawData()));
									}
								}
							}
						}
					}
				}
				catch (XmlException ex2)
				{
					ex = ex2;
				}
				catch (IOException ex3)
				{
					ex = ex3;
				}
				catch (SecurityException ex4)
				{
					ex = ex4;
				}
				catch (SystemException ex5)
				{
					ex = ex5;
				}
				if (ex != null)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<Exception>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during converting: {0}", ex);
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
					{
						this.metadataUrl,
						ex
					});
				}
				return list;
			}
		}

		// Token: 0x02000101 RID: 257
		internal sealed class JsonOnlineCertificateProvider : TrustedIssuer.OnlineCertificateProvider
		{
			// Token: 0x06000891 RID: 2193 RVA: 0x0003896C File Offset: 0x00036B6C
			public JsonOnlineCertificateProvider(string metadataUrl) : base(metadataUrl)
			{
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x00038975 File Offset: 0x00036B75
			public JsonOnlineCertificateProvider(string metadataUrl, TimeSpan refreshInterval) : base(metadataUrl, refreshInterval)
			{
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x00038980 File Offset: 0x00036B80
			protected override IList<X509Certificate2> ExtractCertificates(string content)
			{
				List<X509Certificate2> list = new List<X509Certificate2>();
				JsonMetadataDocument jsonMetadataDocument = null;
				try
				{
					jsonMetadataDocument = AuthMetadataParser.GetDocument<JsonMetadataDocument>(content);
				}
				catch (AuthMetadataParserException ex)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<AuthMetadataParserException>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during parsing: {0}", ex);
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
					{
						this.metadataUrl,
						ex
					});
					return list;
				}
				if (jsonMetadataDocument != null && jsonMetadataDocument.keys != null)
				{
					foreach (JsonKey jsonKey in jsonMetadataDocument.keys)
					{
						JsonKeyValue keyvalue = jsonKey.keyvalue;
						if (keyvalue != null)
						{
							string value = keyvalue.value;
							if (!string.IsNullOrEmpty(value))
							{
								try
								{
									byte[] rawData = Convert.FromBase64String(value);
									list.Add(new X509Certificate2(rawData));
								}
								catch (FormatException ex2)
								{
									ExTraceGlobals.OAuthTracer.TraceDebug<FormatException>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during converting: {0}", ex2);
									OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
									{
										this.metadataUrl,
										ex2
									});
								}
								catch (CryptographicException ex3)
								{
									ExTraceGlobals.OAuthTracer.TraceDebug<CryptographicException>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during converting: {0}", ex3);
									OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
									{
										this.metadataUrl,
										ex3
									});
								}
							}
						}
					}
				}
				return list;
			}
		}

		// Token: 0x02000102 RID: 258
		internal sealed class OpenIdConnectCertificateProvider : TrustedIssuer.OnlineCertificateProvider
		{
			// Token: 0x06000894 RID: 2196 RVA: 0x00038B28 File Offset: 0x00036D28
			public OpenIdConnectCertificateProvider(string metadataUrl) : base(metadataUrl)
			{
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x00038B31 File Offset: 0x00036D31
			public OpenIdConnectCertificateProvider(string metadataUrl, TimeSpan refreshInterval) : base(metadataUrl, refreshInterval)
			{
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x00038B3C File Offset: 0x00036D3C
			protected override IList<X509Certificate2> ExtractCertificates(string content)
			{
				List<X509Certificate2> list = new List<X509Certificate2>();
				OpenIdConnectJsonMetadataDocument openIdConnectJsonMetadataDocument = null;
				try
				{
					openIdConnectJsonMetadataDocument = AuthMetadataParser.GetDocument<OpenIdConnectJsonMetadataDocument>(content);
				}
				catch (AuthMetadataParserException ex)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<AuthMetadataParserException>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during parsing: {0}", ex);
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
					{
						this.metadataUrl,
						ex
					});
					return list;
				}
				if (string.IsNullOrEmpty(openIdConnectJsonMetadataDocument.jwks_uri))
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<string>((long)this.GetHashCode(), "[OCP:FetchCerts] Cannot find Open Id Connect Key Url Path in metadata document at: {0}", this.metadataUrl);
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
					{
						this.metadataUrl,
						new AuthMetadataParserException(DirectoryStrings.ErrorAuthMetadataNoSigningKey)
					});
					return list;
				}
				AuthMetadataClient authMetadataClient = new AuthMetadataClient(openIdConnectJsonMetadataDocument.jwks_uri, true);
				string content2 = null;
				try
				{
					content2 = authMetadataClient.Acquire(false);
				}
				catch (WebException ex2)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<WebException>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during acquiring: {0}", ex2);
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
					{
						this.metadataUrl,
						ex2
					});
					return list;
				}
				OpenIdConnectKeysJsonMetadataDocument openIdConnectKeysJsonMetadataDocument = null;
				try
				{
					openIdConnectKeysJsonMetadataDocument = AuthMetadataParser.GetDocument<OpenIdConnectKeysJsonMetadataDocument>(content2);
				}
				catch (AuthMetadataParserException ex3)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<AuthMetadataParserException>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during parsing: {0}", ex3);
					OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
					{
						this.metadataUrl,
						ex3
					});
					return list;
				}
				if (openIdConnectKeysJsonMetadataDocument != null && openIdConnectKeysJsonMetadataDocument.keys != null)
				{
					foreach (OpenIdConnectKey openIdConnectKey in openIdConnectKeysJsonMetadataDocument.keys)
					{
						if (!string.IsNullOrEmpty(openIdConnectKey.use) && openIdConnectKey.use.Equals(AuthMetadataConstants.OpenIdConnectSigningKeyUsage, StringComparison.OrdinalIgnoreCase) && openIdConnectKey.x5c != null && openIdConnectKey.x5c.Length != 0)
						{
							string text = openIdConnectKey.x5c[0];
							if (!string.IsNullOrEmpty(text))
							{
								try
								{
									byte[] rawData = Convert.FromBase64String(text);
									list.Add(new X509Certificate2(rawData));
								}
								catch (FormatException ex4)
								{
									ExTraceGlobals.OAuthTracer.TraceDebug<FormatException>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during converting: {0}", ex4);
									OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
									{
										this.metadataUrl,
										ex4
									});
								}
								catch (CryptographicException ex5)
								{
									ExTraceGlobals.OAuthTracer.TraceDebug<CryptographicException>((long)this.GetHashCode(), "[OCP:FetchCerts] hitting exception during converting: {0}", ex5);
									OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailedWhileReadingMetadata, this.metadataUrl, new object[]
									{
										this.metadataUrl,
										ex5
									});
								}
							}
						}
					}
				}
				return list;
			}
		}

		// Token: 0x02000103 RID: 259
		internal sealed class NullOnlineCertificateProvider : TrustedIssuer.OnlineCertificateProvider
		{
			// Token: 0x06000898 RID: 2200 RVA: 0x00038E70 File Offset: 0x00037070
			public override X509Certificate2[] GetCertificates()
			{
				return null;
			}

			// Token: 0x06000899 RID: 2201 RVA: 0x00038E73 File Offset: 0x00037073
			public override void NotifyValidationFailure()
			{
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x00038E75 File Offset: 0x00037075
			protected override IList<X509Certificate2> ExtractCertificates(string content)
			{
				throw new NotImplementedException();
			}
		}
	}
}
