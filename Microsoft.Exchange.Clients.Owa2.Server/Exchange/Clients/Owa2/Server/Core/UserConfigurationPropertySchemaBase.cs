using System;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000D8 RID: 216
	internal abstract class UserConfigurationPropertySchemaBase
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001B204 File Offset: 0x00019404
		internal int Count
		{
			get
			{
				return this.PropertyDefinitions.Length;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600083E RID: 2110
		internal abstract UserConfigurationPropertyDefinition[] PropertyDefinitions { get; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600083F RID: 2111
		internal abstract UserConfigurationPropertyId PropertyDefinitionsBaseId { get; }

		// Token: 0x06000840 RID: 2112 RVA: 0x0001B210 File Offset: 0x00019410
		internal UserConfigurationPropertyDefinition GetPropertyDefinition(UserConfigurationPropertyId id)
		{
			int index = id - this.PropertyDefinitionsBaseId;
			return this.GetPropertyDefinition(index);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001B22D File Offset: 0x0001942D
		internal UserConfigurationPropertyDefinition GetPropertyDefinition(int index)
		{
			ExTraceGlobals.UserOptionsDataTracer.TraceDebug<int, string>(0L, "Get UserConfigurationPropertyDefinition: index = '{0}', name = '{1}'", index, this.PropertyDefinitions[index].PropertyName);
			return this.PropertyDefinitions[index];
		}
	}
}
