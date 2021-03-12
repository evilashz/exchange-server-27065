using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000684 RID: 1668
	[Cmdlet("Set", "ManagementRoleEntry", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetManagementRoleEntry : ManagementRoleEntryActionBase
	{
		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x000FAEED File Offset: 0x000F90ED
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetManagementRoleEntry(this.modifiedEntry.ToString(), this.DataObject.Id.ToString());
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x000FAF0F File Offset: 0x000F910F
		// (set) Token: 0x06003B06 RID: 15110 RVA: 0x000FAF35 File Offset: 0x000F9135
		[Parameter(Mandatory = false)]
		public SwitchParameter UnScopedTopLevel
		{
			get
			{
				return (SwitchParameter)(base.Fields["UnScopedTopLevel"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UnScopedTopLevel"] = value;
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000FAF4D File Offset: 0x000F914D
		// (set) Token: 0x06003B08 RID: 15112 RVA: 0x000FAF64 File Offset: 0x000F9164
		[Parameter(Mandatory = false)]
		public string[] Parameters
		{
			get
			{
				return (string[])base.Fields[RbacCommonParameters.ParameterParameters];
			}
			set
			{
				RoleEntry.FormatParameters(value);
				base.Fields[RbacCommonParameters.ParameterParameters] = value;
			}
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000FAF7D File Offset: 0x000F917D
		// (set) Token: 0x06003B0A RID: 15114 RVA: 0x000FAFA3 File Offset: 0x000F91A3
		[Parameter(Mandatory = false)]
		public SwitchParameter AddParameter
		{
			get
			{
				return (SwitchParameter)(base.Fields["AddParameter"] ?? false);
			}
			set
			{
				base.Fields["AddParameter"] = value;
			}
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000FAFBB File Offset: 0x000F91BB
		// (set) Token: 0x06003B0C RID: 15116 RVA: 0x000FAFE1 File Offset: 0x000F91E1
		[Parameter(Mandatory = false)]
		public SwitchParameter RemoveParameter
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveParameter"] ?? false);
			}
			set
			{
				base.Fields["RemoveParameter"] = value;
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000FAFF9 File Offset: 0x000F91F9
		// (set) Token: 0x06003B0E RID: 15118 RVA: 0x000FB01F File Offset: 0x000F921F
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter SkipScriptExistenceCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipScriptExistenceCheck"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SkipScriptExistenceCheck"] = value;
			}
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000FB038 File Offset: 0x000F9238
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (!base.Fields.IsModified(RbacCommonParameters.ParameterParameters))
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorParametersMandatory), ErrorCategory.InvalidArgument, null);
			}
			if (this.AddParameter.IsPresent && this.RemoveParameter.IsPresent)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorCannotTurnOnBothAndAndRemove), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x000FB0B8 File Offset: 0x000F92B8
		protected override void InternalApplyChangeAndValidate()
		{
			TaskLogger.LogEnter();
			if (!this.DataObject.IsUnscopedTopLevel && this.UnScopedTopLevel)
			{
				base.WriteError(new InvalidOperationException(Strings.ParameterAllowedOnlyForTopLevelRoleManipulation("UnScopedTopLevel", RoleType.UnScoped.ToString())), ErrorCategory.InvalidOperation, null);
			}
			string[] parameters = null;
			this.originalEntry = RoleHelper.GetMandatoryRoleEntry(this.DataObject, this.Identity.CmdletOrScriptName, this.Identity.PSSnapinName, new Task.TaskErrorLoggingDelegate(base.WriteError));
			string[] parameters2;
			if (!this.AddParameter.IsPresent && !this.RemoveParameter.IsPresent)
			{
				parameters2 = this.Parameters;
				parameters = this.Parameters;
			}
			else
			{
				MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
				foreach (string item in this.originalEntry.Parameters)
				{
					multiValuedProperty.Add(item);
				}
				if (this.Parameters != null)
				{
					List<string> list = null;
					if (this.AddParameter.IsPresent)
					{
						parameters = this.Parameters;
						foreach (string item2 in this.Parameters)
						{
							if (multiValuedProperty.Contains(item2))
							{
								if (list == null)
								{
									list = new List<string>();
								}
								list.Add(item2);
							}
							else
							{
								multiValuedProperty.Add(item2);
							}
						}
					}
					else
					{
						foreach (string item3 in this.Parameters)
						{
							if (!multiValuedProperty.Contains(item3))
							{
								if (list == null)
								{
									list = new List<string>();
								}
								list.Add(item3);
							}
							else
							{
								multiValuedProperty.Remove(item3);
							}
						}
					}
					if (list != null && 0 < list.Count)
					{
						LocalizedString value = this.AddParameter.IsPresent ? Strings.ErrorAddExistentParameters(this.Identity.RoleId.ToString(), this.Identity.CmdletOrScriptName, string.Join(",", list.ToArray())) : Strings.ErrorRemoveNonExistentParameters(this.Identity.RoleId.ToString(), this.Identity.CmdletOrScriptName, string.Join(",", list.ToArray()));
						base.WriteError(new InvalidOperationException(value), ErrorCategory.InvalidOperation, this.DataObject.Id);
					}
				}
				parameters2 = multiValuedProperty.ToArray();
			}
			try
			{
				if (this.originalEntry is CmdletRoleEntry)
				{
					string pssnapinName = ((CmdletRoleEntry)this.originalEntry).PSSnapinName;
					if (base.IsEntryValidationRequired())
					{
						base.VerifyCmdletEntry(this.originalEntry.Name, pssnapinName, parameters);
					}
					this.modifiedEntry = new CmdletRoleEntry(this.originalEntry.Name, pssnapinName, parameters2);
				}
				else if (this.originalEntry is ScriptRoleEntry)
				{
					if (base.IsEntryValidationRequired())
					{
						base.VerifyScriptEntry(this.originalEntry.Name, parameters, this.SkipScriptExistenceCheck);
					}
					this.modifiedEntry = new ScriptRoleEntry(this.originalEntry.Name, parameters2);
				}
				else if (this.originalEntry is ApplicationPermissionRoleEntry)
				{
					this.modifiedEntry = new ApplicationPermissionRoleEntry(this.originalEntry.Name, parameters2);
				}
				else
				{
					if (!(this.originalEntry is WebServiceRoleEntry))
					{
						throw new NotSupportedException("this.originalEntry is not a valid type.");
					}
					base.WriteError(new InvalidOperationException(Strings.CannotSetWebServiceRoleEntry(this.originalEntry.Name)), ErrorCategory.InvalidOperation, null);
				}
			}
			catch (FormatException ex)
			{
				base.WriteError(new ArgumentException(new LocalizedString(ex.Message)), ErrorCategory.InvalidArgument, this.DataObject.Id);
			}
			if (!this.modifiedEntry.Equals(this.originalEntry))
			{
				if (null != this.originalEntry)
				{
					this.DataObject.RoleEntries.Remove(this.originalEntry);
				}
				this.DataObject.RoleEntries.Add(this.modifiedEntry);
				this.DataObject.ApplyChangesToDownlevelData(base.ParentRole ?? this.DataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x000FB500 File Offset: 0x000F9700
		protected override bool IsTopLevelUnscopedRoleModificationAllowed()
		{
			return this.UnScopedTopLevel;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x000FB510 File Offset: 0x000F9710
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!base.Force && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x040026B5 RID: 9909
		private const string ParameterAddParameter = "AddParameter";

		// Token: 0x040026B6 RID: 9910
		private const string ParameterRemoveParameter = "RemoveParameter";

		// Token: 0x040026B7 RID: 9911
		private RoleEntry modifiedEntry;

		// Token: 0x040026B8 RID: 9912
		private RoleEntry originalEntry;
	}
}
