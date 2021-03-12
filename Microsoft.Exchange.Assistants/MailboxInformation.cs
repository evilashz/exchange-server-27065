using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200006F RID: 111
	internal sealed class MailboxInformation : IMailboxInformation
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000FFFF File Offset: 0x0000E1FF
		private MailboxInformation(byte[] mailboxGuid, Guid databaseGuid, string displayName, bool active, DateTime lastLogonTime) : this(new Guid(mailboxGuid), databaseGuid, displayName, active, lastLogonTime, null)
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00010014 File Offset: 0x0000E214
		private MailboxInformation(Guid mailboxGuid, Guid databaseGuid, string displayName, bool active, DateTime lastLogonTime, TenantPartitionHint tenantPartitionHint)
		{
			this.MailboxData = new StoreMailboxDataExtended(mailboxGuid, databaseGuid, displayName, null, tenantPartitionHint, this.IsArchiveMailbox(), this.IsGroupMailbox(), this.IsTeamSiteMailbox(), this.IsSharedMailbox());
			this.Active = active;
			this.LastLogonTime = lastLogonTime;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00010060 File Offset: 0x0000E260
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00010068 File Offset: 0x0000E268
		internal StoreMailboxDataExtended MailboxData { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00010071 File Offset: 0x0000E271
		public Guid MailboxGuid
		{
			get
			{
				return this.MailboxData.Guid;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0001007E File Offset: 0x0000E27E
		public string DisplayName
		{
			get
			{
				return this.MailboxData.DisplayName;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0001008B File Offset: 0x0000E28B
		// (set) Token: 0x06000336 RID: 822 RVA: 0x00010093 File Offset: 0x0000E293
		public DateTime LastProcessedDate { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0001009C File Offset: 0x0000E29C
		// (set) Token: 0x06000338 RID: 824 RVA: 0x000100A4 File Offset: 0x0000E2A4
		public bool Active { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000100AD File Offset: 0x0000E2AD
		// (set) Token: 0x0600033A RID: 826 RVA: 0x000100B5 File Offset: 0x0000E2B5
		public DateTime LastLogonTime { get; private set; }

		// Token: 0x0600033B RID: 827 RVA: 0x000100C0 File Offset: 0x0000E2C0
		public static DateTime GetLastLogonTime(PropValue[] propvalueArray)
		{
			PropValue mailboxProperty = MailboxTableQuery.GetMailboxProperty(propvalueArray, PropTag.LastLogonTime);
			if (mailboxProperty.PropType != PropType.SysTime)
			{
				return DateTime.MinValue;
			}
			return mailboxProperty.GetDateTime();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000100F1 File Offset: 0x0000E2F1
		public bool IsArchiveMailbox()
		{
			return StoreSession.IsArchiveMailbox(this.GetIntValue(PropTag.MailboxMiscFlags));
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00010103 File Offset: 0x0000E303
		public bool IsPublicFolderMailbox()
		{
			return StoreSession.IsPublicFolderMailbox(this.GetIntValue(PropTag.MailboxType));
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010115 File Offset: 0x0000E315
		public bool IsGroupMailbox()
		{
			return StoreSession.IsGroupMailbox(this.GetIntValue(PropTag.MailboxTypeDetail));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010127 File Offset: 0x0000E327
		public bool IsUserMailbox()
		{
			return StoreSession.IsUserMailbox(this.GetIntValue(PropTag.MailboxTypeDetail));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010139 File Offset: 0x0000E339
		public bool IsTeamSiteMailbox()
		{
			return StoreSession.IsTeamSiteMailbox(this.GetIntValue(PropTag.MailboxTypeDetail));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001014B File Offset: 0x0000E34B
		public bool IsSharedMailbox()
		{
			return StoreSession.IsSharedMailbox(this.GetIntValue(PropTag.MailboxTypeDetail));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010160 File Offset: 0x0000E360
		public object GetMailboxProperty(PropertyTagPropertyDefinition property)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			foreach (PropValue propValue in this.mailboxProperties)
			{
				uint num = (uint)(propValue.PropTag & (PropTag)4294901760U);
				uint num2 = property.PropertyTag & 4294901760U;
				if (num == num2)
				{
					object result;
					if (propValue.PropTag == (PropTag)property.PropertyTag)
					{
						result = propValue.Value;
					}
					else
					{
						result = null;
					}
					return result;
				}
			}
			throw new ArgumentException("property");
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000101F0 File Offset: 0x0000E3F0
		public override string ToString()
		{
			string text = (this.DisplayName != null) ? this.DisplayName : "<null>";
			return string.Concat(new string[]
			{
				"MailboxGuid=",
				this.MailboxGuid.ToString(),
				",DisplayName='",
				text,
				"'"
			});
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00010254 File Offset: 0x0000E454
		internal static MailboxInformation Create(ExRpcAdmin rpcAdmin, Guid mailboxGuid, Guid databaseGuid)
		{
			string displayName = string.Empty;
			bool flag = false;
			DateTime lastLogonTime = DateTime.MinValue;
			try
			{
				PropValue[][] mailboxTableInfo = rpcAdmin.GetMailboxTableInfo(databaseGuid, mailboxGuid, new PropTag[]
				{
					PropTag.UserGuid,
					PropTag.DisplayName,
					PropTag.DateDiscoveredAbsentInDS,
					PropTag.LastLogonTime
				});
				foreach (PropValue[] array2 in mailboxTableInfo)
				{
					if (array2.Length != 4 || array2[0].PropTag != PropTag.UserGuid || array2[1].PropTag != PropTag.DisplayName || !new Guid(array2[0].GetBytes()).Equals(mailboxGuid))
					{
						MailboxInformation.tracer.TraceDebug(0L, "MailboxInformation: Row does not contain the expected data.");
					}
					else
					{
						displayName = (string)array2[1].RawValue;
						lastLogonTime = MailboxInformation.GetLastLogonTime(array2);
						if (array2[2].PropTag != PropTag.DateDiscoveredAbsentInDS)
						{
							MailboxInformation.tracer.TraceDebug<Guid>(0L, "MailboxInformation: Mailbox {1} is active.", mailboxGuid);
							flag = true;
							break;
						}
					}
				}
			}
			catch (MapiExceptionNotFound)
			{
			}
			MailboxInformation.tracer.TraceDebug<Guid, bool>(0L, "MailboxInformation: Mailbox {1} active state is {2}.", mailboxGuid, flag);
			return new MailboxInformation(mailboxGuid, databaseGuid, displayName, flag, lastLogonTime, null);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000103B0 File Offset: 0x0000E5B0
		internal static MailboxInformation Create(byte[] mailboxGuid, Guid databaseGuid, string displayName, ControlData controlData, PropValue[] mailboxProperties, DateTime lastLogonTime, TenantPartitionHint tenantPartitionHint = null)
		{
			MailboxInformation mailboxInformation = new MailboxInformation(new Guid(mailboxGuid), databaseGuid, displayName, true, lastLogonTime, tenantPartitionHint);
			if (controlData != null)
			{
				mailboxInformation.LastProcessedDate = controlData.LastProcessedDate;
			}
			else
			{
				mailboxInformation.LastProcessedDate = DateTime.MinValue;
			}
			mailboxInformation.mailboxProperties = mailboxProperties;
			mailboxInformation.MailboxData.IsPublicFolderMailbox = mailboxInformation.IsPublicFolderMailbox();
			return mailboxInformation;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010408 File Offset: 0x0000E608
		private int GetIntValue(PropTag tag)
		{
			if (this.mailboxProperties == null)
			{
				return 0;
			}
			int num = tag.Id();
			foreach (PropValue propValue in this.mailboxProperties)
			{
				if (propValue.PropTag.Id() == num)
				{
					int result;
					if (propValue.PropType == PropType.Int)
					{
						result = propValue.GetInt();
					}
					else
					{
						result = 0;
					}
					return result;
				}
			}
			return 0;
		}

		// Token: 0x040001DD RID: 477
		private static Trace tracer = ExTraceGlobals.EventAccessTracer;

		// Token: 0x040001DE RID: 478
		private PropValue[] mailboxProperties;
	}
}
