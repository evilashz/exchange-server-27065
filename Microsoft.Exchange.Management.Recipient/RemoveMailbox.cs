using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.RbacTasks;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000073 RID: 115
	[Cmdlet("Remove", "Mailbox", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMailbox : RemoveMailboxBase<MailboxIdParameter>
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x000259A9 File Offset: 0x00023BA9
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x000259B1 File Offset: 0x00023BB1
		[Parameter(Mandatory = false)]
		public new SwitchParameter ForReconciliation
		{
			get
			{
				return base.ForReconciliation;
			}
			set
			{
				base.ForReconciliation = value;
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000259BC File Offset: 0x00023BBC
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeArbitrationMailbox(adrecipient, base.Arbitration) || MailboxTaskHelper.ExcludePublicFolderMailbox(adrecipient, base.PublicFolder) || MailboxTaskHelper.ExcludeGroupMailbox(adrecipient, false) || MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, false) || MailboxTaskHelper.ExcludeAuditLogMailbox(adrecipient, base.AuditLog))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ExchangeErrorCategory.Client, this.Identity);
			}
			if (adrecipient != null && adrecipient.RecipientTypeDetails == RecipientTypeDetails.TeamMailbox)
			{
				TeamMailbox teamMailbox = TeamMailbox.FromDataObject((ADUser)adrecipient);
				teamMailbox.ClosedTime = new DateTime?(DateTime.UtcNow);
				this.removeTeamMailboxFromResolverCache = true;
				if (teamMailbox.SharePointUrl != null && base.ExchangeRunspaceConfig != null)
				{
					TeamMailboxHelper teamMailboxHelper = new TeamMailboxHelper(teamMailbox, base.ExchangeRunspaceConfig.ExecutingUser, base.ExchangeRunspaceConfig.ExecutingUserOrganizationId, (IRecipientSession)base.DataSession, new TeamMailboxGetDataObject<ADUser>(base.GetDataObject<ADUser>));
					try
					{
						teamMailboxHelper.LinkSharePointSite(null, false, false);
					}
					catch (RecipientTaskException ex)
					{
						this.WriteWarning(Strings.ErrorTeamMailFailedUnlinkSharePointSite(this.Identity.ToString(), teamMailbox.SharePointUrl.ToString(), ex.Message));
					}
				}
			}
			return adrecipient;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00025B3C File Offset: 0x00023D3C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.orgAdminHelper = new RoleAssignmentsGlobalConstraints(this.ConfigurationSession, base.TenantGlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError));
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00025B70 File Offset: 0x00023D70
		protected override void InternalValidate()
		{
			this.latencyContext = ProvisioningPerformanceHelper.StartLatencyDetection(this);
			base.InternalValidate();
			if (base.DataObject != null)
			{
				RemoveMailbox.CheckManagedGroups(base.DataObject, base.TenantGlobalCatalogSession, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				if (this.orgAdminHelper.ShouldPreventLastAdminRemoval(this, base.DataObject.OrganizationId) && this.orgAdminHelper.IsLastAdmin(base.DataObject))
				{
					base.WriteError(new CannotRemoveLastOrgAdminException(Strings.ErrorCannotRemoveLastOrgAdmin(base.DataObject.Identity.ToString())), ExchangeErrorCategory.Client, base.DataObject.Identity);
				}
				RemoveMailbox.CheckModeratedMailboxes(base.DataObject, base.TenantGlobalCatalogSession, new Task.ErrorLoggerDelegate(base.WriteError));
				if (base.DataObject.CatchAllRecipientBL.Count > 0)
				{
					string domain = string.Join(", ", (from r in base.DataObject.CatchAllRecipientBL
					select r.Name).ToArray<string>());
					base.WriteError(new CannotRemoveMailboxCatchAllRecipientException(domain), ExchangeErrorCategory.Client, base.DataObject.Identity);
				}
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00025CA0 File Offset: 0x00023EA0
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
				if (this.removeTeamMailboxFromResolverCache)
				{
					TeamMailboxADUserResolver.RemoveIdIfExists(base.DataObject.Id);
				}
			}
			finally
			{
				ProvisioningPerformanceHelper.StopLatencyDetection(this.latencyContext);
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00025CEC File Offset: 0x00023EEC
		protected override bool ShouldSoftDeleteObject()
		{
			ADRecipient dataObject = base.DataObject;
			return dataObject != null && !(dataObject.OrganizationId == null) && dataObject.OrganizationId.ConfigurationUnit != null && !base.Disconnect && !base.Permanent && Globals.IsMicrosoftHostedOnly && SoftDeletedTaskHelper.IsSoftDeleteSupportedRecipientTypeDetail(dataObject.RecipientTypeDetails);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00025D4C File Offset: 0x00023F4C
		internal static void CheckManagedGroups(ADUser user, IConfigDataProvider session, Task.TaskWarningLoggingDelegate writeWarning)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.MemberJoinRestriction, MemberUpdateType.ApprovalRequired),
				new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.ManagedBy, user.Id)
			});
			IEnumerable<ADGroup> enumerable = session.FindPaged<ADGroup>(filter, null, true, null, 1);
			using (IEnumerator<ADGroup> enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					writeWarning(Strings.WarningRemoveApprovalRequiredGroupOwners(user.Id.ToString()));
				}
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00025DDC File Offset: 0x00023FDC
		internal static void CheckModeratedMailboxes(ADUser user, IConfigDataProvider session, Task.ErrorLoggerDelegate writeError)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ModerationEnabled, true),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ModeratedBy, user.Id)
			});
			IEnumerable<ADUser> enumerable = session.FindPaged<ADUser>(filter, null, true, null, 1);
			using (IEnumerator<ADUser> enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					writeError(new RecipientTaskException(Strings.ErrorRemoveModeratorMailbox(user.Name)), ExchangeErrorCategory.Client, user);
				}
			}
		}

		// Token: 0x040001E4 RID: 484
		private LatencyDetectionContext latencyContext;

		// Token: 0x040001E5 RID: 485
		private RoleAssignmentsGlobalConstraints orgAdminHelper;

		// Token: 0x040001E6 RID: 486
		private bool removeTeamMailboxFromResolverCache;
	}
}
