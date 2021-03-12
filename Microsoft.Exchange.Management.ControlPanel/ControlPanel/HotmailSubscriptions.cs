using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002B0 RID: 688
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class HotmailSubscriptions : DataSourceService, IHotmailSubscriptions, IEditObjectService<HotmailSubscription, SetHotmailSubscription>, IGetObjectService<HotmailSubscription>
	{
		// Token: 0x06002BD6 RID: 11222 RVA: 0x00088580 File Offset: 0x00086780
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-HotmailSubscription?Mailbox&Identity@R:Self")]
		public PowerShellResults<HotmailSubscription> GetObject(Identity identity)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Get-HotmailSubscription");
			psCommand.AddParameters(new GetHotmailSubscription());
			return base.GetObject<HotmailSubscription>(psCommand, identity);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000885B1 File Offset: 0x000867B1
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-HotmailSubscription?Mailbox&Identity@R:Self+Set-HotmailSubscription?Mailbox&Identity@W:Self")]
		public PowerShellResults<HotmailSubscription> SetObject(Identity identity, SetHotmailSubscription properties)
		{
			return base.SetObject<HotmailSubscription, SetHotmailSubscription>("Set-HotmailSubscription", identity, properties);
		}

		// Token: 0x040021B7 RID: 8631
		private const string Noun = "HotmailSubscription";

		// Token: 0x040021B8 RID: 8632
		internal const string GetCmdlet = "Get-HotmailSubscription";

		// Token: 0x040021B9 RID: 8633
		internal const string SetCmdlet = "Set-HotmailSubscription";

		// Token: 0x040021BA RID: 8634
		internal const string ReadScope = "@R:Self";

		// Token: 0x040021BB RID: 8635
		internal const string WriteScope = "@W:Self";

		// Token: 0x040021BC RID: 8636
		private const string GetObjectRole = "MultiTenant+Get-HotmailSubscription?Mailbox&Identity@R:Self";

		// Token: 0x040021BD RID: 8637
		private const string SetObjectRole = "MultiTenant+Get-HotmailSubscription?Mailbox&Identity@R:Self+Set-HotmailSubscription?Mailbox&Identity@W:Self";
	}
}
