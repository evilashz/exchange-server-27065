using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000233 RID: 563
	public abstract class ObjectAccessRule : AccessRule
	{
		// Token: 0x06002040 RID: 8256 RVA: 0x000713B4 File Offset: 0x0006F5B4
		protected ObjectAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
			if (!objectType.Equals(Guid.Empty) && (accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
			{
				this._objectType = objectType;
				this._objectFlags |= ObjectAceFlags.ObjectAceTypePresent;
			}
			else
			{
				this._objectType = Guid.Empty;
			}
			if (!inheritedObjectType.Equals(Guid.Empty) && (inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
			{
				this._inheritedObjectType = inheritedObjectType;
				this._objectFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
				return;
			}
			this._inheritedObjectType = Guid.Empty;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x00071440 File Offset: 0x0006F640
		public Guid ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x00071448 File Offset: 0x0006F648
		public Guid InheritedObjectType
		{
			get
			{
				return this._inheritedObjectType;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00071450 File Offset: 0x0006F650
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				return this._objectFlags;
			}
		}

		// Token: 0x04000BA8 RID: 2984
		private readonly Guid _objectType;

		// Token: 0x04000BA9 RID: 2985
		private readonly Guid _inheritedObjectType;

		// Token: 0x04000BAA RID: 2986
		private readonly ObjectAceFlags _objectFlags;
	}
}
