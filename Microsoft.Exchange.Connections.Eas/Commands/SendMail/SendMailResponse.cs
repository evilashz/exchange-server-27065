using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.ComposeMail;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.SendMail
{
	// Token: 0x02000063 RID: 99
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "ComposeMail", TypeName = "SendMailResponse")]
	[XmlRoot(Namespace = "ComposeMail", ElementName = "SendMail")]
	public class SendMailResponse : SendMail, IEasServerResponse<SendMailStatus>, IHaveAnHttpStatus
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000549E File Offset: 0x0000369E
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000054A6 File Offset: 0x000036A6
		HttpStatus IHaveAnHttpStatus.HttpStatus { get; set; }

		// Token: 0x060001CC RID: 460 RVA: 0x000054AF File Offset: 0x000036AF
		bool IEasServerResponse<SendMailStatus>.IsSucceeded(SendMailStatus status)
		{
			return SendMailStatus.Success == status;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000054B8 File Offset: 0x000036B8
		SendMailStatus IEasServerResponse<SendMailStatus>.ConvertStatusToEnum()
		{
			byte status = base.Status;
			if (!SendMailResponse.StatusToEnumMap.ContainsKey(status))
			{
				return (SendMailStatus)status;
			}
			return SendMailResponse.StatusToEnumMap[status];
		}

		// Token: 0x04000194 RID: 404
		private static readonly IReadOnlyDictionary<byte, SendMailStatus> StatusToEnumMap = new Dictionary<byte, SendMailStatus>
		{
			{
				0,
				SendMailStatus.Success
			},
			{
				107,
				SendMailStatus.InvalidMIME
			},
			{
				115,
				SendMailStatus.SendQuotaExceeded
			},
			{
				118,
				SendMailStatus.MessagePreviouslySent
			},
			{
				119,
				SendMailStatus.MessageHasNoRecipient
			},
			{
				120,
				SendMailStatus.MailSubmissionFailed
			}
		};
	}
}
