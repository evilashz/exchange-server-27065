using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200016C RID: 364
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class SecondaryDomainTaskBase : ComponentInfoBasedTask
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0003EC65 File Offset: 0x0003CE65
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x0003EC6D File Offset: 0x0003CE6D
		internal IConfigurationSession Session { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0003EC76 File Offset: 0x0003CE76
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x0003EC7E File Offset: 0x0003CE7E
		private protected ADObjectId RootOrgContainerId { protected get; private set; }

		// Token: 0x06000D7A RID: 3450 RVA: 0x0003EC88 File Offset: 0x0003CE88
		protected SecondaryDomainTaskBase()
		{
			base.ImplementsResume = false;
			base.IsTenantOrganization = true;
			base.IsDatacenter = true;
			base.ShouldLoadDatacenterConfigFile = false;
			base.ComponentInfoFileNames = new List<string>();
			base.ComponentInfoFileNames.Add("setup\\data\\DatacenterSecondaryDomainConfig.xml");
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0003ECD7 File Offset: 0x0003CED7
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x0003ECDE File Offset: 0x0003CEDE
		public new LongPath UpdatesDir
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0003ECE5 File Offset: 0x0003CEE5
		protected override bool IsInnerRunspaceThrottlingEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0003ECE8 File Offset: 0x0003CEE8
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new ManageOrganizationTaskModuleFactory();
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003ECEF File Offset: 0x0003CEEF
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.RootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003ED04 File Offset: 0x0003CF04
		internal virtual IConfigurationSession CreateSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, this.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.RescopeToSubtree(sessionSettings), 110, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\SecondaryDomainTaskBase.cs");
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0003ED5A File Offset: 0x0003CF5A
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			this.Session = this.CreateSession();
			TaskLogger.LogExit();
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0003ED78 File Offset: 0x0003CF78
		protected override bool IsKnownException(Exception e)
		{
			return e is DataSourceOperationException || e is DataSourceTransientException || e is DataValidationException || e is ManagementObjectNotFoundException || e is ManagementObjectAmbiguousException || base.IsKnownException(e);
		}
	}
}
