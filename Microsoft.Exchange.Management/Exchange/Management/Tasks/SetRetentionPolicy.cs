using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.Search;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200044D RID: 1101
	[Cmdlet("set", "RetentionPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRetentionPolicy : SetMailboxPolicyBase<RetentionPolicy>
	{
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x0009A089 File Offset: 0x00098289
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRetentionPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x0009A09B File Offset: 0x0009829B
		// (set) Token: 0x060026EC RID: 9964 RVA: 0x0009A0A3 File Offset: 0x000982A3
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x0009A0AC File Offset: 0x000982AC
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

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x0009A0BE File Offset: 0x000982BE
		// (set) Token: 0x060026EF RID: 9967 RVA: 0x0009A0E4 File Offset: 0x000982E4
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

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x0009A0FC File Offset: 0x000982FC
		// (set) Token: 0x060026F1 RID: 9969 RVA: 0x0009A122 File Offset: 0x00098322
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

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x0009A13A File Offset: 0x0009833A
		// (set) Token: 0x060026F3 RID: 9971 RVA: 0x0009A151 File Offset: 0x00098351
		[Parameter(Mandatory = false)]
		public RetentionPolicyTagIdParameter[] RetentionPolicyTagLinks
		{
			get
			{
				return (RetentionPolicyTagIdParameter[])base.Fields["RetentionPolicyLinks"];
			}
			set
			{
				base.Fields["RetentionPolicyLinks"] = value;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060026F4 RID: 9972 RVA: 0x0009A164 File Offset: 0x00098364
		// (set) Token: 0x060026F5 RID: 9973 RVA: 0x0009A18A File Offset: 0x0009838A
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0009A1A2 File Offset: 0x000983A2
		protected override IConfigurable ResolveDataObject()
		{
			if (!this.IgnoreDehydratedFlag)
			{
				SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			return base.ResolveDataObject();
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0009A224 File Offset: 0x00098424
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.IsDefault && this.IsDefaultArbitrationMailbox)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMultipleDefaultRetentionPolicy), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			if (this.IsDefault)
			{
				this.DataObject.IsDefaultArbitrationMailbox = false;
				this.DataObject.IsDefault = true;
				this.otherDefaultPolicies = RetentionPolicyUtility.GetDefaultPolicies((IConfigurationSession)base.DataSession, false);
			}
			else if (this.IsDefaultArbitrationMailbox)
			{
				this.DataObject.IsDefault = false;
				this.DataObject.IsDefaultArbitrationMailbox = true;
				this.otherDefaultPolicies = RetentionPolicyUtility.GetDefaultPolicies((IConfigurationSession)base.DataSession, true);
			}
			else if ((!this.IsDefault && base.Fields.IsChanged("IsDefault") && this.DataObject.IsDefault) || (!this.IsDefaultArbitrationMailbox && base.Fields.IsChanged("IsDefaultArbitrationMailbox") && this.DataObject.IsDefaultArbitrationMailbox))
			{
				base.WriteError(new InvalidOperationException(Strings.ResettingIsDefaultIsNotSupported(this.DataObject.IsDefault ? "IsDefault" : "IsDefaultArbitrationMailbox", "RetentionPolicy")), ErrorCategory.WriteError, this.DataObject);
			}
			if (this.otherDefaultPolicies != null && this.otherDefaultPolicies.Count > 0)
			{
				this.updateOtherDefaultPolicies = true;
			}
			if (base.Fields.IsModified("RetentionPolicyLinks"))
			{
				this.DataObject.RetentionPolicyTagLinks.Clear();
				if (this.RetentionPolicyTagLinks != null)
				{
					PresentationRetentionPolicyTag[] array = (from x in (from x in this.RetentionPolicyTagLinks
					select (RetentionPolicyTag)base.GetDataObject<RetentionPolicyTag>(x, base.DataSession, null, new LocalizedString?(Strings.ErrorRetentionTagNotFound(x.ToString())), new LocalizedString?(Strings.ErrorAmbiguousRetentionPolicyTagId(x.ToString())))).Distinct<RetentionPolicyTag>()
					select new PresentationRetentionPolicyTag(x)).ToArray<PresentationRetentionPolicyTag>();
					RetentionPolicyValidator.ValicateDefaultTags(this.DataObject, array, new Task.TaskErrorLoggingDelegate(base.WriteError));
					RetentionPolicyValidator.ValidateSystemFolderTags(this.DataObject, array, new Task.TaskErrorLoggingDelegate(base.WriteError));
					array.ForEach(delegate(PresentationRetentionPolicyTag x)
					{
						this.DataObject.RetentionPolicyTagLinks.Add(x.Id);
					});
				}
			}
			string policyName;
			if (this.DataObject.IsChanged(RetentionPolicySchema.RetentionId) && !(base.DataSession as IConfigurationSession).CheckForRetentionPolicyWithConflictingRetentionId(this.DataObject.RetentionId, this.DataObject.Identity.ToString(), out policyName))
			{
				base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorRetentionIdConflictsWithRetentionPolicy(this.DataObject.RetentionId.ToString(), policyName)), ErrorCategory.InvalidOperation, this.DataObject);
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0009A4EC File Offset: 0x000986EC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.DataObject != null && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.DataObject.IsChanged(RetentionPolicySchema.RetentionId) && !this.Force && !base.ShouldContinue(Strings.WarningRetentionPolicyIdChange(this.DataObject.Identity.ToString())))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.updateOtherDefaultPolicies)
			{
				if (!base.ShouldContinue(Strings.ConfirmationMessageSwitchMailboxPolicy("RetentionPolicy", this.Identity.ToString())))
				{
					return;
				}
				try
				{
					RetentionPolicyUtility.ClearDefaultPolicies(base.DataSession as IConfigurationSession, this.otherDefaultPolicies, this.IsDefaultArbitrationMailbox);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
