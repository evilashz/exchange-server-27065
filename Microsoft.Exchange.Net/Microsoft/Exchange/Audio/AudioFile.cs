using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200060B RID: 1547
	internal abstract class AudioFile
	{
		// Token: 0x06001BBC RID: 7100 RVA: 0x00032292 File Offset: 0x00030492
		internal static bool IsMp3(string fileName)
		{
			return AudioFile.HasExtension(fileName, ".mp3");
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0003229F File Offset: 0x0003049F
		internal static bool IsProtectedMp3(string fileName)
		{
			return AudioFile.HasExtension(fileName, ".umrmmp3");
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000322AC File Offset: 0x000304AC
		internal static bool IsWma(string fileName)
		{
			return AudioFile.HasExtension(fileName, ".wma");
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000322B9 File Offset: 0x000304B9
		internal static bool IsProtectedWma(string fileName)
		{
			return AudioFile.HasExtension(fileName, ".umrmwma");
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000322C6 File Offset: 0x000304C6
		internal static bool IsWav(string fileName)
		{
			return AudioFile.HasExtension(fileName, ".wav");
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000322D3 File Offset: 0x000304D3
		internal static bool IsProtectedWav(string fileName)
		{
			return AudioFile.HasExtension(fileName, ".umrmwav");
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000322E0 File Offset: 0x000304E0
		internal static bool IsProtectedVoiceAttachment(string fileName)
		{
			return AudioFile.IsProtectedMp3(fileName) || AudioFile.IsProtectedWav(fileName) || AudioFile.IsProtectedWma(fileName);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00032300 File Offset: 0x00030500
		internal static bool TryGetDRMFileExtension(string extension, out string drmExtension)
		{
			drmExtension = null;
			if (AudioFile.HasExtension(extension, ".wav"))
			{
				drmExtension = ".umrmwav";
			}
			else if (AudioFile.HasExtension(extension, ".wma"))
			{
				drmExtension = ".umrmwma";
			}
			else if (AudioFile.HasExtension(extension, ".mp3"))
			{
				drmExtension = ".umrmmp3";
			}
			return drmExtension != null;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00032358 File Offset: 0x00030558
		internal static bool TryGetNonDRMFileNameFromDRM(string drmFileName, out string nonDRMFileName)
		{
			nonDRMFileName = null;
			string text = null;
			string oldValue = null;
			if (AudioFile.IsProtectedMp3(drmFileName))
			{
				text = ".mp3";
				oldValue = ".umrmmp3";
			}
			else if (AudioFile.IsProtectedWma(drmFileName))
			{
				text = ".wma";
				oldValue = ".umrmwma";
			}
			else if (AudioFile.IsProtectedWav(drmFileName))
			{
				text = ".wav";
				oldValue = ".umrmwav";
			}
			else
			{
				ExTraceGlobals.UtilTracer.TraceError<string>(0L, "Cannot get Non DRM filename for the following filename:{0},", drmFileName);
			}
			if (text != null)
			{
				nonDRMFileName = drmFileName.Replace(oldValue, text);
			}
			return nonDRMFileName != null;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x000323D5 File Offset: 0x000305D5
		private static bool HasExtension(string fileName, string extension)
		{
			return fileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
		}
	}
}
