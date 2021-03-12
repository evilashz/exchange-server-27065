using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000099 RID: 153
	public class DelegateTask : RecipientObjectActionTask<SecurityPrincipalIdParameter, ADGroup>
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002BB28 File Offset: 0x00029D28
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0002BB30 File Offset: 0x00029D30
		protected bool Add
		{
			get
			{
				return this.add;
			}
			set
			{
				this.add = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002BB39 File Offset: 0x00029D39
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x0002BB50 File Offset: 0x00029D50
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public DelegateRoleType Role
		{
			get
			{
				return (DelegateRoleType)base.Fields["Role"];
			}
			set
			{
				base.Fields["Role"] = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002BB68 File Offset: 0x00029D68
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0002BB7F File Offset: 0x00029D7F
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string Scope
		{
			get
			{
				return (string)base.Fields["Scope"];
			}
			set
			{
				base.Fields["Scope"] = value;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002BB92 File Offset: 0x00029D92
		public DelegateTask()
		{
			base.Fields["Role"] = DelegateRoleType.OrgAdmin;
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002BBC4 File Offset: 0x00029DC4
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			recipientSession.EnforceDefaultScope = false;
			return recipientSession;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002BBE8 File Offset: 0x00029DE8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Role == DelegateRoleType.ServerAdmin && string.IsNullOrEmpty(this.Scope))
			{
				base.WriteError(new ArgumentException(Strings.ErrorNeedToSpecifyScopeParameter, "Scope"), ErrorCategory.InvalidArgument, this.Role);
				return;
			}
			if (this.Role != DelegateRoleType.ServerAdmin && !string.IsNullOrEmpty(this.Scope) && string.Compare(this.Scope, Strings.OrganizationWide.ToString(), true, CultureInfo.InvariantCulture) != 0 && string.Compare(this.Scope, "Organization Wide", true, CultureInfo.InvariantCulture) != 0)
			{
				base.WriteError(new ArgumentException(Strings.ErrorCannotSpecifyScopeParameter, "Scope"), ErrorCategory.InvalidArgument, this.Role);
			}
			IEnumerable<ADRecipient> objects = this.Identity.GetObjects<ADRecipient>(this.RootId, base.TenantGlobalCatalogSession);
			using (IEnumerator<ADRecipient> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					this.user = enumerator.Current;
					if (enumerator.MoveNext())
					{
						base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorUserNotUnique(this.Identity.ToString())), ErrorCategory.InvalidData, null);
					}
				}
				else if (this.Add)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorUserNotFound(this.Identity.ToString())), ErrorCategory.ObjectNotFound, null);
				}
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002BD58 File Offset: 0x00029F58
		protected override IConfigurable PrepareDataObject()
		{
			ADGroup adgroup = null;
			this.configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 197, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\permission\\DelegateBaseTask.cs");
			if (this.Role == DelegateRoleType.ServerAdmin)
			{
				ServerIdParameter serverIdParameter = null;
				try
				{
					serverIdParameter = ServerIdParameter.Parse(this.Scope);
				}
				catch (ArgumentException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidData, null);
				}
				this.server = (Server)base.GetDataObject<Server>(serverIdParameter, this.configSession, null, new LocalizedString?(Strings.ErrorServerNotFound((string)serverIdParameter)), new LocalizedString?(Strings.ErrorServerNotUnique((string)serverIdParameter)));
			}
			IRecipientSession recipientSession = (IRecipientSession)base.DataSession;
			bool useGlobalCatalog = recipientSession.UseGlobalCatalog;
			recipientSession.UseGlobalCatalog = true;
			try
			{
				if (this.Role == DelegateRoleType.OrgAdmin)
				{
					adgroup = ((IDirectorySession)base.DataSession).ResolveWellKnownGuid<ADGroup>(WellKnownGuid.EoaWkGuid, this.ConfigurationSession.ConfigurationNamingContext.ToDNString());
				}
				else if (this.Role == DelegateRoleType.RecipientAdmin)
				{
					adgroup = ((IDirectorySession)base.DataSession).ResolveWellKnownGuid<ADGroup>(WellKnownGuid.EmaWkGuid, this.ConfigurationSession.ConfigurationNamingContext.ToDNString());
				}
				else if (this.Role == DelegateRoleType.PublicFolderAdmin)
				{
					adgroup = ((IDirectorySession)base.DataSession).ResolveWellKnownGuid<ADGroup>(WellKnownGuid.EpaWkGuid, this.ConfigurationSession.ConfigurationNamingContext.ToDNString());
				}
				else if (this.Role == DelegateRoleType.ViewOnlyAdmin || this.Role == DelegateRoleType.ServerAdmin)
				{
					adgroup = ((IDirectorySession)base.DataSession).ResolveWellKnownGuid<ADGroup>(WellKnownGuid.EraWkGuid, this.ConfigurationSession.ConfigurationNamingContext.ToDNString());
				}
			}
			finally
			{
				recipientSession.UseGlobalCatalog = useGlobalCatalog;
			}
			if (adgroup == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorExchangeAdministratorsGroupNotFound(this.Role.ToString(), this.Identity.ToString())), ErrorCategory.InvalidData, this.Role);
			}
			return adgroup;
		}

		// Token: 0x0400021B RID: 539
		private bool add = true;

		// Token: 0x0400021C RID: 540
		protected ADRecipient user;

		// Token: 0x0400021D RID: 541
		protected Server server;

		// Token: 0x0400021E RID: 542
		internal IConfigurationSession configSession;
	}
}
