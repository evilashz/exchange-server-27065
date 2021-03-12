using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000038 RID: 56
	internal class FingerprintMatch : IMatch
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x0000E20D File Offset: 0x0000C40D
		internal FingerprintMatch(LshFingerprint fingerprint, double coefficient, bool detectContainment)
		{
			this.fingerprint = fingerprint;
			this.coefficient = coefficient;
			this.detectContainment = detectContainment;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000E22C File Offset: 0x0000C42C
		public bool IsMatch(TextScanContext data)
		{
			LshFingerprint lshFingerprint = data.Fingerprint;
			int num;
			int num2;
			if (this.detectContainment)
			{
				LshFingerprint.ComputeContainment(this.fingerprint, lshFingerprint, out num, out num2);
			}
			else
			{
				LshFingerprint.ComputeSimilarity(this.fingerprint, lshFingerprint, out num, out num2, false, false);
			}
			return (double)num >= this.coefficient * (double)num2;
		}

		// Token: 0x04000133 RID: 307
		private readonly double coefficient;

		// Token: 0x04000134 RID: 308
		private readonly bool detectContainment;

		// Token: 0x04000135 RID: 309
		private LshFingerprint fingerprint;
	}
}
