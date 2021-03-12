using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x020001B0 RID: 432
	internal class RunData
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x00062EE0 File Offset: 0x000610E0
		public RunData(Guid runId, DirectoryProcessorMailboxData mailboxData, ThrowIfShuttingDown throwIfShuttingDown)
		{
			ValidateArgument.NotNull(mailboxData, "mailboxData");
			ValidateArgument.NotNull(throwIfShuttingDown, "throwIfShuttingDown");
			this.RunId = runId;
			this.MailboxData = mailboxData;
			this.throwIfShuttingDown = throwIfShuttingDown;
			this.RunStartTime = DateTime.UtcNow;
			this.TenantId = RunData.GetTenantIdentifiableDN(this.MailboxData.OrgId);
			this.TenantGuid = RunData.GetTenantGuid(this.MailboxData.OrgId);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00062F58 File Offset: 0x00061158
		public static string GetTenantIdentifiableDN(OrganizationId orgId)
		{
			string result;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.DirectoryProcessorTenantLogging.Enabled || Datacenter.IsPartnerHostedOnly(true))
			{
				ExAssert.RetailAssert(null != orgId && null != orgId.OrganizationalUnit, "orgId and orgId.OrganizationalUnit should be non-null in Datacenter");
				result = orgId.OrganizationalUnit.ToDNString();
			}
			else
			{
				ExAssert.RetailAssert(orgId.OrganizationalUnit == null, "orgId.OrganizationalUnit should be null for Enterprise");
				result = "Enterprise";
			}
			return result;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00062FD0 File Offset: 0x000611D0
		private static Guid GetTenantGuid(OrganizationId orgId)
		{
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(orgId);
			return iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId();
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00062FEC File Offset: 0x000611EC
		public void CreateRunFolder()
		{
			string grammarGenerationRunFolderPath = GrammarFileDistributionShare.GetGrammarGenerationRunFolderPath(this.OrgId, this.MailboxGuid, this.RunId);
			if (!Directory.Exists(grammarGenerationRunFolderPath))
			{
				Directory.CreateDirectory(grammarGenerationRunFolderPath);
			}
			this.RunFolderPath = grammarGenerationRunFolderPath;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00063027 File Offset: 0x00061227
		public void ThrowIfShuttingDown()
		{
			this.throwIfShuttingDown(this);
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00063035 File Offset: 0x00061235
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x0006303D File Offset: 0x0006123D
		public Guid RunId { get; private set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00063046 File Offset: 0x00061246
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x0006304E File Offset: 0x0006124E
		public DirectoryProcessorMailboxData MailboxData { get; private set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00063057 File Offset: 0x00061257
		public OrganizationId OrgId
		{
			get
			{
				return this.MailboxData.OrgId;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00063064 File Offset: 0x00061264
		public Guid DatabaseGuid
		{
			get
			{
				return this.MailboxData.DatabaseGuid;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00063071 File Offset: 0x00061271
		public Guid MailboxGuid
		{
			get
			{
				return this.MailboxData.MailboxGuid;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0006307E File Offset: 0x0006127E
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x00063086 File Offset: 0x00061286
		public DateTime RunStartTime { get; private set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0006308F File Offset: 0x0006128F
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00063097 File Offset: 0x00061297
		public string TenantId { get; private set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x000630A0 File Offset: 0x000612A0
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x000630A8 File Offset: 0x000612A8
		public Guid TenantGuid { get; private set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x000630B1 File Offset: 0x000612B1
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x000630B9 File Offset: 0x000612B9
		public string RunFolderPath { get; private set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x000630C2 File Offset: 0x000612C2
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x000630CA File Offset: 0x000612CA
		public int UserCount { get; set; }

		// Token: 0x04000A9E RID: 2718
		private readonly ThrowIfShuttingDown throwIfShuttingDown;
	}
}
