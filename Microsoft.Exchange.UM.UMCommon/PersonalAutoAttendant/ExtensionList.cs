using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000FC RID: 252
	internal class ExtensionList : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable, IDataItem
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x0001F6FC File Offset: 0x0001D8FC
		internal ExtensionList()
		{
			this.extensions = new List<string>();
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001F70F File Offset: 0x0001D90F
		public int Count
		{
			get
			{
				return this.extensions.Count;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0001F71C File Offset: 0x0001D91C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001F71F File Offset: 0x0001D91F
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0001F727 File Offset: 0x0001D927
		public PAAValidationResult ValidationResult
		{
			get
			{
				return this.validationResult;
			}
			set
			{
				this.validationResult = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		public string this[int index]
		{
			get
			{
				return this.extensions[index];
			}
			set
			{
				this.extensions[index] = value;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001F74D File Offset: 0x0001D94D
		public int IndexOf(string item)
		{
			return this.extensions.IndexOf(item);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001F75B File Offset: 0x0001D95B
		public void Insert(int index, string item)
		{
			this.extensions.Insert(index, item);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001F76A File Offset: 0x0001D96A
		public void RemoveAt(int index)
		{
			this.extensions.RemoveAt(index);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001F778 File Offset: 0x0001D978
		public void Add(string item)
		{
			this.extensions.Add(item);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001F786 File Offset: 0x0001D986
		public void Clear()
		{
			this.extensions.Clear();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001F793 File Offset: 0x0001D993
		public bool Contains(string item)
		{
			return this.extensions.Contains(item);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001F7A1 File Offset: 0x0001D9A1
		public void CopyTo(string[] array, int arrayIndex)
		{
			this.extensions.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001F7B0 File Offset: 0x0001D9B0
		public bool Remove(string item)
		{
			return this.extensions.Remove(item);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001F7BE File Offset: 0x0001D9BE
		public IEnumerator<string> GetEnumerator()
		{
			return this.extensions.GetEnumerator();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001F7D0 File Offset: 0x0001D9D0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.extensions.GetEnumerator();
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001F7E4 File Offset: 0x0001D9E4
		public bool Validate(IDataValidator dataValidator)
		{
			string text;
			return dataValidator.ValidateExtensions(this.extensions, out this.validationResult, out text);
		}

		// Token: 0x040004BF RID: 1215
		private List<string> extensions;

		// Token: 0x040004C0 RID: 1216
		private PAAValidationResult validationResult;
	}
}
