using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000116 RID: 278
	internal class PostalAddressAttributedValueProperty : SimpleProperty
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x0002793E File Offset: 0x00025B3E
		protected PostalAddressAttributedValueProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00027947 File Offset: 0x00025B47
		public new static PostalAddressAttributedValueProperty CreateCommand(CommandContext commandContext)
		{
			return new PostalAddressAttributedValueProperty(commandContext);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0002794F File Offset: 0x00025B4F
		protected override object Parse(string propertyString)
		{
			return null;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00027952 File Offset: 0x00025B52
		protected override void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00027954 File Offset: 0x00025B54
		protected override void WriteServiceProperty(object propertyValue, ServiceObject serviceObject, PropertyInformation propInfo)
		{
		}
	}
}
