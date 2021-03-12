using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200029B RID: 667
	internal class DirectoryContext
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x0005620C File Offset: 0x0005440C
		public ITopologyConfigurationSession GlobalConfigSession
		{
			get
			{
				return this.globalConfigSession;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x00056214 File Offset: 0x00054414
		public IConfigurationSession TenantConfigSession
		{
			get
			{
				return this.tenantConfigSession;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0005621C File Offset: 0x0005441C
		public IRecipientSession TenantGalSession
		{
			get
			{
				return this.tenantGalSession;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00056224 File Offset: 0x00054424
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0005622C File Offset: 0x0005442C
		public ClientContext ClientContext
		{
			get
			{
				return this.clientContext;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00056234 File Offset: 0x00054434
		public DiagnosticsContext DiagnosticsContext
		{
			get
			{
				return this.diagnosticsContext;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0005623C File Offset: 0x0005443C
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x00056244 File Offset: 0x00054444
		public TrackingEventBudget TrackingBudget { get; internal set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0005624D File Offset: 0x0005444D
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x00056255 File Offset: 0x00054455
		public TrackingErrorCollection Errors { get; private set; }

		// Token: 0x060012A8 RID: 4776 RVA: 0x00056260 File Offset: 0x00054460
		public DirectoryContext(ClientContext clientContext, OrganizationId organizationId, ITopologyConfigurationSession globalConfigSession, IConfigurationSession tenantConfigSession, IRecipientSession tenantGalSession, TrackingEventBudget trackingBudget, DiagnosticsLevel diagnosticsLevel, TrackingErrorCollection errors, bool suppressIdAllocation)
		{
			this.clientContext = clientContext;
			this.organizationId = organizationId;
			this.globalConfigSession = globalConfigSession;
			this.tenantConfigSession = tenantConfigSession;
			this.tenantGalSession = tenantGalSession;
			this.diagnosticsContext = new DiagnosticsContext(suppressIdAllocation, diagnosticsLevel);
			this.TrackingBudget = trackingBudget;
			this.Errors = errors;
			if (!this.TrySetExternalOrgId(organizationId))
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "Failed to set ExternalOrgId. Assuming forest wide organization", new object[0]);
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000562D6 File Offset: 0x000544D6
		public bool IsTenantInScope(string tenantId)
		{
			return string.IsNullOrEmpty(this.externalOrganizationIdString) || string.Equals(this.externalOrganizationIdString, tenantId, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000562F4 File Offset: 0x000544F4
		public void Acquire()
		{
			Monitor.Enter(this);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000562FC File Offset: 0x000544FC
		public void Yield()
		{
			Monitor.Exit(this);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00056358 File Offset: 0x00054558
		private bool TrySetExternalOrgId(OrganizationId orgId)
		{
			if (orgId.Equals(OrganizationId.ForestWideOrgId))
			{
				this.externalOrganizationIdString = string.Empty;
				return true;
			}
			ExchangeConfigurationUnit configUnitPassedToDelegate = null;
			Guid empty = Guid.Empty;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 241, "TrySetExternalOrgId", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MessageTracking\\DirectoryContext.cs");
				configUnitPassedToDelegate = configurationSession.Read<ExchangeConfigurationUnit>(orgId.ConfigurationUnit);
			});
			if (!adoperationResult.Succeeded)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, Exception>(0, "Failed to get ExternalOrgId from AD. {0} Error: {1}", (adoperationResult.ErrorCode == ADOperationErrorCode.PermanentError) ? "Permanent" : "Retriable", adoperationResult.Exception);
				return false;
			}
			if (configUnitPassedToDelegate == null || !Guid.TryParse(configUnitPassedToDelegate.ExternalDirectoryOrganizationId, out empty))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(0, "Failed read ExternalOrgId from AD Session", new object[0]);
				return false;
			}
			this.externalOrganizationIdString = empty.ToString();
			return true;
		}

		// Token: 0x04000C81 RID: 3201
		private ITopologyConfigurationSession globalConfigSession;

		// Token: 0x04000C82 RID: 3202
		private IConfigurationSession tenantConfigSession;

		// Token: 0x04000C83 RID: 3203
		private IRecipientSession tenantGalSession;

		// Token: 0x04000C84 RID: 3204
		private OrganizationId organizationId;

		// Token: 0x04000C85 RID: 3205
		private string externalOrganizationIdString;

		// Token: 0x04000C86 RID: 3206
		private ClientContext clientContext;

		// Token: 0x04000C87 RID: 3207
		private DiagnosticsContext diagnosticsContext;
	}
}
