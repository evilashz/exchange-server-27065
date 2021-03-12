using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200030B RID: 779
	[Serializable]
	public class ActiveSyncDeviceClasses : ADContainer
	{
		// Token: 0x06002410 RID: 9232 RVA: 0x0009B0BF File Offset: 0x000992BF
		public ActiveSyncDeviceClasses()
		{
			this.Name = "ExchangeDeviceClasses";
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x0009B0D2 File Offset: 0x000992D2
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x0009B0DA File Offset: 0x000992DA
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x0009B0E3 File Offset: 0x000992E3
		internal override ADObjectSchema Schema
		{
			get
			{
				return ActiveSyncDeviceClasses.schema;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x0009B0EA File Offset: 0x000992EA
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchActiveSyncDevices";
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x0009B0F1 File Offset: 0x000992F1
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x0009B0F8 File Offset: 0x000992F8
		internal override ADObjectId ParentPath
		{
			get
			{
				return ActiveSyncDeviceClasses.parentPath;
			}
		}

		// Token: 0x0400164F RID: 5711
		internal const string ContainerName = "ExchangeDeviceClasses";

		// Token: 0x04001650 RID: 5712
		private const string MostDerivedClassName = "msExchActiveSyncDevices";

		// Token: 0x04001651 RID: 5713
		private static ADObjectId parentPath = new ADObjectId("CN=Mobile Mailbox Settings");

		// Token: 0x04001652 RID: 5714
		private static ActiveSyncDeviceClassesSchema schema = ObjectSchema.GetInstance<ActiveSyncDeviceClassesSchema>();
	}
}
