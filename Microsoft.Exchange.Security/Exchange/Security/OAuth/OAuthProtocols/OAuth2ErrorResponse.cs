using System;

namespace Microsoft.Exchange.Security.OAuth.OAuthProtocols
{
	// Token: 0x020000EA RID: 234
	internal class OAuth2ErrorResponse : OAuth2Message
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x000362F4 File Offset: 0x000344F4
		private OAuth2ErrorResponse()
		{
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000362FC File Offset: 0x000344FC
		public OAuth2ErrorResponse(string error)
		{
			this.Error = error;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0003630C File Offset: 0x0003450C
		public static OAuth2ErrorResponse CreateFromEncodedResponse(string responseString)
		{
			OAuth2ErrorResponse oauth2ErrorResponse = new OAuth2ErrorResponse();
			oauth2ErrorResponse.DecodeFromJson(responseString);
			if (string.IsNullOrEmpty(oauth2ErrorResponse.Error))
			{
				throw new ArgumentException("Error property is null or empty. This message is not a valid OAuth2 error response.", "responseString");
			}
			return oauth2ErrorResponse;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00036344 File Offset: 0x00034544
		public override string ToString()
		{
			return base.EncodeToJson();
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0003634C File Offset: 0x0003454C
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0003635E File Offset: 0x0003455E
		public string Error
		{
			get
			{
				return base.Message["error"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Error property cannot be null or empty.", "value");
				}
				base.Message["error"] = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00036389 File Offset: 0x00034589
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0003639B File Offset: 0x0003459B
		public string ErrorDescription
		{
			get
			{
				return base.Message["error_description"];
			}
			set
			{
				base.Message["error_description"] = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000363AE File Offset: 0x000345AE
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x000363C0 File Offset: 0x000345C0
		public string ErrorUri
		{
			get
			{
				return base.Message["error_uri"];
			}
			set
			{
				base.Message["error_uri"] = value;
			}
		}
	}
}
