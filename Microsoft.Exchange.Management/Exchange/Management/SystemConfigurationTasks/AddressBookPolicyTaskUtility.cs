using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007E6 RID: 2022
	internal sealed class AddressBookPolicyTaskUtility
	{
		// Token: 0x060046BF RID: 18111 RVA: 0x00122A8C File Offset: 0x00120C8C
		internal static MultiValuedProperty<ADObjectId> ValidateAddressBook(IConfigDataProvider session, AddressListIdParameter[] addressBooks, AddressBookPolicyTaskUtility.GetUniqueObject getAddressBook, AddressBookMailboxPolicy target, Task.TaskErrorLoggingDelegate writeError)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>(false, AddressBookMailboxPolicySchema.AddressLists, new object[0]);
			if (addressBooks != null)
			{
				foreach (AddressListIdParameter addressListIdParameter in addressBooks)
				{
					if (addressListIdParameter != null)
					{
						IConfigurable configurable = getAddressBook(addressListIdParameter, session, null, new LocalizedString?(Strings.ErrorAddressListOrGlobalAddressListNotFound(addressListIdParameter.ToString())), new LocalizedString?(Strings.ErrorAddressListOrGlobalAddressListNotUnique(addressListIdParameter.ToString())));
						if (configurable != null)
						{
							if (multiValuedProperty.Contains((ADObjectId)configurable.Identity))
							{
								writeError(new InvalidOperationException(Strings.ErrorOabALAlreadyAssigned((target.Identity != null) ? target.Identity.ToString() : target.Name, configurable.Identity.ToString())), ErrorCategory.InvalidOperation, target.Identity);
							}
							else
							{
								multiValuedProperty.Add((ADObjectId)configurable.Identity);
							}
						}
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x020007E7 RID: 2023
		// (Invoke) Token: 0x060046C2 RID: 18114
		internal delegate IConfigurable GetUniqueObject(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError);
	}
}
