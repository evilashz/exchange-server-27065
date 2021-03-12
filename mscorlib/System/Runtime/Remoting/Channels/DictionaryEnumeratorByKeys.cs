using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000825 RID: 2085
	internal class DictionaryEnumeratorByKeys : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06005930 RID: 22832 RVA: 0x00138EBD File Offset: 0x001370BD
		public DictionaryEnumeratorByKeys(IDictionary properties)
		{
			this._properties = properties;
			this._keyEnum = properties.Keys.GetEnumerator();
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x00138EDD File Offset: 0x001370DD
		public bool MoveNext()
		{
			return this._keyEnum.MoveNext();
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x00138EEA File Offset: 0x001370EA
		public void Reset()
		{
			this._keyEnum.Reset();
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06005933 RID: 22835 RVA: 0x00138EF7 File Offset: 0x001370F7
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06005934 RID: 22836 RVA: 0x00138F04 File Offset: 0x00137104
		public DictionaryEntry Entry
		{
			get
			{
				return new DictionaryEntry(this.Key, this.Value);
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06005935 RID: 22837 RVA: 0x00138F17 File Offset: 0x00137117
		public object Key
		{
			get
			{
				return this._keyEnum.Current;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06005936 RID: 22838 RVA: 0x00138F24 File Offset: 0x00137124
		public object Value
		{
			get
			{
				return this._properties[this.Key];
			}
		}

		// Token: 0x04002847 RID: 10311
		private IDictionary _properties;

		// Token: 0x04002848 RID: 10312
		private IEnumerator _keyEnum;
	}
}
