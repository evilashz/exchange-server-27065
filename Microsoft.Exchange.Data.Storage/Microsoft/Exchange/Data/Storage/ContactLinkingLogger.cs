using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004B3 RID: 1203
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContactLinkingLogger : StorageLoggerBase
	{
		// Token: 0x06003571 RID: 13681 RVA: 0x000D78F6 File Offset: 0x000D5AF6
		public ContactLinkingLogger(string activityName, MailboxInfoForLinking mailboxInfo) : this(ContactLinkingLogger.Logger.Member, activityName, mailboxInfo)
		{
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000D790A File Offset: 0x000D5B0A
		internal ContactLinkingLogger(IExtensibleLogger logger, string activityName, MailboxInfoForLinking mailboxInfo) : base(logger)
		{
			ArgumentValidator.ThrowIfNull("activityName", activityName);
			ArgumentValidator.ThrowIfNull("mailboxInfo", mailboxInfo);
			this.mailboxInfo = mailboxInfo;
			this.activityName = activityName;
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000D7937 File Offset: 0x000D5B37
		protected override string TenantName
		{
			get
			{
				return this.mailboxInfo.TenantName;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x000D7944 File Offset: 0x000D5B44
		protected override Guid MailboxGuid
		{
			get
			{
				return this.mailboxInfo.MailboxGuid;
			}
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000D7951 File Offset: 0x000D5B51
		protected override void AppendEventData(ICollection<KeyValuePair<string, object>> eventData)
		{
			base.AppendEventData(eventData);
			eventData.Add(new KeyValuePair<string, object>(ContactLinkingLogger.ActivityNameFieldName, this.activityName));
		}

		// Token: 0x04001C64 RID: 7268
		internal static readonly string ActivityNameFieldName = "ActivityName";

		// Token: 0x04001C65 RID: 7269
		private static readonly LazyMember<ExtensibleLogger> Logger = new LazyMember<ExtensibleLogger>(() => new ExtensibleLogger(ContactLinkingLogConfiguration.Default));

		// Token: 0x04001C66 RID: 7270
		private readonly MailboxInfoForLinking mailboxInfo;

		// Token: 0x04001C67 RID: 7271
		private readonly string activityName;
	}
}
