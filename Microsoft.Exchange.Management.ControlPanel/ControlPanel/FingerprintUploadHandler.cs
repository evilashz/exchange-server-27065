using System;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000201 RID: 513
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class FingerprintUploadHandler : DataSourceService, IUploadHandler
	{
		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000780C4 File Offset: 0x000762C4
		public Type SetParameterType
		{
			get
			{
				return typeof(FingerprintUploadParameters);
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000780D0 File Offset: 0x000762D0
		public PowerShellResults ProcessUpload(UploadFileContext context, WebServiceParameters parameters)
		{
			parameters.FaultIfNull();
			if (parameters is FingerprintUploadParameters)
			{
				FingerprintUploadParameters fingerprintUploadParameters = (FingerprintUploadParameters)parameters;
				byte[] array = new byte[context.FileStream.Length];
				context.FileStream.Read(array, 0, array.Length);
				fingerprintUploadParameters.FileData = array;
				string description = context.FileName.Substring(context.FileName.LastIndexOf("\\") + 1);
				fingerprintUploadParameters.Description = description;
				return base.NewObject<Fingerprint, FingerprintUploadParameters>("New-Fingerprint", fingerprintUploadParameters);
			}
			return null;
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x00078151 File Offset: 0x00076351
		public int MaxFileSize
		{
			get
			{
				return FingerprintUploadHandler.maxFileSize;
			}
		}

		// Token: 0x04001F7C RID: 8060
		private static int maxFileSize = AppConfigLoader.GetConfigIntValue("MaxFileSize", 0, 20971520, 20971520);
	}
}
