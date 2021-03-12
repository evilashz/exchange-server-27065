using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000006 RID: 6
	[Cmdlet("get", "addressrewriteentry")]
	public class GetAddressRewriteEntry : GetSystemConfigurationObjectTask<AddressRewriteEntryIdParameter, AddressRewriteEntry>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002EC4 File Offset: 0x000010C4
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.CreateSession();
			configurationSession.UseConfigNC = false;
			return configurationSession;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002EE5 File Offset: 0x000010E5
		protected override ObjectId RootId
		{
			get
			{
				return Utils.RootId;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002EEC File Offset: 0x000010EC
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
