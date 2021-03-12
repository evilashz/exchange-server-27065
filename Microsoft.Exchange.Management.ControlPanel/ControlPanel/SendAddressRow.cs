using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E7 RID: 743
	[DataContract]
	public class SendAddressRow : DropDownItemData
	{
		// Token: 0x06002D09 RID: 11529 RVA: 0x0008A1C7 File Offset: 0x000883C7
		public SendAddressRow(SendAddress sendAddress)
		{
			base.Text = sendAddress.DisplayName;
			base.Value = sendAddress.AddressId;
		}
	}
}
