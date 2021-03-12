using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop
{
	// Token: 0x02000018 RID: 24
	[StructLayout(LayoutKind.Sequential)]
	internal class NullableGuid : NullableWrapper<Guid>
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00003AEC File Offset: 0x00001CEC
		public new static explicit operator NullableGuid(Guid value)
		{
			return new NullableGuid
			{
				Value = value
			};
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003B07 File Offset: 0x00001D07
		public static explicit operator Guid(NullableGuid nullable)
		{
			if (nullable == null)
			{
				throw new ArgumentNullException("nullable");
			}
			return nullable.Value;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003B20 File Offset: 0x00001D20
		public static NullableGuid FromGuid(Guid value)
		{
			return new NullableGuid
			{
				Value = value
			};
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003B3B File Offset: 0x00001D3B
		public Guid ToGuid()
		{
			return this.Value;
		}
	}
}
