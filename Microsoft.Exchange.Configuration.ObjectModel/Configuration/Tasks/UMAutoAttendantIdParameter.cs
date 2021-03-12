using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200015A RID: 346
	[Serializable]
	public class UMAutoAttendantIdParameter : ADIdParameter
	{
		// Token: 0x06000C7B RID: 3195 RVA: 0x00027582 File Offset: 0x00025782
		public UMAutoAttendantIdParameter()
		{
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002758A File Offset: 0x0002578A
		public UMAutoAttendantIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00027593 File Offset: 0x00025793
		public UMAutoAttendantIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002759C File Offset: 0x0002579C
		public UMAutoAttendantIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x000275A5 File Offset: 0x000257A5
		public static UMAutoAttendantIdParameter Parse(string identity)
		{
			return new UMAutoAttendantIdParameter(identity);
		}
	}
}
