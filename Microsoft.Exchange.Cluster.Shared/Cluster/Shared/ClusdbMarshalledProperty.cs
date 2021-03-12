using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200008C RID: 140
	public class ClusdbMarshalledProperty : IDisposable
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00013104 File Offset: 0x00011304
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0001310C File Offset: 0x0001130C
		public string PropertyName { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00013115 File Offset: 0x00011315
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0001311D File Offset: 0x0001131D
		public object PropertyValue { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00013126 File Offset: 0x00011326
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0001312E File Offset: 0x0001132E
		public RegistryValueKind ValueKind { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00013137 File Offset: 0x00011337
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0001313F File Offset: 0x0001133F
		public IntPtr PropertyValueIntPtr { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00013148 File Offset: 0x00011348
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00013150 File Offset: 0x00011350
		public int PropertyValueSize { get; set; }

		// Token: 0x0600050B RID: 1291 RVA: 0x00013164 File Offset: 0x00011364
		public static IntPtr FromStringArrayToIntPtr(string[] strings, out int valueSize)
		{
			valueSize = strings.Sum((string s) => s.Length + 1) * 2;
			IntPtr intPtr = Marshal.AllocHGlobal(valueSize);
			IntPtr intPtr2 = intPtr;
			foreach (string text in strings)
			{
				Marshal.Copy(text.ToCharArray(), 0, intPtr2, text.Length);
				intPtr2 = new IntPtr(intPtr2.ToInt64() + (long)(text.Length * 2));
				Marshal.WriteInt16(intPtr2, '\0');
				intPtr2 = new IntPtr(intPtr2.ToInt64() + 2L);
			}
			return intPtr;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00013200 File Offset: 0x00011400
		public static string[] FromIntPtrToStringArray(IntPtr intPtr, int valueSize)
		{
			List<string> list = new List<string>(4);
			if (intPtr != IntPtr.Zero)
			{
				int i = 0;
				int num = 0;
				while (i < valueSize)
				{
					IntPtr ptr = new IntPtr(intPtr.ToInt64() + (long)i);
					string text = Marshal.PtrToStringUni(ptr);
					if (text == null)
					{
						break;
					}
					list.Add(text);
					i += (text.Length + 1) * 2;
					num++;
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013268 File Offset: 0x00011468
		public static IntPtr FromByteArrayToIntPtr(byte[] bytes, out int valueSize)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(bytes.Length);
			Marshal.Copy(bytes, 0, intPtr, bytes.Length);
			valueSize = bytes.Length;
			return intPtr;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013290 File Offset: 0x00011490
		public static ClusdbMarshalledProperty Create(string propertyName, object propertyValue, RegistryValueKind valueKind = RegistryValueKind.Unknown)
		{
			int propertyValueSize = 0;
			if (valueKind == RegistryValueKind.Unknown)
			{
				valueKind = Utils.GetValueKind(propertyValue);
			}
			IntPtr intPtr;
			if (propertyValue is string)
			{
				string text = propertyValue as string;
				intPtr = Marshal.StringToHGlobalUni(text);
				propertyValueSize = (text.Length + 1) * 2;
			}
			else if (propertyValue is string[])
			{
				string[] strings = propertyValue as string[];
				intPtr = ClusdbMarshalledProperty.FromStringArrayToIntPtr(strings, out propertyValueSize);
			}
			else if (propertyValue is IEnumerable<string>)
			{
				string[] strings2 = (propertyValue as IEnumerable<string>).ToArray<string>();
				intPtr = ClusdbMarshalledProperty.FromStringArrayToIntPtr(strings2, out propertyValueSize);
			}
			else if (propertyValue is byte[])
			{
				intPtr = ClusdbMarshalledProperty.FromByteArrayToIntPtr(propertyValue as byte[], out propertyValueSize);
			}
			else if (propertyValue is int)
			{
				intPtr = Marshal.AllocHGlobal(4);
				Marshal.WriteInt32(intPtr, Convert.ToInt32(propertyValue));
				propertyValueSize = 4;
			}
			else if (propertyValue is uint)
			{
				intPtr = Marshal.AllocHGlobal(4);
				Marshal.WriteInt32(intPtr, (int)Convert.ToUInt32(propertyValue));
				propertyValueSize = 4;
			}
			else if (propertyValue is long)
			{
				intPtr = Marshal.AllocHGlobal(8);
				Marshal.WriteInt64(intPtr, Convert.ToInt64(propertyValue));
				propertyValueSize = 8;
			}
			else
			{
				if (!(propertyValue is ulong))
				{
					return null;
				}
				intPtr = Marshal.AllocHGlobal(8);
				Marshal.WriteInt64(intPtr, (long)Convert.ToUInt64(propertyValue));
				propertyValueSize = 8;
			}
			return new ClusdbMarshalledProperty
			{
				PropertyName = propertyName,
				PropertyValue = propertyValue,
				ValueKind = valueKind,
				PropertyValueIntPtr = intPtr,
				PropertyValueSize = propertyValueSize
			};
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000133D8 File Offset: 0x000115D8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000133E7 File Offset: 0x000115E7
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (this.PropertyValueIntPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.PropertyValueIntPtr);
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x040002C2 RID: 706
		private bool isDisposed;
	}
}
