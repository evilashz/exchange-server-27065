using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000106 RID: 262
	internal class ExtendedPropertyAttributedValueProperty : SimpleProperty
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x000247B6 File Offset: 0x000229B6
		protected ExtendedPropertyAttributedValueProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000247BF File Offset: 0x000229BF
		public new static ExtendedPropertyAttributedValueProperty CreateCommand(CommandContext commandContext)
		{
			return new ExtendedPropertyAttributedValueProperty(commandContext);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000247C7 File Offset: 0x000229C7
		protected override object Parse(string propertyString)
		{
			return null;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000247CA File Offset: 0x000229CA
		protected override void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000247CC File Offset: 0x000229CC
		protected override void WriteServiceProperty(object propertyValue, ServiceObject serviceObject, PropertyInformation propInfo)
		{
		}
	}
}
