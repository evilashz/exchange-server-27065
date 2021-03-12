using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000139 RID: 313
	internal class ShortArrayValueProperty : SimpleProperty, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x0600088A RID: 2186 RVA: 0x00029E43 File Offset: 0x00028043
		private ShortArrayValueProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00029E4C File Offset: 0x0002804C
		public new static ShortArrayValueProperty CreateCommand(CommandContext commandContext)
		{
			return new ShortArrayValueProperty(commandContext);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00029E54 File Offset: 0x00028054
		public new void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			object value = null;
			serviceObject[propertyInformation] = value;
			if (PropertyCommand.TryGetValueFromPropertyBag<object>(propertyBag, this.propertyDefinition, out value))
			{
				ArrayPropertyInformation arrayPropertyInformation = propertyInformation as ArrayPropertyInformation;
				if (arrayPropertyInformation != null)
				{
					serviceObject[propertyInformation] = value;
				}
			}
		}
	}
}
