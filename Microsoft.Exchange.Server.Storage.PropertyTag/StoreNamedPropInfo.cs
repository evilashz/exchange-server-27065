using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000008 RID: 8
	public sealed class StoreNamedPropInfo : StorePropInfo
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000318B File Offset: 0x0000138B
		public StoreNamedPropInfo(StorePropName propName) : this(null, propName)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003195 File Offset: 0x00001395
		public StoreNamedPropInfo(string descriptiveName, StorePropName propName) : this(descriptiveName, propName, PropertyType.Invalid, 9223372036854775808UL, PropertyCategories.Empty)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000031B2 File Offset: 0x000013B2
		public StoreNamedPropInfo(string descriptiveName, StorePropName propName, PropertyType propType, ulong groupMask, PropertyCategories categories) : base(descriptiveName, 32768, propType, StorePropInfo.Flags.None, groupMask, categories)
		{
			this.propName = propName;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000031CD File Offset: 0x000013CD
		public override StorePropName PropName
		{
			get
			{
				return this.propName;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000031D5 File Offset: 0x000013D5
		public override ushort PropId
		{
			get
			{
				Globals.AssertRetail(false, "We should not call PropId on a StoreNamedPropInfo");
				return 0;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000031E3 File Offset: 0x000013E3
		public override void AppendToString(StringBuilder sb)
		{
			base.AppendToString(sb);
			sb.Append("(");
			this.propName.AppendToString(sb);
			sb.Append(")");
		}

		// Token: 0x0400005A RID: 90
		internal const StorePropInfo.Flags NamedPropFlags = StorePropInfo.Flags.None;

		// Token: 0x0400005B RID: 91
		private StorePropName propName;
	}
}
