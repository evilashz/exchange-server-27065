using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Mobility;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	public class PushNotificationSubscription : ConfigurableObject
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000E40A File Offset: 0x0000C60A
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return PushNotificationSubscription.schema;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000E411 File Offset: 0x0000C611
		public PushNotificationSubscription() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000E420 File Offset: 0x0000C620
		internal PushNotificationSubscription(ADObjectId userId, VersionedId versionId, string subscriptionId, string serializedSubscription) : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(PushNotificationSubscriptionSchema.SubscriptionId, subscriptionId);
			if (versionId != null && userId != null)
			{
				this.propertyBag.SetField(PushNotificationSubscriptionSchema.SubscriptionStoreId, new PushNotificationStoreId(userId, versionId.ObjectId, this.SubscriptionId));
			}
			try
			{
				this.propertyBag.SetField(PushNotificationSubscriptionSchema.DeserializedSubscription, PushNotificationServerSubscription.FromJson(serializedSubscription));
			}
			catch (SerializationException ex)
			{
				this.SerializationError = new PropertyValidationError(Strings.ErrorDeserializingSubscription(serializedSubscription, ex.Message), PushNotificationSubscriptionSchema.DeserializedSubscription, serializedSubscription);
			}
			this.propertyBag.ResetChangeTracking();
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		private ValidationError SerializationError { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000E4DD File Offset: 0x0000C6DD
		public string AppId
		{
			get
			{
				return (string)this[PushNotificationSubscriptionSchema.AppId];
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000E4EF File Offset: 0x0000C6EF
		public string DeviceNotificationId
		{
			get
			{
				return (string)this[PushNotificationSubscriptionSchema.DeviceNotificationId];
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000E501 File Offset: 0x0000C701
		public string DeviceNotificationType
		{
			get
			{
				return (string)this[PushNotificationSubscriptionSchema.DeviceNotificationType];
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000E513 File Offset: 0x0000C713
		public long? InboxUnreadCount
		{
			get
			{
				return new long?((long)this[PushNotificationSubscriptionSchema.InboxUnreadCount]);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000E52A File Offset: 0x0000C72A
		public PushNotificationStoreId SubscriptionStoreId
		{
			get
			{
				return (PushNotificationStoreId)this[PushNotificationSubscriptionSchema.SubscriptionStoreId];
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000E53C File Offset: 0x0000C73C
		public DateTime? LastSubscriptionUpdate
		{
			get
			{
				return (DateTime?)this[PushNotificationSubscriptionSchema.LastSubscriptionUpdate];
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000E54E File Offset: 0x0000C74E
		public string SubscriptionId
		{
			get
			{
				return (string)this[PushNotificationSubscriptionSchema.SubscriptionId];
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000E560 File Offset: 0x0000C760
		internal static object LastSubscriptionUpdateGetter(IPropertyBag propertyBag)
		{
			PushNotificationServerSubscription pushNotificationServerSubscription = PushNotificationSubscription.DeserializedSubscriptionGetter(propertyBag);
			return (pushNotificationServerSubscription == null) ? null : new DateTime?(pushNotificationServerSubscription.LastSubscriptionUpdate);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000E594 File Offset: 0x0000C794
		internal static object AppIdGetter(IPropertyBag propertyBag)
		{
			PushNotificationServerSubscription pushNotificationServerSubscription = PushNotificationSubscription.DeserializedSubscriptionGetter(propertyBag);
			if (pushNotificationServerSubscription != null)
			{
				return pushNotificationServerSubscription.AppId;
			}
			return null;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		internal static object DeviceNotificationIdGetter(IPropertyBag propertyBag)
		{
			PushNotificationServerSubscription pushNotificationServerSubscription = PushNotificationSubscription.DeserializedSubscriptionGetter(propertyBag);
			if (pushNotificationServerSubscription != null)
			{
				return pushNotificationServerSubscription.DeviceNotificationId;
			}
			return null;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		internal static object DeviceNotificationTypeGetter(IPropertyBag propertyBag)
		{
			PushNotificationServerSubscription pushNotificationServerSubscription = PushNotificationSubscription.DeserializedSubscriptionGetter(propertyBag);
			if (pushNotificationServerSubscription != null)
			{
				return pushNotificationServerSubscription.DeviceNotificationType;
			}
			return null;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		internal static object InboxUnreadCountGetter(IPropertyBag propertyBag)
		{
			PushNotificationServerSubscription pushNotificationServerSubscription = PushNotificationSubscription.DeserializedSubscriptionGetter(propertyBag);
			return (pushNotificationServerSubscription == null) ? null : pushNotificationServerSubscription.InboxUnreadCount;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000E621 File Offset: 0x0000C821
		private static PushNotificationServerSubscription DeserializedSubscriptionGetter(IPropertyBag propertyBag)
		{
			return (PushNotificationServerSubscription)propertyBag[PushNotificationSubscriptionSchema.DeserializedSubscription];
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000E634 File Offset: 0x0000C834
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.SubscriptionStoreId == null)
			{
				errors.Add(new PropertyValidationError(Strings.NullSubscriptionStoreId, PushNotificationSubscriptionSchema.SubscriptionStoreId, this.SubscriptionStoreId));
			}
			if (this.SerializationError != null)
			{
				errors.Add(this.SerializationError);
			}
			base.ValidateRead(errors);
		}

		// Token: 0x04000105 RID: 261
		private static PushNotificationSubscriptionSchema schema = ObjectSchema.GetInstance<PushNotificationSubscriptionSchema>();
	}
}
