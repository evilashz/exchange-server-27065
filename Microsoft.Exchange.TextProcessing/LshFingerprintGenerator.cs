using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000014 RID: 20
	internal static class LshFingerprintGenerator
	{
		// Token: 0x060000DE RID: 222 RVA: 0x0000A8F4 File Offset: 0x00008AF4
		public static bool TryGetFingerprint(string text, out LshFingerprint fingerprint, string id = "")
		{
			if (string.IsNullOrEmpty(text))
			{
				LshFingerprint.TryCreateFingerprint(null, out fingerprint, id);
				return false;
			}
			HashSet<ulong> hashSet = new HashSet<ulong>();
			int num = 64;
			ulong[] array = new ulong[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = ulong.MaxValue;
			}
			char c = ' ';
			char c2 = ' ';
			int num2 = Math.Min(text.Length, 50000);
			if (num2 > 12)
			{
				for (int j = 0; j < num2; j++)
				{
					if (!LshFingerprintGenerator.IsBreaker(text[j]))
					{
						if (LshFingerprintGenerator.IsUrlHead(text, ref j))
						{
							int num3 = j;
							while (num3 < num2 && !LshFingerprintGenerator.IsDomainEnd(text[num3]))
							{
								num3++;
							}
							for (int k = j; k < num3; k++)
							{
								char c3 = LshFingerprintGenerator.ToUpperIfAscii(text[k]);
								ulong num4 = (ulong)c;
								num4 = (num4 << 16 | (ulong)c2);
								num4 = (num4 << 16 | (ulong)c3);
								hashSet.Add(num4);
								c = c2;
								c2 = c3;
							}
							for (j = num3; j < num2; j++)
							{
								if (LshFingerprintGenerator.IsUrlEnd(text[j]))
								{
									break;
								}
							}
						}
						else
						{
							char c3 = LshFingerprintGenerator.ToUpperIfAscii(text[j]);
							if (c3 == c2)
							{
								goto IL_161;
							}
							ulong num4 = (ulong)c;
							num4 = (num4 << 16 | (ulong)c2);
							num4 = (num4 << 16 | (ulong)c3);
							hashSet.Add(num4);
							c = c2;
							c2 = c3;
						}
						if (hashSet.Count > 8000)
						{
							IL_17D:
							foreach (ulong input in hashSet)
							{
								for (int l = 0; l < num; l++)
								{
									ulong num5 = LshFingerprintGenerator.UHash(input, LshHashSeeds.Seeds64[l], (ulong)LshHashSeeds.Seeds32[l]);
									array[l] = ((array[l] < num5) ? array[l] : num5);
								}
							}
							if (hashSet.Count > 10)
							{
								uint[] array2 = new uint[]
								{
									0U,
									0U,
									0U,
									0U,
									(uint)(hashSet.Count | 131072)
								};
								for (int m = 0; m < 4; m++)
								{
									int num6 = m * 16;
									array2[m] = 0U;
									for (int n = 0; n < 16; n++)
									{
										uint num7 = (uint)(array[num6 + n] & 3UL);
										num7 <<= 2 * n;
										array2[m] += num7;
									}
								}
								LshFingerprint.TryCreateFingerprint(array2, out fingerprint, id);
								return true;
							}
							fingerprint = LshFingerprint.GetEmptyFingerprint();
							return false;
						}
					}
					IL_161:;
				}
				goto IL_17D;
			}
			LshFingerprint.TryCreateFingerprint(null, out fingerprint, id);
			return false;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000AB90 File Offset: 0x00008D90
		private static char ToUpperIfAscii(char c)
		{
			if ('a' <= c && c <= 'z')
			{
				c &= '￟';
			}
			return c;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000ABA7 File Offset: 0x00008DA7
		private static ulong UHash(ulong input, ulong seed1, ulong seed2)
		{
			return seed1 * input + seed2 >> 32;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		private static bool IsBreaker(char c)
		{
			return c < LshFingerprintConstants.BreakerRange[0] || (c > LshFingerprintConstants.BreakerRange[1] && c < LshFingerprintConstants.BreakerRange[2]) || (c > LshFingerprintConstants.BreakerRange[3] && c < LshFingerprintConstants.BreakerRange[4]) || (c > LshFingerprintConstants.BreakerRange[5] && c < LshFingerprintConstants.BreakerRange[6]) || (c > LshFingerprintConstants.BreakerRange[7] && c < LshFingerprintConstants.BreakerRange[8]) || (c > LshFingerprintConstants.BreakerRange[9] && c < LshFingerprintConstants.BreakerRange[10]) || (c > LshFingerprintConstants.BreakerRange[11] && c < LshFingerprintConstants.BreakerRange[12]);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000AC4C File Offset: 0x00008E4C
		private static bool IsUrlHead(string text, ref int i)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException("The value is set to null or empty", text);
			}
			int num = i;
			int length = text.Length;
			int num2 = LshFingerprintConstants.UrlStart.Length;
			if (text[i] == LshFingerprintConstants.UrlStart[0] && i < length - num2)
			{
				i++;
				if (text[i] == LshFingerprintConstants.UrlStart[1])
				{
					i++;
					if (text[i] == LshFingerprintConstants.UrlStart[2])
					{
						i++;
						if (text[i] == LshFingerprintConstants.UrlStart[3])
						{
							i++;
							if (text[i] == LshFingerprintConstants.UrlStart[4])
							{
								i++;
							}
							if (text[i] == LshFingerprintConstants.UrlStart[5])
							{
								i++;
								if (text[i] == LshFingerprintConstants.UrlStart[6])
								{
									i++;
									if (text[i] == LshFingerprintConstants.UrlStart[7])
									{
										i++;
										return true;
									}
								}
							}
						}
					}
				}
			}
			i = num;
			return false;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000AD4D File Offset: 0x00008F4D
		private static bool IsDomainEnd(char c)
		{
			return c == LshFingerprintConstants.DomanEnd[0] || c == LshFingerprintConstants.DomanEnd[1] || c == LshFingerprintConstants.DomanEnd[2] || c == LshFingerprintConstants.DomanEnd[3];
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000AD7A File Offset: 0x00008F7A
		private static bool IsUrlEnd(char c)
		{
			return c == LshFingerprintConstants.UrlEnd[0] || c == LshFingerprintConstants.UrlEnd[1];
		}
	}
}
