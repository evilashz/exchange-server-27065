using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using Microsoft.Exchange.Common.Net.Cryptography;

namespace Microsoft.Exchange.Clients.Common.FBL
{
	// Token: 0x02000032 RID: 50
	internal class AuthkeyAuthenticationRequest
	{
		// Token: 0x06000186 RID: 390 RVA: 0x0000AD84 File Offset: 0x00008F84
		public static NameValueCollection DecryptSignedUrl(NameValueCollection encryptedQueryString)
		{
			AuthkeyAuthenticationRequest.SignedUrl signedUrl = new AuthkeyAuthenticationRequest.SignedUrl(encryptedQueryString);
			string query = signedUrl.ValidateHashAndDecryptPayload();
			return HttpUtility.ParseQueryString(query);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000ADA7 File Offset: 0x00008FA7
		public static string UrlDecodeBase64String(string urlEncodedBase64String)
		{
			if (string.IsNullOrEmpty(urlEncodedBase64String))
			{
				return string.Empty;
			}
			return HttpUtility.UrlDecode(urlEncodedBase64String).Replace('_', '/').Replace('-', '+').Replace('~', '=');
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public static string UrlEncodeBase64String(string base64String)
		{
			return base64String.Replace('+', '-').Replace('/', '_').Replace('=', '~');
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public static string ConstructQueryString(NameValueCollection paramCollection)
		{
			string result = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in paramCollection)
			{
				string text = (string)obj;
				stringBuilder.Append(XssEncode.UrlEncode(text) + "=" + XssEncode.UrlEncode(paramCollection[text]) + "&");
			}
			if (stringBuilder.Length > 0)
			{
				result = stringBuilder.ToString(0, stringBuilder.Length - 1);
			}
			return result;
		}

		// Token: 0x02000033 RID: 51
		public class SignedUrl
		{
			// Token: 0x0600018B RID: 395 RVA: 0x0000AEA0 File Offset: 0x000090A0
			public SignedUrl(CryptoKeyPayloadType payloadType, string plaintextPayload)
			{
				this.PayloadType = payloadType;
				this.UtcTimestamp = DateTime.UtcNow;
				this.Payload = CryptoUtils.Encrypt(Encoding.UTF8.GetBytes(plaintextPayload), payloadType, out this.keyVersion, out this.InitializationVector);
				string[] hashComponents = new string[]
				{
					this.ToQueryStringFragment(null, false)
				};
				this.MessageAuthenticationCode = HashUtility.ComputeHash(hashComponents, this.HashTypeForPayload);
			}

			// Token: 0x0600018C RID: 396 RVA: 0x0000AF18 File Offset: 0x00009118
			public SignedUrl(NameValueCollection queryString)
			{
				this.KeyVersion = byte.Parse(queryString["kv"], NumberFormatInfo.InvariantInfo);
				if (this.KeyVersion > 1)
				{
					this.PayloadType = (CryptoKeyPayloadType)int.Parse(queryString["pt"], NumberFormatInfo.InvariantInfo);
				}
				else
				{
					AuthkeyAuthenticationRequest.SignedUrl.LegacyPayloadType legacyPayloadType = (AuthkeyAuthenticationRequest.SignedUrl.LegacyPayloadType)int.Parse(queryString["pt"], NumberFormatInfo.InvariantInfo);
					AuthkeyAuthenticationRequest.SignedUrl.LegacyPayloadType legacyPayloadType2 = legacyPayloadType;
					if (legacyPayloadType2 != AuthkeyAuthenticationRequest.SignedUrl.LegacyPayloadType.SvmFeedback)
					{
						throw new SecurityException("failed to parse query string collection");
					}
					this.PayloadType = CryptoKeyPayloadType.SvmFeedbackEncryption;
				}
				this.UtcTimestamp = DateTime.FromFileTimeUtc(long.Parse(queryString["ts"], NumberFormatInfo.InvariantInfo));
				this.InitializationVector = Convert.FromBase64String(AuthkeyAuthenticationRequest.UrlDecodeBase64String(queryString["iv"]));
				this.Payload = Convert.FromBase64String(AuthkeyAuthenticationRequest.UrlDecodeBase64String(queryString["authKey"]));
				this.MessageAuthenticationCode = Convert.FromBase64String(AuthkeyAuthenticationRequest.UrlDecodeBase64String(queryString["hmac"]));
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600018D RID: 397 RVA: 0x0000B014 File Offset: 0x00009214
			// (set) Token: 0x0600018E RID: 398 RVA: 0x0000B01C File Offset: 0x0000921C
			public byte KeyVersion
			{
				get
				{
					return this.keyVersion;
				}
				set
				{
					this.keyVersion = value;
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x0600018F RID: 399 RVA: 0x0000B025 File Offset: 0x00009225
			// (set) Token: 0x06000190 RID: 400 RVA: 0x0000B02D File Offset: 0x0000922D
			public byte[] MessageAuthenticationCode { get; set; }

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000191 RID: 401 RVA: 0x0000B038 File Offset: 0x00009238
			private CryptoKeyPayloadType HashTypeForPayload
			{
				get
				{
					CryptoKeyPayloadType payloadType = this.PayloadType;
					if (payloadType == CryptoKeyPayloadType.SvmFeedbackEncryption)
					{
						return CryptoKeyPayloadType.SvmFeedbackHash;
					}
					throw new InvalidOperationException("Didn't find matching hash algorithm for PayloadType: " + Enum.GetName(typeof(CryptoKeyPayloadType), this.PayloadType));
				}
			}

			// Token: 0x06000192 RID: 402 RVA: 0x0000B07C File Offset: 0x0000927C
			public string ToQueryStringFragment(NameValueCollection additionalParameters, bool includeHMAC = true)
			{
				NameValueCollection nameValueCollection = new NameValueCollection();
				if (this.KeyVersion > 1)
				{
					nameValueCollection.Add("pt", ((int)this.PayloadType).ToString(NumberFormatInfo.InvariantInfo));
				}
				else
				{
					CryptoKeyPayloadType payloadType = this.PayloadType;
					if (payloadType != CryptoKeyPayloadType.SvmFeedbackEncryption)
					{
						throw new InvalidOperationException(string.Format("Key of type {0} is not supported by legacy payloads, Key version must be greater than 1", Enum.GetName(typeof(CryptoKeyPayloadType), this.PayloadType)));
					}
					AuthkeyAuthenticationRequest.SignedUrl.LegacyPayloadType legacyPayloadType = AuthkeyAuthenticationRequest.SignedUrl.LegacyPayloadType.SvmFeedback;
					NameValueCollection nameValueCollection2 = nameValueCollection;
					string name = "pt";
					int num = (int)legacyPayloadType;
					nameValueCollection2.Add(name, num.ToString(NumberFormatInfo.InvariantInfo));
				}
				nameValueCollection.Add("kv", this.KeyVersion.ToString(NumberFormatInfo.InvariantInfo));
				nameValueCollection.Add("ts", this.UtcTimestamp.ToFileTimeUtc().ToString(NumberFormatInfo.InvariantInfo));
				nameValueCollection.Add("iv", AuthkeyAuthenticationRequest.UrlEncodeBase64String(Convert.ToBase64String(this.InitializationVector)));
				nameValueCollection.Add("authKey", AuthkeyAuthenticationRequest.UrlEncodeBase64String(Convert.ToBase64String(this.Payload)));
				if (includeHMAC && this.MessageAuthenticationCode != null)
				{
					nameValueCollection.Add("hmac", AuthkeyAuthenticationRequest.UrlEncodeBase64String(Convert.ToBase64String(this.MessageAuthenticationCode)));
				}
				if (additionalParameters != null)
				{
					nameValueCollection.Add(additionalParameters);
				}
				return AuthkeyAuthenticationRequest.ConstructQueryString(nameValueCollection);
			}

			// Token: 0x06000193 RID: 403 RVA: 0x0000B1D0 File Offset: 0x000093D0
			public string ValidateHashAndDecryptPayload()
			{
				string[] hashComponents = new string[]
				{
					this.ToQueryStringFragment(null, false)
				};
				byte[] array = HashUtility.ComputeHash(hashComponents, this.HashTypeForPayload);
				if (array.Length == this.MessageAuthenticationCode.Length)
				{
					if (!array.Where((byte t, int i) => t != this.MessageAuthenticationCode[i]).Any<byte>())
					{
						return Encoding.UTF8.GetString(CryptoUtils.Decrypt(this.Payload, this.PayloadType, this.KeyVersion, this.InitializationVector));
					}
				}
				throw new SecurityException("failed to validate hash and decrypt payload");
			}

			// Token: 0x040002EB RID: 747
			public readonly byte SerializationVersion = 1;

			// Token: 0x040002EC RID: 748
			public readonly CryptoKeyPayloadType PayloadType;

			// Token: 0x040002ED RID: 749
			public readonly DateTime UtcTimestamp;

			// Token: 0x040002EE RID: 750
			public readonly byte[] InitializationVector;

			// Token: 0x040002EF RID: 751
			public readonly byte[] Payload;

			// Token: 0x040002F0 RID: 752
			private byte keyVersion;

			// Token: 0x02000034 RID: 52
			public enum LegacyPayloadType
			{
				// Token: 0x040002F3 RID: 755
				SvmFeedback
			}
		}
	}
}
