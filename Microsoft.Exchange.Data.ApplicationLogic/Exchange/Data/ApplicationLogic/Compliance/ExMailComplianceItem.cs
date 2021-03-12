using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Office.CompliancePolicy.ComplianceData;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000CD RID: 205
	internal class ExMailComplianceItem : ComplianceItem
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x000228C0 File Offset: 0x00020AC0
		internal ExMailComplianceItem(MailboxSession session, object[] values)
		{
			if (values[0] != null)
			{
				this.id = (values[0] as StoreId);
			}
			if (values[1] != null && !(values[1] is PropertyError))
			{
				this.whenCreated = ((ExDateTime)values[1]).UniversalTime;
			}
			this.creator = (values[2] as string);
			this.displayName = (values[3] as string);
			if (values[4] != null && !(values[4] is PropertyError))
			{
				this.whenLastModified = ((ExDateTime)values[4]).UniversalTime;
			}
			this.lastModifiedBy = (values[5] as string);
			if (values[6] != null && !(values[6] is PropertyError))
			{
				this.expiryTime = ((ExDateTime)values[6]).UniversalTime;
			}
			else
			{
				this.expiryTime = DateTime.MaxValue;
			}
			this.session = session;
			this.message = null;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0002299A File Offset: 0x00020B9A
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x000229A2 File Offset: 0x00020BA2
		public override string Creator
		{
			get
			{
				return this.creator;
			}
			protected set
			{
				this.creator = value;
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000229B0 File Offset: 0x00020BB0
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x000229B8 File Offset: 0x00020BB8
		public override string DisplayName
		{
			get
			{
				return this.displayName;
			}
			protected set
			{
				this.displayName = value;
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000229C6 File Offset: 0x00020BC6
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x000229D3 File Offset: 0x00020BD3
		public override string Id
		{
			get
			{
				return this.id.ToString();
			}
			protected set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x000229DA File Offset: 0x00020BDA
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x000229E2 File Offset: 0x00020BE2
		public override DateTime WhenCreated
		{
			get
			{
				return this.whenCreated;
			}
			protected set
			{
				this.whenCreated = value;
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x000229F0 File Offset: 0x00020BF0
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x000229F8 File Offset: 0x00020BF8
		public override DateTime WhenLastModified
		{
			get
			{
				return this.whenLastModified;
			}
			protected set
			{
				this.whenLastModified = value;
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00022A06 File Offset: 0x00020C06
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00022A0E File Offset: 0x00020C0E
		public override string LastModifier
		{
			get
			{
				return this.lastModifiedBy;
			}
			protected set
			{
				this.lastModifiedBy = value;
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00022A1C File Offset: 0x00020C1C
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x00022A24 File Offset: 0x00020C24
		public override DateTime ExpiryTime
		{
			get
			{
				return this.expiryTime;
			}
			protected set
			{
				this.expiryTime = value;
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00022A32 File Offset: 0x00020C32
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00022A39 File Offset: 0x00020C39
		public override string Extension
		{
			get
			{
				return "ExMessage";
			}
			protected set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00022A40 File Offset: 0x00020C40
		private void EnsureMessage()
		{
			if (this.message == null)
			{
				this.message = MessageItem.Bind(this.session, this.id, ExMailComplianceItem.MailDataColumns);
			}
		}

		// Token: 0x040003D3 RID: 979
		private const int MessageId = 0;

		// Token: 0x040003D4 RID: 980
		private const int MessageReceivedTime = 1;

		// Token: 0x040003D5 RID: 981
		private const int MessageSenderDisplayName = 2;

		// Token: 0x040003D6 RID: 982
		private const int MessageSubject = 3;

		// Token: 0x040003D7 RID: 983
		private const int MessageLastModifiedTime = 4;

		// Token: 0x040003D8 RID: 984
		private const int MessageLastModifiedBy = 5;

		// Token: 0x040003D9 RID: 985
		private const int MessageRetentionDate = 6;

		// Token: 0x040003DA RID: 986
		internal static readonly PropertyDefinition[] MailDataColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.ReceivedTime,
			MessageItemSchema.SenderDisplayName,
			ItemSchema.Subject,
			StoreObjectSchema.LastModifiedTime,
			ItemSchema.LastModifiedBy,
			ItemSchema.RetentionDate
		};

		// Token: 0x040003DB RID: 987
		private StoreId id;

		// Token: 0x040003DC RID: 988
		private string creator;

		// Token: 0x040003DD RID: 989
		private string displayName;

		// Token: 0x040003DE RID: 990
		private DateTime whenCreated;

		// Token: 0x040003DF RID: 991
		private DateTime whenLastModified;

		// Token: 0x040003E0 RID: 992
		private string lastModifiedBy;

		// Token: 0x040003E1 RID: 993
		private DateTime expiryTime;

		// Token: 0x040003E2 RID: 994
		private MailboxSession session;

		// Token: 0x040003E3 RID: 995
		private MessageItem message;
	}
}
