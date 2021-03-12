using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200022C RID: 556
	internal class PropProblemConverter : IDataConverter<PropProblem, PropProblemData>
	{
		// Token: 0x06001DE0 RID: 7648 RVA: 0x0003DA42 File Offset: 0x0003BC42
		PropProblem IDataConverter<PropProblem, PropProblemData>.GetNativeRepresentation(PropProblemData data)
		{
			return new PropProblem(data.Index, (PropTag)data.PropTag, data.Scode);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0003DA5C File Offset: 0x0003BC5C
		PropProblemData IDataConverter<PropProblem, PropProblemData>.GetDataRepresentation(PropProblem pp)
		{
			return new PropProblemData
			{
				PropTag = (int)pp.PropTag,
				Scode = pp.Scode,
				Index = pp.Index
			};
		}
	}
}
