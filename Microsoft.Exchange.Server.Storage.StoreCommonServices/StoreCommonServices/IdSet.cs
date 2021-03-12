using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000081 RID: 129
	public class IdSet : IEnumerable, IEquatable<IdSet>
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0001D346 File Offset: 0x0001B546
		public IdSet()
		{
			this.wrappee = new IdSet();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001D359 File Offset: 0x0001B559
		internal IdSet(IdSet idSet)
		{
			this.wrappee = idSet;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001D368 File Offset: 0x0001B568
		public ulong CountIds
		{
			get
			{
				return this.wrappee.CountIds;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0001D375 File Offset: 0x0001B575
		public int CountGuids
		{
			get
			{
				return this.wrappee.CountGuids;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0001D382 File Offset: 0x0001B582
		public int CountRanges
		{
			get
			{
				return this.wrappee.CountRanges;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0001D38F File Offset: 0x0001B58F
		public bool IsEmpty
		{
			get
			{
				return this.wrappee.IsEmpty;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001D39C File Offset: 0x0001B59C
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001D3A9 File Offset: 0x0001B5A9
		public bool IsDirty
		{
			get
			{
				return this.wrappee.IsDirty;
			}
			set
			{
				this.wrappee.IsDirty = value;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001D3B7 File Offset: 0x0001B5B7
		public static IdSet Parse(Context context, byte[] idsetBytes)
		{
			return new IdSet(IdSetUtilities.IdSetFromBytes(context, idsetBytes));
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001D3C5 File Offset: 0x0001B5C5
		public static IdSet ThrowableParse(Context context, byte[] idsetBytes)
		{
			return new IdSet(IdSetUtilities.ThrowableIdSetFromBytes(context, idsetBytes));
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001D3D3 File Offset: 0x0001B5D3
		public static IdSet ThrowableParse(Context context, Stream readStream)
		{
			return new IdSet(IdSetUtilities.ThrowableIdSetFromStream(context, readStream));
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001D3E1 File Offset: 0x0001B5E1
		internal static IdSet ThrowableParse(Reader reader)
		{
			return new IdSet(IdSet.ParseWithReplGuids(reader));
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001D3EE File Offset: 0x0001B5EE
		public static IdSet Union(IdSet first, IdSet second)
		{
			return new IdSet(IdSet.Union(first.wrappee, second.wrappee));
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001D406 File Offset: 0x0001B606
		public static IdSet Subtract(IdSet first, IdSet second)
		{
			return new IdSet(IdSet.Subtract(first.wrappee, second.wrappee));
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001D41E File Offset: 0x0001B61E
		public static IdSet Intersect(IdSet first, IdSet second)
		{
			return new IdSet(IdSet.Intersect(first.wrappee, second.wrappee));
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001D436 File Offset: 0x0001B636
		public bool Insert(ExchangeId id)
		{
			return this.Insert(id.Guid, id.Counter);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001D44C File Offset: 0x0001B64C
		public bool Insert(byte[] twentySixByteArray)
		{
			Guid guid;
			ushort num;
			ulong counter;
			ExchangeIdHelpers.From26ByteArray(twentySixByteArray, out guid, out num, out counter);
			return this.Insert(guid, counter);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001D46D File Offset: 0x0001B66D
		public bool Insert(Guid guid, ulong counter)
		{
			return counter != 0UL && this.wrappee.Insert(guid, counter);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001D483 File Offset: 0x0001B683
		internal bool Insert(LongTermIdRange idRange)
		{
			if (!idRange.IsValid())
			{
				throw new StoreException((LID)43129U, ErrorCodeValue.InvalidParameter);
			}
			return this.wrappee.Insert(idRange);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001D4B4 File Offset: 0x0001B6B4
		public bool Insert(IdSet other)
		{
			return this.wrappee.Insert(other.wrappee);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001D4C7 File Offset: 0x0001B6C7
		public bool Remove(ExchangeId id)
		{
			return this.Remove(id.Guid, id.Counter);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		public bool Remove(byte[] twentySixByteArray)
		{
			Guid guid;
			ushort num;
			ulong counter;
			ExchangeIdHelpers.From26ByteArray(twentySixByteArray, out guid, out num, out counter);
			return this.Remove(guid, counter);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001D501 File Offset: 0x0001B701
		public bool Remove(Guid guid, ulong counter)
		{
			return counter != 0UL && this.wrappee.Remove(new GuidGlobCount(guid, counter));
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001D51C File Offset: 0x0001B71C
		public bool Remove(IdSet other)
		{
			return this.wrappee.Remove(other.wrappee);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001D52F File Offset: 0x0001B72F
		public bool Contains(ExchangeId id)
		{
			return this.Contains(id.Guid, id.Counter);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001D548 File Offset: 0x0001B748
		public bool Contains(byte[] twentySixByteArray)
		{
			Guid guid;
			ushort num;
			ulong counter;
			ExchangeIdHelpers.From26ByteArray(twentySixByteArray, out guid, out num, out counter);
			return this.Contains(guid, counter);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001D569 File Offset: 0x0001B769
		public bool Contains(Guid guid, ulong counter)
		{
			return counter != 0UL && this.wrappee.Contains(new GuidGlobCount(guid, counter));
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001D584 File Offset: 0x0001B784
		public bool Equals(IdSet other)
		{
			return other != null && this.wrappee.Equals(other.wrappee);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001D59C File Offset: 0x0001B79C
		public override bool Equals(object other)
		{
			return this.Equals(other as IdSet);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001D5AA File Offset: 0x0001B7AA
		public override int GetHashCode()
		{
			return this.wrappee.GetHashCode();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001D5B7 File Offset: 0x0001B7B7
		public void IdealPack()
		{
			this.wrappee.IdealPack();
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001D5C5 File Offset: 0x0001B7C5
		public byte[] Serialize()
		{
			return IdSetUtilities.BytesFromIdSet(this.wrappee);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001D5D2 File Offset: 0x0001B7D2
		internal byte[] Serialize(Func<Guid, ReplId> replIdFromGuidMapper)
		{
			return this.wrappee.SerializeWithReplIds(replIdFromGuidMapper);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		internal void Serialize(Writer writer)
		{
			this.wrappee.SerializeWithReplGuids(writer);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001D5EE File Offset: 0x0001B7EE
		public override string ToString()
		{
			return this.wrappee.ToString();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001D5FB File Offset: 0x0001B7FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.wrappee.GetEnumerator();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001D608 File Offset: 0x0001B808
		public IdSet Clone()
		{
			return new IdSet(this.wrappee.Clone());
		}

		// Token: 0x040003C4 RID: 964
		private IdSet wrappee;
	}
}
