using System;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200060C RID: 1548
	internal abstract class AudioNormalizer
	{
		// Token: 0x06001BC7 RID: 7111 RVA: 0x000323E7 File Offset: 0x000305E7
		internal static double ConvertDbToEnergyRms(double rmsDB)
		{
			return Math.Pow(10.0, rmsDB / 20.0) * (1.0 / Math.Sqrt(2.0));
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0003241C File Offset: 0x0003061C
		internal static void ProcessBuffer(byte[] rawSpeechBytes, int numofBytes, double noiseFloorLevel, double normalizationLevel, ref double lastRmsValue)
		{
			if (1 == (numofBytes & 1))
			{
				throw new ArgumentException("numofBytes is not even");
			}
			if (numofBytes <= 0 || rawSpeechBytes == null)
			{
				throw new ArgumentException("numofBytes <= 0 or rawSpeechBytes == null");
			}
			double num = AudioNormalizer.CalcEnergyRms(rawSpeechBytes, numofBytes);
			if (num < lastRmsValue)
			{
				num = lastRmsValue;
			}
			else
			{
				lastRmsValue = num;
			}
			if (num < noiseFloorLevel)
			{
				return;
			}
			double num2 = normalizationLevel / num;
			if (num2 > 10.0)
			{
				num2 = 10.0;
			}
			AudioNormalizer.AmplifySignal(rawSpeechBytes, numofBytes, num2);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0003248C File Offset: 0x0003068C
		internal static double CalcEnergyRms(byte[] rawSpeechBytes, int numofBytes)
		{
			double num = 0.0;
			int num2 = numofBytes / 2;
			for (int i = 0; i < numofBytes; i += 2)
			{
				double num3 = (double)((short)((int)rawSpeechBytes[i] | (int)rawSpeechBytes[i + 1] << 8)) / 32767.0;
				num += num3 * num3;
			}
			num /= (double)num2;
			return Math.Sqrt(num);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x000324DC File Offset: 0x000306DC
		private static void AmplifySignal(byte[] rawSpeechBytes, int numofBytes, double ampFactor)
		{
			int num = numofBytes / 2;
			for (int i = 0; i < numofBytes; i += 2)
			{
				double num2 = (double)((short)((int)rawSpeechBytes[i] | (int)rawSpeechBytes[i + 1] << 8));
				num2 *= ampFactor;
				if (num2 > 32767.0)
				{
					num2 = 32767.0;
				}
				else if (num2 < -32768.0)
				{
					num2 = -32768.0;
				}
				short num3 = (short)num2;
				rawSpeechBytes[i] = (byte)(num3 & 255);
				rawSpeechBytes[i + 1] = (byte)(num3 >> 8 & 255);
			}
		}

		// Token: 0x04001CB2 RID: 7346
		private const double MaxAmpFactor = 10.0;
	}
}
