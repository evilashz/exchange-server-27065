using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A37 RID: 2615
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeInternalCalendarRequest
	{
		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x00102D3A File Offset: 0x00100F3A
		// (set) Token: 0x060049C4 RID: 18884 RVA: 0x00102D42 File Offset: 0x00100F42
		[DataMember]
		public string EmailAddress { get; set; }

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x00102D4B File Offset: 0x00100F4B
		// (set) Token: 0x060049C6 RID: 18886 RVA: 0x00102D53 File Offset: 0x00100F53
		[DataMember]
		private string ParentGroupGuid { get; set; }

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x00102D5C File Offset: 0x00100F5C
		// (set) Token: 0x060049C8 RID: 18888 RVA: 0x00102D64 File Offset: 0x00100F64
		internal ADRecipient Recipient { get; private set; }

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060049C9 RID: 18889 RVA: 0x00102D6D File Offset: 0x00100F6D
		// (set) Token: 0x060049CA RID: 18890 RVA: 0x00102D75 File Offset: 0x00100F75
		internal SmtpAddress SmtpAddress { get; private set; }

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x00102D7E File Offset: 0x00100F7E
		// (set) Token: 0x060049CC RID: 18892 RVA: 0x00102D86 File Offset: 0x00100F86
		internal Guid GroupId { get; private set; }

		// Token: 0x060049CD RID: 18893 RVA: 0x00102D90 File Offset: 0x00100F90
		internal void ValidateRequest()
		{
			Guid groupId;
			if (!Guid.TryParse(this.ParentGroupGuid, out groupId))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(CoreResources.IDs.ErrorInvalidId), FaultParty.Sender);
			}
			this.GroupId = groupId;
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
			this.RetrieveADRecipient();
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x00102E28 File Offset: 0x00101028
		private void RetrieveADRecipient()
		{
			IRecipientSession adrecipientSession = CallContext.Current.ADRecipientSessionContext.GetADRecipientSession();
			this.Recipient = adrecipientSession.FindByProxyAddress(ProxyAddress.Parse(this.SmtpAddress.ToString()));
		}
	}
}
