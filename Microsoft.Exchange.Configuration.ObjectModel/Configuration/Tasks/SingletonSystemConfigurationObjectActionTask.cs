using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000042 RID: 66
	public abstract class SingletonSystemConfigurationObjectActionTask<TDataObject> : SingletonObjectActionTask<TDataObject> where TDataObject : ADObject, new()
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C068 File Offset: 0x0000A268
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0000C070 File Offset: 0x0000A270
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C079 File Offset: 0x0000A279
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new ADObjectTaskModuleFactory();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C080 File Offset: 0x0000A280
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 251, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADObjectActionTask.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000C0CF File Offset: 0x0000A2CF
		protected override IConfigDataProvider CreateSession()
		{
			return this.configurationSession;
		}

		// Token: 0x040000BA RID: 186
		private ITopologyConfigurationSession configurationSession;
	}
}
