using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005AF RID: 1455
	[Serializable]
	public sealed class SyncedPerimeterConfig : PerimeterConfig
	{
		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x000FC3FA File Offset: 0x000FA5FA
		public MultiValuedProperty<string> SyncErrors
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncedPerimeterConfigSchema.SyncErrors];
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x000FC40C File Offset: 0x000FA60C
		internal override ADObjectSchema Schema
		{
			get
			{
				return SyncedPerimeterConfig.SchemaObject;
			}
		}

		// Token: 0x04002D8C RID: 11660
		private static readonly SyncedPerimeterConfigSchema SchemaObject = ObjectSchema.GetInstance<SyncedPerimeterConfigSchema>();
	}
}
