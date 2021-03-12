using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C53 RID: 3155
	public abstract class UpdateVirtualDirectoryVersion<T> : DataAccessTask<T> where T : ExchangeVirtualDirectory, new()
	{
		// Token: 0x17002507 RID: 9479
		// (get) Token: 0x060077CE RID: 30670 RVA: 0x001E82F4 File Offset: 0x001E64F4
		// (set) Token: 0x060077CF RID: 30671 RVA: 0x001E82FC File Offset: 0x001E64FC
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

		// Token: 0x17002508 RID: 9480
		// (get) Token: 0x060077D0 RID: 30672 RVA: 0x001E8305 File Offset: 0x001E6505
		// (set) Token: 0x060077D1 RID: 30673 RVA: 0x001E830D File Offset: 0x001E650D
		protected Server Server
		{
			get
			{
				return this.server;
			}
			set
			{
				this.server = value;
			}
		}

		// Token: 0x060077D2 RID: 30674 RVA: 0x001E8318 File Offset: 0x001E6518
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 70, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\VirtualDirectoryTasks\\UpdateVirtualDirectoryVersion.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x060077D3 RID: 30675 RVA: 0x001E8366 File Offset: 0x001E6566
		protected override IConfigDataProvider CreateSession()
		{
			return this.configurationSession;
		}

		// Token: 0x060077D4 RID: 30676 RVA: 0x001E8370 File Offset: 0x001E6570
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			IConfigDataProvider dataSession = base.DataSession;
			IEnumerable<T> enumerable = dataSession.FindPaged<T>(null, this.Server.Identity, true, null, 0);
			foreach (T t in enumerable)
			{
				if (!this.ShouldSkipVDir(t) && t.ExchangeVersion.IsOlderThan(t.MaximumSupportedExchangeObjectVersion))
				{
					try
					{
						t.SetExchangeVersion(t.MaximumSupportedExchangeObjectVersion);
						base.DataSession.Save(t);
					}
					catch (DataSourceTransientException exception)
					{
						base.WriteError(exception, ErrorCategory.WriteError, null);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060077D5 RID: 30677 RVA: 0x001E8454 File Offset: 0x001E6654
		protected virtual bool ShouldSkipVDir(T vDir)
		{
			return false;
		}

		// Token: 0x060077D6 RID: 30678 RVA: 0x001E8458 File Offset: 0x001E6658
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			ServerIdParameter serverIdParameter = new ServerIdParameter();
			this.Server = (Server)base.GetDataObject<Server>(serverIdParameter, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
			if (!this.ShouldUpdateVirtualDirectory())
			{
				base.WriteError(this.Server.GetServerRoleError(ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport), ErrorCategory.InvalidOperation, this.Server);
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x060077D7 RID: 30679 RVA: 0x001E84D6 File Offset: 0x001E66D6
		protected virtual bool ShouldUpdateVirtualDirectory()
		{
			return this.Server.IsClientAccessServer || this.Server.IsFfoWebServiceRole || this.Server.IsCafeServer || this.Server.IsOSPRole;
		}

		// Token: 0x04003BEE RID: 15342
		private ITopologyConfigurationSession configurationSession;

		// Token: 0x04003BEF RID: 15343
		private Server server;
	}
}
