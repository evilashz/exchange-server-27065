using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001E2 RID: 482
	internal sealed class OABGeneratorTaskContext : AssistantTaskContext
	{
		// Token: 0x060012A6 RID: 4774 RVA: 0x0006CF0B File Offset: 0x0006B10B
		private OABGeneratorTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job) : base(mailboxData, job, null)
		{
			this.returnStep = new Stack<AssistantStep>(5);
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0006CF22 File Offset: 0x0006B122
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x0006CF2A File Offset: 0x0006B12A
		public AssistantStep OABStep
		{
			get
			{
				return this.oabStep;
			}
			set
			{
				this.oabStep = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x0006CF33 File Offset: 0x0006B133
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x0006CF3B File Offset: 0x0006B13B
		public Action<OABGeneratorTaskContext> Cleanup
		{
			get
			{
				return this.cleanup;
			}
			set
			{
				this.cleanup = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x0006CF44 File Offset: 0x0006B144
		public Stack<AssistantStep> ReturnStep
		{
			get
			{
				return this.returnStep;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0006CF4C File Offset: 0x0006B14C
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x0006CF54 File Offset: 0x0006B154
		public Queue<OfflineAddressBook> OABsToGenerate
		{
			get
			{
				return this.oabsToGenerate;
			}
			set
			{
				this.oabsToGenerate = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x0006CF5D File Offset: 0x0006B15D
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x0006CF65 File Offset: 0x0006B165
		public OfflineAddressBook CurrentOAB
		{
			get
			{
				return this.currentOAB;
			}
			set
			{
				this.currentOAB = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0006CF6E File Offset: 0x0006B16E
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x0006CF76 File Offset: 0x0006B176
		public IConfigurationSession PerOrgAdSystemConfigSession
		{
			get
			{
				return this.perOrgAdSystemConfigSession;
			}
			set
			{
				this.perOrgAdSystemConfigSession = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0006CF7F File Offset: 0x0006B17F
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x0006CF87 File Offset: 0x0006B187
		public OABGenerator OABGenerator
		{
			get
			{
				return this.oabGenerator;
			}
			set
			{
				this.oabGenerator = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0006CF90 File Offset: 0x0006B190
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x0006CF98 File Offset: 0x0006B198
		public ADUser OrganizationMailbox
		{
			get
			{
				return this.organizationMailbox;
			}
			set
			{
				this.organizationMailbox = value;
			}
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0006CFA4 File Offset: 0x0006B1A4
		public static OABGeneratorTaskContext FromAssistantTaskContext(AssistantTaskContext assistantTaskContext)
		{
			return new OABGeneratorTaskContext(assistantTaskContext.MailboxData, assistantTaskContext.Job)
			{
				Args = assistantTaskContext.Args,
				Step = assistantTaskContext.Step
			};
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0006CFDC File Offset: 0x0006B1DC
		public static OABGeneratorTaskContext FromOABGeneratorTaskContext(OABGeneratorTaskContext oabGeneratorTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = new OABGeneratorTaskContext(oabGeneratorTaskContext.MailboxData, oabGeneratorTaskContext.Job);
			oabgeneratorTaskContext.Args = null;
			oabgeneratorTaskContext.Step = oabGeneratorTaskContext.Step;
			oabgeneratorTaskContext.oabStep = oabGeneratorTaskContext.oabStep;
			oabgeneratorTaskContext.cleanup = oabGeneratorTaskContext.cleanup;
			AssistantStep[] array = oabGeneratorTaskContext.returnStep.ToArray();
			for (int i = array.Length - 1; i >= 0; i--)
			{
				oabgeneratorTaskContext.returnStep.Push(array[i]);
			}
			if (oabGeneratorTaskContext.oabsToGenerate != null)
			{
				oabgeneratorTaskContext.oabsToGenerate = new Queue<OfflineAddressBook>(oabGeneratorTaskContext.oabsToGenerate);
			}
			oabgeneratorTaskContext.currentOAB = oabGeneratorTaskContext.currentOAB;
			oabgeneratorTaskContext.perOrgAdSystemConfigSession = oabGeneratorTaskContext.perOrgAdSystemConfigSession;
			oabgeneratorTaskContext.oabGenerator = oabGeneratorTaskContext.oabGenerator;
			oabgeneratorTaskContext.organizationMailbox = oabGeneratorTaskContext.organizationMailbox;
			return oabgeneratorTaskContext;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0006D09A File Offset: 0x0006B29A
		public void ClearPerOABData()
		{
			this.cleanup = null;
			this.returnStep.Clear();
			if (this.oabGenerator != null)
			{
				this.oabGenerator.Dispose();
				this.oabGenerator = null;
			}
		}

		// Token: 0x04000B53 RID: 2899
		private AssistantStep oabStep;

		// Token: 0x04000B54 RID: 2900
		private Action<OABGeneratorTaskContext> cleanup;

		// Token: 0x04000B55 RID: 2901
		private Stack<AssistantStep> returnStep;

		// Token: 0x04000B56 RID: 2902
		private Queue<OfflineAddressBook> oabsToGenerate;

		// Token: 0x04000B57 RID: 2903
		private OfflineAddressBook currentOAB;

		// Token: 0x04000B58 RID: 2904
		private IConfigurationSession perOrgAdSystemConfigSession;

		// Token: 0x04000B59 RID: 2905
		private OABGenerator oabGenerator;

		// Token: 0x04000B5A RID: 2906
		private ADUser organizationMailbox;
	}
}
