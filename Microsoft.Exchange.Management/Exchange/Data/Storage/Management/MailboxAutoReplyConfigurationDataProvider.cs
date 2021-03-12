using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.OOF;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020007BE RID: 1982
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAutoReplyConfigurationDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x060045A6 RID: 17830 RVA: 0x0011E1A2 File Offset: 0x0011C3A2
		public MailboxAutoReplyConfigurationDataProvider(ExchangePrincipal mailboxOwner, string action) : base(mailboxOwner, action)
		{
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x0011E1AC File Offset: 0x0011C3AC
		public MailboxAutoReplyConfigurationDataProvider(MailboxSession session) : base(session)
		{
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x0011E474 File Offset: 0x0011C674
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			if (filter != null && !(filter is FalseFilter))
			{
				throw new NotSupportedException("filter");
			}
			if (rootId != null && rootId is ADObjectId && !ADObjectId.Equals((ADObjectId)rootId, base.MailboxSession.MailboxOwner.ObjectId))
			{
				throw new NotSupportedException("rootId");
			}
			if (!typeof(MailboxAutoReplyConfiguration).IsAssignableFrom(typeof(T)))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
			MailboxAutoReplyConfiguration configObject = (MailboxAutoReplyConfiguration)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			configObject.MailboxOwnerId = base.MailboxSession.MailboxOwner.ObjectId;
			UserOofSettings userOofSettings = UserOofSettings.GetUserOofSettings(base.MailboxSession);
			if (userOofSettings == null)
			{
				userOofSettings = UserOofSettings.CreateDefault();
			}
			configObject.AutoReplyState = userOofSettings.OofState;
			configObject.ExternalAudience = userOofSettings.ExternalAudience;
			configObject.ExternalMessage = userOofSettings.ExternalReply.Message;
			configObject.InternalMessage = userOofSettings.InternalReply.Message;
			if (userOofSettings.Duration != null)
			{
				configObject.StartTime = userOofSettings.Duration.StartTime.ToLocalTime();
				configObject.EndTime = userOofSettings.Duration.EndTime.ToLocalTime();
			}
			yield return (T)((object)configObject);
			yield break;
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x0011E4A0 File Offset: 0x0011C6A0
		protected override void InternalSave(ConfigurableObject instance)
		{
			MailboxAutoReplyConfiguration mailboxAutoReplyConfiguration = (MailboxAutoReplyConfiguration)instance;
			UserOofSettings userOofSettings = UserOofSettings.GetUserOofSettings(base.MailboxSession);
			if (userOofSettings == null)
			{
				userOofSettings = UserOofSettings.CreateDefault();
			}
			if (mailboxAutoReplyConfiguration.IsModified(MailboxAutoReplyConfigurationSchema.AutoReplyState))
			{
				userOofSettings.OofState = mailboxAutoReplyConfiguration.AutoReplyState;
			}
			if (mailboxAutoReplyConfiguration.IsModified(MailboxAutoReplyConfigurationSchema.ExternalAudience))
			{
				userOofSettings.ExternalAudience = mailboxAutoReplyConfiguration.ExternalAudience;
			}
			if (mailboxAutoReplyConfiguration.IsModified(MailboxAutoReplyConfigurationSchema.ExternalMessage))
			{
				userOofSettings.ExternalReply.Message = mailboxAutoReplyConfiguration.ExternalMessage;
				userOofSettings.ExternalReply.LanguageTag = Thread.CurrentThread.CurrentCulture.Name;
			}
			if (mailboxAutoReplyConfiguration.IsModified(MailboxAutoReplyConfigurationSchema.InternalMessage))
			{
				userOofSettings.InternalReply.Message = mailboxAutoReplyConfiguration.InternalMessage;
				userOofSettings.InternalReply.LanguageTag = Thread.CurrentThread.CurrentCulture.Name;
			}
			if (mailboxAutoReplyConfiguration.IsModified(MailboxAutoReplyConfigurationSchema.StartTime))
			{
				userOofSettings.Duration.StartTime = mailboxAutoReplyConfiguration.StartTime.ToUniversalTime();
			}
			if (mailboxAutoReplyConfiguration.IsModified(MailboxAutoReplyConfigurationSchema.EndTime))
			{
				userOofSettings.Duration.EndTime = mailboxAutoReplyConfiguration.EndTime.ToUniversalTime();
			}
			try
			{
				userOofSettings.Save(base.MailboxSession);
			}
			catch (ObjectExistedException)
			{
				userOofSettings.Save(base.MailboxSession);
			}
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x0011E5E4 File Offset: 0x0011C7E4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxAutoReplyConfigurationDataProvider>(this);
		}
	}
}
