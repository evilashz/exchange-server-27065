using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000451 RID: 1105
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RpcCommon
	{
		// Token: 0x06003123 RID: 12579 RVA: 0x000C96FD File Offset: 0x000C78FD
		public static byte[] ConvertRpcParametersToByteArray(Dictionary<string, object> parameters)
		{
			if (parameters == null || parameters.Count < 1)
			{
				throw new ArgumentNullException("parameters");
			}
			return RpcCommon.ObjectToBytes(parameters);
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000C971C File Offset: 0x000C791C
		public static Dictionary<string, object> ConvertByteArrayToRpcParameters(byte[] data)
		{
			if (data == null || data.Length < 1)
			{
				throw new ArgumentNullException("data");
			}
			Dictionary<string, object> dictionary = RpcCommon.BytesToObject(data) as Dictionary<string, object>;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, object>();
			}
			return dictionary;
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000C9754 File Offset: 0x000C7954
		private static byte[] ObjectToBytes(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			byte[] bytesFromBuffer;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream, obj);
				bytesFromBuffer = Util.GetBytesFromBuffer(memoryStream);
			}
			return bytesFromBuffer;
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000C97A0 File Offset: 0x000C79A0
		private static object BytesToObject(byte[] binaryData)
		{
			if (binaryData == null || binaryData.Length == 0)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			object result;
			using (MemoryStream memoryStream = new MemoryStream(binaryData, false))
			{
				result = binaryFormatter.Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x04001AA2 RID: 6818
		public const int CurrentRpcVersion = 1;
	}
}
