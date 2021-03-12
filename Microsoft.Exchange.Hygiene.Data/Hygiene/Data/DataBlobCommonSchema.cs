using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000AF RID: 175
	internal class DataBlobCommonSchema
	{
		// Token: 0x0400038D RID: 909
		public static HygienePropertyDefinition DataTypeIdProperty = new HygienePropertyDefinition("DataTypeId", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400038E RID: 910
		public static HygienePropertyDefinition BlobIdProperty = new HygienePropertyDefinition("BlobId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400038F RID: 911
		public static HygienePropertyDefinition MajorVersionProperty = new HygienePropertyDefinition("MajorVersion", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000390 RID: 912
		public static HygienePropertyDefinition MinorVersionProperty = new HygienePropertyDefinition("MinorVersion", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000391 RID: 913
		public static HygienePropertyDefinition BuildNumberProperty = new HygienePropertyDefinition("BuildNumber", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000392 RID: 914
		public static HygienePropertyDefinition RevisionNumberProperty = new HygienePropertyDefinition("RevisionNumber", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000393 RID: 915
		public static HygienePropertyDefinition BlobSizeBytesProperty = new HygienePropertyDefinition("BlobSizeBytes", typeof(long), -1L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000394 RID: 916
		public static HygienePropertyDefinition IsCompleteBlobProperty = new HygienePropertyDefinition("IsCompleteBlob", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000395 RID: 917
		public static readonly HygienePropertyDefinition ChunkIdProperty = new HygienePropertyDefinition("ChunkId", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000396 RID: 918
		public static readonly HygienePropertyDefinition IsLastChunkProperty = new HygienePropertyDefinition("IsLastchunk", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000397 RID: 919
		public static readonly HygienePropertyDefinition ChecksumProperty = new HygienePropertyDefinition("Checksum", typeof(long), 0L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000398 RID: 920
		public static readonly HygienePropertyDefinition DataChunkProperty = new HygienePropertyDefinition("DataChunk", typeof(byte[]));

		// Token: 0x04000399 RID: 921
		public static readonly HygienePropertyDefinition BlobVersionTypeProperty = new HygienePropertyDefinition("BlobVersionType", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400039A RID: 922
		public static readonly HygienePropertyDefinition PrimaryOnlyProperty = new HygienePropertyDefinition("PrimaryOnly", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400039B RID: 923
		public static readonly HygienePropertyDefinition MajorVersionOnlyProperty = new HygienePropertyDefinition("MajorVersionOnly", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400039C RID: 924
		public static readonly HygienePropertyDefinition LatestVersionOnlyProperty = new HygienePropertyDefinition("LatestVersionOnly", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400039D RID: 925
		public static readonly HygienePropertyDefinition SinceVersionOnlyProperty = new HygienePropertyDefinition("SinceVersionOnly", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
