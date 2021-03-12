using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D5 RID: 2517
	internal sealed class CLRIReferenceArrayImpl<T> : CLRIPropertyValueImpl, IReferenceArray<T>, IPropertyValue, ICustomPropertyProvider, IList, ICollection, IEnumerable
	{
		// Token: 0x0600641C RID: 25628 RVA: 0x001541A7 File Offset: 0x001523A7
		public CLRIReferenceArrayImpl(PropertyType type, T[] obj) : base(type, obj)
		{
			this._value = obj;
			this._list = this._value;
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x0600641D RID: 25629 RVA: 0x001541C4 File Offset: 0x001523C4
		public T[] Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x001541CC File Offset: 0x001523CC
		public override string ToString()
		{
			if (this._value != null)
			{
				return this._value.ToString();
			}
			return base.ToString();
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x001541E8 File Offset: 0x001523E8
		ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
		{
			return ICustomPropertyProviderImpl.CreateProperty(this._value, name);
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x001541F6 File Offset: 0x001523F6
		ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
		{
			return ICustomPropertyProviderImpl.CreateIndexedProperty(this._value, name, indexParameterType);
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x00154205 File Offset: 0x00152405
		string ICustomPropertyProvider.GetStringRepresentation()
		{
			return this._value.ToString();
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06006422 RID: 25634 RVA: 0x00154212 File Offset: 0x00152412
		Type ICustomPropertyProvider.Type
		{
			get
			{
				return this._value.GetType();
			}
		}

		// Token: 0x06006423 RID: 25635 RVA: 0x0015421F File Offset: 0x0015241F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._value.GetEnumerator();
		}

		// Token: 0x1700114E RID: 4430
		object IList.this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x00154249 File Offset: 0x00152449
		int IList.Add(object value)
		{
			return this._list.Add(value);
		}

		// Token: 0x06006427 RID: 25639 RVA: 0x00154257 File Offset: 0x00152457
		bool IList.Contains(object value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06006428 RID: 25640 RVA: 0x00154265 File Offset: 0x00152465
		void IList.Clear()
		{
			this._list.Clear();
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06006429 RID: 25641 RVA: 0x00154272 File Offset: 0x00152472
		bool IList.IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x0600642A RID: 25642 RVA: 0x0015427F File Offset: 0x0015247F
		bool IList.IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x0015428C File Offset: 0x0015248C
		int IList.IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x0015429A File Offset: 0x0015249A
		void IList.Insert(int index, object value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x0600642D RID: 25645 RVA: 0x001542A9 File Offset: 0x001524A9
		void IList.Remove(object value)
		{
			this._list.Remove(value);
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x001542B7 File Offset: 0x001524B7
		void IList.RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x001542C5 File Offset: 0x001524C5
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06006430 RID: 25648 RVA: 0x001542D4 File Offset: 0x001524D4
		int ICollection.Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06006431 RID: 25649 RVA: 0x001542E1 File Offset: 0x001524E1
		object ICollection.SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06006432 RID: 25650 RVA: 0x001542EE File Offset: 0x001524EE
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x001542FC File Offset: 0x001524FC
		[FriendAccessAllowed]
		internal static object UnboxHelper(object wrapper)
		{
			IReferenceArray<T> referenceArray = (IReferenceArray<T>)wrapper;
			return referenceArray.Value;
		}

		// Token: 0x04002C7A RID: 11386
		private T[] _value;

		// Token: 0x04002C7B RID: 11387
		private IList _list;
	}
}
