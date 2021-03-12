using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200010E RID: 270
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ClientExtensionType
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x000213BB File Offset: 0x0001F5BB
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x000213C3 File Offset: 0x0001F5C3
		[XmlArrayItem("String", IsNullable = false)]
		public string[] SpecificUsers
		{
			get
			{
				return this.specificUsersField;
			}
			set
			{
				this.specificUsersField = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x000213CC File Offset: 0x0001F5CC
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x000213D4 File Offset: 0x0001F5D4
		[XmlElement(DataType = "base64Binary")]
		public byte[] Manifest
		{
			get
			{
				return this.manifestField;
			}
			set
			{
				this.manifestField = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000213DD File Offset: 0x0001F5DD
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x000213E5 File Offset: 0x0001F5E5
		[XmlAttribute]
		public bool IsAvailable
		{
			get
			{
				return this.isAvailableField;
			}
			set
			{
				this.isAvailableField = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x000213EE File Offset: 0x0001F5EE
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x000213F6 File Offset: 0x0001F5F6
		[XmlIgnore]
		public bool IsAvailableSpecified
		{
			get
			{
				return this.isAvailableFieldSpecified;
			}
			set
			{
				this.isAvailableFieldSpecified = value;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000213FF File Offset: 0x0001F5FF
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x00021407 File Offset: 0x0001F607
		[XmlAttribute]
		public bool IsMandatory
		{
			get
			{
				return this.isMandatoryField;
			}
			set
			{
				this.isMandatoryField = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00021410 File Offset: 0x0001F610
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x00021418 File Offset: 0x0001F618
		[XmlIgnore]
		public bool IsMandatorySpecified
		{
			get
			{
				return this.isMandatoryFieldSpecified;
			}
			set
			{
				this.isMandatoryFieldSpecified = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00021421 File Offset: 0x0001F621
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x00021429 File Offset: 0x0001F629
		[XmlAttribute]
		public bool IsEnabledByDefault
		{
			get
			{
				return this.isEnabledByDefaultField;
			}
			set
			{
				this.isEnabledByDefaultField = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00021432 File Offset: 0x0001F632
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x0002143A File Offset: 0x0001F63A
		[XmlIgnore]
		public bool IsEnabledByDefaultSpecified
		{
			get
			{
				return this.isEnabledByDefaultFieldSpecified;
			}
			set
			{
				this.isEnabledByDefaultFieldSpecified = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00021443 File Offset: 0x0001F643
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x0002144B File Offset: 0x0001F64B
		[XmlAttribute]
		public ClientExtensionProvidedToType ProvidedTo
		{
			get
			{
				return this.providedToField;
			}
			set
			{
				this.providedToField = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00021454 File Offset: 0x0001F654
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x0002145C File Offset: 0x0001F65C
		[XmlIgnore]
		public bool ProvidedToSpecified
		{
			get
			{
				return this.providedToFieldSpecified;
			}
			set
			{
				this.providedToFieldSpecified = value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00021465 File Offset: 0x0001F665
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0002146D File Offset: 0x0001F66D
		[XmlAttribute]
		public ClientExtensionTypeType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00021476 File Offset: 0x0001F676
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0002147E File Offset: 0x0001F67E
		[XmlIgnore]
		public bool TypeSpecified
		{
			get
			{
				return this.typeFieldSpecified;
			}
			set
			{
				this.typeFieldSpecified = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00021487 File Offset: 0x0001F687
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0002148F File Offset: 0x0001F68F
		[XmlAttribute]
		public ClientExtensionScopeType Scope
		{
			get
			{
				return this.scopeField;
			}
			set
			{
				this.scopeField = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00021498 File Offset: 0x0001F698
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x000214A0 File Offset: 0x0001F6A0
		[XmlIgnore]
		public bool ScopeSpecified
		{
			get
			{
				return this.scopeFieldSpecified;
			}
			set
			{
				this.scopeFieldSpecified = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x000214A9 File Offset: 0x0001F6A9
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x000214B1 File Offset: 0x0001F6B1
		[XmlAttribute]
		public string MarketplaceAssetId
		{
			get
			{
				return this.marketplaceAssetIdField;
			}
			set
			{
				this.marketplaceAssetIdField = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x000214BA File Offset: 0x0001F6BA
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x000214C2 File Offset: 0x0001F6C2
		[XmlAttribute]
		public string MarketplaceContentMarket
		{
			get
			{
				return this.marketplaceContentMarketField;
			}
			set
			{
				this.marketplaceContentMarketField = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x000214CB File Offset: 0x0001F6CB
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x000214D3 File Offset: 0x0001F6D3
		[XmlAttribute]
		public string AppStatus
		{
			get
			{
				return this.appStatusField;
			}
			set
			{
				this.appStatusField = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000214DC File Offset: 0x0001F6DC
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x000214E4 File Offset: 0x0001F6E4
		[XmlAttribute]
		public string Etoken
		{
			get
			{
				return this.etokenField;
			}
			set
			{
				this.etokenField = value;
			}
		}

		// Token: 0x04000872 RID: 2162
		private string[] specificUsersField;

		// Token: 0x04000873 RID: 2163
		private byte[] manifestField;

		// Token: 0x04000874 RID: 2164
		private bool isAvailableField;

		// Token: 0x04000875 RID: 2165
		private bool isAvailableFieldSpecified;

		// Token: 0x04000876 RID: 2166
		private bool isMandatoryField;

		// Token: 0x04000877 RID: 2167
		private bool isMandatoryFieldSpecified;

		// Token: 0x04000878 RID: 2168
		private bool isEnabledByDefaultField;

		// Token: 0x04000879 RID: 2169
		private bool isEnabledByDefaultFieldSpecified;

		// Token: 0x0400087A RID: 2170
		private ClientExtensionProvidedToType providedToField;

		// Token: 0x0400087B RID: 2171
		private bool providedToFieldSpecified;

		// Token: 0x0400087C RID: 2172
		private ClientExtensionTypeType typeField;

		// Token: 0x0400087D RID: 2173
		private bool typeFieldSpecified;

		// Token: 0x0400087E RID: 2174
		private ClientExtensionScopeType scopeField;

		// Token: 0x0400087F RID: 2175
		private bool scopeFieldSpecified;

		// Token: 0x04000880 RID: 2176
		private string marketplaceAssetIdField;

		// Token: 0x04000881 RID: 2177
		private string marketplaceContentMarketField;

		// Token: 0x04000882 RID: 2178
		private string appStatusField;

		// Token: 0x04000883 RID: 2179
		private string etokenField;
	}
}
