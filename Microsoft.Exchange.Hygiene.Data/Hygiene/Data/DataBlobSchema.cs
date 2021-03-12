using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000AE RID: 174
	internal class DataBlobSchema
	{
		// Token: 0x04000383 RID: 899
		public static HygienePropertyDefinition DataTypeIdProperty = DataBlobCommonSchema.DataTypeIdProperty;

		// Token: 0x04000384 RID: 900
		public static HygienePropertyDefinition BlobIdProperty = DataBlobCommonSchema.BlobIdProperty;

		// Token: 0x04000385 RID: 901
		public static HygienePropertyDefinition MajorVersionProperty = DataBlobCommonSchema.MajorVersionProperty;

		// Token: 0x04000386 RID: 902
		public static HygienePropertyDefinition MinorVersionProperty = DataBlobCommonSchema.MinorVersionProperty;

		// Token: 0x04000387 RID: 903
		public static HygienePropertyDefinition BuildNumberProperty = DataBlobCommonSchema.BuildNumberProperty;

		// Token: 0x04000388 RID: 904
		public static HygienePropertyDefinition RevisionNumberProperty = DataBlobCommonSchema.RevisionNumberProperty;

		// Token: 0x04000389 RID: 905
		public static readonly HygienePropertyDefinition ChunkIdProperty = DataBlobCommonSchema.ChunkIdProperty;

		// Token: 0x0400038A RID: 906
		public static readonly HygienePropertyDefinition IsLastChunkProperty = DataBlobCommonSchema.IsLastChunkProperty;

		// Token: 0x0400038B RID: 907
		public static readonly HygienePropertyDefinition ChecksumProperty = DataBlobCommonSchema.ChecksumProperty;

		// Token: 0x0400038C RID: 908
		public static readonly HygienePropertyDefinition DataChunkProperty = DataBlobCommonSchema.DataChunkProperty;
	}
}
