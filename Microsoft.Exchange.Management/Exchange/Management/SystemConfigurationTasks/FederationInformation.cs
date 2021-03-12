using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009DB RID: 2523
	[Serializable]
	public sealed class FederationInformation : ConfigurableObject
	{
		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x06005A43 RID: 23107 RVA: 0x00179EB3 File Offset: 0x001780B3
		[Parameter]
		public Uri TargetApplicationUri
		{
			get
			{
				return (Uri)this.propertyBag[FederationInformationSchema.TargetApplicationUri];
			}
		}

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x06005A44 RID: 23108 RVA: 0x00179ECA File Offset: 0x001780CA
		[Parameter]
		public MultiValuedProperty<SmtpDomain> DomainNames
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this.propertyBag[FederationInformationSchema.DomainNames];
			}
		}

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x06005A45 RID: 23109 RVA: 0x00179EE1 File Offset: 0x001780E1
		[Parameter]
		public Uri TargetAutodiscoverEpr
		{
			get
			{
				return (Uri)this.propertyBag[FederationInformationSchema.TargetAutodiscoverEpr];
			}
		}

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x06005A46 RID: 23110 RVA: 0x00179EF8 File Offset: 0x001780F8
		[Parameter]
		public MultiValuedProperty<Uri> TokenIssuerUris
		{
			get
			{
				return (MultiValuedProperty<Uri>)this.propertyBag[FederationInformationSchema.TokenIssuerUris];
			}
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x06005A47 RID: 23111 RVA: 0x00179F0F File Offset: 0x0017810F
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x00179F18 File Offset: 0x00178118
		internal FederationInformation(SmtpDomain identity, Uri targetApplicationUri, ICollection tokenIssuers, ICollection domainNames, Uri targetAutodiscoverEpr) : base(new FederationInformationPropertyBag())
		{
			this.propertyBag[FederationInformationSchema.Identity] = identity;
			this.propertyBag[FederationInformationSchema.TargetApplicationUri] = targetApplicationUri;
			this.propertyBag[FederationInformationSchema.DomainNames] = new MultiValuedProperty<SmtpDomain>(true, null, domainNames);
			this.propertyBag[FederationInformationSchema.TargetAutodiscoverEpr] = targetAutodiscoverEpr;
			if (tokenIssuers != null)
			{
				this.propertyBag[FederationInformationSchema.TokenIssuerUris] = new MultiValuedProperty<Uri>(true, null, tokenIssuers);
			}
			base.ResetChangeTracking(true);
		}

		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x06005A49 RID: 23113 RVA: 0x00179F9F File Offset: 0x0017819F
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return FederationInformation.schema;
			}
		}

		// Token: 0x040033B7 RID: 13239
		private static ObjectSchema schema = new FederationInformationSchema();
	}
}
