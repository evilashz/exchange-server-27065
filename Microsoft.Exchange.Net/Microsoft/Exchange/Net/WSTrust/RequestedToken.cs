using System;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B6C RID: 2924
	internal sealed class RequestedToken
	{
		// Token: 0x06003E8F RID: 16015 RVA: 0x000A3587 File Offset: 0x000A1787
		internal RequestedToken(XmlElement securityToken, XmlElement securityTokenReference, XmlElement requestedUnattachedReference, SymmetricSecurityKey proofToken, Timestamp lifetime)
		{
			this.securityToken = securityToken;
			this.securityTokenReference = securityTokenReference;
			this.requestedUnattachedReference = requestedUnattachedReference;
			this.proofToken = proofToken;
			this.lifetime = lifetime;
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003E90 RID: 16016 RVA: 0x000A35B4 File Offset: 0x000A17B4
		public XmlElement SecurityToken
		{
			get
			{
				return this.securityToken;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x000A35BC File Offset: 0x000A17BC
		public XmlElement SecurityTokenReference
		{
			get
			{
				return this.securityTokenReference;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000A35C4 File Offset: 0x000A17C4
		public XmlElement RequestUnattachedReference
		{
			get
			{
				return this.requestedUnattachedReference;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x000A35CC File Offset: 0x000A17CC
		public SymmetricSecurityKey ProofToken
		{
			get
			{
				return this.proofToken;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x000A35D4 File Offset: 0x000A17D4
		public Timestamp Lifetime
		{
			get
			{
				return this.lifetime;
			}
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x000A35DC File Offset: 0x000A17DC
		public GenericXmlSecurityToken GetSecurityToken()
		{
			return new GenericXmlSecurityToken(this.securityToken, new BinarySecretSecurityToken(this.proofToken.GetSymmetricKey()), this.lifetime.Created, this.lifetime.Expires, new SamlAssertionKeyIdentifierClause(this.securityTokenReference.InnerText), new SamlAssertionKeyIdentifierClause(this.requestedUnattachedReference.InnerText), null);
		}

		// Token: 0x0400367F RID: 13951
		private XmlElement securityToken;

		// Token: 0x04003680 RID: 13952
		private XmlElement securityTokenReference;

		// Token: 0x04003681 RID: 13953
		private XmlElement requestedUnattachedReference;

		// Token: 0x04003682 RID: 13954
		private SymmetricSecurityKey proofToken;

		// Token: 0x04003683 RID: 13955
		private Timestamp lifetime;
	}
}
