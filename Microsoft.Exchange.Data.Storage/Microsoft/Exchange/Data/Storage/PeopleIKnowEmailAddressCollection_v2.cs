using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200051D RID: 1309
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleIKnowEmailAddressCollection_v2 : BinarySearchableEmailAddressCollection
	{
		// Token: 0x06003813 RID: 14355 RVA: 0x000E5CE0 File Offset: 0x000E3EE0
		public PeopleIKnowEmailAddressCollection_v2(IDictionary<string, PeopleIKnowMetadata> peopleInfo, ITracer tracer, int traceId) : base(peopleInfo.Keys, tracer, traceId)
		{
			this.metadataSerializer = new PeopleIKnowEmailAddressCollection_v2.MetadataSerializerV2(peopleInfo);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000E5CFC File Offset: 0x000E3EFC
		public PeopleIKnowEmailAddressCollection_v2(byte[] data, ITracer tracer, int traceId) : base(data, tracer, traceId)
		{
			this.metadataSerializer = new PeopleIKnowEmailAddressCollection_v2.MetadataSerializerV2(null);
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06003815 RID: 14357 RVA: 0x000E5D13 File Offset: 0x000E3F13
		protected sealed override byte Version
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000E5D16 File Offset: 0x000E3F16
		protected sealed override BinarySearchableEmailAddressCollection.IMetadataSerializer MetadataSerializer
		{
			get
			{
				return this.metadataSerializer;
			}
		}

		// Token: 0x04001DF2 RID: 7666
		private readonly BinarySearchableEmailAddressCollection.IMetadataSerializer metadataSerializer;

		// Token: 0x0200051E RID: 1310
		private class MetadataSerializerV2 : BinarySearchableEmailAddressCollection.IMetadataSerializer
		{
			// Token: 0x06003817 RID: 14359 RVA: 0x000E5D1E File Offset: 0x000E3F1E
			public MetadataSerializerV2(IDictionary<string, PeopleIKnowMetadata> metadata = null)
			{
				this.metadata = metadata;
			}

			// Token: 0x06003818 RID: 14360 RVA: 0x000E5D2D File Offset: 0x000E3F2D
			public byte[] Serialize(string email)
			{
				return BitConverter.GetBytes(this.metadata[email].RelevanceScore);
			}

			// Token: 0x06003819 RID: 14361 RVA: 0x000E5D48 File Offset: 0x000E3F48
			public PeopleIKnowMetadata Deserialize(byte[] buffer)
			{
				if (buffer.Length != this.SizeOfMetadata)
				{
					throw new ArgumentException(string.Format("Size of the buffer unexpected: {0}", buffer.Length));
				}
				return new PeopleIKnowMetadata
				{
					RelevanceScore = BitConverter.ToInt32(buffer, 0)
				};
			}

			// Token: 0x1700115D RID: 4445
			// (get) Token: 0x0600381A RID: 14362 RVA: 0x000E5D8C File Offset: 0x000E3F8C
			public int SizeOfMetadata
			{
				get
				{
					return 4;
				}
			}

			// Token: 0x04001DF3 RID: 7667
			private readonly IDictionary<string, PeopleIKnowMetadata> metadata;
		}
	}
}
