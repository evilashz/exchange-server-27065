using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Compliance.Serialization.Formatters;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000156 RID: 342
	internal static class Serialization
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x00029498 File Offset: 0x00027698
		public static byte[] ObjectToBytes(object obj)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DiagnosticTracer, null, "In Serialization.ObjectToBytes", new object[0]);
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = null;
			try
			{
				BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
				if (obj == null)
				{
					return null;
				}
				binaryFormatter.Serialize(memoryStream, obj);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				int num = (int)memoryStream.Length;
				array = new byte[num];
				memoryStream.Read(array, 0, num);
			}
			catch (SerializationException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.DiagnosticTracer, null, "In Serialization.ObjectToBytes, Got Error = {0}", new object[]
				{
					ex
				});
			}
			finally
			{
				memoryStream.Close();
			}
			return array;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002954C File Offset: 0x0002774C
		public static object BytesToObject(byte[] mbinaryData)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.DiagnosticTracer, 0, "In Serialization.BytesToObject", new object[0]);
			MemoryStream memoryStream = new MemoryStream();
			object result = null;
			try
			{
				if (mbinaryData == null || mbinaryData.Length == 0)
				{
					return null;
				}
				memoryStream.Write(mbinaryData, 0, mbinaryData.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = TypedBinaryFormatter.DeserializeObject(memoryStream, SerializationTypeBinder.Instance);
			}
			catch (SerializationException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.DiagnosticTracer, null, "In Serialization.BytesToObject, Got Error = {0}", new object[]
				{
					ex
				});
			}
			finally
			{
				memoryStream.Close();
			}
			return result;
		}
	}
}
