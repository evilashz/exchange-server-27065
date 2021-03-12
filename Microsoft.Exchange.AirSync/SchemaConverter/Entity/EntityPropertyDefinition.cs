using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x020001A6 RID: 422
	[Serializable]
	public class EntityPropertyDefinition : Microsoft.Exchange.Data.PropertyDefinition
	{
		// Token: 0x0600120A RID: 4618 RVA: 0x000620FC File Offset: 0x000602FC
		public EntityPropertyDefinition(Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition edmProperty) : base(edmProperty.Name, edmProperty.ValueType)
		{
			this.EdmDefinition = edmProperty;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00062117 File Offset: 0x00060317
		public EntityPropertyDefinition(Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition edmProperty, Func<IItem, object> getter) : this(edmProperty)
		{
			this.Getter = getter;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00062127 File Offset: 0x00060327
		public EntityPropertyDefinition(Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition edmProperty, Func<IItem, object> getter, Func<IItem, object, object> setter) : this(edmProperty, getter)
		{
			this.Setter = setter;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00062138 File Offset: 0x00060338
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00062140 File Offset: 0x00060340
		internal Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition EdmDefinition { get; private set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00062149 File Offset: 0x00060349
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x00062151 File Offset: 0x00060351
		internal Func<IItem, object> Getter { get; private set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0006215A File Offset: 0x0006035A
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x00062162 File Offset: 0x00060362
		internal Func<IItem, object, object> Setter { get; private set; }

		// Token: 0x06001213 RID: 4627 RVA: 0x0006216B File Offset: 0x0006036B
		public override string ToString()
		{
			return string.Format("Property: {0} ({1})", base.Name, base.Type.FullName);
		}
	}
}
