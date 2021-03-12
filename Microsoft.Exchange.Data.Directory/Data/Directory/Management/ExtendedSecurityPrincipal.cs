using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200070B RID: 1803
	[Serializable]
	public class ExtendedSecurityPrincipal : ADConfigurationObject
	{
		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x060054E3 RID: 21731 RVA: 0x00132976 File Offset: 0x00130B76
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExtendedSecurityPrincipal.schema;
			}
		}

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x060054E4 RID: 21732 RVA: 0x0013297D File Offset: 0x00130B7D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExtendedSecurityPrincipal.mostDerivedClass;
			}
		}

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x060054E6 RID: 21734 RVA: 0x0013298C File Offset: 0x00130B8C
		public SecurityIdentifier SID
		{
			get
			{
				return (SecurityIdentifier)this[IADSecurityPrincipalSchema.Sid];
			}
		}

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x060054E7 RID: 21735 RVA: 0x0013299E File Offset: 0x00130B9E
		public string DisplayName
		{
			get
			{
				return (string)this[ExtendedSecurityPrincipalSchema.DisplayName];
			}
		}

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x060054E8 RID: 21736 RVA: 0x001329B0 File Offset: 0x00130BB0
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[ExtendedSecurityPrincipalSchema.RecipientTypeDetails];
			}
		}

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x060054E9 RID: 21737 RVA: 0x001329C2 File Offset: 0x00130BC2
		public SecurityPrincipalType Type
		{
			get
			{
				return this.securityPrincipalType;
			}
		}

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x060054EA RID: 21738 RVA: 0x001329CA File Offset: 0x00130BCA
		public string InFolder
		{
			get
			{
				return this.inFolder;
			}
		}

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x060054EB RID: 21739 RVA: 0x001329D2 File Offset: 0x00130BD2
		// (set) Token: 0x060054EC RID: 21740 RVA: 0x001329DA File Offset: 0x00130BDA
		public string UserFriendlyName { get; internal set; }

		// Token: 0x060054ED RID: 21741 RVA: 0x001329E3 File Offset: 0x00130BE3
		internal static void SecurityPrincipalTypeDetailsSetter(object value, IPropertyBag propertyBag)
		{
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x001329E8 File Offset: 0x00130BE8
		internal static object SecurityPrincipalTypeDetailsGetter(IPropertyBag propertyBag)
		{
			Exception ex = null;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.ObjectCategory];
				if (adobjectId != null)
				{
					MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADObjectSchema.ObjectClass];
					string text = adobjectId.Name.ToLower();
					if (text == ADUser.ObjectCategoryNameInternal && multiValuedProperty.Count > 0 && multiValuedProperty.Contains(ADUser.MostDerivedClass))
					{
						return SecurityPrincipalType.User;
					}
					if (text == ADGroup.MostDerivedClass)
					{
						return SecurityPrincipalType.Group;
					}
					if (text == "foreign-security-principal")
					{
						return SecurityPrincipalType.WellknownSecurityPrincipal;
					}
					if (text == ExtendedSecurityPrincipal.computer && multiValuedProperty.Count > 0 && multiValuedProperty.Contains(ADUser.MostDerivedClass))
					{
						return SecurityPrincipalType.Computer;
					}
					ex = new ArgumentException(DirectoryStrings.ExArgumentException("ObjectCategory", text));
				}
				else
				{
					ex = new ArgumentNullException(DirectoryStrings.ExArgumentNullException("ObjectCategory"));
				}
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("SecurityPrincipalTypes", ex.Message), ExtendedSecurityPrincipalSchema.SecurityPrincipalTypes, propertyBag[ADObjectSchema.ObjectCategory]), ex);
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x00132B30 File Offset: 0x00130D30
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			this.securityPrincipalType = (SecurityPrincipalType)this[ExtendedSecurityPrincipalSchema.SecurityPrincipalTypes];
			if (this.securityPrincipalType == SecurityPrincipalType.User || this.securityPrincipalType == SecurityPrincipalType.Group)
			{
				this.inFolder = base.Id.Parent.ToCanonicalName();
				return;
			}
			if (this.securityPrincipalType == SecurityPrincipalType.WellknownSecurityPrincipal)
			{
				this.inFolder = null;
				return;
			}
			errors.Add(new PropertyValidationError(DataStrings.BadEnumValue(typeof(SecurityPrincipalType)), ADObjectSchema.ObjectClass, null));
		}

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x060054F0 RID: 21744 RVA: 0x00132BB4 File Offset: 0x00130DB4
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADUser.ObjectCategoryNameInternal),
						ADObject.ObjectClassFilter(ADUser.MostDerivedClass, true)
					}),
					new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADGroup.MostDerivedClass),
						new BitMaskOrFilter(ADGroupSchema.GroupType, (ulong)int.MinValue)
					}),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ExtendedSecurityPrincipal.WellknownSecurityPrincipalClassName)
				});
			}
		}

		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x060054F1 RID: 21745 RVA: 0x00132C4A File Offset: 0x00130E4A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ADRecipient.PublicFolderMailboxObjectVersion;
			}
		}

		// Token: 0x040038FE RID: 14590
		private const string WellknownSecurityPrincipalName = "foreign-security-principal";

		// Token: 0x040038FF RID: 14591
		internal static string WellknownSecurityPrincipalClassName = "foreignSecurityPrincipal";

		// Token: 0x04003900 RID: 14592
		private static ExtendedSecurityPrincipalSchema schema = ObjectSchema.GetInstance<ExtendedSecurityPrincipalSchema>();

		// Token: 0x04003901 RID: 14593
		private static string mostDerivedClass = "securityPrincipal";

		// Token: 0x04003902 RID: 14594
		private static string computer = "computer";

		// Token: 0x04003903 RID: 14595
		private SecurityPrincipalType securityPrincipalType;

		// Token: 0x04003904 RID: 14596
		private string inFolder;
	}
}
