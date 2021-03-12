using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000086 RID: 134
	internal class StoreConfigProvider : ConfigProviderBase
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00014318 File Offset: 0x00012518
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x00014320 File Offset: 0x00012520
		internal ConfigSchemaBase Schema { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00014329 File Offset: 0x00012529
		public static StoreConfigProvider Default
		{
			get
			{
				return StoreConfigProvider.instance;
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00014330 File Offset: 0x00012530
		public StoreConfigProvider(ConfigSchemaBase schema) : base(schema)
		{
			this.Schema = schema;
			StoreConfigProvider.instance = this;
			this.AddConfigDriver(new AppConfigDriver(schema));
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00014352 File Offset: 0x00012552
		public new void AddConfigDriver(IConfigDriver configDriver)
		{
			configDriver.Initialize();
			base.AddConfigDriver(configDriver);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00014361 File Offset: 0x00012561
		internal new void RemoveConfigDriver(IConfigDriver configDriver)
		{
			base.RemoveConfigDriver(configDriver);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001436A File Offset: 0x0001256A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<StoreConfigProvider>(this);
		}

		// Token: 0x0400067B RID: 1659
		private static StoreConfigProvider instance;
	}
}
