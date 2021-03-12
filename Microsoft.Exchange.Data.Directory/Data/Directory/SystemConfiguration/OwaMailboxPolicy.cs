using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000537 RID: 1335
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class OwaMailboxPolicy : MailboxPolicy
	{
		// Token: 0x06003B56 RID: 15190 RVA: 0x000E5057 File Offset: 0x000E3257
		internal static QueryFilter IsDefaultFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(MailboxPolicySchema.MailboxPolicyFlags, 1UL));
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x000E506B File Offset: 0x000E326B
		internal override ADObjectSchema Schema
		{
			get
			{
				return OwaMailboxPolicy.schema;
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x000E5072 File Offset: 0x000E3272
		internal override string MostDerivedObjectClass
		{
			get
			{
				return OwaMailboxPolicy.mostDerivedClass;
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x000E5079 File Offset: 0x000E3279
		internal override ADObjectId ParentPath
		{
			get
			{
				return OwaMailboxPolicy.parentPath;
			}
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x000E5080 File Offset: 0x000E3280
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x000E5087 File Offset: 0x000E3287
		// (set) Token: 0x06003B5C RID: 15196 RVA: 0x000E5099 File Offset: 0x000E3299
		[Parameter(Mandatory = false)]
		public bool DirectFileAccessOnPublicComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.DirectFileAccessOnPublicComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.DirectFileAccessOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000E50AC File Offset: 0x000E32AC
		// (set) Token: 0x06003B5E RID: 15198 RVA: 0x000E50BE File Offset: 0x000E32BE
		[Parameter(Mandatory = false)]
		public bool DirectFileAccessOnPrivateComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.DirectFileAccessOnPrivateComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.DirectFileAccessOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x000E50D1 File Offset: 0x000E32D1
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x000E50E3 File Offset: 0x000E32E3
		[Parameter(Mandatory = false)]
		public bool WebReadyDocumentViewingOnPublicComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WebReadyDocumentViewingOnPublicComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.WebReadyDocumentViewingOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x000E50F6 File Offset: 0x000E32F6
		// (set) Token: 0x06003B62 RID: 15202 RVA: 0x000E5108 File Offset: 0x000E3308
		[Parameter(Mandatory = false)]
		public bool WebReadyDocumentViewingOnPrivateComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WebReadyDocumentViewingOnPrivateComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.WebReadyDocumentViewingOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x000E511B File Offset: 0x000E331B
		// (set) Token: 0x06003B64 RID: 15204 RVA: 0x000E512D File Offset: 0x000E332D
		[Parameter(Mandatory = false)]
		public bool ForceWebReadyDocumentViewingFirstOnPublicComputers
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ForceWebReadyDocumentViewingFirstOnPublicComputers];
			}
			set
			{
				this[OwaMailboxPolicySchema.ForceWebReadyDocumentViewingFirstOnPublicComputers] = value;
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x000E5140 File Offset: 0x000E3340
		// (set) Token: 0x06003B66 RID: 15206 RVA: 0x000E5152 File Offset: 0x000E3352
		[Parameter(Mandatory = false)]
		public bool ForceWebReadyDocumentViewingFirstOnPrivateComputers
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ForceWebReadyDocumentViewingFirstOnPrivateComputers];
			}
			set
			{
				this[OwaMailboxPolicySchema.ForceWebReadyDocumentViewingFirstOnPrivateComputers] = value;
			}
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x000E5165 File Offset: 0x000E3365
		// (set) Token: 0x06003B68 RID: 15208 RVA: 0x000E5177 File Offset: 0x000E3377
		[Parameter(Mandatory = false)]
		public bool WacViewingOnPublicComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WacViewingOnPublicComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.WacViewingOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x000E518A File Offset: 0x000E338A
		// (set) Token: 0x06003B6A RID: 15210 RVA: 0x000E519C File Offset: 0x000E339C
		[Parameter(Mandatory = false)]
		public bool WacViewingOnPrivateComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WacViewingOnPrivateComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.WacViewingOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x000E51AF File Offset: 0x000E33AF
		// (set) Token: 0x06003B6C RID: 15212 RVA: 0x000E51C1 File Offset: 0x000E33C1
		[Parameter(Mandatory = false)]
		public bool ForceWacViewingFirstOnPublicComputers
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ForceWacViewingFirstOnPublicComputers];
			}
			set
			{
				this[OwaMailboxPolicySchema.ForceWacViewingFirstOnPublicComputers] = value;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x000E51D4 File Offset: 0x000E33D4
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x000E51E6 File Offset: 0x000E33E6
		[Parameter(Mandatory = false)]
		public bool ForceWacViewingFirstOnPrivateComputers
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ForceWacViewingFirstOnPrivateComputers];
			}
			set
			{
				this[OwaMailboxPolicySchema.ForceWacViewingFirstOnPrivateComputers] = value;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x000E51F9 File Offset: 0x000E33F9
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x000E520B File Offset: 0x000E340B
		[Parameter(Mandatory = false)]
		public AttachmentBlockingActions ActionForUnknownFileAndMIMETypes
		{
			get
			{
				return (AttachmentBlockingActions)this[OwaMailboxPolicySchema.ActionForUnknownFileAndMIMETypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.ActionForUnknownFileAndMIMETypes] = value;
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x000E521E File Offset: 0x000E341E
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x000E5230 File Offset: 0x000E3430
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> WebReadyFileTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OwaMailboxPolicySchema.WebReadyFileTypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.WebReadyFileTypes] = value;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000E523E File Offset: 0x000E343E
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x000E5250 File Offset: 0x000E3450
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> WebReadyMimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OwaMailboxPolicySchema.WebReadyMimeTypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.WebReadyMimeTypes] = value;
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000E525E File Offset: 0x000E345E
		// (set) Token: 0x06003B76 RID: 15222 RVA: 0x000E5270 File Offset: 0x000E3470
		[Parameter(Mandatory = false)]
		public bool WebReadyDocumentViewingForAllSupportedTypes
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WebReadyDocumentViewingForAllSupportedTypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.WebReadyDocumentViewingForAllSupportedTypes] = value;
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x000E5283 File Offset: 0x000E3483
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> WebReadyDocumentViewingSupportedMimeTypes
		{
			get
			{
				return OwaMailboxPolicy.webReadyDocumentViewingSupportedMimeTypes;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000E528A File Offset: 0x000E348A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> WebReadyDocumentViewingSupportedFileTypes
		{
			get
			{
				return OwaMailboxPolicy.webReadyDocumentViewingSupportedFileTypes;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000E5291 File Offset: 0x000E3491
		// (set) Token: 0x06003B7A RID: 15226 RVA: 0x000E52A3 File Offset: 0x000E34A3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AllowedFileTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OwaMailboxPolicySchema.AllowedFileTypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.AllowedFileTypes] = value;
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000E52B1 File Offset: 0x000E34B1
		// (set) Token: 0x06003B7C RID: 15228 RVA: 0x000E52C3 File Offset: 0x000E34C3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AllowedMimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OwaMailboxPolicySchema.AllowedMimeTypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.AllowedMimeTypes] = value;
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x000E52D1 File Offset: 0x000E34D1
		// (set) Token: 0x06003B7E RID: 15230 RVA: 0x000E52E3 File Offset: 0x000E34E3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ForceSaveFileTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OwaMailboxPolicySchema.ForceSaveFileTypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.ForceSaveFileTypes] = value;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x000E52F1 File Offset: 0x000E34F1
		// (set) Token: 0x06003B80 RID: 15232 RVA: 0x000E5303 File Offset: 0x000E3503
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ForceSaveMimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OwaMailboxPolicySchema.ForceSaveMimeTypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.ForceSaveMimeTypes] = value;
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x000E5311 File Offset: 0x000E3511
		// (set) Token: 0x06003B82 RID: 15234 RVA: 0x000E5323 File Offset: 0x000E3523
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> BlockedFileTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OwaMailboxPolicySchema.BlockedFileTypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.BlockedFileTypes] = value;
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x000E5331 File Offset: 0x000E3531
		// (set) Token: 0x06003B84 RID: 15236 RVA: 0x000E5343 File Offset: 0x000E3543
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> BlockedMimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OwaMailboxPolicySchema.BlockedMimeTypes];
			}
			set
			{
				this[OwaMailboxPolicySchema.BlockedMimeTypes] = value;
			}
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000E5351 File Offset: 0x000E3551
		internal static object WebReadyFileTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[OwaMailboxPolicySchema.ADWebReadyFileTypes], OwaMailboxPolicySchema.ADWebReadyFileTypes);
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000E536D File Offset: 0x000E356D
		internal static void WebReadyFileTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OwaMailboxPolicySchema.ADWebReadyFileTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, OwaMailboxPolicySchema.ADWebReadyFileTypes, propertyBag);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000E538B File Offset: 0x000E358B
		internal static object WebReadyMimeTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[OwaMailboxPolicySchema.ADWebReadyMimeTypes], OwaMailboxPolicySchema.ADWebReadyMimeTypes);
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000E53A7 File Offset: 0x000E35A7
		internal static void WebReadyMimeTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OwaMailboxPolicySchema.ADWebReadyMimeTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, OwaMailboxPolicySchema.ADWebReadyMimeTypes, propertyBag);
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000E53C5 File Offset: 0x000E35C5
		internal static object AllowedFileTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[OwaMailboxPolicySchema.ADAllowedFileTypes], OwaMailboxPolicySchema.ADAllowedFileTypes);
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000E53E1 File Offset: 0x000E35E1
		internal static void AllowedFileTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OwaMailboxPolicySchema.ADAllowedFileTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, OwaMailboxPolicySchema.ADAllowedFileTypes, propertyBag);
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000E53FF File Offset: 0x000E35FF
		internal static object AllowedMimeTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[OwaMailboxPolicySchema.ADAllowedMimeTypes], OwaMailboxPolicySchema.ADAllowedMimeTypes);
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000E541B File Offset: 0x000E361B
		internal static void AllowedMimeTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OwaMailboxPolicySchema.ADAllowedMimeTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, OwaMailboxPolicySchema.ADAllowedMimeTypes, propertyBag);
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000E5439 File Offset: 0x000E3639
		internal static object ForceSaveFileTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[OwaMailboxPolicySchema.ADForceSaveFileTypes], OwaMailboxPolicySchema.ADForceSaveFileTypes);
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000E5455 File Offset: 0x000E3655
		internal static void ForceSaveFileTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OwaMailboxPolicySchema.ADForceSaveFileTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, OwaMailboxPolicySchema.ADForceSaveFileTypes, propertyBag);
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000E5473 File Offset: 0x000E3673
		internal static object ForceSaveMimeTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[OwaMailboxPolicySchema.ADForceSaveMimeTypes], OwaMailboxPolicySchema.ADForceSaveMimeTypes);
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000E548F File Offset: 0x000E368F
		internal static void ForceSaveMimeTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OwaMailboxPolicySchema.ADForceSaveMimeTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, OwaMailboxPolicySchema.ADForceSaveMimeTypes, propertyBag);
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000E54AD File Offset: 0x000E36AD
		internal static object BlockedFileTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[OwaMailboxPolicySchema.ADBlockedFileTypes], OwaMailboxPolicySchema.ADBlockedFileTypes);
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000E54C9 File Offset: 0x000E36C9
		internal static void BlockedFileTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OwaMailboxPolicySchema.ADBlockedFileTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, OwaMailboxPolicySchema.ADBlockedFileTypes, propertyBag);
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000E54E7 File Offset: 0x000E36E7
		internal static object BlockedMimeTypesGetter(IPropertyBag propertyBag)
		{
			return ExchangeVirtualDirectory.RemoveDNStringSyntax((MultiValuedProperty<string>)propertyBag[OwaMailboxPolicySchema.ADBlockedMimeTypes], OwaMailboxPolicySchema.ADBlockedMimeTypes);
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000E5503 File Offset: 0x000E3703
		internal static void BlockedMimeTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OwaMailboxPolicySchema.ADBlockedMimeTypes] = ExchangeVirtualDirectory.AddDNStringSyntax((MultiValuedProperty<string>)value, OwaMailboxPolicySchema.ADBlockedMimeTypes, propertyBag);
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x000E5524 File Offset: 0x000E3724
		internal override bool CheckForAssociatedUsers()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.OrganizationId, null, false);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 2539, "CheckForAssociatedUsers", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OwaMailboxPolicy.cs");
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.OwaMailboxPolicy, base.Id);
			ADUser[] array = tenantOrRootOrgRecipientSession.FindADUser(null, QueryScope.SubTree, filter, null, 1);
			return array != null && array.Length > 0;
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x000E5589 File Offset: 0x000E3789
		// (set) Token: 0x06003B97 RID: 15255 RVA: 0x000E559B File Offset: 0x000E379B
		[Parameter(Mandatory = false)]
		public bool PhoneticSupportEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.PhoneticSupportEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.PhoneticSupportEnabled] = value;
			}
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x000E55AE File Offset: 0x000E37AE
		// (set) Token: 0x06003B99 RID: 15257 RVA: 0x000E55C0 File Offset: 0x000E37C0
		[Parameter(Mandatory = false)]
		public string DefaultTheme
		{
			get
			{
				return (string)this[OwaMailboxPolicySchema.DefaultTheme];
			}
			set
			{
				this[OwaMailboxPolicySchema.DefaultTheme] = value;
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x000E55CE File Offset: 0x000E37CE
		// (set) Token: 0x06003B9B RID: 15259 RVA: 0x000E55E0 File Offset: 0x000E37E0
		public override bool IsDefault
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.IsDefault];
			}
			set
			{
				this[OwaMailboxPolicySchema.IsDefault] = value;
			}
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x000E55F3 File Offset: 0x000E37F3
		// (set) Token: 0x06003B9D RID: 15261 RVA: 0x000E5605 File Offset: 0x000E3805
		[Parameter(Mandatory = false)]
		public int DefaultClientLanguage
		{
			get
			{
				return (int)this[OwaMailboxPolicySchema.DefaultClientLanguage];
			}
			set
			{
				this[OwaMailboxPolicySchema.DefaultClientLanguage] = value;
			}
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x000E5618 File Offset: 0x000E3818
		// (set) Token: 0x06003B9F RID: 15263 RVA: 0x000E562A File Offset: 0x000E382A
		[Parameter(Mandatory = false)]
		public int LogonAndErrorLanguage
		{
			get
			{
				return (int)this[OwaMailboxPolicySchema.LogonAndErrorLanguage];
			}
			set
			{
				this[OwaMailboxPolicySchema.LogonAndErrorLanguage] = value;
			}
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000E563D File Offset: 0x000E383D
		// (set) Token: 0x06003BA1 RID: 15265 RVA: 0x000E5652 File Offset: 0x000E3852
		[Parameter(Mandatory = false)]
		public bool UseGB18030
		{
			get
			{
				return (int)this[OwaMailboxPolicySchema.UseGB18030] == 1;
			}
			set
			{
				this[OwaMailboxPolicySchema.UseGB18030] = (value ? 1 : 0);
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000E566B File Offset: 0x000E386B
		// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x000E5680 File Offset: 0x000E3880
		[Parameter(Mandatory = false)]
		public bool UseISO885915
		{
			get
			{
				return (int)this[OwaMailboxPolicySchema.UseISO885915] == 1;
			}
			set
			{
				this[OwaMailboxPolicySchema.UseISO885915] = (value ? 1 : 0);
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000E5699 File Offset: 0x000E3899
		// (set) Token: 0x06003BA5 RID: 15269 RVA: 0x000E56AB File Offset: 0x000E38AB
		[Parameter(Mandatory = false)]
		public OutboundCharsetOptions OutboundCharset
		{
			get
			{
				return (OutboundCharsetOptions)this[OwaMailboxPolicySchema.OutboundCharset];
			}
			set
			{
				this[OwaMailboxPolicySchema.OutboundCharset] = value;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000E56BE File Offset: 0x000E38BE
		// (set) Token: 0x06003BA7 RID: 15271 RVA: 0x000E56D0 File Offset: 0x000E38D0
		[Parameter(Mandatory = false)]
		public bool GlobalAddressListEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.GlobalAddressListEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.GlobalAddressListEnabled] = value;
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000E56E3 File Offset: 0x000E38E3
		// (set) Token: 0x06003BA9 RID: 15273 RVA: 0x000E56F5 File Offset: 0x000E38F5
		[Parameter(Mandatory = false)]
		public bool OrganizationEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.OrganizationEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.OrganizationEnabled] = value;
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x000E5708 File Offset: 0x000E3908
		// (set) Token: 0x06003BAB RID: 15275 RVA: 0x000E571A File Offset: 0x000E391A
		[Parameter(Mandatory = false)]
		public bool ExplicitLogonEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ExplicitLogonEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.ExplicitLogonEnabled] = value;
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x000E572D File Offset: 0x000E392D
		// (set) Token: 0x06003BAD RID: 15277 RVA: 0x000E573F File Offset: 0x000E393F
		[Parameter(Mandatory = false)]
		public bool OWALightEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.OWALightEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.OWALightEnabled] = value;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06003BAE RID: 15278 RVA: 0x000E5752 File Offset: 0x000E3952
		// (set) Token: 0x06003BAF RID: 15279 RVA: 0x000E5764 File Offset: 0x000E3964
		[Parameter(Mandatory = false)]
		public bool DelegateAccessEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.DelegateAccessEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.DelegateAccessEnabled] = value;
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x000E5777 File Offset: 0x000E3977
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x000E5789 File Offset: 0x000E3989
		[Parameter(Mandatory = false)]
		public bool IRMEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.IRMEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.IRMEnabled] = value;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x000E579C File Offset: 0x000E399C
		// (set) Token: 0x06003BB3 RID: 15283 RVA: 0x000E57AE File Offset: 0x000E39AE
		[Parameter(Mandatory = false)]
		public bool CalendarEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.CalendarEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.CalendarEnabled] = value;
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x000E57C1 File Offset: 0x000E39C1
		// (set) Token: 0x06003BB5 RID: 15285 RVA: 0x000E57D3 File Offset: 0x000E39D3
		[Parameter(Mandatory = false)]
		public bool ContactsEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ContactsEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.ContactsEnabled] = value;
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000E57E6 File Offset: 0x000E39E6
		// (set) Token: 0x06003BB7 RID: 15287 RVA: 0x000E57F8 File Offset: 0x000E39F8
		[Parameter(Mandatory = false)]
		public bool TasksEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.TasksEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.TasksEnabled] = value;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000E580B File Offset: 0x000E3A0B
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x000E581D File Offset: 0x000E3A1D
		[Parameter(Mandatory = false)]
		public bool JournalEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.JournalEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.JournalEnabled] = value;
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000E5830 File Offset: 0x000E3A30
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x000E5842 File Offset: 0x000E3A42
		[Parameter(Mandatory = false)]
		public bool NotesEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.NotesEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.NotesEnabled] = value;
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000E5855 File Offset: 0x000E3A55
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x000E5867 File Offset: 0x000E3A67
		[Parameter(Mandatory = false)]
		public bool RemindersAndNotificationsEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.RemindersAndNotificationsEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.RemindersAndNotificationsEnabled] = value;
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x000E587A File Offset: 0x000E3A7A
		// (set) Token: 0x06003BBF RID: 15295 RVA: 0x000E588C File Offset: 0x000E3A8C
		[Parameter(Mandatory = false)]
		public bool PremiumClientEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.PremiumClientEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.PremiumClientEnabled] = value;
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x000E589F File Offset: 0x000E3A9F
		// (set) Token: 0x06003BC1 RID: 15297 RVA: 0x000E58B1 File Offset: 0x000E3AB1
		[Parameter(Mandatory = false)]
		public bool SpellCheckerEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.SpellCheckerEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.SpellCheckerEnabled] = value;
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x000E58C4 File Offset: 0x000E3AC4
		// (set) Token: 0x06003BC3 RID: 15299 RVA: 0x000E58D6 File Offset: 0x000E3AD6
		[Parameter(Mandatory = false)]
		public bool SearchFoldersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.SearchFoldersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.SearchFoldersEnabled] = value;
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06003BC4 RID: 15300 RVA: 0x000E58E9 File Offset: 0x000E3AE9
		// (set) Token: 0x06003BC5 RID: 15301 RVA: 0x000E58FB File Offset: 0x000E3AFB
		[Parameter(Mandatory = false)]
		public bool SignaturesEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.SignaturesEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.SignaturesEnabled] = value;
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06003BC6 RID: 15302 RVA: 0x000E590E File Offset: 0x000E3B0E
		// (set) Token: 0x06003BC7 RID: 15303 RVA: 0x000E5920 File Offset: 0x000E3B20
		[Parameter(Mandatory = false)]
		public bool ThemeSelectionEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ThemeSelectionEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.ThemeSelectionEnabled] = value;
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x000E5933 File Offset: 0x000E3B33
		// (set) Token: 0x06003BC9 RID: 15305 RVA: 0x000E5945 File Offset: 0x000E3B45
		[Parameter(Mandatory = false)]
		public bool JunkEmailEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.JunkEmailEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.JunkEmailEnabled] = value;
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x000E5958 File Offset: 0x000E3B58
		// (set) Token: 0x06003BCB RID: 15307 RVA: 0x000E596A File Offset: 0x000E3B6A
		[Parameter(Mandatory = false)]
		public bool UMIntegrationEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.UMIntegrationEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.UMIntegrationEnabled] = value;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x000E597D File Offset: 0x000E3B7D
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x000E598F File Offset: 0x000E3B8F
		[Parameter(Mandatory = false)]
		public bool WSSAccessOnPublicComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WSSAccessOnPublicComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.WSSAccessOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x000E59A2 File Offset: 0x000E3BA2
		// (set) Token: 0x06003BCF RID: 15311 RVA: 0x000E59B4 File Offset: 0x000E3BB4
		[Parameter(Mandatory = false)]
		public bool WSSAccessOnPrivateComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WSSAccessOnPrivateComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.WSSAccessOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x000E59C7 File Offset: 0x000E3BC7
		// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x000E59D9 File Offset: 0x000E3BD9
		[Parameter(Mandatory = false)]
		public bool ChangePasswordEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ChangePasswordEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.ChangePasswordEnabled] = value;
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06003BD2 RID: 15314 RVA: 0x000E59EC File Offset: 0x000E3BEC
		// (set) Token: 0x06003BD3 RID: 15315 RVA: 0x000E59FE File Offset: 0x000E3BFE
		[Parameter(Mandatory = false)]
		public bool UNCAccessOnPublicComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.UNCAccessOnPublicComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.UNCAccessOnPublicComputersEnabled] = value;
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06003BD4 RID: 15316 RVA: 0x000E5A11 File Offset: 0x000E3C11
		// (set) Token: 0x06003BD5 RID: 15317 RVA: 0x000E5A23 File Offset: 0x000E3C23
		[Parameter(Mandatory = false)]
		public bool UNCAccessOnPrivateComputersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.UNCAccessOnPrivateComputersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.UNCAccessOnPrivateComputersEnabled] = value;
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x000E5A36 File Offset: 0x000E3C36
		// (set) Token: 0x06003BD7 RID: 15319 RVA: 0x000E5A48 File Offset: 0x000E3C48
		[Parameter(Mandatory = false)]
		public bool ActiveSyncIntegrationEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ActiveSyncIntegrationEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.ActiveSyncIntegrationEnabled] = value;
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x000E5A5B File Offset: 0x000E3C5B
		// (set) Token: 0x06003BD9 RID: 15321 RVA: 0x000E5A6D File Offset: 0x000E3C6D
		[Parameter(Mandatory = false)]
		public bool AllAddressListsEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.AllAddressListsEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.AllAddressListsEnabled] = value;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06003BDA RID: 15322 RVA: 0x000E5A80 File Offset: 0x000E3C80
		// (set) Token: 0x06003BDB RID: 15323 RVA: 0x000E5A92 File Offset: 0x000E3C92
		[Parameter(Mandatory = false)]
		public bool RulesEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.RulesEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.RulesEnabled] = value;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06003BDC RID: 15324 RVA: 0x000E5AA5 File Offset: 0x000E3CA5
		// (set) Token: 0x06003BDD RID: 15325 RVA: 0x000E5AB7 File Offset: 0x000E3CB7
		[Parameter(Mandatory = false)]
		public bool PublicFoldersEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.PublicFoldersEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.PublicFoldersEnabled] = value;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x000E5ACA File Offset: 0x000E3CCA
		// (set) Token: 0x06003BDF RID: 15327 RVA: 0x000E5ADC File Offset: 0x000E3CDC
		[Parameter(Mandatory = false)]
		public bool SMimeEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.SMimeEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.SMimeEnabled] = value;
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x000E5AEF File Offset: 0x000E3CEF
		// (set) Token: 0x06003BE1 RID: 15329 RVA: 0x000E5B01 File Offset: 0x000E3D01
		[Parameter(Mandatory = false)]
		public bool RecoverDeletedItemsEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.RecoverDeletedItemsEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.RecoverDeletedItemsEnabled] = value;
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x000E5B14 File Offset: 0x000E3D14
		// (set) Token: 0x06003BE3 RID: 15331 RVA: 0x000E5B26 File Offset: 0x000E3D26
		[Parameter(Mandatory = false)]
		public bool InstantMessagingEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.InstantMessagingEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.InstantMessagingEnabled] = value;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x000E5B39 File Offset: 0x000E3D39
		// (set) Token: 0x06003BE5 RID: 15333 RVA: 0x000E5B4B File Offset: 0x000E3D4B
		[Parameter(Mandatory = false)]
		public bool TextMessagingEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.TextMessagingEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.TextMessagingEnabled] = value;
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06003BE6 RID: 15334 RVA: 0x000E5B5E File Offset: 0x000E3D5E
		// (set) Token: 0x06003BE7 RID: 15335 RVA: 0x000E5B70 File Offset: 0x000E3D70
		[Parameter(Mandatory = false)]
		public bool ForceSaveAttachmentFilteringEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ForceSaveAttachmentFilteringEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.ForceSaveAttachmentFilteringEnabled] = value;
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06003BE8 RID: 15336 RVA: 0x000E5B83 File Offset: 0x000E3D83
		// (set) Token: 0x06003BE9 RID: 15337 RVA: 0x000E5B95 File Offset: 0x000E3D95
		[Parameter(Mandatory = false)]
		public bool SilverlightEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.SilverlightEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.SilverlightEnabled] = value;
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000E5BA8 File Offset: 0x000E3DA8
		// (set) Token: 0x06003BEB RID: 15339 RVA: 0x000E5BBF File Offset: 0x000E3DBF
		[Parameter(Mandatory = false)]
		public InstantMessagingTypeOptions? InstantMessagingType
		{
			get
			{
				return new InstantMessagingTypeOptions?((InstantMessagingTypeOptions)this[OwaMailboxPolicySchema.InstantMessagingType]);
			}
			set
			{
				this[OwaMailboxPolicySchema.InstantMessagingType] = value;
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x000E5BD2 File Offset: 0x000E3DD2
		// (set) Token: 0x06003BED RID: 15341 RVA: 0x000E5BE4 File Offset: 0x000E3DE4
		[Parameter(Mandatory = false)]
		public bool DisplayPhotosEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.DisplayPhotosEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.DisplayPhotosEnabled] = value;
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000E5BF7 File Offset: 0x000E3DF7
		// (set) Token: 0x06003BEF RID: 15343 RVA: 0x000E5C09 File Offset: 0x000E3E09
		[Parameter(Mandatory = false)]
		public bool SetPhotoEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.SetPhotoEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.SetPhotoEnabled] = value;
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000E5C1C File Offset: 0x000E3E1C
		// (set) Token: 0x06003BF1 RID: 15345 RVA: 0x000E5C2E File Offset: 0x000E3E2E
		[Parameter(Mandatory = false)]
		public AllowOfflineOnEnum AllowOfflineOn
		{
			get
			{
				return (AllowOfflineOnEnum)this[OwaMailboxPolicySchema.AllowOfflineOn];
			}
			set
			{
				this[OwaMailboxPolicySchema.AllowOfflineOn] = value;
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000E5C41 File Offset: 0x000E3E41
		// (set) Token: 0x06003BF3 RID: 15347 RVA: 0x000E5C53 File Offset: 0x000E3E53
		[Parameter(Mandatory = false)]
		public string SetPhotoURL
		{
			get
			{
				return (string)this[OwaMailboxPolicySchema.SetPhotoURL];
			}
			set
			{
				this[OwaMailboxPolicySchema.SetPhotoURL] = value;
			}
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x000E5C61 File Offset: 0x000E3E61
		// (set) Token: 0x06003BF5 RID: 15349 RVA: 0x000E5C73 File Offset: 0x000E3E73
		[Parameter(Mandatory = false)]
		public bool PlacesEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.PlacesEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.PlacesEnabled] = value;
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000E5C86 File Offset: 0x000E3E86
		// (set) Token: 0x06003BF7 RID: 15351 RVA: 0x000E5C98 File Offset: 0x000E3E98
		[Parameter(Mandatory = false)]
		public bool WeatherEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WeatherEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.WeatherEnabled] = value;
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x000E5CAB File Offset: 0x000E3EAB
		// (set) Token: 0x06003BF9 RID: 15353 RVA: 0x000E5CBD File Offset: 0x000E3EBD
		[Parameter(Mandatory = false)]
		public bool AllowCopyContactsToDeviceAddressBook
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.AllowCopyContactsToDeviceAddressBook];
			}
			set
			{
				this[OwaMailboxPolicySchema.AllowCopyContactsToDeviceAddressBook] = value;
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x000E5CD0 File Offset: 0x000E3ED0
		// (set) Token: 0x06003BFB RID: 15355 RVA: 0x000E5CE2 File Offset: 0x000E3EE2
		[Parameter(Mandatory = false)]
		public bool PredictedActionsEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.PredictedActionsEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.PredictedActionsEnabled] = value;
			}
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06003BFC RID: 15356 RVA: 0x000E5CF5 File Offset: 0x000E3EF5
		// (set) Token: 0x06003BFD RID: 15357 RVA: 0x000E5D07 File Offset: 0x000E3F07
		[Parameter(Mandatory = false)]
		public bool UserDiagnosticEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.UserDiagnosticEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.UserDiagnosticEnabled] = value;
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06003BFE RID: 15358 RVA: 0x000E5D1A File Offset: 0x000E3F1A
		// (set) Token: 0x06003BFF RID: 15359 RVA: 0x000E5D2C File Offset: 0x000E3F2C
		[Parameter(Mandatory = false)]
		public bool FacebookEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.FacebookEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.FacebookEnabled] = value;
			}
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x000E5D3F File Offset: 0x000E3F3F
		// (set) Token: 0x06003C01 RID: 15361 RVA: 0x000E5D51 File Offset: 0x000E3F51
		[Parameter(Mandatory = false)]
		public bool LinkedInEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.LinkedInEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.LinkedInEnabled] = value;
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06003C02 RID: 15362 RVA: 0x000E5D64 File Offset: 0x000E3F64
		// (set) Token: 0x06003C03 RID: 15363 RVA: 0x000E5D76 File Offset: 0x000E3F76
		[Parameter(Mandatory = false)]
		public bool WacExternalServicesEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WacExternalServicesEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.WacExternalServicesEnabled] = value;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06003C04 RID: 15364 RVA: 0x000E5D89 File Offset: 0x000E3F89
		// (set) Token: 0x06003C05 RID: 15365 RVA: 0x000E5D9B File Offset: 0x000E3F9B
		[Parameter(Mandatory = false)]
		public bool WacOMEXEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.WacOMEXEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.WacOMEXEnabled] = value;
			}
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06003C06 RID: 15366 RVA: 0x000E5DAE File Offset: 0x000E3FAE
		// (set) Token: 0x06003C07 RID: 15367 RVA: 0x000E5DC0 File Offset: 0x000E3FC0
		[Parameter(Mandatory = false)]
		public bool ReportJunkEmailEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.ReportJunkEmailEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.ReportJunkEmailEnabled] = value;
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06003C08 RID: 15368 RVA: 0x000E5DD3 File Offset: 0x000E3FD3
		// (set) Token: 0x06003C09 RID: 15369 RVA: 0x000E5DE5 File Offset: 0x000E3FE5
		[Parameter(Mandatory = false)]
		public bool GroupCreationEnabled
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.GroupCreationEnabled];
			}
			set
			{
				this[OwaMailboxPolicySchema.GroupCreationEnabled] = value;
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06003C0A RID: 15370 RVA: 0x000E5DF8 File Offset: 0x000E3FF8
		// (set) Token: 0x06003C0B RID: 15371 RVA: 0x000E5E0A File Offset: 0x000E400A
		[Parameter(Mandatory = false)]
		public bool SkipCreateUnifiedGroupCustomSharepointClassification
		{
			get
			{
				return (bool)this[OwaMailboxPolicySchema.SkipCreateUnifiedGroupCustomSharepointClassification];
			}
			set
			{
				this[OwaMailboxPolicySchema.SkipCreateUnifiedGroupCustomSharepointClassification] = value;
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06003C0C RID: 15372 RVA: 0x000E5E1D File Offset: 0x000E401D
		// (set) Token: 0x06003C0D RID: 15373 RVA: 0x000E5E2F File Offset: 0x000E402F
		[Parameter(Mandatory = false)]
		public WebPartsFrameOptions WebPartsFrameOptionsType
		{
			get
			{
				return (WebPartsFrameOptions)this[ADOwaVirtualDirectorySchema.WebPartsFrameOptionsType];
			}
			set
			{
				this[ADOwaVirtualDirectorySchema.WebPartsFrameOptionsType] = (int)value;
			}
		}

		// Token: 0x0400288E RID: 10382
		private static OwaMailboxPolicySchema schema = ObjectSchema.GetInstance<OwaMailboxPolicySchema>();

		// Token: 0x0400288F RID: 10383
		private static string mostDerivedClass = "msExchOWAMailboxPolicy";

		// Token: 0x04002890 RID: 10384
		private static ADObjectId parentPath = new ADObjectId("CN=OWA Mailbox Policies");

		// Token: 0x04002891 RID: 10385
		private static MultiValuedProperty<string> webReadyDocumentViewingSupportedFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultWebReadyFileTypes);

		// Token: 0x04002892 RID: 10386
		private static MultiValuedProperty<string> webReadyDocumentViewingSupportedMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultWebReadyMimeTypes);
	}
}
