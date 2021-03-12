using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000839 RID: 2105
	internal class MessageDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06005A08 RID: 23048 RVA: 0x0013B0A3 File Offset: 0x001392A3
		public MessageDictionaryEnumerator(MessageDictionary md, IDictionary hashtable)
		{
			this._md = md;
			if (hashtable != null)
			{
				this._enumHash = hashtable.GetEnumerator();
				return;
			}
			this._enumHash = null;
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005A09 RID: 23049 RVA: 0x0013B0D0 File Offset: 0x001392D0
		public object Key
		{
			get
			{
				if (this.i < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
				}
				if (this.i < this._md._keys.Length)
				{
					return this._md._keys[this.i];
				}
				return this._enumHash.Key;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06005A0A RID: 23050 RVA: 0x0013B12C File Offset: 0x0013932C
		public object Value
		{
			get
			{
				if (this.i < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
				}
				if (this.i < this._md._keys.Length)
				{
					return this._md.GetMessageValue(this.i);
				}
				return this._enumHash.Value;
			}
		}

		// Token: 0x06005A0B RID: 23051 RVA: 0x0013B184 File Offset: 0x00139384
		public bool MoveNext()
		{
			if (this.i == -2)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
			this.i++;
			if (this.i < this._md._keys.Length)
			{
				return true;
			}
			if (this._enumHash != null && this._enumHash.MoveNext())
			{
				return true;
			}
			this.i = -2;
			return false;
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06005A0C RID: 23052 RVA: 0x0013B1F0 File Offset: 0x001393F0
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06005A0D RID: 23053 RVA: 0x0013B1FD File Offset: 0x001393FD
		public DictionaryEntry Entry
		{
			get
			{
				return new DictionaryEntry(this.Key, this.Value);
			}
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x0013B210 File Offset: 0x00139410
		public void Reset()
		{
			this.i = -1;
			if (this._enumHash != null)
			{
				this._enumHash.Reset();
			}
		}

		// Token: 0x0400288B RID: 10379
		private int i = -1;

		// Token: 0x0400288C RID: 10380
		private IDictionaryEnumerator _enumHash;

		// Token: 0x0400288D RID: 10381
		private MessageDictionary _md;
	}
}
