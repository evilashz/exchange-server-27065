using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000A8 RID: 168
	internal class SqlPropertyDefinition : ConfigurablePropertyBag
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00012A60 File Offset: 0x00010C60
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.PropertyId.ToString());
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00012A80 File Offset: 0x00010C80
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00012A92 File Offset: 0x00010C92
		public string EntityName
		{
			get
			{
				return (string)this[SqlPropertyDefinition.EntityNameProp];
			}
			set
			{
				this[SqlPropertyDefinition.EntityNameProp] = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00012AA0 File Offset: 0x00010CA0
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x00012AB2 File Offset: 0x00010CB2
		public string PropertyName
		{
			get
			{
				return (string)this[SqlPropertyDefinition.PropertyNameProp];
			}
			set
			{
				this[SqlPropertyDefinition.PropertyNameProp] = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00012AC0 File Offset: 0x00010CC0
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00012AD2 File Offset: 0x00010CD2
		public int EntityId
		{
			get
			{
				return (int)this[SqlPropertyDefinition.EntityIdProp];
			}
			set
			{
				this[SqlPropertyDefinition.EntityIdProp] = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00012AE5 File Offset: 0x00010CE5
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00012AF7 File Offset: 0x00010CF7
		public int PropertyId
		{
			get
			{
				return (int)this[SqlPropertyDefinition.PropertyIdProp];
			}
			set
			{
				this[SqlPropertyDefinition.PropertyIdProp] = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00012B0A File Offset: 0x00010D0A
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00012B1C File Offset: 0x00010D1C
		public SqlPropertyDefinitionFlags Flags
		{
			get
			{
				return (SqlPropertyDefinitionFlags)this[SqlPropertyDefinition.FlagsProp];
			}
			set
			{
				this[SqlPropertyDefinition.FlagsProp] = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00012B2F File Offset: 0x00010D2F
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00012B41 File Offset: 0x00010D41
		public SqlPropertyTypes Type
		{
			get
			{
				return (SqlPropertyTypes)this[SqlPropertyDefinition.TypeProp];
			}
			set
			{
				this[SqlPropertyDefinition.TypeProp] = value;
			}
		}

		// Token: 0x04000373 RID: 883
		public static readonly HygienePropertyDefinition PropertyNameProp = new HygienePropertyDefinition("PropertyName", typeof(string));

		// Token: 0x04000374 RID: 884
		public static readonly HygienePropertyDefinition EntityNameProp = new HygienePropertyDefinition("EntityName", typeof(string));

		// Token: 0x04000375 RID: 885
		public static readonly HygienePropertyDefinition EntityIdProp = new HygienePropertyDefinition("EntityId", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000376 RID: 886
		public static readonly HygienePropertyDefinition PropertyIdProp = new HygienePropertyDefinition("PropertyId", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000377 RID: 887
		public static readonly HygienePropertyDefinition FlagsProp = new HygienePropertyDefinition("Flags", typeof(SqlPropertyDefinitionFlags), SqlPropertyDefinitionFlags.None, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000378 RID: 888
		public static readonly HygienePropertyDefinition TypeProp = new HygienePropertyDefinition("Type", typeof(SqlPropertyTypes), SqlPropertyTypes.String, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
