using System;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E91 RID: 3729
	internal class BodyPropertyProvider : EwsPropertyProvider
	{
		// Token: 0x0600612F RID: 24879 RVA: 0x0012EECA File Offset: 0x0012D0CA
		public BodyPropertyProvider(PropertyInformation propertyInforation) : base(propertyInforation)
		{
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x0012EED4 File Offset: 0x0012D0D4
		protected override void GetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			BodyContentType valueOrDefault = ewsObject.GetValueOrDefault<BodyContentType>(base.PropertyInformation);
			if (valueOrDefault != null)
			{
				entity[property] = valueOrDefault.ToItemBody();
			}
		}

		// Token: 0x06006131 RID: 24881 RVA: 0x0012EF00 File Offset: 0x0012D100
		protected override void SetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			ItemBody itemBody = entity[property] as ItemBody;
			if (itemBody != null && !string.IsNullOrEmpty(itemBody.Content))
			{
				ewsObject[base.PropertyInformation] = itemBody.ToBodyContentType();
			}
		}
	}
}
