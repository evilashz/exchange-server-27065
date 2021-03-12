using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200081E RID: 2078
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ChannelDataStore : IChannelDataStore
	{
		// Token: 0x06005909 RID: 22793 RVA: 0x00138A3C File Offset: 0x00136C3C
		private ChannelDataStore(string[] channelUrls, DictionaryEntry[] extraData)
		{
			this._channelURIs = channelUrls;
			this._extraData = extraData;
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x00138A52 File Offset: 0x00136C52
		public ChannelDataStore(string[] channelURIs)
		{
			this._channelURIs = channelURIs;
			this._extraData = null;
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x00138A68 File Offset: 0x00136C68
		[SecurityCritical]
		internal ChannelDataStore InternalShallowCopy()
		{
			return new ChannelDataStore(this._channelURIs, this._extraData);
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600590C RID: 22796 RVA: 0x00138A7B File Offset: 0x00136C7B
		// (set) Token: 0x0600590D RID: 22797 RVA: 0x00138A83 File Offset: 0x00136C83
		public string[] ChannelUris
		{
			[SecurityCritical]
			get
			{
				return this._channelURIs;
			}
			set
			{
				this._channelURIs = value;
			}
		}

		// Token: 0x17000EE7 RID: 3815
		public object this[object key]
		{
			[SecurityCritical]
			get
			{
				foreach (DictionaryEntry dictionaryEntry in this._extraData)
				{
					if (dictionaryEntry.Key.Equals(key))
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				if (this._extraData == null)
				{
					this._extraData = new DictionaryEntry[1];
					this._extraData[0] = new DictionaryEntry(key, value);
					return;
				}
				int num = this._extraData.Length;
				DictionaryEntry[] array = new DictionaryEntry[num + 1];
				int i;
				for (i = 0; i < num; i++)
				{
					array[i] = this._extraData[i];
				}
				array[i] = new DictionaryEntry(key, value);
				this._extraData = array;
			}
		}

		// Token: 0x04002840 RID: 10304
		private string[] _channelURIs;

		// Token: 0x04002841 RID: 10305
		private DictionaryEntry[] _extraData;
	}
}
