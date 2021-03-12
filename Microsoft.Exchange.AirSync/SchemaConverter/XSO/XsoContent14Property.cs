using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200020E RID: 526
	internal class XsoContent14Property : XsoContentProperty, IContent14Property, IContentProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x06001452 RID: 5202 RVA: 0x000753A5 File Offset: 0x000735A5
		public XsoContent14Property(PropertyType type) : base(type)
		{
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x000753AE File Offset: 0x000735AE
		public XsoContent14Property()
		{
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x000753B8 File Offset: 0x000735B8
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
