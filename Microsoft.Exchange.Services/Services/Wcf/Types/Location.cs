using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A55 RID: 2645
	[DataContract]
	public class Location
	{
		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06004B02 RID: 19202 RVA: 0x001053BC File Offset: 0x001035BC
		// (set) Token: 0x06004B03 RID: 19203 RVA: 0x001053C4 File Offset: 0x001035C4
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06004B04 RID: 19204 RVA: 0x001053CD File Offset: 0x001035CD
		// (set) Token: 0x06004B05 RID: 19205 RVA: 0x001053D5 File Offset: 0x001035D5
		[DataMember]
		public LocationSource Source { get; set; }

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x001053DE File Offset: 0x001035DE
		// (set) Token: 0x06004B07 RID: 19207 RVA: 0x001053E6 File Offset: 0x001035E6
		[DataMember]
		public string Uri { get; set; }

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06004B08 RID: 19208 RVA: 0x001053EF File Offset: 0x001035EF
		// (set) Token: 0x06004B09 RID: 19209 RVA: 0x001053F7 File Offset: 0x001035F7
		[DataMember(EmitDefaultValue = false)]
		public double? Latitude { get; set; }

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06004B0A RID: 19210 RVA: 0x00105400 File Offset: 0x00103600
		// (set) Token: 0x06004B0B RID: 19211 RVA: 0x00105408 File Offset: 0x00103608
		[DataMember(EmitDefaultValue = false)]
		public double? Longitude { get; set; }

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06004B0C RID: 19212 RVA: 0x00105411 File Offset: 0x00103611
		// (set) Token: 0x06004B0D RID: 19213 RVA: 0x00105419 File Offset: 0x00103619
		[DataMember(EmitDefaultValue = false)]
		public double? Accuracy { get; set; }

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06004B0E RID: 19214 RVA: 0x00105422 File Offset: 0x00103622
		// (set) Token: 0x06004B0F RID: 19215 RVA: 0x0010542A File Offset: 0x0010362A
		[DataMember(EmitDefaultValue = false)]
		public double? Altitude { get; set; }

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06004B10 RID: 19216 RVA: 0x00105433 File Offset: 0x00103633
		// (set) Token: 0x06004B11 RID: 19217 RVA: 0x0010543B File Offset: 0x0010363B
		[DataMember(EmitDefaultValue = false)]
		public double? AltitudeAccuracy { get; set; }

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06004B12 RID: 19218 RVA: 0x00105444 File Offset: 0x00103644
		// (set) Token: 0x06004B13 RID: 19219 RVA: 0x0010544C File Offset: 0x0010364C
		[DataMember]
		public string StreetAddress { get; set; }
	}
}
