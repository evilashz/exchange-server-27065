using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020002AE RID: 686
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Uninstall", "MsiPackage", DefaultParameterSetName = "PackagePath")]
	public class UninstallMsi : MsiTaskBase
	{
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00065F8A File Offset: 0x0006418A
		internal static TaskException RebootRequiredException
		{
			get
			{
				return UninstallMsi.rebootRequiredException;
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00065F94 File Offset: 0x00064194
		public UninstallMsi()
		{
			base.Activity = Strings.RemovingFiles;
			base.Fields["ProductCode"] = Guid.Empty;
			base.Fields["WarnOnRebootRequests"] = false;
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x00065FE2 File Offset: 0x000641E2
		// (set) Token: 0x0600183D RID: 6205 RVA: 0x00065FF9 File Offset: 0x000641F9
		[Parameter(Mandatory = true, ParameterSetName = "PackagePath")]
		public string PackagePath
		{
			get
			{
				return (string)base.Fields["PackagePath"];
			}
			set
			{
				base.Fields["PackagePath"] = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0006600C File Offset: 0x0006420C
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x00066023 File Offset: 0x00064223
		[Parameter(Mandatory = true, ParameterSetName = "ProductCode")]
		public Guid ProductCode
		{
			get
			{
				return (Guid)base.Fields["ProductCode"];
			}
			set
			{
				base.Fields["ProductCode"] = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x0006603B File Offset: 0x0006423B
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x00066057 File Offset: 0x00064257
		[Parameter(Mandatory = false)]
		public SwitchParameter WarnOnRebootRequests
		{
			get
			{
				return new SwitchParameter((bool)base.Fields["WarnOnRebootRequests"]);
			}
			set
			{
				base.Fields["WarnOnRebootRequests"] = value.ToBool();
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00066078 File Offset: 0x00064278
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.ProductCode == Guid.Empty && string.IsNullOrEmpty(this.PackagePath))
			{
				base.ThrowTerminatingError(new ArgumentNullException("ProductInfo", Strings.ExceptionProductInfoRequired), ErrorCategory.InvalidArgument, null);
			}
			else
			{
				try
				{
					if (!string.IsNullOrEmpty(this.PackagePath) && MsiUtility.IsInstalled(this.PackagePath))
					{
						this.isInstalled = true;
					}
					else if (this.ProductCode != Guid.Empty && MsiUtility.IsInstalled(this.ProductCode))
					{
						this.isInstalled = true;
					}
				}
				catch (ArgumentNullException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				}
				catch (ArgumentOutOfRangeException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
				}
				catch (TaskException exception3)
				{
					base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
				}
			}
			if (this.isInstalled)
			{
				if (base.Features != null && base.Features.Length != 0)
				{
					List<string> list = new List<string>(base.Features);
					bool flag = string.Equals("ExchangeServer.MSI", Path.GetFileName(this.PackagePath), StringComparison.OrdinalIgnoreCase);
					if (flag)
					{
						bool flag2 = false;
						bool flag3 = false;
						foreach (string b in list)
						{
							if (string.Equals("Gateway", b, StringComparison.OrdinalIgnoreCase))
							{
								flag2 = true;
							}
							else if (string.Equals("AdminTools", b, StringComparison.OrdinalIgnoreCase))
							{
								flag3 = true;
							}
						}
						if (flag2 && !flag3)
						{
							base.PropertyValues = string.Format("{0} ADDLOCAL={1}", base.PropertyValues, "AdminToolsNonGateway");
						}
						if (flag2 && flag3)
						{
							list.Add("AdminToolsNonGateway");
						}
					}
					base.PropertyValues = string.Format("{0} REMOVE={1}", base.PropertyValues, string.Join(",", base.Features));
				}
				else
				{
					base.PropertyValues = string.Format("{0} REMOVE=ALL", base.PropertyValues);
				}
				if (base.ParameterSetName == "PackagePath")
				{
					string fullPath = Path.GetFullPath(this.PackagePath);
					base.WriteVerbose(Strings.MsiFullPackagePath(this.PackagePath, fullPath));
					this.PackagePath = fullPath;
					try
					{
						this.ProductCode = MsiUtility.GetProductCode(this.PackagePath);
					}
					catch (ArgumentOutOfRangeException exception4)
					{
						base.WriteError(exception4, ErrorCategory.InvalidArgument, null);
					}
					catch (TaskException exception5)
					{
						base.WriteError(exception5, ErrorCategory.InvalidOperation, null);
					}
				}
				if (!base.Fields.IsChanged("LogFile") && this.PackagePath != null && this.PackagePath != string.Empty)
				{
					base.LogFile = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, Path.ChangeExtension(Path.GetFileName(this.PackagePath), ".msilog"));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0006635C File Offset: 0x0006455C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.isInstalled)
			{
				base.UpdateProgress(0);
				this.RemoveProductCode(this.ProductCode);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00066384 File Offset: 0x00064584
		internal void RemoveProductCode(Guid productCode)
		{
			TaskLogger.LogEnter();
			base.SetLogging();
			try
			{
				MsiUtility.PushExternalUI(base.UIHandler, (InstallLogMode)10115);
				try
				{
					string text = productCode.ToString("B").ToUpper(CultureInfo.InvariantCulture);
					base.WriteVerbose(Strings.RemovingPackageByProductCode(productCode));
					uint num = MsiNativeMethods.ConfigureProduct(text, InstallLevel.Default, InstallState.Default, base.PropertyValues);
					if (num != 0U)
					{
						Win32Exception ex = new Win32Exception((int)num);
						if (num == 3010U)
						{
							if (this.WarnOnRebootRequests)
							{
								TaskException ex2 = new TaskException(Strings.MsiRebootRequiredToFinish(text), ex);
								TaskLogger.SendWatsonReport(ex2);
								base.WriteWarning(ex2.Message);
							}
							else if (UninstallMsi.rebootRequiredException == null)
							{
								UninstallMsi.rebootRequiredException = new TaskException(Strings.MsiRebootRequiredToContinue(text), ex);
							}
						}
						else if (string.IsNullOrEmpty(base.LastMsiError))
						{
							base.WriteError(new TaskException(Strings.MsiCouldNotRemoveProduct(productCode, ex.Message, (int)num), ex), ErrorCategory.InvalidOperation, null);
						}
						else
						{
							base.WriteError(new TaskException(Strings.MsiCouldNotRemoveProductDetailed(productCode, ex.Message, (int)num, base.LastMsiError), ex), ErrorCategory.InvalidOperation, null);
						}
					}
				}
				finally
				{
					MsiUtility.PopExternalUI();
				}
			}
			catch (ArgumentOutOfRangeException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, 0);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000A98 RID: 2712
		private bool isInstalled;

		// Token: 0x04000A99 RID: 2713
		private static TaskException rebootRequiredException;
	}
}
