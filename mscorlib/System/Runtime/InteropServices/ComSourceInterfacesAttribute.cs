using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008F4 RID: 2292
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComSourceInterfacesAttribute : Attribute
	{
		// Token: 0x06005EF1 RID: 24305 RVA: 0x00146C4B File Offset: 0x00144E4B
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(string sourceInterfaces)
		{
			this._val = sourceInterfaces;
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x00146C5A File Offset: 0x00144E5A
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface)
		{
			this._val = sourceInterface.FullName;
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x00146C6E File Offset: 0x00144E6E
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
		{
			this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x00146C94 File Offset: 0x00144E94
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
		{
			this._val = string.Concat(new string[]
			{
				sourceInterface1.FullName,
				"\0",
				sourceInterface2.FullName,
				"\0",
				sourceInterface3.FullName
			});
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x00146CE4 File Offset: 0x00144EE4
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
		{
			this._val = string.Concat(new string[]
			{
				sourceInterface1.FullName,
				"\0",
				sourceInterface2.FullName,
				"\0",
				sourceInterface3.FullName,
				"\0",
				sourceInterface4.FullName
			});
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06005EF6 RID: 24310 RVA: 0x00146D45 File Offset: 0x00144F45
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029C0 RID: 10688
		internal string _val;
	}
}
