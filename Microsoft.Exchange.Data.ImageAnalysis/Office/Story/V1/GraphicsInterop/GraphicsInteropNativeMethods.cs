using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Story.V1.GraphicsInterop.Misc;

namespace Microsoft.Office.Story.V1.GraphicsInterop
{
	// Token: 0x02000016 RID: 22
	internal static class GraphicsInteropNativeMethods
	{
		// Token: 0x060000AC RID: 172
		[DllImport("ole32.dll")]
		public static extern int PropVariantClear(ref PROPVARIANT pvar);

		// Token: 0x060000AD RID: 173 RVA: 0x00003984 File Offset: 0x00001B84
		public static Guid GetGuidForInterface<T>()
		{
			GuidAttribute guidAttribute = (from GuidAttribute guid in typeof(T).GetTypeInfo().GetCustomAttributes(typeof(GuidAttribute), false)
			select guid).FirstOrDefault<GuidAttribute>();
			if (guidAttribute == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unable to get GUID for type '{0}' as GuidAttribute is not present on that type.", new object[]
				{
					typeof(T)
				}));
			}
			return new Guid(guidAttribute.Value);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003A04 File Offset: 0x00001C04
		public static T CreateComInstanceFromInterface<T>()
		{
			return GraphicsInteropNativeMethods.CreateComInstanceFromInterface<T>(GraphicsInteropNativeMethods.GetGuidForInterface<T>());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003A10 File Offset: 0x00001C10
		public static T CreateComInstanceFromInterface<T>(Guid coClassId)
		{
			T result = default(T);
			Type typeFromCLSID = Marshal.GetTypeFromCLSID(coClassId);
			if (null != typeFromCLSID)
			{
				result = (T)((object)Activator.CreateInstance(typeFromCLSID));
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003A42 File Offset: 0x00001C42
		public static void SafeReleaseComObject(object comObject)
		{
			if (comObject != null)
			{
				Marshal.ReleaseComObject(comObject);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003A4E File Offset: 0x00001C4E
		public static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003A57 File Offset: 0x00001C57
		public static bool Failed(int hr)
		{
			return hr < 0;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003A5D File Offset: 0x00001C5D
		public static void CheckNativeResult(int hr)
		{
			if (GraphicsInteropNativeMethods.Failed(hr))
			{
				throw Marshal.GetExceptionForHR(hr);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003A6E File Offset: 0x00001C6E
		public static int GetThreadId()
		{
			return Environment.CurrentManagedThreadId;
		}

		// Token: 0x04000058 RID: 88
		private const string KERNEL32DLL = "kernel32.dll";

		// Token: 0x04000059 RID: 89
		private const string MFPLATDLL = "Mfplat.dll";

		// Token: 0x0400005A RID: 90
		private const string DXGIDLL = "Dxgi.dll";

		// Token: 0x0400005B RID: 91
		private const string D2D1DLL = "D2D1.dll";

		// Token: 0x0400005C RID: 92
		private const string DWRITEDLL = "Dwrite.dll";

		// Token: 0x0400005D RID: 93
		private const string D3D11DLL = "d3d11.dll";

		// Token: 0x0400005E RID: 94
		private const string OLE32DLL = "ole32.dll";

		// Token: 0x0400005F RID: 95
		public const int MF_SDK_VERSION = 2;

		// Token: 0x04000060 RID: 96
		public const int MF_API_VERSION = 112;

		// Token: 0x04000061 RID: 97
		public const int MF_VERSION = 131184;

		// Token: 0x04000062 RID: 98
		public const int MFSTARTUP_NOSOCKET = 1;

		// Token: 0x04000063 RID: 99
		public const int D3D11_SDK_VERSION = 7;

		// Token: 0x04000064 RID: 100
		public const int D3D10_SDK_VERSION = 29;

		// Token: 0x04000065 RID: 101
		public const int D3D10_1_SDK_VERSION = 32;

		// Token: 0x04000066 RID: 102
		public const float D3D11_MIN_DEPTH = 0f;

		// Token: 0x04000067 RID: 103
		public const float D3D11_MAX_DEPTH = 1f;

		// Token: 0x04000068 RID: 104
		public const int S_OK = 0;

		// Token: 0x04000069 RID: 105
		public const int E_NOT_SUFFICIENT_BUFFER = -2147024774;

		// Token: 0x0400006A RID: 106
		public const int E_POINTER = -2147467261;

		// Token: 0x0400006B RID: 107
		public const int E_UNEXPECTED = -2147418113;

		// Token: 0x0400006C RID: 108
		public const int E_OUTOFMEMORY = -2147024882;

		// Token: 0x0400006D RID: 109
		public const int E_INVALIDARG = -2147024809;

		// Token: 0x0400006E RID: 110
		public const int E_HANDLE = -2147024890;

		// Token: 0x0400006F RID: 111
		public const int E_ABORT = -2147467260;

		// Token: 0x04000070 RID: 112
		public const int E_FAIL = -2147467259;

		// Token: 0x04000071 RID: 113
		public const int E_ACCESSDENIED = -2147024891;

		// Token: 0x04000072 RID: 114
		public const int E_NOTIMPL = -2147467263;

		// Token: 0x04000073 RID: 115
		public const int WINCODEC_ERR_PROPERTYNOTFOUND = -2003292352;

		// Token: 0x04000074 RID: 116
		public const int DXGI_ERROR_DEVICE_REMOVED = -2005270523;

		// Token: 0x04000075 RID: 117
		public const int MF_E_ATTRIBUTENOTFOUND = -1072875802;

		// Token: 0x04000076 RID: 118
		public const int D2DERR_RECREATE_TARGET = -2003238900;
	}
}
