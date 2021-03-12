using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009FB RID: 2555
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class AggregatedAccountListConfiguration : UserConfigurationObject
	{
		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x06005D69 RID: 23913 RVA: 0x0018B8C7 File Offset: 0x00189AC7
		internal override UserConfigurationObjectSchema Schema
		{
			get
			{
				return AggregatedAccountListConfiguration.schema;
			}
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x0018B8CE File Offset: 0x00189ACE
		public AggregatedAccountListConfiguration()
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2012);
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06005D6B RID: 23915 RVA: 0x0018B8E1 File Offset: 0x00189AE1
		// (set) Token: 0x06005D6C RID: 23916 RVA: 0x0018B8F3 File Offset: 0x00189AF3
		public List<AggregatedAccountInfo> AggregatedAccountList
		{
			get
			{
				return (List<AggregatedAccountInfo>)this[AggregatedAccountListConfigurationSchema.AggregatedAccountList];
			}
			set
			{
				this[AggregatedAccountListConfigurationSchema.AggregatedAccountList] = value;
			}
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x0018B901 File Offset: 0x00189B01
		// (set) Token: 0x06005D6E RID: 23918 RVA: 0x0018B909 File Offset: 0x00189B09
		public Guid AggregatedMailboxGuid { get; set; }

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06005D6F RID: 23919 RVA: 0x0018B912 File Offset: 0x00189B12
		// (set) Token: 0x06005D70 RID: 23920 RVA: 0x0018B91A File Offset: 0x00189B1A
		public SmtpAddress SmtpAddress { get; set; }

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06005D71 RID: 23921 RVA: 0x0018B923 File Offset: 0x00189B23
		// (set) Token: 0x06005D72 RID: 23922 RVA: 0x0018B92B File Offset: 0x00189B2B
		public Guid RequestGuid { get; set; }

		// Token: 0x06005D73 RID: 23923 RVA: 0x0018B948 File Offset: 0x00189B48
		public override void Delete(MailboxStoreTypeProvider session)
		{
			AggregatedAccountListConfiguration aggregatedAccountListConfiguration = this.Read(session, null) as AggregatedAccountListConfiguration;
			if (aggregatedAccountListConfiguration != null)
			{
				this.AggregatedAccountList = aggregatedAccountListConfiguration.AggregatedAccountList;
			}
			if (this.AggregatedAccountList != null)
			{
				this.AggregatedAccountList = this.AggregatedAccountList.FindAll((AggregatedAccountInfo account) => account.RequestGuid != this.RequestGuid);
			}
			if (this.AggregatedAccountList == null || this.AggregatedAccountList.Count == 0)
			{
				UserConfigurationHelper.DeleteMailboxConfiguration(session.MailboxSession, "AggregatedAccountList");
				return;
			}
			this.UpdateFAI(session);
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x0018B9CC File Offset: 0x00189BCC
		public override IConfigurable Read(MailboxStoreTypeProvider session, ObjectId identity)
		{
			base.Principal = ExchangePrincipal.FromADUser(session.ADUser, null);
			IConfigurable result;
			using (UserConfigurationXmlAdapter<AggregatedAccountListConfiguration> userConfigurationXmlAdapter = new UserConfigurationXmlAdapter<AggregatedAccountListConfiguration>(session.MailboxSession, "AggregatedAccountList", SaveMode.NoConflictResolution, new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), new GetReadableUserConfigurationDelegate(UserConfigurationHelper.GetReadOnlyMailboxConfiguration), AggregatedAccountListConfiguration.property))
			{
				result = userConfigurationXmlAdapter.Read(base.Principal);
			}
			return result;
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x0018BA58 File Offset: 0x00189C58
		public override void Save(MailboxStoreTypeProvider session)
		{
			if (!(this.RequestGuid == Guid.Empty))
			{
				SmtpAddress smtpAddress = this.SmtpAddress;
				if (!string.IsNullOrEmpty(this.SmtpAddress.ToString()))
				{
					AggregatedAccountListConfiguration aggregatedAccountListConfiguration = this.Read(session, null) as AggregatedAccountListConfiguration;
					if (aggregatedAccountListConfiguration != null)
					{
						this.AggregatedAccountList = aggregatedAccountListConfiguration.AggregatedAccountList;
					}
					if (this.AggregatedAccountList == null)
					{
						this.AggregatedAccountList = new List<AggregatedAccountInfo>();
					}
					AggregatedAccountInfo aggregatedAccountInfo = this.AggregatedAccountList.Find((AggregatedAccountInfo account) => account.RequestGuid == this.RequestGuid);
					if (aggregatedAccountInfo == null)
					{
						this.AggregatedAccountList.Add(new AggregatedAccountInfo(this.AggregatedMailboxGuid, this.SmtpAddress, this.RequestGuid));
					}
					else
					{
						aggregatedAccountInfo.SmtpAddress = aggregatedAccountInfo.SmtpAddress;
					}
					this.UpdateFAI(session);
					return;
				}
			}
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x0018BB1C File Offset: 0x00189D1C
		private void UpdateFAI(MailboxStoreTypeProvider session)
		{
			using (UserConfigurationXmlAdapter<AggregatedAccountListConfiguration> userConfigurationXmlAdapter = new UserConfigurationXmlAdapter<AggregatedAccountListConfiguration>(session.MailboxSession, "AggregatedAccountList", SaveMode.ResolveConflicts, new GetUserConfigurationDelegate(UserConfigurationHelper.GetMailboxConfiguration), AggregatedAccountListConfiguration.property))
			{
				userConfigurationXmlAdapter.Save(this);
			}
			base.ResetChangeTracking();
		}

		// Token: 0x04003431 RID: 13361
		private static AggregatedAccountListConfigurationSchema schema = ObjectSchema.GetInstance<AggregatedAccountListConfigurationSchema>();

		// Token: 0x04003432 RID: 13362
		private static readonly SimplePropertyDefinition property = AggregatedAccountListConfigurationSchema.AggregatedAccountList;
	}
}
