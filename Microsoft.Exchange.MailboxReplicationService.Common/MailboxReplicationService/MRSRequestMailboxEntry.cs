using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F0 RID: 496
	[Serializable]
	internal class MRSRequestMailboxEntry : MRSRequest, IRequestIndexEntry, IConfigurable
	{
		// Token: 0x060014E3 RID: 5347 RVA: 0x0002F250 File Offset: 0x0002D450
		public MRSRequestMailboxEntry()
		{
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0002F258 File Offset: 0x0002D458
		public MRSRequestMailboxEntry(UserConfiguration userConfiguration)
		{
			UserConfigurationDictionaryHelper.Fill(userConfiguration, this, MRSRequestSchema.PersistedProperties);
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x0002F26C File Offset: 0x0002D46C
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x0002F274 File Offset: 0x0002D474
		public RequestIndexId RequestIndexId
		{
			get
			{
				return this.requestIndexId;
			}
			internal set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.requestIndexId = value;
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0002F28C File Offset: 0x0002D48C
		public static ICollection<MRSRequestMailboxEntry> Read(MailboxSession mailboxSession, RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = null)
		{
			List<MRSRequestMailboxEntry> list = new List<MRSRequestMailboxEntry>();
			ICollection<UserConfiguration> collection = mailboxSession.UserConfigurationManager.FindMailboxConfigurations("MRSRequest", UserConfigurationSearchFlags.Prefix);
			try
			{
				foreach (UserConfiguration userConfiguration in collection)
				{
					MRSRequestMailboxEntry mrsrequestMailboxEntry = new MRSRequestMailboxEntry(userConfiguration);
					if (mrsrequestMailboxEntry.MatchesFilter(requestIndexEntryQueryFilter))
					{
						list.Add(mrsrequestMailboxEntry);
					}
				}
			}
			finally
			{
				foreach (UserConfiguration userConfiguration2 in collection)
				{
					userConfiguration2.Dispose();
				}
			}
			return list;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0002F350 File Offset: 0x0002D550
		private bool MatchesFilter(RequestIndexEntryQueryFilter requestIndexEntryQueryFilter)
		{
			if (requestIndexEntryQueryFilter == null)
			{
				return true;
			}
			if (requestIndexEntryQueryFilter.RequestType != base.Type)
			{
				return false;
			}
			if (requestIndexEntryQueryFilter.RequestGuid != Guid.Empty && requestIndexEntryQueryFilter.RequestGuid != base.RequestGuid)
			{
				return false;
			}
			if (requestIndexEntryQueryFilter.SourceMailbox != null && requestIndexEntryQueryFilter.SourceMailbox.ObjectGuid != ((base.SourceUserId == null) ? Guid.Empty : base.SourceUserId.ObjectGuid))
			{
				return false;
			}
			if (requestIndexEntryQueryFilter.TargetMailbox != null && requestIndexEntryQueryFilter.TargetMailbox.ObjectGuid != ((base.TargetUserId == null) ? Guid.Empty : base.TargetUserId.ObjectGuid))
			{
				return false;
			}
			if (requestIndexEntryQueryFilter.SourceDatabase != null && requestIndexEntryQueryFilter.SourceDatabase.ObjectGuid != ((base.SourceMDB == null) ? Guid.Empty : base.SourceMDB.ObjectGuid))
			{
				return false;
			}
			if (requestIndexEntryQueryFilter.TargetDatabase != null && requestIndexEntryQueryFilter.TargetDatabase.ObjectGuid != ((base.TargetMDB == null) ? Guid.Empty : base.TargetMDB.ObjectGuid))
			{
				return false;
			}
			if (requestIndexEntryQueryFilter.Status != RequestStatus.None && requestIndexEntryQueryFilter.Status != base.Status)
			{
				return false;
			}
			if (requestIndexEntryQueryFilter.Flags != RequestFlags.None && (requestIndexEntryQueryFilter.Flags & base.Flags) != requestIndexEntryQueryFilter.Flags)
			{
				return false;
			}
			if (requestIndexEntryQueryFilter.NotFlags != RequestFlags.None && (~requestIndexEntryQueryFilter.NotFlags & base.Flags) != base.Flags)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(requestIndexEntryQueryFilter.BatchName) && requestIndexEntryQueryFilter.BatchName.Equals(base.BatchName, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			string text = requestIndexEntryQueryFilter.WildcardedNameSearch ? Wildcard.ConvertToRegexPattern(requestIndexEntryQueryFilter.RequestName) : requestIndexEntryQueryFilter.RequestName;
			return string.IsNullOrEmpty(text) || !(requestIndexEntryQueryFilter.WildcardedNameSearch ? (!Regex.IsMatch(base.Name, text, RegexOptions.IgnoreCase)) : (!base.Name.Equals(text, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0002F53E File Offset: 0x0002D73E
		public RequestJobObjectId GetRequestJobId()
		{
			return new RequestJobObjectId(base.RequestGuid, (base.StorageMDB == null) ? Guid.Empty : base.StorageMDB.ObjectGuid, this);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0002F566 File Offset: 0x0002D766
		public RequestIndexEntryObjectId GetRequestIndexEntryId(RequestBase owner)
		{
			return new RequestIndexEntryObjectId(base.RequestGuid, base.Type, base.OrganizationId, this.RequestIndexId, owner);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0002F586 File Offset: 0x0002D786
		Guid IRequestIndexEntry.get_RequestGuid()
		{
			return base.RequestGuid;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0002F58E File Offset: 0x0002D78E
		void IRequestIndexEntry.set_RequestGuid(Guid A_1)
		{
			base.RequestGuid = A_1;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0002F597 File Offset: 0x0002D797
		string IRequestIndexEntry.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0002F59F File Offset: 0x0002D79F
		void IRequestIndexEntry.set_Name(string A_1)
		{
			base.Name = A_1;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0002F5A8 File Offset: 0x0002D7A8
		RequestStatus IRequestIndexEntry.get_Status()
		{
			return base.Status;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		void IRequestIndexEntry.set_Status(RequestStatus A_1)
		{
			base.Status = A_1;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0002F5B9 File Offset: 0x0002D7B9
		RequestFlags IRequestIndexEntry.get_Flags()
		{
			return base.Flags;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0002F5C1 File Offset: 0x0002D7C1
		void IRequestIndexEntry.set_Flags(RequestFlags A_1)
		{
			base.Flags = A_1;
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0002F5CA File Offset: 0x0002D7CA
		string IRequestIndexEntry.get_RemoteHostName()
		{
			return base.RemoteHostName;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0002F5D2 File Offset: 0x0002D7D2
		void IRequestIndexEntry.set_RemoteHostName(string A_1)
		{
			base.RemoteHostName = A_1;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0002F5DB File Offset: 0x0002D7DB
		string IRequestIndexEntry.get_BatchName()
		{
			return base.BatchName;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0002F5E3 File Offset: 0x0002D7E3
		void IRequestIndexEntry.set_BatchName(string A_1)
		{
			base.BatchName = A_1;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0002F5EC File Offset: 0x0002D7EC
		ADObjectId IRequestIndexEntry.get_SourceMDB()
		{
			return base.SourceMDB;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0002F5F4 File Offset: 0x0002D7F4
		void IRequestIndexEntry.set_SourceMDB(ADObjectId A_1)
		{
			base.SourceMDB = A_1;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0002F5FD File Offset: 0x0002D7FD
		ADObjectId IRequestIndexEntry.get_TargetMDB()
		{
			return base.TargetMDB;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0002F605 File Offset: 0x0002D805
		void IRequestIndexEntry.set_TargetMDB(ADObjectId A_1)
		{
			base.TargetMDB = A_1;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0002F60E File Offset: 0x0002D80E
		ADObjectId IRequestIndexEntry.get_StorageMDB()
		{
			return base.StorageMDB;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0002F616 File Offset: 0x0002D816
		void IRequestIndexEntry.set_StorageMDB(ADObjectId A_1)
		{
			base.StorageMDB = A_1;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0002F61F File Offset: 0x0002D81F
		string IRequestIndexEntry.get_FilePath()
		{
			return base.FilePath;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0002F627 File Offset: 0x0002D827
		void IRequestIndexEntry.set_FilePath(string A_1)
		{
			base.FilePath = A_1;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0002F630 File Offset: 0x0002D830
		MRSRequestType IRequestIndexEntry.get_Type()
		{
			return base.Type;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0002F638 File Offset: 0x0002D838
		void IRequestIndexEntry.set_Type(MRSRequestType A_1)
		{
			base.Type = A_1;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0002F641 File Offset: 0x0002D841
		ADObjectId IRequestIndexEntry.get_TargetUserId()
		{
			return base.TargetUserId;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0002F649 File Offset: 0x0002D849
		void IRequestIndexEntry.set_TargetUserId(ADObjectId A_1)
		{
			base.TargetUserId = A_1;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0002F652 File Offset: 0x0002D852
		ADObjectId IRequestIndexEntry.get_SourceUserId()
		{
			return base.SourceUserId;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0002F65A File Offset: 0x0002D85A
		void IRequestIndexEntry.set_SourceUserId(ADObjectId A_1)
		{
			base.SourceUserId = A_1;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0002F663 File Offset: 0x0002D863
		OrganizationId IRequestIndexEntry.get_OrganizationId()
		{
			return base.OrganizationId;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0002F66B File Offset: 0x0002D86B
		DateTime? IRequestIndexEntry.get_WhenChanged()
		{
			return base.WhenChanged;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0002F673 File Offset: 0x0002D873
		DateTime? IRequestIndexEntry.get_WhenCreated()
		{
			return base.WhenCreated;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0002F67B File Offset: 0x0002D87B
		DateTime? IRequestIndexEntry.get_WhenChangedUTC()
		{
			return base.WhenChangedUTC;
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0002F683 File Offset: 0x0002D883
		DateTime? IRequestIndexEntry.get_WhenCreatedUTC()
		{
			return base.WhenCreatedUTC;
		}

		// Token: 0x04000A5A RID: 2650
		private RequestIndexId requestIndexId;
	}
}
