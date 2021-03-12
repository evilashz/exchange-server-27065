using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C46 RID: 3142
	[Cmdlet("Set", "EcpVirtualDirectory", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetEcpVirtualDirectory : SetWebAppVirtualDirectory<ADEcpVirtualDirectory>
	{
		// Token: 0x170024A1 RID: 9377
		// (get) Token: 0x060076DB RID: 30427 RVA: 0x001E4F57 File Offset: 0x001E3157
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetEcpVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x170024A2 RID: 9378
		// (get) Token: 0x060076DC RID: 30428 RVA: 0x001E4F69 File Offset: 0x001E3169
		// (set) Token: 0x060076DD RID: 30429 RVA: 0x001E4F71 File Offset: 0x001E3171
		[Parameter]
		public new bool DigestAuthentication
		{
			get
			{
				return base.DigestAuthentication;
			}
			set
			{
				base.DigestAuthentication = value;
			}
		}

		// Token: 0x170024A3 RID: 9379
		// (get) Token: 0x060076DE RID: 30430 RVA: 0x001E4F7A File Offset: 0x001E317A
		// (set) Token: 0x060076DF RID: 30431 RVA: 0x001E4F82 File Offset: 0x001E3182
		[Parameter]
		public new bool FormsAuthentication
		{
			get
			{
				return base.FormsAuthentication;
			}
			set
			{
				base.FormsAuthentication = value;
			}
		}

		// Token: 0x170024A4 RID: 9380
		// (get) Token: 0x060076E0 RID: 30432 RVA: 0x001E4F8B File Offset: 0x001E318B
		// (set) Token: 0x060076E1 RID: 30433 RVA: 0x001E4F93 File Offset: 0x001E3193
		[Parameter]
		public new bool AdfsAuthentication
		{
			get
			{
				return base.AdfsAuthentication;
			}
			set
			{
				base.AdfsAuthentication = value;
			}
		}

		// Token: 0x060076E2 RID: 30434 RVA: 0x001E4F9C File Offset: 0x001E319C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADOwaVirtualDirectory adowaVirtualDirectory = WebAppVirtualDirectoryHelper.FindWebAppVirtualDirectoryInSameWebSite<ADOwaVirtualDirectory>(this.DataObject, base.DataSession);
			if (adowaVirtualDirectory != null)
			{
				WebAppVirtualDirectoryHelper.CheckTwoWebAppVirtualDirectories(this.DataObject, adowaVirtualDirectory, new Action<string>(base.WriteWarning), Strings.EcpAuthenticationNotMatchOwaWarning, Strings.EcpUrlNotMatchOwaWarning);
			}
			else
			{
				this.WriteWarning(Strings.CreateOwaForEcpWarning);
			}
			if (this.DataObject.IsChanged(ADEcpVirtualDirectorySchema.AdminEnabled) || this.DataObject.IsChanged(ADEcpVirtualDirectorySchema.OwaOptionsEnabled))
			{
				this.WriteWarning(Strings.NeedIisRestartForChangingECPFeatureWarning);
			}
			base.InternalProcessRecord();
			ADEcpVirtualDirectory dataObject = this.DataObject;
			WebAppVirtualDirectoryHelper.UpdateMetabase(dataObject, dataObject.MetabasePath, true);
			if (!ExchangeServiceVDirHelper.IsBackEndVirtualDirectory(this.DataObject) && base.Fields.Contains("FormsAuthentication"))
			{
				ExchangeServiceVDirHelper.UpdateFrontEndHttpModule(this.DataObject, this.FormsAuthentication);
			}
			if (base.Fields.Contains("AdfsAuthentication"))
			{
				ExchangeServiceVDirHelper.SetAdfsAuthenticationModule(this.DataObject.AdfsAuthentication, false, this.DataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060076E3 RID: 30435 RVA: 0x001E509C File Offset: 0x001E329C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Fields.Contains("AdfsAuthentication") && !this.DataObject.AdfsAuthentication)
			{
				ADOwaVirtualDirectory adowaVirtualDirectory = WebAppVirtualDirectoryHelper.FindWebAppVirtualDirectoryInSameWebSite<ADOwaVirtualDirectory>(this.DataObject, base.DataSession);
				if (adowaVirtualDirectory != null && adowaVirtualDirectory.AdfsAuthentication)
				{
					base.WriteError(new TaskException(Strings.CannotDisableAdfsEcpWithoutOwaFirst), ErrorCategory.InvalidOperation, null);
				}
			}
		}

		// Token: 0x060076E4 RID: 30436 RVA: 0x001E5108 File Offset: 0x001E3308
		protected override void UpdateDataObject(ADEcpVirtualDirectory dataObject)
		{
			if (base.Fields.Contains("GzipLevel") && base.GzipLevel != dataObject.GzipLevel)
			{
				dataObject.GzipLevel = base.GzipLevel;
				base.CheckGzipLevelIsNotError(dataObject.GzipLevel);
			}
			base.UpdateDataObject(dataObject);
		}
	}
}
