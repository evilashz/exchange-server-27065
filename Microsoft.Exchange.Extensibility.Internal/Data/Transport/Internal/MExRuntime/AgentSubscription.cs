using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	internal sealed class AgentSubscription : IDisposable
	{
		// Token: 0x06000387 RID: 903 RVA: 0x00011D03 File Offset: 0x0000FF03
		public AgentSubscription(string name, string[] agents, string[][] eventTopics)
		{
			this.name = name;
			this.agents = agents;
			this.Update(eventTopics);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00011D20 File Offset: 0x0000FF20
		public IEnumerable<string> EventTopics
		{
			get
			{
				return this.topics;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00011D28 File Offset: 0x0000FF28
		internal long Size
		{
			get
			{
				long length;
				using (MemoryStream memoryStream = new MemoryStream(1024))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, this);
					length = memoryStream.Length;
				}
				return length;
			}
		}

		// Token: 0x170000E1 RID: 225
		public IEnumerable<string> this[string eventTopic]
		{
			get
			{
				this.EnsureInitialized();
				if (!this.subscriptions.ContainsKey(eventTopic))
				{
					return AgentSubscription.EmptyList;
				}
				return this.subscriptions[eventTopic];
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00011D9C File Offset: 0x0000FF9C
		public static AgentSubscription Load(string name)
		{
			MemoryMappedFile readOnlyFile = AgentSubscription.GetReadOnlyFile(name);
			if (readOnlyFile == null)
			{
				return null;
			}
			AgentSubscription result;
			try
			{
				int num = 24;
				int num2 = 50;
				do
				{
					Thread.Sleep(10);
					result = null;
					using (MapFileStream mapFileStream = readOnlyFile.CreateView(0, num))
					{
						byte[] array = new byte[24L];
						int num3 = mapFileStream.Read(array, 0, num);
						if (num3 == num)
						{
							AgentSubscription.Header header = new AgentSubscription.Header(array);
							if (header.Magic == 96101745125713L && header.PayloadSize > 0L && header.PayloadSize <= 1048576L)
							{
								using (MapFileStream mapFileStream2 = readOnlyFile.CreateView(0, num + (int)header.PayloadSize))
								{
									mapFileStream2.Seek((long)num, SeekOrigin.Begin);
									BinaryFormatter binaryFormatter = new BinaryFormatter();
									try
									{
										result = (AgentSubscription)binaryFormatter.Deserialize(mapFileStream2);
									}
									catch (SerializationException)
									{
										goto IL_137;
									}
								}
								mapFileStream.Seek(0L, SeekOrigin.Begin);
								num3 = mapFileStream.Read(array, 0, num);
								if (num3 == num)
								{
									AgentSubscription.Header header2 = new AgentSubscription.Header(array);
									if (header.Magic == header2.Magic && header.PayloadSize == header2.PayloadSize && header.Ticks == header2.Ticks)
									{
										break;
									}
								}
							}
						}
					}
					IL_137:;
				}
				while (num2-- > 0);
			}
			finally
			{
				readOnlyFile.Close();
			}
			return result;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011F5C File Offset: 0x0001015C
		public void Dispose()
		{
			if (this.mappedFile != null)
			{
				this.mappedFile.Close();
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011F74 File Offset: 0x00010174
		internal void Save()
		{
			if (this.topics.Length > 1000 || this.agents.Length > 1000)
			{
				return;
			}
			long size = this.Size;
			if (size > 1048576L)
			{
				return;
			}
			MemoryMappedFile writableFile = this.GetWritableFile();
			if (writableFile == null)
			{
				return;
			}
			using (MapFileStream mapFileStream = writableFile.CreateView(0, (int)(size + 24L)))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				byte[] array = new byte[24L];
				mapFileStream.Write(array, 0, array.Length);
				mapFileStream.Flush();
				binaryFormatter.Serialize(mapFileStream, this);
				AgentSubscription.Header header = new AgentSubscription.Header(size);
				array = header.GetBytes();
				mapFileStream.Seek(0L, SeekOrigin.Begin);
				mapFileStream.Write(array, 0, array.Length);
				mapFileStream.Flush();
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00012040 File Offset: 0x00010240
		internal void Update(string[][] eventTopics)
		{
			this.subscriptions = null;
			Dictionary<string, short> dictionary = new Dictionary<string, short>();
			short num = 0;
			for (int i = 0; i < eventTopics.Length; i++)
			{
				for (int j = 0; j < eventTopics[i].Length; j++)
				{
					if (!dictionary.ContainsKey(eventTopics[i][j]))
					{
						Dictionary<string, short> dictionary2 = dictionary;
						string key = eventTopics[i][j];
						short num2 = num;
						num = num2 + 1;
						dictionary2.Add(key, num2);
					}
				}
			}
			this.topics = new string[dictionary.Count];
			dictionary.Keys.CopyTo(this.topics, 0);
			this.subscriptionTable = new short[this.agents.Length][];
			for (int k = 0; k < this.subscriptionTable.Length; k++)
			{
				this.subscriptionTable[k] = new short[eventTopics[k].Length];
				for (int l = 0; l < eventTopics[k].Length; l++)
				{
					this.subscriptionTable[k][l] = dictionary[eventTopics[k][l]];
				}
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00012127 File Offset: 0x00010327
		private static MemoryMappedFile GetReadOnlyFile(string name)
		{
			return AgentSubscription.GetMemoryMappedFile(name, false);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00012130 File Offset: 0x00010330
		private static MemoryMappedFile GetMemoryMappedFile(string name, bool writable)
		{
			MemoryMappedFile result = null;
			for (int i = 0; i < 10; i++)
			{
				try
				{
					result = new MemoryMappedFile("Global\\MExRuntimeAgentSubscription" + AgentSubscription.Digits[i] + name, 1048600, writable);
					break;
				}
				catch (IOException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00012180 File Offset: 0x00010380
		private void EnsureInitialized()
		{
			if (this.subscriptions != null)
			{
				return;
			}
			this.subscriptions = new Dictionary<string, List<string>>();
			for (int i = 0; i < this.agents.Length; i++)
			{
				for (int j = 0; j < this.subscriptionTable[i].Length; j++)
				{
					string key = this.topics[(int)this.subscriptionTable[i][j]];
					if (!this.subscriptions.ContainsKey(key))
					{
						this.subscriptions.Add(key, new List<string>());
					}
					this.subscriptions[key].Add(this.agents[i]);
				}
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00012213 File Offset: 0x00010413
		private MemoryMappedFile GetWritableFile()
		{
			if (this.mappedFile != null)
			{
				return this.mappedFile;
			}
			this.mappedFile = AgentSubscription.GetMemoryMappedFile(this.name, true);
			return this.mappedFile;
		}

		// Token: 0x04000460 RID: 1120
		private const int MaxSize = 1048576;

		// Token: 0x04000461 RID: 1121
		private const int MaxEntries = 1000;

		// Token: 0x04000462 RID: 1122
		private const string FileNamePrefix = "Global\\MExRuntimeAgentSubscription";

		// Token: 0x04000463 RID: 1123
		[NonSerialized]
		private static readonly string[] Digits = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9"
		};

		// Token: 0x04000464 RID: 1124
		[NonSerialized]
		private static readonly List<string> EmptyList = new List<string>(0);

		// Token: 0x04000465 RID: 1125
		private string name;

		// Token: 0x04000466 RID: 1126
		private string[] agents;

		// Token: 0x04000467 RID: 1127
		private string[] topics;

		// Token: 0x04000468 RID: 1128
		private short[][] subscriptionTable;

		// Token: 0x04000469 RID: 1129
		[NonSerialized]
		private Dictionary<string, List<string>> subscriptions;

		// Token: 0x0400046A RID: 1130
		[NonSerialized]
		private MemoryMappedFile mappedFile;

		// Token: 0x02000075 RID: 117
		private struct Header
		{
			// Token: 0x06000394 RID: 916 RVA: 0x000122B4 File Offset: 0x000104B4
			public Header(long payloadSize)
			{
				this.magic = 96101745125713L;
				this.payloadSize = payloadSize;
				this.ticks = DateTime.UtcNow.Ticks;
			}

			// Token: 0x06000395 RID: 917 RVA: 0x000122EA File Offset: 0x000104EA
			public Header(byte[] data)
			{
				this.magic = BitConverter.ToInt64(data, 0);
				this.payloadSize = BitConverter.ToInt64(data, 8);
				this.ticks = BitConverter.ToInt64(data, 16);
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000396 RID: 918 RVA: 0x00012314 File Offset: 0x00010514
			public long Magic
			{
				get
				{
					return this.magic;
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000397 RID: 919 RVA: 0x0001231C File Offset: 0x0001051C
			public long PayloadSize
			{
				get
				{
					return this.payloadSize;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000398 RID: 920 RVA: 0x00012324 File Offset: 0x00010524
			public long Ticks
			{
				get
				{
					return this.ticks;
				}
			}

			// Token: 0x06000399 RID: 921 RVA: 0x0001232C File Offset: 0x0001052C
			public byte[] GetBytes()
			{
				byte[] bytes = BitConverter.GetBytes(this.magic);
				byte[] bytes2 = BitConverter.GetBytes(this.payloadSize);
				byte[] bytes3 = BitConverter.GetBytes(this.ticks);
				byte[] array = new byte[24L];
				bytes.CopyTo(array, 0);
				bytes2.CopyTo(array, 8);
				bytes3.CopyTo(array, 16);
				return array;
			}

			// Token: 0x0400046B RID: 1131
			public const long AgentSubscriptionMagic = 96101745125713L;

			// Token: 0x0400046C RID: 1132
			public const long Size = 24L;

			// Token: 0x0400046D RID: 1133
			private long magic;

			// Token: 0x0400046E RID: 1134
			private long payloadSize;

			// Token: 0x0400046F RID: 1135
			private long ticks;
		}
	}
}
