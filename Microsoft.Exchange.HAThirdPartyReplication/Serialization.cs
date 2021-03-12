using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000010 RID: 16
	internal static class Serialization
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002C88 File Offset: 0x00000E88
		public static byte[] ObjectToBytes(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			byte[] buffer;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream, obj);
				buffer = memoryStream.GetBuffer();
			}
			return buffer;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public static object BytesToObject(byte[] mBinaryData)
		{
			if (mBinaryData == null || mBinaryData.Length == 0)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			object result;
			using (MemoryStream memoryStream = new MemoryStream(mBinaryData, false))
			{
				result = binaryFormatter.Deserialize(memoryStream);
			}
			return result;
		}
	}
}
