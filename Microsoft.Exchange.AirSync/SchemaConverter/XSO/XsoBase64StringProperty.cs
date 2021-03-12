using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000202 RID: 514
	[Serializable]
	internal class XsoBase64StringProperty : XsoStringProperty
	{
		// Token: 0x060013FC RID: 5116 RVA: 0x000739CA File Offset: 0x00071BCA
		public XsoBase64StringProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x000739D3 File Offset: 0x00071BD3
		public override string StringData
		{
			get
			{
				return Convert.ToBase64String((byte[])base.XsoItem.TryGetProperty(base.PropertyDef));
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x000739F0 File Offset: 0x00071BF0
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.XsoItem[base.PropertyDef] = Convert.FromBase64String(((IStringProperty)srcProperty).StringData);
		}
	}
}
