using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A7E RID: 2686
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailMessageDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x06006257 RID: 25175 RVA: 0x0019F6F3 File Offset: 0x0019D8F3
		public MailMessageDataProvider(ADSessionSettings adSessionSettings, ADUser mailboxOwner, ISecurityAccessToken userToken, string action) : base(adSessionSettings, mailboxOwner, userToken, action)
		{
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x0019F700 File Offset: 0x0019D900
		public MailMessageDataProvider(ADSessionSettings adSessionSettings, ADUser mailboxOwner, string action) : base(adSessionSettings, mailboxOwner, action)
		{
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x0019F70B File Offset: 0x0019D90B
		internal MailMessageDataProvider()
		{
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x0019F970 File Offset: 0x0019DB70
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			if (filter != null && !(filter is FalseFilter))
			{
				throw new NotSupportedException("filter");
			}
			if (sortBy != null)
			{
				throw new NotSupportedException("sortBy");
			}
			if (rootId != null && !(rootId is MailboxStoreObjectId))
			{
				throw new NotSupportedException("rootId: " + rootId.GetType().FullName);
			}
			if (!typeof(MailMessage).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
			MailboxStoreObjectId messageId = rootId as MailboxStoreObjectId;
			if (messageId != null)
			{
				MailMessage mailMessage = new MailMessage();
				try
				{
					using (MessageItem messageItem = MessageItem.Bind(base.MailboxSession, messageId.StoreObjectId, mailMessage.Schema.AllDependentXsoProperties))
					{
						mailMessage.LoadDataFromXso(messageId.MailboxOwnerId, messageItem);
						mailMessage.SetRecipients(messageItem.Recipients);
					}
				}
				catch (ObjectNotFoundException)
				{
					yield break;
				}
				yield return (T)((object)mailMessage);
			}
			yield break;
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x0019F9A3 File Offset: 0x0019DBA3
		protected override void InternalSave(ConfigurableObject instance)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x0019F9AA File Offset: 0x0019DBAA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailMessageDataProvider>(this);
		}
	}
}
