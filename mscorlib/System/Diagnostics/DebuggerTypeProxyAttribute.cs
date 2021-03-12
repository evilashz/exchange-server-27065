using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003C1 RID: 961
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		// Token: 0x060031F1 RID: 12785 RVA: 0x000C01D2 File Offset: 0x000BE3D2
		[__DynamicallyInvokable]
		public DebuggerTypeProxyAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.typeName = type.AssemblyQualifiedName;
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000C01FA File Offset: 0x000BE3FA
		[__DynamicallyInvokable]
		public DebuggerTypeProxyAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000C0209 File Offset: 0x000BE409
		[__DynamicallyInvokable]
		public string ProxyTypeName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x000C023A File Offset: 0x000BE43A
		// (set) Token: 0x060031F4 RID: 12788 RVA: 0x000C0211 File Offset: 0x000BE411
		[__DynamicallyInvokable]
		public Type Target
		{
			[__DynamicallyInvokable]
			get
			{
				return this.target;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000C0242 File Offset: 0x000BE442
		// (set) Token: 0x060031F7 RID: 12791 RVA: 0x000C024A File Offset: 0x000BE44A
		[__DynamicallyInvokable]
		public string TargetTypeName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.targetName;
			}
			[__DynamicallyInvokable]
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x040015ED RID: 5613
		private string typeName;

		// Token: 0x040015EE RID: 5614
		private string targetName;

		// Token: 0x040015EF RID: 5615
		private Type target;
	}
}
