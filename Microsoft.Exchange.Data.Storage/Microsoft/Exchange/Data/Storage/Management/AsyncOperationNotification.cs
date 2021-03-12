using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009B5 RID: 2485
	[Serializable]
	public class AsyncOperationNotification : EwsStoreObject
	{
		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x0017E320 File Offset: 0x0017C520
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return AsyncOperationNotification.schema;
			}
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06005BAB RID: 23467 RVA: 0x0017E327 File Offset: 0x0017C527
		internal override SearchFilter ItemClassFilter
		{
			get
			{
				return new SearchFilter.ContainsSubstring(ItemSchema.ItemClass, "IPM.Notification.", 1, 0);
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x0017E33A File Offset: 0x0017C53A
		// (set) Token: 0x06005BAD RID: 23469 RVA: 0x0017E34C File Offset: 0x0017C54C
		public KeyValuePair<string, LocalizedString>[] ExtendedAttributes
		{
			get
			{
				return (KeyValuePair<string, LocalizedString>[])this[AsyncOperationNotificationSchema.ExtendedAttributes];
			}
			set
			{
				this[AsyncOperationNotificationSchema.ExtendedAttributes] = value;
			}
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x06005BAE RID: 23470 RVA: 0x0017E35A File Offset: 0x0017C55A
		public DateTime? LastModified
		{
			get
			{
				return (DateTime?)this[AsyncOperationNotificationSchema.LastModified];
			}
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x06005BAF RID: 23471 RVA: 0x0017E36C File Offset: 0x0017C56C
		// (set) Token: 0x06005BB0 RID: 23472 RVA: 0x0017E37E File Offset: 0x0017C57E
		public LocalizedString Message
		{
			get
			{
				return (LocalizedString)this[AsyncOperationNotificationSchema.Message];
			}
			internal set
			{
				this[AsyncOperationNotificationSchema.Message] = value;
			}
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x06005BB1 RID: 23473 RVA: 0x0017E391 File Offset: 0x0017C591
		// (set) Token: 0x06005BB2 RID: 23474 RVA: 0x0017E3A3 File Offset: 0x0017C5A3
		public int? PercentComplete
		{
			get
			{
				return (int?)this[AsyncOperationNotificationSchema.PercentComplete];
			}
			internal set
			{
				this[AsyncOperationNotificationSchema.PercentComplete] = value;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06005BB3 RID: 23475 RVA: 0x0017E3B6 File Offset: 0x0017C5B6
		public string StartedBy
		{
			get
			{
				return (string)this[AsyncOperationNotificationSchema.StartedBy];
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06005BB4 RID: 23476 RVA: 0x0017E3C8 File Offset: 0x0017C5C8
		// (set) Token: 0x06005BB5 RID: 23477 RVA: 0x0017E3DA File Offset: 0x0017C5DA
		internal ADRecipientOrAddress StartedByValue
		{
			get
			{
				return (ADRecipientOrAddress)this[AsyncOperationNotificationSchema.StartedByValue];
			}
			set
			{
				this[AsyncOperationNotificationSchema.StartedByValue] = value;
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x06005BB6 RID: 23478 RVA: 0x0017E3E8 File Offset: 0x0017C5E8
		public DateTime? StartTime
		{
			get
			{
				return (DateTime?)this[AsyncOperationNotificationSchema.StartTime];
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x06005BB7 RID: 23479 RVA: 0x0017E3FA File Offset: 0x0017C5FA
		// (set) Token: 0x06005BB8 RID: 23480 RVA: 0x0017E40C File Offset: 0x0017C60C
		public AsyncOperationStatus Status
		{
			get
			{
				return (AsyncOperationStatus)this[AsyncOperationNotificationSchema.Status];
			}
			internal set
			{
				this[AsyncOperationNotificationSchema.Status] = value;
			}
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x06005BB9 RID: 23481 RVA: 0x0017E41F File Offset: 0x0017C61F
		// (set) Token: 0x06005BBA RID: 23482 RVA: 0x0017E431 File Offset: 0x0017C631
		public LocalizedString DisplayName
		{
			get
			{
				return (LocalizedString)this[AsyncOperationNotificationSchema.DisplayName];
			}
			internal set
			{
				this[AsyncOperationNotificationSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06005BBB RID: 23483 RVA: 0x0017E444 File Offset: 0x0017C644
		// (set) Token: 0x06005BBC RID: 23484 RVA: 0x0017E456 File Offset: 0x0017C656
		public AsyncOperationType Type
		{
			get
			{
				return (AsyncOperationType)this[AsyncOperationNotificationSchema.Type];
			}
			internal set
			{
				this[AsyncOperationNotificationSchema.Type] = value;
			}
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06005BBD RID: 23485 RVA: 0x0017E469 File Offset: 0x0017C669
		// (set) Token: 0x06005BBE RID: 23486 RVA: 0x0017E47B File Offset: 0x0017C67B
		public MultiValuedProperty<ADRecipientOrAddress> NotificationEmails
		{
			get
			{
				return (MultiValuedProperty<ADRecipientOrAddress>)this[AsyncOperationNotificationSchema.NotificationEmails];
			}
			internal set
			{
				this[AsyncOperationNotificationSchema.NotificationEmails] = value;
			}
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06005BBF RID: 23487 RVA: 0x0017E489 File Offset: 0x0017C689
		internal bool IsSettingsObject
		{
			get
			{
				return AsyncOperationNotification.IsSettingsObjectId(base.AlternativeId);
			}
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06005BC0 RID: 23488 RVA: 0x0017E496 File Offset: 0x0017C696
		// (set) Token: 0x06005BC1 RID: 23489 RVA: 0x0017E4A8 File Offset: 0x0017C6A8
		internal bool IsNotificationEmailFromTaskSent
		{
			get
			{
				return (bool)this[AsyncOperationNotificationSchema.IsNotificationEmailFromTaskSent];
			}
			set
			{
				this[AsyncOperationNotificationSchema.IsNotificationEmailFromTaskSent] = value;
			}
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x0017E4BB File Offset: 0x0017C6BB
		public static bool IsSettingsObjectId(string id)
		{
			return AsyncOperationNotificationDataProvider.SettingsObjectIdentityMap.ContainsValue(id);
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x0017E4C8 File Offset: 0x0017C6C8
		public LocalizedString GetExtendedAttributeValue(string key)
		{
			LocalizedString result;
			if (this.TryGetExtendedAttributeValue(key, out result))
			{
				return result;
			}
			throw new KeyNotFoundException(key);
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x0017E4E8 File Offset: 0x0017C6E8
		public bool TryGetExtendedAttributeValue(string key, out LocalizedString result)
		{
			result = LocalizedString.Empty;
			bool result2 = false;
			if (this.ExtendedAttributes != null)
			{
				foreach (KeyValuePair<string, LocalizedString> keyValuePair in this.ExtendedAttributes)
				{
					if (key == keyValuePair.Key)
					{
						result = keyValuePair.Value;
						result2 = true;
					}
				}
			}
			return result2;
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x0017E54C File Offset: 0x0017C74C
		internal static object GetAsyncOperationType(IPropertyBag propertyPag)
		{
			AsyncOperationType asyncOperationType = AsyncOperationType.Unknown;
			string text = (string)propertyPag[EwsStoreObjectSchema.ItemClass];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			if (text.StartsWith("IPM.Notification."))
			{
				Enum.TryParse<AsyncOperationType>(text.Substring("IPM.Notification.".Length), true, out asyncOperationType);
			}
			return asyncOperationType;
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x0017E5A2 File Offset: 0x0017C7A2
		internal static void SetAsyncOperationType(object value, IPropertyBag propertyPag)
		{
			propertyPag[EwsStoreObjectSchema.ItemClass] = "IPM.Notification." + ((AsyncOperationType)value).ToString();
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x0017E5CC File Offset: 0x0017C7CC
		internal static object GetStartedBy(IPropertyBag propertyPag)
		{
			ADRecipientOrAddress adrecipientOrAddress = (ADRecipientOrAddress)propertyPag[AsyncOperationNotificationSchema.StartedByValue];
			string result;
			if (adrecipientOrAddress != null)
			{
				if ((result = adrecipientOrAddress.DisplayName) == null)
				{
					return adrecipientOrAddress.Address;
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x0017E608 File Offset: 0x0017C808
		internal static void SetNotificationEmails(Item item, object value)
		{
			if (value != null)
			{
				EmailAddressCollection toRecipients = ((EmailMessage)item).ToRecipients;
				IEnumerable<string> source = (IEnumerable<string>)value;
				toRecipients.Clear();
				toRecipients.AddRange((from x in source
				select new EmailAddress(x)).ToArray<EmailAddress>());
			}
		}

		// Token: 0x0400327E RID: 12926
		internal const string ItemClassPrefix = "IPM.Notification.";

		// Token: 0x0400327F RID: 12927
		private static ObjectSchema schema = new AsyncOperationNotificationSchema();
	}
}
