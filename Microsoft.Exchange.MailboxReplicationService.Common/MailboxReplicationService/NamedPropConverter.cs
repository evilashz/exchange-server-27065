using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200022B RID: 555
	internal class NamedPropConverter : IDataConverter<NamedProp, NamedPropData>
	{
		// Token: 0x06001DDD RID: 7645 RVA: 0x0003D9A6 File Offset: 0x0003BBA6
		NamedProp IDataConverter<NamedProp, NamedPropData>.GetNativeRepresentation(NamedPropData npd)
		{
			if (npd == null)
			{
				return null;
			}
			if (npd.Kind == 0)
			{
				return new NamedProp(npd.Guid, npd.Id);
			}
			return new NamedProp(npd.Guid, npd.Name);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0003D9D8 File Offset: 0x0003BBD8
		NamedPropData IDataConverter<NamedProp, NamedPropData>.GetDataRepresentation(NamedProp np)
		{
			if (np == null)
			{
				return null;
			}
			NamedPropData namedPropData = new NamedPropData();
			namedPropData.Kind = (int)np.Kind;
			namedPropData.Guid = np.Guid;
			if (np.Kind == NamedPropKind.String)
			{
				namedPropData.Name = np.Name;
				namedPropData.Id = 0;
			}
			else
			{
				namedPropData.Name = null;
				namedPropData.Id = np.Id;
			}
			return namedPropData;
		}
	}
}
