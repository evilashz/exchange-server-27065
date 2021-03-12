using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200011A RID: 282
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ObsoleteAttribute : Attribute
	{
		// Token: 0x060010BB RID: 4283 RVA: 0x00032789 File Offset: 0x00030989
		[__DynamicallyInvokable]
		public ObsoleteAttribute()
		{
			this._message = null;
			this._error = false;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0003279F File Offset: 0x0003099F
		[__DynamicallyInvokable]
		public ObsoleteAttribute(string message)
		{
			this._message = message;
			this._error = false;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000327B5 File Offset: 0x000309B5
		[__DynamicallyInvokable]
		public ObsoleteAttribute(string message, bool error)
		{
			this._message = message;
			this._error = error;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x000327CB File Offset: 0x000309CB
		[__DynamicallyInvokable]
		public string Message
		{
			[__DynamicallyInvokable]
			get
			{
				return this._message;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x000327D3 File Offset: 0x000309D3
		[__DynamicallyInvokable]
		public bool IsError
		{
			[__DynamicallyInvokable]
			get
			{
				return this._error;
			}
		}

		// Token: 0x040005CD RID: 1485
		private string _message;

		// Token: 0x040005CE RID: 1486
		private bool _error;
	}
}
