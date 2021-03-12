using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200021F RID: 543
	[Serializable]
	internal class XsoIntegerProperty : XsoProperty, IIntegerProperty, IProperty
	{
		// Token: 0x060014B3 RID: 5299 RVA: 0x000783D9 File Offset: 0x000765D9
		public XsoIntegerProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000783E2 File Offset: 0x000765E2
		public XsoIntegerProperty(StorePropertyDefinition propertyDef, bool nodelete) : base(propertyDef)
		{
			this.nodelete = nodelete;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000783F2 File Offset: 0x000765F2
		public XsoIntegerProperty(StorePropertyDefinition propertyDef, PropertyType type) : base(propertyDef, type)
		{
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x000783FC File Offset: 0x000765FC
		public XsoIntegerProperty(StorePropertyDefinition propertyDef, PropertyDefinition[] prefetchProps) : base(propertyDef, prefetchProps)
		{
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00078406 File Offset: 0x00076606
		public XsoIntegerProperty(StorePropertyDefinition propertyDef, PropertyType type, PropertyDefinition[] prefetchProps) : base(propertyDef, type, prefetchProps)
		{
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00078411 File Offset: 0x00076611
		public virtual int IntegerData
		{
			get
			{
				return (int)base.XsoItem.TryGetProperty(base.PropertyDef);
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00078429 File Offset: 0x00076629
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.XsoItem[base.PropertyDef] = ((IIntegerProperty)srcProperty).IntegerData;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0007844C File Offset: 0x0007664C
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			if (this.nodelete)
			{
				return;
			}
			if (base.XsoItem.TryGetProperty(base.PropertyDef) is PropertyError)
			{
				return;
			}
			base.XsoItem.DeleteProperties(new PropertyDefinition[]
			{
				base.PropertyDef
			});
		}

		// Token: 0x04000C6D RID: 3181
		private bool nodelete;
	}
}
