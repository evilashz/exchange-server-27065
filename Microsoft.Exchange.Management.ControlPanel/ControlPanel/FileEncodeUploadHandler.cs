using System;
using System.IO;
using System.IO.Compression;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001FF RID: 511
	internal class FileEncodeUploadHandler : IUploadHandler
	{
		// Token: 0x06002694 RID: 9876 RVA: 0x00077FCF File Offset: 0x000761CF
		public static Stream DecodeContent(string content)
		{
			return new GZipStream(new MemoryStream(Convert.FromBase64String(content)), CompressionMode.Decompress);
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x00077FE2 File Offset: 0x000761E2
		public virtual Type SetParameterType
		{
			get
			{
				return typeof(BaseWebServiceParameters);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x00077FF0 File Offset: 0x000761F0
		public virtual PowerShellResults ProcessUpload(UploadFileContext context, WebServiceParameters parameters)
		{
			PowerShellResults result;
			using (MemoryStream memoryStream = new MemoryStream(Math.Max((int)context.FileStream.Length / 2, 1024)))
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
				{
					context.FileStream.CopyTo(gzipStream);
				}
				result = new PowerShellResults<EncodedFile>
				{
					Output = new EncodedFile[]
					{
						new EncodedFile
						{
							FileContent = Convert.ToBase64String(memoryStream.ToArray())
						}
					}
				};
			}
			return result;
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x0007809C File Offset: 0x0007629C
		public virtual int MaxFileSize
		{
			get
			{
				return 10485760;
			}
		}
	}
}
