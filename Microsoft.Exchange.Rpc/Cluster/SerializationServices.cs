using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x0200010B RID: 267
	internal abstract class SerializationServices
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x00028EB0 File Offset: 0x000282B0
		public unsafe static int Serialize(object input, byte** _ppb, int* _pcb)
		{
			return <Module>.GetUnmanagedBytes(SerializationServices.Serialize(input), _ppb, _pcb);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00028D18 File Offset: 0x00028118
		public static byte[] Serialize(object input)
		{
			if (input == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			byte[] buffer;
			try
			{
				ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null).Serialize(memoryStream, input);
				buffer = memoryStream.GetBuffer();
			}
			finally
			{
				memoryStream.Close();
			}
			return buffer;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00028ECC File Offset: 0x000282CC
		public unsafe static T Deserialize<T>(byte* _pb, int _cb) where T : class
		{
			byte[] array = new byte[_cb];
			if (_cb > 0)
			{
				Marshal.Copy((IntPtr)((void*)_pb), array, 0, _cb);
			}
			return SerializationServices.Deserialize<T>(array);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00028D6C File Offset: 0x0002816C
		public static T Deserialize<T>(byte[] serializedBytes) where T : class
		{
			T result = default(T);
			if (serializedBytes != null && serializedBytes.Length > 0)
			{
				MemoryStream serializationStream = new MemoryStream(serializedBytes);
				result = (T)((object)ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null).Deserialize(serializationStream));
			}
			return result;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00028EF8 File Offset: 0x000282F8
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool TryDeserialize<T>(byte* _pb, int _cb, ref T outputObject, ref Exception deserializeEx) where T : class
		{
			try
			{
				T t = SerializationServices.Deserialize<T>(<Module>.MakeManagedBytes(_pb, _cb));
				outputObject = t;
				return true;
			}
			catch (SerializationException ex)
			{
				deserializeEx = ex;
			}
			catch (TargetInvocationException ex2)
			{
				deserializeEx = ex2;
			}
			catch (DecoderFallbackException ex3)
			{
				deserializeEx = ex3;
			}
			return false;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00001B2C File Offset: 0x00000F2C
		public SerializationServices()
		{
		}
	}
}
