using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000160 RID: 352
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OABPropertyDescriptor
	{
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0003AD49 File Offset: 0x00038F49
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x0003AD51 File Offset: 0x00038F51
		public PropTag PropTag { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0003AD5A File Offset: 0x00038F5A
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x0003AD62 File Offset: 0x00038F62
		public OABPropertyFlags PropFlags { get; set; }

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003AD6B File Offset: 0x00038F6B
		public void WriteTo(BinaryWriter writer)
		{
			writer.Write((uint)this.PropTag);
			writer.Write((uint)this.PropFlags);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003AD88 File Offset: 0x00038F88
		public static OABPropertyDescriptor ReadFrom(BinaryReader reader, string elementName)
		{
			return new OABPropertyDescriptor
			{
				PropTag = (PropTag)reader.ReadUInt32(elementName + ".PropTag"),
				PropFlags = (OABPropertyFlags)reader.ReadUInt32(elementName + ".PropFlags")
			};
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003ADCC File Offset: 0x00038FCC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"PropTag=",
				this.PropTag.ToString("X8"),
				", PropFlags=",
				this.PropFlags
			});
		}

		// Token: 0x04000784 RID: 1924
		public static readonly int Size = 8;
	}
}
