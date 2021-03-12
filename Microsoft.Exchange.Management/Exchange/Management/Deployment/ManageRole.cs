using System;
using System.IO;
using System.Management.Automation;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001B1 RID: 433
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageRole : ComponentInfoBasedTask
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x00043AC8 File Offset: 0x00041CC8
		protected ManageRole()
		{
			this.role = RoleManager.GetRoleByName(this.RoleName);
			base.Fields["LanguagePacksPath"] = this.GetMsiSourcePath();
			base.Fields["FqdnOrName"] = base.GetFqdnOrNetbiosName();
			base.Fields["InstallPath"] = ConfigurationContext.Setup.InstallPath;
			base.Fields["DatacenterPath"] = ConfigurationContext.Setup.DatacenterPath;
			base.Fields["SetupLoggingPath"] = ConfigurationContext.Setup.SetupLoggingPath;
			base.Fields["LoggingPath"] = ConfigurationContext.Setup.LoggingPath;
			base.Fields["BinPath"] = ConfigurationContext.Setup.BinPath;
			base.Fields["LoggedOnUser"] = this.GetLoggedOnUserName();
			base.Fields["IsFfo"] = new SwitchParameter(false);
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00043BB2 File Offset: 0x00041DB2
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x00043BC9 File Offset: 0x00041DC9
		[Parameter(Mandatory = false)]
		public SwitchParameter IsFfo
		{
			get
			{
				return (SwitchParameter)base.Fields["IsFfo"];
			}
			set
			{
				base.Fields["IsFfo"] = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00043BE1 File Offset: 0x00041DE1
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x00043BF8 File Offset: 0x00041DF8
		[Parameter(Mandatory = false)]
		public string LanguagePacksPath
		{
			get
			{
				return (string)base.Fields["LanguagePacksPath"];
			}
			set
			{
				base.Fields["LanguagePacksPath"] = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00043C0C File Offset: 0x00041E0C
		protected string RoleName
		{
			get
			{
				if (base.Fields["RoleName"] == null)
				{
					Attribute[] customAttributes = Attribute.GetCustomAttributes(base.GetType());
					foreach (Attribute attribute in customAttributes)
					{
						if (attribute is CmdletAttribute)
						{
							CmdletAttribute cmdletAttribute = (CmdletAttribute)attribute;
							base.Fields["RoleName"] = cmdletAttribute.NounName;
							TaskLogger.Trace("Role name is \"{0}\"", new object[]
							{
								base.Fields["RoleName"]
							});
							break;
						}
					}
				}
				return (string)base.Fields["RoleName"];
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00043CB4 File Offset: 0x00041EB4
		protected override InstallationModes InstallationMode
		{
			get
			{
				if ((InstallationModes)base.Fields["InstallationMode"] == InstallationModes.Unknown)
				{
					Attribute[] customAttributes = Attribute.GetCustomAttributes(base.GetType());
					foreach (Attribute attribute in customAttributes)
					{
						if (attribute is CmdletAttribute)
						{
							CmdletAttribute cmdletAttribute = (CmdletAttribute)attribute;
							base.Fields["InstallationMode"] = (InstallationModes)Enum.Parse(typeof(InstallationModes), cmdletAttribute.VerbName);
							TaskLogger.Trace("Attributed mode is \"{0}\"", new object[]
							{
								base.Fields["InstallationMode"]
							});
							break;
						}
					}
				}
				return (InstallationModes)base.Fields["InstallationMode"];
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00043D80 File Offset: 0x00041F80
		protected override void CheckInstallationMode()
		{
			InstallationModes installationModes = (InstallationModes)base.Fields["InstallationMode"];
			if (installationModes == InstallationModes.Install && this.role.IsInstalled)
			{
				base.Fields["InstallationMode"] = InstallationModes.BuildToBuildUpgrade;
				TaskLogger.Trace("Updating mode to \"{0}\" because this role is installed", new object[]
				{
					base.Fields["InstallationMode"]
				});
			}
			if ((installationModes == InstallationModes.Install || installationModes == InstallationModes.BuildToBuildUpgrade) && this.role.IsMissingPostSetup)
			{
				base.Fields["InstallationMode"] = InstallationModes.PostSetupOnly;
				TaskLogger.Trace("Updating mode to \"{0}\" because this role is missing post setup", new object[]
				{
					base.Fields["InstallationMode"]
				});
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00043E70 File Offset: 0x00042070
		protected override void FilterComponents()
		{
			base.FilterComponents();
			base.ComponentInfoList.RemoveAll(delegate(SetupComponentInfo component)
			{
				if (!base.IsDatacenter)
				{
					return false;
				}
				if (this.IsFfo)
				{
					return component.DatacenterMode == DatacenterMode.ExO;
				}
				return component.DatacenterMode == DatacenterMode.Ffo;
			});
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00043E90 File Offset: 0x00042090
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.RoleName,
				this.InstallationMode
			});
			base.WriteVerbose(Strings.AttemptingToManageRole(this.InstallationMode.ToString(), this.RoleName));
			try
			{
				if (!this.role.IsUnpacked && this.InstallationMode != InstallationModes.Uninstall)
				{
					base.ThrowTerminatingError(new TaskException(Strings.MustBeUnpacked), ErrorCategory.InvalidOperation, this.role);
				}
				this.CheckInstallationMode();
				if (this.InstallationMode == InstallationModes.Install)
				{
					bool flag = false;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (Role role in RoleManager.GetInstalledRoles())
					{
						if (!role.IsCurrent)
						{
							flag = true;
							stringBuilder.Append(role.RoleName);
							stringBuilder.Append(" ");
						}
					}
					if (flag)
					{
						base.ThrowTerminatingError(new TaskException(Strings.CannotInstallWithNonCurrentRoles(stringBuilder.ToString())), ErrorCategory.ObjectNotFound, this.role);
					}
				}
				base.InternalValidate();
			}
			catch (Exception ex)
			{
				base.WriteVerbose(Strings.RoleNotConfigured(ex.Message));
				throw;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00043FDC File Offset: 0x000421DC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				base.ComponentInfoList = new SetupComponentInfoCollection();
				try
				{
					base.ComponentInfoList.AddRange(RoleManager.GetRequiredComponents(this.RoleName, this.InstallationMode));
				}
				catch (FileNotFoundException exception)
				{
					base.WriteError(exception, ErrorCategory.ObjectNotFound, null);
				}
				catch (XmlDeserializationException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidData, null);
				}
				if (this.InstallationMode != InstallationModes.Uninstall)
				{
					ProvisioningLayer.Disabled = false;
				}
				base.GenerateAndExecuteTaskScript(InstallationCircumstances.Standalone);
			}
			finally
			{
				TaskLogger.LogExit();
				RoleManager.Reset();
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0004407C File Offset: 0x0004227C
		protected override void PopulateContextVariables()
		{
			base.Fields["PreviousVersion"] = RolesUtility.GetConfiguredVersion(this.RoleName);
			base.Fields["TargetVersion"] = ConfigurationContext.Setup.GetExecutingVersion();
			base.Fields["NetBIOSName"] = base.GetNetBIOSName((string)base.Fields["FqdnOrName"]);
			this.PopulateRoles(RoleManager.Roles, "AllRoles");
			RoleCollection installedRoles = RoleManager.GetInstalledRoles();
			foreach (Role role in installedRoles)
			{
				base.Fields[string.Format("Is{0}Installed", role.RoleName)] = role.IsInstalled;
			}
			this.PopulateRoles(installedRoles, "Roles");
			base.PopulateContextVariables();
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0004416C File Offset: 0x0004236C
		private string GetLoggedOnUserName()
		{
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			return windowsPrincipal.Identity.Name;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00044194 File Offset: 0x00042394
		private string GetMsiSourcePath()
		{
			string text = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{4934D1EA-BE46-48B1-8847-F1AF20E892C1}", "InstallSource", null);
			if (text == null)
			{
				base.WriteError(new LocalizedException(Strings.ExceptionRegistryKeyNotFound("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{4934D1EA-BE46-48B1-8847-F1AF20E892C1}\\InstallSource")), ErrorCategory.InvalidData, null);
			}
			return text;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x000441DC File Offset: 0x000423DC
		private void PopulateRoles(RoleCollection roleCollection, string fieldName)
		{
			if (roleCollection.Count > 0)
			{
				string[] value = roleCollection.ConvertAll<string>((Role r) => r.RoleName).ToArray();
				base.Fields[fieldName] = string.Join(",", value);
			}
		}

		// Token: 0x04000723 RID: 1827
		private Role role;
	}
}
