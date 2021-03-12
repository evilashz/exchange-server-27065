using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000A5 RID: 165
	internal class SetEntityDataRequest : ConfigurablePropertyBag
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000126B3 File Offset: 0x000108B3
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x000126C5 File Offset: 0x000108C5
		public Guid PartitionId
		{
			get
			{
				return (Guid)this[SetEntityDataRequest.PartitionIdPropertyDefinition];
			}
			set
			{
				this[SetEntityDataRequest.PartitionIdPropertyDefinition] = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000126D8 File Offset: 0x000108D8
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x000126EA File Offset: 0x000108EA
		public string TableName
		{
			get
			{
				return (string)this[SetEntityDataRequest.TableNamePropertyDefinition];
			}
			set
			{
				this[SetEntityDataRequest.TableNamePropertyDefinition] = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000126F8 File Offset: 0x000108F8
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x0001270A File Offset: 0x0001090A
		public string ColumnName
		{
			get
			{
				return (string)this[SetEntityDataRequest.ColumnNamePropertyDefinition];
			}
			set
			{
				this[SetEntityDataRequest.ColumnNamePropertyDefinition] = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00012718 File Offset: 0x00010918
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0001272A File Offset: 0x0001092A
		public string Condition
		{
			get
			{
				return (string)this[SetEntityDataRequest.ConditionPropertyDefinition];
			}
			set
			{
				this[SetEntityDataRequest.ConditionPropertyDefinition] = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00012738 File Offset: 0x00010938
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0001274A File Offset: 0x0001094A
		public string NewValue
		{
			get
			{
				return (string)this[SetEntityDataRequest.NewValuePropertyDefinition];
			}
			set
			{
				this[SetEntityDataRequest.NewValuePropertyDefinition] = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00012758 File Offset: 0x00010958
		public override ObjectId Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0400036B RID: 875
		public static readonly HygienePropertyDefinition PartitionIdPropertyDefinition = new HygienePropertyDefinition("id_PartitionId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400036C RID: 876
		internal static readonly HygienePropertyDefinition TableNamePropertyDefinition = new HygienePropertyDefinition("nvc_TableName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400036D RID: 877
		internal static readonly HygienePropertyDefinition ColumnNamePropertyDefinition = new HygienePropertyDefinition("nvc_ColumnName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400036E RID: 878
		internal static readonly HygienePropertyDefinition ConditionPropertyDefinition = new HygienePropertyDefinition("nvc_Condition", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400036F RID: 879
		internal static readonly HygienePropertyDefinition NewValuePropertyDefinition = new HygienePropertyDefinition("nvc_NewValue", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
