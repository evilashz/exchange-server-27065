using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B29 RID: 2857
	[Cmdlet("Get", "DsnSupportedLanguages")]
	public sealed class GetDsnSupportedLanguages : Task
	{
		// Token: 0x060066DA RID: 26330 RVA: 0x001A9234 File Offset: 0x001A7434
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			int[] sendToPipeline = LanguagePackInfo.expectedCultureLcids.ToArray();
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}
	}
}
