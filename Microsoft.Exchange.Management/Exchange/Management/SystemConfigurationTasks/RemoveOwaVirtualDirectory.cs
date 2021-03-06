using System;
using System.Collections;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.IisTasks;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C39 RID: 3129
	[Cmdlet("Remove", "OwaVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveOwaVirtualDirectory : RemoveExchangeVirtualDirectory<ADOwaVirtualDirectory>
	{
		// Token: 0x1700247C RID: 9340
		// (get) Token: 0x0600766E RID: 30318 RVA: 0x001E374C File Offset: 0x001E194C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveOwaVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x0600766F RID: 30319 RVA: 0x001E3760 File Offset: 0x001E1960
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (this.scriptMapPhysicalPath != null && !IisUtility.IsAnyWebVirtualDirUsingThisExecutableInScriptMap(base.DataObject.Server.Name, this.scriptMapPhysicalPath))
			{
				using (IsapiExtensionList isapiExtensionList = new IsapiExtensionList(base.DataObject.Server.Name))
				{
					isapiExtensionList.RemoveByExecutable(this.scriptMapPhysicalPath);
					isapiExtensionList.CommitChanges();
					IisUtility.CommitMetabaseChanges((base.DataObject.Server == null) ? null : base.DataObject.Server.Name);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007670 RID: 30320 RVA: 0x001E380C File Offset: 0x001E1A0C
		protected override void PreDeleteFromMetabase()
		{
			if (!DirectoryEntry.Exists(base.DataObject.MetabasePath))
			{
				return;
			}
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(base.DataObject.MetabasePath, new Task.TaskErrorLoggingReThrowDelegate(this.WriteError), this.Identity))
			{
				PropertyValueCollection propertyValueCollection = directoryEntry.Properties["ScriptMaps"];
				foreach (object obj in propertyValueCollection)
				{
					string text = (string)obj;
					string[] array = text.Split(new char[]
					{
						','
					});
					if (array.Length >= 2)
					{
						string fileName = Path.GetFileName(array[1]);
						if (string.Compare(fileName, "davex.dll", true, CultureInfo.InvariantCulture) == 0 || string.Compare(fileName, "exprox.dll", true, CultureInfo.InvariantCulture) == 0)
						{
							this.scriptMapPhysicalPath = array[1];
							break;
						}
					}
				}
				try
				{
					string parent = null;
					string text2 = null;
					string name = null;
					IisUtility.ParseApplicationRootPath(directoryEntry.Path, ref parent, ref text2, ref name);
					if (IisUtility.Exists(parent, name, "IIsWebVirtualDir"))
					{
						OwaVirtualDirectoryHelper.DisableIsapiFilter(base.DataObject);
					}
				}
				catch (Exception)
				{
					this.WriteWarning(Strings.OwaMetabaseIsapiUninstallFailure);
					throw;
				}
			}
		}

		// Token: 0x06007671 RID: 30321 RVA: 0x001E396C File Offset: 0x001E1B6C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			ADOwaVirtualDirectory dataObject = base.DataObject;
			if (dataObject.ExchangeVersion.IsOlderThan(ADOwaVirtualDirectory.MinimumSupportedExchangeObjectVersion))
			{
				base.WriteError(new TaskException(Strings.ErrorSetOlderVirtualDirectory(dataObject.Identity.ToString(), dataObject.ExchangeVersion.ToString(), ADOwaVirtualDirectory.MinimumSupportedExchangeObjectVersion.ToString())), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06007672 RID: 30322 RVA: 0x001E39D4 File Offset: 0x001E1BD4
		protected override void DeleteFromMetabase()
		{
			string webSiteRoot = IisUtility.GetWebSiteRoot(base.DataObject.MetabasePath);
			IList legacyVirtualDirectories = OwaVirtualDirectoryHelper.GetLegacyVirtualDirectories();
			if (legacyVirtualDirectories != null)
			{
				foreach (object obj in legacyVirtualDirectories)
				{
					string name = (string)obj;
					if (IisUtility.WebDirObjectExists(webSiteRoot, name))
					{
						IisUtility.DeleteWebDirObject(webSiteRoot, name);
					}
				}
			}
			base.DeleteFromMetabase();
		}

		// Token: 0x04003BC3 RID: 15299
		private string scriptMapPhysicalPath;
	}
}
