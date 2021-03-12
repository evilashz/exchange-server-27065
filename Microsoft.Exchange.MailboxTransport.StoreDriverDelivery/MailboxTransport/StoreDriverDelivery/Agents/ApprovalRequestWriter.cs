using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Approval;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000062 RID: 98
	internal abstract class ApprovalRequestWriter : IDisposable
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001076D File Offset: 0x0000E96D
		public virtual bool SupportMultipleRequestsForDifferentCultures
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00010770 File Offset: 0x0000E970
		public static ApprovalRequestWriter GetInstance(ApprovalApplicationId? applicationId, OrganizationId organizationId, InitiationMessage initiationMessage)
		{
			if (applicationId != null)
			{
				switch (applicationId.Value)
				{
				case ApprovalApplicationId.AutoGroup:
					return AutoGroupApprovalRequestWriter.GetInstance(initiationMessage);
				case ApprovalApplicationId.ModeratedRecipient:
					return ModerationApprovalRequestWriter.GetInstance(organizationId, initiationMessage);
				}
			}
			return DefaultApprovalRequestWriter.GetInstance(initiationMessage);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public static string FormatApprovalRequestMessageId(string local, int identifier, string domain, bool addAngleBrackets)
		{
			if (addAngleBrackets)
			{
				return string.Concat(new object[]
				{
					'<',
					local,
					'-',
					identifier.ToString(NumberFormatInfo.InvariantInfo),
					'@',
					domain,
					'>'
				});
			}
			return string.Concat(new object[]
			{
				local,
				'-',
				identifier.ToString(NumberFormatInfo.InvariantInfo),
				'@',
				domain
			});
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001084C File Offset: 0x0000EA4C
		public static string FormatStoredApprovalRequestMessageId(string local, string domain)
		{
			return string.Concat(new object[]
			{
				'<',
				local,
				'@',
				domain,
				'>'
			});
		}

		// Token: 0x060003BE RID: 958
		public abstract bool WriteSubjectAndBody(MessageItemApprovalRequest approvalRequest, CultureInfo cultureInfo, out CultureInfo cultureInfoWritten);

		// Token: 0x060003BF RID: 959 RVA: 0x0001088C File Offset: 0x0000EA8C
		public virtual void Dispose()
		{
		}
	}
}
