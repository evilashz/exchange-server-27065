using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Misc
{
	// Token: 0x02000023 RID: 35
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct PROPVARIANT : IDisposable
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000043E0 File Offset: 0x000025E0
		public object Value
		{
			get
			{
				object value = this.GetValue();
				if (value == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unknown or unsupported type {0}", new object[]
					{
						this.vt
					}));
				}
				return value;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004423 File Offset: 0x00002623
		public void Dispose()
		{
			GraphicsInteropNativeMethods.PropVariantClear(ref this);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000442C File Offset: 0x0000262C
		public object GetValue()
		{
			object result = null;
			PROPID propid = this.vt;
			if (propid <= PROPID.VT_LPWSTR)
			{
				switch (propid)
				{
				case PROPID.VT_I2:
					result = this.iVal;
					break;
				case PROPID.VT_I4:
					result = this.lVal;
					break;
				case PROPID.VT_R4:
					result = this.fltVal;
					break;
				case PROPID.VT_R8:
					result = this.dblVal;
					break;
				case PROPID.VT_CY:
				case PROPID.VT_DATE:
				case PROPID.VT_BSTR:
				case PROPID.VT_DISPATCH:
				case PROPID.VT_VARIANT:
				case PROPID.VT_DECIMAL:
				case PROPID.VT_NULL | PROPID.VT_I2 | PROPID.VT_R4 | PROPID.VT_BSTR:
					break;
				case PROPID.VT_ERROR:
					result = this.scode;
					break;
				case PROPID.VT_BOOL:
					result = (0 != this.boolVal);
					break;
				case PROPID.VT_UNKNOWN:
					result = Marshal.GetObjectForIUnknown(this.ptr);
					break;
				case PROPID.VT_I1:
					result = this.cVal;
					break;
				case PROPID.VT_UI1:
					result = this.bVal;
					break;
				case PROPID.VT_UI2:
					result = this.uiVal;
					break;
				case PROPID.VT_UI4:
					result = this.ulVal;
					break;
				case PROPID.VT_I8:
					result = this.hVal;
					break;
				case PROPID.VT_UI8:
					result = this.uhVal;
					break;
				default:
					switch (propid)
					{
					case PROPID.VT_LPSTR:
						result = Marshal.PtrToStringAnsi(this.ptr);
						break;
					case PROPID.VT_LPWSTR:
						result = Marshal.PtrToStringUni(this.ptr);
						break;
					}
					break;
				}
			}
			else if (propid != PROPID.VT_FILETIME)
			{
				if (propid == PROPID.VT_CLSID)
				{
					result = Marshal.PtrToStructure(this.ptr, typeof(Guid));
				}
			}
			else
			{
				result = PROPVARIANT.FileTimeStart.Add(new TimeSpan(this.filetime));
			}
			return result;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000045F7 File Offset: 0x000027F7
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004604 File Offset: 0x00002804
		public string ToString(IFormatProvider formatProvider)
		{
			return string.Format(formatProvider ?? CultureInfo.InvariantCulture, "Type {0} = {1}", new object[]
			{
				this.vt,
				this.GetValue()
			});
		}

		// Token: 0x040000AE RID: 174
		private static readonly DateTime FileTimeStart = new DateTime(1601, 1, 1);

		// Token: 0x040000AF RID: 175
		[FieldOffset(0)]
		public PROPID vt;

		// Token: 0x040000B0 RID: 176
		[FieldOffset(2)]
		private readonly byte wReserved1;

		// Token: 0x040000B1 RID: 177
		[FieldOffset(3)]
		private readonly byte wReserved2;

		// Token: 0x040000B2 RID: 178
		[FieldOffset(4)]
		private readonly int wReserved3;

		// Token: 0x040000B3 RID: 179
		[FieldOffset(8)]
		public sbyte cVal;

		// Token: 0x040000B4 RID: 180
		[FieldOffset(8)]
		public byte bVal;

		// Token: 0x040000B5 RID: 181
		[FieldOffset(8)]
		public short iVal;

		// Token: 0x040000B6 RID: 182
		[FieldOffset(8)]
		public ushort uiVal;

		// Token: 0x040000B7 RID: 183
		[FieldOffset(8)]
		public int lVal;

		// Token: 0x040000B8 RID: 184
		[FieldOffset(8)]
		public uint ulVal;

		// Token: 0x040000B9 RID: 185
		[FieldOffset(8)]
		public int intVal;

		// Token: 0x040000BA RID: 186
		[FieldOffset(8)]
		public uint uintVal;

		// Token: 0x040000BB RID: 187
		[FieldOffset(8)]
		public long hVal;

		// Token: 0x040000BC RID: 188
		[FieldOffset(8)]
		public ulong uhVal;

		// Token: 0x040000BD RID: 189
		[FieldOffset(8)]
		public float fltVal;

		// Token: 0x040000BE RID: 190
		[FieldOffset(8)]
		public double dblVal;

		// Token: 0x040000BF RID: 191
		[FieldOffset(8)]
		public short boolVal;

		// Token: 0x040000C0 RID: 192
		[FieldOffset(8)]
		public int scode;

		// Token: 0x040000C1 RID: 193
		[FieldOffset(8)]
		public long filetime;

		// Token: 0x040000C2 RID: 194
		[FieldOffset(8)]
		private IntPtr ptr;

		// Token: 0x040000C3 RID: 195
		[FieldOffset(8)]
		public int cElems;

		// Token: 0x040000C4 RID: 196
		[FieldOffset(12)]
		private IntPtr pElems;

		// Token: 0x040000C5 RID: 197
		[FieldOffset(8)]
		public int cbSize;

		// Token: 0x040000C6 RID: 198
		[FieldOffset(12)]
		private IntPtr pBlobData;
	}
}
