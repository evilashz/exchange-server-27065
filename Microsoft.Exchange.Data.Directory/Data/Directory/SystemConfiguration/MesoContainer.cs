using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004B1 RID: 1201
	[Serializable]
	public class MesoContainer : ADConfigurationObject
	{
		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x000D6F0D File Offset: 0x000D510D
		internal override ADObjectSchema Schema
		{
			get
			{
				return MesoContainer.schema;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x000D6F14 File Offset: 0x000D5114
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MesoContainer.MostDerivedClass;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x060036DF RID: 14047 RVA: 0x000D6F1B File Offset: 0x000D511B
		// (set) Token: 0x060036E0 RID: 14048 RVA: 0x000D6F32 File Offset: 0x000D5132
		public int ObjectVersion
		{
			get
			{
				return (int)this.propertyBag[MesoContainerSchema.ObjectVersion];
			}
			internal set
			{
				this.propertyBag[MesoContainerSchema.ObjectVersion] = value;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000D6F52 File Offset: 0x000D5152
		public static int DomainPrepVersion
		{
			get
			{
				return MesoContainerSchema.DomainPrepVersion;
			}
		}

		// Token: 0x04002512 RID: 9490
		internal const string MESOContainerName = "Microsoft Exchange System Objects";

		// Token: 0x04002513 RID: 9491
		private static MesoContainerSchema schema = ObjectSchema.GetInstance<MesoContainerSchema>();

		// Token: 0x04002514 RID: 9492
		internal static string MostDerivedClass = "msExchSystemObjectsContainer";
	}
}
