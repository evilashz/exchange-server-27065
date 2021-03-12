using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200013C RID: 316
	internal class StringArrayAttributedValueProperty : SimpleProperty
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x0002A364 File Offset: 0x00028564
		protected StringArrayAttributedValueProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002A36D File Offset: 0x0002856D
		public new static StringArrayAttributedValueProperty CreateCommand(CommandContext commandContext)
		{
			return new StringArrayAttributedValueProperty(commandContext);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002A375 File Offset: 0x00028575
		protected override object Parse(string propertyString)
		{
			return null;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002A378 File Offset: 0x00028578
		protected override void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
			string propertyString = serviceObject[this.commandContext.PropertyInformation] as string;
			object value = this.Parse(propertyString);
			base.SetPropertyValueOnStoreObject(storeObject, this.propertyDefinition, value);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002A3B4 File Offset: 0x000285B4
		protected override void WriteServiceProperty(object propertyValue, ServiceObject serviceObject, PropertyInformation propInfo)
		{
			string propertyValue2 = this.ToString(propertyValue);
			base.WriteServiceProperty(propertyValue2, serviceObject, propInfo);
		}
	}
}
