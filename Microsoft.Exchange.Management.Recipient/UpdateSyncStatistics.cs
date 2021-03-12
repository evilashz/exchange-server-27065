using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000D2 RID: 210
	[Cmdlet("Update", "SyncStatistics", SupportsShouldProcess = true)]
	public sealed class UpdateSyncStatistics : SetMultitenancySingletonSystemConfigurationObjectTask<GALSyncOrganization>
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0003B95E File Offset: 0x00039B5E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUpdateSyncStatistics;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0003B965 File Offset: 0x00039B65
		internal new OrganizationIdParameter Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0003B968 File Offset: 0x00039B68
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x0003B989 File Offset: 0x00039B89
		[Parameter]
		[ValidateRange(0, 2147483647)]
		public int NumberOfMailboxesCreated
		{
			get
			{
				return (int)(base.Fields["NumberOfMailboxesCreated"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfMailboxesCreated"] = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0003B9A1 File Offset: 0x00039BA1
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x0003B9C2 File Offset: 0x00039BC2
		[ValidateRange(0, 2147483647)]
		[Parameter]
		public int NumberOfMailboxesToCreate
		{
			get
			{
				return (int)(base.Fields["NumberOfMailboxesToCreate"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfMailboxesToCreate"] = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0003B9DA File Offset: 0x00039BDA
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x0003B9FB File Offset: 0x00039BFB
		[ValidateRange(0, 2147483647)]
		[Parameter]
		public int MailboxCreationElapsedMilliseconds
		{
			get
			{
				return (int)(base.Fields["MailboxCreationElapsedMilliseconds"] ?? 0);
			}
			set
			{
				base.Fields["MailboxCreationElapsedMilliseconds"] = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0003BA13 File Offset: 0x00039C13
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x0003BA34 File Offset: 0x00039C34
		[Parameter]
		[ValidateRange(0, 2147483647)]
		public int NumberOfExportSyncRuns
		{
			get
			{
				return (int)(base.Fields["NumberOfExportSyncRuns"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfExportSyncRuns"] = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0003BA4C File Offset: 0x00039C4C
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x0003BA6D File Offset: 0x00039C6D
		[ValidateRange(0, 2147483647)]
		[Parameter]
		public int NumberOfImportSyncRuns
		{
			get
			{
				return (int)(base.Fields["NumberOfImportSyncRuns"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfImportSyncRuns"] = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x0003BA85 File Offset: 0x00039C85
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x0003BAA6 File Offset: 0x00039CA6
		[Parameter]
		[ValidateRange(0, 2147483647)]
		public int NumberOfSucessfulExportSyncRuns
		{
			get
			{
				return (int)(base.Fields["NumberOfSucessfulExportSyncRuns"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfSucessfulExportSyncRuns"] = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0003BABE File Offset: 0x00039CBE
		// (set) Token: 0x06001072 RID: 4210 RVA: 0x0003BADF File Offset: 0x00039CDF
		[ValidateRange(0, 2147483647)]
		[Parameter]
		public int NumberOfSucessfulImportSyncRuns
		{
			get
			{
				return (int)(base.Fields["NumberOfSucessfulImportSyncRuns"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfSucessfulImportSyncRuns"] = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0003BAF7 File Offset: 0x00039CF7
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x0003BB18 File Offset: 0x00039D18
		[Parameter]
		[ValidateRange(0, 2147483647)]
		public int NumberOfConnectionErrors
		{
			get
			{
				return (int)(base.Fields["NumberOfConnectionErrors"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfConnectionErrors"] = value;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0003BB30 File Offset: 0x00039D30
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x0003BB51 File Offset: 0x00039D51
		[Parameter]
		[ValidateRange(0, 2147483647)]
		public int NumberOfPermissionErrors
		{
			get
			{
				return (int)(base.Fields["NumberOfPermissionErrors"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfPermissionErrors"] = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0003BB69 File Offset: 0x00039D69
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x0003BB8A File Offset: 0x00039D8A
		[ValidateRange(0, 2147483647)]
		[Parameter]
		public int NumberOfIlmLogicErrors
		{
			get
			{
				return (int)(base.Fields["NumberOfIlmLogicErrors"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfIlmLogicErrors"] = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0003BBA2 File Offset: 0x00039DA2
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x0003BBC3 File Offset: 0x00039DC3
		[ValidateRange(0, 2147483647)]
		[Parameter]
		public int NumberOfIlmOtherErrors
		{
			get
			{
				return (int)(base.Fields["NumberOfIlmOtherErrors"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfIlmOtherErrors"] = value;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0003BBDB File Offset: 0x00039DDB
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x0003BBFC File Offset: 0x00039DFC
		[ValidateRange(0, 2147483647)]
		[Parameter]
		public int NumberOfLiveIdErrors
		{
			get
			{
				return (int)(base.Fields["NumberOfLiveIdErrors"] ?? 0);
			}
			set
			{
				base.Fields["NumberOfLiveIdErrors"] = value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0003BC14 File Offset: 0x00039E14
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x0003BC2B File Offset: 0x00039E2B
		[Parameter]
		public string ClientData
		{
			get
			{
				return (string)base.Fields["CustomerData"];
			}
			set
			{
				base.Fields["CustomerData"] = value;
			}
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003BC40 File Offset: 0x00039E40
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			GALSyncOrganization galsyncOrganization = (GALSyncOrganization)base.PrepareDataObject();
			if (string.IsNullOrEmpty(this.ClientData))
			{
				galsyncOrganization.GALSyncClientData = string.Format("<xml><Version>14.01.0098.00</Version><LastRun>{0}</LastRun></xml>", ExDateTime.Now.ToUtc());
			}
			else
			{
				galsyncOrganization.GALSyncClientData = this.ClientData;
			}
			TaskLogger.LogExit();
			return galsyncOrganization;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0003BCA4 File Offset: 0x00039EA4
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (base.Fields.IsChanged("NumberOfMailboxesCreated"))
			{
				GalsyncCounters.ClientReportedNumberOfMailboxesCreated.IncrementBy((long)this.NumberOfMailboxesCreated);
			}
			if (base.Fields.IsChanged("NumberOfMailboxesToCreate"))
			{
				GalsyncCounters.ClientReportedNumberOfMailboxesToCreate.IncrementBy((long)this.NumberOfMailboxesToCreate);
			}
			if (base.Fields.IsChanged("MailboxCreationElapsedMilliseconds"))
			{
				GalsyncCounters.ClientReportedMailboxCreationElapsedMilliseconds.IncrementBy((long)this.MailboxCreationElapsedMilliseconds);
			}
			if (base.Fields.IsChanged("NumberOfExportSyncRuns"))
			{
				GalsyncCounters.NumberOfExportSyncRuns.IncrementBy((long)this.NumberOfExportSyncRuns);
			}
			if (base.Fields.IsChanged("NumberOfImportSyncRuns"))
			{
				GalsyncCounters.NumberOfImportSyncRuns.IncrementBy((long)this.NumberOfImportSyncRuns);
			}
			if (base.Fields.IsChanged("NumberOfSucessfulExportSyncRuns"))
			{
				GalsyncCounters.NumberOfSucessfulExportSyncRuns.IncrementBy((long)this.NumberOfSucessfulExportSyncRuns);
			}
			if (base.Fields.IsChanged("NumberOfSucessfulImportSyncRuns"))
			{
				GalsyncCounters.NumberOfSucessfulImportSyncRuns.IncrementBy((long)this.NumberOfSucessfulImportSyncRuns);
			}
			if (base.Fields.IsChanged("NumberOfConnectionErrors"))
			{
				GalsyncCounters.NumberOfConnectionErrors.IncrementBy((long)this.NumberOfConnectionErrors);
			}
			if (base.Fields.IsChanged("NumberOfPermissionErrors"))
			{
				GalsyncCounters.NumberOfPermissionErrors.IncrementBy((long)this.NumberOfPermissionErrors);
			}
			if (base.Fields.IsChanged("NumberOfIlmLogicErrors"))
			{
				GalsyncCounters.NumberOfILMLogicErrors.IncrementBy((long)this.NumberOfIlmLogicErrors);
			}
			if (base.Fields.IsChanged("NumberOfIlmOtherErrors"))
			{
				GalsyncCounters.NumberOfILMOtherErrors.IncrementBy((long)this.NumberOfIlmOtherErrors);
			}
			if (base.Fields.IsChanged("NumberOfLiveIdErrors"))
			{
				GalsyncCounters.NumberOfLiveIdErrors.IncrementBy((long)this.NumberOfLiveIdErrors);
			}
		}

		// Token: 0x040002E9 RID: 745
		private const string MailboxesCreatedPara = "NumberOfMailboxesCreated";

		// Token: 0x040002EA RID: 746
		private const string MailboxesToCreatePara = "NumberOfMailboxesToCreate";

		// Token: 0x040002EB RID: 747
		private const string MailboxCreationElapsedMillisecondsPara = "MailboxCreationElapsedMilliseconds";

		// Token: 0x040002EC RID: 748
		private const string ExportSyncRunsPara = "NumberOfExportSyncRuns";

		// Token: 0x040002ED RID: 749
		private const string ImportSyncRunsPara = "NumberOfImportSyncRuns";

		// Token: 0x040002EE RID: 750
		private const string SucessfulExportSyncRunsPara = "NumberOfSucessfulExportSyncRuns";

		// Token: 0x040002EF RID: 751
		private const string SucessfulImportSyncRunsPara = "NumberOfSucessfulImportSyncRuns";

		// Token: 0x040002F0 RID: 752
		private const string ConnectionErrorsPara = "NumberOfConnectionErrors";

		// Token: 0x040002F1 RID: 753
		private const string PermissionErrorsPara = "NumberOfPermissionErrors";

		// Token: 0x040002F2 RID: 754
		private const string IlmLogicErrorsPara = "NumberOfIlmLogicErrors";

		// Token: 0x040002F3 RID: 755
		private const string IlmOtherErrorsPara = "NumberOfIlmOtherErrors";

		// Token: 0x040002F4 RID: 756
		private const string LiveIdErrorsPara = "NumberOfLiveIdErrors";

		// Token: 0x040002F5 RID: 757
		private const string ClientDataPara = "CustomerData";

		// Token: 0x040002F6 RID: 758
		private const string ClientDataStringFmt = "<xml><Version>14.01.0098.00</Version><LastRun>{0}</LastRun></xml>";
	}
}
