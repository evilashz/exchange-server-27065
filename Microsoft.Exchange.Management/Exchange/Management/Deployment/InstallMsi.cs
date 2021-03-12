using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000297 RID: 663
	[Cmdlet("Install", "MsiPackage")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallMsi : MsiTaskBase
	{
		// Token: 0x060017F4 RID: 6132 RVA: 0x00064CD8 File Offset: 0x00062ED8
		public InstallMsi()
		{
			base.Activity = Strings.UnpackingFiles;
			base.Fields["Reinstall"] = false;
			base.Fields["ReinstallMode"] = "vmous";
			base.Fields["NoActionIfInstalled"] = false;
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00064D3E File Offset: 0x00062F3E
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x00064D55 File Offset: 0x00062F55
		[Parameter(Mandatory = false)]
		public LongPath UpdatesDir
		{
			get
			{
				return (LongPath)base.Fields["UpdatesDir"];
			}
			set
			{
				base.Fields["UpdatesDir"] = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00064D68 File Offset: 0x00062F68
		// (set) Token: 0x060017F8 RID: 6136 RVA: 0x00064D7F File Offset: 0x00062F7F
		[Parameter(Mandatory = true)]
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

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00064D92 File Offset: 0x00062F92
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x00064DA9 File Offset: 0x00062FA9
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TargetDirectory
		{
			get
			{
				return (LocalLongFullPath)base.Fields["TargetDirectory"];
			}
			set
			{
				base.Fields["TargetDirectory"] = value;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x00064DBC File Offset: 0x00062FBC
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x00064DD3 File Offset: 0x00062FD3
		[Parameter(Mandatory = false)]
		public bool Reinstall
		{
			get
			{
				return (bool)base.Fields["Reinstall"];
			}
			set
			{
				base.Fields["Reinstall"] = value;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x00064DEB File Offset: 0x00062FEB
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x00064E02 File Offset: 0x00063002
		[Parameter(Mandatory = false)]
		public string ReinstallMode
		{
			get
			{
				return (string)base.Fields["ReinstallMode"];
			}
			set
			{
				base.Fields["ReinstallMode"] = value;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x00064E15 File Offset: 0x00063015
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x00064E31 File Offset: 0x00063031
		[Parameter(Mandatory = false)]
		public SwitchParameter NoActionIfInstalled
		{
			get
			{
				return new SwitchParameter((bool)base.Fields["NoActionIfInstalled"]);
			}
			set
			{
				base.Fields["NoActionIfInstalled"] = value.ToBool();
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00064E50 File Offset: 0x00063050
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			try
			{
				this.newProduct = !MsiUtility.IsInstalled(this.PackagePath);
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
			if (!this.NoActionIfInstalled || this.newProduct)
			{
				if (!this.Reinstall)
				{
					if (this.newProduct && this.TargetDirectory != null && this.TargetDirectory.ToString() != string.Empty)
					{
						base.PropertyValues = string.Format("{0} TARGETDIR=\"{1}\"", base.PropertyValues, this.TargetDirectory);
					}
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
								base.PropertyValues = string.Format("{0} REMOVE={1}", base.PropertyValues, "AdminToolsNonGateway");
							}
							if (!flag2 && flag3)
							{
								list.Add("AdminToolsNonGateway");
							}
						}
						base.PropertyValues = string.Format("{0} ADDLOCAL={1}", base.PropertyValues, string.Join(",", list.ToArray()));
					}
				}
				else
				{
					base.PropertyValues = string.Format("{0} REINSTALL=ALL REINSTALLMODE={1}", base.PropertyValues, this.ReinstallMode);
				}
				if (!base.Fields.IsChanged("LogFile"))
				{
					base.LogFile = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, Path.ChangeExtension(Path.GetFileName(this.PackagePath), ".msilog"));
				}
				string fullPath = Path.GetFullPath(this.PackagePath);
				base.WriteVerbose(Strings.MsiFullPackagePath(this.PackagePath, fullPath));
				this.PackagePath = fullPath;
			}
			else
			{
				base.WriteVerbose(Strings.MsiPackageAlreadyInstalled(this.PackagePath));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x000650D0 File Offset: 0x000632D0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (!File.Exists(this.PackagePath))
			{
				base.WriteError(new FileNotFoundException(this.PackagePath), ErrorCategory.InvalidData, null);
			}
			if (this.Reinstall)
			{
				string packageName = null;
				try
				{
					packageName = MsiUtility.GetProductInfo(MsiUtility.GetProductCode(this.PackagePath), InstallProperty.PackageName);
				}
				catch (ArgumentOutOfRangeException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				}
				catch (TaskException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
				}
				if (this.newProduct)
				{
					base.WriteError(new TaskException(Strings.MsiIsNotInstalled(packageName)), ErrorCategory.InvalidArgument, 0);
				}
				else if (base.Features != null && base.Features.Length != 0)
				{
					base.WriteError(new TaskException(Strings.MsiReinstallMustAll), ErrorCategory.InvalidArgument, 0);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x000651A8 File Offset: 0x000633A8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.NoActionIfInstalled || this.newProduct)
			{
				base.UpdateProgress(0);
				this.InstallPackageFile(this.PackagePath);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000651DC File Offset: 0x000633DC
		internal void InstallPackageFile(string packagePath)
		{
			TaskLogger.LogEnter();
			try
			{
				Guid productCode = MsiUtility.GetProductCode(this.PackagePath);
				base.SetLogging();
				MsiUtility.PushExternalUI(base.UIHandler, (InstallLogMode)10115);
				try
				{
					base.WriteVerbose(Strings.InstallingPackage(packagePath));
					List<MsiNativeMethods.PatchSequenceInfo> list = new List<MsiNativeMethods.PatchSequenceInfo>();
					uint num;
					if (this.UpdatesDir != null)
					{
						try
						{
							string[] files = Directory.GetFiles(this.UpdatesDir.PathName, "*.msp");
							foreach (string patchData in files)
							{
								MsiNativeMethods.PatchSequenceInfo item = new MsiNativeMethods.PatchSequenceInfo
								{
									patchData = patchData,
									patchDataType = PatchDataType.PatchFile,
									order = -1,
									status = 0U
								};
								list.Add(item);
								base.WriteVerbose(Strings.PatchFileFound(item.patchData));
							}
						}
						catch (DirectoryNotFoundException)
						{
							base.WriteVerbose(Strings.UpdatesDirectoryNotFound(this.UpdatesDir.PathName));
						}
						catch (IOException exception)
						{
							base.WriteError(exception, ErrorCategory.InvalidOperation, null);
						}
						if (list.Count > 0)
						{
							MsiNativeMethods.PatchSequenceInfo[] array2 = list.ToArray();
							num = MsiNativeMethods.DetermineApplicablePatches(this.PackagePath, array2.Length, array2);
							if (num != 0U)
							{
								base.WriteError(new TaskException(Strings.FailedToSortPatches(num)), ErrorCategory.InvalidOperation, null);
							}
							else
							{
								base.WriteVerbose(Strings.SortedAvailablePatches);
								List<MsiNativeMethods.PatchSequenceInfo> list2 = new List<MsiNativeMethods.PatchSequenceInfo>();
								foreach (MsiNativeMethods.PatchSequenceInfo item2 in array2)
								{
									base.WriteVerbose(Strings.PrintPatchOrderInfo(item2.patchData, item2.order, item2.status));
									if (item2.order >= 0)
									{
										list2.Add(item2);
									}
								}
								if (list2.Count > 0)
								{
									list2.Sort(new Comparison<MsiNativeMethods.PatchSequenceInfo>(MsiNativeMethods.ComparePatchSequence));
									StringBuilder stringBuilder = new StringBuilder();
									foreach (MsiNativeMethods.PatchSequenceInfo patchSequenceInfo in list2)
									{
										base.WriteVerbose(Strings.ValidPatch(patchSequenceInfo.patchData, patchSequenceInfo.order));
										stringBuilder.Append(patchSequenceInfo.patchData);
										stringBuilder.Append(";");
									}
									base.WriteVerbose(Strings.PatchAttributeValue(stringBuilder.ToString()));
									base.PropertyValues = string.Format("PATCH=\"{0}\" {1}", stringBuilder.ToString(), base.PropertyValues);
								}
							}
						}
					}
					else
					{
						base.WriteVerbose(Strings.NoUpdatesDirectorySpecified);
					}
					if (this.newProduct)
					{
						base.WriteVerbose(Strings.NewProductInstallation(packagePath, base.PropertyValues));
						num = MsiNativeMethods.InstallProduct(packagePath, base.PropertyValues);
					}
					else
					{
						string productCodeString = productCode.ToString("B").ToUpper(CultureInfo.InvariantCulture);
						base.WriteVerbose(Strings.ExistingProductConfiguration(productCodeString, base.PropertyValues));
						num = MsiNativeMethods.ConfigureProduct(productCodeString, InstallLevel.Default, InstallState.Default, base.PropertyValues);
					}
					if (num != 0U)
					{
						Win32Exception ex = new Win32Exception((int)num);
						if (num == 3010U)
						{
							throw new TaskException(Strings.MsiRebootRequiredToContinue(packagePath), ex);
						}
						if (string.IsNullOrEmpty(base.LastMsiError))
						{
							base.WriteError(new TaskException(Strings.MsiInstallFailed(packagePath, ex.Message, (int)num), ex), ErrorCategory.InvalidOperation, null);
						}
						else
						{
							base.WriteError(new TaskException(Strings.MsiInstallFailedDetailed(packagePath, ex.Message, (int)num, base.LastMsiError), ex), ErrorCategory.InvalidOperation, null);
						}
					}
				}
				finally
				{
					MsiUtility.PopExternalUI();
				}
			}
			catch (ArgumentOutOfRangeException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
			}
			catch (TaskException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000A16 RID: 2582
		private bool newProduct = true;
	}
}
