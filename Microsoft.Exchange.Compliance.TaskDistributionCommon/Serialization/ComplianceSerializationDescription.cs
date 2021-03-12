using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization
{
	// Token: 0x0200005F RID: 95
	public class ComplianceSerializationDescription<T> where T : new()
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000D22C File Offset: 0x0000B42C
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000D234 File Offset: 0x0000B434
		public byte ComplianceStructureId { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000D23D File Offset: 0x0000B43D
		public int TotalByteFields
		{
			get
			{
				return this.byteGetters.Count;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000D24A File Offset: 0x0000B44A
		public int TotalShortFields
		{
			get
			{
				return this.shortGetters.Count;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000D257 File Offset: 0x0000B457
		public int TotalIntegerFields
		{
			get
			{
				return this.integerGetters.Count;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000D264 File Offset: 0x0000B464
		public int TotalLongFields
		{
			get
			{
				return this.longGetters.Count;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000D271 File Offset: 0x0000B471
		public int TotalDoubleFields
		{
			get
			{
				return this.doubleGetters.Count;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000D27E File Offset: 0x0000B47E
		public int TotalGuidFields
		{
			get
			{
				return this.guidGetters.Count;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000D28B File Offset: 0x0000B48B
		public int TotalStringFields
		{
			get
			{
				return this.stringGetters.Count;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000D298 File Offset: 0x0000B498
		public int TotalBlobFields
		{
			get
			{
				return this.blobGetters.Count;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000D2A5 File Offset: 0x0000B4A5
		public int TotalCollectionFields
		{
			get
			{
				return this.collectionItemTypeGetters.Count;
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D2B2 File Offset: 0x0000B4B2
		public void RegisterBytePropertyGetterAndSetter(byte fieldIndex, Func<T, byte> getter, Action<T, byte> setter)
		{
			if (getter == null || setter == null)
			{
				throw new ArgumentNullException();
			}
			this.byteGetters.Add(getter);
			this.byteSetters.Add(setter);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		public void RegisterShortPropertyGetterAndSetter(byte fieldIndex, Func<T, short> getter, Action<T, short> setter)
		{
			if (getter == null || setter == null)
			{
				throw new ArgumentNullException();
			}
			this.shortGetters.Add(getter);
			this.shortSetters.Add(setter);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D2FE File Offset: 0x0000B4FE
		public void RegisterIntegerPropertyGetterAndSetter(byte fieldIndex, Func<T, int> getter, Action<T, int> setter)
		{
			if (getter == null || setter == null)
			{
				throw new ArgumentNullException();
			}
			this.integerGetters.Add(getter);
			this.integerSetters.Add(setter);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D324 File Offset: 0x0000B524
		public void RegisterLongPropertyGetterAndSetter(byte fieldIndex, Func<T, long> getter, Action<T, long> setter)
		{
			if (getter == null || setter == null)
			{
				throw new ArgumentNullException();
			}
			this.longGetters.Add(getter);
			this.longSetters.Add(setter);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D34A File Offset: 0x0000B54A
		public void RegisterDoublePropertyGetterAndSetter(byte fieldIndex, Func<T, double> getter, Action<T, double> setter)
		{
			if (getter == null || setter == null)
			{
				throw new ArgumentNullException();
			}
			this.doubleGetters.Add(getter);
			this.doubleSetters.Add(setter);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D370 File Offset: 0x0000B570
		public void RegisterGuidPropertyGetterAndSetter(byte fieldIndex, Func<T, Guid> getter, Action<T, Guid> setter)
		{
			if (getter == null || setter == null)
			{
				throw new ArgumentNullException();
			}
			this.guidGetters.Add(getter);
			this.guidSetters.Add(setter);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D396 File Offset: 0x0000B596
		public void RegisterStringPropertyGetterAndSetter(byte fieldIndex, Func<T, string> getter, Action<T, string> setter)
		{
			if (getter == null || setter == null)
			{
				throw new ArgumentNullException();
			}
			this.stringGetters.Add(getter);
			this.stringSetters.Add(setter);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		public void RegisterBlobPropertyGetterAndSetter(byte fieldIndex, Func<T, byte[]> getter, Action<T, byte[]> setter)
		{
			if (getter == null || setter == null)
			{
				throw new ArgumentNullException();
			}
			this.blobGetters.Add(getter);
			this.blobSetters.Add(setter);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D3E2 File Offset: 0x0000B5E2
		public void RegisterCollectionPropertyAccessors(byte fieldIndex, Func<CollectionItemType> itemTypeGetter, Func<T, int> itemCountGetter, Func<T, int, object> itemGetter, Action<T, object, int> itemAdder)
		{
			this.RegisterCollectionPropertyAccessors(fieldIndex, itemTypeGetter, itemCountGetter, itemGetter, itemAdder, null);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		public void RegisterCollectionPropertyAccessors(byte fieldIndex, Func<CollectionItemType> itemTypeGetter, Func<T, int> itemCountGetter, Func<T, int, object> itemGetter, Action<T, object, int> itemAdder, Action<T, int> collectionInitializer)
		{
			if (itemTypeGetter == null || itemCountGetter == null || itemGetter == null || itemAdder == null)
			{
				throw new ArgumentNullException();
			}
			this.collectionItemTypeGetters.Add(itemTypeGetter);
			this.collectionItemCountGetters.Add(itemCountGetter);
			this.collectionItemGetters.Add(itemGetter);
			this.collectionItemAdders.Add(itemAdder);
			this.collectionInitializers.Add(collectionInitializer);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D49C File Offset: 0x0000B69C
		public void RegisterComplexCollectionAccessor<I>(byte fieldIndex, Func<T, int> itemCountGetter, Func<T, int, I> itemGetter, Action<T, I, int> itemAdder, ComplianceSerializationDescription<I> listItemDescription) where I : class, new()
		{
			this.RegisterCollectionPropertyAccessors(fieldIndex, () => CollectionItemType.Blob, itemCountGetter, (T item, int index) => ComplianceSerializer.Serialize<I>(listItemDescription, itemGetter(item, index)), delegate(T item, object obj, int index)
			{
				itemAdder(item, ComplianceSerializer.DeSerialize<I>(listItemDescription, (byte[])obj), index);
			});
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D530 File Offset: 0x0000B730
		public void RegisterComplexPropertyAsBlobGetterAndSetter<I>(byte fieldIndex, Func<T, I> getter, Action<T, I> setter, ComplianceSerializationDescription<I> itemDescription) where I : class, new()
		{
			this.RegisterBlobPropertyGetterAndSetter(fieldIndex, (T item) => ComplianceSerializer.Serialize<I>(itemDescription, getter(item)), delegate(T item, byte[] obj)
			{
				setter(item, ComplianceSerializer.DeSerialize<I>(itemDescription, obj));
			});
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D578 File Offset: 0x0000B778
		public bool TryGetByteProperty(T obj, byte fieldIndex, out byte value)
		{
			value = 0;
			if ((int)fieldIndex >= this.byteGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			value = this.byteGetters[(int)fieldIndex](obj);
			return true;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
		public bool TrySetByteProperty(T obj, byte fieldIndex, byte value)
		{
			if ((int)fieldIndex >= this.byteSetters.Count || fieldIndex < 0)
			{
				return false;
			}
			this.byteSetters[(int)fieldIndex](obj, value);
			return true;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		public bool TryGetShortProperty(T obj, byte fieldIndex, out short value)
		{
			value = 0;
			if ((int)fieldIndex >= this.shortGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			value = this.shortGetters[(int)fieldIndex](obj);
			return true;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D5FE File Offset: 0x0000B7FE
		public bool TrySetShortProperty(T obj, byte fieldIndex, short value)
		{
			if ((int)fieldIndex >= this.shortSetters.Count || fieldIndex < 0)
			{
				return false;
			}
			this.shortSetters[(int)fieldIndex](obj, value);
			return true;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D628 File Offset: 0x0000B828
		public bool TryGetIntegerProperty(T obj, byte fieldIndex, out int value)
		{
			value = 0;
			if ((int)fieldIndex >= this.integerGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			value = this.integerGetters[(int)fieldIndex](obj);
			return true;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000D656 File Offset: 0x0000B856
		public bool TrySetIntegerProperty(T obj, byte fieldIndex, int value)
		{
			if ((int)fieldIndex >= this.integerSetters.Count || fieldIndex < 0)
			{
				return false;
			}
			this.integerSetters[(int)fieldIndex](obj, value);
			return true;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000D680 File Offset: 0x0000B880
		public bool TryGetLongProperty(T obj, byte fieldIndex, out long value)
		{
			value = 0L;
			if ((int)fieldIndex >= this.longGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			value = this.longGetters[(int)fieldIndex](obj);
			return true;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000D6AF File Offset: 0x0000B8AF
		public bool TrySetLongProperty(T obj, byte fieldIndex, long value)
		{
			if ((int)fieldIndex >= this.longSetters.Count || fieldIndex < 0)
			{
				return false;
			}
			this.longSetters[(int)fieldIndex](obj, value);
			return true;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000D6D9 File Offset: 0x0000B8D9
		public bool TryGetDoubleProperty(T obj, byte fieldIndex, out double value)
		{
			value = 0.0;
			if ((int)fieldIndex >= this.doubleGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			value = this.doubleGetters[(int)fieldIndex](obj);
			return true;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000D70F File Offset: 0x0000B90F
		public bool TrySetDoubleProperty(T obj, byte fieldIndex, double value)
		{
			if ((int)fieldIndex >= this.doubleSetters.Count || fieldIndex < 0)
			{
				return false;
			}
			this.doubleSetters[(int)fieldIndex](obj, value);
			return true;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000D739 File Offset: 0x0000B939
		public bool TryGetGuidProperty(T obj, byte fieldIndex, out Guid value)
		{
			value = Guid.Empty;
			if ((int)fieldIndex >= this.guidGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			value = this.guidGetters[(int)fieldIndex](obj);
			return true;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000D773 File Offset: 0x0000B973
		public bool TrySetGuidProperty(T obj, byte fieldIndex, Guid value)
		{
			if ((int)fieldIndex >= this.guidSetters.Count || fieldIndex < 0)
			{
				return false;
			}
			this.guidSetters[(int)fieldIndex](obj, value);
			return true;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000D79D File Offset: 0x0000B99D
		public bool TryGetStringProperty(T obj, byte fieldIndex, out string value)
		{
			value = string.Empty;
			if ((int)fieldIndex >= this.stringGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			value = this.stringGetters[(int)fieldIndex](obj);
			return true;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000D7CF File Offset: 0x0000B9CF
		public bool TrySetStringProperty(T obj, byte fieldIndex, string value)
		{
			if ((int)fieldIndex >= this.stringSetters.Count || fieldIndex < 0)
			{
				return false;
			}
			this.stringSetters[(int)fieldIndex](obj, value);
			return true;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000D7F9 File Offset: 0x0000B9F9
		public bool TryGetBlobProperty(T obj, byte fieldIndex, out byte[] value)
		{
			value = null;
			if ((int)fieldIndex >= this.blobGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			value = this.blobGetters[(int)fieldIndex](obj);
			return true;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000D827 File Offset: 0x0000BA27
		public bool TrySetBlobProperty(T obj, byte fieldIndex, byte[] value)
		{
			if ((int)fieldIndex >= this.blobSetters.Count || fieldIndex < 0)
			{
				return false;
			}
			this.blobSetters[(int)fieldIndex](obj, value);
			return true;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000D851 File Offset: 0x0000BA51
		public bool TryGetCollectionPropertyItemType(byte fieldIndex, out CollectionItemType type)
		{
			type = CollectionItemType.NotDefined;
			if ((int)fieldIndex >= this.collectionItemTypeGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			type = this.collectionItemTypeGetters[(int)fieldIndex]();
			return true;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000D880 File Offset: 0x0000BA80
		public IEnumerable<object> GetCollectionItems(T obj, byte fieldIndex)
		{
			List<object> list = new List<object>();
			if ((int)fieldIndex >= this.collectionItemTypeGetters.Count || fieldIndex < 0)
			{
				return list;
			}
			int num = this.collectionItemCountGetters[(int)fieldIndex](obj);
			for (int i = 0; i < num; i++)
			{
				object item = this.collectionItemGetters[(int)fieldIndex](obj, i);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public bool TrySetCollectionItems(T obj, byte fieldIndex, IList<object> items)
		{
			if ((int)fieldIndex >= this.collectionItemTypeGetters.Count || fieldIndex < 0)
			{
				return false;
			}
			if (this.collectionInitializers[(int)fieldIndex] != null)
			{
				this.collectionInitializers[(int)fieldIndex](obj, items.Count);
			}
			int num = 0;
			foreach (object arg in items)
			{
				this.collectionItemAdders[(int)fieldIndex](obj, arg, num++);
			}
			return true;
		}

		// Token: 0x04000216 RID: 534
		private List<Action<T, byte>> byteSetters = new List<Action<T, byte>>();

		// Token: 0x04000217 RID: 535
		private List<Func<T, byte>> byteGetters = new List<Func<T, byte>>();

		// Token: 0x04000218 RID: 536
		private List<Action<T, short>> shortSetters = new List<Action<T, short>>();

		// Token: 0x04000219 RID: 537
		private List<Func<T, short>> shortGetters = new List<Func<T, short>>();

		// Token: 0x0400021A RID: 538
		private List<Action<T, int>> integerSetters = new List<Action<T, int>>();

		// Token: 0x0400021B RID: 539
		private List<Func<T, int>> integerGetters = new List<Func<T, int>>();

		// Token: 0x0400021C RID: 540
		private List<Action<T, long>> longSetters = new List<Action<T, long>>();

		// Token: 0x0400021D RID: 541
		private List<Func<T, long>> longGetters = new List<Func<T, long>>();

		// Token: 0x0400021E RID: 542
		private List<Action<T, double>> doubleSetters = new List<Action<T, double>>();

		// Token: 0x0400021F RID: 543
		private List<Func<T, double>> doubleGetters = new List<Func<T, double>>();

		// Token: 0x04000220 RID: 544
		private List<Action<T, Guid>> guidSetters = new List<Action<T, Guid>>();

		// Token: 0x04000221 RID: 545
		private List<Func<T, Guid>> guidGetters = new List<Func<T, Guid>>();

		// Token: 0x04000222 RID: 546
		private List<Action<T, string>> stringSetters = new List<Action<T, string>>();

		// Token: 0x04000223 RID: 547
		private List<Func<T, string>> stringGetters = new List<Func<T, string>>();

		// Token: 0x04000224 RID: 548
		private List<Action<T, byte[]>> blobSetters = new List<Action<T, byte[]>>();

		// Token: 0x04000225 RID: 549
		private List<Func<T, byte[]>> blobGetters = new List<Func<T, byte[]>>();

		// Token: 0x04000226 RID: 550
		private List<Func<CollectionItemType>> collectionItemTypeGetters = new List<Func<CollectionItemType>>();

		// Token: 0x04000227 RID: 551
		private List<Func<T, int>> collectionItemCountGetters = new List<Func<T, int>>();

		// Token: 0x04000228 RID: 552
		private List<Func<T, int, object>> collectionItemGetters = new List<Func<T, int, object>>();

		// Token: 0x04000229 RID: 553
		private List<Action<T, object, int>> collectionItemAdders = new List<Action<T, object, int>>();

		// Token: 0x0400022A RID: 554
		private List<Action<T, int>> collectionInitializers = new List<Action<T, int>>();
	}
}
