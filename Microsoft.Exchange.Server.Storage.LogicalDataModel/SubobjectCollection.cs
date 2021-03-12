using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.AttachmentBlob;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000D9 RID: 217
	public class SubobjectCollection
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x0005F9E6 File Offset: 0x0005DBE6
		public SubobjectCollection(Item item)
		{
			this.item = item;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0005FA00 File Offset: 0x0005DC00
		public SubobjectCollection(Item item, byte[] blob)
		{
			this.item = item;
			if (blob != null)
			{
				int count = AttachmentBlob.GetCount(blob, false);
				if (count != 0)
				{
					this.subobjects = new List<SubobjectCollection.SubobjectEntry>(count + 1);
					this.subobjects.AddRange(from kvp in AttachmentBlob.Deserialize(blob, false)
					select SubobjectCollection.SubobjectEntry.ConstructDeserialized(kvp));
					foreach (SubobjectCollection.SubobjectEntry subobjectEntry in this.subobjects)
					{
						this.SubobjectReferenceState.Addref(subobjectEntry.Inid);
						if (subobjectEntry.IsChild && subobjectEntry.ChildNumber >= this.nextChildNumber)
						{
							this.nextChildNumber = subobjectEntry.ChildNumber + 1;
						}
					}
					this.descendantCount = this.subobjects.Count;
				}
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0005FAFC File Offset: 0x0005DCFC
		public bool IsDirty
		{
			get
			{
				return this.dirty;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0005FB04 File Offset: 0x0005DD04
		public int ChildrenCount
		{
			get
			{
				int num = 0;
				if (this.subobjects != null)
				{
					foreach (SubobjectCollection.SubobjectEntry subobjectEntry in this.subobjects)
					{
						if (!subobjectEntry.IsDeleted && subobjectEntry.IsChild)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0005FB74 File Offset: 0x0005DD74
		public int DescendantCount
		{
			get
			{
				return this.descendantCount;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0005FB7C File Offset: 0x0005DD7C
		private SubobjectReferenceState SubobjectReferenceState
		{
			get
			{
				return this.item.SubobjectReferenceState;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0005FB8C File Offset: 0x0005DD8C
		public void ReleaseAll(bool calledFromFinalizer, Context context)
		{
			List<SubobjectCollection.SubobjectEntry> list = Interlocked.Exchange<List<SubobjectCollection.SubobjectEntry>>(ref this.subobjects, null);
			this.descendantCount = 0;
			if (list != null)
			{
				foreach (SubobjectCollection.SubobjectEntry subobjectEntry in list)
				{
					this.SubobjectReferenceState.Release(context, subobjectEntry.Inid, calledFromFinalizer ? null : this.item.Mailbox);
				}
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0005FC10 File Offset: 0x0005DE10
		public void ClearDeleted(Context context)
		{
			if (this.subobjects != null)
			{
				int num = 0;
				for (int i = 0; i < this.subobjects.Count; i++)
				{
					if (!this.subobjects[i].IsDeleted)
					{
						if (num != i)
						{
							this.subobjects[num] = this.subobjects[i];
						}
						num++;
					}
					else
					{
						this.SubobjectReferenceState.Release(context, this.subobjects[i].Inid, this.item.Mailbox);
					}
				}
				if (num != this.subobjects.Count)
				{
					if (num != 0)
					{
						this.subobjects.RemoveRange(num, this.subobjects.Count - num);
						return;
					}
					this.subobjects = null;
				}
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0005FCD8 File Offset: 0x0005DED8
		public void Add(Context context, SubobjectCollection otherCollection)
		{
			if (otherCollection.subobjects != null)
			{
				foreach (SubobjectCollection.SubobjectEntry subobjectEntry in otherCollection.subobjects)
				{
					if (!subobjectEntry.IsDeleted)
					{
						int num = this.FindDescendant(subobjectEntry.Inid);
						if (num < 0)
						{
							this.CheckMaxDescendantCount();
							this.subobjects.Add(SubobjectCollection.SubobjectEntry.ConstructNew(subobjectEntry.Inid));
							this.SubobjectReferenceState.Addref(subobjectEntry.Inid);
							this.dirty = true;
							this.descendantCount++;
						}
						else if (this.subobjects[num].IsDeleted)
						{
							this.CheckMaxDescendantCount();
							this.subobjects[num] = SubobjectCollection.SubobjectEntry.ConstructResurected(this.subobjects[num]);
							this.dirty = true;
							this.descendantCount++;
						}
					}
				}
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0005FDE8 File Offset: 0x0005DFE8
		public void Delete(Context context, SubobjectCollection otherCollection)
		{
			if (otherCollection.subobjects != null)
			{
				foreach (SubobjectCollection.SubobjectEntry subobjectEntry in otherCollection.subobjects)
				{
					if (!subobjectEntry.IsDeleted)
					{
						int num = this.FindDescendant(subobjectEntry.Inid);
						if (num >= 0 && !this.subobjects[num].IsDeleted)
						{
							long size = (subobjectEntry.Size != -1L) ? subobjectEntry.Size : this.subobjects[num].Size;
							this.subobjects[num] = SubobjectCollection.SubobjectEntry.ConstructDeleted(this.subobjects[num], size);
							this.dirty = true;
							this.descendantCount--;
						}
					}
				}
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0005FED8 File Offset: 0x0005E0D8
		public void DeleteDeleted(Context context, SubobjectCollection otherCollection)
		{
			if (otherCollection.subobjects != null)
			{
				foreach (SubobjectCollection.SubobjectEntry subobjectEntry in otherCollection.subobjects)
				{
					if (subobjectEntry.IsDeleted)
					{
						int num = this.FindDescendant(subobjectEntry.Inid);
						if (num >= 0 && !this.subobjects[num].IsDeleted)
						{
							this.subobjects[num] = SubobjectCollection.SubobjectEntry.ConstructDeleted(this.subobjects[num]);
							this.dirty = true;
							this.descendantCount--;
						}
					}
				}
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0005FF94 File Offset: 0x0005E194
		public void TombstoneAll(Context context)
		{
			if (this.subobjects != null)
			{
				foreach (SubobjectCollection.SubobjectEntry subobjectEntry in this.subobjects)
				{
					if (!subobjectEntry.IsDeleted && !subobjectEntry.IsNew)
					{
						SubobjectCleanup.AddTombstone(context, this.item, subobjectEntry.Inid, 0L);
					}
				}
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00060010 File Offset: 0x0005E210
		public void TombstoneDeleted(Context context)
		{
			if (this.subobjects != null)
			{
				foreach (SubobjectCollection.SubobjectEntry subobjectEntry in this.subobjects)
				{
					if (subobjectEntry.IsDeleted && !subobjectEntry.IsNew)
					{
						SubobjectCleanup.AddTombstone(context, this.item, subobjectEntry.Inid, (subobjectEntry.Size != -1L) ? subobjectEntry.Size : 0L);
					}
				}
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000600A0 File Offset: 0x0005E2A0
		public void CommitAll(Context context)
		{
			if (this.subobjects != null)
			{
				for (int i = 0; i < this.subobjects.Count; i++)
				{
					if (!this.subobjects[i].IsDeleted && this.subobjects[i].IsNew)
					{
						SubobjectCleanup.RemoveTombstone(context, this.item, this.subobjects[i].Inid);
						this.subobjects[i] = SubobjectCollection.SubobjectEntry.ConstructCommitted(this.subobjects[i]);
					}
				}
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00060134 File Offset: 0x0005E334
		public long? GetChildInid(int childNumber)
		{
			int num = this.FindChild(childNumber);
			if (num >= 0)
			{
				return new long?(this.subobjects[num].Inid);
			}
			return null;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00060170 File Offset: 0x0005E370
		public long GetChildSize(int childNumber)
		{
			int num = this.FindChild(childNumber);
			if (num >= 0)
			{
				return this.subobjects[num].Size;
			}
			return 0L;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000601A0 File Offset: 0x0005E3A0
		public void SetChildSize(int childNumber, long size)
		{
			int index = this.FindChild(childNumber);
			this.subobjects[index] = SubobjectCollection.SubobjectEntry.ConstructChildChangeSize(this.subobjects[index], size);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000601D4 File Offset: 0x0005E3D4
		public IEnumerable<int> GetChildNumbers()
		{
			int childrenCount = this.ChildrenCount;
			if (childrenCount == 0)
			{
				return Enumerable.Empty<int>();
			}
			int[] array = new int[childrenCount];
			int num = 0;
			foreach (SubobjectCollection.SubobjectEntry subobjectEntry in this.subobjects)
			{
				if (!subobjectEntry.IsDeleted && subobjectEntry.IsChild)
				{
					array[num++] = subobjectEntry.ChildNumber;
				}
			}
			Array.Sort<int>(array);
			return array;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00060264 File Offset: 0x0005E464
		public bool ContainsChild(int childNumber)
		{
			return this.FindChild(childNumber) >= 0;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00060274 File Offset: 0x0005E474
		public void AddOrUpdateChild(Context context, int childNumber, long inid, long size)
		{
			int num = this.FindChild(childNumber);
			if (num < 0)
			{
				if (this.subobjects == null)
				{
					this.subobjects = new List<SubobjectCollection.SubobjectEntry>(1);
				}
				this.CheckMaxDescendantCount();
				this.subobjects.Add(SubobjectCollection.SubobjectEntry.ConstructNewChild(inid, childNumber, size));
				this.SubobjectReferenceState.Addref(inid);
				this.dirty = true;
				this.descendantCount++;
				return;
			}
			this.subobjects.Add(SubobjectCollection.SubobjectEntry.ConstructDeleted(this.subobjects[num]));
			this.subobjects[num] = SubobjectCollection.SubobjectEntry.ConstructNewChild(inid, childNumber, size);
			this.SubobjectReferenceState.Addref(inid);
			this.dirty = true;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00060324 File Offset: 0x0005E524
		public void DeleteChild(Context context, int childNumber, long childSize)
		{
			int index = this.FindChild(childNumber);
			this.subobjects[index] = SubobjectCollection.SubobjectEntry.ConstructDeleted(this.subobjects[index], childSize);
			this.descendantCount--;
			this.dirty = true;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0006036C File Offset: 0x0005E56C
		public int ReserveChildNumber()
		{
			if (this.nextChildNumber == 2147483647)
			{
				throw new StoreException((LID)51192U, ErrorCodeValue.CallFailed, "child number overflow");
			}
			return this.nextChildNumber++;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000603B1 File Offset: 0x0005E5B1
		public void ResetNextChildNumber()
		{
			this.nextChildNumber = 0;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000603BA File Offset: 0x0005E5BA
		public void SetDirty()
		{
			this.dirty = true;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000603F4 File Offset: 0x0005E5F4
		public byte[] Serialize(bool renumberChildren, bool resetDirtyFlag)
		{
			if (this.subobjects == null)
			{
				if (resetDirtyFlag)
				{
					this.dirty = false;
				}
				return null;
			}
			byte[] result = AttachmentBlob.Serialize(from so in this.subobjects
			where !so.IsDeleted
			select new KeyValuePair<int, long>(so.IsChild ? so.ChildNumber : int.MinValue, so.Inid), renumberChildren);
			if (resetDirtyFlag)
			{
				this.dirty = false;
			}
			return result;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000604AC File Offset: 0x0005E6AC
		public byte[] SerializeChildren()
		{
			if (this.subobjects == null)
			{
				return null;
			}
			return AttachmentBlob.Serialize(from so in this.subobjects
			where !so.IsDeleted && so.IsChild
			select new KeyValuePair<int, long>(so.IsChild ? so.ChildNumber : int.MinValue, so.Inid), false);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00060513 File Offset: 0x0005E713
		[Conditional("DEBUG")]
		internal void AssertHasChild(long inid)
		{
			this.FindDescendant(inid);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00060520 File Offset: 0x0005E720
		[Conditional("DEBUG")]
		internal void AssertHasAllDescendants(SubobjectCollection otherCollection)
		{
			if (otherCollection != null && otherCollection.subobjects != null)
			{
				foreach (SubobjectCollection.SubobjectEntry subobjectEntry in otherCollection.subobjects)
				{
					if (!subobjectEntry.IsDeleted)
					{
						this.FindDescendant(subobjectEntry.Inid);
					}
				}
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00060590 File Offset: 0x0005E790
		private int FindChild(int childNumber)
		{
			int result = -1;
			if (this.subobjects != null)
			{
				for (int i = 0; i < this.subobjects.Count; i++)
				{
					if (!this.subobjects[i].IsDeleted && this.subobjects[i].IsChild && this.subobjects[i].ChildNumber == childNumber)
					{
						result = i;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00060608 File Offset: 0x0005E808
		private int FindDescendant(long inid)
		{
			int result = -1;
			if (this.subobjects != null)
			{
				for (int i = 0; i < this.subobjects.Count; i++)
				{
					if (this.subobjects[i].Inid == inid)
					{
						result = i;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00060651 File Offset: 0x0005E851
		private void CheckMaxDescendantCount()
		{
			if (this.DescendantCount >= AttachmentBlob.MaxSupportedDescendantCountWrite)
			{
				throw new StoreException((LID)63328U, ErrorCodeValue.MaxAttachmentExceeded);
			}
		}

		// Token: 0x04000582 RID: 1410
		private readonly Item item;

		// Token: 0x04000583 RID: 1411
		private List<SubobjectCollection.SubobjectEntry> subobjects;

		// Token: 0x04000584 RID: 1412
		private bool dirty;

		// Token: 0x04000585 RID: 1413
		private int nextChildNumber;

		// Token: 0x04000586 RID: 1414
		private int descendantCount;

		// Token: 0x020000DA RID: 218
		private struct SubobjectEntry
		{
			// Token: 0x06000BF1 RID: 3057 RVA: 0x00060675 File Offset: 0x0005E875
			private SubobjectEntry(long inid, bool isDeleted, bool isNew, bool isChild, int childNumber, long size)
			{
				this.inid = inid;
				this.isDeleted = isDeleted;
				this.isNew = isNew;
				this.isChild = isChild;
				this.childNumber = childNumber;
				this.size = size;
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x000606A4 File Offset: 0x0005E8A4
			public long Inid
			{
				get
				{
					return this.inid;
				}
			}

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x000606AC File Offset: 0x0005E8AC
			public int ChildNumber
			{
				get
				{
					return this.childNumber;
				}
			}

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x000606B4 File Offset: 0x0005E8B4
			public long Size
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x000606BC File Offset: 0x0005E8BC
			public bool IsDeleted
			{
				get
				{
					return this.isDeleted;
				}
			}

			// Token: 0x17000265 RID: 613
			// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x000606C4 File Offset: 0x0005E8C4
			public bool IsChild
			{
				get
				{
					return this.isChild;
				}
			}

			// Token: 0x17000266 RID: 614
			// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x000606CC File Offset: 0x0005E8CC
			public bool IsNew
			{
				get
				{
					return this.isNew;
				}
			}

			// Token: 0x06000BF8 RID: 3064 RVA: 0x000606D4 File Offset: 0x0005E8D4
			public static SubobjectCollection.SubobjectEntry ConstructDeserialized(KeyValuePair<int, long> deserializedPair)
			{
				return new SubobjectCollection.SubobjectEntry(deserializedPair.Value, false, false, deserializedPair.Key >= 0, deserializedPair.Key, -1L);
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x000606FA File Offset: 0x0005E8FA
			public static SubobjectCollection.SubobjectEntry ConstructNew(long inid)
			{
				return new SubobjectCollection.SubobjectEntry(inid, false, true, false, -1, -1L);
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x00060708 File Offset: 0x0005E908
			public static SubobjectCollection.SubobjectEntry ConstructNewChild(long inid, int childNumber, long size)
			{
				return new SubobjectCollection.SubobjectEntry(inid, false, true, true, childNumber, size);
			}

			// Token: 0x06000BFB RID: 3067 RVA: 0x00060715 File Offset: 0x0005E915
			public static SubobjectCollection.SubobjectEntry ConstructDeleted(SubobjectCollection.SubobjectEntry originalSubobjectEntry)
			{
				return new SubobjectCollection.SubobjectEntry(originalSubobjectEntry.Inid, true, originalSubobjectEntry.IsNew, originalSubobjectEntry.IsChild, originalSubobjectEntry.ChildNumber, originalSubobjectEntry.Size);
			}

			// Token: 0x06000BFC RID: 3068 RVA: 0x00060740 File Offset: 0x0005E940
			public static SubobjectCollection.SubobjectEntry ConstructDeleted(SubobjectCollection.SubobjectEntry originalSubobjectEntry, long size)
			{
				return new SubobjectCollection.SubobjectEntry(originalSubobjectEntry.Inid, true, originalSubobjectEntry.IsNew, originalSubobjectEntry.IsChild, originalSubobjectEntry.ChildNumber, size);
			}

			// Token: 0x06000BFD RID: 3069 RVA: 0x00060765 File Offset: 0x0005E965
			public static SubobjectCollection.SubobjectEntry ConstructResurected(SubobjectCollection.SubobjectEntry originalSubobjectEntry)
			{
				return new SubobjectCollection.SubobjectEntry(originalSubobjectEntry.Inid, false, originalSubobjectEntry.IsNew, originalSubobjectEntry.IsChild, originalSubobjectEntry.ChildNumber, originalSubobjectEntry.Size);
			}

			// Token: 0x06000BFE RID: 3070 RVA: 0x00060790 File Offset: 0x0005E990
			public static SubobjectCollection.SubobjectEntry ConstructChildChangeSize(SubobjectCollection.SubobjectEntry originalSubobjectEntry, long newSize)
			{
				return new SubobjectCollection.SubobjectEntry(originalSubobjectEntry.Inid, originalSubobjectEntry.IsDeleted, originalSubobjectEntry.IsNew, originalSubobjectEntry.IsChild, originalSubobjectEntry.ChildNumber, newSize);
			}

			// Token: 0x06000BFF RID: 3071 RVA: 0x000607BB File Offset: 0x0005E9BB
			public static SubobjectCollection.SubobjectEntry ConstructCommitted(SubobjectCollection.SubobjectEntry originalSubobjectEntry)
			{
				return new SubobjectCollection.SubobjectEntry(originalSubobjectEntry.Inid, originalSubobjectEntry.IsDeleted, false, originalSubobjectEntry.IsChild, originalSubobjectEntry.ChildNumber, originalSubobjectEntry.Size);
			}

			// Token: 0x0400058C RID: 1420
			private long inid;

			// Token: 0x0400058D RID: 1421
			private bool isDeleted;

			// Token: 0x0400058E RID: 1422
			private bool isNew;

			// Token: 0x0400058F RID: 1423
			private bool isChild;

			// Token: 0x04000590 RID: 1424
			private int childNumber;

			// Token: 0x04000591 RID: 1425
			private long size;
		}
	}
}
