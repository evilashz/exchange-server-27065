using System;
using System.Collections;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000200 RID: 512
	internal abstract class UMPromptRpcRequestHandler : RequestHandler
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x00043F60 File Offset: 0x00042160
		protected static PromptPreviewRpcResponse GenerateAndEncode(ArrayList prompts)
		{
			PromptPreviewRpcResponse result;
			using (ITempFile tempFile = UMPromptRpcRequestHandler.ToPcm(prompts))
			{
				using (ITempFile tempFile2 = UMPromptRpcRequestHandler.ToCodec(tempFile.FilePath, AudioCodecEnum.Mp3))
				{
					result = UMPromptRpcRequestHandler.GenerateResponse(tempFile2.FilePath);
				}
			}
			return result;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00043FC0 File Offset: 0x000421C0
		protected static ITempFile ToPcm(ArrayList prompts)
		{
			UMPromptRpcRequestHandler.LogPrompts(prompts);
			return Platform.Utilities.SynthesizePromptsToPcmWavFile(prompts);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00043FD3 File Offset: 0x000421D3
		protected static ITempFile ToCodec(ITempFile pcmFile, AudioCodecEnum codec)
		{
			return MediaMethods.FromPcm(pcmFile, codec);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00043FDC File Offset: 0x000421DC
		protected static ITempFile ToCodec(string pcmFilePath, AudioCodecEnum codec)
		{
			return MediaMethods.FromPcm(pcmFilePath, codec);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00043FE8 File Offset: 0x000421E8
		protected static PromptPreviewRpcResponse GenerateResponse(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			if (fileInfo.Length > 5242880L)
			{
				throw new AudioDataIsOversizeException(5, 5L);
			}
			return new PromptPreviewRpcResponse(filePath);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0004401C File Offset: 0x0004221C
		protected static ITempFile ChangeFileNameForTests(ITempFile tempFile, string newFileName, out string filename)
		{
			ITempFile tempFile2 = TempFileFactory.CreateTempDir();
			string text = Path.Combine(tempFile2.FilePath, Path.GetFileName(newFileName));
			text += ".wav";
			File.Copy(tempFile.FilePath, text);
			tempFile.Dispose();
			filename = text;
			return tempFile2;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00044063 File Offset: 0x00042263
		protected override ResponseBase Execute(RequestBase requestBase)
		{
			return UMPromptRpcRequestHandler.GenerateAndEncode(this.GetPrompts(requestBase));
		}

		// Token: 0x06000F03 RID: 3843
		protected abstract ArrayList GetPrompts(RequestBase requestBase);

		// Token: 0x06000F04 RID: 3844 RVA: 0x00044074 File Offset: 0x00042274
		private static void LogPrompts(ArrayList prompts)
		{
			if (prompts.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < prompts.Count; i++)
				{
					stringBuilder.Append("\n");
					stringBuilder.Append(prompts[i].ToString());
				}
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PromptsPlayed, null, new object[]
				{
					Guid.NewGuid().ToString(),
					stringBuilder.ToString()
				});
			}
		}
	}
}
