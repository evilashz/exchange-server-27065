using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001B3 RID: 435
	internal class MailUserCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FA2 RID: 4002 RVA: 0x0002F250 File Offset: 0x0002D450
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"DisplayName",
				"Alias",
				"HiddenFromAddressListsEnabled",
				"UseMapiRichTextFormat",
				"WhenChanged",
				"CustomAttribute1",
				"CustomAttribute2",
				"CustomAttribute3",
				"CustomAttribute4",
				"CustomAttribute5",
				"CustomAttribute6",
				"CustomAttribute7",
				"CustomAttribute8",
				"CustomAttribute9",
				"CustomAttribute10",
				"CustomAttribute11",
				"CustomAttribute12",
				"CustomAttribute13",
				"CustomAttribute14",
				"CustomAttribute15",
				"EmailAddressPolicyEnabled",
				"EmailAddresses",
				"ExternalEmailAddress",
				"MaxSendSize",
				"MaxReceiveSize",
				"AcceptMessagesOnlyFromSendersOrMembers",
				"RejectMessagesFromSendersOrMembers",
				"RequireSenderAuthenticationEnabled"
			};
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0002F364 File Offset: 0x0002D564
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "RequireSenderAuthenticationEnabled")
			{
				configObject.propertyBag[MailEnabledRecipientSchema.RequireSenderAuthenticationEnabled] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, MailEnabledRecipientSchema.RequireSenderAuthenticationEnabled.Type);
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
