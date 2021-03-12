using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A23 RID: 2595
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarFolderConfigurationRequest
	{
		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x0600492C RID: 18732 RVA: 0x00102489 File Offset: 0x00100689
		// (set) Token: 0x0600492D RID: 18733 RVA: 0x00102491 File Offset: 0x00100691
		[DataMember]
		public BaseFolderId FolderId { get; set; }

		// Token: 0x0600492E RID: 18734 RVA: 0x0010249A File Offset: 0x0010069A
		internal void ValidateRequest()
		{
			if (this.FolderId == null)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
		}
	}
}
