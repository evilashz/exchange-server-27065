using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Optics
{
	// Token: 0x020007EE RID: 2030
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class MailboxLoggerBase : StorageLoggerBase
	{
		// Token: 0x06004C0C RID: 19468 RVA: 0x0013BF8B File Offset: 0x0013A18B
		protected MailboxLoggerBase(Guid mailboxGuid, OrganizationId organizationId, IExtensibleLogger logger) : base(logger)
		{
			if (organizationId != null && organizationId.OrganizationalUnit != null)
			{
				this.tenantName = organizationId.OrganizationalUnit.ToString();
			}
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06004C0D RID: 19469 RVA: 0x0013BFBD File Offset: 0x0013A1BD
		protected override string TenantName
		{
			get
			{
				return this.tenantName;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x0013BFC5 File Offset: 0x0013A1C5
		protected override Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x0013BFCD File Offset: 0x0013A1CD
		protected override void AppendEventData(ICollection<KeyValuePair<string, object>> eventData)
		{
			base.AppendEventData(eventData);
			eventData.Add(MailboxLoggerBase.ApplicationIdKeyValuePair.Value);
			eventData.Add(MailboxLoggerBase.ApplicationVersionKeyValuePair.Value);
		}

		// Token: 0x0400295F RID: 10591
		private const string ApplicationIdName = "ApplicationId";

		// Token: 0x04002960 RID: 10592
		private const string ApplicationVersionName = "ApplicationVersion";

		// Token: 0x04002961 RID: 10593
		private static readonly Lazy<KeyValuePair<string, object>> ApplicationIdKeyValuePair = new Lazy<KeyValuePair<string, object>>(() => new KeyValuePair<string, object>("ApplicationId", ApplicationName.Current.Name));

		// Token: 0x04002962 RID: 10594
		private static readonly Lazy<KeyValuePair<string, object>> ApplicationVersionKeyValuePair = new Lazy<KeyValuePair<string, object>>(() => new KeyValuePair<string, object>("ApplicationVersion", "15.00.1497.010"));

		// Token: 0x04002963 RID: 10595
		private readonly string tenantName;

		// Token: 0x04002964 RID: 10596
		private readonly Guid mailboxGuid;
	}
}
