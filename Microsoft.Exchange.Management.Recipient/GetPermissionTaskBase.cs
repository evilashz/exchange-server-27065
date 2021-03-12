using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000A0 RID: 160
	public abstract class GetPermissionTaskBase<TIdentity, TDataObject> : GetObjectWithIdentityTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002CEE9 File Offset: 0x0002B0E9
		internal IRecipientSession ReadOnlyRecipientSession
		{
			get
			{
				return this.readOnlyRecipientSession;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002CEF1 File Offset: 0x0002B0F1
		internal IRecipientSession GlobalCatalogRecipientSession
		{
			get
			{
				return this.globalCatalogRecipientSession;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002CEF9 File Offset: 0x0002B0F9
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0002CF01 File Offset: 0x0002B101
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002CF0A File Offset: 0x0002B10A
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0002CF21 File Offset: 0x0002B121
		[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
		public override TIdentity Identity
		{
			get
			{
				return (TIdentity)((object)base.Fields["Identity"]);
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002CF39 File Offset: 0x0002B139
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002CF50 File Offset: 0x0002B150
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		public SecurityPrincipalIdParameter User
		{
			get
			{
				return (SecurityPrincipalIdParameter)base.Fields["User"];
			}
			set
			{
				base.Fields["User"] = value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002CF63 File Offset: 0x0002B163
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0002CF7A File Offset: 0x0002B17A
		[Parameter(Mandatory = false, ParameterSetName = "Owner")]
		public SwitchParameter Owner
		{
			get
			{
				return (SwitchParameter)base.Fields["Owner"];
			}
			set
			{
				base.Fields["Owner"] = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002CF92 File Offset: 0x0002B192
		protected SecurityIdentifier SecurityPrincipal
		{
			get
			{
				return this.securityPrincipal;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002CF9A File Offset: 0x0002B19A
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0002CFA2 File Offset: 0x0002B1A2
		protected bool HasObjectMatchingIdentity
		{
			get
			{
				return this.hasObjectMatchingIdentity;
			}
			set
			{
				this.hasObjectMatchingIdentity = value;
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002CFAB File Offset: 0x0002B1AB
		public GetPermissionTaskBase()
		{
			base.Fields["Owner"] = new SwitchParameter(false);
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002CFDC File Offset: 0x0002B1DC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.readOnlyRecipientSession = PermissionTaskHelper.GetReadOnlyRecipientSession(this.DomainController);
			if (this.readOnlyRecipientSession.UseGlobalCatalog)
			{
				this.globalCatalogRecipientSession = this.readOnlyRecipientSession;
			}
			else
			{
				this.globalCatalogRecipientSession = PermissionTaskHelper.GetReadOnlyRecipientSession(null);
			}
			if (this.User != null)
			{
				this.securityPrincipal = SecurityPrincipalIdParameter.GetUserSid(this.GlobalCatalogRecipientSession, this.User, new Task.TaskErrorLoggingDelegate(base.ThrowTerminatingError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002D068 File Offset: 0x0002B268
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.HasObjectMatchingIdentity = false;
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002D084 File Offset: 0x0002B284
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (this.Identity != null)
			{
				LocalizedString? localizedString;
				IEnumerable<TDataObject> dataObjects = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString);
				this.WriteResult<TDataObject>(dataObjects);
				if (!base.HasErrors && !this.HasObjectMatchingIdentity)
				{
					LocalizedString? localizedString2 = localizedString;
					LocalizedString message;
					if (localizedString2 == null)
					{
						TIdentity identity = this.Identity;
						message = base.GetErrorMessageObjectNotFound(identity.ToString(), typeof(TDataObject).ToString(), (base.DataSession != null) ? base.DataSession.Source : null);
					}
					else
					{
						message = localizedString2.GetValueOrDefault();
					}
					base.WriteError(new ManagementObjectNotFoundException(message), ErrorCategory.InvalidData, null);
				}
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000233 RID: 563
		private bool hasObjectMatchingIdentity;

		// Token: 0x04000234 RID: 564
		private SecurityIdentifier securityPrincipal;

		// Token: 0x04000235 RID: 565
		private IRecipientSession readOnlyRecipientSession;

		// Token: 0x04000236 RID: 566
		private IRecipientSession globalCatalogRecipientSession;
	}
}
