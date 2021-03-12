using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000622 RID: 1570
	internal abstract class WmaWriter : IDisposable
	{
		// Token: 0x06001C4F RID: 7247 RVA: 0x00033858 File Offset: 0x00031A58
		protected WmaWriter(string outputFile, WaveFormat waveFormat, WmaCodec codec)
		{
			this.Codec = codec;
			WmaWriter.profileManager.LoadProfileByData(this.ProfileString, out this.profile);
			this.writer = WindowsMediaNativeMethods.CreateWriter();
			this.writer.SetProfile(this.profile);
			this.writer.SetOutputFilename(outputFile);
			this.waveFormat = waveFormat;
			this.filePath = outputFile;
			this.SetInputProperties();
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x000338C4 File Offset: 0x00031AC4
		internal string ProfileString
		{
			get
			{
				switch (this.Codec)
				{
				case WmaCodec.Wma9Voice:
					return this.Wma9VoiceProfileString;
				case WmaCodec.Pcm:
					return this.WmaPcmProfileString;
				default:
					throw new NotImplementedException();
				}
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x000338FB File Offset: 0x00031AFB
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x00033903 File Offset: 0x00031B03
		internal WmaCodec Codec { get; private set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x0003390C File Offset: 0x00031B0C
		internal int BufferSize
		{
			get
			{
				int num = (this.Codec == WmaCodec.Wma9Voice) ? (5 * this.waveFormat.AvgBytesPerSec) : (this.waveFormat.AvgBytesPerSec / 2);
				return num + num % (int)this.waveFormat.BlockAlign;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x0003394F File Offset: 0x00031B4F
		internal string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001C55 RID: 7253
		protected abstract string Wma9VoiceProfileString { get; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001C56 RID: 7254
		protected abstract string WmaPcmProfileString { get; }

		// Token: 0x06001C57 RID: 7255 RVA: 0x00033958 File Offset: 0x00031B58
		public void Dispose()
		{
			if (this.profile != null)
			{
				Marshal.ReleaseComObject(this.profile);
				this.profile = null;
			}
			if (this.writer != null)
			{
				try
				{
					this.writer.EndWriting();
				}
				finally
				{
					Marshal.ReleaseComObject(this.writer);
					this.writer = null;
				}
			}
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000339BC File Offset: 0x00031BBC
		internal static WmaWriter Create(string outputFile, WaveFormat waveFormat)
		{
			return WmaWriter.Create(outputFile, waveFormat, WmaCodec.Wma9Voice);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000339C6 File Offset: 0x00031BC6
		internal static WmaWriter Create(string outputFile, WaveFormat waveFormat, WmaCodec codec)
		{
			if (waveFormat.SamplesPerSec == 8000)
			{
				return new Wma8Writer(outputFile, WaveFormat.Pcm8WaveFormat, codec);
			}
			if (waveFormat.SamplesPerSec == 16000)
			{
				return new Wma16Writer(outputFile, WaveFormat.Pcm16WaveFormat, codec);
			}
			throw new UnsupportedAudioFormat(outputFile);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00033A04 File Offset: 0x00031C04
		internal void Write(byte[] buffer, int count)
		{
			INSSBuffer inssbuffer;
			this.writer.AllocateSample((uint)count, out inssbuffer);
			using (WindowsMediaBuffer windowsMediaBuffer = new WindowsMediaBuffer(inssbuffer))
			{
				windowsMediaBuffer.Write(buffer, count);
				this.writer.WriteSample(0U, this.sampleTime * 10000UL, 0U, inssbuffer);
				this.sampleTime += (ulong)((long)count * 1000L / (long)this.waveFormat.AvgBytesPerSec);
			}
			inssbuffer = null;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00033A8C File Offset: 0x00031C8C
		private void SetInputProperties()
		{
			IWMInputMediaProps iwminputMediaProps;
			this.writer.GetInputProps(0U, out iwminputMediaProps);
			Guid guid;
			iwminputMediaProps.GetType(out guid);
			WindowsMediaNativeMethods.WM_MEDIA_TYPE wm_MEDIA_TYPE;
			wm_MEDIA_TYPE.majortype = WindowsMediaNativeMethods.MediaTypes.WMMEDIATYPE_Audio;
			wm_MEDIA_TYPE.subtype = WindowsMediaNativeMethods.MediaTypes.WMMEDIASUBTYPE_PCM;
			wm_MEDIA_TYPE.bFixedSizeSamples = true;
			wm_MEDIA_TYPE.bTemporalCompression = false;
			wm_MEDIA_TYPE.lSampleSize = (uint)this.waveFormat.BlockAlign;
			wm_MEDIA_TYPE.formattype = WindowsMediaNativeMethods.MediaTypes.WMFORMAT_WaveFormatEx;
			wm_MEDIA_TYPE.pUnk = IntPtr.Zero;
			wm_MEDIA_TYPE.cbFormat = 16U;
			GCHandle gchandle = GCHandle.Alloc(this.waveFormat, GCHandleType.Pinned);
			try
			{
				wm_MEDIA_TYPE.pbFormat = gchandle.AddrOfPinnedObject();
				iwminputMediaProps.SetMediaType(ref wm_MEDIA_TYPE);
			}
			finally
			{
				gchandle.Free();
			}
			this.writer.SetInputProps(0U, iwminputMediaProps);
			this.writer.BeginWriting();
		}

		// Token: 0x04001CF8 RID: 7416
		private const int NumBuffersPerByteRate = 10;

		// Token: 0x04001CF9 RID: 7417
		private const int MilliSecondsTo100NSFactor = 10000;

		// Token: 0x04001CFA RID: 7418
		private const int SecondsToMilliSecondsFactor = 1000;

		// Token: 0x04001CFB RID: 7419
		private static IWMProfileManager profileManager = WindowsMediaNativeMethods.CreateProfileManager();

		// Token: 0x04001CFC RID: 7420
		private IWMWriter writer;

		// Token: 0x04001CFD RID: 7421
		private IWMProfile profile;

		// Token: 0x04001CFE RID: 7422
		private WaveFormat waveFormat;

		// Token: 0x04001CFF RID: 7423
		private ulong sampleTime;

		// Token: 0x04001D00 RID: 7424
		private string filePath;
	}
}
