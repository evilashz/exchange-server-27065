using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.DDIService;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006A6 RID: 1702
	public static class IdentityExtensions
	{
		// Token: 0x060048BE RID: 18622 RVA: 0x000DEA05 File Offset: 0x000DCC05
		public static Identity ToIdentity(this MailEnabledRecipient entry)
		{
			return entry.Id.ToIdentity(entry.DisplayName);
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x000DEA18 File Offset: 0x000DCC18
		public static Identity ToIdentity(this ReducedRecipient entry)
		{
			return entry.Id.ToIdentity(entry.DisplayName);
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x000DEA2B File Offset: 0x000DCC2B
		public static Identity ToIdentity(this CASMailbox entry)
		{
			return entry.Id.ToIdentity(entry.DisplayName);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x000DEA3E File Offset: 0x000DCC3E
		public static Identity ToIdentity(this ADObject entry)
		{
			return entry.Id.ToIdentity(entry.Name);
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x000DEA51 File Offset: 0x000DCC51
		public static Identity ToIdentity(this ADObjectId identity)
		{
			if (identity != null)
			{
				return identity.ToIdentity(identity.Name);
			}
			return null;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x000DEA64 File Offset: 0x000DCC64
		public static Identity ToIdentity(this ADObjectId identity, string displayName)
		{
			if (identity != null)
			{
				return new Identity(identity, displayName);
			}
			return null;
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x000DEA72 File Offset: 0x000DCC72
		public static Identity ToIdentity(this ObjectId identity, string displayName)
		{
			if (identity != null)
			{
				return new Identity(identity.ToString(), displayName);
			}
			return null;
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x000DEA85 File Offset: 0x000DCC85
		public static Identity ToIdentity(this IConfigurable entry, string displayName)
		{
			return new Identity(entry.Identity.ToString(), displayName);
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x000DEA98 File Offset: 0x000DCC98
		public static Identity ToIdentity(this SupervisionListEntry entry)
		{
			return new Identity(entry.Identity.ToString(), entry.EntryName);
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x000DEAB0 File Offset: 0x000DCCB0
		public static Identity ToIdentity(this MailboxFolder entry)
		{
			string name = entry.Name;
			if (entry.DefaultFolderType == DefaultFolderType.Root)
			{
				Microsoft.Exchange.Data.Storage.Management.MailboxFolderId mailboxFolderId = (Microsoft.Exchange.Data.Storage.Management.MailboxFolderId)entry.Identity;
				if (ADObjectId.Equals(mailboxFolderId.MailboxOwnerId, EacRbacPrincipal.Instance.ExecutingUserId))
				{
					name = EacRbacPrincipal.Instance.Name;
				}
				else
				{
					name = mailboxFolderId.MailboxOwnerId.Name;
				}
			}
			return new Identity(entry.FolderStoreObjectId, name);
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x000DEB2A File Offset: 0x000DCD2A
		public static Identity ToIdentity(this ExtendedOrganizationalUnit ou)
		{
			return ou.Id.ToIdentity(ou.CanonicalName);
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x000DEB40 File Offset: 0x000DCD40
		public static Identity ToIdentity(this InboxRuleId id)
		{
			if (id.StoreObjectId != null && id.MailboxOwnerId != null)
			{
				return new Identity(id.MailboxOwnerId.ObjectGuid.ToString() + '\\' + id.StoreObjectId.ToString(), id.Name);
			}
			return id.ToIdentity(id.Name);
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x000DEBB8 File Offset: 0x000DCDB8
		public static Identity ToIdentity(this UMCallAnsweringRuleId id)
		{
			Guid ruleIdGuid = id.RuleIdGuid;
			if (id.MailboxOwnerId != null)
			{
				return new Identity(id.RuleIdGuid.ToString(), id.RuleIdGuid.ToString());
			}
			return id.ToIdentity(id.RuleIdGuid.ToString());
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x000DEC1C File Offset: 0x000DCE1C
		public static Identity ToIdentity(this AppId id)
		{
			if (id.AppIdValue != null && id.MailboxOwnerId != null)
			{
				return new Identity(id.MailboxOwnerId.ObjectGuid.ToString() + '\\' + id.AppIdValue, id.DisplayName);
			}
			return id.ToIdentity(id.DisplayName);
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x000DEC7C File Offset: 0x000DCE7C
		public static Identity ToIdentity(this AggregationSubscriptionIdentity id, string displayName)
		{
			return new Identity(id.GuidIdentityString, displayName);
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x000DEC8C File Offset: 0x000DCE8C
		public static Identity ToIdentity(this MobileDeviceConfiguration deviceInfo, string displayName)
		{
			return new Identity(deviceInfo.Guid.ToString(), displayName);
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x000DECBB File Offset: 0x000DCEBB
		public static IEnumerable<Identity> ToIdentities(this IEnumerable<ADObjectId> identities)
		{
			return from identity in identities
			select identity.ToIdentity();
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x000DED0C File Offset: 0x000DCF0C
		public static Identity ToIdentity(this MessageClassification[] classifications)
		{
			if (classifications.IsNullOrEmpty())
			{
				return null;
			}
			string[] value = Array.ConvertAll<MessageClassification, string>(classifications, (MessageClassification x) => x.DisplayName);
			string[] value2 = Array.ConvertAll<MessageClassification, string>(classifications, (MessageClassification x) => x.Guid.ToString());
			return new Identity(string.Join(",", value2), string.Join(",", value));
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x000DED90 File Offset: 0x000DCF90
		public static Identity ToIdentity(this AggregationSubscriptionIdentity[] subscriptionIds, InboxRule ruleObj)
		{
			IList<string> list = null;
			if (!subscriptionIds.IsNullOrEmpty())
			{
				list = ruleObj.GetSubscriptionEmailAddresses((IList<AggregationSubscriptionIdentity>)subscriptionIds);
			}
			if (list != null && list.Count > 0)
			{
				string[] value = Array.ConvertAll<AggregationSubscriptionIdentity, string>(subscriptionIds, (AggregationSubscriptionIdentity x) => x.ToString());
				return new Identity(string.Join(",", value), string.Join(",", list.ToArray<string>()));
			}
			return null;
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x000DEE06 File Offset: 0x000DD006
		public static Identity ToIdentity(this ExDateTime? dateTimeValue)
		{
			if (dateTimeValue == null)
			{
				return null;
			}
			return new Identity(dateTimeValue.ToUserDateTimeGeneralFormatString(), dateTimeValue.ToUserWeekdayDateString());
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x000DEE24 File Offset: 0x000DD024
		public static INamedIdentity ToIdParameter(this Identity identity)
		{
			if (null == identity)
			{
				return null;
			}
			return identity;
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x000DEE32 File Offset: 0x000DD032
		public static INamedIdentity ToMailboxFolderIdParameter(this Identity identity)
		{
			if (null != identity && identity.RawIdentity.IndexOf(':') < 0)
			{
				return new Identity(':' + identity.RawIdentity, identity.DisplayName);
			}
			return identity;
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x000DEE74 File Offset: 0x000DD074
		public static MailboxFolderPermissionIdentity ToMailboxFolderPermissionIdentity(this Identity identity)
		{
			if (null == identity)
			{
				return null;
			}
			string text = HttpUtility.UrlDecode(identity.RawIdentity);
			Identity[] array = Array.ConvertAll<string, Identity>(text.Split(new char[]
			{
				':'
			}), (string x) => Identity.ParseIdentity(x));
			return new MailboxFolderPermissionIdentity(array[0], array[1]);
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x000DEED9 File Offset: 0x000DD0D9
		[Conditional("DEBUG")]
		private static void EnsureMailboxFolderSeparator(string idString)
		{
			if (idString.IndexOf(':') == -1)
			{
				throw new ArgumentException("Identity must contain the mailbox folder seperator ('" + ':' + "') for conversion to MailboxFolderPermissionIdentity.");
			}
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x000DEF04 File Offset: 0x000DD104
		public static string[] ToTaskIdStringArray(this Identity identity)
		{
			if (identity == null || string.IsNullOrEmpty(identity.RawIdentity))
			{
				return null;
			}
			return new string[]
			{
				identity.RawIdentity
			};
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x000DEF43 File Offset: 0x000DD143
		public static INamedIdentity[] ToIdParameters(this IEnumerable<Identity> identities)
		{
			if (identities == null)
			{
				return null;
			}
			return (from identity in identities
			where identity != null
			select identity).ToArray<Identity>();
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x000DEF72 File Offset: 0x000DD172
		public static void FaultIfNull(this Identity identity)
		{
			if (null == identity)
			{
				throw new FaultException(new ArgumentNullException("identity").Message);
			}
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x000DEF92 File Offset: 0x000DD192
		public static void FaultIfNull(this Identity identity, string errorMessage)
		{
			if (null == identity)
			{
				throw new FaultException(errorMessage);
			}
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x000DEFA4 File Offset: 0x000DD1A4
		public static void FaultIfNullOrEmpty(this Identity[] identities)
		{
			identities.FaultIfNull();
			if (identities.IsNullOrEmpty())
			{
				throw new FaultException(new ArgumentNullException("identities").Message);
			}
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x000DEFC9 File Offset: 0x000DD1C9
		public static void FaultIfNotExactlyOne(this Identity[] identities)
		{
			identities.FaultIfNullOrEmpty();
			if (identities.Length > 1)
			{
				throw new FaultException(new ArgumentException("identities").Message);
			}
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x000DEFF5 File Offset: 0x000DD1F5
		public static void FaultIfNull(this IEnumerable<Identity> identities)
		{
			if (identities != null)
			{
				if (!identities.Any((Identity x) => x == null))
				{
					return;
				}
			}
			throw new FaultException(new ArgumentNullException("identities").Message);
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x000DF034 File Offset: 0x000DD234
		public static bool IsNullOrEmpty(this Array array)
		{
			return array == null || 0 == array.Length;
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x000DF044 File Offset: 0x000DD244
		public static Identity ResolveByType(this Identity identity, IdentityType type)
		{
			Identity result = null;
			if (type == IdentityType.MailboxFolder)
			{
				PowerShellResults<MailboxFolder> @object = MailboxFolders.Instance.GetObject(identity);
				if (@object.SucceededWithValue)
				{
					result = @object.Value.Folder.ToIdentity();
				}
				return result;
			}
			throw new NotSupportedException();
		}
	}
}
