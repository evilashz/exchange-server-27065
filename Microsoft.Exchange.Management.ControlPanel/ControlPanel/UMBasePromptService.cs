using System;
using System.IO;
using System.Management.Automation;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004AA RID: 1194
	public abstract class UMBasePromptService : DataSourceService, IUploadHandler
	{
		// Token: 0x17002361 RID: 9057
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x000B311E File Offset: 0x000B131E
		public Type SetParameterType
		{
			get
			{
				return typeof(UploadUMParameter);
			}
		}

		// Token: 0x17002362 RID: 9058
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x000B312A File Offset: 0x000B132A
		public int MaxFileSize
		{
			get
			{
				return 5242880;
			}
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000B3134 File Offset: 0x000B1334
		[PrincipalPermission(SecurityAction.Demand, Role = "Import-UMPrompt?PromptFileStream&PromptFileName@W:Organization")]
		public PowerShellResults ProcessUpload(UploadFileContext context, WebServiceParameters param)
		{
			param.FaultIfNull();
			UploadUMParameter uploadUMParameter = (UploadUMParameter)param;
			uploadUMParameter.PromptFileStream = context.FileStream;
			uploadUMParameter.PromptFileName = Path.GetFileName(context.FileName);
			if (uploadUMParameter.UMAutoAttendant == null && uploadUMParameter.UMDialPlan == null)
			{
				uploadUMParameter.UMAutoAttendant.FaultIfNull();
			}
			return this.ImportObject(uploadUMParameter);
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000B319C File Offset: 0x000B139C
		private PowerShellResults ImportObject(UploadUMParameter parameters)
		{
			Identity translationIdentity = (parameters.UMAutoAttendant != null) ? parameters.UMAutoAttendant : parameters.UMDialPlan;
			return base.Invoke(new PSCommand().AddCommand("Import-UMPrompt"), translationIdentity, parameters);
		}

		// Token: 0x04002761 RID: 10081
		private const string ProcessUploadRole = "Import-UMPrompt?PromptFileStream&PromptFileName@W:Organization";
	}
}
