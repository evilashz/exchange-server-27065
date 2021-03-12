using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class SidStringAndAttributes
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000640C File Offset: 0x0000460C
		public string SecurityIdentifier
		{
			get
			{
				return this.securityIdentifier;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006414 File Offset: 0x00004614
		[CLSCompliant(false)]
		public uint Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000641C File Offset: 0x0000461C
		private SidStringAndAttributes()
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006424 File Offset: 0x00004624
		[CLSCompliant(false)]
		public SidStringAndAttributes(string identifier, uint attribute)
		{
			this.securityIdentifier = identifier;
			this.attributes = attribute;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000643C File Offset: 0x0000463C
		internal static SidStringAndAttributes Read(IntPtr pointer, SecurityIdentifier localMachineAuthoritySid, ref int offset)
		{
			IntPtr binaryForm = Marshal.ReadIntPtr(pointer, offset);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(binaryForm);
			offset += Marshal.SizeOf(typeof(IntPtr));
			uint attribute = (uint)Marshal.ReadInt32(pointer, offset);
			offset += Marshal.SizeOf(typeof(IntPtr));
			if (securityIdentifier.IsEqualDomainSid(localMachineAuthoritySid))
			{
				return null;
			}
			string text = securityIdentifier.ToString();
			if (!text.StartsWith("S-1-16", StringComparison.OrdinalIgnoreCase))
			{
				return new SidStringAndAttributes(securityIdentifier.ToString(), attribute);
			}
			return null;
		}

		// Token: 0x040000B9 RID: 185
		private const string IntegritySidPrefix = "S-1-16";

		// Token: 0x040000BA RID: 186
		private string securityIdentifier;

		// Token: 0x040000BB RID: 187
		private uint attributes;
	}
}
