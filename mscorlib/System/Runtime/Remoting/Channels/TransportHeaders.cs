using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000820 RID: 2080
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class TransportHeaders : ITransportHeaders
	{
		// Token: 0x06005913 RID: 22803 RVA: 0x00138B4B File Offset: 0x00136D4B
		public TransportHeaders()
		{
			this._headerList = new ArrayList(6);
		}

		// Token: 0x17000EE9 RID: 3817
		public object this[object key]
		{
			[SecurityCritical]
			get
			{
				string strB = (string)key;
				foreach (object obj in this._headerList)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (string.Compare((string)dictionaryEntry.Key, strB, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				if (key == null)
				{
					return;
				}
				string strB = (string)key;
				for (int i = this._headerList.Count - 1; i >= 0; i--)
				{
					string strA = (string)((DictionaryEntry)this._headerList[i]).Key;
					if (string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0)
					{
						this._headerList.RemoveAt(i);
						break;
					}
				}
				if (value != null)
				{
					this._headerList.Add(new DictionaryEntry(key, value));
				}
			}
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x00138C62 File Offset: 0x00136E62
		[SecurityCritical]
		public IEnumerator GetEnumerator()
		{
			return this._headerList.GetEnumerator();
		}

		// Token: 0x04002842 RID: 10306
		private ArrayList _headerList;
	}
}
