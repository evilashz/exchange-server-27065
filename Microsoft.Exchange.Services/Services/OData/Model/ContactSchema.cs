using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E61 RID: 3681
	internal class ContactSchema : ItemSchema
	{
		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06005F7F RID: 24447 RVA: 0x00129568 File Offset: 0x00127768
		public new static ContactSchema SchemaInstance
		{
			get
			{
				return ContactSchema.ContactSchemaInstance.Member;
			}
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06005F80 RID: 24448 RVA: 0x00129574 File Offset: 0x00127774
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return Contact.EdmEntityType;
			}
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06005F81 RID: 24449 RVA: 0x0012957B File Offset: 0x0012777B
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return ContactSchema.DeclaredContactProperties;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06005F82 RID: 24450 RVA: 0x00129582 File Offset: 0x00127782
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return ContactSchema.AllContactProperties;
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06005F83 RID: 24451 RVA: 0x00129589 File Offset: 0x00127789
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return ContactSchema.DefaultContactProperties;
			}
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06005F84 RID: 24452 RVA: 0x00129590 File Offset: 0x00127790
		public override ReadOnlyCollection<PropertyDefinition> MandatoryCreationProperties
		{
			get
			{
				return ContactSchema.MandatoryContactCreationProperties;
			}
		}

		// Token: 0x06005F86 RID: 24454 RVA: 0x00129724 File Offset: 0x00127924
		// Note: this type is marked as 'beforefieldinit'.
		static ContactSchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("ParentFolderId", typeof(string));
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetString(true);
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider = new SimpleEwsPropertyProvider(ItemSchema.ParentFolderId);
			simpleEwsPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				FolderId folderId = s[sp] as FolderId;
				if (folderId != null && !string.IsNullOrEmpty(folderId.Id))
				{
					e[ep] = EwsIdConverter.EwsIdToODataId(folderId.Id);
				}
			};
			propertyDefinition2.EwsPropertyProvider = simpleEwsPropertyProvider;
			ContactSchema.ParentFolderId = propertyDefinition;
			ContactSchema.Birthday = new PropertyDefinition("Birthday", typeof(DateTime))
			{
				EdmType = EdmCoreModel.Instance.GetDateTimeOffset(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new DateTimePropertyProvider(ContactSchema.Birthday)
			};
			ContactSchema.FileAs = new PropertyDefinition("FileAs", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.FileAs)
			};
			ContactSchema.DisplayName = new PropertyDefinition("DisplayName", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.DisplayName)
			};
			ContactSchema.GivenName = new PropertyDefinition("GivenName", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.GivenName)
			};
			ContactSchema.Initials = new PropertyDefinition("Initials", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.Initials)
			};
			ContactSchema.MiddleName = new PropertyDefinition("MiddleName", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.MiddleName)
			};
			ContactSchema.NickName = new PropertyDefinition("NickName", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.Nickname)
			};
			ContactSchema.Surname = new PropertyDefinition("Surname", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.Surname)
			};
			PropertyDefinition propertyDefinition3 = new PropertyDefinition("Title", typeof(string));
			propertyDefinition3.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition3.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition4 = propertyDefinition3;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider2 = new SimpleEwsPropertyProvider(ContactSchema.CompleteName);
			simpleEwsPropertyProvider2.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				CompleteNameType valueOrDefault = s.GetValueOrDefault<CompleteNameType>(sp);
				if (valueOrDefault != null)
				{
					e[ep] = valueOrDefault.Title;
				}
			};
			propertyDefinition4.EwsPropertyProvider = simpleEwsPropertyProvider2;
			ContactSchema.Title = propertyDefinition3;
			PropertyDefinition propertyDefinition5 = new PropertyDefinition("YomiFirstName", typeof(string));
			propertyDefinition5.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition5.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition6 = propertyDefinition5;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider3 = new SimpleEwsPropertyProvider(ContactSchema.CompleteName);
			simpleEwsPropertyProvider3.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				CompleteNameType valueOrDefault = s.GetValueOrDefault<CompleteNameType>(sp);
				if (valueOrDefault != null)
				{
					e[ep] = valueOrDefault.YomiFirstName;
				}
			};
			propertyDefinition6.EwsPropertyProvider = simpleEwsPropertyProvider3;
			ContactSchema.YomiFirstName = propertyDefinition5;
			PropertyDefinition propertyDefinition7 = new PropertyDefinition("YomiLastName", typeof(string));
			propertyDefinition7.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition7.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition8 = propertyDefinition7;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider4 = new SimpleEwsPropertyProvider(ContactSchema.CompleteName);
			simpleEwsPropertyProvider4.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				CompleteNameType valueOrDefault = s.GetValueOrDefault<CompleteNameType>(sp);
				if (valueOrDefault != null)
				{
					e[ep] = s.GetValueOrDefault<CompleteNameType>(sp).YomiLastName;
				}
			};
			propertyDefinition8.EwsPropertyProvider = simpleEwsPropertyProvider4;
			ContactSchema.YomiLastName = propertyDefinition7;
			ContactSchema.Generation = new PropertyDefinition("Generation", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.Generation)
			};
			PropertyDefinition propertyDefinition9 = new PropertyDefinition("EmailAddress1", typeof(string));
			propertyDefinition9.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition9.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition10 = propertyDefinition9;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider5 = new SimpleEwsPropertyProvider(ContactSchema.EmailAddressEmailAddress1);
			simpleEwsPropertyProvider5.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				EmailAddressDictionaryEntryType valueOrDefault = s.GetValueOrDefault<EmailAddressDictionaryEntryType>(sp);
				e[ep] = ((valueOrDefault != null) ? valueOrDefault.Value : null);
			};
			simpleEwsPropertyProvider5.Setter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				s[sp] = new EmailAddressDictionaryEntryType(EmailAddressKeyType.EmailAddress1, e[ep] as string);
			};
			propertyDefinition10.EwsPropertyProvider = simpleEwsPropertyProvider5;
			ContactSchema.EmailAddress1 = propertyDefinition9;
			PropertyDefinition propertyDefinition11 = new PropertyDefinition("EmailAddress2", typeof(string));
			propertyDefinition11.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition11.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition12 = propertyDefinition11;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider6 = new SimpleEwsPropertyProvider(ContactSchema.EmailAddressEmailAddress2);
			simpleEwsPropertyProvider6.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				EmailAddressDictionaryEntryType valueOrDefault = s.GetValueOrDefault<EmailAddressDictionaryEntryType>(sp);
				e[ep] = ((valueOrDefault != null) ? valueOrDefault.Value : null);
			};
			simpleEwsPropertyProvider6.Setter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				s[sp] = new EmailAddressDictionaryEntryType(EmailAddressKeyType.EmailAddress2, e[ep] as string);
			};
			propertyDefinition12.EwsPropertyProvider = simpleEwsPropertyProvider6;
			ContactSchema.EmailAddress2 = propertyDefinition11;
			PropertyDefinition propertyDefinition13 = new PropertyDefinition("EmailAddress3", typeof(string));
			propertyDefinition13.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition13.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition14 = propertyDefinition13;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider7 = new SimpleEwsPropertyProvider(ContactSchema.EmailAddressEmailAddress3);
			simpleEwsPropertyProvider7.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				EmailAddressDictionaryEntryType valueOrDefault = s.GetValueOrDefault<EmailAddressDictionaryEntryType>(sp);
				e[ep] = ((valueOrDefault != null) ? valueOrDefault.Value : null);
			};
			simpleEwsPropertyProvider7.Setter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				s[sp] = new EmailAddressDictionaryEntryType(EmailAddressKeyType.EmailAddress3, e[ep] as string);
			};
			propertyDefinition14.EwsPropertyProvider = simpleEwsPropertyProvider7;
			ContactSchema.EmailAddress3 = propertyDefinition13;
			ContactSchema.ImAddress1 = new PropertyDefinition("ImAddress1", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.ImAddressImAddress1)
			};
			ContactSchema.ImAddress2 = new PropertyDefinition("ImAddress2", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.ImAddressImAddress2)
			};
			ContactSchema.ImAddress3 = new PropertyDefinition("ImAddress3", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.ImAddressImAddress3)
			};
			ContactSchema.JobTitle = new PropertyDefinition("JobTitle", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.JobTitle)
			};
			ContactSchema.CompanyName = new PropertyDefinition("CompanyName", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.CompanyName)
			};
			ContactSchema.Department = new PropertyDefinition("Department", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.Department)
			};
			ContactSchema.OfficeLocation = new PropertyDefinition("OfficeLocation", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.OfficeLocation)
			};
			ContactSchema.Profession = new PropertyDefinition("Profession", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.Profession)
			};
			ContactSchema.BusinessHomePage = new PropertyDefinition("BusinessHomePage", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.BusinessHomePage)
			};
			ContactSchema.AssistantName = new PropertyDefinition("AssistantName", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.AssistantName)
			};
			ContactSchema.Manager = new PropertyDefinition("Manager", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.Manager)
			};
			ContactSchema.HomePhone1 = new PropertyDefinition("HomePhone1", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.PhoneNumberHomePhone)
			};
			ContactSchema.HomePhone2 = new PropertyDefinition("HomePhone2", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.PhoneNumberHomePhone2)
			};
			ContactSchema.MobilePhone1 = new PropertyDefinition("MobilePhone1", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.PhoneNumberMobilePhone)
			};
			ContactSchema.BusinessPhone1 = new PropertyDefinition("BusinessPhone1", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.PhoneNumberBusinessPhone)
			};
			ContactSchema.BusinessPhone2 = new PropertyDefinition("BusinessPhone2", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.PhoneNumberBusinessPhone2)
			};
			ContactSchema.OtherPhone = new PropertyDefinition("OtherPhone", typeof(string))
			{
				EdmType = EdmCoreModel.Instance.GetString(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ContactSchema.PhoneNumberOtherTelephone)
			};
			ContactSchema.HomeAddress = new PropertyDefinition("HomeAddress", typeof(string))
			{
				EdmType = new EdmComplexTypeReference(PhysicalAddress.EdmComplexType.Member, true),
				Flags = (PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new HomeAddressPropertyProvider(),
				ODataPropertyValueConverter = new PhysicalAddressODataConverter()
			};
			ContactSchema.BusinessAddress = new PropertyDefinition("BusinessAddress", typeof(string))
			{
				EdmType = new EdmComplexTypeReference(PhysicalAddress.EdmComplexType.Member, true),
				Flags = (PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new BusinessAddressPropertyProvider(),
				ODataPropertyValueConverter = new PhysicalAddressODataConverter()
			};
			ContactSchema.OtherAddress = new PropertyDefinition("OtherAddress", typeof(string))
			{
				EdmType = new EdmComplexTypeReference(PhysicalAddress.EdmComplexType.Member, true),
				Flags = (PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new OtherAddressPropertyProvider(),
				ODataPropertyValueConverter = new PhysicalAddressODataConverter()
			};
			ContactSchema.DeclaredContactProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				ContactSchema.ParentFolderId,
				ContactSchema.Birthday,
				ContactSchema.FileAs,
				ContactSchema.DisplayName,
				ContactSchema.GivenName,
				ContactSchema.Initials,
				ContactSchema.MiddleName,
				ContactSchema.NickName,
				ContactSchema.Surname,
				ContactSchema.Title,
				ContactSchema.Generation,
				ContactSchema.EmailAddress1,
				ContactSchema.EmailAddress2,
				ContactSchema.EmailAddress3,
				ContactSchema.ImAddress1,
				ContactSchema.ImAddress2,
				ContactSchema.ImAddress3,
				ContactSchema.JobTitle,
				ContactSchema.CompanyName,
				ContactSchema.Department,
				ContactSchema.OfficeLocation,
				ContactSchema.Profession,
				ContactSchema.BusinessHomePage,
				ContactSchema.AssistantName,
				ContactSchema.Manager,
				ContactSchema.HomePhone1,
				ContactSchema.HomePhone2,
				ContactSchema.BusinessPhone1,
				ContactSchema.BusinessPhone2,
				ContactSchema.MobilePhone1,
				ContactSchema.OtherPhone,
				ContactSchema.HomeAddress,
				ContactSchema.BusinessAddress,
				ContactSchema.OtherAddress,
				ItemSchema.DateTimeCreated,
				ItemSchema.LastModifiedTime
			});
			ContactSchema.AllContactProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(ItemSchema.AllItemProperties.Union(ContactSchema.DeclaredContactProperties)));
			ContactSchema.DefaultContactProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(ItemSchema.DefaultItemProperties)
			{
				ContactSchema.ParentFolderId,
				ContactSchema.Birthday,
				ContactSchema.FileAs,
				ContactSchema.DisplayName,
				ContactSchema.GivenName,
				ContactSchema.Initials,
				ContactSchema.MiddleName,
				ContactSchema.NickName,
				ContactSchema.Surname,
				ContactSchema.Title,
				ContactSchema.Generation,
				ContactSchema.EmailAddress1,
				ContactSchema.EmailAddress2,
				ContactSchema.EmailAddress3,
				ContactSchema.ImAddress1,
				ContactSchema.ImAddress2,
				ContactSchema.ImAddress3,
				ContactSchema.JobTitle,
				ContactSchema.CompanyName,
				ContactSchema.Department,
				ContactSchema.OfficeLocation,
				ContactSchema.Profession,
				ContactSchema.BusinessHomePage,
				ContactSchema.AssistantName,
				ContactSchema.Manager,
				ContactSchema.HomePhone1,
				ContactSchema.HomePhone2,
				ContactSchema.BusinessPhone1,
				ContactSchema.BusinessPhone2,
				ContactSchema.MobilePhone1,
				ContactSchema.OtherPhone,
				ContactSchema.HomeAddress,
				ContactSchema.BusinessAddress,
				ContactSchema.OtherAddress,
				ItemSchema.DateTimeCreated,
				ItemSchema.LastModifiedTime
			});
			ContactSchema.MandatoryContactCreationProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				ContactSchema.GivenName
			});
			ContactSchema.ContactSchemaInstance = new LazyMember<ContactSchema>(() => new ContactSchema());
		}

		// Token: 0x040033A1 RID: 13217
		public static readonly PropertyDefinition ParentFolderId;

		// Token: 0x040033A2 RID: 13218
		public static readonly PropertyDefinition Birthday;

		// Token: 0x040033A3 RID: 13219
		public static readonly PropertyDefinition FileAs;

		// Token: 0x040033A4 RID: 13220
		public static readonly PropertyDefinition DisplayName;

		// Token: 0x040033A5 RID: 13221
		public static readonly PropertyDefinition GivenName;

		// Token: 0x040033A6 RID: 13222
		public static readonly PropertyDefinition Initials;

		// Token: 0x040033A7 RID: 13223
		public static readonly PropertyDefinition MiddleName;

		// Token: 0x040033A8 RID: 13224
		public static readonly PropertyDefinition NickName;

		// Token: 0x040033A9 RID: 13225
		public static readonly PropertyDefinition Surname;

		// Token: 0x040033AA RID: 13226
		public static readonly PropertyDefinition Title;

		// Token: 0x040033AB RID: 13227
		public static readonly PropertyDefinition YomiFirstName;

		// Token: 0x040033AC RID: 13228
		public static readonly PropertyDefinition YomiLastName;

		// Token: 0x040033AD RID: 13229
		public static readonly PropertyDefinition Generation;

		// Token: 0x040033AE RID: 13230
		public static readonly PropertyDefinition EmailAddress1;

		// Token: 0x040033AF RID: 13231
		public static readonly PropertyDefinition EmailAddress2;

		// Token: 0x040033B0 RID: 13232
		public static readonly PropertyDefinition EmailAddress3;

		// Token: 0x040033B1 RID: 13233
		public static readonly PropertyDefinition ImAddress1;

		// Token: 0x040033B2 RID: 13234
		public static readonly PropertyDefinition ImAddress2;

		// Token: 0x040033B3 RID: 13235
		public static readonly PropertyDefinition ImAddress3;

		// Token: 0x040033B4 RID: 13236
		public static readonly PropertyDefinition JobTitle;

		// Token: 0x040033B5 RID: 13237
		public static readonly PropertyDefinition CompanyName;

		// Token: 0x040033B6 RID: 13238
		public static readonly PropertyDefinition Department;

		// Token: 0x040033B7 RID: 13239
		public static readonly PropertyDefinition OfficeLocation;

		// Token: 0x040033B8 RID: 13240
		public static readonly PropertyDefinition Profession;

		// Token: 0x040033B9 RID: 13241
		public static readonly PropertyDefinition BusinessHomePage;

		// Token: 0x040033BA RID: 13242
		public static readonly PropertyDefinition AssistantName;

		// Token: 0x040033BB RID: 13243
		public static readonly PropertyDefinition Manager;

		// Token: 0x040033BC RID: 13244
		public static readonly PropertyDefinition HomePhone1;

		// Token: 0x040033BD RID: 13245
		public static readonly PropertyDefinition HomePhone2;

		// Token: 0x040033BE RID: 13246
		public static readonly PropertyDefinition MobilePhone1;

		// Token: 0x040033BF RID: 13247
		public static readonly PropertyDefinition BusinessPhone1;

		// Token: 0x040033C0 RID: 13248
		public static readonly PropertyDefinition BusinessPhone2;

		// Token: 0x040033C1 RID: 13249
		public static readonly PropertyDefinition OtherPhone;

		// Token: 0x040033C2 RID: 13250
		public static readonly PropertyDefinition HomeAddress;

		// Token: 0x040033C3 RID: 13251
		public static readonly PropertyDefinition BusinessAddress;

		// Token: 0x040033C4 RID: 13252
		public static readonly PropertyDefinition OtherAddress;

		// Token: 0x040033C5 RID: 13253
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredContactProperties;

		// Token: 0x040033C6 RID: 13254
		public static readonly ReadOnlyCollection<PropertyDefinition> AllContactProperties;

		// Token: 0x040033C7 RID: 13255
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultContactProperties;

		// Token: 0x040033C8 RID: 13256
		public static readonly ReadOnlyCollection<PropertyDefinition> MandatoryContactCreationProperties;

		// Token: 0x040033C9 RID: 13257
		private static readonly LazyMember<ContactSchema> ContactSchemaInstance;
	}
}
