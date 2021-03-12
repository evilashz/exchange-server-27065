using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000495 RID: 1173
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PeopleIKnowEmailAddressCollection
	{
		// Token: 0x060033F5 RID: 13301 RVA: 0x000D3307 File Offset: 0x000D1507
		public static PeopleIKnowEmailAddressCollection CreateFromStringCollection(IDictionary<string, PeopleIKnowMetadata> peopleInfo, ITracer tracer, int traceId)
		{
			return PeopleIKnowEmailAddressCollection.CreateFromStringCollection(peopleInfo, tracer, traceId, 1);
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000D3314 File Offset: 0x000D1514
		public static PeopleIKnowEmailAddressCollection CreateFromStringCollection(IDictionary<string, PeopleIKnowMetadata> peopleInfo, ITracer tracer, int traceId, int version)
		{
			ArgumentValidator.ThrowIfNull("peopleInfo", peopleInfo);
			switch (version)
			{
			case 1:
				return new PeopleIKnowEmailAddressCollection_v1(peopleInfo.Keys, tracer, traceId);
			case 2:
				return new PeopleIKnowEmailAddressCollection_v2(peopleInfo, tracer, traceId);
			default:
				throw new ArgumentOutOfRangeException("Version number unexpected: {0}".FormatWith(new object[]
				{
					version
				}));
			}
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000D3378 File Offset: 0x000D1578
		public static PeopleIKnowEmailAddressCollection CreateFromByteArray(byte[] bytes, ITracer tracer, int traceId)
		{
			ArgumentValidator.ThrowIfNull("bytes", bytes);
			if (bytes.Length == 0)
			{
				throw new ArgumentOutOfRangeException("bytes");
			}
			byte b = bytes[0];
			if (b == 0)
			{
				throw new InvalidDataException("0 is not a valid version for this data format");
			}
			if (b == 1)
			{
				return new PeopleIKnowEmailAddressCollection_v1(bytes, tracer, traceId);
			}
			if (b == 2)
			{
				return new PeopleIKnowEmailAddressCollection_v2(bytes, tracer, traceId);
			}
			return null;
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060033F9 RID: 13305
		public abstract byte[] Data { get; }

		// Token: 0x060033FA RID: 13306 RVA: 0x000D33D8 File Offset: 0x000D15D8
		public bool Contains(string s)
		{
			PeopleIKnowMetadata peopleIKnowMetadata;
			return this.Contains(s, out peopleIKnowMetadata);
		}

		// Token: 0x060033FB RID: 13307
		public abstract bool Contains(string s, out PeopleIKnowMetadata metadata);
	}
}
