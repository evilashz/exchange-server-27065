using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200051B RID: 1307
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleIKnowEmailAddressCollection_v1 : BinarySearchableEmailAddressCollection
	{
		// Token: 0x0600380B RID: 14347 RVA: 0x000E5CAB File Offset: 0x000E3EAB
		public PeopleIKnowEmailAddressCollection_v1(ICollection<string> strings, ITracer tracer, int traceId) : base(strings, tracer, traceId)
		{
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000E5CB6 File Offset: 0x000E3EB6
		public PeopleIKnowEmailAddressCollection_v1(byte[] data, ITracer tracer, int traceId) : base(data, tracer, traceId)
		{
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x000E5CC1 File Offset: 0x000E3EC1
		protected sealed override byte Version
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x000E5CC4 File Offset: 0x000E3EC4
		protected sealed override BinarySearchableEmailAddressCollection.IMetadataSerializer MetadataSerializer
		{
			get
			{
				return new PeopleIKnowEmailAddressCollection_v1.MetadataSerializerV1();
			}
		}

		// Token: 0x0200051C RID: 1308
		private class MetadataSerializerV1 : BinarySearchableEmailAddressCollection.IMetadataSerializer
		{
			// Token: 0x1700115A RID: 4442
			// (get) Token: 0x0600380F RID: 14351 RVA: 0x000E5CCB File Offset: 0x000E3ECB
			public int SizeOfMetadata
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06003810 RID: 14352 RVA: 0x000E5CCE File Offset: 0x000E3ECE
			public byte[] Serialize(string email)
			{
				return Array<byte>.Empty;
			}

			// Token: 0x06003811 RID: 14353 RVA: 0x000E5CD5 File Offset: 0x000E3ED5
			public PeopleIKnowMetadata Deserialize(byte[] buffer)
			{
				return null;
			}
		}
	}
}
