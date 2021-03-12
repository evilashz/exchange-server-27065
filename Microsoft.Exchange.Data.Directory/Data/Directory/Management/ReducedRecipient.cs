using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ValidationRules;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000781 RID: 1921
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ReducedRecipient : ADObject
	{
		// Token: 0x17002214 RID: 8724
		// (get) Token: 0x06005FDF RID: 24543 RVA: 0x00146910 File Offset: 0x00144B10
		internal static Dictionary<PropertySet, ADPropertyDefinition[]> Properties
		{
			get
			{
				if (ReducedRecipient.properties == null)
				{
					ReducedRecipient.properties = new Dictionary<PropertySet, ADPropertyDefinition[]>
					{
						{
							PropertySet.All,
							null
						},
						{
							PropertySet.ControlPanel,
							new ADPropertyDefinition[]
							{
								ReducedRecipientSchema.PrimarySmtpAddress,
								ReducedRecipientSchema.DisplayName,
								ReducedRecipientSchema.ArchiveGuid,
								ReducedRecipientSchema.AuthenticationType,
								ReducedRecipientSchema.RecipientType,
								ReducedRecipientSchema.RecipientTypeDetails,
								ReducedRecipientSchema.ResourceType,
								ReducedRecipientSchema.WindowsLiveID,
								ADObjectSchema.Id,
								ADObjectSchema.ExchangeVersion,
								ADObjectSchema.OrganizationId
							}
						},
						{
							PropertySet.ConsoleSmallSet,
							new ADPropertyDefinition[]
							{
								ReducedRecipientSchema.DisplayName,
								ReducedRecipientSchema.Alias,
								ReducedRecipientSchema.OrganizationalUnit,
								ReducedRecipientSchema.PrimarySmtpAddress,
								ReducedRecipientSchema.EmailAddresses,
								ReducedRecipientSchema.HiddenFromAddressListsEnabled,
								ADObjectSchema.Name,
								ADObjectSchema.WhenChanged,
								ReducedRecipientSchema.City,
								ReducedRecipientSchema.Company,
								ReducedRecipientSchema.CountryOrRegion,
								ReducedRecipientSchema.DatabaseName,
								ReducedRecipientSchema.Department,
								ReducedRecipientSchema.ExpansionServer,
								ReducedRecipientSchema.ExternalEmailAddress,
								ReducedRecipientSchema.FirstName,
								ReducedRecipientSchema.LastName,
								ReducedRecipientSchema.Office,
								ReducedRecipientSchema.StateOrProvince,
								ReducedRecipientSchema.Title,
								ReducedRecipientSchema.UMEnabled,
								ReducedRecipientSchema.HasActiveSyncDevicePartnership,
								ReducedRecipientSchema.Manager,
								ReducedRecipientSchema.SharingPolicy,
								ReducedRecipientSchema.ArchiveGuid,
								ReducedRecipientSchema.IsValidSecurityPrincipal,
								ReducedRecipientSchema.ArchiveState,
								ReducedRecipientSchema.MailboxMoveTargetMDB,
								ReducedRecipientSchema.MailboxMoveSourceMDB,
								ReducedRecipientSchema.MailboxMoveTargetArchiveMDB,
								ReducedRecipientSchema.MailboxMoveSourceArchiveMDB,
								ReducedRecipientSchema.MailboxMoveFlags,
								ReducedRecipientSchema.MailboxMoveRemoteHostName,
								ReducedRecipientSchema.MailboxMoveBatchName,
								ReducedRecipientSchema.MailboxMoveStatus,
								ReducedRecipientSchema.MailboxRelease,
								ReducedRecipientSchema.ArchiveRelease,
								ReducedRecipientSchema.RecipientType,
								ReducedRecipientSchema.RecipientTypeDetails,
								ADObjectSchema.Id,
								ADObjectSchema.ExchangeVersion,
								ADObjectSchema.OrganizationId
							}
						},
						{
							PropertySet.ConsoleLargeSet,
							new ADPropertyDefinition[]
							{
								ReducedRecipientSchema.DisplayName,
								ReducedRecipientSchema.Alias,
								ReducedRecipientSchema.OrganizationalUnit,
								ReducedRecipientSchema.PrimarySmtpAddress,
								ReducedRecipientSchema.CustomAttribute1,
								ReducedRecipientSchema.CustomAttribute2,
								ReducedRecipientSchema.CustomAttribute3,
								ReducedRecipientSchema.CustomAttribute4,
								ReducedRecipientSchema.CustomAttribute5,
								ReducedRecipientSchema.CustomAttribute6,
								ReducedRecipientSchema.CustomAttribute7,
								ReducedRecipientSchema.CustomAttribute8,
								ReducedRecipientSchema.CustomAttribute9,
								ReducedRecipientSchema.CustomAttribute10,
								ReducedRecipientSchema.CustomAttribute11,
								ReducedRecipientSchema.CustomAttribute12,
								ReducedRecipientSchema.CustomAttribute13,
								ReducedRecipientSchema.CustomAttribute14,
								ReducedRecipientSchema.CustomAttribute15,
								ReducedRecipientSchema.ExtensionCustomAttribute1,
								ReducedRecipientSchema.ExtensionCustomAttribute2,
								ReducedRecipientSchema.ExtensionCustomAttribute3,
								ReducedRecipientSchema.ExtensionCustomAttribute4,
								ReducedRecipientSchema.ExtensionCustomAttribute5,
								ReducedRecipientSchema.EmailAddresses,
								ReducedRecipientSchema.HiddenFromAddressListsEnabled,
								ADObjectSchema.Name,
								ADObjectSchema.WhenChanged,
								ReducedRecipientSchema.City,
								ReducedRecipientSchema.Company,
								ReducedRecipientSchema.CountryOrRegion,
								ReducedRecipientSchema.DatabaseName,
								ReducedRecipientSchema.ArchiveDatabase,
								ReducedRecipientSchema.Department,
								ReducedRecipientSchema.ExpansionServer,
								ReducedRecipientSchema.ExternalEmailAddress,
								ReducedRecipientSchema.ExternalDirectoryObjectId,
								ReducedRecipientSchema.FirstName,
								ReducedRecipientSchema.LastName,
								ReducedRecipientSchema.Office,
								ReducedRecipientSchema.Phone,
								ReducedRecipientSchema.PostalCode,
								ReducedRecipientSchema.StateOrProvince,
								ReducedRecipientSchema.Title,
								ReducedRecipientSchema.UMEnabled,
								ReducedRecipientSchema.UMMailboxPolicy,
								ReducedRecipientSchema.UMRecipientDialPlanId,
								ReducedRecipientSchema.ManagedFolderMailboxPolicy,
								ReducedRecipientSchema.ActiveSyncMailboxPolicy,
								ReducedRecipientSchema.OwaMailboxPolicy,
								ReducedRecipientSchema.AddressBookPolicy,
								ReducedRecipientSchema.HasActiveSyncDevicePartnership,
								ReducedRecipientSchema.SharingPolicy,
								ReducedRecipientSchema.ArchiveGuid,
								ReducedRecipientSchema.IsValidSecurityPrincipal,
								ReducedRecipientSchema.ArchiveState,
								ReducedRecipientSchema.MailboxMoveTargetMDB,
								ReducedRecipientSchema.MailboxMoveSourceMDB,
								ReducedRecipientSchema.MailboxMoveFlags,
								ReducedRecipientSchema.MailboxMoveRemoteHostName,
								ReducedRecipientSchema.MailboxMoveBatchName,
								ReducedRecipientSchema.MailboxMoveStatus,
								ReducedRecipientSchema.MailboxRelease,
								ReducedRecipientSchema.ArchiveRelease,
								ReducedRecipientSchema.RecipientType,
								ReducedRecipientSchema.RecipientTypeDetails,
								ADObjectSchema.Id,
								ADObjectSchema.ExchangeVersion,
								ADObjectSchema.OrganizationId
							}
						},
						{
							PropertySet.Minimum,
							new ADPropertyDefinition[]
							{
								ReducedRecipientSchema.RecipientType,
								ReducedRecipientSchema.RecipientTypeDetails,
								ADObjectSchema.Id,
								ADObjectSchema.ExchangeVersion,
								ADObjectSchema.OrganizationId
							}
						}
					};
				}
				return ReducedRecipient.properties;
			}
		}

		// Token: 0x17002215 RID: 8725
		// (get) Token: 0x06005FE0 RID: 24544 RVA: 0x00146DE0 File Offset: 0x00144FE0
		public override ObjectId Identity
		{
			get
			{
				ObjectId objectId = base.Identity;
				if (objectId is ADObjectId && SuppressingPiiContext.NeedPiiSuppression)
				{
					objectId = (ObjectId)SuppressingPiiProperty.TryRedact(ADObjectSchema.Id, objectId);
				}
				return objectId;
			}
		}

		// Token: 0x17002216 RID: 8726
		// (get) Token: 0x06005FE1 RID: 24545 RVA: 0x00146E15 File Offset: 0x00145015
		internal override ADObjectSchema Schema
		{
			get
			{
				return ReducedRecipient.schema;
			}
		}

		// Token: 0x17002217 RID: 8727
		// (get) Token: 0x06005FE2 RID: 24546 RVA: 0x00146E1C File Offset: 0x0014501C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002218 RID: 8728
		// (get) Token: 0x06005FE3 RID: 24547 RVA: 0x00146E1F File Offset: 0x0014501F
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return Filters.DefaultRecipientFilter;
			}
		}

		// Token: 0x17002219 RID: 8729
		// (get) Token: 0x06005FE4 RID: 24548 RVA: 0x00146E26 File Offset: 0x00145026
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ADRecipient.PublicFolderMailboxObjectVersion;
			}
		}

		// Token: 0x1700221A RID: 8730
		// (get) Token: 0x06005FE5 RID: 24549 RVA: 0x00146E2D File Offset: 0x0014502D
		internal override bool SkipPiiRedaction
		{
			get
			{
				return ADRecipient.IsSystemMailbox(this.RecipientTypeDetails);
			}
		}

		// Token: 0x1700221B RID: 8731
		// (get) Token: 0x06005FE7 RID: 24551 RVA: 0x00146E42 File Offset: 0x00145042
		public string Alias
		{
			get
			{
				return (string)this[ReducedRecipientSchema.Alias];
			}
		}

		// Token: 0x1700221C RID: 8732
		// (get) Token: 0x06005FE8 RID: 24552 RVA: 0x00146E54 File Offset: 0x00145054
		public Guid ArchiveGuid
		{
			get
			{
				return (Guid)this[ReducedRecipientSchema.ArchiveGuid];
			}
		}

		// Token: 0x1700221D RID: 8733
		// (get) Token: 0x06005FE9 RID: 24553 RVA: 0x00146E66 File Offset: 0x00145066
		// (set) Token: 0x06005FEA RID: 24554 RVA: 0x00146E78 File Offset: 0x00145078
		public AuthenticationType? AuthenticationType
		{
			get
			{
				return (AuthenticationType?)this[ReducedRecipientSchema.AuthenticationType];
			}
			internal set
			{
				this[ReducedRecipientSchema.AuthenticationType] = value;
			}
		}

		// Token: 0x1700221E RID: 8734
		// (get) Token: 0x06005FEB RID: 24555 RVA: 0x00146E8B File Offset: 0x0014508B
		public string City
		{
			get
			{
				return (string)this[ReducedRecipientSchema.City];
			}
		}

		// Token: 0x1700221F RID: 8735
		// (get) Token: 0x06005FEC RID: 24556 RVA: 0x00146E9D File Offset: 0x0014509D
		public string Notes
		{
			get
			{
				return (string)this[ReducedRecipientSchema.Notes];
			}
		}

		// Token: 0x17002220 RID: 8736
		// (get) Token: 0x06005FED RID: 24557 RVA: 0x00146EAF File Offset: 0x001450AF
		public string Company
		{
			get
			{
				return (string)this[ReducedRecipientSchema.Company];
			}
		}

		// Token: 0x17002221 RID: 8737
		// (get) Token: 0x06005FEE RID: 24558 RVA: 0x00146EC1 File Offset: 0x001450C1
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)this[ReducedRecipientSchema.CountryOrRegion];
			}
		}

		// Token: 0x17002222 RID: 8738
		// (get) Token: 0x06005FEF RID: 24559 RVA: 0x00146ED3 File Offset: 0x001450D3
		public string PostalCode
		{
			get
			{
				return (string)this[ReducedRecipientSchema.PostalCode];
			}
		}

		// Token: 0x17002223 RID: 8739
		// (get) Token: 0x06005FF0 RID: 24560 RVA: 0x00146EE5 File Offset: 0x001450E5
		public string CustomAttribute1
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute1];
			}
		}

		// Token: 0x17002224 RID: 8740
		// (get) Token: 0x06005FF1 RID: 24561 RVA: 0x00146EF7 File Offset: 0x001450F7
		public string CustomAttribute2
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute2];
			}
		}

		// Token: 0x17002225 RID: 8741
		// (get) Token: 0x06005FF2 RID: 24562 RVA: 0x00146F09 File Offset: 0x00145109
		public string CustomAttribute3
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute3];
			}
		}

		// Token: 0x17002226 RID: 8742
		// (get) Token: 0x06005FF3 RID: 24563 RVA: 0x00146F1B File Offset: 0x0014511B
		public string CustomAttribute4
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute4];
			}
		}

		// Token: 0x17002227 RID: 8743
		// (get) Token: 0x06005FF4 RID: 24564 RVA: 0x00146F2D File Offset: 0x0014512D
		public string CustomAttribute5
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute5];
			}
		}

		// Token: 0x17002228 RID: 8744
		// (get) Token: 0x06005FF5 RID: 24565 RVA: 0x00146F3F File Offset: 0x0014513F
		public string CustomAttribute6
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute6];
			}
		}

		// Token: 0x17002229 RID: 8745
		// (get) Token: 0x06005FF6 RID: 24566 RVA: 0x00146F51 File Offset: 0x00145151
		public string CustomAttribute7
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute7];
			}
		}

		// Token: 0x1700222A RID: 8746
		// (get) Token: 0x06005FF7 RID: 24567 RVA: 0x00146F63 File Offset: 0x00145163
		public string CustomAttribute8
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute8];
			}
		}

		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x06005FF8 RID: 24568 RVA: 0x00146F75 File Offset: 0x00145175
		public string CustomAttribute9
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute9];
			}
		}

		// Token: 0x1700222C RID: 8748
		// (get) Token: 0x06005FF9 RID: 24569 RVA: 0x00146F87 File Offset: 0x00145187
		public string CustomAttribute10
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute10];
			}
		}

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x06005FFA RID: 24570 RVA: 0x00146F99 File Offset: 0x00145199
		public string CustomAttribute11
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute11];
			}
		}

		// Token: 0x1700222E RID: 8750
		// (get) Token: 0x06005FFB RID: 24571 RVA: 0x00146FAB File Offset: 0x001451AB
		public string CustomAttribute12
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute12];
			}
		}

		// Token: 0x1700222F RID: 8751
		// (get) Token: 0x06005FFC RID: 24572 RVA: 0x00146FBD File Offset: 0x001451BD
		public string CustomAttribute13
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute13];
			}
		}

		// Token: 0x17002230 RID: 8752
		// (get) Token: 0x06005FFD RID: 24573 RVA: 0x00146FCF File Offset: 0x001451CF
		public string CustomAttribute14
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute14];
			}
		}

		// Token: 0x17002231 RID: 8753
		// (get) Token: 0x06005FFE RID: 24574 RVA: 0x00146FE1 File Offset: 0x001451E1
		public string CustomAttribute15
		{
			get
			{
				return (string)this[ReducedRecipientSchema.CustomAttribute15];
			}
		}

		// Token: 0x17002232 RID: 8754
		// (get) Token: 0x06005FFF RID: 24575 RVA: 0x00146FF3 File Offset: 0x001451F3
		public MultiValuedProperty<string> ExtensionCustomAttribute1
		{
			get
			{
				return (MultiValuedProperty<string>)this[ReducedRecipientSchema.ExtensionCustomAttribute1];
			}
		}

		// Token: 0x17002233 RID: 8755
		// (get) Token: 0x06006000 RID: 24576 RVA: 0x00147005 File Offset: 0x00145205
		public MultiValuedProperty<string> ExtensionCustomAttribute2
		{
			get
			{
				return (MultiValuedProperty<string>)this[ReducedRecipientSchema.ExtensionCustomAttribute2];
			}
		}

		// Token: 0x17002234 RID: 8756
		// (get) Token: 0x06006001 RID: 24577 RVA: 0x00147017 File Offset: 0x00145217
		public MultiValuedProperty<string> ExtensionCustomAttribute3
		{
			get
			{
				return (MultiValuedProperty<string>)this[ReducedRecipientSchema.ExtensionCustomAttribute3];
			}
		}

		// Token: 0x17002235 RID: 8757
		// (get) Token: 0x06006002 RID: 24578 RVA: 0x00147029 File Offset: 0x00145229
		public MultiValuedProperty<string> ExtensionCustomAttribute4
		{
			get
			{
				return (MultiValuedProperty<string>)this[ReducedRecipientSchema.ExtensionCustomAttribute4];
			}
		}

		// Token: 0x17002236 RID: 8758
		// (get) Token: 0x06006003 RID: 24579 RVA: 0x0014703B File Offset: 0x0014523B
		public MultiValuedProperty<string> ExtensionCustomAttribute5
		{
			get
			{
				return (MultiValuedProperty<string>)this[ReducedRecipientSchema.ExtensionCustomAttribute5];
			}
		}

		// Token: 0x17002237 RID: 8759
		// (get) Token: 0x06006004 RID: 24580 RVA: 0x0014704D File Offset: 0x0014524D
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[ReducedRecipientSchema.Database];
			}
		}

		// Token: 0x17002238 RID: 8760
		// (get) Token: 0x06006005 RID: 24581 RVA: 0x0014705F File Offset: 0x0014525F
		public ADObjectId ArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[ReducedRecipientSchema.ArchiveDatabase];
			}
		}

		// Token: 0x17002239 RID: 8761
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x00147071 File Offset: 0x00145271
		public string DatabaseName
		{
			get
			{
				return (string)this[ReducedRecipientSchema.DatabaseName];
			}
		}

		// Token: 0x1700223A RID: 8762
		// (get) Token: 0x06006007 RID: 24583 RVA: 0x00147083 File Offset: 0x00145283
		public string Department
		{
			get
			{
				return (string)this[ReducedRecipientSchema.Department];
			}
		}

		// Token: 0x1700223B RID: 8763
		// (get) Token: 0x06006008 RID: 24584 RVA: 0x00147095 File Offset: 0x00145295
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this[ReducedRecipientSchema.ExternalDirectoryObjectId];
			}
		}

		// Token: 0x1700223C RID: 8764
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x001470A7 File Offset: 0x001452A7
		public ADObjectId ManagedFolderMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[ReducedRecipientSchema.ManagedFolderMailboxPolicy];
			}
		}

		// Token: 0x1700223D RID: 8765
		// (get) Token: 0x0600600A RID: 24586 RVA: 0x001470B9 File Offset: 0x001452B9
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[ReducedRecipientSchema.EmailAddresses];
			}
		}

		// Token: 0x1700223E RID: 8766
		// (get) Token: 0x0600600B RID: 24587 RVA: 0x001470CB File Offset: 0x001452CB
		public string ExpansionServer
		{
			get
			{
				return (string)this[ReducedRecipientSchema.ExpansionServer];
			}
		}

		// Token: 0x1700223F RID: 8767
		// (get) Token: 0x0600600C RID: 24588 RVA: 0x001470DD File Offset: 0x001452DD
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)this[ReducedRecipientSchema.ExternalEmailAddress];
			}
		}

		// Token: 0x17002240 RID: 8768
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x001470EF File Offset: 0x001452EF
		// (set) Token: 0x0600600E RID: 24590 RVA: 0x00147101 File Offset: 0x00145301
		public string DisplayName
		{
			get
			{
				return (string)this[ReducedRecipientSchema.DisplayName];
			}
			internal set
			{
				this[ReducedRecipientSchema.DisplayName] = value;
			}
		}

		// Token: 0x17002241 RID: 8769
		// (get) Token: 0x0600600F RID: 24591 RVA: 0x0014710F File Offset: 0x0014530F
		public string FirstName
		{
			get
			{
				return (string)this[ReducedRecipientSchema.FirstName];
			}
		}

		// Token: 0x17002242 RID: 8770
		// (get) Token: 0x06006010 RID: 24592 RVA: 0x00147121 File Offset: 0x00145321
		public bool HiddenFromAddressListsEnabled
		{
			get
			{
				return (bool)this[ReducedRecipientSchema.HiddenFromAddressListsEnabled];
			}
		}

		// Token: 0x17002243 RID: 8771
		// (get) Token: 0x06006011 RID: 24593 RVA: 0x00147133 File Offset: 0x00145333
		public bool EmailAddressPolicyEnabled
		{
			get
			{
				return (bool)this[ReducedRecipientSchema.EmailAddressPolicyEnabled];
			}
		}

		// Token: 0x17002244 RID: 8772
		// (get) Token: 0x06006012 RID: 24594 RVA: 0x00147145 File Offset: 0x00145345
		internal bool IsDirSynced
		{
			get
			{
				return (bool)this[ReducedRecipientSchema.IsDirSynced];
			}
		}

		// Token: 0x17002245 RID: 8773
		// (get) Token: 0x06006013 RID: 24595 RVA: 0x00147157 File Offset: 0x00145357
		internal bool IsDirSyncEnabled
		{
			get
			{
				return ADObject.IsRecipientDirSynced(this.IsDirSynced);
			}
		}

		// Token: 0x17002246 RID: 8774
		// (get) Token: 0x06006014 RID: 24596 RVA: 0x00147164 File Offset: 0x00145364
		internal MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return (MultiValuedProperty<string>)this[ReducedRecipientSchema.DirSyncAuthorityMetadata];
			}
		}

		// Token: 0x17002247 RID: 8775
		// (get) Token: 0x06006015 RID: 24597 RVA: 0x00147176 File Offset: 0x00145376
		public string LastName
		{
			get
			{
				return (string)this[ReducedRecipientSchema.LastName];
			}
		}

		// Token: 0x17002248 RID: 8776
		// (get) Token: 0x06006016 RID: 24598 RVA: 0x00147188 File Offset: 0x00145388
		public ExchangeResourceType? ResourceType
		{
			get
			{
				return (ExchangeResourceType?)this[ReducedRecipientSchema.ResourceType];
			}
		}

		// Token: 0x17002249 RID: 8777
		// (get) Token: 0x06006017 RID: 24599 RVA: 0x0014719A File Offset: 0x0014539A
		public MultiValuedProperty<ADObjectId> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ReducedRecipientSchema.ManagedBy];
			}
		}

		// Token: 0x1700224A RID: 8778
		// (get) Token: 0x06006018 RID: 24600 RVA: 0x001471AC File Offset: 0x001453AC
		public ADObjectId Manager
		{
			get
			{
				return (ADObjectId)this[ReducedRecipientSchema.Manager];
			}
		}

		// Token: 0x1700224B RID: 8779
		// (get) Token: 0x06006019 RID: 24601 RVA: 0x001471BE File Offset: 0x001453BE
		// (set) Token: 0x0600601A RID: 24602 RVA: 0x001471DA File Offset: 0x001453DA
		public ADObjectId ActiveSyncMailboxPolicy
		{
			get
			{
				return this.activeSyncMailboxPolicy ?? ((ADObjectId)this[ReducedRecipientSchema.ActiveSyncMailboxPolicy]);
			}
			internal set
			{
				this.activeSyncMailboxPolicy = value;
			}
		}

		// Token: 0x1700224C RID: 8780
		// (get) Token: 0x0600601B RID: 24603 RVA: 0x001471E3 File Offset: 0x001453E3
		// (set) Token: 0x0600601C RID: 24604 RVA: 0x001471FF File Offset: 0x001453FF
		public bool ActiveSyncMailboxPolicyIsDefaulted
		{
			get
			{
				return (bool)(this[ReducedRecipientSchema.ActiveSyncMailboxPolicyIsDefaulted] ?? false);
			}
			internal set
			{
				this[ReducedRecipientSchema.ActiveSyncMailboxPolicyIsDefaulted] = value;
			}
		}

		// Token: 0x1700224D RID: 8781
		// (get) Token: 0x0600601D RID: 24605 RVA: 0x00147212 File Offset: 0x00145412
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
		}

		// Token: 0x1700224E RID: 8782
		// (get) Token: 0x0600601E RID: 24606 RVA: 0x00147224 File Offset: 0x00145424
		public string Office
		{
			get
			{
				return (string)this[ReducedRecipientSchema.Office];
			}
		}

		// Token: 0x1700224F RID: 8783
		// (get) Token: 0x0600601F RID: 24607 RVA: 0x00147236 File Offset: 0x00145436
		public new ADObjectId ObjectCategory
		{
			get
			{
				return (ADObjectId)this[ADObjectSchema.ObjectCategory];
			}
		}

		// Token: 0x17002250 RID: 8784
		// (get) Token: 0x06006020 RID: 24608 RVA: 0x00147248 File Offset: 0x00145448
		public string OrganizationalUnit
		{
			get
			{
				return (string)this[ReducedRecipientSchema.OrganizationalUnit];
			}
		}

		// Token: 0x17002251 RID: 8785
		// (get) Token: 0x06006021 RID: 24609 RVA: 0x0014725A File Offset: 0x0014545A
		public string Phone
		{
			get
			{
				return (string)this[ReducedRecipientSchema.Phone];
			}
		}

		// Token: 0x17002252 RID: 8786
		// (get) Token: 0x06006022 RID: 24610 RVA: 0x0014726C File Offset: 0x0014546C
		public MultiValuedProperty<string> PoliciesIncluded
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.PoliciesIncluded];
			}
		}

		// Token: 0x17002253 RID: 8787
		// (get) Token: 0x06006023 RID: 24611 RVA: 0x0014727E File Offset: 0x0014547E
		public MultiValuedProperty<string> PoliciesExcluded
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.PoliciesExcluded];
			}
		}

		// Token: 0x17002254 RID: 8788
		// (get) Token: 0x06006024 RID: 24612 RVA: 0x00147290 File Offset: 0x00145490
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[ReducedRecipientSchema.PrimarySmtpAddress];
			}
		}

		// Token: 0x17002255 RID: 8789
		// (get) Token: 0x06006025 RID: 24613 RVA: 0x001472A2 File Offset: 0x001454A2
		public RecipientType RecipientType
		{
			get
			{
				return (RecipientType)this[ReducedRecipientSchema.RecipientType];
			}
		}

		// Token: 0x17002256 RID: 8790
		// (get) Token: 0x06006026 RID: 24614 RVA: 0x001472B4 File Offset: 0x001454B4
		// (set) Token: 0x06006027 RID: 24615 RVA: 0x001472C6 File Offset: 0x001454C6
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[ReducedRecipientSchema.RecipientTypeDetails];
			}
			internal set
			{
				this[ReducedRecipientSchema.RecipientTypeDetails] = value;
			}
		}

		// Token: 0x17002257 RID: 8791
		// (get) Token: 0x06006028 RID: 24616 RVA: 0x001472D9 File Offset: 0x001454D9
		public string SamAccountName
		{
			get
			{
				return (string)this[ReducedRecipientSchema.SamAccountName];
			}
		}

		// Token: 0x17002258 RID: 8792
		// (get) Token: 0x06006029 RID: 24617 RVA: 0x001472EB File Offset: 0x001454EB
		public string ServerLegacyDN
		{
			get
			{
				return (string)this[ReducedRecipientSchema.ServerLegacyDN];
			}
		}

		// Token: 0x17002259 RID: 8793
		// (get) Token: 0x0600602A RID: 24618 RVA: 0x001472FD File Offset: 0x001454FD
		public string ServerName
		{
			get
			{
				return (string)this[ReducedRecipientSchema.ServerName];
			}
		}

		// Token: 0x1700225A RID: 8794
		// (get) Token: 0x0600602B RID: 24619 RVA: 0x0014730F File Offset: 0x0014550F
		public string StateOrProvince
		{
			get
			{
				return (string)this[ReducedRecipientSchema.StateOrProvince];
			}
		}

		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x0600602C RID: 24620 RVA: 0x00147321 File Offset: 0x00145521
		public string StorageGroupName
		{
			get
			{
				return (string)this[ReducedRecipientSchema.StorageGroupName];
			}
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x0600602D RID: 24621 RVA: 0x00147333 File Offset: 0x00145533
		public string Title
		{
			get
			{
				return (string)this[ReducedRecipientSchema.Title];
			}
		}

		// Token: 0x1700225D RID: 8797
		// (get) Token: 0x0600602E RID: 24622 RVA: 0x00147345 File Offset: 0x00145545
		public bool UMEnabled
		{
			get
			{
				return (bool)this[ReducedRecipientSchema.UMEnabled];
			}
		}

		// Token: 0x1700225E RID: 8798
		// (get) Token: 0x0600602F RID: 24623 RVA: 0x00147357 File Offset: 0x00145557
		public ADObjectId UMMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[ReducedRecipientSchema.UMMailboxPolicy];
			}
		}

		// Token: 0x1700225F RID: 8799
		// (get) Token: 0x06006030 RID: 24624 RVA: 0x00147369 File Offset: 0x00145569
		public ADObjectId UMRecipientDialPlanId
		{
			get
			{
				return (ADObjectId)this[ReducedRecipientSchema.UMRecipientDialPlanId];
			}
		}

		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x06006031 RID: 24625 RVA: 0x0014737B File Offset: 0x0014557B
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return (SmtpAddress)this[ReducedRecipientSchema.WindowsLiveID];
			}
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x06006032 RID: 24626 RVA: 0x0014738D File Offset: 0x0014558D
		public bool HasActiveSyncDevicePartnership
		{
			get
			{
				return (bool)this[ReducedRecipientSchema.HasActiveSyncDevicePartnership];
			}
		}

		// Token: 0x17002262 RID: 8802
		// (get) Token: 0x06006033 RID: 24627 RVA: 0x0014739F File Offset: 0x0014559F
		public MultiValuedProperty<ADObjectId> AddressListMembership
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ReducedRecipientSchema.AddressListMembership];
			}
		}

		// Token: 0x17002263 RID: 8803
		// (get) Token: 0x06006034 RID: 24628 RVA: 0x001473B1 File Offset: 0x001455B1
		// (set) Token: 0x06006035 RID: 24629 RVA: 0x001473CD File Offset: 0x001455CD
		public ADObjectId OwaMailboxPolicy
		{
			get
			{
				return this.owaMailboxPolicy ?? ((ADObjectId)this[ReducedRecipientSchema.OwaMailboxPolicy]);
			}
			internal set
			{
				this.owaMailboxPolicy = value;
			}
		}

		// Token: 0x17002264 RID: 8804
		// (get) Token: 0x06006036 RID: 24630 RVA: 0x001473D6 File Offset: 0x001455D6
		// (set) Token: 0x06006037 RID: 24631 RVA: 0x001473F2 File Offset: 0x001455F2
		public ADObjectId AddressBookPolicy
		{
			get
			{
				return this.addressBookPolicy ?? ((ADObjectId)this[ReducedRecipientSchema.AddressBookPolicy]);
			}
			internal set
			{
				this.addressBookPolicy = value;
			}
		}

		// Token: 0x17002265 RID: 8805
		// (get) Token: 0x06006038 RID: 24632 RVA: 0x001473FB File Offset: 0x001455FB
		// (set) Token: 0x06006039 RID: 24633 RVA: 0x00147417 File Offset: 0x00145617
		public ADObjectId SharingPolicy
		{
			get
			{
				return this.sharingPolicy ?? ((ADObjectId)this[ReducedRecipientSchema.SharingPolicy]);
			}
			internal set
			{
				this.sharingPolicy = value;
			}
		}

		// Token: 0x17002266 RID: 8806
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x00147420 File Offset: 0x00145620
		// (set) Token: 0x0600603B RID: 24635 RVA: 0x0014743C File Offset: 0x0014563C
		public ADObjectId RetentionPolicy
		{
			get
			{
				return this.retentionPolicy ?? ((ADObjectId)this[ReducedRecipientSchema.RetentionPolicy]);
			}
			internal set
			{
				this.retentionPolicy = value;
			}
		}

		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x0600603C RID: 24636 RVA: 0x00147445 File Offset: 0x00145645
		// (set) Token: 0x0600603D RID: 24637 RVA: 0x00147457 File Offset: 0x00145657
		public bool ShouldUseDefaultRetentionPolicy
		{
			get
			{
				return (bool)this[ReducedRecipientSchema.ShouldUseDefaultRetentionPolicy];
			}
			internal set
			{
				this[ReducedRecipientSchema.ShouldUseDefaultRetentionPolicy] = value;
			}
		}

		// Token: 0x17002268 RID: 8808
		// (get) Token: 0x0600603E RID: 24638 RVA: 0x0014746A File Offset: 0x0014566A
		public ADObjectId MailboxMoveTargetMDB
		{
			get
			{
				return (ADObjectId)this[ReducedRecipientSchema.MailboxMoveTargetMDB];
			}
		}

		// Token: 0x17002269 RID: 8809
		// (get) Token: 0x0600603F RID: 24639 RVA: 0x0014747C File Offset: 0x0014567C
		public ADObjectId MailboxMoveSourceMDB
		{
			get
			{
				return (ADObjectId)this[ReducedRecipientSchema.MailboxMoveSourceMDB];
			}
		}

		// Token: 0x1700226A RID: 8810
		// (get) Token: 0x06006040 RID: 24640 RVA: 0x0014748E File Offset: 0x0014568E
		public RequestFlags MailboxMoveFlags
		{
			get
			{
				return (RequestFlags)this[ReducedRecipientSchema.MailboxMoveFlags];
			}
		}

		// Token: 0x1700226B RID: 8811
		// (get) Token: 0x06006041 RID: 24641 RVA: 0x001474A0 File Offset: 0x001456A0
		public string MailboxMoveRemoteHostName
		{
			get
			{
				return (string)this[ReducedRecipientSchema.MailboxMoveRemoteHostName];
			}
		}

		// Token: 0x1700226C RID: 8812
		// (get) Token: 0x06006042 RID: 24642 RVA: 0x001474B2 File Offset: 0x001456B2
		public string MailboxMoveBatchName
		{
			get
			{
				return (string)this[ReducedRecipientSchema.MailboxMoveBatchName];
			}
		}

		// Token: 0x1700226D RID: 8813
		// (get) Token: 0x06006043 RID: 24643 RVA: 0x001474C4 File Offset: 0x001456C4
		public RequestStatus MailboxMoveStatus
		{
			get
			{
				return (RequestStatus)this[ReducedRecipientSchema.MailboxMoveStatus];
			}
		}

		// Token: 0x1700226E RID: 8814
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x001474D6 File Offset: 0x001456D6
		public string MailboxRelease
		{
			get
			{
				return (string)this[ReducedRecipientSchema.MailboxRelease];
			}
		}

		// Token: 0x1700226F RID: 8815
		// (get) Token: 0x06006045 RID: 24645 RVA: 0x001474E8 File Offset: 0x001456E8
		public string ArchiveRelease
		{
			get
			{
				return (string)this[ReducedRecipientSchema.ArchiveRelease];
			}
		}

		// Token: 0x17002270 RID: 8816
		// (get) Token: 0x06006046 RID: 24646 RVA: 0x001474FA File Offset: 0x001456FA
		public bool IsValidSecurityPrincipal
		{
			get
			{
				return (bool)this[ReducedRecipientSchema.IsValidSecurityPrincipal];
			}
		}

		// Token: 0x17002271 RID: 8817
		// (get) Token: 0x06006047 RID: 24647 RVA: 0x0014750C File Offset: 0x0014570C
		public bool LitigationHoldEnabled
		{
			get
			{
				return (bool)this[ReducedRecipientSchema.LitigationHoldEnabled];
			}
		}

		// Token: 0x17002272 RID: 8818
		// (get) Token: 0x06006048 RID: 24648 RVA: 0x0014751E File Offset: 0x0014571E
		// (set) Token: 0x06006049 RID: 24649 RVA: 0x00147530 File Offset: 0x00145730
		public MultiValuedProperty<Capability> Capabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)this[ReducedRecipientSchema.Capabilities];
			}
			private set
			{
				this[RoleGroupSchema.Capabilities] = value;
			}
		}

		// Token: 0x17002273 RID: 8819
		// (get) Token: 0x0600604A RID: 24650 RVA: 0x0014753E File Offset: 0x0014573E
		public ArchiveState ArchiveState
		{
			get
			{
				return (ArchiveState)this[ReducedRecipientSchema.ArchiveState];
			}
		}

		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x0600604B RID: 24651 RVA: 0x00147550 File Offset: 0x00145750
		public bool? SKUAssigned
		{
			get
			{
				return (bool?)this[ReducedRecipientSchema.SKUAssigned];
			}
		}

		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x0600604C RID: 24652 RVA: 0x00147562 File Offset: 0x00145762
		public DateTime? WhenMailboxCreated
		{
			get
			{
				return (DateTime?)this[ReducedRecipientSchema.WhenMailboxCreated];
			}
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x0600604D RID: 24653 RVA: 0x00147574 File Offset: 0x00145774
		public CountryInfo UsageLocation
		{
			get
			{
				return (CountryInfo)this[ReducedRecipientSchema.UsageLocation];
			}
		}

		// Token: 0x17002277 RID: 8823
		// (get) Token: 0x0600604E RID: 24654 RVA: 0x00147586 File Offset: 0x00145786
		// (set) Token: 0x0600604F RID: 24655 RVA: 0x00147598 File Offset: 0x00145798
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[ReducedRecipientSchema.ExchangeGuid];
			}
			internal set
			{
				this[ReducedRecipientSchema.ExchangeGuid] = value;
			}
		}

		// Token: 0x17002278 RID: 8824
		// (get) Token: 0x06006050 RID: 24656 RVA: 0x001475AB File Offset: 0x001457AB
		// (set) Token: 0x06006051 RID: 24657 RVA: 0x001475BD File Offset: 0x001457BD
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return (ArchiveStatusFlags)this[ReducedRecipientSchema.ArchiveStatus];
			}
			internal set
			{
				this[ReducedRecipientSchema.ArchiveStatus] = value;
			}
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x001475D0 File Offset: 0x001457D0
		internal void PopulateCapabilitiesProperty()
		{
			this.Capabilities = CapabilityIdentifierEvaluatorFactory.GetCapabilities(this);
		}

		// Token: 0x0400405A RID: 16474
		private static Dictionary<PropertySet, ADPropertyDefinition[]> properties;

		// Token: 0x0400405B RID: 16475
		private static ReducedRecipientSchema schema = ObjectSchema.GetInstance<ReducedRecipientSchema>();

		// Token: 0x0400405C RID: 16476
		private ADObjectId addressBookPolicy;

		// Token: 0x0400405D RID: 16477
		private ADObjectId activeSyncMailboxPolicy;

		// Token: 0x0400405E RID: 16478
		private ADObjectId owaMailboxPolicy;

		// Token: 0x0400405F RID: 16479
		private ADObjectId sharingPolicy;

		// Token: 0x04004060 RID: 16480
		private ADObjectId retentionPolicy;
	}
}
