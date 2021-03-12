using System;
using System.IO;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F0 RID: 752
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class UserPhotoService : DataSourceService, IUploadHandler, IUserPhotoService
	{
		// Token: 0x06002D2D RID: 11565 RVA: 0x0008A6A8 File Offset: 0x000888A8
		[PrincipalPermission(SecurityAction.Demand, Role = "Set-UserPhoto?Identity@W:Self")]
		public PowerShellResults ProcessUpload(UploadFileContext context, WebServiceParameters param)
		{
			param.FaultIfNull();
			SetUserPhotoParameters setUserPhotoParameters = (SetUserPhotoParameters)param;
			setUserPhotoParameters.PhotoStream = context.FileStream;
			Identity identity = Identity.ParseIdentity(setUserPhotoParameters.Identity);
			if (identity == null || string.IsNullOrEmpty(identity.RawIdentity))
			{
				throw new BadQueryParameterException("Identity");
			}
			return this.SetPhoto(identity, context.FileStream);
		}

		// Token: 0x17001E28 RID: 7720
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x0008A708 File Offset: 0x00088908
		public Type SetParameterType
		{
			get
			{
				return typeof(SetUserPhotoParameters);
			}
		}

		// Token: 0x17001E29 RID: 7721
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x0008A714 File Offset: 0x00088914
		public int MaxFileSize
		{
			get
			{
				return 20971520;
			}
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x0008A71C File Offset: 0x0008891C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UserPhoto?Identity@R:Self")]
		public PowerShellResults<UserPhoto> GetSavedPhoto(Identity identity)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Get-UserPhoto");
			return base.GetObject<UserPhoto>(pscommand, identity);
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x0008A744 File Offset: 0x00088944
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UserPhoto?Identity@R:Self")]
		public PowerShellResults<UserPhoto> GetPreviewPhoto(Identity identity)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Get-UserPhoto");
			pscommand.AddParameters(new GetUserPhotoParameters
			{
				Preview = new SwitchParameter(true)
			});
			return base.GetObject<UserPhoto>(pscommand, identity);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x0008A788 File Offset: 0x00088988
		[PrincipalPermission(SecurityAction.Demand, Role = "Set-UserPhoto?Identity@W:Self")]
		public PowerShellResults SetPhoto(Identity identity, Stream stream)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Set-UserPhoto");
			SetUserPhotoParameters setUserPhotoParameters = new SetUserPhotoParameters();
			setUserPhotoParameters.PhotoStream = stream;
			setUserPhotoParameters.Preview = new SwitchParameter(true);
			return base.Invoke(pscommand, new Identity[]
			{
				identity
			}, setUserPhotoParameters);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0008A7D4 File Offset: 0x000889D4
		[PrincipalPermission(SecurityAction.Demand, Role = "Set-UserPhoto?Identity@W:Self")]
		public PowerShellResults SavePhoto(Identity identity)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Set-UserPhoto");
			SetUserPhotoParameters setUserPhotoParameters = new SetUserPhotoParameters();
			setUserPhotoParameters.Save = new SwitchParameter(true);
			return base.Invoke(pscommand, new Identity[]
			{
				identity
			}, setUserPhotoParameters);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0008A81C File Offset: 0x00088A1C
		[PrincipalPermission(SecurityAction.Demand, Role = "Set-UserPhoto?Identity@W:Self")]
		public PowerShellResults CancelPhoto(Identity identity)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Set-UserPhoto");
			SetUserPhotoParameters setUserPhotoParameters = new SetUserPhotoParameters();
			setUserPhotoParameters.Cancel = new SwitchParameter(true);
			return base.Invoke(pscommand, new Identity[]
			{
				identity
			}, setUserPhotoParameters);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x0008A864 File Offset: 0x00088A64
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-UserPhoto?Identity@W:Self")]
		public PowerShellResults RemovePhoto(Identity identity)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Remove-UserPhoto");
			RemoveUserPhotoParameters parameters = new RemoveUserPhotoParameters();
			return base.Invoke(pscommand, new Identity[]
			{
				identity
			}, parameters);
		}

		// Token: 0x0400223C RID: 8764
		internal const string GetCmdlet = "Get-UserPhoto";

		// Token: 0x0400223D RID: 8765
		internal const string RemoveCmdlet = "Remove-UserPhoto";

		// Token: 0x0400223E RID: 8766
		internal const string SetCmdlet = "Set-UserPhoto";

		// Token: 0x0400223F RID: 8767
		internal const string ReadScope = "@R:Self";

		// Token: 0x04002240 RID: 8768
		internal const string WriteScope = "@W:Self";

		// Token: 0x04002241 RID: 8769
		private const int MaxFileSizeAllowed = 20971520;

		// Token: 0x04002242 RID: 8770
		private const string Noun = "UserPhoto";

		// Token: 0x04002243 RID: 8771
		private const string GetUserPhotoRole = "Get-UserPhoto?Identity@R:Self";

		// Token: 0x04002244 RID: 8772
		private const string RemoveUserPhotoRole = "Remove-UserPhoto?Identity@W:Self";

		// Token: 0x04002245 RID: 8773
		private const string SetUserPhotoRole = "Set-UserPhoto?Identity@W:Self";
	}
}
