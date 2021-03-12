using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200012A RID: 298
	[Serializable]
	public class MailboxStoreIdParameter : IIdentityParameter
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00022D72 File Offset: 0x00020F72
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00022D7A File Offset: 0x00020F7A
		protected string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
			set
			{
				this.rawIdentity = value;
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00022D84 File Offset: 0x00020F84
		public MailboxStoreIdParameter(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (identity.Length == 0)
			{
				throw new ArgumentException(Strings.ErrorEmptyParameter(base.GetType().ToString()), "identity");
			}
			this.rawIdentity = identity;
			this.name = identity;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00022DDC File Offset: 0x00020FDC
		public MailboxStoreIdParameter(Mailbox mailbox)
		{
			if (mailbox == null)
			{
				throw new ArgumentNullException("mailbox");
			}
			if (mailbox.Identity == null)
			{
				throw new ArgumentNullException("mailbox.Identity");
			}
			this.rawIdentity = mailbox.Id.ToString();
			this.name = mailbox.Id.Name;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00022E32 File Offset: 0x00021032
		public MailboxStoreIdParameter(ADObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			this.rawIdentity = objectId.ToString();
			this.name = objectId.Name;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00022E60 File Offset: 0x00021060
		public MailboxStoreIdParameter(MailboxStoreIdentity objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			this.rawIdentity = objectId.ToString();
			this.name = objectId.MailboxOwnerId.Name;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00022E93 File Offset: 0x00021093
		public MailboxStoreIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00022EAD File Offset: 0x000210AD
		public MailboxStoreIdParameter()
		{
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00022EB5 File Offset: 0x000210B5
		public static MailboxStoreIdParameter Parse(string identity)
		{
			return new MailboxStoreIdParameter(identity);
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00022EBD File Offset: 0x000210BD
		public virtual bool IsFullyQualified
		{
			get
			{
				return !string.IsNullOrEmpty(this.name);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00022ECD File Offset: 0x000210CD
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00022ED5 File Offset: 0x000210D5
		public override string ToString()
		{
			return this.RawIdentity;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00022EE0 File Offset: 0x000210E0
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00022EF8 File Offset: 0x000210F8
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (this.IsFullyQualified)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
			}
			else
			{
				notFoundReason = null;
			}
			QueryFilter queryFilter = this.GetQueryFilter(session);
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					optionalData.AdditionalFilter
				});
			}
			return session.FindPaged<T>(queryFilter, rootId, false, null, 0);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00022F68 File Offset: 0x00021168
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			MailboxStoreIdentity mailboxStoreIdentity = objectId as MailboxStoreIdentity;
			if (mailboxStoreIdentity == null)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterType("objectId", typeof(MailboxStoreIdentity).Name), "objectId");
			}
			this.rawIdentity = mailboxStoreIdentity.ToString();
			this.name = mailboxStoreIdentity.MailboxOwnerId.Name;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00022FC5 File Offset: 0x000211C5
		public virtual string GetADUserName()
		{
			return this.RawIdentity;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00022FCD File Offset: 0x000211CD
		public virtual QueryFilter GetQueryFilter(IConfigDataProvider session)
		{
			return null;
		}

		// Token: 0x04000288 RID: 648
		private string name;

		// Token: 0x04000289 RID: 649
		private string rawIdentity;
	}
}
