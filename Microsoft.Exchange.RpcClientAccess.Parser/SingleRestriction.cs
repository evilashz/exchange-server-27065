using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000209 RID: 521
	internal abstract class SingleRestriction : Restriction
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x0002437A File Offset: 0x0002257A
		internal SingleRestriction(Restriction childRestriction)
		{
			this.childRestriction = childRestriction;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00024389 File Offset: 0x00022589
		public Restriction ChildRestriction
		{
			get
			{
				return this.childRestriction;
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00024391 File Offset: 0x00022591
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			if (this.childRestriction != null)
			{
				this.childRestriction.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x04000670 RID: 1648
		private readonly Restriction childRestriction;
	}
}
