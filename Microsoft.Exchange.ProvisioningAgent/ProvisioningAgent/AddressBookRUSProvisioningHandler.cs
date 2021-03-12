using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.DefaultProvisioningAgent.Rus;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200002B RID: 43
	internal class AddressBookRUSProvisioningHandler : RUSProvisioningHandler
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000071E8 File Offset: 0x000053E8
		protected override void Validate(IConfigurable readOnlyADObject, List<ProvisioningValidationError> errors)
		{
			base.Validate(readOnlyADObject, errors);
			ExTraceGlobals.RusTracer.TraceDebug<string, string>((long)this.GetHashCode(), "RUSProvisioningHandler.Validate: readOnlyADObject={0}, TaskName={1}", readOnlyADObject.Identity.ToString(), base.TaskName);
			ADObject adobject;
			if (readOnlyADObject is ADPresentationObject)
			{
				adobject = ((ADPresentationObject)readOnlyADObject).DataObject;
			}
			else
			{
				adobject = (ADObject)readOnlyADObject;
			}
			AddressBookBase addressBookBase = adobject as AddressBookBase;
			if (addressBookBase != null)
			{
				errors.AddRange(new AddressBookHandler(null, null, null, null, base.PartitionId, base.UserScope, base.ProvisioningCache, base.LogMessage).Validate(addressBookBase));
			}
		}

		// Token: 0x040000A0 RID: 160
		internal static readonly string[] SupportedTasks = new string[]
		{
			"New-AddressList",
			"Set-AddressList",
			"Update-AddressList",
			"New-GlobalAddressList",
			"Set-GlobalAddressList",
			"Update-GlobalAddressList"
		};
	}
}
