using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B50 RID: 2896
	internal static class Constants
	{
		// Token: 0x02000B51 RID: 2897
		public static class Soap
		{
			// Token: 0x0400363A RID: 13882
			public const string UpnUri = "http://schemas.xmlsoap.org/claims/UPN";

			// Token: 0x0400363B RID: 13883
			public const string IdentityClaimUri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";

			// Token: 0x0400363C RID: 13884
			public const string GenericClaimUri = "http://schemas.xmlsoap.org/claims";
		}

		// Token: 0x02000B52 RID: 2898
		public static class Saml
		{
			// Token: 0x0400363D RID: 13885
			public const string AuthenticationMethodPasswordUri = "urn:oasis:names:tc:SAML:1.0:am:password";

			// Token: 0x0400363E RID: 13886
			public const string TokenProfile11 = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1";

			// Token: 0x0400363F RID: 13887
			public const string EmailAddressClaimName = "EmailAddress";

			// Token: 0x04003640 RID: 13888
			public const string EmailAddressListClaimName = "EmailAddressList";

			// Token: 0x04003641 RID: 13889
			public const string ConfirmationSenderVouches = "urn:oasis:names:tc:SAML:1.0:cm:sender-vouches";

			// Token: 0x04003642 RID: 13890
			public const string EmailAddress = "http://schemas.xmlsoap.org/claims/EmailAddress";

			// Token: 0x04003643 RID: 13891
			public const string EmailAddressList = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/EmailAddressList";

			// Token: 0x04003644 RID: 13892
			public const string TokenProfileValueType = "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID";
		}

		// Token: 0x02000B53 RID: 2899
		public static class WSAddressing
		{
			// Token: 0x04003645 RID: 13893
			public const string Anonymous = "http://www.w3.org/2005/08/addressing/anonymous";
		}

		// Token: 0x02000B54 RID: 2900
		public static class WSTrust
		{
			// Token: 0x04003646 RID: 13894
			public const string Issue = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";

			// Token: 0x04003647 RID: 13895
			public const string RequestTypeIssue = "http://schemas.xmlsoap.org/ws/2005/02/trust/Issue";

			// Token: 0x04003648 RID: 13896
			public const string BinarySecretTypeNonce = "http://schemas.xmlsoap.org/ws/2005/02/trust/Nonce";

			// Token: 0x04003649 RID: 13897
			public const string SymmetricKey = "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey";

			// Token: 0x0400364A RID: 13898
			public const string PSHA1 = "http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1";
		}

		// Token: 0x02000B55 RID: 2901
		public static class XmlEncryption
		{
			// Token: 0x0400364B RID: 13899
			public const string C14Canonalization = "http://www.w3.org/2001/10/xml-exc-c14n#";

			// Token: 0x0400364C RID: 13900
			public const string AES256_CBC = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";
		}

		// Token: 0x02000B56 RID: 2902
		public static class XmlSignature
		{
			// Token: 0x0400364D RID: 13901
			public const string HMAC_SHA1 = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";
		}

		// Token: 0x02000B57 RID: 2903
		public static class WSAuthorization
		{
			// Token: 0x0400364E RID: 13902
			public const string RequestorScope = "http://schemas.xmlsoap.org/ws/2006/12/authorization/ctx/requestor";

			// Token: 0x0400364F RID: 13903
			public const string Action = "http://schemas.xmlsoap.org/ws/2006/12/authorization/claims/action";

			// Token: 0x04003650 RID: 13904
			public const string Dialect = "http://schemas.xmlsoap.org/ws/2006/12/authorization/authclaims";
		}

		// Token: 0x02000B58 RID: 2904
		public static class MicrosoftWLID
		{
			// Token: 0x04003651 RID: 13905
			public const string RequestorName = "http://schemas.microsoft.com/wlid/requestor";

			// Token: 0x04003652 RID: 13906
			public const string ImmutableIdUri = "http://schemas.microsoft.com/LiveID/Federation/2008/05/ImmutableID";
		}

		// Token: 0x02000B59 RID: 2905
		public static class X509TokenProfile
		{
			// Token: 0x04003653 RID: 13907
			public const string X509v3 = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3";

			// Token: 0x04003654 RID: 13908
			public const string SKI = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier";
		}
	}
}
