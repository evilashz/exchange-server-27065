using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200003C RID: 60
	internal sealed class RegionDefinition : BackgroundJobBackendBase
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000789C File Offset: 0x00005A9C
		// (set) Token: 0x060001FB RID: 507 RVA: 0x000078AE File Offset: 0x00005AAE
		public Regions RegionId
		{
			get
			{
				return (Regions)this[RegionDefinition.RegionIdProperty];
			}
			set
			{
				this[RegionDefinition.RegionIdProperty] = (int)value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000078C1 File Offset: 0x00005AC1
		// (set) Token: 0x060001FD RID: 509 RVA: 0x000078D3 File Offset: 0x00005AD3
		public string Name
		{
			get
			{
				return (string)this[RegionDefinition.NameProperty];
			}
			set
			{
				this[RegionDefinition.NameProperty] = value;
			}
		}

		// Token: 0x0400015E RID: 350
		internal static readonly BackgroundJobBackendPropertyDefinition NameProperty = new BackgroundJobBackendPropertyDefinition("Name", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x0400015F RID: 351
		internal static readonly BackgroundJobBackendPropertyDefinition RegionIdProperty = new BackgroundJobBackendPropertyDefinition("RegionId", typeof(int), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);
	}
}
