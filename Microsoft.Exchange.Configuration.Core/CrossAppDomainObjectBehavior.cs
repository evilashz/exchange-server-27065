using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000003 RID: 3
	internal abstract class CrossAppDomainObjectBehavior : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal CrossAppDomainObjectBehavior(string namedPipeName, BehaviorDirection direction)
		{
			this.NamedPipeName = namedPipeName;
			this.Direction = direction;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E8 File Offset: 0x000002E8
		~CrossAppDomainObjectBehavior()
		{
			this.Dispose(false);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002118 File Offset: 0x00000318
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002120 File Offset: 0x00000320
		internal string NamedPipeName { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002129 File Offset: 0x00000329
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002131 File Offset: 0x00000331
		internal BehaviorDirection Direction { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000213A File Offset: 0x0000033A
		internal virtual bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000213D File Offset: 0x0000033D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000214C File Offset: 0x0000034C
		internal static bool ConnectClientStream(NamedPipeClientStream clientStream, int timeOutInMilliseconds, string namePipeName, bool swallowKnownException = true)
		{
			try
			{
				clientStream.Connect(timeOutInMilliseconds);
				return true;
			}
			catch (Exception ex)
			{
				if (!swallowKnownException || (!(ex is IOException) && !(ex is TimeoutException)))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002190 File Offset: 0x00000390
		internal static byte[] PackMessages(IEnumerable<string> messages)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream, messages);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D8 File Offset: 0x000003D8
		internal static IEnumerable<string> UnpackMessages(byte[] data)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			IEnumerable<string> result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				result = (binaryFormatter.Deserialize(memoryStream) as IEnumerable<string>);
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000221C File Offset: 0x0000041C
		internal static byte[] LoopReadData(CrossAppDomainObjectBehavior.SingleReadAction readAction)
		{
			List<byte[]> list = new List<byte[]>(5000);
			byte[] array = new byte[5000];
			int num;
			do
			{
				num = readAction(array, 0, 5000);
				if (num > 0)
				{
					byte[] array2 = new byte[num];
					Buffer.BlockCopy(array, 0, array2, 0, num);
					list.Add(array2);
				}
			}
			while (num == 5000);
			return CrossAppDomainObjectBehavior.MergeData(list);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002279 File Offset: 0x00000479
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002280 File Offset: 0x00000480
		private static byte[] MergeData(List<byte[]> dataList)
		{
			byte[] array = new byte[dataList.Sum((byte[] a) => a.Length)];
			int num = 0;
			foreach (byte[] array2 in dataList)
			{
				Buffer.BlockCopy(array2, 0, array, num, array2.Length);
				num += array2.Length;
			}
			return array;
		}

		// Token: 0x04000004 RID: 4
		protected const int MaxBytesSizeSentInNamedPipe = 5000;

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x06000011 RID: 17
		internal delegate int SingleReadAction(byte[] buffer, int offset, int count);
	}
}
