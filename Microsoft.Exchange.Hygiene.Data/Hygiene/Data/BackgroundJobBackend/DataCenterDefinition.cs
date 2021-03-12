using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000039 RID: 57
	internal sealed class DataCenterDefinition : BackgroundJobBackendBase
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007499 File Offset: 0x00005699
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000074AB File Offset: 0x000056AB
		public long DataCenterId
		{
			get
			{
				return (long)this[DataCenterDefinition.DataCenterIdProperty];
			}
			set
			{
				this[DataCenterDefinition.DataCenterIdProperty] = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000074BE File Offset: 0x000056BE
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000074D0 File Offset: 0x000056D0
		public string Name
		{
			get
			{
				return (string)this[DataCenterDefinition.NameProperty];
			}
			set
			{
				this[DataCenterDefinition.NameProperty] = value;
			}
		}

		// Token: 0x04000146 RID: 326
		internal static readonly BackgroundJobBackendPropertyDefinition NameProperty = new BackgroundJobBackendPropertyDefinition("Name", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000147 RID: 327
		internal static readonly BackgroundJobBackendPropertyDefinition DataCenterIdProperty = new BackgroundJobBackendPropertyDefinition("DCId", typeof(long), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0L);
	}
}
