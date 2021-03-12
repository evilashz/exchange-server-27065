using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200088D RID: 2189
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	[FriendAccessAllowed]
	internal sealed class FriendAccessAllowedAttribute : Attribute
	{
	}
}
