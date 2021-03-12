using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.Common.Search;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200044A RID: 1098
	[Cmdlet("New", "RetentionPolicy", SupportsShouldProcess = true)]
	public sealed class NewRetentionPolicy : NewMailboxPolicyBase<RetentionPolicy>
	{
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x00099947 File Offset: 0x00097B47
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x00099959 File Offset: 0x00097B59
		// (set) Token: 0x060026CD RID: 9933 RVA: 0x00099970 File Offset: 0x00097B70
		[Parameter(Mandatory = false)]
		public Guid RetentionId
		{
			get
			{
				return (Guid)base.Fields["RetentionId"];
			}
			set
			{
				base.Fields["RetentionId"] = value;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x00099988 File Offset: 0x00097B88
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewRetentionPolicy(base.Name.ToString());
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x0009999A File Offset: 0x00097B9A
		// (set) Token: 0x060026D0 RID: 9936 RVA: 0x000999B1 File Offset: 0x00097BB1
		[Parameter(Mandatory = false)]
		public RetentionPolicyTagIdParameter[] RetentionPolicyTagLinks
		{
			get
			{
				return (RetentionPolicyTagIdParameter[])base.Fields["PolicyTagLinks"];
			}
			set
			{
				base.Fields["PolicyTagLinks"] = value;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000999C4 File Offset: 0x00097BC4
		// (set) Token: 0x060026D2 RID: 9938 RVA: 0x000999CC File Offset: 0x00097BCC
		[Parameter(Mandatory = false)]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x000999D5 File Offset: 0x00097BD5
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x000999FB File Offset: 0x00097BFB
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefault
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefault"] ?? false);
			}
			set
			{
				base.Fields["IsDefault"] = value;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x00099A13 File Offset: 0x00097C13
		// (set) Token: 0x060026D6 RID: 9942 RVA: 0x00099A39 File Offset: 0x00097C39
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefaultArbitrationMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefaultArbitrationMailbox"] ?? false);
			}
			set
			{
				base.Fields["IsDefaultArbitrationMailbox"] = value;
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x00099AA8 File Offset: 0x00097CA8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (Datacenter.IsMicrosoftHostedOnly(false))
			{
				List<RetentionPolicy> allRetentionPolicies = AdPolicyReader.GetAllRetentionPolicies(this.ConfigurationSession, base.OrganizationId);
				if (allRetentionPolicies.Count >= 100)
				{
					base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorTenantRetentionPolicyLimitReached(100)), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			if (this.IsDefault && this.IsDefaultArbitrationMailbox)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMultipleDefaultRetentionPolicy), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			if (!this.IgnoreDehydratedFlag && SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
			{
				base.WriteError(new ArgumentException(Strings.ErrorWriteOpOnDehydratedTenant), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			if (this.IsDefault && this.IsDefaultArbitrationMailbox)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMultipleDefaultRetentionPolicy), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			if (this.IsDefault)
			{
				this.DataObject.IsDefault = true;
				this.existingDefaultPolicies = RetentionPolicyUtility.GetDefaultPolicies((IConfigurationSession)base.DataSession, false);
			}
			else if (this.IsDefaultArbitrationMailbox)
			{
				this.DataObject.IsDefaultArbitrationMailbox = true;
				this.existingDefaultPolicies = RetentionPolicyUtility.GetDefaultPolicies((IConfigurationSession)base.DataSession, true);
			}
			if (this.existingDefaultPolicies != null && this.existingDefaultPolicies.Count > 0)
			{
				this.updateExistingDefaultPolicies = true;
			}
			if (this.RetentionPolicyTagLinks != null)
			{
				this.DataObject.RetentionPolicyTagLinks.Clear();
				PresentationRetentionPolicyTag[] array = (from x in (from x in this.RetentionPolicyTagLinks
				select (RetentionPolicyTag)base.GetDataObject<RetentionPolicyTag>(x, base.DataSession, null, new LocalizedString?(Strings.ErrorRetentionTagNotFound(x.ToString())), new LocalizedString?(Strings.ErrorAmbiguousRetentionPolicyTagId(x.ToString())))).Distinct(new ADObjectComparer<RetentionPolicyTag>())
				select new PresentationRetentionPolicyTag(x)).ToArray<PresentationRetentionPolicyTag>();
				RetentionPolicyValidator.ValicateDefaultTags(this.DataObject, array, new Task.TaskErrorLoggingDelegate(base.WriteError));
				RetentionPolicyValidator.ValidateSystemFolderTags(this.DataObject, array, new Task.TaskErrorLoggingDelegate(base.WriteError));
				array.ForEach(delegate(PresentationRetentionPolicyTag x)
				{
					this.DataObject.RetentionPolicyTagLinks.Add(x.Id);
				});
			}
			if (base.Fields.Contains("RetentionId"))
			{
				this.DataObject.RetentionId = this.RetentionId;
				string policyName;
				if (!(base.DataSession as IConfigurationSession).CheckForRetentionPolicyWithConflictingRetentionId(this.DataObject.RetentionId, out policyName))
				{
					base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorRetentionIdConflictsWithRetentionPolicy(this.DataObject.RetentionId.ToString(), policyName)), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00099D68 File Offset: 0x00097F68
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.DataObject != null && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.updateExistingDefaultPolicies)
			{
				if (!base.ShouldContinue(Strings.ConfirmationMessageSwitchMailboxPolicy("RetentionPolicy", this.DataObject.Name)))
				{
					return;
				}
				try
				{
					RetentionPolicyUtility.ClearDefaultPolicies(base.DataSession as IConfigurationSession, this.existingDefaultPolicies, this.IsDefaultArbitrationMailbox);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x04001D8B RID: 7563
		private const int TenantPolicyLimit = 100;
	}
}
