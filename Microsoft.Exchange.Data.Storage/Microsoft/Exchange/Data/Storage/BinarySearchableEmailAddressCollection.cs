using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000496 RID: 1174
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class BinarySearchableEmailAddressCollection : PeopleIKnowEmailAddressCollection
	{
		// Token: 0x060033FC RID: 13308 RVA: 0x000D33EE File Offset: 0x000D15EE
		public BinarySearchableEmailAddressCollection(ICollection<string> strings, ITracer tracer, int traceId)
		{
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.tracer = tracer;
			this.traceId = traceId;
			this.strings = strings;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000D3416 File Offset: 0x000D1616
		public BinarySearchableEmailAddressCollection(byte[] data, ITracer tracer, int traceId)
		{
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.tracer = tracer;
			this.traceId = traceId;
			this.data = data;
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x060033FE RID: 13310
		protected abstract byte Version { get; }

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x060033FF RID: 13311
		protected abstract BinarySearchableEmailAddressCollection.IMetadataSerializer MetadataSerializer { get; }

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x000D343E File Offset: 0x000D163E
		private int Count
		{
			get
			{
				if (this.Data == null || this.Data.Length < 2)
				{
					return 0;
				}
				return (int)this.ReadUShortFromDataArray(1);
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000D345C File Offset: 0x000D165C
		public override byte[] Data
		{
			get
			{
				if (this.data == null)
				{
					this.PackStrings(this.strings);
				}
				return this.data;
			}
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000D3478 File Offset: 0x000D1678
		public override bool Contains(string s, out PeopleIKnowMetadata metadata)
		{
			this.tracer.TraceDebug((long)this.traceId, "BinarySearchableEmailAddressCollection.Contains: entering");
			metadata = null;
			if (this.Data == null)
			{
				this.tracer.TraceDebug((long)this.traceId, "this.Data is null; returning false");
				return false;
			}
			if (string.IsNullOrEmpty(s))
			{
				this.tracer.TraceDebug((long)this.traceId, "s is null or empty; returning false");
				return false;
			}
			byte[] array = this.NormalizeAndConvertToUTF8(s);
			byte[] array2 = new byte[array.Length + 1];
			Array.Copy(array, 0, array2, 0, array.Length);
			int i = 0;
			int num = this.Count - 1;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				int num3 = (int)this.ReadUShortFromDataArray(3 + num2 * 2);
				int num4 = BinarySearchableEmailAddressCollection.byteArrayComparer.CompareNullTerminatedByteStrings(this.Data, num3, array2, 0);
				if (num4 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					if (num4 <= 0)
					{
						this.tracer.TraceDebug((long)this.traceId, "BinarySearchableEmailAddressCollection.Contains: search string was found; returning true");
						while (this.Data[num3++] != 0)
						{
						}
						byte[] array3 = new byte[this.MetadataSerializer.SizeOfMetadata];
						Array.Copy(this.Data, num3, array3, 0, this.MetadataSerializer.SizeOfMetadata);
						metadata = this.MetadataSerializer.Deserialize(array3);
						return true;
					}
					num = num2 - 1;
				}
			}
			this.tracer.TraceDebug((long)this.traceId, "BinarySearchableEmailAddressCollection.Contains: search string was not found; returning false");
			return false;
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000D35F0 File Offset: 0x000D17F0
		private void PackStrings(ICollection<string> strings)
		{
			this.tracer.TraceDebug((long)this.traceId, "BinarySearchableEmailAddressCollection.PackStrings: entering");
			List<Tuple<string, byte[]>> list = new List<Tuple<string, byte[]>>(strings.Count);
			int num = 3;
			this.tracer.TraceDebug<int>((long)this.traceId, "BinarySearchableEmailAddressCollection.PackStrings: cumulativeByteCount initialized to {0}", num);
			foreach (string text in strings)
			{
				if (!string.IsNullOrEmpty(text))
				{
					byte[] array = this.NormalizeAndConvertToUTF8(text);
					if (num + array.Length + 1 + 2 + this.MetadataSerializer.SizeOfMetadata > 30000)
					{
						this.tracer.TraceWarning<int>((long)this.traceId, "BinarySearchableEmailAddressCollection.PackStrings: size limit of {0} exceeded; string NOT added and list truncated at this point", 30000);
						break;
					}
					num += array.Length + 1 + 2 + this.MetadataSerializer.SizeOfMetadata;
					list.Add(Tuple.Create<string, byte[]>(text, array));
					this.tracer.TraceDebug<int>((long)this.traceId, "BinarySearchableEmailAddressCollection.PackStrings: string added, cumulativeByteCount = {0}", num);
				}
			}
			this.tracer.TraceDebug<int>((long)this.traceId, "BinarySearchableEmailAddressCollection.PackStrings: sorting {0} entries", list.Count);
			list.Sort((Tuple<string, byte[]> t1, Tuple<string, byte[]> t2) => BinarySearchableEmailAddressCollection.byteArrayComparer.Compare(t1.Item2, t2.Item2));
			this.tracer.TraceDebug((long)this.traceId, "BinarySearchableEmailAddressCollection.PackStrings: writing final packed and sorted list");
			this.data = new byte[num];
			this.data[0] = this.Version;
			this.WriteUShortToDataArray(1, (ushort)list.Count);
			int num2 = 3;
			int num3 = 3 + list.Count * 2;
			foreach (Tuple<string, byte[]> tuple in list)
			{
				string item = tuple.Item1;
				byte[] item2 = tuple.Item2;
				this.WriteUShortToDataArray(num2, (ushort)num3);
				Array.Copy(item2, 0, this.data, num3, item2.Length);
				this.data[num3 + item2.Length] = 0;
				byte[] array2 = this.MetadataSerializer.Serialize(item);
				if (array2.Length != this.MetadataSerializer.SizeOfMetadata)
				{
					throw new InvalidOperationException(string.Format("Metadata length does not match SizeOfMetadata. Actual size: {0}", array2.Length));
				}
				Array.Copy(array2, 0, this.data, num3 + item2.Length + 1, this.MetadataSerializer.SizeOfMetadata);
				num2 += 2;
				num3 += item2.Length + 1 + this.MetadataSerializer.SizeOfMetadata;
			}
			this.tracer.TraceDebug((long)this.traceId, "BinarySearchableEmailAddressCollection.PackStrings: exiting");
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000D389C File Offset: 0x000D1A9C
		private byte[] NormalizeAndConvertToUTF8(string s)
		{
			string text = s.Normalize().Trim().ToLowerInvariant();
			this.tracer.TraceDebug<string>((long)this.traceId, "BinarySearchableEmailAddressCollection.NormalizeAndConvertToUTF8: normalized string is {0}", text);
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000D38DD File Offset: 0x000D1ADD
		private ushort ReadUShortFromDataArray(int index)
		{
			return BitConverter.ToUInt16(this.data, index);
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000D38EB File Offset: 0x000D1AEB
		private void WriteUShortToDataArray(int index, ushort value)
		{
			ExBitConverter.Write(value, this.data, index);
		}

		// Token: 0x04001BF4 RID: 7156
		public const int MaximumDataSize = 30000;

		// Token: 0x04001BF5 RID: 7157
		private const int NumberOfHeaderBytes = 3;

		// Token: 0x04001BF6 RID: 7158
		private const int NumberOfEntriesOffset = 1;

		// Token: 0x04001BF7 RID: 7159
		private const int SizeOfOffsetRecord = 2;

		// Token: 0x04001BF8 RID: 7160
		private static BinarySearchableEmailAddressCollection.ByteArrayComparer byteArrayComparer = new BinarySearchableEmailAddressCollection.ByteArrayComparer();

		// Token: 0x04001BF9 RID: 7161
		private readonly ITracer tracer;

		// Token: 0x04001BFA RID: 7162
		private readonly int traceId;

		// Token: 0x04001BFB RID: 7163
		private readonly ICollection<string> strings;

		// Token: 0x04001BFC RID: 7164
		private byte[] data;

		// Token: 0x02000497 RID: 1175
		protected interface IMetadataSerializer
		{
			// Token: 0x1700102F RID: 4143
			// (get) Token: 0x06003409 RID: 13321
			int SizeOfMetadata { get; }

			// Token: 0x0600340A RID: 13322
			byte[] Serialize(string email);

			// Token: 0x0600340B RID: 13323
			PeopleIKnowMetadata Deserialize(byte[] buffer);
		}

		// Token: 0x02000498 RID: 1176
		private class ByteArrayComparer : Comparer<byte[]>, IEqualityComparer<byte[]>
		{
			// Token: 0x0600340C RID: 13324 RVA: 0x000D3908 File Offset: 0x000D1B08
			public override int Compare(byte[] a, byte[] b)
			{
				int num = 0;
				while (num < a.Length && num < b.Length && a[num] == b[num])
				{
					num++;
				}
				if (num == a.Length || num == b.Length)
				{
					return a.Length - b.Length;
				}
				return (int)(a[num] - b[num]);
			}

			// Token: 0x0600340D RID: 13325 RVA: 0x000D394C File Offset: 0x000D1B4C
			public int CompareNullTerminatedByteStrings(byte[] a, int ia, byte[] b, int ib)
			{
				while (a[ia] == b[ib])
				{
					if (a[ia] == 0)
					{
						return 0;
					}
					ia++;
					ib++;
				}
				return (int)(a[ia] - b[ib]);
			}

			// Token: 0x0600340E RID: 13326 RVA: 0x000D3973 File Offset: 0x000D1B73
			public bool Equals(byte[] a, byte[] b)
			{
				return this.Compare(a, b) == 0;
			}

			// Token: 0x0600340F RID: 13327 RVA: 0x000D3980 File Offset: 0x000D1B80
			public int GetHashCode(byte[] a)
			{
				return a.GetHashCode();
			}
		}
	}
}
