using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A66 RID: 2662
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMasterCategoryListRequest
	{
		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06004B80 RID: 19328 RVA: 0x001058B7 File Offset: 0x00103AB7
		// (set) Token: 0x06004B81 RID: 19329 RVA: 0x001058BF File Offset: 0x00103ABF
		[DataMember]
		public string EmailAddress { get; set; }

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06004B82 RID: 19330 RVA: 0x001058C8 File Offset: 0x00103AC8
		// (set) Token: 0x06004B83 RID: 19331 RVA: 0x001058D0 File Offset: 0x00103AD0
		internal SmtpAddress SmtpAddress { get; private set; }

		// Token: 0x06004B84 RID: 19332 RVA: 0x001058DC File Offset: 0x00103ADC
		internal void ValidateRequest()
		{
			if (this.EmailAddress == null)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(CoreResources.IDs.ErrorInvalidSmtpAddress), FaultParty.Sender);
			}
			try
			{
				this.SmtpAddress = SmtpAddress.Parse(this.EmailAddress);
			}
			catch (FormatException innerException)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(CoreResources.IDs.ErrorInvalidSmtpAddress, innerException), FaultParty.Sender);
			}
		}
	}
}
