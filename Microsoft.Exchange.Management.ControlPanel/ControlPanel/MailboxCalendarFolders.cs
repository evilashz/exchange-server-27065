using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000C1 RID: 193
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MailboxCalendarFolders : DataSourceService, IMailboxCalendarFolder, IEditObjectService<MailboxCalendarFolderRow, SetMailboxCalendarFolder>, IGetObjectService<MailboxCalendarFolderRow>
	{
		// Token: 0x06001CD4 RID: 7380 RVA: 0x0005922E File Offset: 0x0005742E
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxCalendarFolder?Identity@R:Self")]
		public PowerShellResults<MailboxCalendarFolderRow> GetObject(Identity identity)
		{
			identity.FaultIfNull();
			return base.GetObject<MailboxCalendarFolderRow>("Get-MailboxCalendarFolder", (Identity)identity.ToMailboxFolderIdParameter());
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0005924C File Offset: 0x0005744C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxCalendarFolder?Identity@R:Self+Set-MailboxCalendarFolder?Identity@W:Self")]
		public PowerShellResults<MailboxCalendarFolderRow> SetObject(Identity identity, SetMailboxCalendarFolder properties)
		{
			identity.FaultIfNull();
			return base.SetObject<MailboxCalendarFolderRow, SetMailboxCalendarFolder>("Set-MailboxCalendarFolder", (Identity)identity.ToMailboxFolderIdParameter(), properties);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0005926B File Offset: 0x0005746B
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxCalendarFolder?Identity@R:Self+Set-MailboxCalendarFolder?Identity@W:Self")]
		public PowerShellResults<MailboxCalendarFolderRow> StartPublishing(Identity identity, SetMailboxCalendarFolder properties)
		{
			properties.FaultIfNull();
			properties.PublishEnabled = true;
			return this.SetObject(identity, properties);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x00059282 File Offset: 0x00057482
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxCalendarFolder?Identity@R:Self+Set-MailboxCalendarFolder?Identity@W:Self")]
		public PowerShellResults<MailboxCalendarFolderRow> StopPublishing(Identity identity, SetMailboxCalendarFolder properties)
		{
			properties.FaultIfNull();
			properties.PublishEnabled = false;
			return this.SetObject(identity, properties);
		}

		// Token: 0x04001BAF RID: 7087
		internal const string GetCmdlet = "Get-MailboxCalendarFolder";

		// Token: 0x04001BB0 RID: 7088
		internal const string SetCmdlet = "Set-MailboxCalendarFolder";

		// Token: 0x04001BB1 RID: 7089
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001BB2 RID: 7090
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001BB3 RID: 7091
		private const string GetObjectRole = "Get-MailboxCalendarFolder?Identity@R:Self";

		// Token: 0x04001BB4 RID: 7092
		private const string SetObjectRole = "Get-MailboxCalendarFolder?Identity@R:Self+Set-MailboxCalendarFolder?Identity@W:Self";
	}
}
