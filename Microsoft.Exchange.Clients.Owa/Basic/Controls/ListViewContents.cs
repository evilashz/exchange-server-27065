﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000007 RID: 7
	public abstract class ListViewContents
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00003224 File Offset: 0x00001424
		protected ListViewContents(ViewDescriptor viewDescriptor, ColumnId sortedColumn, SortOrder sortOrder, bool showFolderNameTooltip, UserContext userContext)
		{
			if (viewDescriptor == null)
			{
				throw new ArgumentNullException("viewDescriptor");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.userContext = userContext;
			this.viewDescriptor = viewDescriptor;
			this.sortedColumn = ListViewColumns.GetColumn(sortedColumn);
			this.sortOrder = sortOrder;
			this.showFolderNameTooltip = showFolderNameTooltip;
			for (int i = 0; i < viewDescriptor.PropertyCount; i++)
			{
				this.AddProperty(viewDescriptor.GetProperty(i));
			}
			if (showFolderNameTooltip)
			{
				this.AddProperty(ItemSchema.ParentDisplayName);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000032C1 File Offset: 0x000014C1
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000032C9 File Offset: 0x000014C9
		public Hashtable Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000032D1 File Offset: 0x000014D1
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000032D9 File Offset: 0x000014D9
		internal ListViewDataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000032E2 File Offset: 0x000014E2
		public ViewDescriptor ViewDescriptor
		{
			get
			{
				return this.viewDescriptor;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000032EA File Offset: 0x000014EA
		protected SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000032F2 File Offset: 0x000014F2
		protected Column SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000032FA File Offset: 0x000014FA
		protected void AddProperty(PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			if (this.properties.ContainsKey(propertyDefinition))
			{
				return;
			}
			this.properties.Add(propertyDefinition, null);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003326 File Offset: 0x00001526
		public void Render(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.Render(writer, 1, this.dataSource.RangeCount);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000334C File Offset: 0x0000154C
		public void Render(TextWriter writer, int startRange, int endRange)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (startRange < 1)
			{
				throw new ArgumentOutOfRangeException("startRange", "startRange must be greater than or equal to 1");
			}
			if (endRange < startRange)
			{
				throw new ArgumentOutOfRangeException("endRange", "endRange must be greater than or equal to startRange");
			}
			this.ValidatedRender(writer, startRange, endRange);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003398 File Offset: 0x00001598
		private void ValidatedRender(TextWriter writer, int startRange, int endRange)
		{
			for (int i = startRange - 1; i < endRange; i++)
			{
				writer.Write("<tr");
				bool isBold = this.RenderItemRowStyle(writer, i);
				writer.Write(">");
				for (int j = 0; j < this.ViewDescriptor.ColumnCount; j++)
				{
					ColumnId column = this.ViewDescriptor.GetColumn(j);
					Column column2 = ListViewColumns.GetColumn(column);
					writer.Write("<td nowrap");
					if (column2.HorizontalAlign != HorizontalAlign.NotSet)
					{
						switch (column2.HorizontalAlign)
						{
						case HorizontalAlign.Center:
							writer.Write(" align=\"center\"");
							break;
						case HorizontalAlign.Right:
							writer.Write(" align=\"right\"");
							break;
						}
					}
					if (this.SortedColumn.Id == column)
					{
						writer.Write(" class=\"sc");
						if (i == startRange - 1)
						{
							writer.Write(" frst");
						}
						writer.Write("\"");
					}
					else if (i == startRange - 1)
					{
						writer.Write(" class=\"frst\"");
					}
					writer.Write(">");
					this.RenderColumn(writer, column, i, isBold, this.IsItemForCompose(i));
					writer.Write("</td>");
				}
				writer.Write("</tr>");
			}
			if (this.meetingMessageIdStringBuilder.Length > 0)
			{
				writer.Write("<input type=\"hidden\" name=\"hidmtgmsg\" value=\"{0}\">", this.meetingMessageIdStringBuilder.ToString());
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000034EF File Offset: 0x000016EF
		protected virtual bool IsItemForCompose(int itemIndex)
		{
			return false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000034F4 File Offset: 0x000016F4
		protected virtual bool RenderIcon(TextWriter writer, int itemIndex)
		{
			string itemClass = this.dataSource.GetItemProperty(itemIndex, StoreObjectSchema.ItemClass) as string;
			return ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, itemClass);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003528 File Offset: 0x00001728
		protected void RenderColumn(TextWriter writer, ColumnId columnId, int itemIndex, bool isBold, bool openForCompose)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (itemIndex < 0 || itemIndex >= this.dataSource.RangeCount)
			{
				throw new ArgumentOutOfRangeException("itemIndex", itemIndex.ToString());
			}
			Column column = ListViewColumns.GetColumn(columnId);
			switch (columnId)
			{
			case ColumnId.MailIcon:
				if (!this.RenderIcon(writer, itemIndex))
				{
					goto IL_857;
				}
				goto IL_857;
			case ColumnId.From:
			case ColumnId.To:
			{
				string itemPropertyString = this.dataSource.GetItemPropertyString(itemIndex, column[0]);
				if (itemPropertyString.Length != 0)
				{
					Utilities.CropAndRenderText(writer, itemPropertyString, 16);
					goto IL_857;
				}
				goto IL_857;
			}
			case ColumnId.Subject:
			{
				writer.Write("<h1");
				if (isBold)
				{
					writer.Write(" class=\"bld\"");
				}
				writer.Write(">");
				writer.Write("<a href=\"#\"");
				this.RenderFolderNameTooltip(writer, itemIndex);
				writer.Write(" onClick=\"onClkRdMsg(this, '");
				string s = this.dataSource.GetItemProperty(itemIndex, StoreObjectSchema.ItemClass) as string;
				Utilities.HtmlEncode(Utilities.JavascriptEncode(s), writer);
				writer.Write("', {0}, {1});\">", itemIndex, openForCompose ? 1 : 0);
				string itemPropertyString2 = this.dataSource.GetItemPropertyString(itemIndex, column[0]);
				if (string.IsNullOrEmpty(itemPropertyString2.Trim()))
				{
					writer.Write(LocalizedStrings.GetHtmlEncoded(730745110));
				}
				else
				{
					Utilities.CropAndRenderText(writer, itemPropertyString2, 32);
				}
				writer.Write("</a></h1>");
				goto IL_857;
			}
			case ColumnId.Department:
			case ColumnId.ContactIcon:
			case ColumnId.BusinessPhone:
			case ColumnId.BusinessFax:
			case ColumnId.MobilePhone:
			case ColumnId.HomePhone:
				goto IL_74A;
			case ColumnId.HasAttachment:
			{
				bool hasAttachments = false;
				object itemProperty = this.dataSource.GetItemProperty(itemIndex, ItemSchema.HasAttachment);
				if (itemProperty is bool)
				{
					hasAttachments = (bool)itemProperty;
				}
				if (!ListViewContentsRenderingUtilities.RenderHasAttachments(writer, this.userContext, hasAttachments))
				{
					goto IL_857;
				}
				goto IL_857;
			}
			case ColumnId.Importance:
			{
				Importance importance = Importance.Normal;
				object itemProperty2 = this.dataSource.GetItemProperty(itemIndex, ItemSchema.Importance);
				if (itemProperty2 is Importance || itemProperty2 is int)
				{
					importance = (Importance)itemProperty2;
				}
				if (!ListViewContentsRenderingUtilities.RenderImportance(writer, this.UserContext, importance))
				{
					goto IL_857;
				}
				goto IL_857;
			}
			case ColumnId.DeliveryTime:
			{
				ExDateTime itemPropertyExDateTime = this.dataSource.GetItemPropertyExDateTime(itemIndex, ItemSchema.ReceivedTime);
				if (!this.RenderDate(writer, itemPropertyExDateTime))
				{
					goto IL_857;
				}
				goto IL_857;
			}
			case ColumnId.SentTime:
			{
				ExDateTime itemPropertyExDateTime2 = this.dataSource.GetItemPropertyExDateTime(itemIndex, ItemSchema.SentTime);
				if (!this.RenderDate(writer, itemPropertyExDateTime2))
				{
					goto IL_857;
				}
				goto IL_857;
			}
			case ColumnId.Size:
			{
				int num = 0;
				object itemProperty3 = this.dataSource.GetItemProperty(itemIndex, ItemSchema.Size);
				if (itemProperty3 is int)
				{
					num = (int)itemProperty3;
				}
				Utilities.RenderSizeWithUnits(writer, (long)num, true);
				goto IL_857;
			}
			case ColumnId.FileAs:
				break;
			case ColumnId.Title:
			case ColumnId.CompanyName:
				goto IL_5B5;
			case ColumnId.PhoneNumbers:
				if (!this.RenderPhoneNumberColumn(writer, itemIndex))
				{
					goto IL_857;
				}
				goto IL_857;
			case ColumnId.EmailAddresses:
			{
				Participant[] array = new Participant[3];
				bool flag = false;
				array[0] = (this.dataSource.GetItemProperty(itemIndex, ContactSchema.Email1) as Participant);
				array[1] = (this.dataSource.GetItemProperty(itemIndex, ContactSchema.Email2) as Participant);
				array[2] = (this.dataSource.GetItemProperty(itemIndex, ContactSchema.Email3) as Participant);
				foreach (Participant participant in array)
				{
					if (participant != null && !string.IsNullOrEmpty(participant.EmailAddress))
					{
						string text = null;
						string text2 = null;
						ContactUtilities.GetParticipantEmailAddress(participant, out text, out text2);
						Utilities.CropAndRenderText(writer, text, 24);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					goto IL_857;
				}
				goto IL_857;
			}
			default:
			{
				switch (columnId)
				{
				case ColumnId.CheckBox:
				{
					VersionedId itemPropertyVersionedId = this.dataSource.GetItemPropertyVersionedId(itemIndex, ItemSchema.Id);
					ListViewContentsRenderingUtilities.RenderCheckBox(writer, itemPropertyVersionedId.ObjectId.ToBase64String());
					string itemClass = this.dataSource.GetItemProperty(itemIndex, StoreObjectSchema.ItemClass) as string;
					if (ObjectClass.IsMeetingRequest(itemClass) || ObjectClass.IsMeetingCancellation(itemClass))
					{
						if (this.meetingMessageIdStringBuilder.Length > 0)
						{
							this.meetingMessageIdStringBuilder.Append(",");
						}
						this.meetingMessageIdStringBuilder.Append(itemPropertyVersionedId.ObjectId.ToBase64String());
						goto IL_857;
					}
					goto IL_857;
				}
				case ColumnId.CheckBoxContact:
				case ColumnId.CheckBoxAD:
				{
					string arg;
					if (columnId == ColumnId.CheckBoxAD)
					{
						arg = Utilities.GetBase64StringFromGuid((Guid)this.dataSource.GetItemProperty(itemIndex, ADObjectSchema.Guid));
					}
					else
					{
						VersionedId itemPropertyVersionedId2 = this.dataSource.GetItemPropertyVersionedId(itemIndex, ItemSchema.Id);
						arg = itemPropertyVersionedId2.ObjectId.ToBase64String();
					}
					writer.Write("<input type=\"checkbox\" name=\"chkRcpt\" value=\"{0}\"", arg);
					writer.Write(" onClick=\"onClkRChkBx(this);\">");
					goto IL_857;
				}
				case ColumnId.ADIcon:
				case ColumnId.BusinessFaxAD:
				case ColumnId.BusinessPhoneAD:
				case ColumnId.DepartmentAD:
					goto IL_74A;
				case ColumnId.AliasAD:
					break;
				case ColumnId.CompanyAD:
					goto IL_5B5;
				case ColumnId.DisplayNameAD:
					goto IL_383;
				default:
					switch (columnId)
					{
					case ColumnId.PhoneAD:
						break;
					case ColumnId.TitleAD:
						goto IL_5B5;
					default:
						goto IL_74A;
					}
					break;
				}
				string text3 = this.dataSource.GetItemProperty(itemIndex, column[0]) as string;
				if (!string.IsNullOrEmpty(text3))
				{
					Utilities.CropAndRenderText(writer, text3, 24);
					goto IL_857;
				}
				goto IL_857;
			}
			}
			IL_383:
			StringBuilder stringBuilder = new StringBuilder();
			object obj;
			int num2;
			if (columnId == ColumnId.DisplayNameAD)
			{
				string value = this.dataSource.GetItemProperty(itemIndex, ADRecipientSchema.DisplayName) as string;
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.Append(value);
				}
				obj = Utilities.GetBase64StringFromGuid((Guid)this.dataSource.GetItemProperty(itemIndex, ADObjectSchema.Guid));
				num2 = (isBold ? 2 : 1);
			}
			else
			{
				string value = this.dataSource.GetItemProperty(itemIndex, ContactBaseSchema.FileAs) as string;
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.Append(value);
				}
				bool flag2 = (columnId == ColumnId.DisplayNameAD) ? this.userContext.IsPhoneticNamesEnabled : Utilities.IsJapanese;
				if (flag2)
				{
					string value2 = this.dataSource.GetItemProperty(itemIndex, ContactSchema.YomiFirstName) as string;
					string value3 = this.dataSource.GetItemProperty(itemIndex, ContactSchema.YomiLastName) as string;
					bool flag3 = false;
					if (stringBuilder.Length > 0 && (!string.IsNullOrEmpty(value3) || !string.IsNullOrEmpty(value2)))
					{
						stringBuilder.Append(" (");
						flag3 = true;
					}
					if (!string.IsNullOrEmpty(value3))
					{
						stringBuilder.Append(value3);
						if (!string.IsNullOrEmpty(value2))
						{
							stringBuilder.Append(" ");
						}
					}
					if (!string.IsNullOrEmpty(value2))
					{
						stringBuilder.Append(value2);
					}
					if (flag3)
					{
						stringBuilder.Append(")");
					}
				}
				VersionedId itemPropertyVersionedId3 = this.dataSource.GetItemPropertyVersionedId(itemIndex, ItemSchema.Id);
				obj = itemPropertyVersionedId3.ObjectId.ToBase64String();
				num2 = (isBold ? 4 : 3);
			}
			if (Utilities.WhiteSpaceOnlyOrNullEmpty(stringBuilder.ToString()))
			{
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(-808148510));
			}
			writer.Write("<h1");
			if (isBold)
			{
				writer.Write(" class=\"bld\"");
			}
			writer.Write("><a href=\"#\" id=\"{0}\"", obj.ToString());
			if (isBold)
			{
				writer.Write(" class=\"lvwdl\"");
			}
			this.RenderFolderNameTooltip(writer, itemIndex);
			writer.Write(" onClick=\"return onClkRcpt(this, {0});\">", num2);
			if (isBold)
			{
				ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, "IPM.DistList");
			}
			Utilities.CropAndRenderText(writer, stringBuilder.ToString(), 32);
			writer.Write("</a></h1>");
			goto IL_857;
			IL_5B5:
			string text4 = this.dataSource.GetItemProperty(itemIndex, column[0]) as string;
			if (!string.IsNullOrEmpty(text4))
			{
				Utilities.CropAndRenderText(writer, text4, 16);
				goto IL_857;
			}
			goto IL_857;
			IL_74A:
			object itemProperty4 = this.dataSource.GetItemProperty(itemIndex, column[0]);
			string text5 = itemProperty4 as string;
			if (itemProperty4 is ExDateTime)
			{
				writer.Write(((ExDateTime)itemProperty4).ToString());
			}
			else if (itemProperty4 is DateTime)
			{
				ExDateTime exDateTime = new ExDateTime(this.userContext.TimeZone, (DateTime)itemProperty4);
				writer.Write(exDateTime.ToString());
			}
			else if (text5 != null)
			{
				if (text5.Length != 0)
				{
					Utilities.HtmlEncode(text5, writer);
				}
			}
			else if (itemProperty4 is int)
			{
				Utilities.HtmlEncode(((int)itemProperty4).ToString(CultureInfo.CurrentCulture.NumberFormat), writer);
			}
			else if (itemProperty4 is Unlimited<int>)
			{
				Unlimited<int> unlimited = (Unlimited<int>)itemProperty4;
				if (!unlimited.IsUnlimited)
				{
					Utilities.HtmlEncode(unlimited.Value.ToString(CultureInfo.CurrentCulture.NumberFormat), writer);
				}
			}
			else if (!(itemProperty4 is PropertyError))
			{
			}
			IL_857:
			writer.Write("&nbsp;");
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003D98 File Offset: 0x00001F98
		private void RenderFolderNameTooltip(TextWriter writer, int itemIndex)
		{
			if (this.showFolderNameTooltip)
			{
				writer.Write(" title=\"");
				string itemPropertyString = this.dataSource.GetItemPropertyString(itemIndex, ItemSchema.ParentDisplayName);
				writer.Write(string.Format(LocalizedStrings.GetHtmlEncoded(699235260), Utilities.HtmlEncode(itemPropertyString)));
				writer.Write('"');
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003DF0 File Offset: 0x00001FF0
		private bool RenderPhoneNumberColumn(TextWriter writer, int itemIndex)
		{
			PhoneNumberType itemPropertyInt = (PhoneNumberType)this.dataSource.GetItemPropertyInt(itemIndex, ContactSchema.SelectedPreferredPhoneNumber, 1);
			PropertyDefinition propertyDefinitionFromPhoneNumberType = ContactUtilities.GetPropertyDefinitionFromPhoneNumberType(itemPropertyInt);
			string text = this.dataSource.GetItemProperty(itemIndex, propertyDefinitionFromPhoneNumberType) as string;
			this.propertyIconMap = ContactsListViewContents.PropertyIconMap;
			if (text != null && text.Length > 0)
			{
				ThemeFileId themeFileId = this.propertyIconMap[propertyDefinitionFromPhoneNumberType];
				if (themeFileId == ThemeFileId.None)
				{
					writer.Write("<span class=\"phpd\">&nbsp;</span>");
				}
				else
				{
					writer.Write("<img class=\"cPh\" src=\"");
					this.userContext.RenderThemeFileUrl(writer, themeFileId);
					writer.Write("\"");
					Strings.IDs localizedID;
					if (ContactsListViewContents.PropertyAltMap.TryGetValue(propertyDefinitionFromPhoneNumberType, out localizedID))
					{
						writer.Write(" alt=\"");
						writer.Write(LocalizedStrings.GetHtmlEncoded(localizedID));
						writer.Write("\"");
					}
					writer.Write(">");
				}
				Utilities.CropAndRenderText(writer, text, 24);
				return true;
			}
			foreach (KeyValuePair<PropertyDefinition, ThemeFileId> keyValuePair in this.propertyIconMap)
			{
				string text2 = this.dataSource.GetItemProperty(itemIndex, keyValuePair.Key) as string;
				if (text2 != null && text2.Length > 0)
				{
					ThemeFileId value = keyValuePair.Value;
					if (value == ThemeFileId.None)
					{
						writer.Write("<span class=\"phpd\">&nbsp;</span>");
					}
					else
					{
						writer.Write("<img class=\"cPh\" src=\"");
						this.userContext.RenderThemeFileUrl(writer, value);
						writer.Write("\">");
					}
					Utilities.CropAndRenderText(writer, text2, 24);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003F8C File Offset: 0x0000218C
		protected virtual bool RenderItemRowStyle(TextWriter writer, int itemIndex)
		{
			return false;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003F90 File Offset: 0x00002190
		protected bool RenderDate(TextWriter writer, ExDateTime date)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (date != ExDateTime.MinValue)
			{
				writer.Write(date.ToString(this.userContext.UserOptions.DateFormat));
				writer.Write("&nbsp;");
				writer.Write(date.ToString(this.userContext.UserOptions.TimeFormat));
				return true;
			}
			return false;
		}

		// Token: 0x04000017 RID: 23
		private const int MaxCharactersInFromAndTo = 16;

		// Token: 0x04000018 RID: 24
		private const int MaxCharactersInSubject = 32;

		// Token: 0x04000019 RID: 25
		private const int MaxCharactersInDisplayName = 32;

		// Token: 0x0400001A RID: 26
		private const int MaxCharactersInAliasAndPhone = 24;

		// Token: 0x0400001B RID: 27
		private const int MaxCharactersInTitleAndCompany = 16;

		// Token: 0x0400001C RID: 28
		private ListViewDataSource dataSource;

		// Token: 0x0400001D RID: 29
		private UserContext userContext;

		// Token: 0x0400001E RID: 30
		private ViewDescriptor viewDescriptor;

		// Token: 0x0400001F RID: 31
		private Column sortedColumn;

		// Token: 0x04000020 RID: 32
		private SortOrder sortOrder;

		// Token: 0x04000021 RID: 33
		private StringBuilder meetingMessageIdStringBuilder = new StringBuilder();

		// Token: 0x04000022 RID: 34
		private bool showFolderNameTooltip;

		// Token: 0x04000023 RID: 35
		private Hashtable properties = new Hashtable();

		// Token: 0x04000024 RID: 36
		private Dictionary<PropertyDefinition, ThemeFileId> propertyIconMap;

		// Token: 0x02000008 RID: 8
		public enum ReadItemType
		{
			// Token: 0x04000026 RID: 38
			None,
			// Token: 0x04000027 RID: 39
			AdOrgPerson,
			// Token: 0x04000028 RID: 40
			AdDistributionList,
			// Token: 0x04000029 RID: 41
			Contact,
			// Token: 0x0400002A RID: 42
			ContactDistributionList
		}
	}
}
