using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data.Configuration
{
	// Token: 0x02000002 RID: 2
	internal sealed class AppConfigCollection : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AppConfigCollection()
		{
			this.dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E8 File Offset: 0x000002E8
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020F5 File Offset: 0x000002F5
		bool ICollection<KeyValuePair<string, string>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public ICollection<string> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002105 File Offset: 0x00000305
		public ICollection<string> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x17000005 RID: 5
		public string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (key.Length > 255)
				{
					throw new ArgumentOutOfRangeException("key");
				}
				return this.dictionary[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (key.Length > 255)
				{
					throw new ArgumentOutOfRangeException("key");
				}
				this.dictionary[key] = value;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217B File Offset: 0x0000037B
		public void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length > 255)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			this.dictionary.Add(key, value);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B0 File Offset: 0x000003B0
		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length > 255)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			return this.dictionary.Remove(key);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E4 File Offset: 0x000003E4
		public void Clear()
		{
			this.dictionary.Clear();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021F1 File Offset: 0x000003F1
		public bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length > 255)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002225 File Offset: 0x00000425
		public bool TryGetValue(string key, out string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length > 255)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000225A File Offset: 0x0000045A
		void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002270 File Offset: 0x00000470
		bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
		{
			if (item.Key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (item.Key.Length > 255)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			return ((ICollection<KeyValuePair<string, string>>)this.dictionary).Contains(item);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022B0 File Offset: 0x000004B0
		void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, string>>)this.dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022BF File Offset: 0x000004BF
		bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
		{
			if (item.Key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (item.Key.Length > 255)
			{
				throw new ArgumentOutOfRangeException("key");
			}
			return ((ICollection<KeyValuePair<string, string>>)this.dictionary).Remove(item);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022FF File Offset: 0x000004FF
		IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002311 File Offset: 0x00000511
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x04000001 RID: 1
		private Dictionary<string, string> dictionary;
	}
}
