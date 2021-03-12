using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003B9 RID: 953
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ADClientAccessRule : ADConfigurationObject
	{
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000B4EFF File Offset: 0x000B30FF
		// (set) Token: 0x06002BA9 RID: 11177 RVA: 0x000B4F11 File Offset: 0x000B3111
		internal string RuleName
		{
			get
			{
				return (string)this[ADClientAccessRuleSchema.RuleName];
			}
			set
			{
				this[ADClientAccessRuleSchema.RuleName] = value;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000B4F1F File Offset: 0x000B311F
		// (set) Token: 0x06002BAB RID: 11179 RVA: 0x000B4F31 File Offset: 0x000B3131
		internal int InternalPriority
		{
			get
			{
				return (int)this[ADClientAccessRuleSchema.InternalPriority];
			}
			set
			{
				this[ADClientAccessRuleSchema.InternalPriority] = value;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x000B4F44 File Offset: 0x000B3144
		// (set) Token: 0x06002BAD RID: 11181 RVA: 0x000B4F56 File Offset: 0x000B3156
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return (int)this[ADClientAccessRuleSchema.Priority];
			}
			set
			{
				this[ADClientAccessRuleSchema.Priority] = value;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x000B4F69 File Offset: 0x000B3169
		// (set) Token: 0x06002BAF RID: 11183 RVA: 0x000B4F7B File Offset: 0x000B317B
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)this[ADClientAccessRuleSchema.Enabled];
			}
			set
			{
				this[ADClientAccessRuleSchema.Enabled] = value;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000B4F8E File Offset: 0x000B318E
		// (set) Token: 0x06002BB1 RID: 11185 RVA: 0x000B4FA0 File Offset: 0x000B31A0
		public bool DatacenterAdminsOnly
		{
			get
			{
				return (bool)this[ADClientAccessRuleSchema.DatacenterAdminsOnly];
			}
			set
			{
				this[ADClientAccessRuleSchema.DatacenterAdminsOnly] = value;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x000B4FB3 File Offset: 0x000B31B3
		// (set) Token: 0x06002BB3 RID: 11187 RVA: 0x000B4FC5 File Offset: 0x000B31C5
		[Parameter(Mandatory = false)]
		public ClientAccessRulesAction Action
		{
			get
			{
				return (ClientAccessRulesAction)this[ADClientAccessRuleSchema.Action];
			}
			set
			{
				this[ADClientAccessRuleSchema.Action] = value;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x000B4FD8 File Offset: 0x000B31D8
		// (set) Token: 0x06002BB5 RID: 11189 RVA: 0x000B4FEA File Offset: 0x000B31EA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> AnyOfClientIPAddressesOrRanges
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[ADClientAccessRuleSchema.AnyOfClientIPAddressesOrRanges];
			}
			set
			{
				this[ADClientAccessRuleSchema.AnyOfClientIPAddressesOrRanges] = value;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000B4FF8 File Offset: 0x000B31F8
		// (set) Token: 0x06002BB7 RID: 11191 RVA: 0x000B500A File Offset: 0x000B320A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> ExceptAnyOfClientIPAddressesOrRanges
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[ADClientAccessRuleSchema.ExceptAnyOfClientIPAddressesOrRanges];
			}
			set
			{
				this[ADClientAccessRuleSchema.ExceptAnyOfClientIPAddressesOrRanges] = value;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000B5018 File Offset: 0x000B3218
		// (set) Token: 0x06002BB9 RID: 11193 RVA: 0x000B502A File Offset: 0x000B322A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IntRange> AnyOfSourceTcpPortNumbers
		{
			get
			{
				return (MultiValuedProperty<IntRange>)this[ADClientAccessRuleSchema.AnyOfSourceTcpPortNumbers];
			}
			set
			{
				this[ADClientAccessRuleSchema.AnyOfSourceTcpPortNumbers] = value;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x000B5038 File Offset: 0x000B3238
		// (set) Token: 0x06002BBB RID: 11195 RVA: 0x000B504A File Offset: 0x000B324A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IntRange> ExceptAnyOfSourceTcpPortNumbers
		{
			get
			{
				return (MultiValuedProperty<IntRange>)this[ADClientAccessRuleSchema.ExceptAnyOfSourceTcpPortNumbers];
			}
			set
			{
				this[ADClientAccessRuleSchema.ExceptAnyOfSourceTcpPortNumbers] = value;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000B5058 File Offset: 0x000B3258
		// (set) Token: 0x06002BBD RID: 11197 RVA: 0x000B506A File Offset: 0x000B326A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UsernameMatchesAnyOfPatterns
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADClientAccessRuleSchema.UsernameMatchesAnyOfPatterns];
			}
			set
			{
				this[ADClientAccessRuleSchema.UsernameMatchesAnyOfPatterns] = value;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000B5078 File Offset: 0x000B3278
		// (set) Token: 0x06002BBF RID: 11199 RVA: 0x000B508A File Offset: 0x000B328A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExceptUsernameMatchesAnyOfPatterns
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADClientAccessRuleSchema.ExceptUsernameMatchesAnyOfPatterns];
			}
			set
			{
				this[ADClientAccessRuleSchema.ExceptUsernameMatchesAnyOfPatterns] = value;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000B5098 File Offset: 0x000B3298
		// (set) Token: 0x06002BC1 RID: 11201 RVA: 0x000B50AA File Offset: 0x000B32AA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UserIsMemberOf
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADClientAccessRuleSchema.UserIsMemberOf];
			}
			set
			{
				this[ADClientAccessRuleSchema.UserIsMemberOf] = value;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000B50B8 File Offset: 0x000B32B8
		// (set) Token: 0x06002BC3 RID: 11203 RVA: 0x000B50CA File Offset: 0x000B32CA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExceptUserIsMemberOf
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADClientAccessRuleSchema.ExceptUserIsMemberOf];
			}
			set
			{
				this[ADClientAccessRuleSchema.ExceptUserIsMemberOf] = value;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000B50D8 File Offset: 0x000B32D8
		// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x000B50EA File Offset: 0x000B32EA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ClientAccessAuthenticationMethod> AnyOfAuthenticationTypes
		{
			get
			{
				return (MultiValuedProperty<ClientAccessAuthenticationMethod>)this[ADClientAccessRuleSchema.AnyOfAuthenticationTypes];
			}
			set
			{
				this[ADClientAccessRuleSchema.AnyOfAuthenticationTypes] = value;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x000B50F8 File Offset: 0x000B32F8
		// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x000B510A File Offset: 0x000B330A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ClientAccessAuthenticationMethod> ExceptAnyOfAuthenticationTypes
		{
			get
			{
				return (MultiValuedProperty<ClientAccessAuthenticationMethod>)this[ADClientAccessRuleSchema.ExceptAnyOfAuthenticationTypes];
			}
			set
			{
				this[ADClientAccessRuleSchema.ExceptAnyOfAuthenticationTypes] = value;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x000B5118 File Offset: 0x000B3318
		// (set) Token: 0x06002BC9 RID: 11209 RVA: 0x000B512A File Offset: 0x000B332A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ClientAccessProtocol> AnyOfProtocols
		{
			get
			{
				return (MultiValuedProperty<ClientAccessProtocol>)this[ADClientAccessRuleSchema.AnyOfProtocols];
			}
			set
			{
				this[ADClientAccessRuleSchema.AnyOfProtocols] = value;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x000B5138 File Offset: 0x000B3338
		// (set) Token: 0x06002BCB RID: 11211 RVA: 0x000B514A File Offset: 0x000B334A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ClientAccessProtocol> ExceptAnyOfProtocols
		{
			get
			{
				return (MultiValuedProperty<ClientAccessProtocol>)this[ADClientAccessRuleSchema.ExceptAnyOfProtocols];
			}
			set
			{
				this[ADClientAccessRuleSchema.ExceptAnyOfProtocols] = value;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000B5158 File Offset: 0x000B3358
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x000B516A File Offset: 0x000B336A
		[Parameter(Mandatory = false)]
		public string UserRecipientFilter
		{
			get
			{
				return (string)this[ADClientAccessRuleSchema.UserRecipientFilter];
			}
			set
			{
				this[ADClientAccessRuleSchema.UserRecipientFilter] = value;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x000B5178 File Offset: 0x000B3378
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADClientAccessRule.schema;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x000B517F File Offset: 0x000B337F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADClientAccessRule.mostDerivedClass;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x000B5186 File Offset: 0x000B3386
		// (set) Token: 0x06002BD1 RID: 11217 RVA: 0x000B5198 File Offset: 0x000B3398
		private string Xml
		{
			get
			{
				return (string)this[ADClientAccessRuleSchema.Xml];
			}
			set
			{
				this[ADClientAccessRuleSchema.Xml] = value;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x000B51A6 File Offset: 0x000B33A6
		// (set) Token: 0x06002BD3 RID: 11219 RVA: 0x000B51AE File Offset: 0x000B33AE
		private new OrganizationId OrganizationId
		{
			get
			{
				return base.OrganizationId;
			}
			set
			{
				base.OrganizationId = value;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x000B51B7 File Offset: 0x000B33B7
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000B51BE File Offset: 0x000B33BE
		internal override ADObjectId ParentPath
		{
			get
			{
				return ADClientAccessRule.parentPath;
			}
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000B51C5 File Offset: 0x000B33C5
		internal ClientAccessRule GetClientAccessRule()
		{
			return ClientAccessRule.FromADProperties((string)this[ADClientAccessRuleSchema.Xml], this.Identity, base.Name, this.Priority, this.Enabled, this.DatacenterAdminsOnly, false);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000B51FC File Offset: 0x000B33FC
		internal bool HasAnyOfSpecificProtocolsPredicate(List<ClientAccessProtocol> protocols)
		{
			bool flag = false;
			if (this.AnyOfProtocols != null)
			{
				if (this.AnyOfProtocols.Except(protocols).Count<ClientAccessProtocol>() > 0)
				{
					return false;
				}
				flag = (flag || this.AnyOfProtocols.Intersect(protocols).Count<ClientAccessProtocol>() > 0);
			}
			if (this.ExceptAnyOfProtocols != null)
			{
				if (this.ExceptAnyOfProtocols.Except(protocols).Count<ClientAccessProtocol>() > 0)
				{
					return false;
				}
				flag = (flag || this.ExceptAnyOfProtocols.Intersect(protocols).Count<ClientAccessProtocol>() > 0);
			}
			return flag;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000B527E File Offset: 0x000B347E
		internal bool HasAuthenticationMethodPredicate(ClientAccessAuthenticationMethod authenticationMethod)
		{
			return (this.AnyOfAuthenticationTypes != null && this.AnyOfAuthenticationTypes.Contains(authenticationMethod)) || (this.ExceptAnyOfAuthenticationTypes != null && this.ExceptAnyOfAuthenticationTypes.Contains(authenticationMethod));
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000B52B1 File Offset: 0x000B34B1
		internal bool HasAnyAuthenticationMethodPredicate()
		{
			return (this.AnyOfAuthenticationTypes != null && this.AnyOfAuthenticationTypes.Count > 0) || (this.ExceptAnyOfAuthenticationTypes != null && this.ExceptAnyOfAuthenticationTypes.Count > 0);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000B52E4 File Offset: 0x000B34E4
		internal bool ValidateUserRecipientFilterParsesWithSchema()
		{
			if (!string.IsNullOrEmpty(this.UserRecipientFilter))
			{
				new QueryParser(this.UserRecipientFilter, ObjectSchema.GetInstance<ClientAccessRulesRecipientFilterSchema>(), QueryParser.Capabilities.All, null, new QueryParser.ConvertValueFromStringDelegate(QueryParserUtils.ConvertValueFromString));
			}
			return true;
		}

		// Token: 0x04001A4E RID: 6734
		private static ADClientAccessRuleSchema schema = ObjectSchema.GetInstance<ADClientAccessRuleSchema>();

		// Token: 0x04001A4F RID: 6735
		private static string mostDerivedClass = "msExchClientAccessRule";

		// Token: 0x04001A50 RID: 6736
		private static ADObjectId parentPath = new ADObjectId("CN=" + ADClientAccessRuleCollection.ContainerName);
	}
}
