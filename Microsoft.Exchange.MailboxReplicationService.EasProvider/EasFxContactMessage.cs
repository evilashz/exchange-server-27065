using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Eas.Model.Response.AirSync;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000012 RID: 18
	internal class EasFxContactMessage : IMessage, IDisposable
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00007025 File Offset: 0x00005225
		public EasFxContactMessage(MessageRec messageRec)
		{
			ArgumentValidator.ThrowIfNull("messageRec", messageRec);
			this.propertyBag = EasFxContactMessage.CreatePropertyBag(messageRec);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00007044 File Offset: 0x00005244
		IPropertyBag IMessage.PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000704C File Offset: 0x0000524C
		bool IMessage.IsAssociated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000070F0 File Offset: 0x000052F0
		IEnumerable<IRecipient> IMessage.GetRecipients()
		{
			yield break;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000710D File Offset: 0x0000530D
		IRecipient IMessage.CreateRecipient()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007114 File Offset: 0x00005314
		void IMessage.RemoveRecipient(int rowId)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000071BC File Offset: 0x000053BC
		IEnumerable<IAttachmentHandle> IMessage.GetAttachments()
		{
			yield break;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000071D9 File Offset: 0x000053D9
		IAttachment IMessage.CreateAttachment()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000071E0 File Offset: 0x000053E0
		void IMessage.Save()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000071E7 File Offset: 0x000053E7
		void IMessage.SetLongTermId(StoreLongTermId longTermId)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000071EE File Offset: 0x000053EE
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000071F0 File Offset: 0x000053F0
		internal static List<PropValueData> GetContactProperties(ApplicationData applicationData)
		{
			List<PropValueData> list = new List<PropValueData>();
			list.Add(new PropValueData(PropTag.PartnerNetworkId, "outlook.com"));
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.WeddingAnniversary, applicationData.Birthday);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.Assistant, applicationData.AssistantName);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.AssistantTelephoneNumber, applicationData.AssistantPhoneNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.Birthday, applicationData.Birthday);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.Business2TelephoneNumber, applicationData.Business2PhoneNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.WorkAddressCity, applicationData.BusinessAddressCity);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.BusinessTelephoneNumber, applicationData.BusinessPhoneNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.LegacyWebPage, applicationData.WebPage);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.WorkAddressCountry, applicationData.BusinessAddressCountry);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.DepartmentName, applicationData.Department);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.Email1EmailAddress, applicationData.Email1Address);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.Email2EmailAddress, applicationData.Email2Address);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.Email3EmailAddress, applicationData.Email3Address);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.BusinessFaxNumber, applicationData.BusinessFaxNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.FileAsStringInternal, applicationData.FileAs);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.NormalizedSubject, applicationData.FileAs);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.GivenName, applicationData.FirstName);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.Account, applicationData.FirstName);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.MiddleName, applicationData.MiddleName);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.HomeAddressCity, applicationData.HomeAddressCity);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.HomeAddressCountry, applicationData.HomeAddressCountry);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.HomeFaxNumber, applicationData.HomeFaxNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.HomeTelephoneNumber, applicationData.HomePhoneNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.Home2TelephoneNumber, applicationData.Home2PhoneNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.HomeAddressPostalCode, applicationData.HomeAddressPostalCode);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.HomeAddressStateOrProvince, applicationData.HomeAddressState);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.HomeAddressStreet, applicationData.HomeAddressStreet);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.MobileTelephoneNumber, applicationData.MobilePhoneNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.CompanyName, applicationData.CompanyName);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.OtherAddressCity, applicationData.OtherAddressCity);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.OtherAddressCountry, applicationData.OtherAddressCountry);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.CarTelephoneNumber, applicationData.CarPhoneNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.OtherAddressPostalCode, applicationData.OtherAddressPostalCode);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.OtherAddressStateOrProvince, applicationData.OtherAddressState);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.OtherAddressStreet, applicationData.OtherAddressStreet);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.PagerTelephoneNumber, applicationData.PagerNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.WorkAddressPostalCode, applicationData.BusinessAddressPostalCode);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.Surname, applicationData.LastName);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.SpouseName, applicationData.Spouse);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.WorkAddressState, applicationData.BusinessAddressState);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.WorkAddressStreet, applicationData.BusinessAddressStreet);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.Title, applicationData.JobTitle);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.YomiFirstName, applicationData.YomiFirstName);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.YomiLastName, applicationData.YomiLastName);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.YomiCompany, applicationData.YomiCompanyName);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.OfficeLocation, applicationData.OfficeLocation);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.RadioTelephoneNumber, applicationData.RadioPhoneNumber);
			EasFxContactMessage.AddToContactPropertiesIfValid(list, PropTag.PartnerNetworkThumbnailPhotoUrl, applicationData.Picture);
			if (applicationData.FirstName != null && applicationData.LastName != null)
			{
				EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.DisplayNameFirstLast, applicationData.FirstName + " " + applicationData.LastName);
				EasFxContactMessage.AddToContactPropertiesIfValid(list, (PropTag)SyncContactSchema.DisplayNameLastFirst, applicationData.LastName + " " + applicationData.FirstName);
			}
			return list;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000075E6 File Offset: 0x000057E6
		private static void AddToContactPropertiesIfValid(List<PropValueData> contactProperties, PropTag propTag, object valueToSet)
		{
			if (valueToSet != null)
			{
				contactProperties.Add(new PropValueData(propTag, valueToSet));
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000075F8 File Offset: 0x000057F8
		private static FxPropertyBag CreatePropertyBag(MessageRec messageRec)
		{
			FxPropertyBag result = new FxPropertyBag(new FxSession(SyncContactSchema.PropertyTagToNamedProperties));
			foreach (PropertyTag propertyTag in SyncContactSchema.AllContactPropertyTags)
			{
				EasFxContactMessage.SetIfValid(result, propertyTag, messageRec[(PropTag)propertyTag]);
			}
			return result;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007668 File Offset: 0x00005868
		private static void SetIfValid(FxPropertyBag propertyBag, PropertyTag propertyTag, object valueToSet)
		{
			if (valueToSet != null)
			{
				if (propertyTag.PropertyType == PropertyType.SysTime)
				{
					DateTime dateTime;
					if (DateTime.TryParse(valueToSet.ToString(), out dateTime))
					{
						propertyBag[propertyTag] = new ExDateTime(ExTimeZone.UtcTimeZone, dateTime);
						return;
					}
				}
				else
				{
					propertyBag[propertyTag] = valueToSet;
				}
			}
		}

		// Token: 0x0400005F RID: 95
		private readonly IPropertyBag propertyBag;
	}
}
