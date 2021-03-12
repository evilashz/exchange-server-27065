using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000229 RID: 553
	internal class AdrEntryConverter : IDataConverter<AdrEntry, AdrEntryData>
	{
		// Token: 0x06001DD4 RID: 7636 RVA: 0x0003D838 File Offset: 0x0003BA38
		AdrEntry IDataConverter<AdrEntry, AdrEntryData>.GetNativeRepresentation(AdrEntryData data)
		{
			return new AdrEntry(DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(data.Values));
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0003D84C File Offset: 0x0003BA4C
		AdrEntryData IDataConverter<AdrEntry, AdrEntryData>.GetDataRepresentation(AdrEntry ae)
		{
			return new AdrEntryData
			{
				Values = DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(ae.Values)
			};
		}
	}
}
