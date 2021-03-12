using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E56 RID: 3670
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StringData : ConstStringData
	{
		// Token: 0x06007F2B RID: 32555 RVA: 0x0022D974 File Offset: 0x0022BB74
		public StringData()
		{
		}

		// Token: 0x06007F2C RID: 32556 RVA: 0x0022D97C File Offset: 0x0022BB7C
		public StringData(string data) : base(data)
		{
		}

		// Token: 0x170021EA RID: 8682
		// (get) Token: 0x06007F2D RID: 32557 RVA: 0x0022D985 File Offset: 0x0022BB85
		// (set) Token: 0x06007F2E RID: 32558 RVA: 0x0022D98C File Offset: 0x0022BB8C
		public override ushort TypeId
		{
			get
			{
				return StringData.typeId;
			}
			set
			{
				StringData.typeId = value;
			}
		}

		// Token: 0x06007F2F RID: 32559 RVA: 0x0022D994 File Offset: 0x0022BB94
		public override void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool)
		{
			if (reader.ReadBoolean())
			{
				base.Data = null;
				return;
			}
			base.Data = reader.ReadString();
		}

		// Token: 0x06007F30 RID: 32560 RVA: 0x0022D9B2 File Offset: 0x0022BBB2
		public override ICustomSerializable BuildObject()
		{
			return new StringData();
		}

		// Token: 0x0400562B RID: 22059
		private static ushort typeId;
	}
}
