using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000034 RID: 52
	public class ObjectListSourceItem : IComparable
	{
		// Token: 0x06000215 RID: 533 RVA: 0x000087EC File Offset: 0x000069EC
		public ObjectListSourceItem(string text, object value)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException("Text can not be null or empty", "text");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.value = value;
			this.text = text;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00008828 File Offset: 0x00006A28
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00008830 File Offset: 0x00006A30
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008838 File Offset: 0x00006A38
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			ObjectListSourceItem objectListSourceItem = obj as ObjectListSourceItem;
			if (objectListSourceItem == null)
			{
				throw new ArgumentException();
			}
			return this.Text.CompareTo(objectListSourceItem.Text);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000886C File Offset: 0x00006A6C
		public override bool Equals(object obj)
		{
			ObjectListSourceItem objectListSourceItem = obj as ObjectListSourceItem;
			return objectListSourceItem != null && this.Value.Equals(objectListSourceItem.Value);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008896 File Offset: 0x00006A96
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x0400007E RID: 126
		private string text;

		// Token: 0x0400007F RID: 127
		private object value;
	}
}
