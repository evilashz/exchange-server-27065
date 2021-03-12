using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000238 RID: 568
	internal class XsoSmsDateReceivedProperty : XsoUtcDateTimeProperty
	{
		// Token: 0x0600150D RID: 5389 RVA: 0x0007B738 File Offset: 0x00079938
		public XsoSmsDateReceivedProperty() : base(ItemSchema.ReceivedTime, new PropertyDefinition[]
		{
			ItemSchema.ReceivedTime,
			ItemSchema.SentTime
		})
		{
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0007B768 File Offset: 0x00079968
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.InternalCopyFromModified(srcProperty);
			base.XsoItem[ItemSchema.SentTime] = base.XsoItem[ItemSchema.ReceivedTime];
		}
	}
}
