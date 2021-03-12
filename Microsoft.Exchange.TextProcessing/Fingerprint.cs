using System;
using Microsoft.Exchange.Diagnostics.Components.TextProcessing;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000008 RID: 8
	internal class Fingerprint
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002DE1 File Offset: 0x00000FE1
		public Fingerprint(long id, ushort[] fingerprintData, ulong shingleCount, int version)
		{
			this.Identifier = id;
			this.ShingleCount = shingleCount;
			this.FingerprintData = fingerprintData;
			this.Version = version;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002E06 File Offset: 0x00001006
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002E0E File Offset: 0x0000100E
		public long Identifier { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002E17 File Offset: 0x00001017
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002E1F File Offset: 0x0000101F
		public int Version { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002E28 File Offset: 0x00001028
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002E30 File Offset: 0x00001030
		public ulong ShingleCount { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002E39 File Offset: 0x00001039
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002E41 File Offset: 0x00001041
		public ushort[] FingerprintData { get; private set; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002E4C File Offset: 0x0000104C
		public static double ComputeSimilarity(Fingerprint first, Fingerprint second)
		{
			if (first.Version != second.Version)
			{
				throw new ArgumentException(Strings.MismatchedFingerprintVersions(first.Version, second.Version));
			}
			if (first.FingerprintData.Length != second.FingerprintData.Length || first.FingerprintData.Length == 0)
			{
				throw new ArgumentException(Strings.MismatchedFingerprintSize(first.FingerprintData.Length, second.FingerprintData.Length));
			}
			ulong num = 0UL;
			for (int i = 0; i < first.FingerprintData.Length; i++)
			{
				if (first.FingerprintData[i] == second.FingerprintData[i])
				{
					num += 1UL;
				}
			}
			double num2 = num / (double)first.FingerprintData.Length;
			ExTraceGlobals.FingerprintTracer.TraceDebug<double, long, long>(0L, "Computed fingerprint similarity of '{0}', for fingerprints with id '{1}', '{2}'.", num2, first.Identifier, second.Identifier);
			return num2;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002F1C File Offset: 0x0000111C
		public static double ComputeContainment(Fingerprint template, Fingerprint document)
		{
			if (template.Version != document.Version)
			{
				throw new ArgumentException(Strings.MismatchedFingerprintVersions(template.Version, document.Version));
			}
			if (0UL == template.ShingleCount)
			{
				throw new ArgumentException(Strings.InvalidShingleCountForTemplate(template.ShingleCount));
			}
			double num = Fingerprint.ComputeSimilarity(template, document);
			double num2 = num / (1.0 + num) * ((template.ShingleCount + document.ShingleCount) / template.ShingleCount);
			ExTraceGlobals.FingerprintTracer.TraceDebug<double, long, long>(0L, "Computed fingerprint contaiment coefficient of '{0}' between template fingerprint with id '{1}' and document fingerprinting with id '{2}'.", num2, template.Identifier, document.Identifier);
			return num2;
		}
	}
}
