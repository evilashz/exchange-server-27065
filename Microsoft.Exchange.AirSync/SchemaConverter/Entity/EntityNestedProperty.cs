using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x0200019F RID: 415
	internal abstract class EntityNestedProperty : EntityProperty, INestedProperty
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x00061994 File Offset: 0x0005FB94
		public EntityNestedProperty(EntityPropertyDefinition propertyDefinition, PropertyType type = PropertyType.ReadWrite) : base(propertyDefinition, type, false)
		{
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0006199F File Offset: 0x0005FB9F
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x000619A7 File Offset: 0x0005FBA7
		public virtual INestedData NestedData { get; set; }

		// Token: 0x060011EC RID: 4588 RVA: 0x000619B0 File Offset: 0x0005FBB0
		public override void Bind(IItem item)
		{
			this.Unbind();
			base.Bind(item);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000619BF File Offset: 0x0005FBBF
		public override void Unbind()
		{
			base.Unbind();
			this.NestedData.Clear();
		}
	}
}
