using System;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200003E RID: 62
	internal sealed class EditContactHelper : DisposeTrackableBase
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000F08D File Offset: 0x0000D28D
		public EditContactHelper(UserContext userContext, HttpRequest httpRequest, bool isSaving)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			this.userContext = userContext;
			this.httpRequest = httpRequest;
			this.GetContact(isSaving);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000F0C6 File Offset: 0x0000D2C6
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.contact != null)
			{
				this.contact.Dispose();
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000F0DE File Offset: 0x0000D2DE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EditContactHelper>(this);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		private void GetContact(bool isSaving)
		{
			this.contact = null;
			this.isExistingItem = false;
			string text = this.GetQueryStringParameter("id") ?? this.GetFormParameterStringValue("hidid");
			if (isSaving)
			{
				string formParameterStringValue = this.GetFormParameterStringValue("hidchk");
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(formParameterStringValue))
				{
					VersionedId storeId = Utilities.CreateItemId(this.userContext.MailboxSession, text, formParameterStringValue);
					this.contact = Utilities.GetItem<Contact>(this.userContext, storeId, new PropertyDefinition[0]);
					this.folderId = this.contact.ParentId;
					this.isExistingItem = true;
					return;
				}
				this.MakeFolderId();
				this.contact = Contact.Create(this.userContext.MailboxSession, this.folderId);
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
					return;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(text))
				{
					this.contact = Utilities.GetItem<Contact>(this.userContext, Utilities.CreateStoreObjectId(this.userContext.MailboxSession, text), ContactUtilities.PrefetchProperties);
					this.folderId = this.contact.ParentId;
					return;
				}
				this.MakeFolderId();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000F204 File Offset: 0x0000D404
		private void MakeFolderId()
		{
			string text = this.GetFormParameterStringValue("hidfldid") ?? this.GetQueryStringParameter("fId");
			if (!string.IsNullOrEmpty(text))
			{
				this.folderId = Utilities.CreateStoreObjectId(this.userContext.MailboxSession, text);
				return;
			}
			this.folderId = this.userContext.ContactsFolderId;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000F25D File Offset: 0x0000D45D
		public bool IsPropertiesChanged
		{
			get
			{
				return this.isPropertiesChanged;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000F265 File Offset: 0x0000D465
		public Contact Contact
		{
			get
			{
				return this.contact;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000F26D File Offset: 0x0000D46D
		public bool IsExistingItem
		{
			get
			{
				return this.isExistingItem;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000F275 File Offset: 0x0000D475
		public StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000F27D File Offset: 0x0000D47D
		public string GetStringValue(ContactPropertyInfo propertyInfo)
		{
			return this.GetStringValue(propertyInfo, string.Empty);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000F28C File Offset: 0x0000D48C
		public string GetStringValue(ContactPropertyInfo propertyInfo, string defaultValue)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			string formParameterStringValue = this.GetFormParameterStringValue(propertyInfo.Id);
			if (this.Contact != null)
			{
				string contactPropertyStringValue = this.GetContactPropertyStringValue(propertyInfo);
				if (formParameterStringValue != null && !string.Equals(formParameterStringValue, contactPropertyStringValue))
				{
					this.isPropertiesChanged = true;
				}
				return (formParameterStringValue ?? contactPropertyStringValue) ?? defaultValue;
			}
			if (formParameterStringValue != null && formParameterStringValue != defaultValue)
			{
				this.isPropertiesChanged = true;
			}
			return formParameterStringValue ?? defaultValue;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		public string GetNotes()
		{
			string formParameterStringValue = this.GetFormParameterStringValue("notes");
			if (this.Contact != null)
			{
				string itemBody = ItemUtility.GetItemBody(this.Contact, BodyFormat.TextPlain);
				if (formParameterStringValue != null && !string.Equals(formParameterStringValue, itemBody))
				{
					this.isPropertiesChanged = true;
				}
				return (formParameterStringValue ?? itemBody) ?? string.Empty;
			}
			if (!string.IsNullOrEmpty(formParameterStringValue))
			{
				this.isPropertiesChanged = true;
			}
			return formParameterStringValue ?? string.Empty;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000F368 File Offset: 0x0000D568
		public int GetIntValue(ContactPropertyInfo propertyInfo, int defaultValue)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			int? formParameterIntValue = this.GetFormParameterIntValue(propertyInfo);
			if (this.Contact != null)
			{
				int? contactPropertyIntValue = this.GetContactPropertyIntValue(propertyInfo);
				if (formParameterIntValue != null && formParameterIntValue != contactPropertyIntValue)
				{
					this.isPropertiesChanged = true;
				}
				int? num = formParameterIntValue ?? contactPropertyIntValue;
				if (num == null)
				{
					return defaultValue;
				}
				return num.GetValueOrDefault();
			}
			else
			{
				if (formParameterIntValue != null && formParameterIntValue != defaultValue)
				{
					this.isPropertiesChanged = true;
				}
				int? num2 = formParameterIntValue;
				if (num2 == null)
				{
					return defaultValue;
				}
				return num2.GetValueOrDefault();
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000F44C File Offset: 0x0000D64C
		private string GetContactPropertyStringValue(ContactPropertyInfo property)
		{
			string result = string.Empty;
			if (this.Contact == null)
			{
				return result;
			}
			string result2 = null;
			string result3 = null;
			if (property == ContactUtilities.Email1EmailAddress || property == ContactUtilities.Email2EmailAddress || property == ContactUtilities.Email3EmailAddress)
			{
				EmailAddressIndex emailPropertyIndex = ContactUtilities.GetEmailPropertyIndex(property);
				ContactUtilities.GetContactEmailAddress(this.Contact, emailPropertyIndex, out result2, out result3);
				return result2;
			}
			if (property == ContactUtilities.Email1DisplayName || property == ContactUtilities.Email2DisplayName || property == ContactUtilities.Email3DisplayName)
			{
				EmailAddressIndex emailPropertyIndex2 = ContactUtilities.GetEmailPropertyIndex(property);
				ContactUtilities.GetContactEmailAddress(this.Contact, emailPropertyIndex2, out result2, out result3);
				return result3;
			}
			string text = this.Contact.TryGetProperty(property.PropertyDefinition) as string;
			if (text != null)
			{
				result = text;
			}
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		private int? GetContactPropertyIntValue(ContactPropertyInfo propertyInfo)
		{
			if (propertyInfo == ContactUtilities.FileAsId)
			{
				return new int?((int)ContactUtilities.GetFileAs(this.Contact));
			}
			object obj = this.Contact.TryGetProperty(propertyInfo.PropertyDefinition);
			if (obj != null && obj is int)
			{
				return new int?((int)obj);
			}
			return null;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000F54C File Offset: 0x0000D74C
		private int? GetFormParameterIntValue(ContactPropertyInfo property)
		{
			return this.GetFormParameterIntValue(property.Id);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000F55A File Offset: 0x0000D75A
		public string GetFormParameterStringValue(string name)
		{
			if (!Utilities.IsPostRequest(this.httpRequest))
			{
				return null;
			}
			return Utilities.GetFormParameter(this.httpRequest, name, false);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000F578 File Offset: 0x0000D778
		public string GetQueryStringParameter(string name)
		{
			return Utilities.GetQueryStringParameter(this.httpRequest, name, false);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000F588 File Offset: 0x0000D788
		private int? GetFormParameterIntValue(string name)
		{
			if (!Utilities.IsPostRequest(this.httpRequest))
			{
				return null;
			}
			int value;
			if (int.TryParse(this.GetFormParameterStringValue(name), out value))
			{
				return new int?(value);
			}
			return null;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F5CC File Offset: 0x0000D7CC
		public SaveResult SaveContact()
		{
			foreach (ContactPropertyInfo contactPropertyInfo in ContactUtilities.AllContactProperties)
			{
				if (contactPropertyInfo == ContactUtilities.FileAsId || contactPropertyInfo == ContactUtilities.PostalAddressId || contactPropertyInfo == ContactUtilities.DefaultPhoneNumber)
				{
					this.SetInt(contactPropertyInfo);
				}
				else if (contactPropertyInfo == ContactUtilities.WorkAddressStreet || contactPropertyInfo == ContactUtilities.HomeStreet || contactPropertyInfo == ContactUtilities.OtherStreet)
				{
					this.SetText(contactPropertyInfo, 256);
				}
				else
				{
					this.SetText(contactPropertyInfo);
				}
			}
			this.SetEmail(ContactUtilities.Email1EmailAddress);
			this.SetEmail(ContactUtilities.Email2EmailAddress);
			this.SetEmail(ContactUtilities.Email3EmailAddress);
			this.SetNotes();
			AttachmentUtility.PromoteInlineAttachments(this.contact);
			ConflictResolutionResult conflictResolutionResult = this.Contact.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus != SaveResult.IrresolvableConflict && this.IsExistingItem && Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsUpdated.Increment();
			}
			return conflictResolutionResult.SaveStatus;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		private void SetText(ContactPropertyInfo propertyInfo)
		{
			string text = this.GetFormParameterStringValue(propertyInfo.Id);
			if (Utilities.WhiteSpaceOnlyOrNullEmpty(text))
			{
				text = string.Empty;
			}
			this.Contact[propertyInfo.PropertyDefinition] = text;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		private void SetText(ContactPropertyInfo propertyInfo, int maxLength)
		{
			string text = this.GetFormParameterStringValue(propertyInfo.Id);
			if (Utilities.WhiteSpaceOnlyOrNullEmpty(text))
			{
				text = string.Empty;
			}
			if (text.Length > maxLength)
			{
				text = text.Substring(0, maxLength);
			}
			this.Contact[propertyInfo.PropertyDefinition] = text;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F730 File Offset: 0x0000D930
		private void SetInt(ContactPropertyInfo propertyInfo)
		{
			int num;
			if (int.TryParse(this.GetFormParameterStringValue(propertyInfo.Id), out num))
			{
				this.Contact[propertyInfo.PropertyDefinition] = num;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F76C File Offset: 0x0000D96C
		private void SetEmail(ContactPropertyInfo propertyInfo)
		{
			ContactPropertyInfo emailDisplayAsProperty = ContactUtilities.GetEmailDisplayAsProperty(propertyInfo);
			string text = this.GetFormParameterStringValue(propertyInfo.Id);
			string text2 = this.GetFormParameterStringValue(emailDisplayAsProperty.Id);
			EmailAddressIndex emailPropertyIndex = ContactUtilities.GetEmailPropertyIndex(propertyInfo);
			if (Utilities.WhiteSpaceOnlyOrNullEmpty(text))
			{
				text = null;
			}
			if (Utilities.WhiteSpaceOnlyOrNullEmpty(text2))
			{
				text2 = null;
			}
			ContactUtilities.SetContactEmailAddress(this.Contact, emailPropertyIndex, text, text2);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		private void SetNotes()
		{
			string formParameterStringValue = this.GetFormParameterStringValue("notes");
			if (formParameterStringValue != null)
			{
				string itemBody = ItemUtility.GetItemBody(this.contact, BodyFormat.TextPlain);
				if (formParameterStringValue.Trim() != itemBody.Trim())
				{
					ItemUtility.SetItemBody(this.contact, BodyFormat.TextPlain, formParameterStringValue);
				}
			}
		}

		// Token: 0x0400013C RID: 316
		public const string NotesFormParameter = "notes";

		// Token: 0x0400013D RID: 317
		public const string ContactIdFormParameter = "hidid";

		// Token: 0x0400013E RID: 318
		public const string ContactIdQueryParameter = "id";

		// Token: 0x0400013F RID: 319
		public const string FolderIdQueryParameter = "fId";

		// Token: 0x04000140 RID: 320
		public const string FolderIdFormParameter = "hidfldid";

		// Token: 0x04000141 RID: 321
		public const string ChangeKeyFormParameter = "hidchk";

		// Token: 0x04000142 RID: 322
		public const int MaxAddressLength = 256;

		// Token: 0x04000143 RID: 323
		private readonly UserContext userContext;

		// Token: 0x04000144 RID: 324
		private readonly HttpRequest httpRequest;

		// Token: 0x04000145 RID: 325
		private StoreObjectId folderId;

		// Token: 0x04000146 RID: 326
		private Contact contact;

		// Token: 0x04000147 RID: 327
		private bool isExistingItem;

		// Token: 0x04000148 RID: 328
		private bool isPropertiesChanged;
	}
}
