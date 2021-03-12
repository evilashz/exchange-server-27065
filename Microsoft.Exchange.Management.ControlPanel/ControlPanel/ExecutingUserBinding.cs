using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200058F RID: 1423
	public class ExecutingUserBinding : StaticBinding
	{
		// Token: 0x17002572 RID: 9586
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x000C7D8F File Offset: 0x000C5F8F
		public override bool HasValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002573 RID: 9587
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x000C7D94 File Offset: 0x000C5F94
		// (set) Token: 0x060041A7 RID: 16807 RVA: 0x000C7DAD File Offset: 0x000C5FAD
		public override object Value
		{
			get
			{
				Identity identity = Identity.FromExecutingUserId();
				return identity.RawIdentity;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
