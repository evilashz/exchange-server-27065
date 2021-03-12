using System;
using System.Reflection;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000186 RID: 390
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class OwaEventFieldAttribute : Attribute
	{
		// Token: 0x06000E3B RID: 3643 RVA: 0x0005B990 File Offset: 0x00059B90
		public OwaEventFieldAttribute(string name, bool isOptional, object defaultValue)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.isOptional = isOptional;
			this.defaultValue = defaultValue;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0005B9BB File Offset: 0x00059BBB
		public OwaEventFieldAttribute(string name) : this(name, false, null)
		{
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0005B9C6 File Offset: 0x00059BC6
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0005B9CE File Offset: 0x00059BCE
		internal bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0005B9D6 File Offset: 0x00059BD6
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0005B9DE File Offset: 0x00059BDE
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.fieldInfo;
			}
			set
			{
				this.fieldInfo = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0005B9E7 File Offset: 0x00059BE7
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x0005B9EF File Offset: 0x00059BEF
		internal Type FieldType
		{
			get
			{
				return this.fieldType;
			}
			set
			{
				this.fieldType = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0005B9F8 File Offset: 0x00059BF8
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0005BA00 File Offset: 0x00059C00
		internal uint FieldMask
		{
			get
			{
				return this.fieldMask;
			}
			set
			{
				this.fieldMask = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0005BA09 File Offset: 0x00059C09
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x0005BA11 File Offset: 0x00059C11
		internal object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x040009AE RID: 2478
		private string name;

		// Token: 0x040009AF RID: 2479
		private bool isOptional;

		// Token: 0x040009B0 RID: 2480
		private FieldInfo fieldInfo;

		// Token: 0x040009B1 RID: 2481
		private Type fieldType;

		// Token: 0x040009B2 RID: 2482
		private uint fieldMask;

		// Token: 0x040009B3 RID: 2483
		private object defaultValue;
	}
}
