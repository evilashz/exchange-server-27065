using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000598 RID: 1432
	[Serializable]
	public class ServersContainer : ADLegacyVersionableObject
	{
		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x060042A3 RID: 17059 RVA: 0x000FB33B File Offset: 0x000F953B
		internal override ADObjectSchema Schema
		{
			get
			{
				return ServersContainer.schema;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x000FB342 File Offset: 0x000F9542
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ServersContainer.mostDerivedClass;
			}
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x000FB354 File Offset: 0x000F9554
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADConfigurationObjectSchema.AdminDisplayName))
			{
				base.AdminDisplayName = ServersContainer.DefaultName;
			}
			if (!base.IsModified(ServersContainerSchema.ContainerInfo))
			{
				this[ServersContainerSchema.ContainerInfo] = ContainerInfo.Servers;
			}
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.None;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x04002D56 RID: 11606
		public static readonly string DefaultName = "Servers";

		// Token: 0x04002D57 RID: 11607
		private static ServersContainerSchema schema = ObjectSchema.GetInstance<ServersContainerSchema>();

		// Token: 0x04002D58 RID: 11608
		private static string mostDerivedClass = "msExchServersContainer";
	}
}
