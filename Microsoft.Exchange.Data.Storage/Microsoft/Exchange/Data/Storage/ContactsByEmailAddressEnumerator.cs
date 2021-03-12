using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004CA RID: 1226
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactsByEmailAddressEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x060035D7 RID: 13783 RVA: 0x000D8BD8 File Offset: 0x000D6DD8
		public ContactsByEmailAddressEnumerator(IFolder contactsFolder, PropertyDefinition[] requestedProperties, string emailAddress) : this(contactsFolder, requestedProperties, new string[]
		{
			emailAddress
		})
		{
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000D8BF9 File Offset: 0x000D6DF9
		public ContactsByEmailAddressEnumerator(IFolder contactsFolder, PropertyDefinition[] requestedProperties, IEnumerable<string> emailAddresses)
		{
			ArgumentValidator.ThrowIfNull("contactsFolder", contactsFolder);
			ArgumentValidator.ThrowIfNull("requestedProperties", requestedProperties);
			ArgumentValidator.ThrowIfNull("emailAddress", emailAddresses);
			this.contactsFolder = contactsFolder;
			this.requestedProperties = requestedProperties;
			this.emailAddresses = emailAddresses;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000D8E24 File Offset: 0x000D7024
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			foreach (PropertyDefinition property in ContactsByEmailAddressEnumerator.EmailAddressSearchProperties)
			{
				IEnumerable<IStorePropertyBag> contactsEnumerator = new ContactsByPropertyValueEnumerator(this.contactsFolder, property, this.emailAddresses, this.requestedProperties);
				foreach (IStorePropertyBag storePropertyBag in contactsEnumerator)
				{
					yield return storePropertyBag;
				}
			}
			yield break;
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000D8E40 File Offset: 0x000D7040
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generics interface of GetEnumerator.");
		}

		// Token: 0x04001CDE RID: 7390
		private readonly PropertyDefinition[] requestedProperties;

		// Token: 0x04001CDF RID: 7391
		private readonly IEnumerable<string> emailAddresses;

		// Token: 0x04001CE0 RID: 7392
		public static PropertyDefinition[] EmailAddressSearchProperties = new PropertyDefinition[]
		{
			InternalSchema.Email1EmailAddress,
			InternalSchema.Email2EmailAddress,
			InternalSchema.Email3EmailAddress,
			ContactProtectedPropertiesSchema.ProtectedEmailAddress
		};

		// Token: 0x04001CE1 RID: 7393
		private readonly IFolder contactsFolder;
	}
}
