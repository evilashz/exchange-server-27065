using System;
using System.Management.Automation;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D7 RID: 471
	public class AddExtensionService : DataSourceService, IUploadHandler
	{
		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x000734AE File Offset: 0x000716AE
		public virtual Type SetParameterType
		{
			get
			{
				return typeof(UploadExtensionParameter);
			}
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x000734BA File Offset: 0x000716BA
		public int MaxFileSize
		{
			get
			{
				return 10485760;
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000734C1 File Offset: 0x000716C1
		protected virtual void AddParameters(PSCommand installCommand, WebServiceParameters param)
		{
			installCommand.AddParameters(param);
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000734CC File Offset: 0x000716CC
		[PrincipalPermission(SecurityAction.Demand, Role = "New-App?FileStream@W:Self")]
		public PowerShellResults ProcessUpload(UploadFileContext context, WebServiceParameters param)
		{
			param.FaultIfNull();
			UploadExtensionParameter uploadExtensionParameter = (UploadExtensionParameter)param;
			uploadExtensionParameter.FileStream = context.FileStream;
			if (RbacPrincipal.Current.RbacConfiguration.HasRoleOfType(RoleType.MyReadWriteMailboxApps))
			{
				uploadExtensionParameter.AllowReadWriteMailbox = new SwitchParameter(true);
			}
			PSCommand pscommand = new PSCommand().AddCommand("New-App");
			this.AddParameters(pscommand, param);
			return base.Invoke<ExtensionRow>(pscommand);
		}

		// Token: 0x04001F07 RID: 7943
		public const string NewApp = "New-App";

		// Token: 0x04001F08 RID: 7944
		public const int MaxExtensionSize = 10;

		// Token: 0x04001F09 RID: 7945
		public const int BytesInMB = 1048576;

		// Token: 0x04001F0A RID: 7946
		private const string ProcessUploadRole = "New-App?FileStream@W:Self";
	}
}
