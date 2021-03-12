using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000625 RID: 1573
	internal class WmaReader : IDisposable
	{
		// Token: 0x06001C65 RID: 7269 RVA: 0x00033BB4 File Offset: 0x00031DB4
		internal WmaReader(string fileName)
		{
			this.reader = WindowsMediaNativeMethods.CreateSyncReader();
			try
			{
				this.reader.Open(fileName);
				this.Initialize();
			}
			catch
			{
				try
				{
					this.reader.Close();
				}
				finally
				{
					Marshal.ReleaseComObject(this.reader);
					this.reader = null;
				}
				throw;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00033C30 File Offset: 0x00031E30
		internal WaveFormat Format
		{
			get
			{
				return new WaveFormat(this.waveFormat.SamplesPerSec, (int)this.waveFormat.BitsPerSample, (int)this.waveFormat.Channels);
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00033C58 File Offset: 0x00031E58
		internal int SampleSize
		{
			get
			{
				return (int)this.sampleSize;
			}
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00033C60 File Offset: 0x00031E60
		public void Dispose()
		{
			if (this.reader != null)
			{
				try
				{
					this.reader.Close();
				}
				finally
				{
					Marshal.ReleaseComObject(this.reader);
					this.reader = null;
				}
			}
			if (this.bufferReader != null)
			{
				this.bufferReader.Dispose();
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00033CBC File Offset: 0x00031EBC
		internal int Read(byte[] buffer, int numBytes)
		{
			int i = 0;
			if (this.reader != null)
			{
				if (this.fileSize > 0L && this.fileSize - this.position < (long)numBytes)
				{
					numBytes = (int)(this.fileSize - this.position);
				}
				if (this.bufferReader != null)
				{
					i += this.bufferReader.Read(buffer, 0, numBytes);
				}
				while (i < numBytes)
				{
					INSSBuffer inssBuffer = null;
					ulong num = 0UL;
					ulong num2 = 0UL;
					uint num3 = 0U;
					try
					{
						this.reader.GetNextSample(this.outputStream, out inssBuffer, out num, out num2, out num3, out this.outputNumber, out this.outputStream);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode == -1072886833)
						{
							break;
						}
						throw;
					}
					this.bufferReader = new WindowsMediaBuffer(inssBuffer);
					inssBuffer = null;
					i += this.bufferReader.Read(buffer, i, numBytes - i);
				}
				if (this.bufferReader != null && this.bufferReader.Position >= this.bufferReader.Length)
				{
					this.bufferReader.Dispose();
					this.bufferReader = null;
				}
				this.position += (long)i;
				return i;
			}
			return i;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00033DDC File Offset: 0x00031FDC
		private void Initialize()
		{
			IWMOutputMediaProps iwmoutputMediaProps = null;
			uint num = 0U;
			this.reader.GetOutputCount(out num);
			if (num != 1U)
			{
				throw new InvalidWmaFormatException();
			}
			this.reader.GetOutputFormat(0U, 0U, out iwmoutputMediaProps);
			uint cb = 0U;
			iwmoutputMediaProps.GetMediaType(IntPtr.Zero, ref cb);
			IntPtr intPtr = Marshal.AllocCoTaskMem((int)cb);
			try
			{
				iwmoutputMediaProps.GetMediaType(intPtr, ref cb);
				WindowsMediaNativeMethods.WM_MEDIA_TYPE wm_MEDIA_TYPE = (WindowsMediaNativeMethods.WM_MEDIA_TYPE)Marshal.PtrToStructure(intPtr, typeof(WindowsMediaNativeMethods.WM_MEDIA_TYPE));
				if (!(wm_MEDIA_TYPE.majortype == WindowsMediaNativeMethods.MediaTypes.WMMEDIATYPE_Audio) || !(wm_MEDIA_TYPE.formattype == WindowsMediaNativeMethods.MediaTypes.WMFORMAT_WaveFormatEx) || wm_MEDIA_TYPE.cbFormat < 16U)
				{
					throw new InvalidWmaFormatException();
				}
				this.sampleSize = wm_MEDIA_TYPE.lSampleSize;
				this.waveFormat = new WaveFormat(8000, 16, 1);
				Marshal.PtrToStructure(wm_MEDIA_TYPE.pbFormat, this.waveFormat);
			}
			finally
			{
				Marshal.FreeCoTaskMem(intPtr);
				Marshal.ReleaseComObject(iwmoutputMediaProps);
				iwmoutputMediaProps = null;
			}
			this.fileSize = (long)this.GetFileSize();
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00033EE4 File Offset: 0x000320E4
		private ulong GetFileSize()
		{
			IWMHeaderInfo iwmheaderInfo = this.reader as IWMHeaderInfo;
			ushort num = 0;
			ushort num2 = 8;
			WindowsMediaNativeMethods.WMT_ATTR_DATATYPE wmt_ATTR_DATATYPE = WindowsMediaNativeMethods.WMT_ATTR_DATATYPE.WMT_TYPE_QWORD;
			ulong num3 = 0UL;
			try
			{
				ulong num4 = 0UL;
				iwmheaderInfo.GetAttributeByName(ref num, "Duration", out wmt_ATTR_DATATYPE, ref num4, ref num2);
				num3 = num4 * (ulong)((long)this.waveFormat.AvgBytesPerSec) / 10000000UL;
				num3 -= num3 % (ulong)((long)this.waveFormat.BlockAlign);
			}
			finally
			{
			}
			return num3;
		}

		// Token: 0x04001D02 RID: 7426
		private const ulong SecondsToNSFactor = 10000000UL;

		// Token: 0x04001D03 RID: 7427
		private const string Duration = "Duration";

		// Token: 0x04001D04 RID: 7428
		private const int NsENoMoreSamples = -1072886833;

		// Token: 0x04001D05 RID: 7429
		private IWMSyncReader reader;

		// Token: 0x04001D06 RID: 7430
		private WaveFormat waveFormat;

		// Token: 0x04001D07 RID: 7431
		private WindowsMediaBuffer bufferReader;

		// Token: 0x04001D08 RID: 7432
		private long position;

		// Token: 0x04001D09 RID: 7433
		private long fileSize = -1L;

		// Token: 0x04001D0A RID: 7434
		private uint sampleSize;

		// Token: 0x04001D0B RID: 7435
		private ushort outputStream;

		// Token: 0x04001D0C RID: 7436
		private uint outputNumber;
	}
}
