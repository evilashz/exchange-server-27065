using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000004 RID: 4
	public class ObjectListSource : IListSource
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002131 File Offset: 0x00000331
		public ObjectListSource(Array values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.values = values;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000214E File Offset: 0x0000034E
		public ObjectListSource(Array values, string displayProperty, string valueProperty) : this(values)
		{
			this.displayProperty = displayProperty;
			this.valueProperty = valueProperty;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002168 File Offset: 0x00000368
		public IList GetList()
		{
			ArrayList arrayList = new ArrayList(this.Values.Length);
			foreach (object objectValue in this.Values)
			{
				arrayList.Add(new ObjectListSourceItem(this.GetValueText(objectValue), this.GetValue(objectValue)));
			}
			if (arrayList.Count >= 13)
			{
				arrayList.Sort(this.comparer);
			}
			return arrayList;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021F8 File Offset: 0x000003F8
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002200 File Offset: 0x00000400
		public IComparer Comparer
		{
			get
			{
				return this.comparer;
			}
			set
			{
				this.comparer = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002209 File Offset: 0x00000409
		public bool ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000220C File Offset: 0x0000040C
		public Array Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002214 File Offset: 0x00000414
		protected virtual string GetValueText(object objectValue)
		{
			string result = string.Empty;
			if (objectValue != null)
			{
				if (!string.IsNullOrEmpty(this.displayProperty))
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(objectValue)[this.displayProperty];
					result = string.Format("{0}", propertyDescriptor.GetValue(objectValue));
				}
				else
				{
					result = objectValue.ToString();
				}
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002268 File Offset: 0x00000468
		private object GetValue(object objectValue)
		{
			object result = objectValue;
			if (!string.IsNullOrEmpty(this.valueProperty) && objectValue != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(objectValue)[this.valueProperty];
				result = propertyDescriptor.GetValue(objectValue);
			}
			return result;
		}

		// Token: 0x04000004 RID: 4
		private const int SortThreshold = 13;

		// Token: 0x04000005 RID: 5
		public const string ValueMemberColumnName = "Value";

		// Token: 0x04000006 RID: 6
		public const string DisplayMemberColumnName = "Text";

		// Token: 0x04000007 RID: 7
		private string displayProperty;

		// Token: 0x04000008 RID: 8
		private string valueProperty;

		// Token: 0x04000009 RID: 9
		private IComparer comparer;

		// Token: 0x0400000A RID: 10
		private Array values;
	}
}
