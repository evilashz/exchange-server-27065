using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000FE RID: 254
	internal class AttributionProperty : SimpleProperty
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x00022C41 File Offset: 0x00020E41
		protected AttributionProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00022C4A File Offset: 0x00020E4A
		public new static AttributionProperty CreateCommand(CommandContext commandContext)
		{
			return new AttributionProperty(commandContext);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00022C52 File Offset: 0x00020E52
		protected override object Parse(string propertyString)
		{
			return null;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00022C55 File Offset: 0x00020E55
		protected override void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00022C57 File Offset: 0x00020E57
		protected override void WriteServiceProperty(object propertyValue, ServiceObject serviceObject, PropertyInformation propInfo)
		{
		}
	}
}
