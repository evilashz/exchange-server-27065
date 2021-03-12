using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000105 RID: 261
	internal class EmailAddressAttributedValueProperty : SimpleProperty
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x0002479E File Offset: 0x0002299E
		protected EmailAddressAttributedValueProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000247A7 File Offset: 0x000229A7
		public new static EmailAddressAttributedValueProperty CreateCommand(CommandContext commandContext)
		{
			return new EmailAddressAttributedValueProperty(commandContext);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000247AF File Offset: 0x000229AF
		protected override object Parse(string propertyString)
		{
			return null;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000247B2 File Offset: 0x000229B2
		protected override void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000247B4 File Offset: 0x000229B4
		protected override void WriteServiceProperty(object propertyValue, ServiceObject serviceObject, PropertyInformation propInfo)
		{
		}
	}
}
