using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public sealed class ApnsFeedbackManagerSettings : RegistryObject
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006BE2 File Offset: 0x00004DE2
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public int ExpirationThresholdInMilliseconds
		{
			get
			{
				return (int)this[ApnsFeedbackManagerSettings.Schema.ExpirationThresholdInMilliseconds];
			}
			set
			{
				this[ApnsFeedbackManagerSettings.Schema.ExpirationThresholdInMilliseconds] = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006C07 File Offset: 0x00004E07
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00006C19 File Offset: 0x00004E19
		public int UpdateIntervalInMilliseconds
		{
			get
			{
				return (int)this[ApnsFeedbackManagerSettings.Schema.UpdateIntervalInMilliseconds];
			}
			set
			{
				this[ApnsFeedbackManagerSettings.Schema.UpdateIntervalInMilliseconds] = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006C2C File Offset: 0x00004E2C
		internal override RegistryObjectSchema RegistrySchema
		{
			get
			{
				return ApnsFeedbackManagerSettings.schema;
			}
		}

		// Token: 0x040000A3 RID: 163
		private static readonly ApnsFeedbackManagerSettings.Schema schema = ObjectSchema.GetInstance<ApnsFeedbackManagerSettings.Schema>();

		// Token: 0x0200002A RID: 42
		internal class Schema : RegistryObjectSchema
		{
			// Token: 0x1700006D RID: 109
			// (get) Token: 0x060001AF RID: 431 RVA: 0x00006C47 File Offset: 0x00004E47
			public override string DefaultRegistryKeyPath
			{
				get
				{
					return "SYSTEM\\CurrentControlSet\\Services\\MSExchange PushNotifications";
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006C4E File Offset: 0x00004E4E
			public override string DefaultName
			{
				get
				{
					return "Feedback";
				}
			}

			// Token: 0x040000A4 RID: 164
			public const string RegistryRootName = "Feedback";

			// Token: 0x040000A5 RID: 165
			public static readonly RegistryObjectId RootObjectId = new RegistryObjectId("SYSTEM\\CurrentControlSet\\Services\\MSExchange PushNotifications", "Feedback");

			// Token: 0x040000A6 RID: 166
			public static readonly RegistryPropertyDefinition ExpirationThresholdInMilliseconds = new RegistryPropertyDefinition("ExpirationThresholdInMilliseconds", typeof(int), 259200000);

			// Token: 0x040000A7 RID: 167
			public static readonly RegistryPropertyDefinition UpdateIntervalInMilliseconds = new RegistryPropertyDefinition("UpdateIntervalInMilliseconds", typeof(int), 43200000);
		}
	}
}
