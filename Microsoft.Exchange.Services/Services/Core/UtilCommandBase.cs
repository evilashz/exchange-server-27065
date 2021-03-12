using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002E3 RID: 739
	internal abstract class UtilCommandBase<RequestType, ResponseType> : SingleStepServiceCommand<RequestType, ResponseType> where RequestType : BaseRequest
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x00066BF0 File Offset: 0x00064DF0
		public UtilCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00066BFA File Offset: 0x00064DFA
		protected static void ResolutionResponseAttributesToXml(XmlElement xmlResolution, int totalItemsInView, bool includesLastItemInRange)
		{
			ServiceXml.CreateAttribute(xmlResolution, "TotalItemsInView", totalItemsInView.ToString(CultureInfo.InvariantCulture));
			ServiceXml.CreateAttribute(xmlResolution, "IncludesLastItemInRange", includesLastItemInRange ? bool.TrueString.ToLowerInvariant() : bool.FalseString.ToLowerInvariant());
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00066C3C File Offset: 0x00064E3C
		protected static XmlElement CreateResponseXml(string xmlElementName, XmlDocument xmlDocument)
		{
			return ServiceXml.CreateElement(xmlDocument, xmlElementName, "http://schemas.microsoft.com/exchange/services/2006/messages");
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00066C58 File Offset: 0x00064E58
		protected void InitActiveDirectoryNameResolutionContext()
		{
			if (base.CallContext.AccessingPrincipal != null && base.CallContext.AccessingPrincipal.PreferredCultures.Any<CultureInfo>())
			{
				int lcid = base.CallContext.AccessingPrincipal.PreferredCultures.First<CultureInfo>().LCID;
			}
			this.directoryRecipientSession = base.CallContext.ADRecipientSessionContext.GetGALScopedADRecipientSession(base.CallContext.EffectiveCaller.ClientSecurityContext);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00066CCC File Offset: 0x00064ECC
		protected ContactsFolder InitStoreNameResolutionContext(BaseFolderId folderId)
		{
			ContactsFolder result;
			if (folderId != null)
			{
				IdConverter idConverter = new IdConverter(base.CallContext);
				IdAndSession idAndSession = idConverter.ConvertFolderIdToIdAndSessionReadOnly(folderId);
				if (idAndSession.GetAsStoreObjectId().ObjectType != StoreObjectType.ContactsFolder)
				{
					throw new ResolveNamesExceptionInvalidFolderType();
				}
				result = ContactsFolder.Bind(idAndSession.Session, idAndSession.Id, null);
			}
			else
			{
				this.storeMailboxSession = base.GetMailboxIdentityMailboxSession();
				result = ContactsFolder.Bind(this.storeMailboxSession, DefaultFolderType.Contacts);
			}
			return result;
		}

		// Token: 0x04000DAA RID: 3498
		protected const string XmlAttributeNameKey = "Key";

		// Token: 0x04000DAB RID: 3499
		protected const string XmlAttributeNameIncludesLastItemInRange = "IncludesLastItemInRange";

		// Token: 0x04000DAC RID: 3500
		protected const string XmlAttributeNameTotalItemsInView = "TotalItemsInView";

		// Token: 0x04000DAD RID: 3501
		protected const string XmlAttributeValueActiveDirectory = "ActiveDirectory";

		// Token: 0x04000DAE RID: 3502
		protected const string XmlAttributeValueBusiness = "Business";

		// Token: 0x04000DAF RID: 3503
		protected const string XmlAttributeValueContact = "Contact";

		// Token: 0x04000DB0 RID: 3504
		protected const string XmlAttributeValueEmailAddress = "EmailAddress";

		// Token: 0x04000DB1 RID: 3505
		protected const string XmlAttributeValueEmailAddress1 = "EmailAddress1";

		// Token: 0x04000DB2 RID: 3506
		protected const string XmlAttributeValueEmailAddress2 = "EmailAddress2";

		// Token: 0x04000DB3 RID: 3507
		protected const string XmlAttributeValueEmailAddress3 = "EmailAddress3";

		// Token: 0x04000DB4 RID: 3508
		protected const string XmlAttributeValueMailbox = "Mailbox";

		// Token: 0x04000DB5 RID: 3509
		protected const string XmlAttributeValuePublicDL = "PublicDL";

		// Token: 0x04000DB6 RID: 3510
		protected const string XmlAttributeValuePrivateDL = "PrivateDL";

		// Token: 0x04000DB7 RID: 3511
		protected const string XmlAttributeValuePublicFolder = "PublicFolder";

		// Token: 0x04000DB8 RID: 3512
		protected const string XmlElementContactSourceStoreValue = "Store";

		// Token: 0x04000DB9 RID: 3513
		protected const string XmlElementNameAssistantName = "AssistantName";

		// Token: 0x04000DBA RID: 3514
		protected const string XmlElementNameAssistantPhone = "AssistantPhone";

		// Token: 0x04000DBB RID: 3515
		protected const string XmlElementNameBusinessFax = "BusinessFax";

		// Token: 0x04000DBC RID: 3516
		protected const string XmlElementNameBusinessPhone = "BusinessPhone";

		// Token: 0x04000DBD RID: 3517
		protected const string XmlElementNameCity = "City";

		// Token: 0x04000DBE RID: 3518
		protected const string XmlElementNameCompanyName = "CompanyName";

		// Token: 0x04000DBF RID: 3519
		protected const string XmlElementNameContact = "Contact";

		// Token: 0x04000DC0 RID: 3520
		protected const string XmlElementNameContactSource = "ContactSource";

		// Token: 0x04000DC1 RID: 3521
		protected const string XmlElementNameCountryOrRegion = "CountryOrRegion";

		// Token: 0x04000DC2 RID: 3522
		protected const string XmlElementNameCulture = "Culture";

		// Token: 0x04000DC3 RID: 3523
		protected const string XmlElementNameDepartment = "Department";

		// Token: 0x04000DC4 RID: 3524
		protected const string XmlElementNameDisplayName = "DisplayName";

		// Token: 0x04000DC5 RID: 3525
		protected const string XmlElementNameDLExpansion = "DLExpansion";

		// Token: 0x04000DC6 RID: 3526
		protected const string XmlElementNameEmailAddress = "EmailAddress";

		// Token: 0x04000DC7 RID: 3527
		protected const string XmlElementNameEmailAddresses = "EmailAddresses";

		// Token: 0x04000DC8 RID: 3528
		protected const string XmlElementNameEntry = "Entry";

		// Token: 0x04000DC9 RID: 3529
		protected const string XmlElementNameExpandDL = "ExpandDL";

		// Token: 0x04000DCA RID: 3530
		protected const string XmlElementNameGivenName = "GivenName";

		// Token: 0x04000DCB RID: 3531
		protected const string XmlElementNameHomePhone = "HomePhone";

		// Token: 0x04000DCC RID: 3532
		protected const string XmlElementNameHomePhone2 = "HomePhone2";

		// Token: 0x04000DCD RID: 3533
		protected const string XmlElementNameInitials = "Initials";

		// Token: 0x04000DCE RID: 3534
		protected const string XmlElementNameItemId = "ItemId";

		// Token: 0x04000DCF RID: 3535
		protected const string XmlElementNameJobTitle = "JobTitle";

		// Token: 0x04000DD0 RID: 3536
		protected const string XmlElementNameMailbox = "Mailbox";

		// Token: 0x04000DD1 RID: 3537
		protected const string XmlElementNameMailboxType = "MailboxType";

		// Token: 0x04000DD2 RID: 3538
		protected const string XmlElementNameManager = "Manager";

		// Token: 0x04000DD3 RID: 3539
		protected const string XmlElementNameMobilePhone = "MobilePhone";

		// Token: 0x04000DD4 RID: 3540
		protected const string XmlElementNameName = "Name";

		// Token: 0x04000DD5 RID: 3541
		protected const string XmlElementNameOfficeLocation = "OfficeLocation";

		// Token: 0x04000DD6 RID: 3542
		protected const string XmlElementNameOtherFax = "OtherFax";

		// Token: 0x04000DD7 RID: 3543
		protected const string XmlElementNameOtherTelephone = "OtherTelephone";

		// Token: 0x04000DD8 RID: 3544
		protected const string XmlElementNamePager = "Pager";

		// Token: 0x04000DD9 RID: 3545
		protected const string XmlElementNamePhoneNumbers = "PhoneNumbers";

		// Token: 0x04000DDA RID: 3546
		protected const string XmlElementNamePhysicalAddresses = "PhysicalAddresses";

		// Token: 0x04000DDB RID: 3547
		protected const string XmlElementNamePostalCode = "PostalCode";

		// Token: 0x04000DDC RID: 3548
		protected const string XmlElementNameResolution = "Resolution";

		// Token: 0x04000DDD RID: 3549
		protected const string XmlElementNameResolutionSet = "ResolutionSet";

		// Token: 0x04000DDE RID: 3550
		protected const string XmlElementNameRoutingType = "RoutingType";

		// Token: 0x04000DDF RID: 3551
		protected const string XmlElementNameState = "State";

		// Token: 0x04000DE0 RID: 3552
		protected const string XmlElementNameStreet = "Street";

		// Token: 0x04000DE1 RID: 3553
		protected const string XmlElementNameSurname = "Surname";

		// Token: 0x04000DE2 RID: 3554
		protected const string XmlElementNameDirectoryId = "DirectoryId";

		// Token: 0x04000DE3 RID: 3555
		protected const string XmlElementNamePhoto = "Photo";

		// Token: 0x04000DE4 RID: 3556
		protected const string XmlElementNameHasPicture = "HasPicture";

		// Token: 0x04000DE5 RID: 3557
		protected const string XmlElementNameUserSMIMECertificate = "UserSMIMECertificate";

		// Token: 0x04000DE6 RID: 3558
		protected const string XmlElementNameMSExchangeCertificate = "MSExchangeCertificate";

		// Token: 0x04000DE7 RID: 3559
		protected const string XmlElementNameBase64Binary = "Base64Binary";

		// Token: 0x04000DE8 RID: 3560
		protected const string XmlElementNameNotes = "Notes";

		// Token: 0x04000DE9 RID: 3561
		protected const string XmlElementNameAlias = "Alias";

		// Token: 0x04000DEA RID: 3562
		protected const string XmlElementNamePhoneticFullName = "PhoneticFullName";

		// Token: 0x04000DEB RID: 3563
		protected const string XmlElementNamePhoneticFirstName = "PhoneticFirstName";

		// Token: 0x04000DEC RID: 3564
		protected const string XmlElementNamePhoneticLastName = "PhoneticLastName";

		// Token: 0x04000DED RID: 3565
		protected const string XmlElementNameDirectReports = "DirectReports";

		// Token: 0x04000DEE RID: 3566
		protected const string XmlElementNameManagerMailbox = "ManagerMailbox";

		// Token: 0x04000DEF RID: 3567
		protected const int DefaultMaxAmbigiousResults = 100;

		// Token: 0x04000DF0 RID: 3568
		protected static readonly SortBy DirectorySortByName = new SortBy(ADObjectSchema.Name, SortOrder.Ascending);

		// Token: 0x04000DF1 RID: 3569
		protected bool includesLastItemInRange;

		// Token: 0x04000DF2 RID: 3570
		protected IRecipientSession directoryRecipientSession;

		// Token: 0x04000DF3 RID: 3571
		private MailboxSession storeMailboxSession;
	}
}
