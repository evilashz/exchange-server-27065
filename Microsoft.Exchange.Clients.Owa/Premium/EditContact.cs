using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200044E RID: 1102
	public class EditContact : EditItemForm, IRegistryOnlyForm
	{
		// Token: 0x060027E7 RID: 10215 RVA: 0x000E1DD0 File Offset: 0x000DFFD0
		private static PropertyDefinition[] CreatePrefetchProperties()
		{
			PropertyDefinition[] array = new PropertyDefinition[ContactUtilities.PrefetchProperties.Length + EditContact.additionalPrefetchProperties.Length];
			ContactUtilities.PrefetchProperties.CopyTo(array, 0);
			EditContact.additionalPrefetchProperties.CopyTo(array, ContactUtilities.PrefetchProperties.Length);
			return array;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000E1E14 File Offset: 0x000E0014
		protected override void OnLoad(EventArgs e)
		{
			ExTraceGlobals.ContactsCallTracer.TraceDebug((long)this.GetHashCode(), "EditContact.OnLoad");
			base.OnLoad(e);
			this.contact = base.Initialize<Contact>(false, EditContact.prefetchProperties);
			this.firstPhoneProperty = null;
			this.firstEmailProperty = null;
			if (this.contact != null)
			{
				for (int i = 0; i < ContactUtilities.PhoneNumberProperties.Length; i++)
				{
					ContactPropertyInfo contactPropertyInfo = ContactUtilities.PhoneNumberProperties[i];
					string propertyValue = this.GetPropertyValue(contactPropertyInfo.PropertyDefinition);
					if (this.firstPhoneProperty == null && !string.IsNullOrEmpty(propertyValue))
					{
						this.firstPhoneProperty = contactPropertyInfo;
						break;
					}
				}
				for (int j = 0; j < ContactUtilities.EmailAddressProperties.Length; j++)
				{
					ContactPropertyInfo contactPropertyInfo2 = ContactUtilities.EmailAddressProperties[j];
					string value = null;
					string value2 = null;
					this.GetEmailAddressValue(contactPropertyInfo2, out value, out value2);
					this.GetPropertyValue(contactPropertyInfo2.PropertyDefinition);
					if (this.firstEmailProperty == null && (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(value2)))
					{
						this.firstEmailProperty = contactPropertyInfo2;
						break;
					}
				}
				InfobarMessageBuilder.AddFlag(this.infobar, this.contact, base.UserContext);
			}
			if (this.firstPhoneProperty == null)
			{
				this.firstPhoneProperty = ContactUtilities.AssistantPhoneNumber;
			}
			if (this.firstEmailProperty == null)
			{
				this.firstEmailProperty = ContactUtilities.Email1EmailAddress;
			}
			this.isPhoneticNamesEnabled = Utilities.IsJapanese;
			this.toolbar = new EditContactToolbar(this.contact);
			this.toolbar.ToolbarType = ToolbarType.View;
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000E1F70 File Offset: 0x000E0170
		private void GetEmailAddressValue(ContactPropertyInfo propertyInfo, out string displayName, out string email)
		{
			displayName = string.Empty;
			email = string.Empty;
			if (base.Item == null)
			{
				return;
			}
			EmailAddressIndex emailPropertyIndex = ContactUtilities.GetEmailPropertyIndex(propertyInfo);
			ContactUtilities.GetContactEmailAddress(this.contact, emailPropertyIndex, out email, out displayName);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000E1FAC File Offset: 0x000E01AC
		protected void RenderTextProperty(ContactPropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			base.Response.Write("<div class=\"cntLabel\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(propertyInfo.Label));
			base.Response.Write("</div><div class=\"cntField\"><input type=\"text\" id=\"");
			base.Response.Write(propertyInfo.Id);
			base.Response.Write("\" class=\"cntWell\" maxlength=\"255\" value=\"");
			if (base.Item != null)
			{
				string propertyValue = this.GetPropertyValue(propertyInfo.PropertyDefinition);
				Utilities.HtmlEncode(propertyValue, base.Response.Output);
			}
			base.Response.Write("\"></div><div class=\"clear\" />");
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000E2054 File Offset: 0x000E0254
		protected void RenderMultiLineTextProperty(ContactPropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			base.Response.Write("<div class=\"cntLabelMulti\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(propertyInfo.Label));
			base.Response.Write("</div><div class=\"cntMulti\"><textarea id=\"");
			base.Response.Write(propertyInfo.Id);
			base.Response.Write("\" class=\"cntWellMulti\">");
			if (base.Item != null)
			{
				string propertyValue = this.GetPropertyValue(propertyInfo.PropertyDefinition);
				Utilities.HtmlEncode(propertyValue, base.Response.Output);
			}
			base.Response.Write("</textarea></div>");
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000E20FC File Offset: 0x000E02FC
		protected void RenderSeparator()
		{
			base.Response.Write("<div class=\"cntSep\"></div>");
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000E210E File Offset: 0x000E030E
		protected void RenderSeparator(string title)
		{
			base.Response.Write("<div class=\"cntInnerSectTxt\">");
			base.Response.Write(title);
			base.Response.Write("</div>");
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000E213C File Offset: 0x000E033C
		protected void RenderFileAs()
		{
			FileAsDropDownList dropDown = new FileAsDropDownList("divFA", ContactUtilities.GetFileAs(base.Item));
			this.RenderLabelAndDropDown(LocalizedStrings.GetHtmlEncoded(ContactUtilities.FileAsId.Label), dropDown);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000E2178 File Offset: 0x000E0378
		private void RenderLabelAndDropDown(string label, DropDownList dropDown)
		{
			base.Response.Write("<div class=\"cntLabel\">");
			base.Response.Write(label);
			base.Response.Write("</div><div class=\"cntCmbField\">");
			dropDown.Render(base.Response.Output);
			base.Response.Write("</div>");
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000E21D4 File Offset: 0x000E03D4
		private string GetPropertyValue(PropertyDefinition property)
		{
			string result = string.Empty;
			if (base.Item == null)
			{
				return result;
			}
			string text = base.Item.TryGetProperty(property) as string;
			if (text != null)
			{
				result = text;
			}
			return result;
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000E220C File Offset: 0x000E040C
		protected void RenderPhoneProperties()
		{
			int num = ContactUtilities.PhoneNumberProperties.Length;
			DropDownListItem[] array = new DropDownListItem[num];
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				ContactPropertyInfo contactPropertyInfo = ContactUtilities.PhoneNumberProperties[i];
				string propertyValue = this.GetPropertyValue(contactPropertyInfo.PropertyDefinition);
				stringBuilder.Append("<input id=\"");
				stringBuilder.Append(contactPropertyInfo.Id);
				stringBuilder.Append("\"");
				array[i] = new DropDownListItem(contactPropertyInfo.Id, contactPropertyInfo.Label, "drp_" + contactPropertyInfo.Id, !string.IsNullOrEmpty(propertyValue.Trim()));
				if (contactPropertyInfo != this.firstPhoneProperty)
				{
					stringBuilder.Append(" style=\"display:none\"");
				}
				stringBuilder.Append(" maxlength=\"256\" class=\"cntWell\" type=\"text\" value=\"");
				stringBuilder.Append(Utilities.HtmlEncode(propertyValue));
				stringBuilder.Append("\">");
			}
			base.Response.Write("<div class=\"cntLabelCombo\">");
			DropDownList dropDownList = new DropDownList("divPH", this.firstPhoneProperty.Id, array);
			dropDownList.Render(base.Response.Output);
			base.Response.Write("</div><div class=\"cntField\">");
			base.Response.Write(stringBuilder);
			base.Response.Write("</div>");
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000E2358 File Offset: 0x000E0558
		protected void RenderEmailProperties()
		{
			EmailDropDownList emailDropDownList = new EmailDropDownList("divEM", this.firstEmailProperty);
			base.Response.Write("<div class=\"cntLabelCombo\">");
			emailDropDownList.Render(base.Response.Output);
			base.Response.Write("</div><div class=\"cntField\">");
			string[] array = new string[ContactUtilities.EmailAddressProperties.Length];
			for (int i = 0; i < ContactUtilities.EmailAddressProperties.Length; i++)
			{
				string text = null;
				string s = null;
				ContactPropertyInfo contactPropertyInfo = ContactUtilities.EmailAddressProperties[i];
				this.GetEmailAddressValue(contactPropertyInfo, out text, out s);
				array[i] = text;
				base.Response.Write("<input id=\"");
				base.Response.Write(contactPropertyInfo.Id);
				base.Response.Write("\"");
				if (contactPropertyInfo != this.firstEmailProperty)
				{
					base.Response.Write(" style=\"display:none\"");
				}
				ContactPropertyInfo emailDisplayAsProperty = ContactUtilities.GetEmailDisplayAsProperty(contactPropertyInfo);
				base.Response.Write(" maxlength=\"256\" class=\"cntWell\" type=\"text\" value=\"");
				Utilities.HtmlEncode(s, base.Response.Output);
				base.Response.Write("\" _da=\"");
				base.Response.Write(emailDisplayAsProperty.Id);
				base.Response.Write("\">");
			}
			base.Response.Write("</div>");
			base.Response.Write("<div class=\"cntLabel\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(1019177604));
			base.Response.Write("</div><div class=\"cntField\">");
			for (int j = 0; j < ContactUtilities.EmailAddressProperties.Length; j++)
			{
				ContactPropertyInfo contactPropertyInfo2 = ContactUtilities.EmailAddressProperties[j];
				string s2 = array[j];
				base.Response.Write("<input id=\"");
				base.Response.Write(ContactUtilities.GetEmailDisplayAsProperty(contactPropertyInfo2).Id);
				base.Response.Write("\"");
				if (contactPropertyInfo2 != this.firstEmailProperty)
				{
					base.Response.Write(" style=\"display:none\"");
				}
				base.Response.Write(" maxlength=\"256\" class=\"cntWell\" type=\"text\" value=\"");
				Utilities.HtmlEncode(s2, base.Response.Output);
				base.Response.Write("\">");
			}
			base.Response.Write("</div>");
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000E2598 File Offset: 0x000E0798
		protected void RenderMailingAddress()
		{
			MailingAddressDropDownList dropDown = new MailingAddressDropDownList("divMA", (PhysicalAddressType)this.GetMailingAddress());
			this.RenderLabelAndDropDown(LocalizedStrings.GetHtmlEncoded(ContactUtilities.PostalAddressId.Label), dropDown);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000E25CC File Offset: 0x000E07CC
		protected int GetMailingAddress()
		{
			PhysicalAddressType result = PhysicalAddressType.None;
			if (base.Item != null)
			{
				object obj = base.Item.TryGetProperty(ContactSchema.PostalAddressId);
				if (obj is int)
				{
					result = (PhysicalAddressType)obj;
				}
			}
			return (int)result;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000E2604 File Offset: 0x000E0804
		private void RenderAddress(List<ContactPropertyInfo> addressInfo, PropertyDefinition street)
		{
			foreach (ContactPropertyInfo contactPropertyInfo in addressInfo)
			{
				if (contactPropertyInfo.PropertyDefinition == street)
				{
					this.RenderMultiLineTextProperty(contactPropertyInfo);
				}
				else
				{
					this.RenderTextProperty(contactPropertyInfo);
				}
			}
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000E2664 File Offset: 0x000E0864
		protected void RenderAddresses()
		{
			List<ContactPropertyInfo> addressInfo = ContactUtilities.GetAddressInfo(PhysicalAddressType.Business);
			this.RenderSeparator(LocalizedStrings.GetHtmlEncoded(-765825260));
			this.RenderAddress(addressInfo, ContactSchema.WorkAddressStreet);
			this.RenderSeparator(LocalizedStrings.GetHtmlEncoded(1414246315));
			addressInfo = ContactUtilities.GetAddressInfo(PhysicalAddressType.Home);
			this.RenderAddress(addressInfo, ContactSchema.HomeStreet);
			this.RenderSeparator(LocalizedStrings.GetHtmlEncoded(-582599340));
			addressInfo = ContactUtilities.GetAddressInfo(PhysicalAddressType.Other);
			this.RenderAddress(addressInfo, ContactSchema.OtherStreet);
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000E26DA File Offset: 0x000E08DA
		protected void LoadMessageBodyIntoStream()
		{
			BodyConversionUtilities.RenderMeetingPlainTextBody(base.Response.Output, base.Item, base.UserContext, false);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000E26FC File Offset: 0x000E08FC
		protected void RenderAttachments()
		{
			base.Response.Write("<div id=\"cntAttchLabel\" class=\"cntInnerSectTxt\">");
			base.Response.Write(LocalizedStrings.GetHtmlEncoded(796893232));
			base.Response.Write("</div>");
			base.Response.Write("<div id=\"divCntAttchWell\">");
			AttachmentWell.RenderAttachmentWell(base.Response.Output, AttachmentWellType.ReadWrite, this.AttachmentWellRenderObjects, base.UserContext);
			base.Response.Write("</div>");
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000E277C File Offset: 0x000E097C
		protected void CreateAttachmentHelpers()
		{
			if (base.Item != null)
			{
				this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(base.Item, base.AttachmentLinks, base.UserContext.IsPublicLogon);
				foreach (object obj in this.attachmentWellRenderObjects)
				{
					AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)obj;
					if (string.CompareOrdinal("ContactPicture.jpg", attachmentWellInfo.FileName) == 0)
					{
						this.attachmentWellRenderObjects.Remove(attachmentWellInfo);
						break;
					}
				}
				InfobarRenderingHelper infobarRenderingHelper = new InfobarRenderingHelper(this.attachmentWellRenderObjects);
				if (infobarRenderingHelper.HasLevelOne)
				{
					this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-2118248931), InfobarMessageType.Informational, AttachmentWell.AttachmentInfobarHtmlTag);
				}
			}
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000E284C File Offset: 0x000E0A4C
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, base.Item);
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000E2860 File Offset: 0x000E0A60
		protected void RenderCategories()
		{
			base.SanitizingResponse.Write("<div id=\"divWellCategories\"");
			if (!base.HasCategories)
			{
				base.SanitizingResponse.Write(" style=\"display:none\"");
			}
			base.SanitizingResponse.Write("><div id=\"divFieldCategories\">");
			if (base.Item != null)
			{
				CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
			}
			base.SanitizingResponse.Write("</div></div>");
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x000E28D4 File Offset: 0x000E0AD4
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x000E28DC File Offset: 0x000E0ADC
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x000E28E4 File Offset: 0x000E0AE4
		protected EditContactToolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x000E28EC File Offset: 0x000E0AEC
		protected ContactPropertyInfo FirstPhoneProperty
		{
			get
			{
				return this.firstPhoneProperty;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x000E28F4 File Offset: 0x000E0AF4
		protected ContactPropertyInfo FirstEmailProperty
		{
			get
			{
				return this.firstEmailProperty;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x000E28FC File Offset: 0x000E0AFC
		protected int FileAsValue
		{
			get
			{
				return (int)ContactUtilities.GetFileAs(base.Item);
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x000E2909 File Offset: 0x000E0B09
		protected static int StoreObjectTypeContact
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000E2910 File Offset: 0x000E0B10
		protected void RenderTitle()
		{
			if (base.Item == null)
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1873027801));
				return;
			}
			string propertyValue = this.GetPropertyValue(ContactBaseSchema.FileAs);
			if (string.IsNullOrEmpty(propertyValue))
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1873027801));
				return;
			}
			Utilities.HtmlEncode(propertyValue, base.Response.Output);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000E2978 File Offset: 0x000E0B78
		protected void RenderProfileArea()
		{
			if (ContactUtilities.GetDefaultFileAs() != FileAsMapping.LastCommaFirst)
			{
				if (this.isPhoneticNamesEnabled)
				{
					this.RenderTextProperty(ContactUtilities.YomiLastName);
				}
				this.RenderTextProperty(ContactUtilities.SurName);
				if (this.isPhoneticNamesEnabled)
				{
					this.RenderTextProperty(ContactUtilities.YomiFirstName);
				}
				this.RenderTextProperty(ContactUtilities.GivenName);
				if (!this.isPhoneticNamesEnabled)
				{
					this.RenderTextProperty(ContactUtilities.MiddleName);
				}
			}
			else
			{
				if (this.isPhoneticNamesEnabled)
				{
					this.RenderTextProperty(ContactUtilities.YomiFirstName);
				}
				this.RenderTextProperty(ContactUtilities.GivenName);
				if (!this.isPhoneticNamesEnabled)
				{
					this.RenderTextProperty(ContactUtilities.MiddleName);
				}
				if (this.isPhoneticNamesEnabled)
				{
					this.RenderTextProperty(ContactUtilities.YomiLastName);
				}
				this.RenderTextProperty(ContactUtilities.SurName);
			}
			this.RenderFileAs();
			this.RenderSeparator();
			this.RenderTextProperty(ContactUtilities.Title);
			this.RenderTextProperty(ContactUtilities.OfficeLocation);
			this.RenderTextProperty(ContactUtilities.Department);
			if (this.isPhoneticNamesEnabled)
			{
				this.RenderTextProperty(ContactUtilities.CompanyYomi);
			}
			this.RenderTextProperty(ContactUtilities.CompanyName);
			this.RenderTextProperty(ContactUtilities.Manager);
			this.RenderTextProperty(ContactUtilities.AssistantName);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000E2A94 File Offset: 0x000E0C94
		protected void RenderContactArea()
		{
			this.RenderTextProperty(ContactUtilities.BusinessPhoneNumber);
			this.RenderTextProperty(ContactUtilities.HomePhone);
			this.RenderTextProperty(ContactUtilities.MobilePhone);
			this.RenderPhoneProperties();
			this.RenderSeparator();
			this.RenderEmailProperties();
			this.RenderSeparator();
			this.RenderTextProperty(ContactUtilities.IMAddress);
			this.RenderTextProperty(ContactUtilities.WebPage);
		}

		// Token: 0x04001BD9 RID: 7129
		private static readonly PropertyDefinition[] additionalPrefetchProperties = new PropertyDefinition[]
		{
			ItemSchema.FlagStatus,
			ItemSchema.FlagCompleteTime,
			MessageItemSchema.ReplyTime,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.ReminderDueBy,
			ItemSchema.ReminderIsSet,
			StoreObjectSchema.EffectiveRights
		};

		// Token: 0x04001BDA RID: 7130
		private static readonly PropertyDefinition[] prefetchProperties = EditContact.CreatePrefetchProperties();

		// Token: 0x04001BDB RID: 7131
		private bool isPhoneticNamesEnabled;

		// Token: 0x04001BDC RID: 7132
		private Infobar infobar = new Infobar();

		// Token: 0x04001BDD RID: 7133
		private EditContactToolbar toolbar;

		// Token: 0x04001BDE RID: 7134
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001BDF RID: 7135
		private ContactPropertyInfo firstPhoneProperty;

		// Token: 0x04001BE0 RID: 7136
		private ContactPropertyInfo firstEmailProperty;

		// Token: 0x04001BE1 RID: 7137
		private Contact contact;
	}
}
