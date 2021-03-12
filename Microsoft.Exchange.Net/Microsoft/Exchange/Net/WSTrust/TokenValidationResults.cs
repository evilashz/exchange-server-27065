using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B73 RID: 2931
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TokenValidationResults
	{
		// Token: 0x06003EE3 RID: 16099 RVA: 0x000A529B File Offset: 0x000A349B
		private TokenValidationResults(TokenValidationResult result)
		{
			if (result == TokenValidationResult.Valid)
			{
				throw new ArgumentException("result");
			}
			this.result = result;
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x000A52B8 File Offset: 0x000A34B8
		public TokenValidationResults(string externalId, string emailAddress, Offer offer, SecurityToken securityToken, SymmetricSecurityKey proofToken, List<string> emailAddresses)
		{
			this.result = TokenValidationResult.Valid;
			this.externalId = externalId;
			this.emailAddress = emailAddress;
			this.offer = offer;
			this.securityToken = securityToken;
			this.proofToken = proofToken;
			this.emailAddresses = emailAddresses;
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x000A52F4 File Offset: 0x000A34F4
		public TokenValidationResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x000A52FC File Offset: 0x000A34FC
		public string ExternalId
		{
			get
			{
				return this.externalId;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x000A5304 File Offset: 0x000A3504
		public string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x000A530C File Offset: 0x000A350C
		public Offer Offer
		{
			get
			{
				return this.offer;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x000A5314 File Offset: 0x000A3514
		public SecurityToken SecurityToken
		{
			get
			{
				return this.securityToken;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06003EEA RID: 16106 RVA: 0x000A531C File Offset: 0x000A351C
		public SymmetricSecurityKey ProofToken
		{
			get
			{
				return this.proofToken;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06003EEB RID: 16107 RVA: 0x000A5324 File Offset: 0x000A3524
		public List<string> EmailAddresses
		{
			get
			{
				return this.emailAddresses;
			}
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x000A532C File Offset: 0x000A352C
		public override string ToString()
		{
			if (this.result == TokenValidationResult.Valid)
			{
				return string.Concat(new object[]
				{
					"TokenValidationResult(ExternalId=",
					this.externalId.ToString(),
					",EmailAddress=",
					this.emailAddress.ToString(),
					",Offer=",
					this.offer.ToString(),
					",Number of EmailAddresses=",
					this.emailAddresses.Count,
					")"
				});
			}
			return "TokenValidationResult(Result=" + this.result + ")";
		}

		// Token: 0x0400369E RID: 13982
		public static readonly TokenValidationResults InvalidUnknownExternalIdentity = new TokenValidationResults(TokenValidationResult.InvalidUnknownExternalIdentity);

		// Token: 0x0400369F RID: 13983
		public static readonly TokenValidationResults InvalidUnknownEncryption = new TokenValidationResults(TokenValidationResult.InvalidUnknownEncryption);

		// Token: 0x040036A0 RID: 13984
		public static readonly TokenValidationResults InvalidTokenFailedValidation = new TokenValidationResults(TokenValidationResult.InvalidTokenFailedValidation);

		// Token: 0x040036A1 RID: 13985
		public static readonly TokenValidationResults InvalidTokenFormat = new TokenValidationResults(TokenValidationResult.InvalidTokenFormat);

		// Token: 0x040036A2 RID: 13986
		public static readonly TokenValidationResults InvalidTrustBroker = new TokenValidationResults(TokenValidationResult.InvalidTrustBroker);

		// Token: 0x040036A3 RID: 13987
		public static readonly TokenValidationResults InvalidTarget = new TokenValidationResults(TokenValidationResult.InvalidTarget);

		// Token: 0x040036A4 RID: 13988
		public static readonly TokenValidationResults InvalidOffer = new TokenValidationResults(TokenValidationResult.InvalidOffer);

		// Token: 0x040036A5 RID: 13989
		public static readonly TokenValidationResults InvalidUnknownEmailAddress = new TokenValidationResults(TokenValidationResult.InvalidUnknownEmailAddress);

		// Token: 0x040036A6 RID: 13990
		public static readonly TokenValidationResults InvalidExpired = new TokenValidationResults(TokenValidationResult.InvalidExpired);

		// Token: 0x040036A7 RID: 13991
		private TokenValidationResult result;

		// Token: 0x040036A8 RID: 13992
		private string externalId;

		// Token: 0x040036A9 RID: 13993
		private string emailAddress;

		// Token: 0x040036AA RID: 13994
		private Offer offer;

		// Token: 0x040036AB RID: 13995
		private SecurityToken securityToken;

		// Token: 0x040036AC RID: 13996
		private SymmetricSecurityKey proofToken;

		// Token: 0x040036AD RID: 13997
		private List<string> emailAddresses;
	}
}
