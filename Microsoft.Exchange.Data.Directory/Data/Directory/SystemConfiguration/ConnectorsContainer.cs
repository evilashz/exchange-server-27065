using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003C8 RID: 968
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class ConnectorsContainer : ADContainer
	{
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x000B62F2 File Offset: 0x000B44F2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ConnectorsContainer.mostDerivedClass;
			}
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000B62F9 File Offset: 0x000B44F9
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.None;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x04001A76 RID: 6774
		public const string DefaultName = "Connections";

		// Token: 0x04001A77 RID: 6775
		private static string mostDerivedClass = "msExchConnectors";
	}
}
