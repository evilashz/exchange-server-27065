using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200051D RID: 1309
	[DataContract]
	public class ManagementRoleResolveRow : AdObjectResolverRow
	{
		// Token: 0x06003EA5 RID: 16037 RVA: 0x000BD165 File Offset: 0x000BB365
		public ManagementRoleResolveRow(ADRawEntry aDRawEntry) : base(aDRawEntry)
		{
		}

		// Token: 0x1700247F RID: 9343
		// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x000BD16E File Offset: 0x000BB36E
		public string Name
		{
			get
			{
				return this.DisplayName;
			}
		}

		// Token: 0x17002480 RID: 9344
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000BD176 File Offset: 0x000BB376
		public string Description
		{
			get
			{
				return (string)base.ADRawEntry[ExchangeRoleSchema.Description];
			}
		}

		// Token: 0x17002481 RID: 9345
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x000BD18D File Offset: 0x000BB38D
		public ScopeType ImplicitRecipientWriteScopeType
		{
			get
			{
				return (ScopeType)base.ADRawEntry[ExchangeRoleSchema.ImplicitRecipientWriteScope];
			}
		}

		// Token: 0x17002482 RID: 9346
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x000BD1A4 File Offset: 0x000BB3A4
		public ScopeType ImplicitConfigWriteScopeType
		{
			get
			{
				return (ScopeType)base.ADRawEntry[ExchangeRoleSchema.ImplicitConfigWriteScope];
			}
		}

		// Token: 0x040028A3 RID: 10403
		public new static PropertyDefinition[] Properties = new List<PropertyDefinition>(AdObjectResolverRow.Properties)
		{
			ADObjectSchema.Name,
			ExchangeRoleSchema.Description,
			ExchangeRoleSchema.ImplicitConfigWriteScope,
			ExchangeRoleSchema.ImplicitRecipientWriteScope
		}.ToArray();
	}
}
