using System;

namespace System.Reflection
{
	// Token: 0x02000598 RID: 1432
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class AssemblySignatureKeyAttribute : Attribute
	{
		// Token: 0x06004359 RID: 17241 RVA: 0x000F7B42 File Offset: 0x000F5D42
		[__DynamicallyInvokable]
		public AssemblySignatureKeyAttribute(string publicKey, string countersignature)
		{
			this._publicKey = publicKey;
			this._countersignature = countersignature;
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x000F7B58 File Offset: 0x000F5D58
		[__DynamicallyInvokable]
		public string PublicKey
		{
			[__DynamicallyInvokable]
			get
			{
				return this._publicKey;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x000F7B60 File Offset: 0x000F5D60
		[__DynamicallyInvokable]
		public string Countersignature
		{
			[__DynamicallyInvokable]
			get
			{
				return this._countersignature;
			}
		}

		// Token: 0x04001B54 RID: 6996
		private string _publicKey;

		// Token: 0x04001B55 RID: 6997
		private string _countersignature;
	}
}
