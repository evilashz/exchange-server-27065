using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001FA RID: 506
	internal class XsoAccountIdProperty : XsoGuidProperty
	{
		// Token: 0x060013D8 RID: 5080 RVA: 0x00072131 File Offset: 0x00070331
		public XsoAccountIdProperty(PropertyType type) : base(MessageItemSchema.SharingInstanceGuid, type, XsoAccountIdProperty.prefetchProps)
		{
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00072144 File Offset: 0x00070344
		public override string StringData
		{
			get
			{
				object obj = base.XsoItem.TryGetProperty(ItemSchema.CloudId);
				if (obj is PropertyError)
				{
					return string.Empty;
				}
				return base.StringData;
			}
		}

		// Token: 0x04000C47 RID: 3143
		private static readonly PropertyDefinition[] prefetchProps = new PropertyDefinition[]
		{
			MessageItemSchema.SharingInstanceGuid,
			ItemSchema.CloudId
		};
	}
}
