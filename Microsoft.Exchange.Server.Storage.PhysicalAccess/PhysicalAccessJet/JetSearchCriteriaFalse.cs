using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000BA RID: 186
	public class JetSearchCriteriaFalse : SearchCriteriaFalse, IJetSearchCriteria
	{
		// Token: 0x0600080C RID: 2060 RVA: 0x00027481 File Offset: 0x00025681
		internal JetSearchCriteriaFalse()
		{
		}

		// Token: 0x040002EC RID: 748
		public static readonly JetSearchCriteriaFalse Instance = new JetSearchCriteriaFalse();
	}
}
