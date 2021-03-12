using System;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	internal sealed class PolicySyncCookie : ISerializable
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x0001F381 File Offset: 0x0001D581
		public PolicySyncCookie()
		{
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001F394 File Offset: 0x0001D594
		public PolicySyncCookie(SerializationInfo info, StreamingContext context)
		{
			this.keyValueStorage = (PolicyKeyStorage)info.GetValue("KeyValueStorage", typeof(PolicyKeyStorage));
		}

		// Token: 0x17000341 RID: 833
		public string this[string key]
		{
			get
			{
				return this.keyValueStorage[key];
			}
			set
			{
				this.keyValueStorage[key] = value;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001F3E4 File Offset: 0x0001D5E4
		public static PolicySyncCookie Deserialize(byte[] bytes)
		{
			if (bytes == null)
			{
				return new PolicySyncCookie();
			}
			PolicySyncCookie result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				PolicySyncCookie policySyncCookie = PolicySyncCookie.GetSerializer().ReadObject(memoryStream) as PolicySyncCookie;
				if (policySyncCookie == null)
				{
					throw new InvalidOperationException("Failed to deserialize cookie data");
				}
				result = policySyncCookie;
			}
			return result;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001F440 File Offset: 0x0001D640
		public byte[] Serialize()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				PolicySyncCookie.GetSerializer().WriteObject(memoryStream, this);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001F484 File Offset: 0x0001D684
		public bool TryGetValue(string key, out string result)
		{
			return this.keyValueStorage.TryGetValue(key, out result);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001F493 File Offset: 0x0001D693
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("KeyValueStorage", this.keyValueStorage);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		private static DataContractSerializer GetSerializer()
		{
			return new DataContractSerializer(typeof(PolicySyncCookie), new Type[]
			{
				typeof(PolicyKeyStorage)
			});
		}

		// Token: 0x04000564 RID: 1380
		private PolicyKeyStorage keyValueStorage = new PolicyKeyStorage();
	}
}
