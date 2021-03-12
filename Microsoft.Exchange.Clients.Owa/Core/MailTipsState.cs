using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.MailTips;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200015B RID: 347
	internal sealed class MailTipsState
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x0005307C File Offset: 0x0005127C
		public MailTipsState(RecipientInfo[] recipientsInfo, RecipientInfo senderInfo, bool doesNeedConfig, string logonUserLegDn, string logonUserPrimarySmtpAddress, ClientSecurityContext clientSecurityContext, ExTimeZone logonUserTimeZone, CultureInfo logonUserCulture, OrganizationId logonUserOrgId, ADObjectId queryBaseDn, bool shouldHideByDefault, PendingRequestManager pendingRequestManager, string serverName, string weekdayDateTimeFormat)
		{
			this.RecipientsInfo = recipientsInfo;
			this.SenderInfo = senderInfo;
			this.DoesNeedConfig = doesNeedConfig;
			this.LogonUserLegDn = logonUserLegDn;
			this.LogonUserPrimarySmtpAddress = logonUserPrimarySmtpAddress;
			this.ClientSecurityContext = clientSecurityContext;
			this.LogonUserTimeZone = (logonUserTimeZone ?? (ExTimeZone.CurrentTimeZone ?? ExTimeZone.UtcTimeZone));
			this.LogonUserCulture = logonUserCulture;
			this.LogonUserOrgId = logonUserOrgId;
			this.QueryBaseDn = queryBaseDn;
			this.ShouldHideByDefault = shouldHideByDefault;
			this.PendingRequestManager = pendingRequestManager;
			this.ServerName = serverName;
			this.MailTipsResult = new List<MailTips>(this.RecipientsInfo.Length);
			this.WeekdayDateTimeFormat = weekdayDateTimeFormat;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00053121 File Offset: 0x00051321
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x00053129 File Offset: 0x00051329
		public RecipientInfo[] RecipientsInfo { get; private set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00053132 File Offset: 0x00051332
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0005313A File Offset: 0x0005133A
		public RecipientInfo SenderInfo { get; private set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00053143 File Offset: 0x00051343
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0005314B File Offset: 0x0005134B
		public bool DoesNeedConfig { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00053154 File Offset: 0x00051354
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0005315C File Offset: 0x0005135C
		public IBudget Budget { get; internal set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00053165 File Offset: 0x00051365
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x0005316D File Offset: 0x0005136D
		public CachedOrganizationConfiguration CachedOrganizationConfiguration { get; internal set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00053176 File Offset: 0x00051376
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x0005317E File Offset: 0x0005137E
		public string LogonUserLegDn { get; private set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00053187 File Offset: 0x00051387
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x0005318F File Offset: 0x0005138F
		public string LogonUserPrimarySmtpAddress { get; private set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00053198 File Offset: 0x00051398
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x000531A0 File Offset: 0x000513A0
		public ClientSecurityContext ClientSecurityContext { get; private set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x000531A9 File Offset: 0x000513A9
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x000531B1 File Offset: 0x000513B1
		public ExTimeZone LogonUserTimeZone { get; private set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000531BA File Offset: 0x000513BA
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x000531C2 File Offset: 0x000513C2
		public CultureInfo LogonUserCulture { get; private set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x000531CB File Offset: 0x000513CB
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x000531D3 File Offset: 0x000513D3
		public OrganizationId LogonUserOrgId { get; private set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x000531DC File Offset: 0x000513DC
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x000531E4 File Offset: 0x000513E4
		public ProxyAddress SendingAs { get; internal set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x000531ED File Offset: 0x000513ED
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x000531F5 File Offset: 0x000513F5
		public List<MailTips> MailTipsResult { get; private set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x000531FE File Offset: 0x000513FE
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x00053206 File Offset: 0x00051406
		public bool ShouldHideByDefault { get; private set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0005320F File Offset: 0x0005140F
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x00053217 File Offset: 0x00051417
		public PendingRequestManager PendingRequestManager { get; private set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00053220 File Offset: 0x00051420
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x00053228 File Offset: 0x00051428
		public string ServerName { get; private set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00053231 File Offset: 0x00051431
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x00053239 File Offset: 0x00051439
		public ADObjectId QueryBaseDn { get; private set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00053242 File Offset: 0x00051442
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x0005324A File Offset: 0x0005144A
		public string WeekdayDateTimeFormat { get; private set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00053253 File Offset: 0x00051453
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x0005325B File Offset: 0x0005145B
		internal RequestLogger RequestLogger { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00053264 File Offset: 0x00051464
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x0005326C File Offset: 0x0005146C
		internal GetMailTipsQuery GetMailTipsQuery { get; set; }

		// Token: 0x06000C19 RID: 3097 RVA: 0x00053278 File Offset: 0x00051478
		public int GetEstimatedStringLength()
		{
			int num = 0;
			if (this.RecipientsInfo != null)
			{
				num = 100 * this.RecipientsInfo.Length;
			}
			return 450 + 100 * num;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000532A8 File Offset: 0x000514A8
		public override string ToString()
		{
			int estimatedStringLength = this.GetEstimatedStringLength();
			StringBuilder stringBuilder = new StringBuilder(estimatedStringLength);
			if (this.SenderInfo != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Sender: {0}, ", new object[]
				{
					this.SenderInfo.ToProxyAddress()
				});
			}
			if (this.RecipientsInfo != null)
			{
				stringBuilder.Append("Recipients: ");
				foreach (RecipientInfo recipientInfo in this.RecipientsInfo)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}, ", new object[]
					{
						recipientInfo.ToProxyAddress()
					});
				}
			}
			if (!string.IsNullOrEmpty(this.LogonUserLegDn))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "LogonUser: {0}, ", new object[]
				{
					this.LogonUserLegDn
				});
			}
			if (!string.IsNullOrEmpty(this.ServerName))
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "ServerName: {0}", new object[]
				{
					this.ServerName
				});
			}
			return stringBuilder.ToString();
		}
	}
}
