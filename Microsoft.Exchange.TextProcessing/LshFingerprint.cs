using System;
using System.Globalization;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000012 RID: 18
	internal class LshFingerprint
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000A435 File Offset: 0x00008635
		private LshFingerprint(uint[] fingerprintData, string id = "")
		{
			this.Identifier = id;
			this.FingerprintData = fingerprintData;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000A44B File Offset: 0x0000864B
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000A453 File Offset: 0x00008653
		public string Identifier { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000A45C File Offset: 0x0000865C
		public short Version
		{
			get
			{
				if (this.FingerprintData != null)
				{
					return (short)(this.FingerprintData[4] >> 16);
				}
				return 0;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000A474 File Offset: 0x00008674
		public short ShingleCount
		{
			get
			{
				if (this.FingerprintData != null)
				{
					return (short)(this.FingerprintData[4] & 65535U);
				}
				return 0;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000A48F File Offset: 0x0000868F
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000A497 File Offset: 0x00008697
		public uint[] FingerprintData { get; private set; }

		// Token: 0x17000044 RID: 68
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000A4A0 File Offset: 0x000086A0
		internal string EncodedFingerprintData
		{
			set
			{
				this.encodedFingerprintData = value;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000A4A9 File Offset: 0x000086A9
		public static bool ShingleCountClose(LshFingerprint fingerprint, LshFingerprint otherFingerprint)
		{
			return fingerprint != null && otherFingerprint != null && fingerprint.ShingleCount != 0 && (double)Math.Abs((int)(fingerprint.ShingleCount - otherFingerprint.ShingleCount)) < 0.2 * (double)fingerprint.ShingleCount;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public static void ComputeSimilarity(LshFingerprint fingerprint, LshFingerprint otherFingerprint, out int similarityNumerator, out int similarityDenorminator, bool oneBit = false, bool forContainment = false)
		{
			similarityNumerator = 0;
			similarityDenorminator = (oneBit ? 32 : 48);
			if (LshFingerprint.NotQualifiedFingerprint(fingerprint) || LshFingerprint.NotQualifiedFingerprint(otherFingerprint))
			{
				return;
			}
			if (!forContainment && !LshFingerprint.ShingleCountClose(fingerprint, otherFingerprint))
			{
				similarityNumerator = 0;
				return;
			}
			uint[] fingerprintData = fingerprint.FingerprintData;
			uint[] fingerprintData2 = otherFingerprint.FingerprintData;
			uint num = oneBit ? 1U : 3U;
			for (int i = 0; i < 4; i++)
			{
				uint num2 = fingerprintData[i];
				uint num3 = fingerprintData2[i];
				for (int j = 0; j < 16; j++)
				{
					uint num4 = num2 & num;
					uint num5 = num3 & num;
					if (num4 == num5)
					{
						similarityNumerator++;
					}
					num2 >>= 2;
					num3 >>= 2;
				}
			}
			similarityNumerator = Math.Max(similarityNumerator - (oneBit ? 32 : 16), 0);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000A59C File Offset: 0x0000879C
		public static bool AcceptSimilar(LshFingerprint fingerprint, LshFingerprint otherFingerprint, bool oneBit = false)
		{
			if (LshFingerprint.NotQualifiedFingerprint(fingerprint) || LshFingerprint.NotQualifiedFingerprint(otherFingerprint))
			{
				return false;
			}
			if (!LshFingerprint.ShingleCountClose(fingerprint, otherFingerprint))
			{
				return false;
			}
			uint[] fingerprintData = fingerprint.FingerprintData;
			uint[] fingerprintData2 = otherFingerprint.FingerprintData;
			for (int i = 0; i < 4; i++)
			{
				if (oneBit)
				{
					if ((fingerprintData[i] & 1431655765U) == (fingerprintData2[i] & 1431655765U))
					{
						return true;
					}
				}
				else if (fingerprintData[i] == fingerprintData2[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A604 File Offset: 0x00008804
		public static void ComputeContainment(LshFingerprint template, LshFingerprint document, out int containmentNumerator, out int containmentDenorminator)
		{
			int num;
			int num2;
			LshFingerprint.ComputeSimilarity(template, document, out num, out num2, false, true);
			if (num == 0)
			{
				containmentNumerator = 0;
				containmentDenorminator = 1;
				return;
			}
			containmentNumerator = num * (int)(template.ShingleCount + document.ShingleCount);
			containmentDenorminator = (num + num2) * (int)template.ShingleCount;
			if (containmentNumerator > containmentDenorminator)
			{
				containmentNumerator = containmentDenorminator;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000A650 File Offset: 0x00008850
		public static bool TryDecode(string encodedFingerprint, out LshFingerprint decodedFingerprint)
		{
			if (!string.IsNullOrEmpty(encodedFingerprint))
			{
				string[] array = encodedFingerprint.Split(LshFingerprintConstants.DotDelimit);
				int num = 5;
				if (array.Length == num)
				{
					uint[] array2 = new uint[num];
					for (int i = 0; i < array2.Length; i++)
					{
						if (!uint.TryParse(array[i], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out array2[i]))
						{
							decodedFingerprint = LshFingerprint.EmptyFingerprint;
							return false;
						}
					}
					short num2 = (short)(array2[4] >> 16);
					if (num2 == 2)
					{
						decodedFingerprint = new LshFingerprint(array2, "");
						return true;
					}
				}
			}
			decodedFingerprint = LshFingerprint.EmptyFingerprint;
			return false;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000A6DC File Offset: 0x000088DC
		public static bool TryCreateFingerprint(uint[] fingerprintData, out LshFingerprint createdFingerprint, string id = "")
		{
			if (fingerprintData != null)
			{
				if (fingerprintData.Length != 5)
				{
					createdFingerprint = LshFingerprint.EmptyFingerprint;
					return false;
				}
				short num = (short)(fingerprintData[4] >> 16);
				if (num != 2)
				{
					createdFingerprint = LshFingerprint.EmptyFingerprint;
					return false;
				}
			}
			createdFingerprint = new LshFingerprint(fingerprintData, id);
			return true;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000A71A File Offset: 0x0000891A
		public static LshFingerprint GetEmptyFingerprint()
		{
			return LshFingerprint.EmptyFingerprint;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000A724 File Offset: 0x00008924
		public string Encode()
		{
			if (this.encodedFingerprintData == null)
			{
				if (this.FingerprintData != null)
				{
					this.encodedFingerprintData = string.Format("{0}.{1}.{2}.{3}.{4}", new object[]
					{
						this.FingerprintData[0].ToString("X"),
						this.FingerprintData[1].ToString("X"),
						this.FingerprintData[2].ToString("X"),
						this.FingerprintData[3].ToString("X"),
						this.FingerprintData[4].ToString("X")
					});
				}
				else
				{
					this.encodedFingerprintData = string.Empty;
				}
			}
			return this.encodedFingerprintData;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000A7EF File Offset: 0x000089EF
		private static bool NotQualifiedFingerprint(LshFingerprint fingerprint)
		{
			return fingerprint == null || fingerprint.Version != 2 || fingerprint.FingerprintData == null || fingerprint.FingerprintData.Length != 5 || fingerprint.ShingleCount <= 10;
		}

		// Token: 0x0400006C RID: 108
		private const double ShingleCountThreshold = 0.2;

		// Token: 0x0400006D RID: 109
		private static readonly LshFingerprint EmptyFingerprint = new LshFingerprint(null, "");

		// Token: 0x0400006E RID: 110
		private string encodedFingerprintData;
	}
}
