using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000078 RID: 120
	internal class ExportSettings
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001E678 File Offset: 0x0001C878
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001E680 File Offset: 0x0001C880
		public DateTime ExportTime { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001E689 File Offset: 0x0001C889
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x0001E691 File Offset: 0x0001C891
		public bool IncludeDuplicates { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001E69A File Offset: 0x0001C89A
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0001E6A2 File Offset: 0x0001C8A2
		public bool IncludeSearchableItems { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001E6AB File Offset: 0x0001C8AB
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0001E6B3 File Offset: 0x0001C8B3
		public bool IncludeUnsearchableItems { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001E6BC File Offset: 0x0001C8BC
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
		public bool RemoveRms { get; set; }

		// Token: 0x060007EE RID: 2030 RVA: 0x0001E6D0 File Offset: 0x0001C8D0
		public static ExportSettings FromBinary(byte[] data, int startIndex)
		{
			ExportSettings exportSettings = new ExportSettings();
			exportSettings.ExportTime = DateTime.FromBinary(BitConverter.ToInt64(data, startIndex));
			int num = startIndex + 8;
			exportSettings.IncludeDuplicates = BitConverter.ToBoolean(data, num);
			num++;
			exportSettings.IncludeSearchableItems = BitConverter.ToBoolean(data, num);
			num++;
			exportSettings.IncludeUnsearchableItems = BitConverter.ToBoolean(data, num);
			num++;
			exportSettings.RemoveRms = BitConverter.ToBoolean(data, num);
			num++;
			return exportSettings;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001E740 File Offset: 0x0001C940
		public byte[] ToBinary()
		{
			byte[] array = new byte[256];
			int num = 0;
			ExportSettings.CopyData(BitConverter.GetBytes(this.ExportTime.ToBinary()), array, ref num);
			ExportSettings.CopyData(BitConverter.GetBytes(this.IncludeDuplicates), array, ref num);
			ExportSettings.CopyData(BitConverter.GetBytes(this.IncludeSearchableItems), array, ref num);
			ExportSettings.CopyData(BitConverter.GetBytes(this.IncludeUnsearchableItems), array, ref num);
			ExportSettings.CopyData(BitConverter.GetBytes(this.RemoveRms), array, ref num);
			return array;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001E7C2 File Offset: 0x0001C9C2
		private static void CopyData(byte[] source, byte[] dest, ref int destIndex)
		{
			Buffer.BlockCopy(source, 0, dest, destIndex, source.Length);
			destIndex += source.Length;
		}

		// Token: 0x040002E6 RID: 742
		public const int BinaryLength = 256;
	}
}
