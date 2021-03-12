using System;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	internal class MapiAdministrationSession : MapiSession
	{
		// Token: 0x060001DA RID: 474 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		public MapiAdministrationSession(Fqdn serverFqdn) : base(null, serverFqdn)
		{
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C2C2 File Offset: 0x0000A4C2
		public MapiAdministrationSession(string serverExchangeLegacyDn, Fqdn serverFqdn) : base(serverExchangeLegacyDn, serverFqdn)
		{
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		public MapiAdministrationSession(string serverExchangeLegacyDn, Fqdn serverFqdn, ConsistencyMode consistencyMode) : base(serverExchangeLegacyDn, serverFqdn, consistencyMode)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		private void CheckRequirementsOnMailboxIdToContinue(MailboxId mailboxId)
		{
			if (null == mailboxId)
			{
				throw new ArgumentNullException("mailboxId");
			}
			if (null == mailboxId.MailboxDatabaseId || Guid.Empty == mailboxId.MailboxDatabaseId.Guid || Guid.Empty == mailboxId.MailboxGuid)
			{
				throw new ArgumentException(Strings.ExceptionIdentityInvalid);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C340 File Offset: 0x0000A540
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MapiAdministrationSession>(this);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C384 File Offset: 0x0000A584
		public RawSecurityDescriptor GetMailboxSecurityDescriptor(MailboxId mailboxId)
		{
			this.CheckRequirementsOnMailboxIdToContinue(mailboxId);
			RawSecurityDescriptor returnValue = null;
			base.InvokeWithWrappedException(delegate()
			{
				returnValue = this.Administration.GetMailboxSecurityDescriptor(mailboxId.MailboxDatabaseId.Guid, mailboxId.MailboxGuid);
			}, Strings.ExceptionGetMailboxSecurityDescriptor(mailboxId.MailboxDatabaseId.Guid.ToString(), mailboxId.MailboxGuid.ToString()), mailboxId);
			return returnValue;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C450 File Offset: 0x0000A650
		public void SetMailboxSecurityDescriptor(MailboxId mailboxId, RawSecurityDescriptor rawSecurityDescriptor)
		{
			this.CheckRequirementsOnMailboxIdToContinue(mailboxId);
			if (rawSecurityDescriptor == null)
			{
				throw new ArgumentNullException("rawSecurityDescriptor");
			}
			base.InvokeWithWrappedException(delegate()
			{
				this.Administration.SetMailboxSecurityDescriptor(mailboxId.MailboxDatabaseId.Guid, mailboxId.MailboxGuid, rawSecurityDescriptor);
			}, Strings.ExceptionSetMailboxSecurityDescriptor(mailboxId.MailboxDatabaseId.Guid.ToString(), mailboxId.MailboxGuid.ToString()), mailboxId);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C520 File Offset: 0x0000A720
		public void SyncMailboxWithDS(MailboxId mailboxId)
		{
			this.CheckRequirementsOnMailboxIdToContinue(mailboxId);
			base.InvokeWithWrappedException(delegate()
			{
				this.Administration.SyncMailboxWithDS(mailboxId.MailboxDatabaseId.Guid, mailboxId.MailboxGuid);
			}, Strings.ExceptionFailedToLetStorePickupMailboxChange(mailboxId.MailboxGuid.ToString(), mailboxId.MailboxDatabaseId.Guid.ToString()), mailboxId);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public void ForceStoreToRefreshMailbox(MailboxId mailboxId)
		{
			this.CheckRequirementsOnMailboxIdToContinue(mailboxId);
			MapiSession.ErrorTranslatorDelegate translateError = delegate(ref LocalizedString message, Exception e)
			{
				return new FailedToRefreshMailboxException(e.Message, mailboxId.ToString(), e);
			};
			base.InvokeWithWrappedException(delegate()
			{
				int num = 0;
				while (3 > num)
				{
					try
					{
						this.Administration.ClearAbsentInDsFlagOnMailbox(mailboxId.MailboxDatabaseId.Guid, mailboxId.MailboxGuid);
						this.Administration.PurgeCachedMailboxObject(mailboxId.MailboxGuid);
						break;
					}
					catch (MapiExceptionNoAccess)
					{
						if (2 <= num)
						{
							throw;
						}
						this.Administration.PurgeCachedMailboxObject(mailboxId.MailboxGuid);
					}
					catch (MapiExceptionNotFound)
					{
						if (2 <= num)
						{
							throw;
						}
						this.Administration.PurgeCachedMailboxObject(mailboxId.MailboxGuid);
					}
					catch (MapiExceptionUnknownMailbox)
					{
						this.Administration.PurgeCachedMailboxObject(mailboxId.MailboxGuid);
						break;
					}
					num++;
				}
			}, LocalizedString.Empty, null, translateError);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C748 File Offset: 0x0000A948
		public void DeleteMailbox(MailboxId mailboxId)
		{
			this.CheckRequirementsOnMailboxIdToContinue(mailboxId);
			base.InvokeWithWrappedException(delegate()
			{
				this.Administration.DeletePrivateMailbox(mailboxId.MailboxDatabaseId.Guid, mailboxId.MailboxGuid, 2);
			}, Strings.ExceptionDeleteMailbox(mailboxId.MailboxDatabaseId.Guid.ToString(), mailboxId.MailboxGuid.ToString()), mailboxId);
		}
	}
}
