using System;
using System.Collections.Generic;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000014 RID: 20
	internal class AudioCodec
	{
		// Token: 0x0600019C RID: 412 RVA: 0x000068CC File Offset: 0x00004ACC
		static AudioCodec()
		{
			AudioCodec audioCodec = new AudioCodec(AudioCodecEnum.Mp3, "audio/mp3", true, true, 16000, ".mp3", ".umrmmp3");
			AudioCodec.codecTable.Add(audioCodec.Codec, audioCodec);
			audioCodec = new AudioCodec(AudioCodecEnum.Wma, "audio/wma", false, true, 8000, ".wma", ".umrmwma");
			AudioCodec.codecTable.Add(audioCodec.Codec, audioCodec);
			audioCodec = new AudioCodec(AudioCodecEnum.Gsm, "audio/wav", true, false, 8000, ".wav", ".umrmwav");
			AudioCodec.codecTable.Add(audioCodec.Codec, audioCodec);
			audioCodec = new AudioCodec(AudioCodecEnum.G711, "audio/wav", true, false, 8000, ".wav", ".umrmwav");
			AudioCodec.codecTable.Add(audioCodec.Codec, audioCodec);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000699B File Offset: 0x00004B9B
		private AudioCodec(AudioCodecEnum codec, string mimeType, bool supportsACM, bool supportsWideband, int samplingRate, string extension, string extensionForProtectedContent)
		{
			this.Codec = codec;
			this.MimeType = mimeType;
			this.SupportsACM = supportsACM;
			this.SupportsWideband = supportsWideband;
			this.SamplingRate = samplingRate;
			this.FileExtension = extension;
			this.FileExtensionForProtectedContent = extensionForProtectedContent;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000069D8 File Offset: 0x00004BD8
		// (set) Token: 0x0600019F RID: 415 RVA: 0x000069E0 File Offset: 0x00004BE0
		internal AudioCodecEnum Codec { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000069E9 File Offset: 0x00004BE9
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x000069F1 File Offset: 0x00004BF1
		internal string FileExtension { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000069FA File Offset: 0x00004BFA
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00006A02 File Offset: 0x00004C02
		internal string FileExtensionForProtectedContent { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006A0B File Offset: 0x00004C0B
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00006A13 File Offset: 0x00004C13
		internal string MimeType { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006A1C File Offset: 0x00004C1C
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00006A24 File Offset: 0x00004C24
		internal bool SupportsACM { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006A2D File Offset: 0x00004C2D
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00006A35 File Offset: 0x00004C35
		internal bool SupportsWideband { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006A3E File Offset: 0x00004C3E
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00006A46 File Offset: 0x00004C46
		internal int SamplingRate { get; private set; }

		// Token: 0x060001AC RID: 428 RVA: 0x00006A50 File Offset: 0x00004C50
		internal static AudioCodec GetAudioCodec(AudioCodecEnum codec)
		{
			AudioCodec result;
			if (!AudioCodec.codecTable.TryGetValue(codec, out result))
			{
				throw new ArgumentException("codec");
			}
			return result;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006A78 File Offset: 0x00004C78
		internal static bool IsACMSupported(AudioCodecEnum codec)
		{
			return AudioCodec.GetAudioCodec(codec).SupportsACM;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006A85 File Offset: 0x00004C85
		internal static bool IsWidebandSupported(AudioCodecEnum codec)
		{
			return AudioCodec.GetAudioCodec(codec).SupportsWideband;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006A92 File Offset: 0x00004C92
		internal static string GetMimeType(AudioCodecEnum codec)
		{
			return AudioCodec.GetAudioCodec(codec).MimeType;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006A9F File Offset: 0x00004C9F
		internal static string GetFileExtension(AudioCodecEnum codec)
		{
			return AudioCodec.GetAudioCodec(codec).FileExtension;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006AAC File Offset: 0x00004CAC
		internal static string GetFileExtensionForProtectedContent(AudioCodecEnum codec)
		{
			return AudioCodec.GetAudioCodec(codec).FileExtensionForProtectedContent;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006ABC File Offset: 0x00004CBC
		internal static bool ShouldResample(PcmReader inputReader, AudioCodecEnum codec, out WaveFormat resampleFormat)
		{
			resampleFormat = null;
			AudioCodec audioCodec = AudioCodec.GetAudioCodec(codec);
			if (!audioCodec.SupportsACM)
			{
				throw new ArgumentException("codec");
			}
			if (inputReader.WaveFormat.SamplesPerSec != audioCodec.SamplingRate)
			{
				resampleFormat = ((audioCodec.SamplingRate == WaveFormat.Pcm16WaveFormat.SamplesPerSec) ? WaveFormat.Pcm16WaveFormat : WaveFormat.Pcm8WaveFormat);
			}
			return resampleFormat != null;
		}

		// Token: 0x0400007A RID: 122
		private static Dictionary<AudioCodecEnum, AudioCodec> codecTable = new Dictionary<AudioCodecEnum, AudioCodec>();
	}
}
