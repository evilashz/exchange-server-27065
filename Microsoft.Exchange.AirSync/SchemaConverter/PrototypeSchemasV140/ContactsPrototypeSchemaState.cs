using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV140
{
	// Token: 0x020001D2 RID: 466
	internal class ContactsPrototypeSchemaState : AirSyncXsoSchemaState
	{
		// Token: 0x0600130E RID: 4878 RVA: 0x0006A1F5 File Offset: 0x000683F5
		public ContactsPrototypeSchemaState() : base(ContactsPrototypeSchemaState.supportedClassFilter)
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x0006A20F File Offset: 0x0006840F
		internal static QueryFilter SupportedClassQueryFilter
		{
			get
			{
				return ContactsPrototypeSchemaState.supportedClassFilter;
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0006A218 File Offset: 0x00068418
		private void CreatePropertyConversionTable()
		{
			string xmlNodeNamespace = "Contacts:";
			string xmlNodeNamespace2 = "AirSyncBase:";
			string xmlNodeNamespace3 = "Contacts2:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "Anniversary", AirSyncDateFormat.Punctuate, false),
				new XsoUtcDateTimeProperty(ContactSchema.WeddingAnniversary)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncUtcDateTimeProperty(xmlNodeNamespace, "Birthday", AirSyncDateFormat.Punctuate, false),
				new XsoUtcDateTimeProperty(ContactSchema.Birthday)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncContent14Property(xmlNodeNamespace2, "Body", false),
				new XsoContent14Property()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "WebPage", false),
				new XsoStringProperty(ContactSchema.BusinessHomePage)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncMultiValuedStringProperty(xmlNodeNamespace, "Children", "Child", false),
				new XsoMultiValuedStringProperty(ContactSchema.Children)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "BusinessAddressCountry", false),
				new XsoStringProperty(ContactSchema.WorkAddressCountry)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Department", false),
				new XsoStringProperty(ContactSchema.Department)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Email1Address", false),
				new XsoEmailProperty(EmailAddressIndex.Email1)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Email2Address", false),
				new XsoEmailProperty(EmailAddressIndex.Email2)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Email3Address", false),
				new XsoEmailProperty(EmailAddressIndex.Email3)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "BusinessFaxNumber", false),
				new XsoStringProperty(ContactSchema.WorkFax)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "FileAs", false),
				new XsoStringProperty(ContactBaseSchema.FileAs)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "FirstName", false),
				new XsoStringProperty(ContactSchema.GivenName)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "HomeAddressCity", false),
				new XsoStringProperty(ContactSchema.HomeCity)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "HomeAddressCountry", false),
				new XsoStringProperty(ContactSchema.HomeCountry)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "HomeFaxNumber", false),
				new XsoStringProperty(ContactSchema.HomeFax)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "HomePhoneNumber", false),
				new XsoStringProperty(ContactSchema.HomePhone)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Home2PhoneNumber", false),
				new XsoStringProperty(ContactSchema.HomePhone2)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "HomeAddressPostalCode", false),
				new XsoStringProperty(ContactSchema.HomePostalCode)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "HomeAddressState", false),
				new XsoStringProperty(ContactSchema.HomeState)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "HomeAddressStreet", false),
				new XsoStringProperty(ContactSchema.HomeStreet)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "BusinessAddressCity", false),
				new XsoStringProperty(ContactSchema.WorkAddressCity)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "MiddleName", false),
				new XsoStringProperty(ContactSchema.MiddleName)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "MobilePhoneNumber", false),
				new XsoStringProperty(ContactSchema.MobilePhone)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Suffix", false),
				new XsoStringProperty(ContactSchema.Generation)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "CompanyName", false),
				new XsoStringProperty(ContactSchema.CompanyName)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "OtherAddressCity", false),
				new XsoStringProperty(ContactSchema.OtherCity)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "OtherAddressCountry", false),
				new XsoStringProperty(ContactSchema.OtherCountry)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "CarPhoneNumber", false),
				new XsoStringProperty(ContactSchema.OtherMobile)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "OtherAddressPostalCode", false),
				new XsoStringProperty(ContactSchema.OtherPostalCode)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "OtherAddressState", false),
				new XsoStringProperty(ContactSchema.OtherState)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "OtherAddressStreet", false),
				new XsoStringProperty(ContactSchema.OtherStreet)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "PagerNumber", false),
				new XsoStringProperty(ContactSchema.Pager)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Title", false),
				new XsoStringProperty(ContactSchema.DisplayNamePrefix)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "BusinessAddressPostalCode", false),
				new XsoStringProperty(ContactSchema.WorkAddressPostalCode)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "AssistantName", false),
				new XsoStringProperty(ContactSchema.AssistantName)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "AssistantPhoneNumber", false),
				new XsoStringProperty(ContactSchema.AssistantPhoneNumber)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "LastName", false),
				new XsoStringProperty(ContactSchema.Surname)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Spouse", false),
				new XsoStringProperty(ContactSchema.SpouseName)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "BusinessAddressState", false),
				new XsoStringProperty(ContactSchema.WorkAddressState)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "BusinessAddressStreet", false),
				new XsoStringProperty(ContactSchema.WorkAddressStreet)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "BusinessPhoneNumber", false),
				new XsoStringProperty(ContactSchema.BusinessPhoneNumber)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Business2PhoneNumber", false),
				new XsoStringProperty(ContactSchema.BusinessPhoneNumber2)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "JobTitle", false),
				new XsoStringProperty(ContactSchema.Title)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "YomiFirstName", false),
				new XsoStringProperty(ContactSchema.YomiFirstName)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "YomiLastName", false),
				new XsoStringProperty(ContactSchema.YomiLastName)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "YomiCompanyName", false),
				new XsoStringProperty(ContactSchema.YomiCompany)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "OfficeLocation", false),
				new XsoStringProperty(ContactSchema.OfficeLocation)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "RadioPhoneNumber", false),
				new XsoStringProperty(ContactSchema.RadioPhone)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncMultiValuedStringProperty(xmlNodeNamespace, "Categories", "Category", false),
				new XsoCategoriesProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncPictureProperty(xmlNodeNamespace, "Picture", false),
				new XsoPictureProperty()
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "CustomerId", false),
				new XsoStringProperty(ContactsPrototypeSchemaState.customerId)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "GovernmentId", false),
				new XsoStringProperty(ContactsPrototypeSchemaState.governmentIdNumber)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "IMAddress", false),
				new XsoStringProperty(ContactSchema.IMAddress)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "ManagerName", false),
				new XsoManagerProperty(ContactsPrototypeSchemaState.managerName)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "CompanyMainPhone", false),
				new XsoStringProperty(ContactsPrototypeSchemaState.companyMainPhoneNumber)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "AccountName", false),
				new XsoStringProperty(ContactsPrototypeSchemaState.account)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "NickName", false),
				new XsoStringProperty(ContactsPrototypeSchemaState.nickname)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "MMS", false),
				new XsoStringProperty(ContactsPrototypeSchemaState.mms)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "IMAddress2", false),
				new XsoStringProperty(ContactsPrototypeSchemaState.imaddress2)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace3, "IMAddress3", false),
				new XsoStringProperty(ContactsPrototypeSchemaState.imaddress3)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace2, "NativeBodyType", false),
				new XsoNativeBodyProperty()
			});
		}

		// Token: 0x04000BA2 RID: 2978
		private static readonly Guid psetidAirSync = new Guid("{71035549-0739-4DCB-9163-00F0580DBBDF}");

		// Token: 0x04000BA3 RID: 2979
		private static readonly string[] supportedClassTypes = new string[]
		{
			"IPM.CONTACT"
		};

		// Token: 0x04000BA4 RID: 2980
		private static readonly QueryFilter supportedClassFilter = AirSyncXsoSchemaState.BuildMessageClassFilter(ContactsPrototypeSchemaState.supportedClassTypes);

		// Token: 0x04000BA5 RID: 2981
		private static StorePropertyDefinition imaddress2 = GuidNamePropertyDefinition.CreateCustom("IMAddress2", typeof(string), ContactsPrototypeSchemaState.psetidAirSync, "AirSync:IMAddress2", PropertyFlags.None);

		// Token: 0x04000BA6 RID: 2982
		private static StorePropertyDefinition imaddress3 = GuidNamePropertyDefinition.CreateCustom("IMAddress3", typeof(string), ContactsPrototypeSchemaState.psetidAirSync, "AirSync:IMAddress3", PropertyFlags.None);

		// Token: 0x04000BA7 RID: 2983
		private static StorePropertyDefinition mms = GuidNamePropertyDefinition.CreateCustom("MMS", typeof(string), ContactsPrototypeSchemaState.psetidAirSync, "AirSync:MMS", PropertyFlags.None);

		// Token: 0x04000BA8 RID: 2984
		private static StorePropertyDefinition customerId = PropertyTagPropertyDefinition.CreateCustom("CustomerId", 977928223U, PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04000BA9 RID: 2985
		private static StorePropertyDefinition governmentIdNumber = PropertyTagPropertyDefinition.CreateCustom("GovernmentIdNumber", 973537311U, PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04000BAA RID: 2986
		private static StorePropertyDefinition managerName = PropertyTagPropertyDefinition.CreateCustom("ManagerName", 978190367U, PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04000BAB RID: 2987
		private static StorePropertyDefinition companyMainPhoneNumber = PropertyTagPropertyDefinition.CreateCustom("CompanyMainPhoneNumber", 978780191U, PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04000BAC RID: 2988
		private static StorePropertyDefinition account = PropertyTagPropertyDefinition.CreateCustom("Account", 973078559U, PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04000BAD RID: 2989
		private static StorePropertyDefinition nickname = PropertyTagPropertyDefinition.CreateCustom("Nickname", 978255903U, PropertyFlags.None, new PropertyDefinitionConstraint[0]);
	}
}
