using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200013D RID: 317
	internal class StringAttributedValueProperty : SimpleProperty
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x0002A3D2 File Offset: 0x000285D2
		protected StringAttributedValueProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002A3DB File Offset: 0x000285DB
		public new static StringAttributedValueProperty CreateCommand(CommandContext commandContext)
		{
			return new StringAttributedValueProperty(commandContext);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002A3E3 File Offset: 0x000285E3
		protected override object Parse(string propertyString)
		{
			return null;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002A3E8 File Offset: 0x000285E8
		protected override void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
			string propertyString = serviceObject[this.commandContext.PropertyInformation] as string;
			object value = this.Parse(propertyString);
			base.SetPropertyValueOnStoreObject(storeObject, this.propertyDefinition, value);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002A424 File Offset: 0x00028624
		protected override void WriteServiceProperty(object propertyValue, ServiceObject serviceObject, PropertyInformation propInfo)
		{
			string propertyValue2 = this.ToString(propertyValue);
			base.WriteServiceProperty(propertyValue2, serviceObject, propInfo);
		}
	}
}
