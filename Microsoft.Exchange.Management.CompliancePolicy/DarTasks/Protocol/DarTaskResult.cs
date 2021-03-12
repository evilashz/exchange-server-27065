using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class DarTaskResult
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003604 File Offset: 0x00001804
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000360C File Offset: 0x0000180C
		public string DarTaskId { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003615 File Offset: 0x00001815
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000361D File Offset: 0x0000181D
		public TaskStoreObject[] DarTasks { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003626 File Offset: 0x00001826
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000362E File Offset: 0x0000182E
		public TaskAggregateStoreObject[] DarTaskAggregates { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003637 File Offset: 0x00001837
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000363F File Offset: 0x0000183F
		public string LocalizedError { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003648 File Offset: 0x00001848
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003650 File Offset: 0x00001850
		public string LocalizedInformation { get; set; }

		// Token: 0x060000AA RID: 170 RVA: 0x00003659 File Offset: 0x00001859
		public static DarTaskResult Nothing()
		{
			return new DarTaskResult();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003660 File Offset: 0x00001860
		public static DarTaskResult GetResultObject(byte[] data)
		{
			ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			DarTaskResult darTaskResult = DarTaskResult.ObjectFromBytes<DarTaskResult>(data);
			if (darTaskResult.LocalizedError != null)
			{
				throw new DataSourceOperationException(new LocalizedString(darTaskResult.LocalizedError));
			}
			return darTaskResult;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003698 File Offset: 0x00001898
		public static T ObjectFromBytes<T>(byte[] data)
		{
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			T result;
			using (MemoryStream memoryStream = new MemoryStream(data, false))
			{
				result = (T)((object)binaryFormatter.Deserialize(memoryStream));
			}
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000036E0 File Offset: 0x000018E0
		public static byte[] ObjectToBytes<T>(T obj)
		{
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			byte[] buffer;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream, obj);
				buffer = memoryStream.GetBuffer();
			}
			return buffer;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000372C File Offset: 0x0000192C
		public byte[] ToBytes()
		{
			return DarTaskResult.ObjectToBytes<DarTaskResult>(this);
		}
	}
}
