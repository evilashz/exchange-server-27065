using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200006B RID: 107
	public class MailboxCreator : IDataObjectCreator
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public object Create(DataTable table)
		{
			ADUser aduser = new ADUser();
			if (table != null)
			{
				DataColumn dataColumn = table.Columns["ResourceType"];
				if (dataColumn != null)
				{
					if (DBNull.Value.Equals(dataColumn.DefaultValue))
					{
						aduser.ResourceType = null;
					}
					else
					{
						aduser.ResourceType = new ExchangeResourceType?((ExchangeResourceType)dataColumn.DefaultValue);
					}
				}
			}
			return new Mailbox(aduser);
		}

		// Token: 0x04000111 RID: 273
		private const string ResourceType = "ResourceType";
	}
}
