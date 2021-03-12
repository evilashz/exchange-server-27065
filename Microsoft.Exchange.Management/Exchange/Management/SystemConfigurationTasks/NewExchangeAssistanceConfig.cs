using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000999 RID: 2457
	[Cmdlet("New", "ExchangeAssistanceConfig")]
	public sealed class NewExchangeAssistanceConfig : NewMultitenancyFixedNameSystemConfigurationObjectTask<ExchangeAssistance>
	{
		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x0016F3D1 File Offset: 0x0016D5D1
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x060057EB RID: 22507 RVA: 0x0016F3DC File Offset: 0x0016D5DC
		// (set) Token: 0x060057EC RID: 22508 RVA: 0x0016F3E4 File Offset: 0x0016D5E4
		[Parameter(Mandatory = false)]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x060057ED RID: 22509 RVA: 0x0016F3F0 File Offset: 0x0016D5F0
		protected override IConfigurable PrepareDataObject()
		{
			this.DataObject = (ExchangeAssistance)base.PrepareDataObject();
			if (!base.HasErrors)
			{
				this.DataObject.SetId(base.CurrentOrgContainerId.GetChildId("ExchangeAssistance"));
				this.DataObject.ControlPanelHelpURL = new Uri("http://help.outlook.com/140");
				this.DataObject.ControlPanelFeedbackURL = new Uri("http://go.microsoft.com/fwlink/?LinkId=89770");
				this.DataObject.ManagementConsoleHelpURL = new Uri("http://technet.microsoft.com/library(EXCHG.141)");
				this.DataObject.ManagementConsoleFeedbackURL = new Uri("http://go.microsoft.com/fwlink/?LinkId=103028");
				this.DataObject.OWAHelpURL = new Uri("http://help.outlook.com/140");
				this.DataObject.OWALightHelpURL = new Uri("http://help.outlook.com/140");
				this.DataObject.OWAFeedbackURL = new Uri("http://go.microsoft.com/fwlink/?LinkId=103030");
				this.DataObject.OWALightFeedbackURL = new Uri("http://go.microsoft.com/fwlink/?LinkId=103029");
				this.DataObject.WindowsLiveAccountPageURL = new Uri("http://go.microsoft.com/fwlink/?LinkId=91489");
				this.DataObject.PrivacyStatementURL = new Uri("http://go.microsoft.com/fwlink/?LinkId=91487");
				this.DataObject.CommunityURL = new Uri("http://go.microsoft.com/fwlink/?LinkId=178185");
			}
			return this.DataObject;
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x0016F521 File Offset: 0x0016D721
		protected override void InternalProcessRecord()
		{
			if (base.DataSession.Read<ExchangeAssistance>(this.DataObject.Id) == null)
			{
				base.InternalProcessRecord();
			}
			this.InstallCurrentVersionExchangeAssistanceAsChild();
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x0016F548 File Offset: 0x0016D748
		private void InstallCurrentVersionExchangeAssistanceAsChild()
		{
			ADObjectId childId = this.DataObject.Id.GetChildId(NewExchangeAssistanceConfig.CurrentVersionContainerName);
			if (this.IsExchangeAssistanceInstalled(childId, this.DataObject.Id))
			{
				return;
			}
			ExchangeAssistance exchangeAssistance = new ExchangeAssistance();
			exchangeAssistance.ProvisionalClone(this.DataObject);
			exchangeAssistance.SetId(childId);
			exchangeAssistance.ControlPanelHelpURL = new Uri("http://technet.microsoft.com/library(EXCHG.150)");
			exchangeAssistance.ControlPanelFeedbackURL = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253080");
			exchangeAssistance.ManagementConsoleHelpURL = new Uri("http://technet.microsoft.com/library(EXCHG.150)");
			exchangeAssistance.ManagementConsoleFeedbackURL = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253081");
			exchangeAssistance.OWAHelpURL = new Uri("http://o15.officeredir.microsoft.com/r/rlidOfficeWebHelp");
			exchangeAssistance.OWALightHelpURL = new Uri("http://o15.officeredir.microsoft.com/r/rlidOfficeWebHelp");
			exchangeAssistance.OWAFeedbackURL = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253083");
			exchangeAssistance.OWALightFeedbackURL = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253087");
			exchangeAssistance.WindowsLiveAccountPageURL = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253084");
			exchangeAssistance.PrivacyStatementURL = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253085");
			exchangeAssistance.CommunityURL = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253086");
			((IConfigurationSession)base.DataSession).Save(exchangeAssistance);
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x0016F65C File Offset: 0x0016D85C
		private bool IsExchangeAssistanceInstalled(ADObjectId id, ADObjectId rootId)
		{
			IEnumerable<ExchangeAssistance> enumerable = base.DataSession.FindPaged<ExchangeAssistance>(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, id), rootId, true, null, 0);
			IEnumerator<ExchangeAssistance> enumerator = enumerable.GetEnumerator();
			return enumerator != null && enumerator.MoveNext();
		}

		// Token: 0x040032AB RID: 12971
		private const string ContainerName = "ExchangeAssistance";

		// Token: 0x040032AC RID: 12972
		public static readonly string CurrentVersionContainerName = "ExchangeAssistance" + 15;
	}
}
