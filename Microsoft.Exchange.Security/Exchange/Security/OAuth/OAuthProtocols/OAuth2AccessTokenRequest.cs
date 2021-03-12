using System;
using System.Collections.Specialized;
using System.IdentityModel.Protocols.WSTrust;
using System.IO;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Security.OAuth.OAuthProtocols
{
	// Token: 0x020000E8 RID: 232
	internal class OAuth2AccessTokenRequest : OAuth2Message
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x00035F40 File Offset: 0x00034140
		private static StringCollection GetTokenResponseParameters()
		{
			return new StringCollection
			{
				"access_token",
				"expires_in"
			};
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00035F6C File Offset: 0x0003416C
		public static OAuth2AccessTokenRequest Read(StreamReader reader)
		{
			string requestString = null;
			try
			{
				requestString = reader.ReadToEnd();
			}
			catch (DecoderFallbackException innerException)
			{
				throw new InvalidDataException("Request encoding is not ASCII", innerException);
			}
			return OAuth2AccessTokenRequest.Read(requestString);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00035FA8 File Offset: 0x000341A8
		public static OAuth2AccessTokenRequest Read(string requestString)
		{
			OAuth2AccessTokenRequest oauth2AccessTokenRequest = new OAuth2AccessTokenRequest();
			try
			{
				oauth2AccessTokenRequest.Decode(requestString);
			}
			catch (InvalidRequestException)
			{
				NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(requestString);
				if (string.IsNullOrEmpty(nameValueCollection["client_id"]) && string.IsNullOrEmpty(nameValueCollection["assertion"]))
				{
					throw new InvalidDataException("The request body must contain a client_id or assertion parameter.");
				}
				throw;
			}
			foreach (string value in oauth2AccessTokenRequest.Keys)
			{
				if (OAuth2AccessTokenRequest.TokenResponseParameters.Contains(value))
				{
					throw new InvalidDataException();
				}
			}
			return oauth2AccessTokenRequest;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00036058 File Offset: 0x00034258
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0003606A File Offset: 0x0003426A
		public string RefreshToken
		{
			get
			{
				return base.Message["refresh_token"];
			}
			set
			{
				base.Message["refresh_token"] = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0003607D File Offset: 0x0003427D
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x0003608F File Offset: 0x0003428F
		public string Resource
		{
			get
			{
				return base.Message["resource"];
			}
			set
			{
				base.Message["resource"] = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000360A2 File Offset: 0x000342A2
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x000360B4 File Offset: 0x000342B4
		public string Scope
		{
			get
			{
				return base.Message["scope"];
			}
			set
			{
				base.Message["scope"] = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x000360C7 File Offset: 0x000342C7
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x000360D4 File Offset: 0x000342D4
		public string AppContext
		{
			get
			{
				return base["AppContext"];
			}
			set
			{
				base["AppContext"] = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x000360E2 File Offset: 0x000342E2
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x000360EF File Offset: 0x000342EF
		public string Assertion
		{
			get
			{
				return base["assertion"];
			}
			set
			{
				base["assertion"] = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000360FD File Offset: 0x000342FD
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0003610A File Offset: 0x0003430A
		public string GrantType
		{
			get
			{
				return base["grant_type"];
			}
			set
			{
				base["grant_type"] = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00036118 File Offset: 0x00034318
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00036125 File Offset: 0x00034325
		public string ClientId
		{
			get
			{
				return base["client_id"];
			}
			set
			{
				base["client_id"] = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00036133 File Offset: 0x00034333
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00036140 File Offset: 0x00034340
		public string ClientSecret
		{
			get
			{
				return base["client_secret"];
			}
			set
			{
				base["client_secret"] = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0003614E File Offset: 0x0003434E
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0003615B File Offset: 0x0003435B
		public string Code
		{
			get
			{
				return base["code"];
			}
			set
			{
				base["code"] = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00036169 File Offset: 0x00034369
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00036176 File Offset: 0x00034376
		public string Realm
		{
			get
			{
				return base["realm"];
			}
			set
			{
				base["realm"] = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00036184 File Offset: 0x00034384
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00036191 File Offset: 0x00034391
		public string Username
		{
			get
			{
				return base["username"];
			}
			set
			{
				base["username"] = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0003619F File Offset: 0x0003439F
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x000361AC File Offset: 0x000343AC
		public string Password
		{
			get
			{
				return base["password"];
			}
			set
			{
				base["password"] = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000361BA File Offset: 0x000343BA
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x000361C7 File Offset: 0x000343C7
		public string RedirectUri
		{
			get
			{
				return base["redirect_uri"];
			}
			set
			{
				base["redirect_uri"] = value;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000361D5 File Offset: 0x000343D5
		public void SetCustomProperty(string propertyName, string propertyValue)
		{
			base[propertyName] = propertyValue;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000361DF File Offset: 0x000343DF
		public virtual void Write(StreamWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(base.Encode());
		}

		// Token: 0x0400074B RID: 1867
		public static StringCollection TokenResponseParameters = OAuth2AccessTokenRequest.GetTokenResponseParameters();
	}
}
