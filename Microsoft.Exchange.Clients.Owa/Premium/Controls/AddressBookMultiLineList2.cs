using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000306 RID: 774
	internal sealed class AddressBookMultiLineList2 : ItemList2
	{
		// Token: 0x06001D61 RID: 7521 RVA: 0x000A978C File Offset: 0x000A798C
		private static Dictionary<string, AddressBookMultiLineList2.ItemClass> CreateItemClassMap()
		{
			Dictionary<string, AddressBookMultiLineList2.ItemClass> dictionary = new Dictionary<string, AddressBookMultiLineList2.ItemClass>(AddressBookMultiLineList2.itemClasses.Length, StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < AddressBookMultiLineList2.itemClasses.Length; i++)
			{
				dictionary.Add(AddressBookMultiLineList2.itemClasses[i].Key, AddressBookMultiLineList2.itemClasses[i].Value);
			}
			return dictionary;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000A97E4 File Offset: 0x000A79E4
		private static AddressBookMultiLineList2.SharedColumn GetSharedColumn(ColumnId columnId)
		{
			switch (columnId)
			{
			case ColumnId.Department:
				return AddressBookMultiLineList2.SharedColumn.Department;
			case ColumnId.HasAttachment:
			case ColumnId.Importance:
			case ColumnId.DeliveryTime:
			case ColumnId.SentTime:
			case ColumnId.Size:
			case ColumnId.PhoneNumbers:
				return AddressBookMultiLineList2.SharedColumn.Count;
			case ColumnId.ContactIcon:
				return AddressBookMultiLineList2.SharedColumn.ContactType;
			case ColumnId.FileAs:
				break;
			case ColumnId.Title:
				return AddressBookMultiLineList2.SharedColumn.Title;
			case ColumnId.CompanyName:
				return AddressBookMultiLineList2.SharedColumn.Affiliation;
			case ColumnId.BusinessPhone:
				return AddressBookMultiLineList2.SharedColumn.BusinessPhone;
			case ColumnId.BusinessFax:
				return AddressBookMultiLineList2.SharedColumn.BusinessFax;
			case ColumnId.MobilePhone:
				return AddressBookMultiLineList2.SharedColumn.MobilePhone;
			case ColumnId.HomePhone:
				return AddressBookMultiLineList2.SharedColumn.HomePhone;
			case ColumnId.EmailAddresses:
			case ColumnId.Email1:
				return AddressBookMultiLineList2.SharedColumn.EmailAddress1;
			case ColumnId.Email2:
				return AddressBookMultiLineList2.SharedColumn.EmailAddress2;
			case ColumnId.Email3:
				return AddressBookMultiLineList2.SharedColumn.EmailAddress3;
			case ColumnId.GivenName:
				return AddressBookMultiLineList2.SharedColumn.FirstName;
			case ColumnId.Surname:
				return AddressBookMultiLineList2.SharedColumn.LastName;
			default:
				switch (columnId)
				{
				case ColumnId.BusinessFaxAD:
					return AddressBookMultiLineList2.SharedColumn.BusinessFax;
				case ColumnId.BusinessPhoneAD:
					return AddressBookMultiLineList2.SharedColumn.BusinessPhone;
				case ColumnId.CheckBoxAD:
				case ColumnId.CompanyAD:
				case ColumnId.OfficeAD:
				case ColumnId.PhoneAD:
				case ColumnId.YomiCompanyAD:
				case ColumnId.YomiFirstName:
				case ColumnId.YomiLastName:
					return AddressBookMultiLineList2.SharedColumn.Count;
				case ColumnId.DepartmentAD:
					return AddressBookMultiLineList2.SharedColumn.Affiliation;
				case ColumnId.DisplayNameAD:
					break;
				case ColumnId.EmailAddressAD:
					return AddressBookMultiLineList2.SharedColumn.EmailAddress1;
				case ColumnId.HomePhoneAD:
					return AddressBookMultiLineList2.SharedColumn.HomePhone;
				case ColumnId.MobilePhoneAD:
					return AddressBookMultiLineList2.SharedColumn.MobilePhone;
				case ColumnId.TitleAD:
					return AddressBookMultiLineList2.SharedColumn.Title;
				case ColumnId.YomiCompanyName:
				case ColumnId.YomiDepartmentAD:
					return AddressBookMultiLineList2.SharedColumn.YomiAffiliation;
				case ColumnId.YomiFullName:
				case ColumnId.YomiDisplayNameAD:
					return AddressBookMultiLineList2.SharedColumn.YomiName;
				default:
					if (columnId != ColumnId.IMAddress)
					{
						return AddressBookMultiLineList2.SharedColumn.Count;
					}
					return AddressBookMultiLineList2.SharedColumn.IMAddress;
				}
				break;
			}
			return AddressBookMultiLineList2.SharedColumn.Name;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000A98D7 File Offset: 0x000A7AD7
		private static void RenderRowEnd(TextWriter writer, bool shouldGreyOut)
		{
			writer.Write("</div>");
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x000A98E4 File Offset: 0x000A7AE4
		public AddressBookMultiLineList2(ViewDescriptor viewDescriptor, bool isContactView, bool isPicker, ColumnId sortedColumn, SortOrder sortOrder, UserContext userContext, SearchScope folderScope, bool isPAA, bool isMobile) : base(viewDescriptor, sortedColumn, sortOrder, userContext, folderScope)
		{
			this.isPAA = isPAA;
			this.isMobile = isMobile;
			this.isPicker = isPicker;
			this.isContactView = isContactView;
			if (isContactView)
			{
				base.AddProperty(ItemSchema.Id);
				base.AddProperty(StoreObjectSchema.ItemClass);
				base.AddProperty(ContactSchema.Email1);
				base.AddProperty(ContactSchema.Email2);
				base.AddProperty(ContactSchema.Email3);
				base.AddProperty(ContactSchema.ContactBusinessFax);
				base.AddProperty(ContactSchema.ContactHomeFax);
				base.AddProperty(ContactSchema.ContactOtherFax);
				base.AddProperty(ContactSchema.IMAddress);
				base.AddProperty(ContactSchema.MobilePhone);
				Column column = ListViewColumns.GetColumn(sortedColumn);
				for (int i = 0; i < column.PropertyCount; i++)
				{
					base.AddProperty(column[i]);
				}
				this.columnIds = AddressBookMultiLineList2.contactColumnIds;
			}
			else
			{
				base.AddProperty(ADObjectSchema.ObjectCategory);
				base.AddProperty(ADObjectSchema.Guid);
				base.AddProperty(ADRecipientSchema.RecipientType);
				base.AddProperty(ADRecipientSchema.ResourceMetaData);
				base.AddProperty(ADRecipientSchema.RecipientDisplayType);
				base.AddProperty(ADRecipientSchema.PrimarySmtpAddress);
				base.AddProperty(ADRecipientSchema.LegacyExchangeDN);
				base.AddProperty(ADRecipientSchema.EmailAddresses);
				base.AddProperty(ADOrgPersonSchema.MobilePhone);
				Column column2 = ListViewColumns.GetColumn(ColumnId.OfficeAD);
				for (int j = 0; j < column2.PropertyCount; j++)
				{
					base.AddProperty(column2[j]);
				}
				this.columnIds = AddressBookMultiLineList2.directoryColumnIds;
			}
			this.Initialize(viewDescriptor);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000A9A78 File Offset: 0x000A7C78
		private void Initialize(ViewDescriptor viewDescriptor)
		{
			for (int i = 0; i < viewDescriptor.ColumnCount; i++)
			{
				ColumnId column = viewDescriptor.GetColumn(i);
				if (column == this.columnIds[6] || column == this.columnIds[8] || column == this.columnIds[9])
				{
					for (int j = 0; j < AddressBookMultiLineList2.phoneNumberColumns.Length; j++)
					{
						if (column == this.columnIds[(int)AddressBookMultiLineList2.phoneNumberColumns[j]])
						{
							this.isPhoneNumberColumnPresent[j] = true;
						}
					}
				}
				else if (column == this.columnIds[2])
				{
					this.isYomiNameColumnPresent = true;
				}
				else if (column == this.columnIds[4])
				{
					this.isYomiAffiliationColumnPresent = true;
				}
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x000A9B1A File Offset: 0x000A7D1A
		private bool IsContactView
		{
			get
			{
				return this.isContactView;
			}
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000A9B24 File Offset: 0x000A7D24
		protected override void ValidatedRender(TextWriter writer, int startRange, int endRange)
		{
			writer.Write("<div class=\"multiIL abIL\" id=\"");
			writer.Write("divVLVIL");
			writer.Write("\">");
			this.DataSource.MoveToItem(startRange);
			while (this.DataSource.CurrentItem <= endRange)
			{
				AddressBookMultiLineList2.ItemClass itemClass;
				if (!AddressBookMultiLineList2.itemClassMap.TryGetValue(this.DataSource.GetItemClass(), out itemClass))
				{
					itemClass = AddressBookMultiLineList2.ItemClass.Person;
				}
				this.RenderItem(writer, itemClass);
				this.DataSource.MoveNext();
			}
			writer.Write("</div>");
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000A9BA8 File Offset: 0x000A7DA8
		private void RenderRow(TextWriter writer, string contents, int rowNumber, bool shouldGreyOut, bool hasEmailAddress, bool isGroup, bool isFirstRow)
		{
			this.RenderRowStart(writer, rowNumber, shouldGreyOut, hasEmailAddress);
			if (isGroup)
			{
				base.UserContext.RenderThemeImage(writer, ThemeFileId.AddressBookDL);
				writer.Write("&nbsp;&nbsp;<b>");
			}
			writer.Write(contents);
			if (isGroup)
			{
				writer.Write("</b>");
			}
			base.RenderSelectionImage(writer);
			if (isFirstRow)
			{
				base.RenderRowDivider(writer);
			}
			AddressBookMultiLineList2.RenderRowEnd(writer, shouldGreyOut);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000A9C14 File Offset: 0x000A7E14
		private void RenderRowStart(TextWriter writer, int rowNumber, bool shouldGreyOut, bool hasEmailAddress)
		{
			writer.Write("<div id=\"");
			writer.Write((rowNumber == 1) ? "vr" : "sr");
			writer.Write("\"");
			base.RenderItemTooltip(writer);
			writer.Write(" class=\" r" + rowNumber);
			if (shouldGreyOut)
			{
				writer.Write(" grey");
			}
			writer.Write("\"");
			if (rowNumber == 1)
			{
				writer.Write("><div class=\"cData\"");
				ItemList2.RenderRowId(writer, this.DataSource.GetItemId());
				this.RenderItemMetaDataExpandos(writer);
				if (!hasEmailAddress)
				{
					writer.Write(" _em=0");
				}
				writer.Write("></div>");
				return;
			}
			writer.Write(">");
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000A9CD0 File Offset: 0x000A7ED0
		private void RenderItem(TextWriter writer, AddressBookMultiLineList2.ItemClass itemClass)
		{
			bool flag = this.HasIMAddress();
			bool flag2 = this.HasEmailAddress() || flag;
			bool flag3 = this.IsDistributionList();
			bool flag4 = this.HasMobileNumber();
			bool flag5 = (this.isPicker && !this.isMobile && !this.isPAA && !flag2) || (this.isPAA && flag3) || (this.isMobile && !flag3 && !flag4);
			string[] array = new string[3];
			int num = 0;
			ColumnId columnId = base.SortedColumn.Id;
			if (columnId == ColumnId.ContactFlagDueDate || columnId == ColumnId.ContactFlagStartDate)
			{
				columnId = ColumnId.FileAs;
			}
			AddressBookMultiLineList2.SharedColumn sharedColumn = AddressBookMultiLineList2.GetSharedColumn(columnId);
			if (sharedColumn == AddressBookMultiLineList2.SharedColumn.Name)
			{
				array[0] = this.GetName(this.isYomiNameColumnPresent);
			}
			else if (sharedColumn == AddressBookMultiLineList2.SharedColumn.Affiliation)
			{
				array[0] = this.GetCompany(this.isYomiAffiliationColumnPresent);
			}
			else
			{
				array[0] = this.GetProperty(sharedColumn);
			}
			if (array[0] == null)
			{
				array[0] = "&nbsp;";
			}
			num++;
			if (sharedColumn == AddressBookMultiLineList2.SharedColumn.Name)
			{
				string text = this.GetSecondRow(itemClass, this.isYomiAffiliationColumnPresent, sharedColumn);
				if (text != null)
				{
					array[num++] = text;
				}
			}
			else
			{
				string text = this.GetName(this.isYomiNameColumnPresent);
				if (text != null)
				{
					array[num++] = text;
				}
				if (!this.isPicker || flag5 || (this.isPicker && num < 2))
				{
					text = this.GetSecondRow(itemClass, this.isYomiAffiliationColumnPresent, sharedColumn);
					if (text != null)
					{
						array[num++] = text;
					}
				}
			}
			if (((!this.isPicker || (this.isMobile && !flag3) || flag5) && num < 3) || (this.isPicker && num < 2))
			{
				string text = this.GetPhoneNumbers();
				if (text != null)
				{
					array[num++] = text;
				}
			}
			if (!this.isMobile || flag3)
			{
				if (this.isPicker)
				{
					if (num < 3)
					{
						string text = this.GetEmailAddresses();
						if (text != null)
						{
							array[num++] = text;
						}
						else if (flag)
						{
							text = this.GetIMAddress();
							if (text != null)
							{
								array[num++] = text;
							}
						}
					}
				}
				else
				{
					bool flag6 = false;
					int num2 = -1;
					string mobilePhoneNumber = this.GetMobilePhoneNumber();
					if (num < 3)
					{
						string text = this.GetEmailAddress(AddressBookMultiLineList2.SharedColumn.EmailAddress1, flag5);
						if (text != null)
						{
							bool flag7 = this.RowContainsEmailInfo(text);
							if (!flag7 && !flag5 && !flag && !flag6)
							{
								text = this.GetEmailRenderedAsIMAddress(text, null, mobilePhoneNumber);
								flag6 = true;
							}
							else if (flag7)
							{
								flag6 = true;
							}
							else if (!flag6 && num2 < 0)
							{
								num2 = num;
							}
							array[num++] = text;
						}
					}
					if (num < 3)
					{
						string text = this.GetEmailAddress(AddressBookMultiLineList2.SharedColumn.EmailAddress2, flag5);
						if (text != null)
						{
							bool flag8 = this.RowContainsEmailInfo(text);
							if (!flag8 && !flag5 && !flag && !flag6)
							{
								text = this.GetEmailRenderedAsIMAddress(text, null, mobilePhoneNumber);
								flag6 = true;
							}
							else if (flag8)
							{
								flag6 = true;
							}
							else if (!flag6 && num2 < 0)
							{
								num2 = num;
							}
							array[num++] = text;
						}
					}
					if (num < 3)
					{
						string text = this.GetEmailAddress(AddressBookMultiLineList2.SharedColumn.EmailAddress3, flag5);
						if (text != null)
						{
							bool flag9 = this.RowContainsEmailInfo(text);
							if (!flag9 && !flag5 && !flag && !flag6)
							{
								text = this.GetEmailRenderedAsIMAddress(text, null, mobilePhoneNumber);
								flag6 = true;
							}
							else if (flag9)
							{
								flag6 = true;
							}
							else if (!flag6 && num2 < 0)
							{
								num2 = num;
							}
							array[num++] = text;
						}
					}
					if (flag)
					{
						if (num < 3)
						{
							string text = this.GetEmailAddress(AddressBookMultiLineList2.SharedColumn.IMAddress, flag5);
							if (text != null)
							{
								array[num++] = text;
							}
						}
						else if (!flag6 && num2 > -1)
						{
							string email = array[num2];
							string itemProperty = this.DataSource.GetItemProperty<string>(ContactSchema.IMAddress, null);
							array[num2] = this.GetEmailRenderedAsIMAddress(email, itemProperty, mobilePhoneNumber);
						}
					}
				}
			}
			if (num == 1)
			{
				num = 2;
				using (StringWriter stringWriter = new StringWriter())
				{
					array[1] = (this.RenderMobileRecipient(stringWriter, " ") ? stringWriter.ToString() : "&nbsp;");
				}
			}
			this.RenderRow(writer, array[0], 1, flag5, flag2, itemClass == AddressBookMultiLineList2.ItemClass.Group, true);
			if (num == 2)
			{
				this.RenderRow(writer, array[1], 2, flag5, flag2, false, false);
				return;
			}
			if (num == 3)
			{
				this.RenderRow(writer, array[1], 2, flag5, flag2, false, false);
				this.RenderRow(writer, array[2], 3, flag5, flag2, false, false);
			}
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x000AA110 File Offset: 0x000A8310
		private bool RowContainsEmailInfo(string row)
		{
			return row.Contains(" id=\"ea\"");
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x000AA120 File Offset: 0x000A8320
		private string GetEmailRenderedAsIMAddress(string email, string explicitIMAddress, string mobilePhoneNumber)
		{
			email = base.RemoveVLVAnchors(email);
			if (string.IsNullOrEmpty(explicitIMAddress))
			{
				explicitIMAddress = email;
			}
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				base.RenderVLVAnchorOpen(stringWriter);
				stringWriter.Write(InstantMessageUtilities.GetSingleEmailAddress(this.DataSource, email, email, email, null, EmailAddressIndex.None, RecipientAddress.RecipientAddressFlags.None, null, InstantMessageUtilities.ToSipFormat(explicitIMAddress), mobilePhoneNumber, false));
				base.RenderVLVAnchorClose(stringWriter);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x000AA19C File Offset: 0x000A839C
		private string GetSecondRow(AddressBookMultiLineList2.ItemClass itemClass, bool renderYomiCompany, AddressBookMultiLineList2.SharedColumn sortedSharedColumn)
		{
			string text = null;
			if (itemClass == AddressBookMultiLineList2.ItemClass.Person)
			{
				text = this.GetTitleAndOrCompany(renderYomiCompany, sortedSharedColumn != AddressBookMultiLineList2.SharedColumn.Affiliation);
			}
			else
			{
				if (itemClass == AddressBookMultiLineList2.ItemClass.Group)
				{
					using (StringWriter stringWriter = new StringWriter())
					{
						base.RenderVLVAnchorOpen(stringWriter);
						stringWriter.Write(LocalizedStrings.GetHtmlEncoded(-1878983012));
						base.RenderVLVAnchorClose(stringWriter);
						return stringWriter.ToString();
					}
				}
				Column column = ListViewColumns.GetColumn(ColumnId.OfficeAD);
				text = this.DataSource.GetItemProperty<string>(column[0], string.Empty);
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
				else
				{
					text = Utilities.HtmlEncode(text);
				}
			}
			return text;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x000AA240 File Offset: 0x000A8440
		private string GetProperty(AddressBookMultiLineList2.SharedColumn sharedColumn)
		{
			if (AddressBookMultiLineList2.SharedColumn.Count == sharedColumn)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			ColumnId columnId = this.columnIds[(int)sharedColumn];
			if (columnId == ColumnId.Count)
			{
				return null;
			}
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				base.RenderColumn(stringWriter, columnId);
			}
			string text = stringBuilder.ToString();
			if (string.IsNullOrEmpty(text) || string.CompareOrdinal(text, "&nbsp;") == 0)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x000AA2B8 File Offset: 0x000A84B8
		private string GetName(bool renderYomi)
		{
			string property = this.GetProperty(AddressBookMultiLineList2.SharedColumn.Name);
			string text = null;
			if (renderYomi)
			{
				text = this.GetProperty(AddressBookMultiLineList2.SharedColumn.YomiName);
			}
			if (property == null && text == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (property != null)
			{
				stringBuilder.Append(property);
			}
			if (text != null)
			{
				stringBuilder.Append("&nbsp;");
				stringBuilder.Append(base.UserContext.DirectionMark);
				stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(6409762));
				stringBuilder.Append(text);
				stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(-1023695022));
				stringBuilder.Append(base.UserContext.DirectionMark);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000AA358 File Offset: 0x000A8558
		private string GetCompany(bool renderYomi)
		{
			string property = this.GetProperty(AddressBookMultiLineList2.SharedColumn.Affiliation);
			string text = null;
			if (renderYomi)
			{
				text = this.GetProperty(AddressBookMultiLineList2.SharedColumn.YomiAffiliation);
			}
			if (property == null && text == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (property != null)
			{
				stringBuilder.Append(property);
			}
			if (text != null)
			{
				stringBuilder.Append("&nbsp;");
				stringBuilder.Append(base.UserContext.DirectionMark);
				stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(6409762));
				stringBuilder.Append(text);
				stringBuilder.Append(LocalizedStrings.GetHtmlEncoded(-1023695022));
				stringBuilder.Append(base.UserContext.DirectionMark);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000AA3F8 File Offset: 0x000A85F8
		private string GetTitleAndOrCompany(bool renderYomi, bool renderCompany)
		{
			bool flag = false;
			string property = this.GetProperty(AddressBookMultiLineList2.SharedColumn.Title);
			StringBuilder stringBuilder = new StringBuilder();
			if (property != null)
			{
				stringBuilder.Append(property);
				flag = true;
			}
			if (renderCompany)
			{
				string company = this.GetCompany(renderYomi);
				if (company != null)
				{
					if (property != null)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(company);
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000AA454 File Offset: 0x000A8654
		private bool RenderMobileRecipient(TextWriter writer, string displayString)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			string itemProperty = this.DataSource.GetItemProperty<string>(this.IsContactView ? ContactSchema.MobilePhone : ADOrgPersonSchema.MobilePhone, null);
			string text = Utilities.NormalizePhoneNumber(itemProperty);
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			string itemProperty2 = this.DataSource.GetItemProperty<string>(this.isContactView ? ContactBaseSchema.FileAs : ADRecipientSchema.DisplayName, string.Empty);
			string sipUri = null;
			if (!this.isContactView)
			{
				ProxyAddressCollection itemProperty3 = this.DataSource.GetItemProperty<ProxyAddressCollection>(ADRecipientSchema.EmailAddresses, null);
				sipUri = InstantMessageUtilities.GetSipUri(itemProperty3);
			}
			base.RenderSingleEmailAddress(writer, itemProperty2, text, displayString ?? itemProperty, null, EmailAddressIndex.None, RecipientAddress.RecipientAddressFlags.None, "MOBILE", sipUri, text);
			return true;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000AA508 File Offset: 0x000A8708
		private string GetPhoneNumbers()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			for (int i = 0; i < AddressBookMultiLineList2.phoneNumberColumns.Length; i++)
			{
				if (this.isPhoneNumberColumnPresent[i])
				{
					string property = this.GetProperty(AddressBookMultiLineList2.phoneNumberColumns[i]);
					if (!string.IsNullOrEmpty(property))
					{
						if (!flag)
						{
							stringBuilder.Append("&nbsp;&nbsp;&nbsp;");
						}
						else
						{
							flag = false;
						}
						base.UserContext.RenderThemeImage(stringBuilder, AddressBookMultiLineList2.phoneNumberIcons[i], "pn", new object[0]);
						bool flag2 = true;
						if (AddressBookMultiLineList2.phoneNumberColumns[i] == AddressBookMultiLineList2.SharedColumn.MobilePhone)
						{
							using (StringWriter stringWriter = new StringWriter(stringBuilder))
							{
								flag2 = !this.RenderMobileRecipient(stringWriter, null);
							}
						}
						if (flag2)
						{
							stringBuilder.Append(property);
						}
					}
				}
			}
			if (flag)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000AA5E4 File Offset: 0x000A87E4
		private string GetEmailAddress(AddressBookMultiLineList2.SharedColumn column, bool shouldGreyOut)
		{
			if (shouldGreyOut)
			{
				return LocalizedStrings.GetHtmlEncoded(-679566404);
			}
			StringBuilder stringBuilder = new StringBuilder();
			ColumnId columnId = this.columnIds[(int)column];
			if (columnId == ColumnId.Count)
			{
				return null;
			}
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				base.RenderColumn(stringWriter, columnId);
			}
			string text = stringBuilder.ToString();
			if (string.IsNullOrEmpty(text) || string.CompareOrdinal(text, "&nbsp;") == 0)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x000AA668 File Offset: 0x000A8868
		private string GetEmailAddresses()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				base.RenderColumn(stringWriter, this.columnIds[10]);
			}
			string text = stringBuilder.ToString();
			if (string.IsNullOrEmpty(text) || string.CompareOrdinal(text, "&nbsp;") == 0)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x000AA6D0 File Offset: 0x000A88D0
		private string GetIMAddress()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				base.RenderColumn(stringWriter, this.columnIds[17]);
			}
			string text = stringBuilder.ToString();
			if (string.IsNullOrEmpty(text) || string.CompareOrdinal(text, "&nbsp;") == 0)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x000AA738 File Offset: 0x000A8938
		private bool HasEmailAddress(PropertyDefinition emailProperty)
		{
			Participant itemProperty = this.DataSource.GetItemProperty<Participant>(emailProperty, null);
			return itemProperty != null && !string.IsNullOrEmpty(itemProperty.EmailAddress);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x000AA76C File Offset: 0x000A896C
		private string GetSipUri()
		{
			if (!this.IsContactView)
			{
				ProxyAddressCollection itemProperty = this.DataSource.GetItemProperty<ProxyAddressCollection>(ADRecipientSchema.EmailAddresses, null);
				return InstantMessageUtilities.GetSipUri(itemProperty);
			}
			return string.Empty;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x000AA7A0 File Offset: 0x000A89A0
		private string GetUri()
		{
			if (!this.IsContactView)
			{
				ProxyAddressCollection itemProperty = this.DataSource.GetItemProperty<ProxyAddressCollection>(ADRecipientSchema.EmailAddresses, null);
				string text = InstantMessageUtilities.GetSipUri(itemProperty);
				if (string.IsNullOrEmpty(text))
				{
					text = InstantMessageUtilities.ToSipFormat(this.DataSource.GetItemProperty<SmtpAddress>(ADRecipientSchema.PrimarySmtpAddress, SmtpAddress.Empty).ToString());
				}
				return text;
			}
			string itemProperty2 = this.DataSource.GetItemProperty<string>(ContactSchema.IMAddress, string.Empty);
			if (!string.IsNullOrEmpty(itemProperty2))
			{
				return InstantMessageUtilities.ToSipFormat(itemProperty2);
			}
			Participant itemProperty3 = this.DataSource.GetItemProperty<Participant>(ContactSchema.Email1, null);
			if (itemProperty3 != null && !string.IsNullOrEmpty(itemProperty3.EmailAddress))
			{
				return InstantMessageUtilities.ToSipFormat(itemProperty3.EmailAddress);
			}
			itemProperty3 = this.DataSource.GetItemProperty<Participant>(ContactSchema.Email2, null);
			if (itemProperty3 != null && !string.IsNullOrEmpty(itemProperty3.EmailAddress))
			{
				return InstantMessageUtilities.ToSipFormat(itemProperty3.EmailAddress);
			}
			itemProperty3 = this.DataSource.GetItemProperty<Participant>(ContactSchema.Email3, null);
			if (itemProperty3 != null && !string.IsNullOrEmpty(itemProperty3.EmailAddress))
			{
				return InstantMessageUtilities.ToSipFormat(itemProperty3.EmailAddress);
			}
			return string.Empty;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x000AA8CB File Offset: 0x000A8ACB
		private string GetMobilePhoneNumber()
		{
			return Utilities.NormalizePhoneNumber(this.DataSource.GetItemProperty<string>(this.IsContactView ? ContactSchema.MobilePhone : ADOrgPersonSchema.MobilePhone, null));
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x000AA8F4 File Offset: 0x000A8AF4
		private bool HasEmailAddress()
		{
			if (this.IsContactView)
			{
				return ObjectClass.IsDistributionList(this.DataSource.GetItemClass()) || this.HasEmailAddress(ContactSchema.Email1) || this.HasEmailAddress(ContactSchema.Email2) || this.HasEmailAddress(ContactSchema.Email3) || this.HasEmailAddress(ContactSchema.ContactBusinessFax) || this.HasEmailAddress(ContactSchema.ContactHomeFax) || this.HasEmailAddress(ContactSchema.ContactOtherFax);
			}
			return this.DataSource.GetItemProperty<SmtpAddress>(ADRecipientSchema.PrimarySmtpAddress, SmtpAddress.Empty) != SmtpAddress.Empty;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000AA989 File Offset: 0x000A8B89
		private bool HasIMAddress()
		{
			return !string.IsNullOrEmpty(this.DataSource.GetItemProperty<string>(ContactSchema.IMAddress, null));
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000AA9A4 File Offset: 0x000A8BA4
		private bool HasMobileNumber()
		{
			return !string.IsNullOrEmpty(this.GetMobilePhoneNumber());
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000AA9B4 File Offset: 0x000A8BB4
		private bool IsDistributionList()
		{
			if (!this.isContactView)
			{
				RecipientType itemProperty = this.DataSource.GetItemProperty<RecipientType>(ADRecipientSchema.RecipientType, RecipientType.Invalid);
				if (Utilities.IsADDistributionList(itemProperty))
				{
					return true;
				}
			}
			else if (ObjectClass.IsDistributionList(this.DataSource.GetItemClass()))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000AA9FC File Offset: 0x000A8BFC
		protected override void RenderItemMetaDataExpandos(TextWriter writer)
		{
			base.RenderItemMetaDataExpandos(writer);
			if (!this.isContactView)
			{
				RecipientType itemProperty = this.DataSource.GetItemProperty<RecipientType>(ADRecipientSchema.RecipientType, RecipientType.Invalid);
				if (Utilities.IsADDistributionList(itemProperty))
				{
					writer.Write(" _dl=\"1\"");
				}
			}
			string sipUri = this.GetSipUri();
			if (!string.IsNullOrEmpty(sipUri))
			{
				writer.Write(" sipUri=\"");
				Utilities.HtmlEncode(sipUri, writer);
				writer.Write("\"");
			}
			string uri = this.GetUri();
			if (!string.IsNullOrEmpty(uri))
			{
				writer.Write(" uri=\"");
				Utilities.HtmlEncode(uri, writer);
				writer.Write("\"");
			}
			string mobilePhoneNumber = this.GetMobilePhoneNumber();
			if (!string.IsNullOrEmpty(mobilePhoneNumber))
			{
				writer.Write(" mo=\"");
				Utilities.HtmlEncode(mobilePhoneNumber, writer);
				writer.Write("\"");
			}
		}

		// Token: 0x0400158C RID: 5516
		private static readonly KeyValuePair<string, AddressBookMultiLineList2.ItemClass>[] itemClasses = new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>[]
		{
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("IPM.DistList", AddressBookMultiLineList2.ItemClass.Group),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.Group", AddressBookMultiLineList2.ItemClass.Group),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.DynamicDL", AddressBookMultiLineList2.ItemClass.Group),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.MailEnabledUniversalDistributionGroup", AddressBookMultiLineList2.ItemClass.Group),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.MailEnabledUniversalSecurityGroup", AddressBookMultiLineList2.ItemClass.Group),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.MailEnabledNonUniversalGroup", AddressBookMultiLineList2.ItemClass.Group),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("IPM.Contact", AddressBookMultiLineList2.ItemClass.Person),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.User", AddressBookMultiLineList2.ItemClass.Person),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.Contact", AddressBookMultiLineList2.ItemClass.Person),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.MailboxUser", AddressBookMultiLineList2.ItemClass.Person),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.MailEnabledUser", AddressBookMultiLineList2.ItemClass.Person),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.RecipientType.MailEnabledContact", AddressBookMultiLineList2.ItemClass.Person),
			new KeyValuePair<string, AddressBookMultiLineList2.ItemClass>("AD.ResourceType.Room", AddressBookMultiLineList2.ItemClass.Room)
		};

		// Token: 0x0400158D RID: 5517
		private static readonly Dictionary<string, AddressBookMultiLineList2.ItemClass> itemClassMap = AddressBookMultiLineList2.CreateItemClassMap();

		// Token: 0x0400158E RID: 5518
		private static readonly ColumnId[] contactColumnIds = new ColumnId[]
		{
			ColumnId.Department,
			ColumnId.FileAs,
			ColumnId.YomiFullName,
			ColumnId.CompanyName,
			ColumnId.YomiCompanyName,
			ColumnId.Title,
			ColumnId.BusinessPhone,
			ColumnId.BusinessFax,
			ColumnId.MobilePhone,
			ColumnId.HomePhone,
			ColumnId.EmailAddresses,
			ColumnId.Email1,
			ColumnId.Email2,
			ColumnId.Email3,
			ColumnId.GivenName,
			ColumnId.Surname,
			ColumnId.ContactIcon,
			ColumnId.IMAddress
		};

		// Token: 0x0400158F RID: 5519
		private static readonly ColumnId[] directoryColumnIds = new ColumnId[]
		{
			ColumnId.DepartmentAD,
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.DepartmentAD,
			ColumnId.YomiDepartmentAD,
			ColumnId.TitleAD,
			ColumnId.BusinessPhoneAD,
			ColumnId.BusinessFaxAD,
			ColumnId.MobilePhoneAD,
			ColumnId.HomePhoneAD,
			ColumnId.EmailAddressAD,
			ColumnId.EmailAddressAD,
			ColumnId.Count,
			ColumnId.Count,
			ColumnId.Count,
			ColumnId.Count,
			ColumnId.Count,
			ColumnId.Count
		};

		// Token: 0x04001590 RID: 5520
		private static readonly AddressBookMultiLineList2.SharedColumn[] phoneNumberColumns = new AddressBookMultiLineList2.SharedColumn[]
		{
			AddressBookMultiLineList2.SharedColumn.BusinessPhone,
			AddressBookMultiLineList2.SharedColumn.MobilePhone,
			AddressBookMultiLineList2.SharedColumn.HomePhone
		};

		// Token: 0x04001591 RID: 5521
		private static readonly ThemeFileId[] phoneNumberIcons = new ThemeFileId[]
		{
			ThemeFileId.WorkPhone,
			ThemeFileId.MobilePhone,
			ThemeFileId.HomePhone
		};

		// Token: 0x04001592 RID: 5522
		private ColumnId[] columnIds;

		// Token: 0x04001593 RID: 5523
		private bool[] isPhoneNumberColumnPresent = new bool[AddressBookMultiLineList2.phoneNumberColumns.Length];

		// Token: 0x04001594 RID: 5524
		private bool isYomiNameColumnPresent;

		// Token: 0x04001595 RID: 5525
		private bool isYomiAffiliationColumnPresent;

		// Token: 0x04001596 RID: 5526
		private bool isPicker;

		// Token: 0x04001597 RID: 5527
		private bool isContactView;

		// Token: 0x04001598 RID: 5528
		private bool isPAA;

		// Token: 0x04001599 RID: 5529
		private bool isMobile;

		// Token: 0x02000307 RID: 775
		private enum ItemClass
		{
			// Token: 0x0400159B RID: 5531
			Person,
			// Token: 0x0400159C RID: 5532
			Group,
			// Token: 0x0400159D RID: 5533
			Room
		}

		// Token: 0x02000308 RID: 776
		private enum SharedColumn
		{
			// Token: 0x0400159F RID: 5535
			Department,
			// Token: 0x040015A0 RID: 5536
			Name,
			// Token: 0x040015A1 RID: 5537
			YomiName,
			// Token: 0x040015A2 RID: 5538
			Affiliation,
			// Token: 0x040015A3 RID: 5539
			YomiAffiliation,
			// Token: 0x040015A4 RID: 5540
			Title,
			// Token: 0x040015A5 RID: 5541
			BusinessPhone,
			// Token: 0x040015A6 RID: 5542
			BusinessFax,
			// Token: 0x040015A7 RID: 5543
			MobilePhone,
			// Token: 0x040015A8 RID: 5544
			HomePhone,
			// Token: 0x040015A9 RID: 5545
			EmailAddresses,
			// Token: 0x040015AA RID: 5546
			EmailAddress1,
			// Token: 0x040015AB RID: 5547
			EmailAddress2,
			// Token: 0x040015AC RID: 5548
			EmailAddress3,
			// Token: 0x040015AD RID: 5549
			FirstName,
			// Token: 0x040015AE RID: 5550
			LastName,
			// Token: 0x040015AF RID: 5551
			ContactType,
			// Token: 0x040015B0 RID: 5552
			IMAddress,
			// Token: 0x040015B1 RID: 5553
			Count
		}
	}
}
