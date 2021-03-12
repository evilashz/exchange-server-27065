using System;
using System.Runtime.Serialization;
using Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner;

namespace Microsoft.Office.Story.V1.ImageAnalysis.SalientObjectDetection
{
	// Token: 0x02000058 RID: 88
	[DataContract]
	[Serializable]
	internal class SalientObject
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00008239 File Offset: 0x00006439
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00008241 File Offset: 0x00006441
		[DataMember]
		public RegionInfo<ArgbPixel, byte, LabTile> Region { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000824A File Offset: 0x0000644A
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00008252 File Offset: 0x00006452
		[DataMember]
		public float SaliencePortion { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000825B File Offset: 0x0000645B
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00008263 File Offset: 0x00006463
		[DataMember]
		public bool IsPrimary { get; set; }
	}
}
