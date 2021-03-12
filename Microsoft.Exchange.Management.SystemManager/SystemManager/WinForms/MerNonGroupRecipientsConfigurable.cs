using System;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200024A RID: 586
	public class MerNonGroupRecipientsConfigurable : MerMailEnableRecipientConfigurable
	{
		// Token: 0x06001A4C RID: 6732 RVA: 0x00073FC0 File Offset: 0x000721C0
		public MerNonGroupRecipientsConfigurable() : this(true)
		{
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00073FC9 File Offset: 0x000721C9
		public MerNonGroupRecipientsConfigurable(bool allowedPublicFolder) : base(false, true, allowedPublicFolder, Strings.RecipientUserOrContact)
		{
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00073FE0 File Offset: 0x000721E0
		public override ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			ResultsLoaderProfile resultsLoaderProfile = base.BuildResultsLoaderProfile();
			resultsLoaderProfile.HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.MerNonGroupRecipientsPicker";
			return resultsLoaderProfile;
		}
	}
}
