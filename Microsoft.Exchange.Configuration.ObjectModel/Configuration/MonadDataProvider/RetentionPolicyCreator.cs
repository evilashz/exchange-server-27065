using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001B8 RID: 440
	internal class RetentionPolicyCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000FAE RID: 4014 RVA: 0x0002F6BC File Offset: 0x0002D8BC
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"Name",
				"WhenChanged",
				"RetentionPolicyTagLinks",
				"ObjectId"
			};
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0002F6FC File Offset: 0x0002D8FC
		protected override void FillProperty(Type type, PSObject psObject, ConfigurableObject configObject, string propertyName)
		{
			if (propertyName == "RetentionPolicyTagLinks")
			{
				configObject.propertyBag[RetentionPolicySchema.RetentionPolicyTagLinks] = MockObjectCreator.GetPropertyBasedOnDefinition(RetentionPolicySchema.RetentionPolicyTagLinks, psObject.Members[propertyName].Value);
				return;
			}
			base.FillProperty(type, psObject, configObject, propertyName);
		}
	}
}
