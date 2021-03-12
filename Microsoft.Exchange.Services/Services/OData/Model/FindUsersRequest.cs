using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000ECB RID: 3787
	internal class FindUsersRequest : FindEntitiesRequest<User>
	{
		// Token: 0x06006248 RID: 25160 RVA: 0x001339CC File Offset: 0x00131BCC
		public FindUsersRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x001339D5 File Offset: 0x00131BD5
		public override ODataCommand GetODataCommand()
		{
			return new FindUsersCommand(this);
		}
	}
}
