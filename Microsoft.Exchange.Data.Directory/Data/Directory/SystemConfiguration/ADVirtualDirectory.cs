using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000322 RID: 802
	[Serializable]
	public class ADVirtualDirectory : ADConfigurationObject
	{
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x0009DFDF File Offset: 0x0009C1DF
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADVirtualDirectory.schema;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x0009DFE6 File Offset: 0x0009C1E6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADVirtualDirectory.mostDerivedClass;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x0009DFF0 File Offset: 0x0009C1F0
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADAutodiscoverVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADPowerShellCommonVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADOwaVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADWebServicesVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchMobileVirtualDirectory"),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADOabVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADRpcHttpVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADAvailabilityForeignConnectorVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADEcpVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADE12UMVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADMapiVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADO365SuiteServiceVirtualDirectory.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADSnackyServiceVirtualDirectory.MostDerivedClass)
				});
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x0009E11C File Offset: 0x0009C31C
		internal static object ServerGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId == null && (ObjectState)propertyBag[ADObjectSchema.ObjectState] != ObjectState.New)
				{
					throw new InvalidOperationException(DirectoryStrings.IdIsNotSet);
				}
				result = ((adobjectId == null) ? null : adobjectId.AncestorDN(3));
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Server", ex.Message), ADVirtualDirectorySchema.Server, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x0009E1B0 File Offset: 0x0009C3B0
		internal static object ServerNameGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId == null && (ObjectState)propertyBag[ADObjectSchema.ObjectState] != ObjectState.New)
				{
					throw new InvalidOperationException(DirectoryStrings.IdIsNotSet);
				}
				result = ((adobjectId == null) ? null : ADVirtualDirectory.GetServerNameFromVDirObjectId(adobjectId));
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Server", ex.Message), ADVirtualDirectorySchema.Server, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x0009E240 File Offset: 0x0009C440
		internal static object InternalAuthenticationMethodsGetter(IPropertyBag propertyBag)
		{
			AuthenticationMethodFlags authenticationMethodFlags = (AuthenticationMethodFlags)propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethodFlags];
			return ADVirtualDirectory.AuthenticationMethodFlagsToAuthenticationMethodPropertyValue(authenticationMethodFlags);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x0009E264 File Offset: 0x0009C464
		internal static object ExternalAuthenticationMethodsGetter(IPropertyBag propertyBag)
		{
			AuthenticationMethodFlags authenticationMethodFlags = (AuthenticationMethodFlags)propertyBag[ADVirtualDirectorySchema.ExternalAuthenticationMethodFlags];
			return ADVirtualDirectory.AuthenticationMethodFlagsToAuthenticationMethodPropertyValue(authenticationMethodFlags);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x0009E288 File Offset: 0x0009C488
		internal static MultiValuedProperty<AuthenticationMethod> AuthenticationMethodFlagsToAuthenticationMethodPropertyValue(AuthenticationMethodFlags authenticationMethodFlags)
		{
			if (authenticationMethodFlags != AuthenticationMethodFlags.None)
			{
				List<AuthenticationMethod> list = new List<AuthenticationMethod>(3);
				if ((authenticationMethodFlags & AuthenticationMethodFlags.Basic) == AuthenticationMethodFlags.Basic)
				{
					list.Add(AuthenticationMethod.Basic);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.Fba) == AuthenticationMethodFlags.Fba)
				{
					list.Add(AuthenticationMethod.Fba);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.Ntlm) == AuthenticationMethodFlags.Ntlm)
				{
					list.Add(AuthenticationMethod.Ntlm);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.Digest) == AuthenticationMethodFlags.Digest)
				{
					list.Add(AuthenticationMethod.Digest);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.WindowsIntegrated) == AuthenticationMethodFlags.WindowsIntegrated)
				{
					list.Add(AuthenticationMethod.WindowsIntegrated);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.LiveIdFba) == AuthenticationMethodFlags.LiveIdFba)
				{
					list.Add(AuthenticationMethod.LiveIdFba);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.LiveIdBasic) == AuthenticationMethodFlags.LiveIdBasic)
				{
					list.Add(AuthenticationMethod.LiveIdBasic);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.WSSecurity) == AuthenticationMethodFlags.WSSecurity)
				{
					list.Add(AuthenticationMethod.WSSecurity);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.Certificate) == AuthenticationMethodFlags.Certificate)
				{
					list.Add(AuthenticationMethod.Certificate);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.NegoEx) == AuthenticationMethodFlags.NegoEx)
				{
					list.Add(AuthenticationMethod.NegoEx);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.LiveIdNegotiate) == AuthenticationMethodFlags.LiveIdNegotiate)
				{
					list.Add(AuthenticationMethod.LiveIdNegotiate);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.OAuth) == AuthenticationMethodFlags.OAuth)
				{
					list.Add(AuthenticationMethod.OAuth);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.Adfs) == AuthenticationMethodFlags.Adfs)
				{
					list.Add(AuthenticationMethod.Adfs);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.Kerberos) == AuthenticationMethodFlags.Kerberos)
				{
					list.Add(AuthenticationMethod.Kerberos);
				}
				if ((authenticationMethodFlags & AuthenticationMethodFlags.Negotiate) == AuthenticationMethodFlags.Negotiate)
				{
					list.Add(AuthenticationMethod.Negotiate);
				}
				return new MultiValuedProperty<AuthenticationMethod>(list);
			}
			return ADVirtualDirectory.EmptyAuthenticationMethodPropertyValue;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x0009E3C0 File Offset: 0x0009C5C0
		internal static void InternalAuthenticationMethodsSetter(object value, IPropertyBag propertyBag)
		{
			AuthenticationMethodFlags authenticationMethodFlags = ADVirtualDirectory.AuthenticationMethodPropertyValueToAuthenticationMethodFlags((MultiValuedProperty<AuthenticationMethod>)value);
			propertyBag[ADVirtualDirectorySchema.InternalAuthenticationMethodFlags] = authenticationMethodFlags;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0009E3EC File Offset: 0x0009C5EC
		internal static void ExternalAuthenticationMethodsSetter(object value, IPropertyBag propertyBag)
		{
			AuthenticationMethodFlags authenticationMethodFlags = ADVirtualDirectory.AuthenticationMethodPropertyValueToAuthenticationMethodFlags((MultiValuedProperty<AuthenticationMethod>)value);
			propertyBag[ADVirtualDirectorySchema.ExternalAuthenticationMethodFlags] = authenticationMethodFlags;
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x0009E418 File Offset: 0x0009C618
		internal static AuthenticationMethodFlags AuthenticationMethodPropertyValueToAuthenticationMethodFlags(MultiValuedProperty<AuthenticationMethod> authenticationMethods)
		{
			AuthenticationMethodFlags authenticationMethodFlags = AuthenticationMethodFlags.None;
			if (authenticationMethods != null)
			{
				foreach (AuthenticationMethod authenticationMethod in authenticationMethods)
				{
					if (authenticationMethod == AuthenticationMethod.Basic)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.Basic;
					}
					else if (authenticationMethod == AuthenticationMethod.Fba)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.Fba;
					}
					else if (authenticationMethod == AuthenticationMethod.Ntlm)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.Ntlm;
					}
					else if (authenticationMethod == AuthenticationMethod.Digest)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.Digest;
					}
					else if (authenticationMethod == AuthenticationMethod.WindowsIntegrated)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.WindowsIntegrated;
					}
					else if (authenticationMethod == AuthenticationMethod.LiveIdFba)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.LiveIdFba;
					}
					else if (authenticationMethod == AuthenticationMethod.LiveIdBasic)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.LiveIdBasic;
					}
					else if (authenticationMethod == AuthenticationMethod.WSSecurity)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.WSSecurity;
					}
					else if (authenticationMethod == AuthenticationMethod.Certificate)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.Certificate;
					}
					else if (authenticationMethod == AuthenticationMethod.NegoEx)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.NegoEx;
					}
					else if (authenticationMethod == AuthenticationMethod.LiveIdNegotiate)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.LiveIdNegotiate;
					}
					else if (authenticationMethod == AuthenticationMethod.OAuth)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.OAuth;
					}
					else if (authenticationMethod == AuthenticationMethod.Adfs)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.Adfs;
					}
					else if (authenticationMethod == AuthenticationMethod.Kerberos)
					{
						authenticationMethodFlags |= AuthenticationMethodFlags.Kerberos;
					}
					else
					{
						if (authenticationMethod != AuthenticationMethod.Negotiate)
						{
							throw new ArgumentOutOfRangeException("value");
						}
						authenticationMethodFlags |= AuthenticationMethodFlags.Negotiate;
					}
				}
			}
			return authenticationMethodFlags;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x0009E54C File Offset: 0x0009C74C
		// (set) Token: 0x0600252A RID: 9514 RVA: 0x0009E55E File Offset: 0x0009C75E
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[ADVirtualDirectorySchema.AdminDisplayVersion];
			}
			internal set
			{
				this[ADVirtualDirectorySchema.AdminDisplayVersion] = value;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x0009E56C File Offset: 0x0009C76C
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[ADVirtualDirectorySchema.Server];
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x0009E57E File Offset: 0x0009C77E
		// (set) Token: 0x0600252D RID: 9517 RVA: 0x0009E590 File Offset: 0x0009C790
		public Uri InternalUrl
		{
			get
			{
				return (Uri)this[ADVirtualDirectorySchema.InternalUrl];
			}
			set
			{
				this[ADVirtualDirectorySchema.InternalUrl] = value;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600252E RID: 9518 RVA: 0x0009E59E File Offset: 0x0009C79E
		// (set) Token: 0x0600252F RID: 9519 RVA: 0x0009E5B0 File Offset: 0x0009C7B0
		public MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)this[ADVirtualDirectorySchema.InternalAuthenticationMethods];
			}
			set
			{
				this[ADVirtualDirectorySchema.InternalAuthenticationMethods] = value;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x0009E5BE File Offset: 0x0009C7BE
		// (set) Token: 0x06002531 RID: 9521 RVA: 0x0009E5D0 File Offset: 0x0009C7D0
		public Uri ExternalUrl
		{
			get
			{
				return (Uri)this[ADVirtualDirectorySchema.ExternalUrl];
			}
			set
			{
				this[ADVirtualDirectorySchema.ExternalUrl] = value;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x0009E5DE File Offset: 0x0009C7DE
		// (set) Token: 0x06002533 RID: 9523 RVA: 0x0009E5F0 File Offset: 0x0009C7F0
		public MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)this[ADVirtualDirectorySchema.ExternalAuthenticationMethods];
			}
			set
			{
				this[ADVirtualDirectorySchema.ExternalAuthenticationMethods] = value;
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x0009E600 File Offset: 0x0009C800
		internal static string GetServerNameFromVDirObjectId(ADObjectId vDirObjectId)
		{
			ADObjectId adobjectId = vDirObjectId.AncestorDN(3);
			return adobjectId.Name;
		}

		// Token: 0x040016E0 RID: 5856
		private static readonly string mostDerivedClass = "msExchVirtualDirectory";

		// Token: 0x040016E1 RID: 5857
		private static readonly MultiValuedProperty<AuthenticationMethod> EmptyAuthenticationMethodPropertyValue = new MultiValuedProperty<AuthenticationMethod>();

		// Token: 0x040016E2 RID: 5858
		private static readonly ADObjectSchema schema = ObjectSchema.GetInstance<ADVirtualDirectoryProperties>();
	}
}
