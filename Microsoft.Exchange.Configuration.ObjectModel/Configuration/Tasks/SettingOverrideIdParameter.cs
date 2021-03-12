using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000149 RID: 329
	[Serializable]
	public sealed class SettingOverrideIdParameter : ADIdParameter
	{
		// Token: 0x06000BB8 RID: 3000 RVA: 0x00025101 File Offset: 0x00023301
		public SettingOverrideIdParameter()
		{
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00025109 File Offset: 0x00023309
		public SettingOverrideIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00025112 File Offset: 0x00023312
		public SettingOverrideIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002511B File Offset: 0x0002331B
		public SettingOverrideIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00025124 File Offset: 0x00023324
		public SettingOverrideIdParameter(ExchangeSettings exchangeSettings) : base(exchangeSettings.Id)
		{
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00025132 File Offset: 0x00023332
		public static SettingOverrideIdParameter Parse(string identity)
		{
			return new SettingOverrideIdParameter(identity);
		}
	}
}
