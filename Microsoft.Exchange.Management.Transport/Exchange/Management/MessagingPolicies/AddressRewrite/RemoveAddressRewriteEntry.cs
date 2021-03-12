using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000008 RID: 8
	[Cmdlet("remove", "addressrewriteentry", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class RemoveAddressRewriteEntry : RemoveSystemConfigurationObjectTask<AddressRewriteEntryIdParameter, AddressRewriteEntry>
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000030BC File Offset: 0x000012BC
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.CreateSession();
			configurationSession.UseConfigNC = false;
			return configurationSession;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000030DD File Offset: 0x000012DD
		protected override ObjectId RootId
		{
			get
			{
				return Utils.RootId;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000030E4 File Offset: 0x000012E4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAddressrewriteentry(this.Identity.ToString());
			}
		}
	}
}
