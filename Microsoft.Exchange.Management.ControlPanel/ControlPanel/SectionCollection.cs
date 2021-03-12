using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000650 RID: 1616
	public sealed class SectionCollection : IList, ICollection, IEnumerable<Section>, IEnumerable
	{
		// Token: 0x06004686 RID: 18054 RVA: 0x000D55DE File Offset: 0x000D37DE
		internal SectionCollection(WebControl parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent", "Parent control cannot be null.");
			}
			this.parent = parent;
		}

		// Token: 0x1700272F RID: 10031
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x000D5600 File Offset: 0x000D3800
		public int Count
		{
			get
			{
				int num = 0;
				foreach (object obj in this.parent.Controls)
				{
					Control control = (Control)obj;
					if (control is Section)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17002730 RID: 10032
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x000D5668 File Offset: 0x000D3868
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002731 RID: 10033
		public Section this[int index]
		{
			get
			{
				return (Section)this.parent.Controls[this.ToRawIndex(index)];
			}
		}

		// Token: 0x17002732 RID: 10034
		public Section this[string id]
		{
			get
			{
				for (int i = 0; i < this.parent.Controls.Count; i++)
				{
					Section section = this.parent.Controls[i] as Section;
					if (section != null && section.ID == id)
					{
						return section;
					}
				}
				return null;
			}
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x000D56E0 File Offset: 0x000D38E0
		private int ToRawIndex(int paneIndex)
		{
			if (paneIndex < 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "paneIndex = {0} : paneIndex should not be negative", new object[]
				{
					paneIndex
				}));
			}
			int num = -1;
			for (int i = 0; i < this.parent.Controls.Count; i++)
			{
				if (this.parent.Controls[i] is Section && ++num == paneIndex)
				{
					return i;
				}
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "No Section at position {0}", new object[]
			{
				paneIndex
			}));
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x000D577C File Offset: 0x000D397C
		private int FromRawIndex(int index)
		{
			if (index < 0)
			{
				return -1;
			}
			if (index >= this.parent.Controls.Count || !(this.parent.Controls[index] is Section))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "No Section at position {0}", new object[]
				{
					index
				}));
			}
			int num = -1;
			while (index >= 0)
			{
				if (this.parent.Controls[index--] is Section)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x000D580A File Offset: 0x000D3A0A
		public void Add(Section item)
		{
			this.parent.Controls.Add(item);
			this.version++;
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x000D582C File Offset: 0x000D3A2C
		public void Clear()
		{
			for (int i = this.parent.Controls.Count - 1; i >= 0; i--)
			{
				if (this.parent.Controls[i] is Section)
				{
					this.parent.Controls.RemoveAt(i);
				}
			}
			this.version++;
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x000D588D File Offset: 0x000D3A8D
		public bool Contains(Section item)
		{
			return this.parent.Controls.Contains(item);
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x000D58A0 File Offset: 0x000D3AA0
		public void CopyTo(Array array, int index)
		{
			Section[] array2 = array as Section[];
			if (array2 == null)
			{
				throw new ArgumentException("Expected an array of Sections.");
			}
			this.CopyTo(array2, index);
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x000D58CC File Offset: 0x000D3ACC
		public void CopyTo(Section[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Cannot copy into a null array.");
			}
			int num = 0;
			for (int i = 0; i < this.parent.Controls.Count; i++)
			{
				Section section = this.parent.Controls[i] as Section;
				if (section != null)
				{
					if (num + index == array.Length)
					{
						throw new ArgumentException("Array is not large enough for the Sections");
					}
					array[num + index] = section;
					num++;
				}
			}
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x000D5940 File Offset: 0x000D3B40
		public int IndexOf(Section item)
		{
			return this.FromRawIndex(this.parent.Controls.IndexOf(item));
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x000D5959 File Offset: 0x000D3B59
		public void Insert(int index, Section item)
		{
			this.parent.Controls.AddAt(this.ToRawIndex(index), item);
			this.version++;
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x000D5981 File Offset: 0x000D3B81
		public void Remove(Section item)
		{
			this.parent.Controls.Remove(item);
			this.version++;
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x000D59A2 File Offset: 0x000D3BA2
		public void RemoveAt(int index)
		{
			this.parent.Controls.RemoveAt(this.ToRawIndex(index));
			this.version++;
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x000D59C9 File Offset: 0x000D3BC9
		int IList.Add(object value)
		{
			this.Add((Section)value);
			return this.IndexOf((Section)value);
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000D59E3 File Offset: 0x000D3BE3
		bool IList.Contains(object value)
		{
			return this.Contains((Section)value);
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x000D59F1 File Offset: 0x000D3BF1
		int IList.IndexOf(object value)
		{
			return this.IndexOf((Section)value);
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x000D59FF File Offset: 0x000D3BFF
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (Section)value);
		}

		// Token: 0x17002733 RID: 10035
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x000D5A0E File Offset: 0x000D3C0E
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x000D5A11 File Offset: 0x000D3C11
		void IList.Remove(object value)
		{
			this.Remove((Section)value);
		}

		// Token: 0x17002734 RID: 10036
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002735 RID: 10037
		// (get) Token: 0x0600469E RID: 18078 RVA: 0x000D5A2F File Offset: 0x000D3C2F
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002736 RID: 10038
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x000D5A32 File Offset: 0x000D3C32
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x000D5A35 File Offset: 0x000D3C35
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SectionCollection.SectionEnumerator(this);
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x000D5A3D File Offset: 0x000D3C3D
		public IEnumerator<Section> GetEnumerator()
		{
			return new SectionCollection.SectionEnumerator(this);
		}

		// Token: 0x04002FA9 RID: 12201
		private WebControl parent;

		// Token: 0x04002FAA RID: 12202
		private int version;

		// Token: 0x02000651 RID: 1617
		private class SectionEnumerator : IEnumerator<Section>, IDisposable, IEnumerator
		{
			// Token: 0x060046A2 RID: 18082 RVA: 0x000D5A45 File Offset: 0x000D3C45
			public SectionEnumerator(SectionCollection parent)
			{
				this.collection = parent;
				this.parentEnumerator = parent.parent.Controls.GetEnumerator();
				this.version = parent.version;
			}

			// Token: 0x060046A3 RID: 18083 RVA: 0x000D5A76 File Offset: 0x000D3C76
			private void CheckVersion()
			{
				if (this.version != this.collection.version)
				{
					throw new InvalidOperationException("Enumeration can't continue because the collection has been modified.");
				}
			}

			// Token: 0x060046A4 RID: 18084 RVA: 0x000D5A96 File Offset: 0x000D3C96
			public void Dispose()
			{
				this.parentEnumerator = null;
				this.collection = null;
			}

			// Token: 0x17002737 RID: 10039
			// (get) Token: 0x060046A5 RID: 18085 RVA: 0x000D5AA6 File Offset: 0x000D3CA6
			public Section Current
			{
				get
				{
					this.CheckVersion();
					return this.parentEnumerator.Current as Section;
				}
			}

			// Token: 0x17002738 RID: 10040
			// (get) Token: 0x060046A6 RID: 18086 RVA: 0x000D5ABE File Offset: 0x000D3CBE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060046A7 RID: 18087 RVA: 0x000D5AC8 File Offset: 0x000D3CC8
			public bool MoveNext()
			{
				this.CheckVersion();
				bool flag = this.parentEnumerator.MoveNext();
				if (flag && !(this.parentEnumerator.Current is Section))
				{
					flag = this.MoveNext();
				}
				return flag;
			}

			// Token: 0x060046A8 RID: 18088 RVA: 0x000D5B04 File Offset: 0x000D3D04
			public void Reset()
			{
				this.CheckVersion();
				this.parentEnumerator.Reset();
			}

			// Token: 0x04002FAB RID: 12203
			private SectionCollection collection;

			// Token: 0x04002FAC RID: 12204
			private IEnumerator parentEnumerator;

			// Token: 0x04002FAD RID: 12205
			private int version;
		}
	}
}
