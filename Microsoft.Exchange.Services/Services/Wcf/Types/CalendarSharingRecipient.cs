using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A13 RID: 2579
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarSharingRecipient
	{
		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x060048C6 RID: 18630 RVA: 0x00101D35 File Offset: 0x000FFF35
		// (set) Token: 0x060048C7 RID: 18631 RVA: 0x00101D3D File Offset: 0x000FFF3D
		[DataMember]
		public EmailAddressWrapper EmailAddress { get; set; }

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x060048C8 RID: 18632 RVA: 0x00101D46 File Offset: 0x000FFF46
		// (set) Token: 0x060048C9 RID: 18633 RVA: 0x00101D4E File Offset: 0x000FFF4E
		[DataMember]
		public string DetailLevel { get; set; }

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x060048CA RID: 18634 RVA: 0x00101D57 File Offset: 0x000FFF57
		// (set) Token: 0x060048CB RID: 18635 RVA: 0x00101D5F File Offset: 0x000FFF5F
		[DataMember]
		public bool ViewPrivateAppointments { get; set; }

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x060048CC RID: 18636 RVA: 0x00101D68 File Offset: 0x000FFF68
		// (set) Token: 0x060048CD RID: 18637 RVA: 0x00101D70 File Offset: 0x000FFF70
		internal CalendarSharingDetailLevel DetailLevelType { get; private set; }

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x060048CE RID: 18638 RVA: 0x00101D79 File Offset: 0x000FFF79
		// (set) Token: 0x060048CF RID: 18639 RVA: 0x00101D81 File Offset: 0x000FFF81
		internal SmtpAddress SmtpAddress { get; private set; }

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x060048D0 RID: 18640 RVA: 0x00101D8A File Offset: 0x000FFF8A
		// (set) Token: 0x060048D1 RID: 18641 RVA: 0x00101D92 File Offset: 0x000FFF92
		internal ADRecipient ADRecipient { get; set; }

		// Token: 0x060048D2 RID: 18642 RVA: 0x00101D9C File Offset: 0x000FFF9C
		internal void ValidateRequest()
		{
			if (!string.IsNullOrEmpty(this.DetailLevel) && Enum.IsDefined(typeof(CalendarSharingDetailLevel), this.DetailLevel))
			{
				this.DetailLevelType = (CalendarSharingDetailLevel)Enum.Parse(typeof(CalendarSharingDetailLevel), this.DetailLevel);
				try
				{
					this.SmtpAddress = SmtpAddress.Parse(this.EmailAddress.EmailAddress);
				}
				catch (FormatException innerException)
				{
					throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(innerException), FaultParty.Sender);
				}
				return;
			}
			throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
		}
	}
}
