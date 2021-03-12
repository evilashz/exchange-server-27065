using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A7E RID: 2686
	[Cmdlet("Add", "IPBlockListProvider", SupportsShouldProcess = true)]
	public sealed class NewIPBlockListProvider : NewIPListProvider<IPBlockListProvider>
	{
		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x06005F7E RID: 24446 RVA: 0x0018FDE9 File Offset: 0x0018DFE9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddIPBlockListProvider(base.Name.ToString(), base.LookupDomain.ToString());
			}
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x06005F7F RID: 24447 RVA: 0x0018FE06 File Offset: 0x0018E006
		// (set) Token: 0x06005F80 RID: 24448 RVA: 0x0018FE13 File Offset: 0x0018E013
		[Parameter(Mandatory = false)]
		public AsciiString RejectionResponse
		{
			get
			{
				return this.DataObject.RejectionResponse;
			}
			set
			{
				this.DataObject.RejectionResponse = value;
			}
		}
	}
}
