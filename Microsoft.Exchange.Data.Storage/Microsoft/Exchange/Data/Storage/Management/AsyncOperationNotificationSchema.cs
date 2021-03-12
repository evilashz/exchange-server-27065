using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009B4 RID: 2484
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AsyncOperationNotificationSchema : EwsStoreObjectSchema
	{
		// Token: 0x04003271 RID: 12913
		public static readonly EwsStoreObjectPropertyDefinition ExtendedAttributes = new EwsStoreObjectPropertyDefinition("ExtendedAttributes", ExchangeObjectVersion.Exchange2007, typeof(KeyValuePair<string, LocalizedString>[]), PropertyDefinitionFlags.ReturnOnBind, null, null, ExtendedEwsStoreObjectSchema.ExtendedAttributes);

		// Token: 0x04003272 RID: 12914
		public static readonly EwsStoreObjectPropertyDefinition LastModified = new EwsStoreObjectPropertyDefinition("LastModified", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.ReadOnly, null, null, ItemSchema.LastModifiedTime);

		// Token: 0x04003273 RID: 12915
		public static readonly EwsStoreObjectPropertyDefinition Message = new EwsStoreObjectPropertyDefinition("Message", ExchangeObjectVersion.Exchange2007, typeof(LocalizedString), PropertyDefinitionFlags.ReturnOnBind, LocalizedString.Empty, LocalizedString.Empty, ExtendedEwsStoreObjectSchema.Message);

		// Token: 0x04003274 RID: 12916
		public static readonly EwsStoreObjectPropertyDefinition PercentComplete = new EwsStoreObjectPropertyDefinition("PercentComplete", ExchangeObjectVersion.Exchange2007, typeof(int?), PropertyDefinitionFlags.None, null, null, ExtendedEwsStoreObjectSchema.PercentComplete);

		// Token: 0x04003275 RID: 12917
		public static readonly EwsStoreObjectPropertyDefinition StartedByValue = new EwsStoreObjectPropertyDefinition("StartedByValue", ExchangeObjectVersion.Exchange2007, typeof(ADRecipientOrAddress), PropertyDefinitionFlags.None, null, null, EmailMessageSchema.From, delegate(Item item, object value)
		{
			((EmailMessage)item).From = (EmailAddress)value;
		});

		// Token: 0x04003276 RID: 12918
		public static readonly SimplePropertyDefinition StartedBy = new SimplePropertyDefinition("StartedBy", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, string.Empty, string.Empty, Array<PropertyDefinitionConstraint>.Empty, Array<PropertyDefinitionConstraint>.Empty, new ProviderPropertyDefinition[]
		{
			AsyncOperationNotificationSchema.StartedByValue
		}, null, new GetterDelegate(AsyncOperationNotification.GetStartedBy), null);

		// Token: 0x04003277 RID: 12919
		public static readonly EwsStoreObjectPropertyDefinition StartTime = new EwsStoreObjectPropertyDefinition("StartTime", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.ReadOnly, null, null, ItemSchema.DateTimeCreated);

		// Token: 0x04003278 RID: 12920
		public static readonly EwsStoreObjectPropertyDefinition Status = new EwsStoreObjectPropertyDefinition("Status", ExchangeObjectVersion.Exchange2007, typeof(AsyncOperationStatus), PropertyDefinitionFlags.PersistDefaultValue, AsyncOperationStatus.Queued, AsyncOperationStatus.Queued, ExtendedEwsStoreObjectSchema.Status);

		// Token: 0x04003279 RID: 12921
		public static readonly EwsStoreObjectPropertyDefinition DisplayName = new EwsStoreObjectPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2007, typeof(LocalizedString), PropertyDefinitionFlags.ReturnOnBind, LocalizedString.Empty, LocalizedString.Empty, ExtendedEwsStoreObjectSchema.DisplayName);

		// Token: 0x0400327A RID: 12922
		public static readonly EwsStoreObjectPropertyDefinition NotificationEmails = new EwsStoreObjectPropertyDefinition("NotificationEmails", ExchangeObjectVersion.Exchange2007, typeof(ADRecipientOrAddress), PropertyDefinitionFlags.MultiValued, null, null, EmailMessageSchema.ToRecipients, new Action<Item, object>(AsyncOperationNotification.SetNotificationEmails));

		// Token: 0x0400327B RID: 12923
		public static readonly EwsStoreObjectPropertyDefinition IsNotificationEmailFromTaskSent = new EwsStoreObjectPropertyDefinition("IsNotificationEmailFromTaskSent", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, false, ExtendedEwsStoreObjectSchema.IsNotificationEmailFromTaskSent);

		// Token: 0x0400327C RID: 12924
		public static readonly SimplePropertyDefinition Type = new SimplePropertyDefinition("Type", ExchangeObjectVersion.Exchange2007, typeof(AsyncOperationType), PropertyDefinitionFlags.Calculated | PropertyDefinitionFlags.Mandatory, AsyncOperationType.Unknown, AsyncOperationType.Unknown, Array<PropertyDefinitionConstraint>.Empty, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(AsyncOperationType))
		}, new ProviderPropertyDefinition[]
		{
			EwsStoreObjectSchema.ItemClass
		}, null, new GetterDelegate(AsyncOperationNotification.GetAsyncOperationType), new SetterDelegate(AsyncOperationNotification.SetAsyncOperationType));
	}
}
