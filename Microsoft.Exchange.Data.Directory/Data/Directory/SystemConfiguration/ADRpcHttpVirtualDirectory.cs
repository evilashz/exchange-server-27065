using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000572 RID: 1394
	[Serializable]
	public class ADRpcHttpVirtualDirectory : ExchangeVirtualDirectory
	{
		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06003E53 RID: 15955 RVA: 0x000ECAB4 File Offset: 0x000EACB4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06003E54 RID: 15956 RVA: 0x000ECABB File Offset: 0x000EACBB
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADRpcHttpVirtualDirectory.schema;
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06003E55 RID: 15957 RVA: 0x000ECAC2 File Offset: 0x000EACC2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADRpcHttpVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x06003E56 RID: 15958 RVA: 0x000ECAC9 File Offset: 0x000EACC9
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x06003E57 RID: 15959 RVA: 0x000ECADC File Offset: 0x000EACDC
		public string ServerName
		{
			get
			{
				return (string)this[ADVirtualDirectorySchema.ServerName];
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06003E58 RID: 15960 RVA: 0x000ECAEE File Offset: 0x000EACEE
		// (set) Token: 0x06003E59 RID: 15961 RVA: 0x000ECB00 File Offset: 0x000EAD00
		public bool SSLOffloading
		{
			get
			{
				return (bool)this[ADRpcHttpVirtualDirectorySchema.SSLOffloading];
			}
			set
			{
				this[ADRpcHttpVirtualDirectorySchema.SSLOffloading] = value;
			}
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06003E5A RID: 15962 RVA: 0x000ECB13 File Offset: 0x000EAD13
		// (set) Token: 0x06003E5B RID: 15963 RVA: 0x000ECB25 File Offset: 0x000EAD25
		public Hostname ExternalHostname
		{
			get
			{
				return (Hostname)this[ADRpcHttpVirtualDirectorySchema.ExternalHostname];
			}
			set
			{
				this[ADRpcHttpVirtualDirectorySchema.ExternalHostname] = value;
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06003E5C RID: 15964 RVA: 0x000ECB33 File Offset: 0x000EAD33
		// (set) Token: 0x06003E5D RID: 15965 RVA: 0x000ECB45 File Offset: 0x000EAD45
		public Hostname InternalHostname
		{
			get
			{
				return (Hostname)this[ADRpcHttpVirtualDirectorySchema.InternalHostname];
			}
			set
			{
				this[ADRpcHttpVirtualDirectorySchema.InternalHostname] = value;
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06003E5E RID: 15966 RVA: 0x000ECB53 File Offset: 0x000EAD53
		// (set) Token: 0x06003E5F RID: 15967 RVA: 0x000ECB65 File Offset: 0x000EAD65
		public AuthenticationMethod ExternalClientAuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod)this[ADRpcHttpVirtualDirectorySchema.ExternalClientAuthenticationMethod];
			}
			set
			{
				this[ADRpcHttpVirtualDirectorySchema.ExternalClientAuthenticationMethod] = value;
			}
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x000ECB78 File Offset: 0x000EAD78
		// (set) Token: 0x06003E61 RID: 15969 RVA: 0x000ECB8A File Offset: 0x000EAD8A
		public AuthenticationMethod InternalClientAuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod)this[ADRpcHttpVirtualDirectorySchema.InternalClientAuthenticationMethod];
			}
			set
			{
				this[ADRpcHttpVirtualDirectorySchema.InternalClientAuthenticationMethod] = value;
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06003E62 RID: 15970 RVA: 0x000ECB9D File Offset: 0x000EAD9D
		// (set) Token: 0x06003E63 RID: 15971 RVA: 0x000ECBAF File Offset: 0x000EADAF
		public MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)this[ADRpcHttpVirtualDirectorySchema.IISAuthenticationMethods];
			}
			set
			{
				this[ADRpcHttpVirtualDirectorySchema.IISAuthenticationMethods] = value;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06003E64 RID: 15972 RVA: 0x000ECBC0 File Offset: 0x000EADC0
		// (set) Token: 0x06003E65 RID: 15973 RVA: 0x000ECBF2 File Offset: 0x000EADF2
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
			set
			{
				this[ADRpcHttpVirtualDirectorySchema.XropUrl] = new MultiValuedProperty<Uri>(value);
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000ECC05 File Offset: 0x000EAE05
		// (set) Token: 0x06003E67 RID: 15975 RVA: 0x000ECC17 File Offset: 0x000EAE17
		public bool ExternalClientsRequireSsl
		{
			get
			{
				return (bool)this[ADRpcHttpVirtualDirectorySchema.ExternalClientsRequireSsl];
			}
			set
			{
				this[ADRpcHttpVirtualDirectorySchema.ExternalClientsRequireSsl] = value;
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x000ECC2A File Offset: 0x000EAE2A
		// (set) Token: 0x06003E69 RID: 15977 RVA: 0x000ECC3C File Offset: 0x000EAE3C
		public bool InternalClientsRequireSsl
		{
			get
			{
				return (bool)this[ADRpcHttpVirtualDirectorySchema.InternalClientsRequireSsl];
			}
			set
			{
				this[ADRpcHttpVirtualDirectorySchema.InternalClientsRequireSsl] = value;
			}
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000ECC50 File Offset: 0x000EAE50
		internal static object GetClientsRequireSsl(IPropertyBag propertyBag, ADPropertyDefinition adPropertyDefinition)
		{
			Uri uri = (Uri)propertyBag[adPropertyDefinition];
			return uri != null && uri.Scheme == Uri.UriSchemeHttps;
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x000ECC8B File Offset: 0x000EAE8B
		internal static object GetExternalClientsRequireSsl(IPropertyBag propertyBag)
		{
			return ADRpcHttpVirtualDirectory.GetClientsRequireSsl(propertyBag, ADVirtualDirectorySchema.ExternalUrl);
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000ECC98 File Offset: 0x000EAE98
		internal static object GetInternalClientsRequireSsl(IPropertyBag propertyBag)
		{
			return ADRpcHttpVirtualDirectory.GetClientsRequireSsl(propertyBag, ADVirtualDirectorySchema.InternalUrl);
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x000ECCA5 File Offset: 0x000EAEA5
		internal static Uri CreateRpcUri(string uriScheme, string hostNameText)
		{
			return new Uri(uriScheme + "://" + hostNameText + "/rpc");
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x000ECCC0 File Offset: 0x000EAEC0
		internal static void SetClientsRequireSsl(object value, IPropertyBag propertyBag, ADPropertyDefinition adPropertyDefinition)
		{
			Uri uri = (Uri)propertyBag[adPropertyDefinition];
			if (uri == null)
			{
				return;
			}
			Hostname hostname = null;
			if (Hostname.TryParse(uri.DnsSafeHost, out hostname))
			{
				propertyBag[adPropertyDefinition] = ADRpcHttpVirtualDirectory.CreateRpcUri(((bool)value) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp, hostname.ToString());
			}
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x000ECD1B File Offset: 0x000EAF1B
		internal static void SetExternalClientsRequireSsl(object value, IPropertyBag propertyBag)
		{
			ADRpcHttpVirtualDirectory.SetClientsRequireSsl(value, propertyBag, ADVirtualDirectorySchema.ExternalUrl);
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x000ECD29 File Offset: 0x000EAF29
		internal static void SetInternalClientsRequireSsl(object value, IPropertyBag propertyBag)
		{
			ADRpcHttpVirtualDirectory.SetClientsRequireSsl(value, propertyBag, ADVirtualDirectorySchema.InternalUrl);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x000ECD37 File Offset: 0x000EAF37
		internal static void SetHostname(object value, IPropertyBag propertyBag, ADPropertyDefinition adPropertyDefinition)
		{
			if (value != null)
			{
				propertyBag[adPropertyDefinition] = ADRpcHttpVirtualDirectory.CreateRpcUri(((bool)ADRpcHttpVirtualDirectory.GetClientsRequireSsl(propertyBag, adPropertyDefinition)) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp, value.ToString());
				return;
			}
			propertyBag[adPropertyDefinition] = null;
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x000ECD71 File Offset: 0x000EAF71
		internal static void SetExternalHostname(object value, IPropertyBag propertyBag)
		{
			ADRpcHttpVirtualDirectory.SetHostname(value, propertyBag, ADVirtualDirectorySchema.ExternalUrl);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x000ECD7F File Offset: 0x000EAF7F
		internal static void SetInternalHostname(object value, IPropertyBag propertyBag)
		{
			ADRpcHttpVirtualDirectory.SetHostname(value, propertyBag, ADVirtualDirectorySchema.InternalUrl);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000ECD90 File Offset: 0x000EAF90
		internal static object GetHostname(IPropertyBag propertyBag, ADPropertyDefinition adPropertyDefinition)
		{
			Uri uri = (Uri)propertyBag[adPropertyDefinition];
			Hostname result;
			if (uri != null && Hostname.TryParse(uri.DnsSafeHost, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000ECDC5 File Offset: 0x000EAFC5
		internal static object GetExternalHostname(IPropertyBag propertyBag)
		{
			return ADRpcHttpVirtualDirectory.GetHostname(propertyBag, ADVirtualDirectorySchema.ExternalUrl);
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x000ECDD2 File Offset: 0x000EAFD2
		internal static object GetInternalHostname(IPropertyBag propertyBag)
		{
			return ADRpcHttpVirtualDirectory.GetHostname(propertyBag, ADVirtualDirectorySchema.InternalUrl);
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x000ECDE0 File Offset: 0x000EAFE0
		internal static void SetClientAuthenticationMethod(object value, IPropertyBag propertyBag, ADPropertyDefinition adPropertyDefinition)
		{
			AuthenticationMethod authMethod = (AuthenticationMethod)value;
			AuthenticationMethodFlags authenticationMethodFlags = ADRpcHttpVirtualDirectory.ClientAuthenticationMethodToFlags(authMethod);
			propertyBag[adPropertyDefinition] = authenticationMethodFlags;
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x000ECE08 File Offset: 0x000EB008
		internal static AuthenticationMethodFlags ClientAuthenticationMethodToFlags(AuthenticationMethod authMethod)
		{
			switch (authMethod)
			{
			case AuthenticationMethod.Basic:
				return AuthenticationMethodFlags.Basic;
			case AuthenticationMethod.Digest:
				break;
			case AuthenticationMethod.Ntlm:
				return AuthenticationMethodFlags.Ntlm;
			default:
				if (authMethod == AuthenticationMethod.NegoEx)
				{
					return AuthenticationMethodFlags.NegoEx;
				}
				if (authMethod == AuthenticationMethod.Negotiate)
				{
					return AuthenticationMethodFlags.Negotiate;
				}
				break;
			}
			return AuthenticationMethodFlags.None;
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x000ECE46 File Offset: 0x000EB046
		internal static void SetExternalClientAuthenticationMethod(object value, IPropertyBag propertyBag)
		{
			ADRpcHttpVirtualDirectory.SetClientAuthenticationMethod(value, propertyBag, ADVirtualDirectorySchema.ExternalAuthenticationMethodFlags);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000ECE54 File Offset: 0x000EB054
		internal static object GetClientAuthenticationMethod(IPropertyBag propertyBag, ADPropertyDefinition adPropertyDefinition)
		{
			AuthenticationMethodFlags authenticationMethodFlags = (AuthenticationMethodFlags)propertyBag[adPropertyDefinition];
			AuthenticationMethod authenticationMethod = ADRpcHttpVirtualDirectory.ClientAuthenticationMethodFromFlags(authenticationMethodFlags);
			return authenticationMethod;
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x000ECE7C File Offset: 0x000EB07C
		internal static AuthenticationMethod ClientAuthenticationMethodFromFlags(AuthenticationMethodFlags authenticationMethodFlags)
		{
			switch (authenticationMethodFlags)
			{
			case AuthenticationMethodFlags.Basic:
				return AuthenticationMethod.Basic;
			case AuthenticationMethodFlags.Ntlm:
				return AuthenticationMethod.Ntlm;
			default:
				if (authenticationMethodFlags == AuthenticationMethodFlags.NegoEx)
				{
					return AuthenticationMethod.NegoEx;
				}
				if (authenticationMethodFlags != AuthenticationMethodFlags.Negotiate)
				{
					return AuthenticationMethod.Misconfigured;
				}
				return AuthenticationMethod.Negotiate;
			}
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x000ECEBC File Offset: 0x000EB0BC
		internal static object GetExternalClientAuthenticationMethod(IPropertyBag propertyBag)
		{
			return ADRpcHttpVirtualDirectory.GetClientAuthenticationMethod(propertyBag, ADVirtualDirectorySchema.ExternalAuthenticationMethodFlags);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x000ECECC File Offset: 0x000EB0CC
		internal static object GetIISAuthenticationMethods(IPropertyBag propertyBag)
		{
			AuthenticationMethodFlags authenticationMethodFlags = (AuthenticationMethodFlags)propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethodFlags];
			if (authenticationMethodFlags == AuthenticationMethodFlags.None)
			{
				authenticationMethodFlags = (AuthenticationMethodFlags.Basic | AuthenticationMethodFlags.Ntlm | AuthenticationMethodFlags.Negotiate);
			}
			return ADVirtualDirectory.AuthenticationMethodFlagsToAuthenticationMethodPropertyValue(authenticationMethodFlags);
		}

		// Token: 0x17001413 RID: 5139
		// (set) Token: 0x06003E7E RID: 15998 RVA: 0x000ECEF9 File Offset: 0x000EB0F9
		private new Uri InternalUrl
		{
			set
			{
			}
		}

		// Token: 0x17001414 RID: 5140
		// (set) Token: 0x06003E7F RID: 15999 RVA: 0x000ECEFB File Offset: 0x000EB0FB
		private new Uri ExternalUrl
		{
			set
			{
			}
		}

		// Token: 0x17001415 RID: 5141
		// (set) Token: 0x06003E80 RID: 16000 RVA: 0x000ECEFD File Offset: 0x000EB0FD
		private new MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			set
			{
			}
		}

		// Token: 0x17001416 RID: 5142
		// (set) Token: 0x06003E81 RID: 16001 RVA: 0x000ECEFF File Offset: 0x000EB0FF
		private new MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			set
			{
			}
		}

		// Token: 0x04002A3D RID: 10813
		private static readonly ADRpcHttpVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADRpcHttpVirtualDirectorySchema>();

		// Token: 0x04002A3E RID: 10814
		public static readonly string MostDerivedClass = "msExchRpcHttpVirtualDirectory";
	}
}
