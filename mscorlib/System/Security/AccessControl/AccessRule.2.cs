using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000232 RID: 562
	public abstract class AccessRule : AuthorizationRule
	{
		// Token: 0x0600203E RID: 8254 RVA: 0x000712FC File Offset: 0x0006F4FC
		protected AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
		{
			if (type != AccessControlType.Allow && type != AccessControlType.Deny)
			{
				throw new ArgumentOutOfRangeException("type", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			if (inheritanceFlags < InheritanceFlags.None || inheritanceFlags > (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit))
			{
				throw new ArgumentOutOfRangeException("inheritanceFlags", Environment.GetResourceString("Argument_InvalidEnumValue", new object[]
				{
					inheritanceFlags,
					"InheritanceFlags"
				}));
			}
			if (propagationFlags < PropagationFlags.None || propagationFlags > (PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly))
			{
				throw new ArgumentOutOfRangeException("propagationFlags", Environment.GetResourceString("Argument_InvalidEnumValue", new object[]
				{
					inheritanceFlags,
					"PropagationFlags"
				}));
			}
			this._type = type;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x000713AA File Offset: 0x0006F5AA
		public AccessControlType AccessControlType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000BA7 RID: 2983
		private readonly AccessControlType _type;
	}
}
