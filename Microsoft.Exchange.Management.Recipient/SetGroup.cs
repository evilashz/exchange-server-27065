using System;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000031 RID: 49
	[Cmdlet("Set", "Group", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetGroup : SetRecipientObjectTask<GroupIdParameter, WindowsGroup, ADGroup>
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000C495 File Offset: 0x0000A695
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetGroup(this.Identity.ToString());
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000C4A7 File Offset: 0x0000A6A7
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000C4BE File Offset: 0x0000A6BE
		[Parameter(Mandatory = true, ParameterSetName = "Universal", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override GroupIdParameter Identity
		{
			get
			{
				return (GroupIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000C4D1 File Offset: 0x0000A6D1
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		[Parameter(Mandatory = false)]
		public GeneralRecipientIdParameter[] ManagedBy
		{
			get
			{
				return (GeneralRecipientIdParameter[])base.Fields[WindowsGroupSchema.ManagedBy];
			}
			set
			{
				base.Fields[WindowsGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000C4FB File Offset: 0x0000A6FB
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000C512 File Offset: 0x0000A712
		[Parameter(Mandatory = false, ParameterSetName = "Universal")]
		public SwitchParameter Universal
		{
			get
			{
				return (SwitchParameter)base.Fields["Universal"];
			}
			set
			{
				base.Fields["Universal"] = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000C52A File Offset: 0x0000A72A
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000C550 File Offset: 0x0000A750
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassSecurityGroupManagerCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassSecurityGroupManagerCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassSecurityGroupManagerCheck"] = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000C568 File Offset: 0x0000A768
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000C56B File Offset: 0x0000A76B
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return adObject.IsChanged(ADGroupSchema.IsOrganizationalGroup) || base.Fields.IsModified(WindowsGroupSchema.IsHierarchicalGroup);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000C58C File Offset: 0x0000A78C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.Fields.IsModified(WindowsGroupSchema.ManagedBy) && (this.ManagedBy == null || this.ManagedBy.Length == 0))
			{
				base.WriteError(new RecipientTaskException(Strings.AutoGroupManagedByCannotBeEmpty), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			if (base.Fields.IsModified(WindowsGroupSchema.ManagedBy))
			{
				this.managedByRecipients = new MultiValuedProperty<ADRecipient>();
				if (this.ManagedBy != null)
				{
					foreach (GeneralRecipientIdParameter recipientIdParameter in this.ManagedBy)
					{
						ADRecipient item = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())));
						this.managedByRecipients.Add(item);
					}
				}
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000C674 File Offset: 0x0000A874
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADGroup adgroup = (ADGroup)base.PrepareDataObject();
			if (base.Fields.IsModified(DistributionGroupSchema.ManagedBy))
			{
				if (!this.BypassSecurityGroupManagerCheck && GroupTypeFlags.SecurityEnabled == (adgroup.GroupType & GroupTypeFlags.SecurityEnabled))
				{
					ADObjectId userId;
					if (!base.TryGetExecutingUserId(out userId))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorExecutingUserOutOfTargetOrg(base.MyInvocation.MyCommand.Name)), ExchangeErrorCategory.Client, adgroup.Identity.ToString());
					}
					RecipientTaskHelper.ValidateUserIsGroupManager(userId, adgroup, new Task.ErrorLoggerDelegate(base.WriteError), false, null);
				}
				MailboxTaskHelper.StampOnManagedBy(adgroup, this.managedByRecipients, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
			return adgroup;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000C734 File Offset: 0x0000A934
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			MailboxTaskHelper.ValidateGroupManagedBy(base.TenantGlobalCatalogSession, this.DataObject, this.managedByRecipients, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError));
			if (this.DataObject.IsModified(ADMailboxRecipientSchema.SamAccountName))
			{
				RecipientTaskHelper.IsSamAccountNameUnique(this.DataObject, this.DataObject.SamAccountName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if (base.ParameterSetName == "Universal")
			{
				if ((this.DataObject.GroupType & GroupTypeFlags.Universal) == GroupTypeFlags.Universal)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorIsUniversalGroupAlready(this.DataObject.Name)), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				}
				else
				{
					if ((this.DataObject.GroupType & GroupTypeFlags.BuiltinLocal) == GroupTypeFlags.BuiltinLocal || SetGroup.IsBuiltInObject(this.DataObject))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorCannotConvertBuiltInGroup(this.DataObject.Name)), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					}
					GroupTypeFlags groupTypeFlags = (GroupTypeFlags)7;
					this.DataObject.GroupType = ((this.DataObject.GroupType & ~groupTypeFlags) | GroupTypeFlags.Universal);
					base.DesiredRecipientType = this.DataObject.RecipientType;
				}
			}
			if (this.DataObject.IsChanged(ADGroupSchema.Members) || base.ParameterSetName == "Universal")
			{
				MailboxTaskHelper.ValidateAddedMembers(base.TenantGlobalCatalogSession, this.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		private static bool IsBuiltInObject(IADSecurityPrincipal securityPrincipal)
		{
			bool result = false;
			if (securityPrincipal.IsSecurityPrincipal)
			{
				foreach (object obj in Enum.GetValues(typeof(WellKnownSidType)))
				{
					WellKnownSidType type = (WellKnownSidType)obj;
					if (securityPrincipal.Sid.IsWellKnown(type))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000C94C File Offset: 0x0000AB4C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return WindowsGroup.FromDataObject((ADGroup)dataObject);
		}

		// Token: 0x04000051 RID: 81
		private const string ParameterSetUniversal = "Universal";

		// Token: 0x04000052 RID: 82
		private const string ParameterUniversal = "Universal";

		// Token: 0x04000053 RID: 83
		private MultiValuedProperty<ADRecipient> managedByRecipients;
	}
}
