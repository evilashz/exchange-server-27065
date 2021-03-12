using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200034F RID: 847
	public abstract class LegacyListViewContents
	{
		// Token: 0x06001FE5 RID: 8165 RVA: 0x000B8838 File Offset: 0x000B6A38
		protected LegacyListViewContents(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.userContext = userContext;
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x000B8860 File Offset: 0x000B6A60
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x000B8868 File Offset: 0x000B6A68
		protected bool IsForVirtualListView
		{
			get
			{
				return this.isForVirtualListView;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x000B8870 File Offset: 0x000B6A70
		public Hashtable Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000B8878 File Offset: 0x000B6A78
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x000B8880 File Offset: 0x000B6A80
		public virtual IListViewDataSource DataSource
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

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001FEB RID: 8171
		public abstract ViewDescriptor ViewDescriptor { get; }

		// Token: 0x06001FEC RID: 8172 RVA: 0x000B8889 File Offset: 0x000B6A89
		protected void AddProperty(PropertyDefinition propertyDefinition)
		{
			if (this.properties.ContainsKey(propertyDefinition))
			{
				return;
			}
			this.properties.Add(propertyDefinition, null);
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x000B88A7 File Offset: 0x000B6AA7
		public void RenderForVirtualListView(TextWriter writer)
		{
			this.isForVirtualListView = true;
			this.Render(writer);
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x000B88B7 File Offset: 0x000B6AB7
		public void Render(TextWriter writer)
		{
			this.Render(writer, 0, this.dataSource.RangeCount - 1);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000B88CE File Offset: 0x000B6ACE
		public void RenderForVirtualListView(TextWriter writer, int startRange, int endRange)
		{
			this.isForVirtualListView = true;
			this.Render(writer, startRange, endRange);
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x000B88E0 File Offset: 0x000B6AE0
		public void Render(TextWriter writer, int startRange, int endRange)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (startRange < 0)
			{
				throw new ArgumentOutOfRangeException("startRange", "startRange must be greater than or equal to 0");
			}
			if (endRange < startRange)
			{
				throw new ArgumentOutOfRangeException("endRange", "endRange must be greater than or equal to startRange");
			}
			this.ValidatedRender(writer, startRange, endRange);
		}

		// Token: 0x06001FF1 RID: 8177
		protected abstract void ValidatedRender(TextWriter writer, int startRange, int endRange);

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000B892C File Offset: 0x000B6B2C
		protected bool RenderColumn(TextWriter writer, ColumnId columnId)
		{
			return this.RenderColumn(writer, columnId, true);
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000B8938 File Offset: 0x000B6B38
		protected virtual bool RenderIcon(TextWriter writer)
		{
			string itemClass = this.dataSource.GetItemClass();
			return ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, itemClass);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000B8960 File Offset: 0x000B6B60
		protected bool RenderMessageIcon(TextWriter writer)
		{
			string itemProperty = this.dataSource.GetItemProperty<string>(StoreObjectSchema.ItemClass);
			int itemProperty2 = this.dataSource.GetItemProperty<int>(ItemSchema.IconIndex, -1);
			bool itemProperty3 = this.dataSource.GetItemProperty<bool>(MessageItemSchema.MessageInConflict, false);
			bool itemProperty4 = this.dataSource.GetItemProperty<bool>(MessageItemSchema.IsRead, false);
			return ListViewContentsRenderingUtilities.RenderMessageIcon(writer, this.userContext, itemProperty, itemProperty4, itemProperty3, itemProperty2);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x000B89C4 File Offset: 0x000B6BC4
		protected virtual bool RenderUncDocumentIcon(TextWriter writer)
		{
			bool itemProperty = this.dataSource.GetItemProperty<bool>(UncItemSchema.IsFolder, false);
			if (itemProperty)
			{
				return ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, "ipf.documentlibrary.unc");
			}
			Uri itemProperty2 = this.dataSource.GetItemProperty<Uri>(UncItemSchema.Uri);
			string itemClass = Path.GetExtension(itemProperty2.ToString()).ToLowerInvariant();
			return ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, itemClass, "ipm.document");
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000B8A2C File Offset: 0x000B6C2C
		protected virtual bool RenderSharepointDocumentIcon(TextWriter writer)
		{
			if (this.dataSource.GetItemProperty<bool>(DocumentLibraryItemSchema.IsFolder, false))
			{
				return ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, "ipf.documentlibrary.unc");
			}
			Uri itemProperty = this.dataSource.GetItemProperty<Uri>(SharepointDocumentLibraryItemSchema.EncodedAbsoluteUri);
			string itemClass = Path.GetExtension(itemProperty.ToString()).ToLowerInvariant();
			return ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, itemClass, "ipm.document");
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x000B8A92 File Offset: 0x000B6C92
		protected virtual bool IsAssignedTask
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000B8A98 File Offset: 0x000B6C98
		protected bool RenderColumn(TextWriter writer, ColumnId columnId, bool nonBreakingSpaceOnEmpty)
		{
			bool flag = this.InternalRenderColumn(writer, columnId);
			if (!flag && nonBreakingSpaceOnEmpty)
			{
				writer.Write("&nbsp;");
			}
			return flag;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x000B8AC0 File Offset: 0x000B6CC0
		protected virtual bool InternalRenderColumn(TextWriter writer, ColumnId columnId)
		{
			Column column = ListViewColumns.GetColumn(columnId);
			if (columnId <= ColumnId.ADIcon)
			{
				switch (columnId)
				{
				case ColumnId.MailIcon:
				case ColumnId.ContactIcon:
					break;
				case ColumnId.From:
				case ColumnId.To:
				case ColumnId.Subject:
				case ColumnId.Department:
					goto IL_66B;
				case ColumnId.HasAttachment:
				{
					string itemClass = this.dataSource.GetItemClass();
					this.dataSource.GetItemId();
					bool itemProperty = this.dataSource.GetItemProperty<bool>(ItemSchema.HasAttachment, false);
					this.dataSource.GetItemProperty<string>(MessageItemSchema.RequireProtectedPlayOnPhone, string.Empty);
					return ListViewContentsRenderingUtilities.RenderHasAttachments(writer, this.userContext, itemProperty, itemClass);
				}
				case ColumnId.Importance:
					goto IL_17B;
				case ColumnId.DeliveryTime:
				{
					ExDateTime itemProperty2 = this.dataSource.GetItemProperty<ExDateTime>(ItemSchema.ReceivedTime, ExDateTime.MinValue);
					return this.RenderSmartDate(writer, itemProperty2);
				}
				case ColumnId.SentTime:
				{
					ExDateTime itemProperty3 = this.dataSource.GetItemProperty<ExDateTime>(ItemSchema.SentTime, ExDateTime.MinValue);
					return this.RenderSmartDate(writer, itemProperty3);
				}
				case ColumnId.Size:
				{
					int itemProperty4 = this.dataSource.GetItemProperty<int>(ItemSchema.Size, 0);
					Utilities.RenderSizeWithUnits(writer, (long)itemProperty4, true);
					return true;
				}
				default:
					switch (columnId)
					{
					case ColumnId.EmailAddresses:
						if (ObjectClass.IsDistributionList(this.dataSource.GetItemClass()))
						{
							this.RenderSingleEmailAddress(writer, this.dataSource.GetItemProperty<string>(ContactBaseSchema.FileAs, string.Empty), string.Empty, string.Empty, null, EmailAddressIndex.None, RecipientAddress.RecipientAddressFlags.DistributionList);
							return true;
						}
						return this.RenderEmailAddresses(writer);
					case ColumnId.Email1:
					case ColumnId.Email2:
					case ColumnId.Email3:
						goto IL_408;
					case ColumnId.GivenName:
					case ColumnId.Surname:
					case ColumnId.SharepointDocumentDisplayName:
					case ColumnId.SharepointDocumentLastModified:
					case ColumnId.SharepointDocumentModifiedBy:
					case ColumnId.SharepointDocumentCheckedOutTo:
					case ColumnId.UncDocumentDisplayName:
					case ColumnId.UncDocumentLastModified:
						goto IL_66B;
					case ColumnId.Categories:
					case ColumnId.ContactCategories:
						goto IL_34E;
					case ColumnId.SharepointDocumentIcon:
						return this.RenderSharepointDocumentIcon(writer);
					case ColumnId.SharepointDocumentFileSize:
					{
						long itemProperty5 = this.dataSource.GetItemProperty<long>(SharepointDocumentSchema.FileSize, 0L);
						Utilities.RenderSizeWithUnits(writer, itemProperty5, true);
						return true;
					}
					case ColumnId.UncDocumentIcon:
						return this.RenderUncDocumentIcon(writer);
					case ColumnId.UncDocumentLibraryIcon:
						return ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, "ipf.documentlibrary.sharepoint");
					case ColumnId.UncDocumentFileSize:
					{
						long itemProperty6 = this.dataSource.GetItemProperty<long>(UncDocumentSchema.FileSize, 0L);
						Utilities.RenderSizeWithUnits(writer, itemProperty6, true);
						return true;
					}
					case ColumnId.SharepointDocumentLibraryIcon:
						return ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, "ipf.documentlibrary.sharepoint");
					default:
						if (columnId != ColumnId.ADIcon)
						{
							goto IL_66B;
						}
						break;
					}
					break;
				}
			}
			else
			{
				if (columnId == ColumnId.EmailAddressAD)
				{
					return this.RenderADEmailAddress(this.dataSource, writer);
				}
				switch (columnId)
				{
				case ColumnId.YomiFullName:
				{
					StringBuilder stringBuilder = new StringBuilder();
					string itemProperty7 = this.dataSource.GetItemProperty<string>(ContactSchema.YomiLastName, string.Empty);
					if (!string.IsNullOrEmpty(itemProperty7))
					{
						Utilities.HtmlEncode(itemProperty7, stringBuilder);
					}
					string itemProperty8 = this.dataSource.GetItemProperty<string>(ContactSchema.YomiFirstName, string.Empty);
					if (!string.IsNullOrEmpty(itemProperty8))
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(" ");
						}
						Utilities.HtmlEncode(itemProperty8, stringBuilder);
					}
					if (stringBuilder.Length == 0)
					{
						return false;
					}
					writer.Write(stringBuilder.ToString());
					return true;
				}
				case ColumnId.YomiLastName:
				case ColumnId.YomiDisplayNameAD:
				case ColumnId.YomiDepartmentAD:
				case ColumnId.ResourceCapacityAD:
				case ColumnId.FlagStartDate:
				case ColumnId.ContactFlagStartDate:
				case ColumnId.MemberDisplayName:
				case ColumnId.ConversationLastDeliveryTime:
				case ColumnId.ConversationIcon:
				case ColumnId.ConversationSubject:
				case ColumnId.ConversationUnreadCount:
				case ColumnId.ConversationHasAttachment:
				case ColumnId.ConversationSenderList:
					goto IL_66B;
				case ColumnId.FlagDueDate:
				case ColumnId.ContactFlagDueDate:
				case ColumnId.TaskFlag:
				{
					FlagStatus itemProperty9 = this.dataSource.GetItemProperty<FlagStatus>(ItemSchema.FlagStatus, FlagStatus.NotFlagged);
					int itemProperty10 = this.dataSource.GetItemProperty<int>(ItemSchema.ItemColor, int.MinValue);
					ThemeFileId themeFileId = ThemeFileId.FlagEmpty;
					if (itemProperty9 == FlagStatus.NotFlagged)
					{
						string itemClass2 = this.dataSource.GetItemClass();
						if (ObjectClass.IsTask(itemClass2))
						{
							bool itemProperty11 = this.dataSource.GetItemProperty<bool>(ItemSchema.IsComplete, false);
							if (itemProperty11)
							{
								themeFileId = (this.IsAssignedTask ? ThemeFileId.FlagCompleteDisabled : ThemeFileId.FlagComplete);
							}
							else
							{
								themeFileId = (this.IsAssignedTask ? ThemeFileId.FlagDisabled : ThemeFileId.Flag);
							}
						}
					}
					else if (itemProperty10 == -2147483648 && itemProperty9 == FlagStatus.Flagged)
					{
						themeFileId = ThemeFileId.FlagSender;
					}
					else if (itemProperty9 == FlagStatus.Flagged)
					{
						themeFileId = ThemeFileId.Flag;
					}
					else
					{
						themeFileId = ThemeFileId.FlagComplete;
					}
					this.userContext.RenderThemeImage(writer, themeFileId, null, new object[]
					{
						"id=imgFlg"
					});
					return true;
				}
				case ColumnId.TaskIcon:
				case ColumnId.MemberIcon:
					break;
				case ColumnId.MarkCompleteCheckbox:
				{
					bool itemProperty12 = this.dataSource.GetItemProperty<bool>(ItemSchema.IsComplete, false);
					writer.Write("<input id=chkMkCmp type=checkbox class=mkCmp");
					if (this.IsAssignedTask)
					{
						writer.Write(" disabled");
					}
					writer.Write(itemProperty12 ? " checked>" : ">");
					return true;
				}
				case ColumnId.DueDate:
				{
					ExDateTime itemProperty13 = this.dataSource.GetItemProperty<ExDateTime>(TaskSchema.DueDate, ExDateTime.MinValue);
					if (itemProperty13 != ExDateTime.MinValue)
					{
						writer.Write(itemProperty13.ToString(this.userContext.UserOptions.DateFormat));
						return true;
					}
					return false;
				}
				case ColumnId.MemberEmail:
					goto IL_408;
				case ColumnId.DeletedOnTime:
				{
					ExDateTime itemProperty14 = this.dataSource.GetItemProperty<ExDateTime>(StoreObjectSchema.DeletedOnTime, ExDateTime.MinValue);
					return this.RenderWeekdayDateTime(writer, itemProperty14);
				}
				case ColumnId.DumpsterReceivedTime:
				{
					ExDateTime itemProperty15 = this.dataSource.GetItemProperty<ExDateTime>(ItemSchema.ReceivedTime, ExDateTime.MinValue);
					return this.RenderWeekdayDateTime(writer, itemProperty15);
				}
				case ColumnId.ObjectDisplayName:
				{
					string itemProperty16 = this.dataSource.GetItemProperty<string>(FolderSchema.DisplayName, string.Empty);
					if (string.IsNullOrEmpty(itemProperty16))
					{
						itemProperty16 = this.dataSource.GetItemProperty<string>(ItemSchema.Subject, string.Empty);
					}
					if (itemProperty16.Length == 0)
					{
						return false;
					}
					Utilities.HtmlEncode(itemProperty16, writer);
					return true;
				}
				case ColumnId.ObjectIcon:
				{
					VersionedId itemProperty17 = this.dataSource.GetItemProperty<VersionedId>(FolderSchema.Id);
					if (itemProperty17 != null)
					{
						return ListViewContentsRenderingUtilities.RenderItemIcon(writer, this.userContext, ThemeFileId.Folder);
					}
					return this.RenderIcon(writer);
				}
				case ColumnId.ConversationImportance:
					goto IL_17B;
				case ColumnId.ConversationCategories:
					goto IL_34E;
				default:
				{
					if (columnId != ColumnId.IMAddress)
					{
						goto IL_66B;
					}
					string itemProperty18 = this.dataSource.GetItemProperty<string>(ContactSchema.IMAddress, string.Empty);
					this.RenderSingleEmailAddress(writer, itemProperty18, itemProperty18, itemProperty18, null, EmailAddressIndex.None, RecipientAddress.RecipientAddressFlags.None);
					return true;
				}
				}
			}
			return this.RenderIcon(writer);
			IL_17B:
			Importance importance = Importance.Normal;
			object itemProperty19 = this.dataSource.GetItemProperty<object>(ListViewColumns.GetColumn(columnId)[0]);
			if (itemProperty19 is Importance || itemProperty19 is int)
			{
				importance = (Importance)itemProperty19;
			}
			return ListViewContentsRenderingUtilities.RenderImportance(writer, this.UserContext, importance);
			IL_34E:
			this.RenderCategories(writer, columnId == ColumnId.ConversationCategories);
			return true;
			IL_408:
			PropertyDefinition propertyDefinition = ContactSchema.Email1;
			switch (columnId)
			{
			case ColumnId.Email1:
				break;
			case ColumnId.Email2:
				propertyDefinition = ContactSchema.Email2;
				goto IL_447;
			case ColumnId.Email3:
				propertyDefinition = ContactSchema.Email3;
				goto IL_447;
			default:
				if (columnId != ColumnId.MemberEmail)
				{
					goto IL_447;
				}
				break;
			}
			propertyDefinition = ContactSchema.Email1;
			IL_447:
			Participant itemProperty20 = this.dataSource.GetItemProperty<Participant>(propertyDefinition, null);
			if (itemProperty20 == null)
			{
				return false;
			}
			string text = null;
			string text2 = null;
			ContactUtilities.GetParticipantEmailAddress(itemProperty20, out text2, out text);
			if (string.IsNullOrEmpty(text2))
			{
				return false;
			}
			Utilities.HtmlEncode(text2, writer);
			return true;
			IL_66B:
			object itemProperty21 = this.dataSource.GetItemProperty<object>(column[0]);
			if (itemProperty21 is ExDateTime)
			{
				writer.Write(((ExDateTime)itemProperty21).ToString());
			}
			else if (itemProperty21 is DateTime)
			{
				ExDateTime exDateTime = new ExDateTime(this.userContext.TimeZone, (DateTime)itemProperty21);
				writer.Write(exDateTime.ToString());
			}
			else if (itemProperty21 is string)
			{
				string text3 = (string)itemProperty21;
				if (text3.Length == 0)
				{
					return false;
				}
				Utilities.HtmlEncode(text3, writer);
			}
			else if (itemProperty21 is int)
			{
				Utilities.HtmlEncode(((int)itemProperty21).ToString(CultureInfo.CurrentCulture.NumberFormat), writer);
			}
			else if (itemProperty21 is long)
			{
				Utilities.HtmlEncode(((long)itemProperty21).ToString(CultureInfo.CurrentCulture.NumberFormat), writer);
			}
			else if (itemProperty21 is Unlimited<int>)
			{
				if (((Unlimited<int>)itemProperty21).IsUnlimited)
				{
					return false;
				}
				Utilities.HtmlEncode(((Unlimited<int>)itemProperty21).Value.ToString(CultureInfo.CurrentCulture.NumberFormat), writer);
			}
			else
			{
				if (itemProperty21 is PropertyError)
				{
					return false;
				}
				if (itemProperty21 is PropertyError)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x000B9298 File Offset: 0x000B7498
		protected bool RenderADEmailAddress(IListViewDataSource dataSource, TextWriter writer)
		{
			string text = dataSource.GetItemProperty<SmtpAddress>(ADRecipientSchema.PrimarySmtpAddress, SmtpAddress.Empty).ToString();
			if (text.Length == 0)
			{
				return false;
			}
			RecipientAddress.RecipientAddressFlags recipientAddressFlags = RecipientAddress.RecipientAddressFlags.None;
			RecipientDisplayType? itemProperty = dataSource.GetItemProperty<RecipientDisplayType?>(ADRecipientSchema.RecipientDisplayType, null);
			if (itemProperty == RecipientDisplayType.ConferenceRoomMailbox || itemProperty == RecipientDisplayType.SyncedConferenceRoomMailbox)
			{
				recipientAddressFlags |= RecipientAddress.RecipientAddressFlags.Room;
			}
			RecipientType itemProperty2 = dataSource.GetItemProperty<RecipientType>(ADRecipientSchema.RecipientType, RecipientType.Invalid);
			if (Utilities.IsADDistributionList(itemProperty2))
			{
				recipientAddressFlags |= RecipientAddress.RecipientAddressFlags.DistributionList;
			}
			string itemProperty3 = dataSource.GetItemProperty<string>(ADRecipientSchema.LegacyExchangeDN, string.Empty);
			ProxyAddressCollection itemProperty4 = dataSource.GetItemProperty<ProxyAddressCollection>(ADRecipientSchema.EmailAddresses, null);
			string sipUri = InstantMessageUtilities.GetSipUri(itemProperty4);
			string mobilePhoneNumber = Utilities.NormalizePhoneNumber(dataSource.GetItemProperty<string>(ADOrgPersonSchema.MobilePhone, string.Empty));
			this.RenderSingleEmailAddress(writer, dataSource.GetItemProperty<string>(ADRecipientSchema.DisplayName, string.Empty), text, text, itemProperty3, EmailAddressIndex.None, recipientAddressFlags, null, sipUri, mobilePhoneNumber);
			return true;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000B939D File Offset: 0x000B759D
		private static void GetEmailAddressData(Participant participant, out string emailAddress, out string emailAddressForDisplay, out string displayName, out string routingType)
		{
			emailAddressForDisplay = null;
			displayName = null;
			ContactUtilities.GetParticipantEmailAddress(participant, out emailAddressForDisplay, out displayName);
			emailAddress = participant.EmailAddress;
			routingType = participant.RoutingType;
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000B93C0 File Offset: 0x000B75C0
		private void RenderCategories(TextWriter writer, bool conversationMode)
		{
			string[] itemProperty = this.dataSource.GetItemProperty<string[]>(conversationMode ? ConversationItemSchema.ConversationCategories : ItemSchema.Categories, null);
			writer.Write("<span id=\"spanCat\"");
			if (this.userContext.CanActAsOwner && itemProperty != null && 0 < itemProperty.Length)
			{
				writer.Write(" title=\"");
				for (int i = 0; i < itemProperty.Length; i++)
				{
					if (i != 0)
					{
						writer.Write("; ");
					}
					Utilities.HtmlEncode(itemProperty[i], writer);
				}
				writer.Write("\"");
			}
			writer.Write(">");
			int itemColorInt = -1;
			bool isToDoItem = false;
			FlagStatus itemProperty2;
			if (conversationMode)
			{
				itemProperty2 = (FlagStatus)this.dataSource.GetItemProperty<int>(ConversationItemSchema.ConversationFlagStatus, 0);
			}
			else
			{
				itemProperty2 = this.dataSource.GetItemProperty<FlagStatus>(ItemSchema.FlagStatus, FlagStatus.NotFlagged);
				itemColorInt = this.dataSource.GetItemProperty<int>(ItemSchema.ItemColor, -1);
				isToDoItem = this.dataSource.GetItemProperty<bool>(ItemSchema.IsToDoItem, false);
			}
			OwaStoreObjectId folderId = OwaStoreObjectId.CreateFromString(this.dataSource.ContainerId);
			CategorySwatch.RenderViewCategorySwatches(writer, this.userContext, itemProperty, isToDoItem, itemProperty2, itemColorInt, folderId);
			writer.Write("</span>");
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x000B94D4 File Offset: 0x000B76D4
		private bool RenderEmailAddresses(TextWriter writer)
		{
			Dictionary<EmailAddressIndex, StorePropertyDefinition> dictionary = new Dictionary<EmailAddressIndex, StorePropertyDefinition>();
			Dictionary<EmailAddressIndex, Participant> dictionary2 = new Dictionary<EmailAddressIndex, Participant>();
			dictionary.Add(EmailAddressIndex.Email1, ContactSchema.Email1);
			dictionary.Add(EmailAddressIndex.Email2, ContactSchema.Email2);
			dictionary.Add(EmailAddressIndex.Email3, ContactSchema.Email3);
			dictionary.Add(EmailAddressIndex.BusinessFax, ContactSchema.ContactBusinessFax);
			dictionary.Add(EmailAddressIndex.HomeFax, ContactSchema.ContactHomeFax);
			dictionary.Add(EmailAddressIndex.OtherFax, ContactSchema.ContactOtherFax);
			EmailAddressIndex emailAddressIndex = EmailAddressIndex.None;
			foreach (KeyValuePair<EmailAddressIndex, StorePropertyDefinition> keyValuePair in dictionary)
			{
				Participant itemProperty = this.dataSource.GetItemProperty<Participant>(keyValuePair.Value, null);
				if (itemProperty != null && !string.IsNullOrEmpty(itemProperty.EmailAddress))
				{
					emailAddressIndex = keyValuePair.Key;
					dictionary2.Add(keyValuePair.Key, itemProperty);
				}
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			if (dictionary2.Count > 1)
			{
				writer.Write("<select id=ea>");
				foreach (KeyValuePair<EmailAddressIndex, Participant> keyValuePair2 in dictionary2)
				{
					Participant value = keyValuePair2.Value;
					if (value != null && !string.IsNullOrEmpty(value.EmailAddress))
					{
						LegacyListViewContents.GetEmailAddressData(value, out text, out text2, out text3, out text4);
						writer.Write("<option aO=1 dn=\"");
						Utilities.HtmlEncode(text3, writer);
						writer.Write("\" rt=\"");
						if (string.IsNullOrEmpty(text4))
						{
							writer.Write("SMTP");
						}
						else
						{
							writer.Write(text4);
						}
						writer.Write("\" rf=\"");
						writer.Write(0);
						writer.Write("\" em=\"");
						Utilities.HtmlEncode(text, writer);
						writer.Write("\" ei=\"");
						writer.Write((int)keyValuePair2.Key);
						writer.Write("\">");
						Utilities.HtmlEncode(text2, writer);
						writer.Write("</option>");
					}
				}
				writer.Write("</select>");
			}
			else
			{
				if (dictionary2.Count != 1)
				{
					return false;
				}
				Participant participant = dictionary2[emailAddressIndex];
				LegacyListViewContents.GetEmailAddressData(participant, out text, out text2, out text3, out text4);
				this.RenderSingleEmailAddress(writer, text3, text, text2, string.Empty, emailAddressIndex, RecipientAddress.RecipientAddressFlags.None, text4);
			}
			return true;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x000B9730 File Offset: 0x000B7930
		private void RenderSingleEmailAddress(TextWriter writer, string displayName, string emailAddress, string emailAddressForDisplay, string legacyExchangeDN, EmailAddressIndex emailAddressIndex, RecipientAddress.RecipientAddressFlags recipientAddressFlags)
		{
			this.RenderSingleEmailAddress(writer, displayName, emailAddress, emailAddressForDisplay, legacyExchangeDN, emailAddressIndex, recipientAddressFlags, null);
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000B9750 File Offset: 0x000B7950
		private void RenderSingleEmailAddress(TextWriter writer, string displayName, string emailAddress, string emailAddressForDisplay, string legacyExchangeDN, EmailAddressIndex emailAddressIndex, RecipientAddress.RecipientAddressFlags recipientAddressFlags, string routingType)
		{
			this.RenderSingleEmailAddress(writer, displayName, emailAddress, emailAddressForDisplay, legacyExchangeDN, emailAddressIndex, recipientAddressFlags, routingType, null, null);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000B9774 File Offset: 0x000B7974
		internal void RenderSingleEmailAddress(TextWriter writer, string displayName, string emailAddress, string emailAddressForDisplay, string legacyExchangeDN, EmailAddressIndex emailAddressIndex, RecipientAddress.RecipientAddressFlags recipientAddressFlags, string routingType, string sipUri, string mobilePhoneNumber)
		{
			writer.Write("<span id=ea");
			if (this.DataSource is FolderListViewDataSource)
			{
				writer.Write(" aO=1");
			}
			else
			{
				writer.Write(" aO=2");
			}
			writer.Write(" dn=\"");
			Utilities.HtmlEncode(displayName, writer);
			writer.Write("\" rf=");
			int num = (int)recipientAddressFlags;
			Utilities.HtmlEncode(num.ToString(CultureInfo.InvariantCulture), writer);
			writer.Write(" rt=\"");
			if (!string.IsNullOrEmpty(routingType))
			{
				writer.Write(routingType);
			}
			else if (!string.IsNullOrEmpty(legacyExchangeDN))
			{
				writer.Write("EX\" lgDn=\"");
				Utilities.HtmlEncode(legacyExchangeDN, writer);
			}
			else if (recipientAddressFlags == RecipientAddress.RecipientAddressFlags.DistributionList)
			{
				writer.Write("MAPIPDL");
			}
			else
			{
				writer.Write("SMTP");
			}
			writer.Write("\" em=\"");
			Utilities.HtmlEncode(emailAddress, writer);
			writer.Write("\" ei=\"");
			int num2 = (int)emailAddressIndex;
			Utilities.HtmlEncode(num2.ToString(), writer);
			if (!string.IsNullOrEmpty(sipUri))
			{
				writer.Write("\" uri=\"");
				Utilities.HtmlEncode(sipUri, writer);
			}
			if (!string.IsNullOrEmpty(mobilePhoneNumber))
			{
				writer.Write("\" mo=\"");
				Utilities.HtmlEncode(mobilePhoneNumber, writer);
			}
			writer.Write("\">");
			Utilities.HtmlEncode(emailAddressForDisplay, writer);
			writer.Write("</span>");
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000B98C0 File Offset: 0x000B7AC0
		public bool RenderWeekdayDateTime(TextWriter writer, ExDateTime date)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (date == ExDateTime.MinValue)
			{
				return false;
			}
			string text = date.ToString(this.userContext.UserOptions.GetWeekdayDateTimeFormat(false));
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			writer.Write("<span>");
			Utilities.HtmlEncode(text, writer);
			writer.Write("</span>");
			return true;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000B992B File Offset: 0x000B7B2B
		protected bool RenderSmartDate(TextWriter writer, ExDateTime date)
		{
			return ListViewContentsRenderingUtilities.RenderSmartDate(writer, this.userContext, date);
		}

		// Token: 0x04001731 RID: 5937
		protected const string ItemListTableId = "tblIL";

		// Token: 0x04001732 RID: 5938
		protected const string Unselected = "us";

		// Token: 0x04001733 RID: 5939
		protected const string VlvRow = "vr";

		// Token: 0x04001734 RID: 5940
		private IListViewDataSource dataSource;

		// Token: 0x04001735 RID: 5941
		private UserContext userContext;

		// Token: 0x04001736 RID: 5942
		private bool isForVirtualListView;

		// Token: 0x04001737 RID: 5943
		private Hashtable properties = new Hashtable();
	}
}
