using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001AD RID: 429
	internal class UMMailboxCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000F91 RID: 3985 RVA: 0x0002E9E0 File Offset: 0x0002CBE0
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"UMMailboxPolicy",
				"EmailAddresses",
				"AutomaticSpeechRecognitionEnabled",
				"AllowUMCallsFromNonUsers",
				"FaxEnabled",
				"AnonymousCallersCanLeaveMessages",
				"CallAnsweringRulesEnabled",
				"OperatorNumber",
				"UMDialPlan"
			};
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0002EA48 File Offset: 0x0002CC48
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "AutomaticSpeechRecognitionEnabled")
			{
				configObject.propertyBag[UMMailboxSchema.ASREnabled] = MockObjectCreator.GetSingleProperty(psObject.Members[propertyName].Value, UMMailboxSchema.ASREnabled.Type);
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
