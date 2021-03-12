using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000222 RID: 546
	internal class ServiceCookieSchema : BaseCookieSchema
	{
		// Token: 0x04000B47 RID: 2887
		public static readonly HygienePropertyDefinition LastCompletedTimeProperty = new HygienePropertyDefinition("LastCompletedTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
