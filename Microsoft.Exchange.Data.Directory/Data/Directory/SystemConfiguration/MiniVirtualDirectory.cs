using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004EB RID: 1259
	[Serializable]
	public class MiniVirtualDirectory : MiniObject
	{
		// Token: 0x0600384D RID: 14413 RVA: 0x000DA068 File Offset: 0x000D8268
		static MiniVirtualDirectory()
		{
			ADVirtualDirectory advirtualDirectory = new ADVirtualDirectory();
			MiniVirtualDirectory.implicitFilter = advirtualDirectory.ImplicitFilter;
			MiniVirtualDirectory.mostDerivedClass = advirtualDirectory.MostDerivedObjectClass;
			MiniVirtualDirectory.schema = ObjectSchema.GetInstance<MiniVirtualDirectorySchema>();
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000DA0A3 File Offset: 0x000D82A3
		public bool IsAvailabilityForeignConnector
		{
			get
			{
				return base.ObjectClass.Contains(ADAvailabilityForeignConnectorVirtualDirectory.MostDerivedClass);
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000DA0B5 File Offset: 0x000D82B5
		public bool IsE12UM
		{
			get
			{
				return base.ObjectClass.Contains(ADE12UMVirtualDirectory.MostDerivedClass);
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000DA0C7 File Offset: 0x000D82C7
		public bool IsEcp
		{
			get
			{
				return base.ObjectClass.Contains(ADEcpVirtualDirectory.MostDerivedClass);
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x000DA0D9 File Offset: 0x000D82D9
		public bool IsMapi
		{
			get
			{
				return base.ObjectClass.Contains(ADMapiVirtualDirectory.MostDerivedClass);
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x000DA0EB File Offset: 0x000D82EB
		public bool IsMobile
		{
			get
			{
				return base.ObjectClass.Contains("msExchMobileVirtualDirectory");
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000DA0FD File Offset: 0x000D82FD
		public bool IsOab
		{
			get
			{
				return base.ObjectClass.Contains(ADOabVirtualDirectory.MostDerivedClass);
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x000DA10F File Offset: 0x000D830F
		public bool IsOwa
		{
			get
			{
				return base.ObjectClass.Contains(ADOwaVirtualDirectory.MostDerivedClass);
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000DA121 File Offset: 0x000D8321
		public bool IsRpcHttp
		{
			get
			{
				return base.ObjectClass.Contains(ADRpcHttpVirtualDirectory.MostDerivedClass);
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06003857 RID: 14423 RVA: 0x000DA133 File Offset: 0x000D8333
		public bool IsWebServices
		{
			get
			{
				return base.ObjectClass.Contains(ADWebServicesVirtualDirectory.MostDerivedClass);
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000DA145 File Offset: 0x000D8345
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[ADVirtualDirectorySchema.Server];
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x000DA157 File Offset: 0x000D8357
		public Uri InternalUrl
		{
			get
			{
				return (Uri)this[ADVirtualDirectorySchema.InternalUrl];
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000DA169 File Offset: 0x000D8369
		public MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)this[ADVirtualDirectorySchema.InternalAuthenticationMethods];
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x000DA17B File Offset: 0x000D837B
		public Uri ExternalUrl
		{
			get
			{
				return (Uri)this[ADVirtualDirectorySchema.ExternalUrl];
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000DA18D File Offset: 0x000D838D
		public MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)this[ADVirtualDirectorySchema.ExternalAuthenticationMethods];
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x000DA19F File Offset: 0x000D839F
		public string MetabasePath
		{
			get
			{
				return (string)this[ExchangeVirtualDirectorySchema.MetabasePath];
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000DA1B1 File Offset: 0x000D83B1
		public bool LiveIdAuthentication
		{
			get
			{
				return (bool)this[ExchangeWebAppVirtualDirectorySchema.LiveIdAuthentication];
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000DA1C3 File Offset: 0x000D83C3
		public string AvailabilityForeignConnectorType
		{
			get
			{
				return (string)this[ADAvailabilityForeignConnectorVirtualDirectorySchema.AvailabilityForeignConnectorType];
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000DA1D5 File Offset: 0x000D83D5
		public MultiValuedProperty<string> AvailabilityForeignConnectorDomains
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADAvailabilityForeignConnectorVirtualDirectorySchema.AvailabilityForeignConnectorDomains];
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000DA1E7 File Offset: 0x000D83E7
		public bool AdminEnabled
		{
			get
			{
				return (bool)this[ADEcpVirtualDirectorySchema.AdminEnabled];
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000DA1F9 File Offset: 0x000D83F9
		public bool OwaOptionsEnabled
		{
			get
			{
				return (bool)this[ADEcpVirtualDirectorySchema.OwaOptionsEnabled];
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x000DA20B File Offset: 0x000D840B
		public bool MobileClientCertificateProvisioningEnabled
		{
			get
			{
				return (bool)this[ADMobileVirtualDirectorySchema.MobileClientCertificateProvisioningEnabled];
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06003864 RID: 14436 RVA: 0x000DA21D File Offset: 0x000D841D
		public string MobileClientCertificateAuthorityURL
		{
			get
			{
				return (string)this[ADMobileVirtualDirectorySchema.MobileClientCertificateAuthorityURL];
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x000DA22F File Offset: 0x000D842F
		public string MobileClientCertTemplateName
		{
			get
			{
				return (string)this[ADMobileVirtualDirectorySchema.MobileClientCertTemplateName];
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000DA241 File Offset: 0x000D8441
		public MultiValuedProperty<ADObjectId> OfflineAddressBooks
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADOabVirtualDirectorySchema.OfflineAddressBooks];
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06003867 RID: 14439 RVA: 0x000DA254 File Offset: 0x000D8454
		public OwaVersions OwaVersion
		{
			get
			{
				OwaVersions owaVersions = (OwaVersions)this[ADOwaVirtualDirectorySchema.OwaVersion];
				if (base.ExchangeVersion.ExchangeBuild.Major < 14 && owaVersions >= OwaVersions.Exchange2007)
				{
					return OwaVersions.Exchange2007;
				}
				return owaVersions;
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x000DA290 File Offset: 0x000D8490
		public bool? AnonymousFeaturesEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.AnonymousFeaturesEnabled]);
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06003869 RID: 14441 RVA: 0x000DA2C4 File Offset: 0x000D84C4
		public Uri FailbackUrl
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return (Uri)this[ADOwaVirtualDirectorySchema.FailbackUrl];
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000DA2E0 File Offset: 0x000D84E0
		public bool? IntegratedFeaturesEnabled
		{
			get
			{
				if (!this.IsExchange2009OrLater)
				{
					return null;
				}
				return new bool?((bool)this[ADOwaVirtualDirectorySchema.IntegratedFeaturesEnabled]);
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x000DA314 File Offset: 0x000D8514
		public AuthenticationMethod ExternalClientAuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod)this[ADRpcHttpVirtualDirectorySchema.ExternalClientAuthenticationMethod];
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x000DA328 File Offset: 0x000D8528
		public AuthenticationMethod InternalClientAuthenticationMethod
		{
			get
			{
				int num = (int)this[ADEcpVirtualDirectorySchema.ADFeatureSet];
				if (num == -1)
				{
					return AuthenticationMethod.Ntlm;
				}
				return (AuthenticationMethod)num;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x000DA34D File Offset: 0x000D854D
		public MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)this[ADRpcHttpVirtualDirectorySchema.IISAuthenticationMethods];
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x0600386E RID: 14446 RVA: 0x000DA360 File Offset: 0x000D8560
		public Uri XropUrl
		{
			get
			{
				MultiValuedProperty<Uri> multiValuedProperty = (MultiValuedProperty<Uri>)this[ADRpcHttpVirtualDirectorySchema.XropUrl];
				if (multiValuedProperty != null && multiValuedProperty.Count != 0)
				{
					return multiValuedProperty[0];
				}
				return null;
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000DA392 File Offset: 0x000D8592
		public Uri InternalNLBBypassUrl
		{
			get
			{
				return (Uri)this[ADWebServicesVirtualDirectorySchema.InternalNLBBypassUrl];
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x000DA3A4 File Offset: 0x000D85A4
		public bool MRSProxyEnabled
		{
			get
			{
				return (bool)this[ADWebServicesVirtualDirectorySchema.MRSProxyEnabled];
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000DA3B6 File Offset: 0x000D85B6
		internal override ADObjectSchema Schema
		{
			get
			{
				return MiniVirtualDirectory.schema;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x000DA3BD File Offset: 0x000D85BD
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MiniVirtualDirectory.mostDerivedClass;
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000DA3C4 File Offset: 0x000D85C4
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return MiniVirtualDirectory.implicitFilter;
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000DA3CB File Offset: 0x000D85CB
		internal bool IsExchange2009OrLater
		{
			get
			{
				return this.OwaVersion >= OwaVersions.Exchange2010;
			}
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000DA3D9 File Offset: 0x000D85D9
		private int ADFeatureSet
		{
			get
			{
				return (int)this[ADEcpVirtualDirectorySchema.ADFeatureSet];
			}
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000DA3EC File Offset: 0x000D85EC
		internal static MiniVirtualDirectory CreateFrom(ADObject virtualDirectory, ICollection<PropertyDefinition> propertyDefinitions, object[] propertyValues)
		{
			MiniVirtualDirectory miniVirtualDirectory = new MiniVirtualDirectory();
			IEnumerable<PropertyDefinition> allProperties = miniVirtualDirectory.Schema.AllProperties;
			ADPropertyBag adpropertyBag = new ADPropertyBag();
			adpropertyBag.SetIsReadOnly(false);
			foreach (PropertyDefinition propertyDefinition in allProperties)
			{
				ADPropertyDefinition key = (ADPropertyDefinition)propertyDefinition;
				object value = virtualDirectory.propertyBag.Contains(key) ? virtualDirectory.propertyBag[key] : null;
				adpropertyBag.SetField(key, value);
			}
			MultiValuedProperty<string> multiValuedProperty = adpropertyBag[ADObjectSchema.ObjectClass] as MultiValuedProperty<string>;
			if (multiValuedProperty == null || multiValuedProperty.Count == 0)
			{
				multiValuedProperty = new MultiValuedProperty<string>(virtualDirectory.MostDerivedObjectClass);
				adpropertyBag.SetField(ADObjectSchema.ObjectClass, multiValuedProperty);
			}
			if (adpropertyBag[ADObjectSchema.WhenChangedUTC] == null)
			{
				DateTime utcNow = DateTime.UtcNow;
				adpropertyBag.SetField(ADObjectSchema.WhenChangedUTC, utcNow);
				adpropertyBag.SetField(ADObjectSchema.WhenCreatedUTC, utcNow);
			}
			if (propertyDefinitions != null && propertyValues != null)
			{
				adpropertyBag.SetProperties(propertyDefinitions, propertyValues);
			}
			adpropertyBag.SetIsReadOnly(true);
			miniVirtualDirectory.propertyBag = adpropertyBag;
			return miniVirtualDirectory;
		}

		// Token: 0x04002614 RID: 9748
		private static QueryFilter implicitFilter;

		// Token: 0x04002615 RID: 9749
		private static ADObjectSchema schema;

		// Token: 0x04002616 RID: 9750
		private static string mostDerivedClass;
	}
}
