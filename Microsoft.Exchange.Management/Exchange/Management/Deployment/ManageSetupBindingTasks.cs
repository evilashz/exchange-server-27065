using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000203 RID: 515
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageSetupBindingTasks : ComponentInfoBasedTask
	{
		// Token: 0x06001183 RID: 4483 RVA: 0x0004D040 File Offset: 0x0004B240
		public ManageSetupBindingTasks()
		{
			base.Fields["InstallPath"] = ConfigurationContext.Setup.InstallPath;
			base.Fields["DatacenterPath"] = ConfigurationContext.Setup.DatacenterPath;
			base.Fields["SetupLoggingPath"] = ConfigurationContext.Setup.SetupLoggingPath;
			base.Fields["LoggingPath"] = ConfigurationContext.Setup.LoggingPath;
			base.Fields["BinPath"] = ConfigurationContext.Setup.BinPath;
			base.Fields["IsFfo"] = new SwitchParameter(false);
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0004D0D7 File Offset: 0x0004B2D7
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x0004D0EE File Offset: 0x0004B2EE
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

		// Token: 0x06001186 RID: 4486 RVA: 0x0004D108 File Offset: 0x0004B308
		protected virtual string[] GetComponentFiles(string roleNameOrAllRoles)
		{
			if (roleNameOrAllRoles != null)
			{
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6001040-1 == null)
				{
					<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6001040-1 = new Dictionary<string, int>(15)
					{
						{
							"AllRoles",
							0
						},
						{
							"AdminToolsRole",
							1
						},
						{
							"BridgeheadRole",
							2
						},
						{
							"ClientAccessRole",
							3
						},
						{
							"GatewayRole",
							4
						},
						{
							"MailboxRole",
							5
						},
						{
							"UnifiedMessagingRole",
							6
						},
						{
							"FrontendTransportRole",
							7
						},
						{
							"CafeRole",
							8
						},
						{
							"MonitoringRole",
							9
						},
						{
							"CentralAdminRole",
							10
						},
						{
							"CentralAdminDatabaseRole",
							11
						},
						{
							"CentralAdminFrontEndRole",
							12
						},
						{
							"LanguagePacksRole",
							13
						},
						{
							"OSPRole",
							14
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6001040-1.TryGetValue(roleNameOrAllRoles, out num))
				{
					string arg;
					switch (num)
					{
					case 0:
						arg = "AllRoles";
						break;
					case 1:
						arg = "AdminTools";
						break;
					case 2:
						arg = "Bridgehead";
						break;
					case 3:
						arg = "ClientAccess";
						break;
					case 4:
						arg = "Gateway";
						break;
					case 5:
						arg = "Mailbox";
						break;
					case 6:
						arg = "UnifiedMessaging";
						break;
					case 7:
						arg = "FrontendTransport";
						break;
					case 8:
						arg = "Cafe";
						break;
					case 9:
						arg = "MonitoringRole";
						break;
					case 10:
						arg = "CentralAdmin";
						break;
					case 11:
						arg = "CentralAdminDatabase";
						break;
					case 12:
						arg = "CentralAdminFrontEnd";
						break;
					case 13:
						arg = "LanguagePacks";
						break;
					case 14:
						arg = "OSP";
						break;
					default:
						goto IL_1B2;
					}
					object[] customAttributes = base.GetType().GetCustomAttributes(typeof(CmdletAttribute), false);
					string nounName = ((CmdletAttribute)customAttributes[0]).NounName;
					if (roleNameOrAllRoles != "AllRoles" && RoleManager.GetRoleByName(roleNameOrAllRoles).IsDatacenterOnly)
					{
						return new string[]
						{
							string.Format("setup\\data\\Datacenter{0}{1}Component.xml", arg, nounName)
						};
					}
					return new string[]
					{
						string.Format("setup\\data\\{0}{1}Component.xml", arg, nounName),
						string.Format("setup\\data\\Datacenter{0}{1}Component.xml", arg, nounName)
					};
				}
			}
			IL_1B2:
			throw new ArgumentException(string.Format("Unknown role name '{0}'", roleNameOrAllRoles), "roleNameOrAllRoles");
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0004D361 File Offset: 0x0004B561
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x0004D369 File Offset: 0x0004B569
		[Parameter(Mandatory = true)]
		public virtual InstallationModes Mode
		{
			get
			{
				return base.InstallationMode;
			}
			set
			{
				base.InstallationMode = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0004D374 File Offset: 0x0004B574
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x0004D3AF File Offset: 0x0004B5AF
		[Parameter(Mandatory = true)]
		public virtual string[] Roles
		{
			get
			{
				string text = (string)base.Fields["Roles"];
				if (text == null)
				{
					return null;
				}
				return text.Split(new char[]
				{
					','
				});
			}
			set
			{
				base.Fields["Roles"] = string.Join(",", value);
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004D3CC File Offset: 0x0004B5CC
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x0004D3E3 File Offset: 0x0004B5E3
		[Parameter(Mandatory = false)]
		public virtual Version PreviousVersion
		{
			get
			{
				return (Version)base.Fields["PreviousVersion"];
			}
			set
			{
				base.Fields["PreviousVersion"] = value;
			}
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0004D3F6 File Offset: 0x0004B5F6
		protected override void PopulateContextVariables()
		{
			base.Fields["TargetVersion"] = ConfigurationContext.Setup.GetExecutingVersion();
			base.PopulateContextVariables();
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0004D414 File Offset: 0x0004B614
		protected virtual void PopulateComponentInfoFileNames()
		{
			base.ComponentInfoFileNames.AddRange(this.GetComponentFiles("AllRoles"));
			foreach (string text in this.Roles)
			{
				if (RoleManager.GetRoleByName(text) == null)
				{
					base.WriteError(new LocalizedException(Strings.ErrorUnknownRole(text)), ErrorCategory.InvalidArgument, null);
				}
				else
				{
					base.ComponentInfoFileNames.AddRange(this.GetComponentFiles(text));
				}
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0004D47F File Offset: 0x0004B67F
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.ComponentInfoFileNames = new List<string>();
			this.PopulateComponentInfoFileNames();
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0004D4D3 File Offset: 0x0004B6D3
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
	}
}
