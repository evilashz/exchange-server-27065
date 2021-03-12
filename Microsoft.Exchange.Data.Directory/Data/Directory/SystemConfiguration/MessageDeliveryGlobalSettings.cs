using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004B6 RID: 1206
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class MessageDeliveryGlobalSettings : ADConfigurationObject
	{
		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x000D7639 File Offset: 0x000D5839
		internal override ADObjectSchema Schema
		{
			get
			{
				return MessageDeliveryGlobalSettings.schema;
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000D7640 File Offset: 0x000D5840
		// (set) Token: 0x06003709 RID: 14089 RVA: 0x000D7652 File Offset: 0x000D5852
		public Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MessageDeliveryGlobalSettingsSchema.MaxReceiveSize];
			}
			internal set
			{
				this[MessageDeliveryGlobalSettingsSchema.MaxReceiveSize] = value;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000D7665 File Offset: 0x000D5865
		// (set) Token: 0x0600370B RID: 14091 RVA: 0x000D7677 File Offset: 0x000D5877
		public Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MessageDeliveryGlobalSettingsSchema.MaxSendSize];
			}
			internal set
			{
				this[MessageDeliveryGlobalSettingsSchema.MaxSendSize] = value;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000D768A File Offset: 0x000D588A
		// (set) Token: 0x0600370D RID: 14093 RVA: 0x000D769C File Offset: 0x000D589C
		public Unlimited<int> MaxRecipientEnvelopeLimit
		{
			get
			{
				return (Unlimited<int>)this[MessageDeliveryGlobalSettingsSchema.MaxRecipientEnvelopeLimit];
			}
			internal set
			{
				this[MessageDeliveryGlobalSettingsSchema.MaxRecipientEnvelopeLimit] = value;
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x000D76AF File Offset: 0x000D58AF
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MessageDeliveryGlobalSettings.mostDerivedClass;
			}
		}

		// Token: 0x04002533 RID: 9523
		private static MessageDeliveryGlobalSettingsSchema schema = ObjectSchema.GetInstance<MessageDeliveryGlobalSettingsSchema>();

		// Token: 0x04002534 RID: 9524
		private static string mostDerivedClass = "msExchMessageDeliveryConfig";

		// Token: 0x04002535 RID: 9525
		public static readonly string DefaultName = "Message Delivery";
	}
}
