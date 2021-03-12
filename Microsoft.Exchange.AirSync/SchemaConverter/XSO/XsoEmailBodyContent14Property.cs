using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000215 RID: 533
	internal class XsoEmailBodyContent14Property : XsoEmailBodyContentProperty, IContent14Property, IContentProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x0600147A RID: 5242 RVA: 0x00076590 File Offset: 0x00074790
		public XsoEmailBodyContent14Property(PropertyType propertyType = PropertyType.ReadOnly) : base(propertyType)
		{
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0007659C File Offset: 0x0007479C
		public string Preview
		{
			get
			{
				Item item = base.XsoItem as Item;
				if (item == null)
				{
					return null;
				}
				if (BodyConversionUtilities.IsMessageRestrictedAndDecoded(item))
				{
					return ((RightsManagedMessageItem)item).ProtectedBody.PreviewText;
				}
				return item.Body.PreviewText;
			}
		}
	}
}
