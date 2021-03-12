using System;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200060E RID: 1550
	internal abstract class CubicResampler
	{
		// Token: 0x06001BCD RID: 7117 RVA: 0x00032572 File Offset: 0x00030772
		internal static bool CanResample(PcmReader pcmReader, PcmWriter pcmWriter)
		{
			return pcmWriter.WaveFormat.SamplesPerSec == 2 * pcmReader.WaveFormat.SamplesPerSec;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0003258E File Offset: 0x0003078E
		internal static bool TryResample(PcmReader pcmReader, PcmWriter pcmWriter)
		{
			if (CubicResampler.CanResample(pcmReader, pcmWriter))
			{
				CubicResampler.Resample(pcmReader, pcmWriter);
				return true;
			}
			return false;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000325A4 File Offset: 0x000307A4
		internal static void Resample(PcmReader pcmReader, PcmWriter pcmWriter)
		{
			if (!CubicResampler.CanResample(pcmReader, pcmWriter))
			{
				throw new InvalidOperationException();
			}
			byte[] array = new byte[65536];
			byte[] buffer = new byte[131072];
			double num2;
			double y;
			double num = y = (num2 = 0.0);
			for (;;)
			{
				int num3 = pcmReader.Read(array, array.Length);
				if (num3 <= 0)
				{
					break;
				}
				int i = 0;
				int num4 = 0;
				while (i < num3)
				{
					double num5 = (double)CubicResampler.ReadPcmSample(array, i);
					i += 2;
					double sample = CubicResampler.CubicInterpolate(y, num, num2, num5, 0.5);
					CubicResampler.WritePcmSample(buffer, num4, sample);
					num4 += 2;
					CubicResampler.WritePcmSample(buffer, num4, num2);
					num4 += 2;
					y = num;
					num = num2;
					num2 = num5;
				}
				pcmWriter.Write(buffer, num4);
			}
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00032664 File Offset: 0x00030864
		private static double CubicInterpolate(double y0, double y1, double y2, double y3, double x)
		{
			double num = x * x;
			double num2 = y3 - y2 - y0 + y1;
			double num3 = y0 - y1 - num2;
			double num4 = y2 - y0;
			return num2 * x * num + num3 * num + num4 * x + y1;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x000326A0 File Offset: 0x000308A0
		private static void WritePcmSample(byte[] buffer, int position, double sample)
		{
			short num;
			if (sample > 32767.0)
			{
				num = short.MaxValue;
			}
			else if (sample < -32768.0)
			{
				num = short.MinValue;
			}
			else
			{
				num = (short)sample;
			}
			buffer[position] = (byte)(num & 255);
			buffer[position + 1] = (byte)(num >> 8);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000326EC File Offset: 0x000308EC
		private static short ReadPcmSample(byte[] buffer, int position)
		{
			return (short)((int)buffer[position] | (int)buffer[position + 1] << 8);
		}

		// Token: 0x04001CBD RID: 7357
		private const int CbPcmSrcBuffer = 65536;
	}
}
