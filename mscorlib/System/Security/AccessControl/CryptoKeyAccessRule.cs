using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000211 RID: 529
	public sealed class CryptoKeyAccessRule : AccessRule
	{
		// Token: 0x06001EF9 RID: 7929 RVA: 0x0006D095 File Offset: 0x0006B295
		public CryptoKeyAccessRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AccessControlType type) : this(identity, CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x0006D0A9 File Offset: 0x0006B2A9
		public CryptoKeyAccessRule(string identity, CryptoKeyRights cryptoKeyRights, AccessControlType type) : this(new NTAccount(identity), CryptoKeyAccessRule.AccessMaskFromRights(cryptoKeyRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0006D0C2 File Offset: 0x0006B2C2
		private CryptoKeyAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0006D0D3 File Offset: 0x0006B2D3
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return CryptoKeyAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0006D0E0 File Offset: 0x0006B2E0
		private static int AccessMaskFromRights(CryptoKeyRights cryptoKeyRights, AccessControlType controlType)
		{
			if (controlType == AccessControlType.Allow)
			{
				cryptoKeyRights |= CryptoKeyRights.Synchronize;
			}
			else
			{
				if (controlType != AccessControlType.Deny)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", new object[]
					{
						controlType,
						"controlType"
					}), "controlType");
				}
				if (cryptoKeyRights != CryptoKeyRights.FullControl)
				{
					cryptoKeyRights &= ~CryptoKeyRights.Synchronize;
				}
			}
			return (int)cryptoKeyRights;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0006D13F File Offset: 0x0006B33F
		internal static CryptoKeyRights RightsFromAccessMask(int accessMask)
		{
			return (CryptoKeyRights)accessMask;
		}
	}
}
