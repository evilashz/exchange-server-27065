using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000015 RID: 21
	public sealed class PSTSyncStateDictionary : XMLSerializableBase
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00006D16 File Offset: 0x00004F16
		public PSTSyncStateDictionary()
		{
			this.syncStates = new EntryIdMap<string>();
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006D2C File Offset: 0x00004F2C
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00006DB8 File Offset: 0x00004FB8
		[XmlArrayItem("SyncState")]
		[XmlArray("SyncStates")]
		public PSTSyncStateDictionary.SyncStatePair[] SyncStates
		{
			get
			{
				PSTSyncStateDictionary.SyncStatePair[] array = new PSTSyncStateDictionary.SyncStatePair[this.syncStates.Count];
				int num = 0;
				foreach (KeyValuePair<byte[], string> keyValuePair in this.syncStates)
				{
					PSTSyncStateDictionary.SyncStatePair syncStatePair = new PSTSyncStateDictionary.SyncStatePair();
					syncStatePair.Key = keyValuePair.Key;
					syncStatePair.SyncState = keyValuePair.Value;
					array[num++] = syncStatePair;
				}
				return array;
			}
			set
			{
				this.syncStates.Clear();
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						PSTSyncStateDictionary.SyncStatePair syncStatePair = value[i];
						this.syncStates[syncStatePair.Key] = syncStatePair.SyncState;
					}
				}
			}
		}

		// Token: 0x17000024 RID: 36
		[XmlIgnore]
		public string this[byte[] key]
		{
			get
			{
				string result;
				if (!this.syncStates.TryGetValue(key, out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				this.syncStates[key] = value;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006E2F File Offset: 0x0000502F
		internal static PSTSyncStateDictionary Deserialize(string blob)
		{
			return XMLSerializableBase.Deserialize<PSTSyncStateDictionary>(blob, false);
		}

		// Token: 0x04000049 RID: 73
		private EntryIdMap<string> syncStates;

		// Token: 0x02000016 RID: 22
		public class SyncStatePair
		{
			// Token: 0x17000025 RID: 37
			// (get) Token: 0x06000106 RID: 262 RVA: 0x00006E38 File Offset: 0x00005038
			// (set) Token: 0x06000107 RID: 263 RVA: 0x00006E40 File Offset: 0x00005040
			[XmlElement]
			public byte[] Key { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x06000108 RID: 264 RVA: 0x00006E49 File Offset: 0x00005049
			// (set) Token: 0x06000109 RID: 265 RVA: 0x00006E51 File Offset: 0x00005051
			[XmlElement]
			public string SyncState { get; set; }
		}
	}
}
