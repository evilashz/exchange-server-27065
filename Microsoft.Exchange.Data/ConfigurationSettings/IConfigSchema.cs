using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001FA RID: 506
	internal interface IConfigSchema : IDiagnosableObject
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600117F RID: 4479
		string Name { get; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001180 RID: 4480
		string SectionName { get; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001181 RID: 4481
		IEnumerable<string> Settings { get; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001182 RID: 4482
		ExchangeConfigurationSection ScopeSchema { get; }

		// Token: 0x06001183 RID: 4483
		object GetPropertyValue(string propertyName);

		// Token: 0x06001184 RID: 4484
		object ParseAndValidateConfigValue(string settingName, string serializedValue, Type settingType = null);

		// Token: 0x06001185 RID: 4485
		void ValidateScopeName(string scopeName);

		// Token: 0x06001186 RID: 4486
		string ParseAndValidateScopeValue(string scopeName, object value);

		// Token: 0x06001187 RID: 4487
		bool TryGetConfigurationProperty(string name, out ConfigurationProperty property);

		// Token: 0x06001188 RID: 4488
		ConfigurationProperty GetConfigurationProperty(string name, Type type = null);

		// Token: 0x06001189 RID: 4489
		object GetDefaultConfigValue(ConfigurationProperty property);

		// Token: 0x0600118A RID: 4490
		bool CheckSettingExists(string name);

		// Token: 0x0600118B RID: 4491
		void ValidateConfigValue(string settingName, object value);
	}
}
