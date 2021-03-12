using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200022E RID: 558
	internal class PropValueConverter : IDataConverter<PropValue, PropValueData>
	{
		// Token: 0x06001DE6 RID: 7654 RVA: 0x0003DAAD File Offset: 0x0003BCAD
		PropValue IDataConverter<PropValue, PropValueData>.GetNativeRepresentation(PropValueData data)
		{
			return new PropValue((PropTag)data.PropTag, data.Value);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0003DAC0 File Offset: 0x0003BCC0
		PropValueData IDataConverter<PropValue, PropValueData>.GetDataRepresentation(PropValue pv)
		{
			return new PropValueData(pv.PropTag, pv.RawValue);
		}
	}
}
