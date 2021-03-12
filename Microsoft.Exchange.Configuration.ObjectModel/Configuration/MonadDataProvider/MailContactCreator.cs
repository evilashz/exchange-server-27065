using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001B4 RID: 436
	internal class MailContactCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x0002F3C4 File Offset: 0x0002D5C4
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

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0002F4D8 File Offset: 0x0002D6D8
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
