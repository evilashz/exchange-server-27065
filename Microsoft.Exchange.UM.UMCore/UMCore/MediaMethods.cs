using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000173 RID: 371
	internal class MediaMethods
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002DF28 File Offset: 0x0002C128
		internal static void InitializeACM()
		{
			if (MediaMethods.mpegLayer3DriverRegId != IntPtr.Zero)
			{
				throw new InvalidOperationException("InitializeACM called multiple times");
			}
			IntPtr intPtr = ACMNativeMethods.OpenDriver("l3codecp.acm", null, 0);
			if (intPtr == IntPtr.Zero)
			{
				throw MediaMethods.CreateAcmException(Marshal.GetLastWin32Error());
			}
			IntPtr driverModuleHandle = ACMNativeMethods.GetDriverModuleHandle(intPtr);
			if (driverModuleHandle == IntPtr.Zero)
			{
				throw MediaMethods.CreateAcmException(Marshal.GetLastWin32Error());
			}
			IntPtr procAddress = ACMNativeMethods.GetProcAddress(driverModuleHandle, "DriverProc");
			if (procAddress == IntPtr.Zero)
			{
				throw MediaMethods.CreateAcmException(Marshal.GetLastWin32Error());
			}
			int num = ACMNativeMethods.AcmDriverAdd(out MediaMethods.mpegLayer3DriverRegId, driverModuleHandle, procAddress, 0, 3);
			if (num != 0)
			{
				throw MediaMethods.CreateAcmException(num);
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
		internal static void ConvertWmaToWav(string wmaFile, string wavFile)
		{
			try
			{
				using (WmaReader wmaReader = new WmaReader(wmaFile))
				{
					using (PcmWriter pcmWriter = new PcmWriter(wavFile, wmaReader.Format))
					{
						byte[] array = new byte[wmaReader.SampleSize * 2];
						int count;
						while ((count = wmaReader.Read(array, array.Length)) > 0)
						{
							pcmWriter.Write(array, count);
						}
					}
				}
			}
			catch (InvalidWmaFormatException innerException)
			{
				throw new WmaToWavConversionException(wmaFile, wavFile, innerException);
			}
			catch (COMException innerException2)
			{
				throw new WmaToWavConversionException(wmaFile, wavFile, innerException2);
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002E084 File Offset: 0x0002C284
		internal static ITempFile ConvertWavToWma(PcmReader pcmReader)
		{
			ITempFile tempFile = TempFileFactory.CreateTempWmaFile();
			ITempFile result;
			using (WmaWriter wmaWriter = WmaWriter.Create(tempFile.FilePath, pcmReader.WaveFormat))
			{
				MediaMethods.ConvertWavToWma(pcmReader, wmaWriter);
				result = tempFile;
			}
			return result;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002E0D0 File Offset: 0x0002C2D0
		internal static void ConvertWavToWma(PcmReader pcmReader, WmaWriter wmaWriter)
		{
			try
			{
				byte[] array = new byte[wmaWriter.BufferSize];
				double num = 0.0;
				int num2;
				while ((num2 = pcmReader.Read(array, array.Length)) > 0)
				{
					AudioNormalizer.ProcessBuffer(array, num2, GlobCfg.NoiseFloorLevel, GlobCfg.NormalizationLevel, ref num);
					wmaWriter.Write(array, num2);
				}
			}
			catch (InvalidWaveFormatException innerException)
			{
				throw new WavToWmaConversionException(pcmReader.FilePath, wmaWriter.FilePath, innerException);
			}
			catch (COMException innerException2)
			{
				throw new WavToWmaConversionException(pcmReader.FilePath, wmaWriter.FilePath, innerException2);
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002E168 File Offset: 0x0002C368
		internal static ITempWavFile NormalizeAudio(string pcmSrc, double noiseFloorLevelDB, double normalizationLevelDB)
		{
			double noiseFloorLevel = AudioNormalizer.ConvertDbToEnergyRms(noiseFloorLevelDB);
			double normalizationLevel = AudioNormalizer.ConvertDbToEnergyRms(normalizationLevelDB);
			ITempWavFile result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				ITempWavFile tempWavFile = TempFileFactory.CreateTempWavFile();
				disposeGuard.Add<ITempWavFile>(tempWavFile);
				using (PcmReader pcmReader = new PcmReader(pcmSrc))
				{
					using (PcmWriter pcmWriter = new PcmWriter(tempWavFile.FilePath, pcmReader.WaveFormat))
					{
						int num = pcmWriter.WaveFormat.AvgBytesPerSec * 5;
						byte[] array = new byte[num];
						double num2 = 0.0;
						int num3;
						while ((num3 = pcmReader.Read(array, array.Length)) > 0)
						{
							AudioNormalizer.ProcessBuffer(array, num3, noiseFloorLevel, normalizationLevel, ref num2);
							pcmWriter.Write(array, num3);
						}
					}
				}
				disposeGuard.Success();
				result = tempWavFile;
			}
			return result;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002E268 File Offset: 0x0002C468
		internal static void RemoveAudioFromEnd(string waveFileName, TimeSpan removeTimeSpan)
		{
			int waveDataLength;
			long waveDataPosition;
			int num;
			using (WaveReader waveReader = new PcmReader(waveFileName))
			{
				waveDataLength = waveReader.WaveDataLength;
				waveDataPosition = waveReader.WaveDataPosition;
				num = (int)removeTimeSpan.TotalMilliseconds * waveReader.WaveFormat.AvgBytesPerSec / 1000;
				if (num > waveDataLength)
				{
					num = waveDataLength;
				}
			}
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(waveFileName)))
			{
				Stream baseStream = binaryWriter.BaseStream;
				baseStream.Seek(waveDataPosition - 4L, SeekOrigin.Begin);
				binaryWriter.Write(waveDataLength - num);
				baseStream.SetLength(baseStream.Length - (long)num);
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002E320 File Offset: 0x0002C520
		internal static ITempWavFile Pcm16ToPcm8(PcmReader pcm16Reader, bool fnormalizeRMS)
		{
			return MediaMethods.PcmResample(pcm16Reader, WaveFormat.Pcm8WaveFormat, fnormalizeRMS);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002E330 File Offset: 0x0002C530
		internal static ITempWavFile PcmResample(PcmReader pcmReader, WaveFormat targetFormat, bool fnormalizeRMS)
		{
			ITempWavFile tempWavFile = TempFileFactory.CreateTempWavFile();
			using (PcmWriter pcmWriter = new PcmWriter(tempWavFile.FilePath, targetFormat))
			{
				if (!CubicResampler.TryResample(pcmReader, pcmWriter))
				{
					MediaMethods.AcmConvert(pcmReader, pcmWriter, 65536, fnormalizeRMS);
				}
			}
			return tempWavFile;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002E384 File Offset: 0x0002C584
		internal static ITempWavFile ToPcm(ITempFile src)
		{
			return MediaMethods.ToPcm(src.FilePath);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002E394 File Offset: 0x0002C594
		internal static ITempWavFile ToPcm(string src)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			ITempWavFile tempWavFile = TempFileFactory.CreateTempWavFile();
			SoundReader soundReader = null;
			ITempWavFile result;
			try
			{
				if (AudioFile.IsWma(src))
				{
					MediaMethods.ConvertWmaToWav(src, tempWavFile.FilePath);
				}
				else
				{
					if (Mp3Reader.TryCreate(src, out soundReader))
					{
						using (PcmWriter pcmWriter = new PcmWriter(tempWavFile.FilePath, WaveFormat.Pcm8WaveFormat))
						{
							MediaMethods.AcmConvert(soundReader, pcmWriter, (int)((MPEGLAYER3WAVEFORMAT)soundReader.WaveFormat).BlockSize, false);
							goto IL_FD;
						}
					}
					if (GsmReader.TryCreate(src, out soundReader))
					{
						using (PcmWriter pcmWriter2 = new PcmWriter(tempWavFile.FilePath, WaveFormat.Pcm8WaveFormat))
						{
							MediaMethods.AcmConvert(soundReader, pcmWriter2, 6500, false);
							goto IL_FD;
						}
					}
					if (G711Reader.TryCreate(src, out soundReader))
					{
						using (PcmWriter pcmWriter3 = new PcmWriter(tempWavFile.FilePath, WaveFormat.Pcm8WaveFormat))
						{
							MediaMethods.AcmConvert(soundReader, pcmWriter3, 32768, false);
							goto IL_FD;
						}
					}
					if (!PcmReader.TryCreate(src, out soundReader))
					{
						throw new UnsupportedAudioFormat(src);
					}
					File.Copy(src, tempWavFile.FilePath, true);
				}
				IL_FD:
				result = tempWavFile;
			}
			finally
			{
				if (soundReader != null)
				{
					soundReader.Dispose();
					soundReader = null;
				}
			}
			return result;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002E4E8 File Offset: 0x0002C6E8
		internal static ITempFile FromPcm(ITempFile pcmSrc, AudioCodecEnum codec)
		{
			return MediaMethods.FromPcm(pcmSrc.FilePath, codec);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002E4F8 File Offset: 0x0002C6F8
		internal static ITempFile FromPcm(string pcmSrc, AudioCodecEnum codec)
		{
			if (pcmSrc == null)
			{
				throw new ArgumentNullException("src");
			}
			ITempFile result = null;
			using (PcmReader pcmReader = new PcmReader(pcmSrc))
			{
				if (codec == AudioCodecEnum.Wma)
				{
					result = MediaMethods.ConvertWavToWma(pcmReader);
				}
				else
				{
					result = MediaMethods.AcmConvertFromPcm(pcmReader, codec);
				}
			}
			return result;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002E550 File Offset: 0x0002C750
		internal static void Append(SoundReader src1, SoundReader src2, SoundWriter dst)
		{
			ValidateArgument.NotNull(src1, "src1");
			ValidateArgument.NotNull(src2, "src2");
			ValidateArgument.NotNull(dst, "dst");
			byte[] array = new byte[4096];
			int count;
			while ((count = src1.Read(array, array.Length)) != 0)
			{
				dst.Write(array, count);
			}
			while ((count = src2.Read(array, array.Length)) != 0)
			{
				dst.Write(array, count);
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002E5BC File Offset: 0x0002C7BC
		private static ITempFile AcmConvertFromPcm(PcmReader pcmReader, AudioCodecEnum codec)
		{
			if (!AudioCodec.IsACMSupported(codec))
			{
				throw new ArgumentException("codec");
			}
			if (pcmReader.WaveFormat.SamplesPerSec != WaveFormat.Pcm8WaveFormat.SamplesPerSec && pcmReader.WaveFormat.SamplesPerSec != WaveFormat.Pcm16WaveFormat.SamplesPerSec)
			{
				throw new ArgumentException("SamplesPerSec");
			}
			PcmReader pcmReader2 = null;
			SoundWriter soundWriter = null;
			ITempWavFile tempWavFile = null;
			ITempWavFile tempWavFile2 = null;
			ACMNativeMethods.AcmInstanceHandle acmInstanceHandle = null;
			ITempFile result;
			try
			{
				if (MediaMethods.TryCreateSoundWriter(codec, pcmReader, out soundWriter, out tempWavFile, out acmInstanceHandle))
				{
					MediaMethods.ResampleIfNeeded(codec, pcmReader, out pcmReader2, out tempWavFile2);
					MediaMethods.AcmConvert(acmInstanceHandle, pcmReader2 ?? pcmReader, soundWriter, 65536, true);
				}
				result = tempWavFile;
			}
			finally
			{
				if (pcmReader2 != null)
				{
					pcmReader2.Dispose();
					pcmReader2 = null;
				}
				if (tempWavFile2 != null)
				{
					tempWavFile2.Dispose();
					tempWavFile2 = null;
				}
				if (acmInstanceHandle != null)
				{
					acmInstanceHandle.Dispose();
					acmInstanceHandle = null;
				}
				if (soundWriter != null)
				{
					soundWriter.Dispose();
					soundWriter = null;
				}
			}
			return result;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002E698 File Offset: 0x0002C898
		private static void ResampleIfNeeded(AudioCodecEnum codec, PcmReader inputReader, out PcmReader resampleReader, out ITempWavFile resampleFile)
		{
			WaveFormat targetFormat = null;
			if (AudioCodec.ShouldResample(inputReader, codec, out targetFormat))
			{
				resampleFile = MediaMethods.PcmResample(inputReader, targetFormat, false);
				resampleReader = new PcmReader(resampleFile.FilePath);
				return;
			}
			resampleFile = null;
			resampleReader = null;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002E6D4 File Offset: 0x0002C8D4
		private static bool TryCreateSoundWriter(AudioCodecEnum codec, PcmReader inputReader, out SoundWriter soundWriter, out ITempWavFile waveFile, out ACMNativeMethods.AcmInstanceHandle driver)
		{
			switch (codec)
			{
			case AudioCodecEnum.G711:
				driver = new ACMNativeMethods.AcmInstanceHandle(IntPtr.Zero, false);
				waveFile = TempFileFactory.CreateTempWavFile();
				soundWriter = new G711Writer(waveFile.FilePath, GlobCfg.G711Format);
				goto IL_A4;
			case AudioCodecEnum.Gsm:
				driver = new ACMNativeMethods.AcmInstanceHandle(IntPtr.Zero, false);
				waveFile = TempFileFactory.CreateTempWavFile();
				soundWriter = new GsmWriter(waveFile.FilePath);
				goto IL_A4;
			case AudioCodecEnum.Mp3:
				driver = MediaMethods.OpenAcmMp3Driver();
				waveFile = TempFileFactory.CreateTempWavFile(false, ".mp3");
				soundWriter = Mp3Writer.Create(waveFile.FilePath, inputReader.WaveFormat.SamplesPerSec);
				goto IL_A4;
			}
			throw new ArgumentException("codec");
			IL_A4:
			return soundWriter != null;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002E790 File Offset: 0x0002C990
		private static void AcmConvert(SoundReader src, SoundWriter dst, int cbsrcBuffer, bool fnormalizeRMS)
		{
			using (ACMNativeMethods.AcmInstanceHandle acmInstanceHandle = new ACMNativeMethods.AcmInstanceHandle(IntPtr.Zero, false))
			{
				MediaMethods.AcmConvert(acmInstanceHandle, src, dst, cbsrcBuffer, fnormalizeRMS);
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002E7D0 File Offset: 0x0002C9D0
		private static void AcmConvert(ACMNativeMethods.AcmInstanceHandle driverInstance, SoundReader src, SoundWriter dst, int cbsrcBuffer, bool fnormalizeRMS)
		{
			int ec = 0;
			ACMNativeMethods.SafeStreamHandle safeStreamHandle = null;
			ACMNativeMethods.ACMSTREAMHEADER acmstreamheader = default(ACMNativeMethods.ACMSTREAMHEADER);
			acmstreamheader.Zero();
			byte[] array = null;
			byte[] array2 = null;
			GCHandle gchandle = default(GCHandle);
			GCHandle gchandle2 = default(GCHandle);
			try
			{
				ec = ACMNativeMethods.AcmStreamOpen(out safeStreamHandle, driverInstance, src.WaveFormat, dst.WaveFormat, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 4);
				MediaMethods.BailOnFailure(ec, src.WaveFormat, dst.WaveFormat);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "AudioCompressionStage: Acquired ACM conversion stream handle without bailing", new object[0]);
				int num = 0;
				ec = ACMNativeMethods.AcmStreamSize(safeStreamHandle, cbsrcBuffer, out num, 0);
				MediaMethods.BailOnFailure(ec, src.WaveFormat, dst.WaveFormat);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "AudioCompressionStage: Allowed source & destination buffers without bailing", new object[0]);
				array = new byte[cbsrcBuffer];
				array2 = new byte[num];
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				gchandle2 = GCHandle.Alloc(array2, GCHandleType.Pinned);
				acmstreamheader.cbStruct = Marshal.SizeOf(acmstreamheader);
				acmstreamheader.pbSrc = gchandle.AddrOfPinnedObject();
				acmstreamheader.cbSrcLength = cbsrcBuffer;
				acmstreamheader.pbDst = gchandle2.AddrOfPinnedObject();
				acmstreamheader.cbDstLength = num;
				ec = ACMNativeMethods.AcmStreamPrepareHeader(safeStreamHandle, ref acmstreamheader, 0);
				MediaMethods.BailOnFailure(ec, src.WaveFormat, dst.WaveFormat);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "AudioCompressionStage: Prepared header without bailing", new object[0]);
				double num2 = 0.0;
				acmstreamheader.cbSrcLength = 0;
				MediaMethods.FillBuffer(src, array, fnormalizeRMS, ref acmstreamheader, ref num2);
				int num3 = 0;
				int fdwConvert = 20;
				while (acmstreamheader.cbSrcLength > num3)
				{
					ec = ACMNativeMethods.AcmStreamConvert(safeStreamHandle, ref acmstreamheader, fdwConvert);
					MediaMethods.BailOnFailure(ec, src.WaveFormat, dst.WaveFormat);
					dst.Write(array2, acmstreamheader.cbDstLengthUsed);
					num3 = acmstreamheader.cbSrcLength - acmstreamheader.cbSrcLengthUsed;
					MediaMethods.FillBuffer(src, array, fnormalizeRMS, ref acmstreamheader, ref num2);
					fdwConvert = 4;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "AudioCompressionStage: Converted stream without bailing", new object[0]);
				if (acmstreamheader.cbSrcLength > 0)
				{
					ec = ACMNativeMethods.AcmStreamConvert(safeStreamHandle, ref acmstreamheader, 0);
					MediaMethods.BailOnFailure(ec, src.WaveFormat, dst.WaveFormat);
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "AudioCompressionStage: Converted leftover stream without bailing", new object[0]);
					dst.Write(array2, acmstreamheader.cbDstLengthUsed);
				}
				acmstreamheader.cbSrcLength = 0;
				ec = ACMNativeMethods.AcmStreamConvert(safeStreamHandle, ref acmstreamheader, 32);
				MediaMethods.BailOnFailure(ec, src.WaveFormat, dst.WaveFormat);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "AudioCompressionStage: Completed stream conversion without bailing", new object[0]);
				dst.Write(array2, acmstreamheader.cbDstLengthUsed);
			}
			finally
			{
				if ((acmstreamheader.fdwStatus & 131072) != 0 && safeStreamHandle != null && array != null && array2 != null)
				{
					acmstreamheader.cbSrcLength = array.Length;
					acmstreamheader.cbDstLength = array2.Length;
					ec = ACMNativeMethods.AcmStreamUnprepareHeader(safeStreamHandle, ref acmstreamheader, 0);
					if ((acmstreamheader.fdwStatus & 131072) != 0)
					{
						ec = 0;
					}
				}
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (gchandle2.IsAllocated)
				{
					gchandle2.Free();
				}
				if (safeStreamHandle != null)
				{
					safeStreamHandle.Dispose();
				}
				MediaMethods.BailOnFailure(ec, src.WaveFormat, dst.WaveFormat);
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002EB0C File Offset: 0x0002CD0C
		private static void FillBuffer(SoundReader srcReader, byte[] srcBuffer, bool fnormalizeRMS, ref ACMNativeMethods.ACMSTREAMHEADER streamHeader, ref double rmsEnergy)
		{
			int num = streamHeader.cbSrcLength - streamHeader.cbSrcLengthUsed;
			for (int i = 0; i < num; i++)
			{
				srcBuffer[i] = srcBuffer[i + streamHeader.cbSrcLengthUsed];
			}
			int num2 = srcReader.Read(srcBuffer, num, srcBuffer.Length - num);
			num += num2;
			streamHeader.cbSrcLength = num;
			if (0 < num && fnormalizeRMS)
			{
				AudioNormalizer.ProcessBuffer(srcBuffer, num, GlobCfg.NoiseFloorLevel, GlobCfg.NormalizationLevel, ref rmsEnergy);
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002EB73 File Offset: 0x0002CD73
		private static void BailOnFailure(int ec, WaveFormat src, WaveFormat dst)
		{
			MediaMethods.BailOnFailure(ec, src.FormatTag, dst.FormatTag);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002EB88 File Offset: 0x0002CD88
		private static void BailOnFailure(int ec, short sourceFormatTag, short destinationFormatTag)
		{
			if (ec != 0)
			{
				Exception ex = MediaMethods.CreateAcmException(ec);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AcmConversionFailed, null, new object[]
				{
					sourceFormatTag,
					destinationFormatTag,
					ex.Message
				});
				throw ex;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002EBD8 File Offset: 0x0002CDD8
		private static AcmException CreateAcmException(int ec)
		{
			Win32Exception ex = new Win32Exception(ec);
			return new AcmException(ex.Message, ex);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002EBF8 File Offset: 0x0002CDF8
		private static ACMNativeMethods.AcmInstanceHandle OpenAcmMp3Driver()
		{
			ACMNativeMethods.AcmInstanceHandle result = null;
			int ec = ACMNativeMethods.AcmDriverOpen(out result, MediaMethods.mpegLayer3DriverRegId, 0);
			MediaMethods.BailOnFailure(ec, 1, 85);
			return result;
		}

		// Token: 0x0400098C RID: 2444
		private const int CbGsmSrcBuffer = 6500;

		// Token: 0x0400098D RID: 2445
		private const int CbPcmSrcBuffer = 65536;

		// Token: 0x0400098E RID: 2446
		private const int CbG711SrcBuffer = 32768;

		// Token: 0x0400098F RID: 2447
		private const int EvenFactor = 2;

		// Token: 0x04000990 RID: 2448
		private static IntPtr mpegLayer3DriverRegId = IntPtr.Zero;
	}
}
