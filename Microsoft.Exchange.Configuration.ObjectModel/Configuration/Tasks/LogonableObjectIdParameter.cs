using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000122 RID: 290
	[Serializable]
	public class LogonableObjectIdParameter : GeneralMailboxIdParameter
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x00022A44 File Offset: 0x00020C44
		public LogonableObjectIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00022A4D File Offset: 0x00020C4D
		public LogonableObjectIdParameter()
		{
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00022A55 File Offset: 0x00020C55
		public LogonableObjectIdParameter(MailboxEntry storeMailboxEntry) : base(storeMailboxEntry)
		{
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00022A5E File Offset: 0x00020C5E
		public LogonableObjectIdParameter(MailboxId storeMailboxId) : base(storeMailboxId)
		{
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00022A67 File Offset: 0x00020C67
		public LogonableObjectIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00022A70 File Offset: 0x00020C70
		public LogonableObjectIdParameter(ReducedRecipient reducedRecipient) : base(reducedRecipient)
		{
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00022A79 File Offset: 0x00020C79
		public LogonableObjectIdParameter(ADUser user) : base(user)
		{
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00022A82 File Offset: 0x00020C82
		public LogonableObjectIdParameter(ADSystemAttendantMailbox systemAttendant) : base(systemAttendant)
		{
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00022A8B File Offset: 0x00020C8B
		public LogonableObjectIdParameter(Mailbox mailbox) : base(mailbox)
		{
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00022A94 File Offset: 0x00020C94
		public LogonableObjectIdParameter(LogonStatisticsEntry logonStatisticsEntry) : this((logonStatisticsEntry == null) ? null : logonStatisticsEntry.Identity)
		{
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00022AA8 File Offset: 0x00020CA8
		public LogonableObjectIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00022AB1 File Offset: 0x00020CB1
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return LogonableObjectIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00022AB8 File Offset: 0x00020CB8
		public new static LogonableObjectIdParameter Parse(string identity)
		{
			return new LogonableObjectIdParameter(identity);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00022AC0 File Offset: 0x00020CC0
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			TaskLogger.LogEnter();
			notFoundReason = null;
			IEnumerable<T> result = new List<T>();
			if (typeof(Database).IsAssignableFrom(typeof(T)) && !string.IsNullOrEmpty(base.RawIdentity))
			{
				LegacyDN legacyDN = null;
				if (LegacyDN.TryParse(base.RawIdentity, out legacyDN))
				{
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DatabaseSchema.ExchangeLegacyDN, base.RawIdentity);
					result = base.PerformPrimarySearch<T>(filter, rootId, session, true, optionalData);
				}
			}
			else
			{
				result = base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			}
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00022B4E File Offset: 0x00020D4E
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeLogonableObjectIdParameter(id);
		}

		// Token: 0x04000284 RID: 644
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.UserMailbox,
			RecipientType.SystemAttendantMailbox,
			RecipientType.SystemMailbox,
			RecipientType.PublicDatabase
		};
	}
}
