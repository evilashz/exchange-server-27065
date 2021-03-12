using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200059C RID: 1436
	internal static class SharingPolicyNonAdProperties
	{
		// Token: 0x060042C2 RID: 17090 RVA: 0x000FB725 File Offset: 0x000F9925
		internal static object GetDefault(IPropertyBag properties)
		{
			return properties[SharingPolicyNonAdProperties.DefaultPropetyDefinition];
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x000FB732 File Offset: 0x000F9932
		internal static void SetDefault(object value, IPropertyBag properties)
		{
			properties[SharingPolicyNonAdProperties.DefaultPropetyDefinition] = value;
		}

		// Token: 0x04002D5F RID: 11615
		internal static readonly ADPropertyDefinition DefaultPropetyDefinition = new ADPropertyDefinition("Default", ExchangeObjectVersion.Exchange2010, typeof(bool), "Default", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
