using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000008 RID: 8
	public class DataList<T> : BindingList<T>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023E8 File Offset: 0x000005E8
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000023F0 File Offset: 0x000005F0
		public bool AllowSorting
		{
			get
			{
				return this.allowSorting;
			}
			set
			{
				if (this.AllowSorting != value)
				{
					this.allowSorting = value;
					base.ResetBindings();
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002408 File Offset: 0x00000608
		protected override bool SupportsSortingCore
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002410 File Offset: 0x00000610
		protected override ListSortDirection SortDirectionCore
		{
			get
			{
				return this.sortDirection;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002418 File Offset: 0x00000618
		protected override PropertyDescriptor SortPropertyCore
		{
			get
			{
				return this.sortProperty;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002420 File Offset: 0x00000620
		protected override bool IsSortedCore
		{
			get
			{
				return null != this.sortProperty;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002430 File Offset: 0x00000630
		protected override void InsertItem(int index, T item)
		{
			if (this.IsSortedCore)
			{
				List<T> list = (List<T>)base.Items;
				index = list.BinarySearch(item, new PropertyComparer<T>(this.SortPropertyCore, this.SortDirectionCore));
				if (index < 0)
				{
					index = ~index;
				}
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000247B File Offset: 0x0000067B
		protected override void SetItem(int index, T item)
		{
			base.SetItem(index, item);
			if (this.IsSortedCore)
			{
				this.ApplySortCore(this.sortProperty, this.sortDirection);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024A0 File Offset: 0x000006A0
		public void ApplySort(string propertyName, ListSortDirection direction)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
			PropertyDescriptor propertyDescriptor = properties[propertyName];
			if (propertyDescriptor == null)
			{
				throw new ArgumentOutOfRangeException("propertyName");
			}
			this.ApplySortCore(propertyDescriptor, direction);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024DC File Offset: 0x000006DC
		protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
		{
			if (!this.SupportsSortingCore)
			{
				throw new NotSupportedException();
			}
			List<T> list = (List<T>)base.Items;
			list.Sort(new PropertyComparer<T>(property, direction));
			this.sortProperty = property;
			this.sortDirection = direction;
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000252B File Offset: 0x0000072B
		protected override void RemoveSortCore()
		{
			this.sortProperty = null;
			this.sortDirection = ListSortDirection.Ascending;
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002548 File Offset: 0x00000748
		protected override bool SupportsSearchingCore
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000254C File Offset: 0x0000074C
		public int Find(string propertyName, object key)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
			PropertyDescriptor prop = properties[propertyName];
			return this.FindCore(prop, key);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000257C File Offset: 0x0000077C
		protected override int FindCore(PropertyDescriptor property, object key)
		{
			if (property != null)
			{
				for (int i = 0; i < base.Items.Count; i++)
				{
					object value = property.GetValue(base.Items[i]);
					if (StringComparer.OrdinalIgnoreCase.Compare(key, value) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025CC File Offset: 0x000007CC
		public T[] ToArray()
		{
			T[] array = new T[base.Count];
			base.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025F0 File Offset: 0x000007F0
		public void CopyFrom(IEnumerable enumerable)
		{
			bool raiseListChangedEvents = base.RaiseListChangedEvents;
			base.RaiseListChangedEvents = false;
			PropertyDescriptor propertyDescriptor = this.sortProperty;
			this.sortProperty = null;
			try
			{
				base.Clear();
				if (enumerable != null)
				{
					foreach (object obj in enumerable)
					{
						T item = (T)((object)obj);
						base.Add(item);
					}
				}
			}
			finally
			{
				base.RaiseListChangedEvents = raiseListChangedEvents;
				if (propertyDescriptor != null && this.SupportsSortingCore)
				{
					this.ApplySortCore(propertyDescriptor, this.SortDirectionCore);
				}
				else
				{
					base.ResetBindings();
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026A4 File Offset: 0x000008A4
		internal void FillWith(string commandText, MonadConnection connection)
		{
			using (MonadCommand monadCommand = new LoggableMonadCommand(commandText, connection))
			{
				this.FillWith(monadCommand);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026DC File Offset: 0x000008DC
		internal void FillWith(MonadCommand command)
		{
			using (new OpenConnection(command.Connection))
			{
				this.CopyFrom(command.Execute());
			}
		}

		// Token: 0x0400000C RID: 12
		private bool allowSorting = true;

		// Token: 0x0400000D RID: 13
		private ListSortDirection sortDirection;

		// Token: 0x0400000E RID: 14
		private PropertyDescriptor sortProperty;
	}
}
