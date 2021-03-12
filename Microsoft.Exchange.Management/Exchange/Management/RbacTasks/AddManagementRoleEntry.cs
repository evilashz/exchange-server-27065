using System;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000681 RID: 1665
	[Cmdlet("Add", "ManagementRoleEntry", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class AddManagementRoleEntry : AddRemoveManagementRoleEntryActionBase
	{
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000FA3A2 File Offset: 0x000F85A2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewManagementRoleEntry(this.addedEntry.ToString(), this.DataObject.Id.ToString());
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000FA3C4 File Offset: 0x000F85C4
		// (set) Token: 0x06003AD6 RID: 15062 RVA: 0x000FA3EA File Offset: 0x000F85EA
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
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

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x000FA402 File Offset: 0x000F8602
		// (set) Token: 0x06003AD8 RID: 15064 RVA: 0x000FA428 File Offset: 0x000F8628
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

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x000FA440 File Offset: 0x000F8640
		// (set) Token: 0x06003ADA RID: 15066 RVA: 0x000FA457 File Offset: 0x000F8657
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "ParentRoleEntry")]
		public RoleEntryIdParameter ParentRoleEntry
		{
			get
			{
				return (RoleEntryIdParameter)base.Fields["ParentRoleEntry"];
			}
			set
			{
				base.Fields["ParentRoleEntry"] = value;
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x000FA46A File Offset: 0x000F866A
		// (set) Token: 0x06003ADC RID: 15068 RVA: 0x000FA481 File Offset: 0x000F8681
		[Parameter(Mandatory = true, ParameterSetName = "ParentRoleEntry")]
		public RoleIdParameter Role
		{
			get
			{
				return (RoleIdParameter)base.Fields[RbacCommonParameters.ParameterRole];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterRole] = value;
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x000FA494 File Offset: 0x000F8694
		// (set) Token: 0x06003ADE RID: 15070 RVA: 0x000FA4AB File Offset: 0x000F86AB
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
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

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x000FA4C4 File Offset: 0x000F86C4
		// (set) Token: 0x06003AE0 RID: 15072 RVA: 0x000FA4DB File Offset: 0x000F86DB
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string PSSnapinName
		{
			get
			{
				return (string)base.Fields[RbacCommonParameters.ParameterPSSnapinName];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterPSSnapinName] = value;
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x000FA4EE File Offset: 0x000F86EE
		// (set) Token: 0x06003AE2 RID: 15074 RVA: 0x000FA505 File Offset: 0x000F8705
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ManagementRoleEntryType Type
		{
			get
			{
				return (ManagementRoleEntryType)base.Fields[RbacCommonParameters.ParameterType];
			}
			set
			{
				base.VerifyValues<ManagementRoleEntryType>(AddManagementRoleEntry.AllowedRoleEntryTypes, value);
				base.Fields[RbacCommonParameters.ParameterType] = value;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x000FA529 File Offset: 0x000F8729
		// (set) Token: 0x06003AE4 RID: 15076 RVA: 0x000FA54F File Offset: 0x000F874F
		[Parameter]
		public SwitchParameter Overwrite
		{
			get
			{
				return (SwitchParameter)(base.Fields["Overwrite"] ?? false);
			}
			set
			{
				base.Fields["Overwrite"] = value;
			}
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x000FA567 File Offset: 0x000F8767
		protected override bool IsTopLevelUnscopedRoleModificationAllowed()
		{
			return this.UnScopedTopLevel;
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x000FA574 File Offset: 0x000F8774
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (base.ParameterSetName == "Identity")
			{
				this.Identity.Parameters = this.Parameters;
				this.Identity.PSSnapinName = this.PSSnapinName;
				if (base.Fields.IsModified(RbacCommonParameters.ParameterType))
				{
					this.Identity.Type = this.Type;
				}
			}
			else
			{
				ExchangeRoleEntryPresentation exchangeRoleEntryPresentation = (ExchangeRoleEntryPresentation)base.GetDataObject<ExchangeRoleEntryPresentation>(this.ParentRoleEntry, base.DataSession, null, new LocalizedString?(Strings.ErrorRoleEntryNotFound(this.ParentRoleEntry.RoleId.ToString(), this.ParentRoleEntry.CmdletOrScriptName)), new LocalizedString?(Strings.ErrorRoleEntryNotUnique(this.ParentRoleEntry.RoleId.ToString(), this.ParentRoleEntry.CmdletOrScriptName)));
				this.Identity = new RoleEntryIdParameter(this.Role, exchangeRoleEntryPresentation.Name, exchangeRoleEntryPresentation.Type);
				string[] array = new string[exchangeRoleEntryPresentation.Parameters.Count];
				exchangeRoleEntryPresentation.Parameters.CopyTo(array, 0);
				this.Parameters = array;
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x000FA6A8 File Offset: 0x000F88A8
		protected override void InternalApplyChangeAndValidate()
		{
			if (this.DataObject.IsUnscopedTopLevel)
			{
				if (this.ParentRoleEntry != null)
				{
					base.WriteError(new InvalidOperationException(Strings.ParameterNotAllowedWithTopLevelRole("ParentRoleEntry", RoleType.UnScoped.ToString())), ErrorCategory.InvalidArgument, this.DataObject.Id);
				}
				if (this.Role != null)
				{
					base.WriteError(new InvalidOperationException(Strings.ParameterNotAllowedWithTopLevelRole("Role", RoleType.UnScoped.ToString())), ErrorCategory.InvalidArgument, this.DataObject.Id);
				}
				ManagementRoleEntryType managementRoleEntryType = ManagementRoleEntryType.Cmdlet;
				if (base.Fields.Contains(RbacCommonParameters.ParameterType))
				{
					if (this.Type == ManagementRoleEntryType.ApplicationPermission || this.Type == ManagementRoleEntryType.All)
					{
						base.WriteError(new InvalidOperationException(Strings.EntryNoAllowedInRoleType(this.Type.ToString(), RoleType.UnScoped.ToString())), ErrorCategory.InvalidArgument, this.DataObject.Id);
					}
					managementRoleEntryType = this.Type;
				}
				else if (Regex.IsMatch(this.Identity.CmdletOrScriptName, "ps\\d?.$", RegexOptions.IgnoreCase))
				{
					managementRoleEntryType = ManagementRoleEntryType.Script;
				}
				if (managementRoleEntryType == ManagementRoleEntryType.Cmdlet && string.IsNullOrEmpty(this.PSSnapinName))
				{
					base.WriteError(new InvalidOperationException(string.Format(Strings.ProvideSnapinNameForCmdletEntryForRole(RoleType.UnScoped.ToString()), new object[0])), ErrorCategory.InvalidArgument, this.DataObject.Id);
				}
				ManagementRoleEntryType managementRoleEntryType2 = managementRoleEntryType;
				switch (managementRoleEntryType2)
				{
				case ManagementRoleEntryType.Cmdlet:
					base.VerifyCmdletEntry(this.Identity.CmdletOrScriptName, this.PSSnapinName, this.Parameters);
					this.addedEntry = new CmdletRoleEntry(this.Identity.CmdletOrScriptName, this.PSSnapinName, this.Parameters);
					break;
				case ManagementRoleEntryType.Script:
					base.VerifyScriptEntry(this.Identity.CmdletOrScriptName, this.Parameters, this.SkipScriptExistenceCheck);
					this.addedEntry = new ScriptRoleEntry(this.Identity.CmdletOrScriptName, this.Parameters);
					break;
				default:
					if (managementRoleEntryType2 == ManagementRoleEntryType.WebService)
					{
						this.VerifyWebServiceEntry(this.Identity.CmdletOrScriptName);
						this.addedEntry = new WebServiceRoleEntry(this.Identity.CmdletOrScriptName, new string[0]);
					}
					break;
				}
				this.roleEntryOnDataObject = RoleHelper.GetRoleEntry(this.DataObject, this.Identity.CmdletOrScriptName, this.Identity.PSSnapinName, managementRoleEntryType, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			else
			{
				if (this.UnScopedTopLevel)
				{
					base.WriteError(new InvalidOperationException(Strings.ParameterAllowedOnlyForTopLevelRoleManipulation("UnScopedTopLevel", RoleType.UnScoped.ToString())), ErrorCategory.InvalidOperation, null);
				}
				RoleEntryIdParameter roleEntryIdParameter = new RoleEntryIdParameter(base.ParentRole.Id, this.Identity.CmdletOrScriptName, this.Identity.Type);
				roleEntryIdParameter.PSSnapinName = this.PSSnapinName;
				ExchangeRoleEntryPresentation exchangeRoleEntryPresentation = (ExchangeRoleEntryPresentation)base.GetDataObject<ExchangeRoleEntryPresentation>(roleEntryIdParameter, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorRoleEntryNotFound(roleEntryIdParameter.RoleId.ToString(), roleEntryIdParameter.CmdletOrScriptName)), new LocalizedString?(Strings.ErrorRoleEntryNotUnique(roleEntryIdParameter.RoleId.ToString(), roleEntryIdParameter.CmdletOrScriptName)));
				string[] array;
				if (base.Fields.IsModified(RbacCommonParameters.ParameterParameters))
				{
					array = this.Parameters;
				}
				else
				{
					array = new string[exchangeRoleEntryPresentation.Parameters.Count];
					exchangeRoleEntryPresentation.Parameters.CopyTo(array, 0);
				}
				try
				{
					ManagementRoleEntryType type = exchangeRoleEntryPresentation.Type;
					switch (type)
					{
					case ManagementRoleEntryType.Cmdlet:
						if (base.IsEntryValidationRequired())
						{
							base.VerifyCmdletEntry(exchangeRoleEntryPresentation.Name, exchangeRoleEntryPresentation.PSSnapinName, array);
						}
						this.addedEntry = new CmdletRoleEntry(exchangeRoleEntryPresentation.Name, exchangeRoleEntryPresentation.PSSnapinName, array);
						break;
					case ManagementRoleEntryType.Script:
						if (base.IsEntryValidationRequired())
						{
							base.VerifyScriptEntry(exchangeRoleEntryPresentation.Name, array, false);
						}
						this.addedEntry = new ScriptRoleEntry(exchangeRoleEntryPresentation.Name, array);
						break;
					case ManagementRoleEntryType.Cmdlet | ManagementRoleEntryType.Script:
						break;
					case ManagementRoleEntryType.ApplicationPermission:
						this.addedEntry = new ApplicationPermissionRoleEntry(exchangeRoleEntryPresentation.Name, array);
						break;
					default:
						if (type == ManagementRoleEntryType.WebService)
						{
							this.VerifyWebServiceEntry(exchangeRoleEntryPresentation.Name);
							this.addedEntry = new WebServiceRoleEntry(exchangeRoleEntryPresentation.Name, new string[0]);
						}
						break;
					}
				}
				catch (FormatException ex)
				{
					base.WriteError(new ArgumentException(new LocalizedString(ex.Message)), ErrorCategory.InvalidArgument, this.DataObject.Id);
				}
				this.roleEntryOnDataObject = RoleHelper.GetRoleEntry(this.DataObject, this.Identity.CmdletOrScriptName, this.Identity.PSSnapinName, exchangeRoleEntryPresentation.Type, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (!this.Overwrite.IsPresent && null != this.roleEntryOnDataObject)
			{
				this.WriteWarning(Strings.WarningRoleEntryAlreadyExists(this.DataObject.Id.ToString(), this.Identity.CmdletOrScriptName));
			}
			this.InternalAddRemoveRoleEntry(this.DataObject.RoleEntries);
			TaskLogger.LogExit();
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x000FABA4 File Offset: 0x000F8DA4
		protected override void InternalAddRemoveRoleEntry(MultiValuedProperty<RoleEntry> roleEntries)
		{
			if (!this.addedEntry.Equals(this.roleEntryOnDataObject))
			{
				if (null != this.roleEntryOnDataObject)
				{
					roleEntries.Remove(this.roleEntryOnDataObject);
				}
				if (!roleEntries.Contains(this.addedEntry))
				{
					roleEntries.Add(this.addedEntry);
				}
			}
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000FABF9 File Offset: 0x000F8DF9
		protected override string GetRoleEntryString()
		{
			return this.addedEntry.ToString();
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x000FAC06 File Offset: 0x000F8E06
		protected override LocalizedException GetRoleSaveException(string roleEntry, string role, string exception)
		{
			return new ExRBACSaveAddRoleEntry(roleEntry, role, exception);
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000FAC10 File Offset: 0x000F8E10
		private void VerifyWebServiceEntry(string webMethodName)
		{
			if (base.Fields.IsModified(RbacCommonParameters.ParameterParameters) || base.Fields.IsModified(RbacCommonParameters.ParameterPSSnapinName))
			{
				base.WriteError(new InvalidOperationException(Strings.WebServiceRoleEntryNotSupportParametersAndSnapin), ErrorCategory.InvalidArgument, this.DataObject.Id);
			}
		}

		// Token: 0x040026AC RID: 9900
		private const string ParameterSetParentRoleEntry = "ParentRoleEntry";

		// Token: 0x040026AD RID: 9901
		private const string ParameterParentRoleEntry = "ParentRoleEntry";

		// Token: 0x040026AE RID: 9902
		private const string ParameterOverwrite = "Overwrite";

		// Token: 0x040026AF RID: 9903
		private RoleEntry addedEntry;

		// Token: 0x040026B0 RID: 9904
		private RoleEntry roleEntryOnDataObject;

		// Token: 0x040026B1 RID: 9905
		private static readonly ManagementRoleEntryType[] AllowedRoleEntryTypes = new ManagementRoleEntryType[]
		{
			ManagementRoleEntryType.Cmdlet,
			ManagementRoleEntryType.Script,
			ManagementRoleEntryType.ApplicationPermission,
			ManagementRoleEntryType.WebService
		};
	}
}
