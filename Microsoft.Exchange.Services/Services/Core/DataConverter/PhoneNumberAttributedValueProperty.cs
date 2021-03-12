using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000115 RID: 277
	internal class PhoneNumberAttributedValueProperty : SimpleProperty
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x00027926 File Offset: 0x00025B26
		protected PhoneNumberAttributedValueProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002792F File Offset: 0x00025B2F
		public new static PhoneNumberAttributedValueProperty CreateCommand(CommandContext commandContext)
		{
			return new PhoneNumberAttributedValueProperty(commandContext);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00027937 File Offset: 0x00025B37
		protected override object Parse(string propertyString)
		{
			return null;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002793A File Offset: 0x00025B3A
		protected override void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0002793C File Offset: 0x00025B3C
		protected override void WriteServiceProperty(object propertyValue, ServiceObject serviceObject, PropertyInformation propInfo)
		{
		}
	}
}
