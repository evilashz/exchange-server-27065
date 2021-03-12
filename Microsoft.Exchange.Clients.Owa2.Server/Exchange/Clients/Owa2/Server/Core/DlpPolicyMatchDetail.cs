using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003CD RID: 973
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DlpPolicyMatchDetail
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0007723B File Offset: 0x0007543B
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x00077243 File Offset: 0x00075443
		[DataMember]
		public DlpPolicyTipAction Action { get; set; }

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x0007724C File Offset: 0x0007544C
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x00077254 File Offset: 0x00075454
		[DataMember]
		public AttachmentIdType[] AttachmentIds { get; set; }

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x0007725D File Offset: 0x0007545D
		// (set) Token: 0x06001F32 RID: 7986 RVA: 0x00077265 File Offset: 0x00075465
		[DataMember]
		public EmailAddressWrapper[] Recipients { get; set; }

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x0007726E File Offset: 0x0007546E
		// (set) Token: 0x06001F34 RID: 7988 RVA: 0x00077276 File Offset: 0x00075476
		[DataMember]
		public string[] Classifications { get; set; }

		// Token: 0x06001F35 RID: 7989 RVA: 0x00077294 File Offset: 0x00075494
		public override string ToString()
		{
			string format = "Action:{0}/AttachmentIds:{1}/Recipients:{2}/Classifications:{3}.";
			object[] array = new object[4];
			array[0] = this.Action.ToString();
			object[] array2 = array;
			int num = 1;
			string text;
			if (this.AttachmentIds != null)
			{
				text = string.Join(";", from attachId in this.AttachmentIds
				select attachId.Id);
			}
			else
			{
				text = string.Empty;
			}
			array2[num] = text;
			object[] array3 = array;
			int num2 = 2;
			string text2;
			if (this.Recipients != null)
			{
				text2 = string.Join(";", from emailAddressWrapper in this.Recipients
				select PolicyTipRequestLogger.MarkAsPII(emailAddressWrapper.EmailAddress));
			}
			else
			{
				text2 = string.Empty;
			}
			array3[num2] = text2;
			array[3] = ((this.Classifications == null) ? string.Empty : string.Join(";", this.Classifications));
			return string.Format(format, array);
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00077378 File Offset: 0x00075578
		public static string ToString(List<DlpPolicyMatchDetail> dlpPolicyMatchDetails)
		{
			if (dlpPolicyMatchDetails == null || !dlpPolicyMatchDetails.Any<DlpPolicyMatchDetail>())
			{
				return string.Empty;
			}
			return string.Join(";", from dlpPolicyMatchDetail in dlpPolicyMatchDetails
			select dlpPolicyMatchDetail.ToString());
		}

		// Token: 0x040011BA RID: 4538
		private const string ToStringFormatString = "Action:{0}/AttachmentIds:{1}/Recipients:{2}/Classifications:{3}.";
	}
}
