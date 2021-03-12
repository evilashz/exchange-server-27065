using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000419 RID: 1049
	internal class EventPayload : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x060034ED RID: 13549 RVA: 0x000CDA31 File Offset: 0x000CBC31
		internal EventPayload(List<string> payloadNames, List<object> payloadValues)
		{
			this.m_names = payloadNames;
			this.m_values = payloadValues;
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000CDA47 File Offset: 0x000CBC47
		public ICollection<string> Keys
		{
			get
			{
				return this.m_names;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000CDA4F File Offset: 0x000CBC4F
		public ICollection<object> Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x170007E9 RID: 2025
		public object this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = 0;
				foreach (string a in this.m_names)
				{
					if (a == key)
					{
						return this.m_values[num];
					}
					num++;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000CDADF File Offset: 0x000CBCDF
		public void Add(string key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000CDAE6 File Offset: 0x000CBCE6
		public void Add(KeyValuePair<string, object> payloadEntry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000CDAED File Offset: 0x000CBCED
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000CDAF4 File Offset: 0x000CBCF4
		public bool Contains(KeyValuePair<string, object> entry)
		{
			return this.ContainsKey(entry.Key);
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000CDB04 File Offset: 0x000CBD04
		public bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			foreach (string a in this.m_names)
			{
				if (a == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000CDB70 File Offset: 0x000CBD70
		public int Count
		{
			get
			{
				return this.m_names.Count;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x000CDB7D File Offset: 0x000CBD7D
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000CDB80 File Offset: 0x000CBD80
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Keys.Count; i = num + 1)
			{
				yield return new KeyValuePair<string, object>(this.m_names[i], this.m_values[i]);
				num = i;
			}
			yield break;
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000CDB90 File Offset: 0x000CBD90
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000CDBA5 File Offset: 0x000CBDA5
		public void CopyTo(KeyValuePair<string, object>[] payloadEntries, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000CDBAC File Offset: 0x000CBDAC
		public bool Remove(string key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000CDBB3 File Offset: 0x000CBDB3
		public bool Remove(KeyValuePair<string, object> entry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000CDBBC File Offset: 0x000CBDBC
		public bool TryGetValue(string key, out object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = 0;
			foreach (string a in this.m_names)
			{
				if (a == key)
				{
					value = this.m_values[num];
					return true;
				}
				num++;
			}
			value = null;
			return false;
		}

		// Token: 0x04001774 RID: 6004
		private List<string> m_names;

		// Token: 0x04001775 RID: 6005
		private List<object> m_values;
	}
}
