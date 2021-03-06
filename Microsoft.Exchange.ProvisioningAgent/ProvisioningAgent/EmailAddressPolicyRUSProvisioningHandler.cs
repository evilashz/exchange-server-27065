using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.DefaultProvisioningAgent.Rus;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200002C RID: 44
	internal class EmailAddressPolicyRUSProvisioningHandler : RUSProvisioningHandler
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000072D0 File Offset: 0x000054D0
		protected bool UpdateSecondaryAddressesOnly
		{
			get
			{
				return base.UserSpecifiedParameters["UpdateSecondaryAddressesOnly"] != null && ((SwitchParameter)base.UserSpecifiedParameters["UpdateSecondaryAddressesOnly"]).ToBool();
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007310 File Offset: 0x00005510
		protected override bool UpdateRecipient(ADRecipient recipient)
		{
			string text = null;
			NetworkCredential credential = null;
			string text2 = (base.UserSpecifiedParameters["DomainController"] != null) ? base.UserSpecifiedParameters["DomainController"].ToString() : null;
			string originatingServer = recipient.OriginatingServer;
			ExTraceGlobals.RusTracer.TraceDebug((long)this.GetHashCode(), "EmailAddressPolicyRUSProvisioningHandler.UpdateRecipient: recipient={0}, TaskName={1}, ConfigurationDomainController={2}, RecipientDomainController={3}, GlobalCatalog={4}", new object[]
			{
				recipient.Identity.ToString(),
				base.TaskName,
				text2,
				originatingServer,
				text
			});
			base.LogMessage(Strings.VerboseUpdateRecipientObject(recipient.Id.ToString(), text2 ?? "<null>", originatingServer ?? "<null>", text ?? "<null>"));
			bool flag = new SystemPolicyHandler(text2, originatingServer, text, credential, base.PartitionId, base.UserScope, base.ProvisioningCache, base.LogMessage).UpdateRecipient(recipient);
			bool flag2 = new EmailAddressPolicyHandler(text2, originatingServer, text, credential, base.PartitionId, base.UserScope, base.ProvisioningCache, this.UpdateSecondaryAddressesOnly, base.LogMessage).UpdateRecipient(recipient);
			return flag || flag2;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007440 File Offset: 0x00005640
		protected override void Validate(IConfigurable readOnlyADObject, List<ProvisioningValidationError> errors)
		{
			base.Validate(readOnlyADObject, errors);
			ExTraceGlobals.RusTracer.TraceDebug<string, string>((long)this.GetHashCode(), "RUSProvisioningHandler.Validate: readOnlyADObject={0}, TaskName={1}", readOnlyADObject.Identity.ToString(), base.TaskName);
			if (!StringComparer.InvariantCultureIgnoreCase.Equals("Remove-EmailAddressPolicy", base.TaskName))
			{
				if (readOnlyADObject is ADPresentationObject)
				{
					ADObject dataObject = ((ADPresentationObject)readOnlyADObject).DataObject;
				}
				else
				{
					ADObject adobject = (ADObject)readOnlyADObject;
				}
				EmailAddressPolicy emailAddressPolicy = readOnlyADObject as EmailAddressPolicy;
				if (emailAddressPolicy != null)
				{
					errors.AddRange(new EmailAddressPolicyHandler(null, null, null, null, base.PartitionId, base.UserScope, base.ProvisioningCache, base.LogMessage).Validate(emailAddressPolicy));
				}
			}
		}

		// Token: 0x040000A1 RID: 161
		internal static readonly string[] SupportedTasks = new string[]
		{
			"New-EmailAddressPolicy",
			"Set-EmailAddressPolicy",
			"Update-EmailAddressPolicy",
			"Remove-EmailAddressPolicy"
		};
	}
}
