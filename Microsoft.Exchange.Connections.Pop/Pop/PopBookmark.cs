using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PopBookmark
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002894 File Offset: 0x00000A94
		public PopBookmark(string encoded)
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000028F4 File Offset: 0x00000AF4
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000290F File Offset: 0x00000B0F
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002917 File Offset: 0x00000B17
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

		// Token: 0x06000028 RID: 40 RVA: 0x00002920 File Offset: 0x00000B20
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

		// Token: 0x06000029 RID: 41 RVA: 0x000029CE File Offset: 0x00000BCE
		internal static PopBookmark Parse(string encoded)
		{
			return new PopBookmark(encoded);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029D6 File Offset: 0x00000BD6
		internal bool SetCapacity(int capacity)
		{
			if (capacity != 0 && this.items == null)
			{
				this.items = new Dictionary<string, string>(capacity);
				return true;
			}
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000029F2 File Offset: 0x00000BF2
		internal bool Contains(string item)
		{
			return this.items != null && this.items.ContainsKey(item);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A0A File Offset: 0x00000C0A
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

		// Token: 0x0600002D RID: 45 RVA: 0x00002A43 File Offset: 0x00000C43
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

		// Token: 0x0600002E RID: 46 RVA: 0x00002A67 File Offset: 0x00000C67
		internal void Clear()
		{
			if (this.items == null || this.items.Count == 0)
			{
				return;
			}
			this.items.Clear();
			this.hasChanged = true;
		}

		// Token: 0x04000015 RID: 21
		private const int MinEncodedLength = 4;

		// Token: 0x04000016 RID: 22
		private static char separator = ' ';

		// Token: 0x04000017 RID: 23
		private static char[] separators = new char[]
		{
			PopBookmark.separator
		};

		// Token: 0x04000018 RID: 24
		private static string[] empty = new string[0];

		// Token: 0x04000019 RID: 25
		private Dictionary<string, string> items;

		// Token: 0x0400001A RID: 26
		private bool hasChanged;
	}
}
