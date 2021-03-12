using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200018E RID: 398
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class OwaEventParameterAttribute : Attribute
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x0005C9BC File Offset: 0x0005ABBC
		public OwaEventParameterAttribute(string name, Type type, bool isArray, bool isOptional)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.name = name;
			this.isOptional = isOptional;
			this.isArray = isArray;
			this.paramType = type;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0005CA0E File Offset: 0x0005AC0E
		public OwaEventParameterAttribute(string name, Type type, bool isArray) : this(name, type, isArray, false)
		{
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0005CA1A File Offset: 0x0005AC1A
		public OwaEventParameterAttribute(string name, Type type) : this(name, type, false, false)
		{
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0005CA26 File Offset: 0x0005AC26
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0005CA2E File Offset: 0x0005AC2E
		internal Type Type
		{
			get
			{
				return this.paramType;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0005CA36 File Offset: 0x0005AC36
		internal bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0005CA3E File Offset: 0x0005AC3E
		internal bool IsArray
		{
			get
			{
				return this.isArray;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0005CA46 File Offset: 0x0005AC46
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x0005CA4E File Offset: 0x0005AC4E
		internal bool IsStruct
		{
			get
			{
				return this.isStruct;
			}
			set
			{
				this.isStruct = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0005CA57 File Offset: 0x0005AC57
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x0005CA5F File Offset: 0x0005AC5F
		internal ulong ParameterMask
		{
			get
			{
				return this.parameterMask;
			}
			set
			{
				this.parameterMask = value;
			}
		}

		// Token: 0x040009F3 RID: 2547
		private string name;

		// Token: 0x040009F4 RID: 2548
		private Type paramType;

		// Token: 0x040009F5 RID: 2549
		private bool isStruct;

		// Token: 0x040009F6 RID: 2550
		private bool isOptional;

		// Token: 0x040009F7 RID: 2551
		private bool isArray;

		// Token: 0x040009F8 RID: 2552
		private ulong parameterMask;
	}
}
