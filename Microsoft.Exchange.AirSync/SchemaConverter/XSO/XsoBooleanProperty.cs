using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000207 RID: 519
	internal class XsoBooleanProperty : XsoProperty, IBooleanProperty, IProperty
	{
		// Token: 0x0600141F RID: 5151 RVA: 0x00074544 File Offset: 0x00072744
		public XsoBooleanProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0007454D File Offset: 0x0007274D
		public XsoBooleanProperty(StorePropertyDefinition propertyDef, bool nodelete) : base(propertyDef)
		{
			this.nodelete = nodelete;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0007455D File Offset: 0x0007275D
		public XsoBooleanProperty(StorePropertyDefinition propertyDef, PropertyType type) : base(propertyDef, type)
		{
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00074567 File Offset: 0x00072767
		public XsoBooleanProperty(StorePropertyDefinition propertyDef, PropertyDefinition[] prefetchPropDef) : base(propertyDef, prefetchPropDef)
		{
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00074571 File Offset: 0x00072771
		public bool BooleanData
		{
			get
			{
				return (bool)base.XsoItem.TryGetProperty(base.PropertyDef);
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00074589 File Offset: 0x00072789
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.XsoItem[base.PropertyDef] = ((IBooleanProperty)srcProperty).BooleanData;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x000745AC File Offset: 0x000727AC
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

		// Token: 0x04000C55 RID: 3157
		private bool nodelete;
	}
}
