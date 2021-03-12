using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.Pop
{
	// Token: 0x020001EC RID: 492
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PopBookmark
	{
		// Token: 0x06001033 RID: 4147 RVA: 0x00032C68 File Offset: 0x00030E68
		private PopBookmark(string encoded)
		{
			if (string.IsNullOrEmpty(encoded))
			{
				return;
			}
			string[] array = encoded.Split(PopBookmark.separators);
			int num = int.Parse(array[0]);
			if (num > 0)
			{
				this.items = new Dictionary<string, string>(num);
				for (int i = 1; i < array.Length; i++)
				{
					this.items[array[i]] = array[i];
				}
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00032CC8 File Offset: 0x00030EC8
		internal ICollection<string> Values
		{
			get
			{
				if (this.items == null)
				{
					return PopBookmark.empty;
				}
				return this.items.Keys;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00032CE3 File Offset: 0x00030EE3
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x00032CEB File Offset: 0x00030EEB
		internal bool HasChanged
		{
			get
			{
				return this.hasChanged;
			}
			set
			{
				this.hasChanged = false;
			}
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00032CF4 File Offset: 0x00030EF4
		internal static PopBookmark Parse(string encoded)
		{
			return new PopBookmark(encoded);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00032CFC File Offset: 0x00030EFC
		internal bool SetCapacity(int capacity)
		{
			if (capacity != 0 && this.items == null)
			{
				this.items = new Dictionary<string, string>(capacity);
				return true;
			}
			return false;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00032D18 File Offset: 0x00030F18
		internal bool Contains(string item)
		{
			return this.items != null && this.items.ContainsKey(item);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00032D30 File Offset: 0x00030F30
		internal void Add(string item)
		{
			if (this.items == null)
			{
				this.items = new Dictionary<string, string>(1);
			}
			if (this.items.ContainsKey(item))
			{
				return;
			}
			this.items.Add(item, item);
			this.hasChanged = true;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00032D69 File Offset: 0x00030F69
		internal bool Remove(string item)
		{
			if (this.items == null)
			{
				return false;
			}
			if (this.items.Remove(item))
			{
				this.hasChanged = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00032D8D File Offset: 0x00030F8D
		internal void Clear()
		{
			if (this.items == null || this.items.Count == 0)
			{
				return;
			}
			this.items.Clear();
			this.hasChanged = true;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00032DB8 File Offset: 0x00030FB8
		public override string ToString()
		{
			if (this.items == null || this.items.Count == 0)
			{
				return string.Empty;
			}
			IEnumerator<string> enumerator = this.items.Keys.GetEnumerator();
			enumerator.MoveNext();
			int num = 4;
			int num2 = enumerator.Current.Length + 1;
			int num3 = this.items.Count * num2;
			int capacity = num + num3;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			stringBuilder.Append(this.items.Count);
			do
			{
				stringBuilder.Append(PopBookmark.separator);
				stringBuilder.Append(enumerator.Current);
			}
			while (enumerator.MoveNext());
			return stringBuilder.ToString();
		}

		// Token: 0x040008D1 RID: 2257
		private const int MinEncodedLength = 4;

		// Token: 0x040008D2 RID: 2258
		private static char separator = ' ';

		// Token: 0x040008D3 RID: 2259
		private static char[] separators = new char[]
		{
			PopBookmark.separator
		};

		// Token: 0x040008D4 RID: 2260
		private static string[] empty = new string[0];

		// Token: 0x040008D5 RID: 2261
		private Dictionary<string, string> items;

		// Token: 0x040008D6 RID: 2262
		private bool hasChanged;
	}
}
