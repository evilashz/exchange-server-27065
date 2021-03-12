using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000203 RID: 515
	internal sealed class NullRestriction : Restriction
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x00023F58 File Offset: 0x00022158
		private NullRestriction()
		{
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00023F60 File Offset: 0x00022160
		internal override RestrictionType RestrictionType
		{
			get
			{
				return RestrictionType.Null;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00023F67 File Offset: 0x00022167
		internal new static NullRestriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			return NullRestriction.Instance;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00023F6E File Offset: 0x0002216E
		public static NullRestriction Instance
		{
			get
			{
				return NullRestriction.instance;
			}
		}

		// Token: 0x04000669 RID: 1641
		private static readonly NullRestriction instance = new NullRestriction();
	}
}
