using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000249 RID: 585
	public class MerMailEnabledGroupConfigurable : MerMailEnableRecipientConfigurable
	{
		// Token: 0x06001A4A RID: 6730 RVA: 0x00073F89 File Offset: 0x00072189
		public MerMailEnabledGroupConfigurable() : base(true, false, false, Strings.MailEnabledGroup)
		{
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00073FA0 File Offset: 0x000721A0
		public override ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			ResultsLoaderProfile resultsLoaderProfile = base.BuildResultsLoaderProfile();
			resultsLoaderProfile.HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.MerMailEnabledGroupPicker";
			return resultsLoaderProfile;
		}
	}
}
