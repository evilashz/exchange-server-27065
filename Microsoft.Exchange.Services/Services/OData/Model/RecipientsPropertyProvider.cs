using System;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E93 RID: 3731
	internal class RecipientsPropertyProvider : EwsPropertyProvider
	{
		// Token: 0x06006136 RID: 24886 RVA: 0x0012F004 File Offset: 0x0012D204
		public RecipientsPropertyProvider(PropertyInformation propertyInformation) : base(propertyInformation)
		{
		}

		// Token: 0x06006137 RID: 24887 RVA: 0x0012F018 File Offset: 0x0012D218
		protected override void GetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			EmailAddressWrapper[] valueOrDefault = ewsObject.GetValueOrDefault<EmailAddressWrapper[]>(base.PropertyInformation);
			object value;
			if (valueOrDefault != null)
			{
				value = Array.ConvertAll<EmailAddressWrapper, Recipient>(valueOrDefault, (EmailAddressWrapper x) => x.ToRecipient());
			}
			else
			{
				value = null;
			}
			entity[property] = value;
		}

		// Token: 0x06006138 RID: 24888 RVA: 0x0012F06A File Offset: 0x0012D26A
		protected override void SetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			ewsObject[base.PropertyInformation] = Array.ConvertAll<Recipient, EmailAddressWrapper>(entity[property] as Recipient[], (Recipient x) => x.ToEmailAddressWrapper());
		}
	}
}
