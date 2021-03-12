using System;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004A1 RID: 1185
	[OwaEventNamespace("EditContactItem")]
	internal sealed class EditContactItemEventHandler : ItemEventHandler
	{
		// Token: 0x06002D9E RID: 11678 RVA: 0x00101F5C File Offset: 0x0010015C
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(EditContactItemEventHandler));
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x00101F70 File Offset: 0x00100170
		[OwaEventParameter("m", typeof(string))]
		[OwaEventParameter("hc", typeof(string))]
		[OwaEventParameter("mn", typeof(string), false, true)]
		[OwaEventParameter("mp", typeof(string))]
		[OwaEventParameter("sn", typeof(string))]
		[OwaEventParameter("t", typeof(string))]
		[OwaEventParameter("of", typeof(string))]
		[OwaEventParameter("ol", typeof(string))]
		[OwaEventParameter("hpc", typeof(string))]
		[OwaEventParameter("hct", typeof(string))]
		[OwaEventParameter("ot", typeof(string))]
		[OwaEventParameter("os", typeof(string))]
		[OwaEventParameter("oc", typeof(string))]
		[OwaEventParameter("ost", typeof(string))]
		[OwaEventParameter("opc", typeof(string))]
		[OwaEventParameter("oct", typeof(string))]
		[OwaEventParameter("omp", typeof(string))]
		[OwaEventParameter("p", typeof(string))]
		[OwaEventParameter("pa", typeof(int))]
		[OwaEventParameter("ptn", typeof(string))]
		[OwaEventParameter("rp", typeof(string))]
		[OwaEventParameter("tn", typeof(string))]
		[OwaEventParameter("ttpn", typeof(string))]
		[OwaEventParameter("yf", typeof(string), false, true)]
		[OwaEventParameter("yl", typeof(string), false, true)]
		[OwaEventParameter("wp", typeof(string))]
		[OwaEventParameter("was", typeof(string))]
		[OwaEventParameter("wac", typeof(string))]
		[OwaEventParameter("wast", typeof(string))]
		[OwaEventParameter("wapc", typeof(string))]
		[OwaEventParameter("wact", typeof(string))]
		[OwaEventParameter("notes", typeof(string), false, true)]
		[OwaEventParameter("hst", typeof(string))]
		[OwaEvent("Save")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("an", typeof(string))]
		[OwaEventParameter("apn", typeof(string))]
		[OwaEventParameter("bpn", typeof(string))]
		[OwaEventParameter("bpn2", typeof(string))]
		[OwaEventParameter("cn", typeof(string))]
		[OwaEventParameter("cy", typeof(string), false, true)]
		[OwaEventParameter("cbp", typeof(string))]
		[OwaEventParameter("cp", typeof(string))]
		[OwaEventParameter("d", typeof(string))]
		[OwaEventParameter("em1", typeof(string))]
		[OwaEventParameter("em2", typeof(string))]
		[OwaEventParameter("em3", typeof(string))]
		[OwaEventParameter("em1dn", typeof(string))]
		[OwaEventParameter("em2dn", typeof(string))]
		[OwaEventParameter("em3dn", typeof(string))]
		[OwaEventParameter("fa", typeof(int))]
		[OwaEventParameter("fn", typeof(string))]
		[OwaEventParameter("gn", typeof(string))]
		[OwaEventParameter("im", typeof(string))]
		[OwaEventParameter("iin", typeof(string))]
		[OwaEventParameter("hf", typeof(string))]
		[OwaEventParameter("hp", typeof(string))]
		[OwaEventParameter("hp2", typeof(string))]
		[OwaEventParameter("hs", typeof(string))]
		public void Save()
		{
			ExTraceGlobals.ContactsCallTracer.TraceDebug((long)this.GetHashCode(), "EditContactItemEventHandler.Save");
			bool flag = base.IsParameterSet("Id");
			using (Contact contact = this.GetContact(new PropertyDefinition[0]))
			{
				this.SetTextPropertyValue(contact, ContactUtilities.GivenName);
				this.SetTextPropertyValue(contact, ContactUtilities.MiddleName);
				this.SetTextPropertyValue(contact, ContactUtilities.SurName);
				this.SetFileAsValue(contact);
				this.SetTextPropertyValue(contact, ContactUtilities.Title);
				this.SetTextPropertyValue(contact, ContactUtilities.CompanyName);
				this.SetTextPropertyValue(contact, ContactUtilities.Manager);
				this.SetTextPropertyValue(contact, ContactUtilities.AssistantName);
				this.SetTextPropertyValue(contact, ContactUtilities.Department);
				this.SetTextPropertyValue(contact, ContactUtilities.OfficeLocation);
				this.SetTextPropertyValue(contact, ContactUtilities.YomiFirstName);
				this.SetTextPropertyValue(contact, ContactUtilities.YomiLastName);
				this.SetTextPropertyValue(contact, ContactUtilities.CompanyYomi);
				this.SetTextPropertyValue(contact, ContactUtilities.BusinessPhoneNumber);
				this.SetTextPropertyValue(contact, ContactUtilities.HomePhone);
				this.SetTextPropertyValue(contact, ContactUtilities.MobilePhone);
				for (int i = 0; i < ContactUtilities.PhoneNumberProperties.Length; i++)
				{
					this.SetTextPropertyValue(contact, ContactUtilities.PhoneNumberProperties[i]);
				}
				this.SetEmailPropertyValue(contact, ContactUtilities.Email1EmailAddress);
				this.SetEmailPropertyValue(contact, ContactUtilities.Email2EmailAddress);
				this.SetEmailPropertyValue(contact, ContactUtilities.Email3EmailAddress);
				this.SetTextPropertyValue(contact, ContactUtilities.IMAddress);
				this.SetTextPropertyValue(contact, ContactUtilities.WebPage);
				int num = (int)base.GetParameter(ContactUtilities.PostalAddressId.Id);
				contact[ContactUtilities.PostalAddressId.PropertyDefinition] = num;
				this.SetTextPropertyValue(contact, ContactUtilities.WorkAddressStreet);
				this.SetTextPropertyValue(contact, ContactUtilities.WorkAddressCity);
				this.SetTextPropertyValue(contact, ContactUtilities.WorkAddressState);
				this.SetTextPropertyValue(contact, ContactUtilities.WorkAddressPostalCode);
				this.SetTextPropertyValue(contact, ContactUtilities.WorkAddressCountry);
				this.SetTextPropertyValue(contact, ContactUtilities.HomeStreet);
				this.SetTextPropertyValue(contact, ContactUtilities.HomeCity);
				this.SetTextPropertyValue(contact, ContactUtilities.HomeState);
				this.SetTextPropertyValue(contact, ContactUtilities.HomePostalCode);
				this.SetTextPropertyValue(contact, ContactUtilities.HomeCountry);
				this.SetTextPropertyValue(contact, ContactUtilities.OtherStreet);
				this.SetTextPropertyValue(contact, ContactUtilities.OtherCity);
				this.SetTextPropertyValue(contact, ContactUtilities.OtherState);
				this.SetTextPropertyValue(contact, ContactUtilities.OtherPostalCode);
				this.SetTextPropertyValue(contact, ContactUtilities.OtherCountry);
				string text = (string)base.GetParameter("notes");
				if (text != null)
				{
					BodyConversionUtilities.SetBody(contact, text, Markup.PlainText, base.UserContext);
				}
				string textPropertyValue = EditContactItemEventHandler.GetTextPropertyValue(contact, ContactBaseSchema.FileAs);
				if (string.IsNullOrEmpty(textPropertyValue) && !string.IsNullOrEmpty(contact.Company))
				{
					contact[ContactUtilities.FileAsId.PropertyDefinition] = FileAsMapping.Company;
				}
				Utilities.SaveItem(contact);
				contact.Load();
				if (!flag)
				{
					if (ExTraceGlobals.ContactsDataTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.ContactsDataTracer.TraceDebug<string>((long)this.GetHashCode(), "New contact item ID is '{0}'", contact.Id.ObjectId.ToBase64String());
					}
					this.Writer.Write("<div id=itemId>");
					this.Writer.Write(Utilities.GetIdAsString(contact));
					this.Writer.Write("</div>");
				}
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(contact.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
				string text2 = EditContactItemEventHandler.GetTextPropertyValue(contact, ContactBaseSchema.FileAs);
				if (string.IsNullOrEmpty(text2))
				{
					text2 = LocalizedStrings.GetNonEncoded(-1873027801);
				}
				this.Writer.Write("<div id=fa>");
				Utilities.HtmlEncode(text2, this.Writer);
				this.Writer.Write("</div>");
				base.MoveItemToDestinationFolderIfInScratchPad(contact);
			}
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x0010232C File Offset: 0x0010052C
		private static string GetTextPropertyValue(Contact contact, PropertyDefinition property)
		{
			string result = string.Empty;
			string text = contact.TryGetProperty(property) as string;
			if (text != null)
			{
				result = text.Trim();
			}
			return result;
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x00102358 File Offset: 0x00100558
		private Contact GetContact(params PropertyDefinition[] prefetchProperties)
		{
			bool flag = base.IsParameterSet("Id");
			Contact result;
			if (flag)
			{
				result = base.GetRequestItem<Contact>(prefetchProperties);
			}
			else
			{
				ExTraceGlobals.ContactsTracer.TraceDebug((long)this.GetHashCode(), "ItemId is null. Creating new contact item.");
				OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
				result = Utilities.CreateItem<Contact>(folderId);
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
			}
			return result;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x001023C4 File Offset: 0x001005C4
		private void SetTextPropertyValue(Contact contact, ContactPropertyInfo propertyInfo)
		{
			string text = (string)base.GetParameter(propertyInfo.Id);
			if (text != null)
			{
				contact[propertyInfo.PropertyDefinition] = text;
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x001023F4 File Offset: 0x001005F4
		private void SetEmailPropertyValue(Contact contact, ContactPropertyInfo propertyInfo)
		{
			ContactPropertyInfo emailDisplayAsProperty = ContactUtilities.GetEmailDisplayAsProperty(propertyInfo);
			EmailAddressIndex emailPropertyIndex = ContactUtilities.GetEmailPropertyIndex(propertyInfo);
			string email = (string)base.GetParameter(propertyInfo.Id);
			string displayName = (string)base.GetParameter(emailDisplayAsProperty.Id);
			ContactUtilities.SetContactEmailAddress(contact, emailPropertyIndex, email, displayName);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x0010243C File Offset: 0x0010063C
		private void SetFileAsValue(Contact contact)
		{
			int num = (int)base.GetParameter(ContactUtilities.FileAsId.Id);
			contact[ContactUtilities.FileAsId.PropertyDefinition] = (FileAsMapping)num;
		}

		// Token: 0x04001E76 RID: 7798
		public const string EventNamespace = "EditContactItem";

		// Token: 0x04001E77 RID: 7799
		public const string AssistantNameId = "an";

		// Token: 0x04001E78 RID: 7800
		public const string AssistantPhoneNumberId = "apn";

		// Token: 0x04001E79 RID: 7801
		public const string BusinessPhoneNumberId = "bpn";

		// Token: 0x04001E7A RID: 7802
		public const string BusinessPhoneNumber2Id = "bpn2";

		// Token: 0x04001E7B RID: 7803
		public const string CompanyNameId = "cn";

		// Token: 0x04001E7C RID: 7804
		public const string CallbackPhoneId = "cbp";

		// Token: 0x04001E7D RID: 7805
		public const string CarPhoneId = "cp";

		// Token: 0x04001E7E RID: 7806
		public const string CompanyYomiId = "cy";

		// Token: 0x04001E7F RID: 7807
		public const string DepartmentId = "d";

		// Token: 0x04001E80 RID: 7808
		public const string Email1EmailAddressId = "em1";

		// Token: 0x04001E81 RID: 7809
		public const string Email2EmailAddressId = "em2";

		// Token: 0x04001E82 RID: 7810
		public const string Email3EmailAddressId = "em3";

		// Token: 0x04001E83 RID: 7811
		public const string Email1DisplayNameId = "em1dn";

		// Token: 0x04001E84 RID: 7812
		public const string Email2DisplayNameId = "em2dn";

		// Token: 0x04001E85 RID: 7813
		public const string Email3DisplayNameId = "em3dn";

		// Token: 0x04001E86 RID: 7814
		public const string FileAsId = "fa";

		// Token: 0x04001E87 RID: 7815
		public const string FaxNumberId = "fn";

		// Token: 0x04001E88 RID: 7816
		public const string GivenNameId = "gn";

		// Token: 0x04001E89 RID: 7817
		public const string IMAddressId = "im";

		// Token: 0x04001E8A RID: 7818
		public const string InternationalIsdnNumberId = "iin";

		// Token: 0x04001E8B RID: 7819
		public const string HomeFaxId = "hf";

		// Token: 0x04001E8C RID: 7820
		public const string HomePhoneId = "hp";

		// Token: 0x04001E8D RID: 7821
		public const string HomePhone2Id = "hp2";

		// Token: 0x04001E8E RID: 7822
		public const string HomeStreetId = "hs";

		// Token: 0x04001E8F RID: 7823
		public const string HomeCityId = "hc";

		// Token: 0x04001E90 RID: 7824
		public const string HomeStateId = "hst";

		// Token: 0x04001E91 RID: 7825
		public const string HomePostalCodeId = "hpc";

		// Token: 0x04001E92 RID: 7826
		public const string HomeCountryId = "hct";

		// Token: 0x04001E93 RID: 7827
		public const string ManagerId = "m";

		// Token: 0x04001E94 RID: 7828
		public const string MiddleNameId = "mn";

		// Token: 0x04001E95 RID: 7829
		public const string MobilePhoneId = "mp";

		// Token: 0x04001E96 RID: 7830
		public const string SurNameId = "sn";

		// Token: 0x04001E97 RID: 7831
		public const string TitleId = "t";

		// Token: 0x04001E98 RID: 7832
		public const string OtherFaxId = "of";

		// Token: 0x04001E99 RID: 7833
		public const string OfficeLocationId = "ol";

		// Token: 0x04001E9A RID: 7834
		public const string OtherTelephoneId = "ot";

		// Token: 0x04001E9B RID: 7835
		public const string OtherStreetId = "os";

		// Token: 0x04001E9C RID: 7836
		public const string OtherCityId = "oc";

		// Token: 0x04001E9D RID: 7837
		public const string OtherStateId = "ost";

		// Token: 0x04001E9E RID: 7838
		public const string OtherPostalCodeId = "opc";

		// Token: 0x04001E9F RID: 7839
		public const string OtherCountryId = "oct";

		// Token: 0x04001EA0 RID: 7840
		public const string OrganizationMainPhoneId = "omp";

		// Token: 0x04001EA1 RID: 7841
		public const string PagerId = "p";

		// Token: 0x04001EA2 RID: 7842
		public const string PostalAddressIdId = "pa";

		// Token: 0x04001EA3 RID: 7843
		public const string PrimaryTelephoneNumberId = "ptn";

		// Token: 0x04001EA4 RID: 7844
		public const string RadioPhoneId = "rp";

		// Token: 0x04001EA5 RID: 7845
		public const string TelexNumberId = "tn";

		// Token: 0x04001EA6 RID: 7846
		public const string TtyTddPhoneNumberId = "ttpn";

		// Token: 0x04001EA7 RID: 7847
		public const string YomiFirstNameId = "yf";

		// Token: 0x04001EA8 RID: 7848
		public const string YomiLastNameId = "yl";

		// Token: 0x04001EA9 RID: 7849
		public const string WebPageId = "wp";

		// Token: 0x04001EAA RID: 7850
		public const string WorkAddressStreetId = "was";

		// Token: 0x04001EAB RID: 7851
		public const string WorkAddressCityId = "wac";

		// Token: 0x04001EAC RID: 7852
		public const string WorkAddressStateId = "wast";

		// Token: 0x04001EAD RID: 7853
		public const string WorkAddressPostalCodeId = "wapc";

		// Token: 0x04001EAE RID: 7854
		public const string WorkAddressCountryId = "wact";

		// Token: 0x04001EAF RID: 7855
		public const string Notes = "notes";
	}
}
