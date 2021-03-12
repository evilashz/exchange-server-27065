using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004A1 RID: 1185
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactEmailAddressesEnumerator : IEnumerable<Tuple<EmailAddress, EmailAddressIndex>>, IEnumerable
	{
		// Token: 0x060034B7 RID: 13495 RVA: 0x000D52BA File Offset: 0x000D34BA
		public ContactEmailAddressesEnumerator(IStorePropertyBag propertyBag, string clientInfoString)
		{
			this.propertyBag = propertyBag;
			this.clientInfoString = clientInfoString;
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000D52D0 File Offset: 0x000D34D0
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generics interface of GetEnumerator.");
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000D5538 File Offset: 0x000D3738
		public IEnumerator<Tuple<EmailAddress, EmailAddressIndex>> GetEnumerator()
		{
			string partnerNetworkId = this.propertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
			if (StringComparer.OrdinalIgnoreCase.Equals(partnerNetworkId, WellKnownNetworkNames.Facebook))
			{
				if (ClientInfo.OWA.IsMatch(this.clientInfoString))
				{
					string protectedEmail = this.propertyBag.TryGetProperty(InternalSchema.ProtectedEmailAddress) as string;
					if (!string.IsNullOrWhiteSpace(protectedEmail))
					{
						EmailAddress emailAddress = new EmailAddress
						{
							RoutingType = "smtp",
							Address = protectedEmail,
							Name = protectedEmail
						};
						yield return new Tuple<EmailAddress, EmailAddressIndex>(emailAddress, EmailAddressIndex.None);
					}
				}
			}
			else
			{
				foreach (EmailAddressProperties properties in EmailAddressProperties.PropertySets)
				{
					EmailAddress emailAddress2 = properties.GetFrom(this.propertyBag);
					if (emailAddress2 != null)
					{
						yield return new Tuple<EmailAddress, EmailAddressIndex>(emailAddress2, properties.EmailAddressIndex);
					}
				}
			}
			yield break;
		}

		// Token: 0x04001C0C RID: 7180
		public static readonly StorePropertyDefinition[] Properties = PropertyDefinitionCollection.Merge<StorePropertyDefinition>(EmailAddressProperties.AllProperties, new StorePropertyDefinition[]
		{
			ContactSchema.PartnerNetworkId,
			InternalSchema.ProtectedEmailAddress
		});

		// Token: 0x04001C0D RID: 7181
		private readonly IStorePropertyBag propertyBag;

		// Token: 0x04001C0E RID: 7182
		private readonly string clientInfoString;
	}
}
