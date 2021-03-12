using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000059 RID: 89
	internal sealed class IdSet : IEquatable<IdSet>, IEnumerable<GuidGlobCountSet>, IEnumerable
	{
		// Token: 0x0600026F RID: 623 RVA: 0x000096A8 File Offset: 0x000078A8
		internal IdSet(GuidGlobCountSet[] sets)
		{
			foreach (GuidGlobCountSet newGuidGlobCountSet in sets)
			{
				this.Insert(newGuidGlobCountSet);
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000096F2 File Offset: 0x000078F2
		internal IdSet()
		{
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000970C File Offset: 0x0000790C
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00009714 File Offset: 0x00007914
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
			set
			{
				this.isDirty = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00009720 File Offset: 0x00007920
		public bool IsEmpty
		{
			get
			{
				foreach (GuidGlobCountSet guidGlobCountSet in this.sets)
				{
					if (!guidGlobCountSet.IsEmpty)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000977C File Offset: 0x0000797C
		public int CountGuids
		{
			get
			{
				return this.sets.Count;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000978C File Offset: 0x0000798C
		public int CountRanges
		{
			get
			{
				int num = 0;
				foreach (GuidGlobCountSet guidGlobCountSet in this.sets)
				{
					num += guidGlobCountSet.CountRanges;
				}
				return num;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000097E8 File Offset: 0x000079E8
		public ulong CountIds
		{
			get
			{
				ulong num = 0UL;
				foreach (GuidGlobCountSet guidGlobCountSet in this.sets)
				{
					num += guidGlobCountSet.CountIds;
				}
				return num;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000984C File Offset: 0x00007A4C
		public static IdSet ParseWithReplGuids(Reader reader)
		{
			return IdSet.Parse(reader, (Reader r) => r.ReadGuid());
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000988C File Offset: 0x00007A8C
		public static IdSet ParseWithReplIds(Reader reader, Func<ReplId, Guid> guidFromReplId)
		{
			return IdSet.Parse(reader, (Reader r) => guidFromReplId(ReplId.Parse(r)));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000098B8 File Offset: 0x00007AB8
		public static IdSet Union(IdSet first, IdSet second)
		{
			IdSet idSet = new IdSet();
			int num = 0;
			int num2 = 0;
			while (num < first.sets.Count || num2 < second.sets.Count)
			{
				int num3 = (num2 >= second.sets.Count) ? -1 : ((num >= first.sets.Count) ? 1 : first.sets[num].Guid.CompareTo(second.sets[num2].Guid));
				if (num3 < 0)
				{
					idSet.sets.Add(first.sets[num].Clone());
					num++;
				}
				else if (num3 > 0)
				{
					idSet.sets.Add(second.sets[num2].Clone());
					num2++;
				}
				else
				{
					GlobCountSet globCountSet = GlobCountSet.Union(first.sets[num].GlobCountSet, second.sets[num2].GlobCountSet);
					idSet.sets.Add(new GuidGlobCountSet(first.sets[num].Guid, globCountSet));
					num++;
					num2++;
				}
			}
			idSet.isDirty = false;
			return idSet;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009A0C File Offset: 0x00007C0C
		public static IdSet Subtract(IdSet first, IdSet second)
		{
			IdSet idSet = new IdSet();
			int i = 0;
			int num = 0;
			while (i < first.sets.Count)
			{
				int num2 = (num >= second.sets.Count) ? -1 : first.sets[i].Guid.CompareTo(second.sets[num].Guid);
				if (num2 < 0)
				{
					idSet.sets.Add(first.sets[i].Clone());
					i++;
				}
				else if (num2 > 0)
				{
					num++;
				}
				else
				{
					GlobCountSet globCountSet = GlobCountSet.Subtract(first.sets[i].GlobCountSet, second.sets[num].GlobCountSet);
					if (globCountSet != null)
					{
						idSet.sets.Add(new GuidGlobCountSet(first.sets[i].Guid, globCountSet));
					}
					i++;
					num++;
				}
			}
			idSet.isDirty = false;
			return idSet;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009B20 File Offset: 0x00007D20
		public static IdSet Intersect(IdSet first, IdSet second)
		{
			IdSet idSet = new IdSet();
			int num = 0;
			int num2 = 0;
			while (num < first.sets.Count && num2 < second.sets.Count)
			{
				int num3 = first.sets[num].Guid.CompareTo(second.sets[num2].Guid);
				if (num3 < 0)
				{
					num++;
				}
				else if (num3 > 0)
				{
					num2++;
				}
				else
				{
					GlobCountSet globCountSet = GlobCountSet.Intersect(first.sets[num].GlobCountSet, second.sets[num2].GlobCountSet);
					if (globCountSet != null)
					{
						idSet.Insert(new GuidGlobCountSet(first.sets[num].Guid, globCountSet));
					}
					num++;
					num2++;
				}
			}
			idSet.isDirty = false;
			return idSet;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009C0C File Offset: 0x00007E0C
		public bool Equals(IdSet other)
		{
			if (other == null || this.sets.Count != other.sets.Count)
			{
				return false;
			}
			for (int i = 0; i < this.sets.Count; i++)
			{
				if (!this.sets[i].Equals(other.sets[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009C74 File Offset: 0x00007E74
		public override bool Equals(object obj)
		{
			IdSet idSet = obj as IdSet;
			return idSet != null && this.Equals(idSet);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009C94 File Offset: 0x00007E94
		public override int GetHashCode()
		{
			int num = this.sets.Count;
			foreach (GuidGlobCountSet guidGlobCountSet in this.sets)
			{
				num ^= guidGlobCountSet.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009D00 File Offset: 0x00007F00
		public bool Insert(IdSet idSet)
		{
			bool flag = false;
			foreach (GuidGlobCountSet newGuidGlobCountSet in idSet.sets)
			{
				flag |= this.Insert(newGuidGlobCountSet);
			}
			return this.SetDirty(flag);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009D60 File Offset: 0x00007F60
		public bool Insert(GuidGlobCountSet newGuidGlobCountSet)
		{
			if (newGuidGlobCountSet.IsEmpty)
			{
				return false;
			}
			int num = this.FindSet(newGuidGlobCountSet.Guid);
			if (num > -1)
			{
				return this.SetDirty(this.sets[num].GlobCountSet.Insert(newGuidGlobCountSet.GlobCountSet));
			}
			this.sets.Insert(~num, newGuidGlobCountSet);
			return this.SetDirty(true);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009DC6 File Offset: 0x00007FC6
		public bool Insert(GuidGlobCount newGuidGlobCount)
		{
			return this.Insert(newGuidGlobCount.Guid, newGuidGlobCount.GlobCount);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009DDC File Offset: 0x00007FDC
		public bool Insert(Guid guid, ulong globCount)
		{
			int num = this.FindSet(guid);
			if (num > -1)
			{
				return this.SetDirty(this.sets[num].GlobCountSet.Insert(globCount));
			}
			GlobCountSet globCountSet = new GlobCountSet();
			globCountSet.Insert(globCount);
			GuidGlobCountSet item = new GuidGlobCountSet(guid, globCountSet);
			this.sets.Insert(~num, item);
			return this.SetDirty(true);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009E44 File Offset: 0x00008044
		public bool Insert(Guid guid, GlobCountRange range)
		{
			int num = this.FindSet(guid);
			if (num > -1)
			{
				return this.SetDirty(this.sets[num].GlobCountSet.Insert(range));
			}
			GlobCountSet globCountSet = new GlobCountSet();
			globCountSet.Insert(range);
			GuidGlobCountSet item = new GuidGlobCountSet(guid, globCountSet);
			this.sets.Insert(~num, item);
			return this.SetDirty(true);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009EAC File Offset: 0x000080AC
		public bool Remove(IdSet idSet)
		{
			bool flag = false;
			foreach (GuidGlobCountSet removedGuidGlobCountSet in idSet.sets)
			{
				flag |= this.Remove(removedGuidGlobCountSet);
			}
			return this.SetDirty(flag);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009F0C File Offset: 0x0000810C
		public bool Remove(GuidGlobCountSet removedGuidGlobCountSet)
		{
			int num = this.FindSet(removedGuidGlobCountSet.Guid);
			if (num > -1)
			{
				bool dirty = this.sets[num].GlobCountSet.Remove(removedGuidGlobCountSet.GlobCountSet);
				if (this.sets[num].GlobCountSet.IsEmpty)
				{
					this.sets.RemoveAt(num);
					this.lastGuidIndex = -1;
				}
				return this.SetDirty(dirty);
			}
			return false;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009F84 File Offset: 0x00008184
		public bool Remove(GuidGlobCount removedGuidGlobCount)
		{
			int num = this.FindSet(removedGuidGlobCount.Guid);
			if (num > -1)
			{
				bool dirty = this.sets[num].GlobCountSet.Remove(removedGuidGlobCount.GlobCount);
				if (this.sets[num].GlobCountSet.IsEmpty)
				{
					this.sets.RemoveAt(num);
					this.lastGuidIndex = -1;
				}
				return this.SetDirty(dirty);
			}
			return false;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009FFC File Offset: 0x000081FC
		public bool Remove(Guid guid, GlobCountRange range)
		{
			int num = this.FindSet(guid);
			if (num > -1)
			{
				bool dirty = this.sets[num].GlobCountSet.Remove(range);
				if (this.sets[num].GlobCountSet.IsEmpty)
				{
					this.sets.RemoveAt(num);
					this.lastGuidIndex = -1;
				}
				return this.SetDirty(dirty);
			}
			return false;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A068 File Offset: 0x00008268
		public bool IdealPack()
		{
			bool flag = false;
			foreach (GuidGlobCountSet guidGlobCountSet in this.sets)
			{
				flag |= guidGlobCountSet.GlobCountSet.IdealPack();
			}
			return this.SetDirty(flag);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A0CC File Offset: 0x000082CC
		public bool Contains(GuidGlobCount guidGlobCount)
		{
			int num = this.FindSet(guidGlobCount.Guid);
			return num > -1 && this.sets[num].GlobCountSet.Contains(guidGlobCount.GlobCount);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A110 File Offset: 0x00008310
		public void SerializeWithReplGuids(Writer writer)
		{
			foreach (GuidGlobCountSet guidGlobCountSet in this.sets)
			{
				writer.WriteGuid(guidGlobCountSet.Guid);
				guidGlobCountSet.GlobCountSet.Serialize(writer);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A178 File Offset: 0x00008378
		public byte[] SerializeWithReplGuids()
		{
			return BufferWriter.Serialize(new BufferWriter.SerializeDelegate(this.SerializeWithReplGuids));
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A1BC File Offset: 0x000083BC
		public void SerializeWithReplIds(Writer writer, Func<Guid, ReplId> replIdFromGuid)
		{
			IOrderedEnumerable<GuidGlobCountSet> orderedEnumerable = from s in this.sets
			orderby replIdFromGuid(s.Guid).Value
			select s;
			foreach (GuidGlobCountSet guidGlobCountSet in orderedEnumerable)
			{
				replIdFromGuid(guidGlobCountSet.Guid).Serialize(writer);
				guidGlobCountSet.GlobCountSet.Serialize(writer);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A268 File Offset: 0x00008468
		public byte[] SerializeWithReplIds(Func<Guid, ReplId> replIdFromGuid)
		{
			return BufferWriter.Serialize(delegate(Writer writer)
			{
				this.SerializeWithReplIds(writer, replIdFromGuid);
			});
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A29A File Offset: 0x0000849A
		public IEnumerator<GuidGlobCountSet> GetEnumerator()
		{
			return this.sets.GetEnumerator();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A2AC File Offset: 0x000084AC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public IdSet Clone()
		{
			IdSet idSet = new IdSet();
			foreach (GuidGlobCountSet guidGlobCountSet in this.sets)
			{
				idSet.Insert(guidGlobCountSet.Clone());
			}
			idSet.isDirty = false;
			return idSet;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A31C File Offset: 0x0000851C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('{');
			bool flag = true;
			foreach (GuidGlobCountSet guidGlobCountSet in this.sets)
			{
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(guidGlobCountSet.ToString());
				flag = false;
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A3AC File Offset: 0x000085AC
		private static IdSet Parse(Reader reader, Func<Reader, Guid> getReplGuid)
		{
			IdSet idSet = new IdSet();
			while (reader.Position < reader.Length)
			{
				Guid guid = getReplGuid(reader);
				GlobCountSet globCountSet = GlobCountSet.Parse(reader);
				idSet.Insert(new GuidGlobCountSet(guid, globCountSet));
			}
			idSet.isDirty = false;
			return idSet;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A3F4 File Offset: 0x000085F4
		private int FindSet(Guid guidToFind)
		{
			if (this.lastGuidIndex < 0 || guidToFind != this.lastGuid)
			{
				GuidGlobCountSet item = new GuidGlobCountSet(guidToFind, null);
				this.lastGuidIndex = this.sets.BinarySearch(item, IdSet.GuidGlobCountSetComparer.Default);
				this.lastGuid = guidToFind;
			}
			return this.lastGuidIndex;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A445 File Offset: 0x00008645
		private bool SetDirty(bool changed)
		{
			this.isDirty = (this.isDirty || changed);
			return changed;
		}

		// Token: 0x04000119 RID: 281
		private readonly List<GuidGlobCountSet> sets = new List<GuidGlobCountSet>();

		// Token: 0x0400011A RID: 282
		private int lastGuidIndex = -1;

		// Token: 0x0400011B RID: 283
		private Guid lastGuid;

		// Token: 0x0400011C RID: 284
		private bool isDirty;

		// Token: 0x0200005A RID: 90
		private class GuidGlobCountSetComparer : IComparer<GuidGlobCountSet>
		{
			// Token: 0x06000296 RID: 662 RVA: 0x0000A45A File Offset: 0x0000865A
			private GuidGlobCountSetComparer()
			{
			}

			// Token: 0x06000297 RID: 663 RVA: 0x0000A464 File Offset: 0x00008664
			int IComparer<GuidGlobCountSet>.Compare(GuidGlobCountSet x, GuidGlobCountSet y)
			{
				return x.Guid.CompareTo(y.Guid);
			}

			// Token: 0x0400011E RID: 286
			internal static IdSet.GuidGlobCountSetComparer Default = new IdSet.GuidGlobCountSetComparer();
		}
	}
}
