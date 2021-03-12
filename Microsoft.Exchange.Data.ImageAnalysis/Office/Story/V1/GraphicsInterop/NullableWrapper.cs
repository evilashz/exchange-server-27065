using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop
{
	// Token: 0x02000017 RID: 23
	[StructLayout(LayoutKind.Sequential)]
	internal class NullableWrapper<T> where T : struct
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003A75 File Offset: 0x00001C75
		public override bool Equals(object obj)
		{
			return this.Value.Equals(obj);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003A89 File Offset: 0x00001C89
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003A9C File Offset: 0x00001C9C
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public static explicit operator NullableWrapper<T>(T value)
		{
			return new NullableWrapper<T>
			{
				Value = value
			};
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003ACB File Offset: 0x00001CCB
		public static explicit operator T(NullableWrapper<T> nullable)
		{
			if (nullable == null)
			{
				throw new ArgumentNullException("nullable");
			}
			return nullable.Value;
		}

		// Token: 0x04000077 RID: 119
		public T Value;
	}
}
