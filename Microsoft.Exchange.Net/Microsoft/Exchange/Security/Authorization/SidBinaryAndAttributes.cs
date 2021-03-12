using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200002E RID: 46
	public class SidBinaryAndAttributes
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006374 File Offset: 0x00004574
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this.securityIdentifier;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000637C File Offset: 0x0000457C
		[CLSCompliant(false)]
		public uint Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006384 File Offset: 0x00004584
		private SidBinaryAndAttributes()
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000638C File Offset: 0x0000458C
		[CLSCompliant(false)]
		public SidBinaryAndAttributes(SecurityIdentifier identifier, uint attribute)
		{
			this.securityIdentifier = identifier;
			this.attributes = attribute;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000063A4 File Offset: 0x000045A4
		internal static SidBinaryAndAttributes Read(IntPtr pointer, SecurityIdentifier localMachineAuthoritySid, ref int offset)
		{
			IntPtr binaryForm = Marshal.ReadIntPtr(pointer, offset);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(binaryForm);
			offset += Marshal.SizeOf(typeof(IntPtr));
			uint num = (uint)Marshal.ReadInt32(pointer, offset);
			offset += Marshal.SizeOf(typeof(IntPtr));
			if (!securityIdentifier.IsEqualDomainSid(localMachineAuthoritySid) && (num & 32U) != 32U)
			{
				return new SidBinaryAndAttributes(securityIdentifier, num);
			}
			return null;
		}

		// Token: 0x040000B7 RID: 183
		private readonly SecurityIdentifier securityIdentifier;

		// Token: 0x040000B8 RID: 184
		private readonly uint attributes;
	}
}
