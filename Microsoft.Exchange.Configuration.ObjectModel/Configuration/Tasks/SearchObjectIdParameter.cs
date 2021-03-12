using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000144 RID: 324
	[Serializable]
	public class SearchObjectIdParameter : IIdentityParameter
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00024B14 File Offset: 0x00022D14
		public bool IsFullyQualified
		{
			get
			{
				return this.isFullyQualified;
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00024B1C File Offset: 0x00022D1C
		private void Parse(string rawString)
		{
			if (string.IsNullOrEmpty(rawString))
			{
				return;
			}
			this.identifier = rawString.Trim();
			SearchObjectId searchObjectId;
			if (SearchObjectId.TryParse(this.identifier, out searchObjectId))
			{
				this.objectIdentifier = searchObjectId;
				return;
			}
			if (this.identifier[this.identifier.Length - 1] == '*')
			{
				this.isFullyQualified = false;
				this.identifier = this.identifier.TrimEnd(new char[]
				{
					'*'
				});
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00024B96 File Offset: 0x00022D96
		public SearchObjectIdParameter()
		{
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00024BA8 File Offset: 0x00022DA8
		public SearchObjectIdParameter(string identity)
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
			this.Parse(identity);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00024C06 File Offset: 0x00022E06
		public SearchObjectIdParameter(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			((IIdentityParameter)this).Initialize(objectId);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00024C2A File Offset: 0x00022E2A
		public SearchObjectIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00024C44 File Offset: 0x00022E44
		internal virtual void Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			SearchObjectId searchObjectId = objectId as SearchObjectId;
			if (searchObjectId == null)
			{
				throw new ArgumentException("objectId");
			}
			this.objectIdentifier = searchObjectId;
			this.identifier = objectId.ToString();
			this.rawIdentity = objectId.ToString();
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00024C93 File Offset: 0x00022E93
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00024C9C File Offset: 0x00022E9C
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00024CB4 File Offset: 0x00022EB4
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			MailboxDataProvider mailboxDataProvider = (MailboxDataProvider)session;
			if (mailboxDataProvider == null)
			{
				throw new ArgumentNullException("session");
			}
			if (this.objectIdentifier == null)
			{
				QueryFilter queryFilter = new TextFilter(SearchObjectBaseSchema.Name, this.identifier, this.IsFullyQualified ? MatchOptions.FullString : MatchOptions.Prefix, MatchFlags.IgnoreCase);
				if (this.IsFullyQualified)
				{
					notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
				}
				else
				{
					notFoundReason = null;
				}
				if (optionalData != null && optionalData.AdditionalFilter != null)
				{
					queryFilter = QueryFilter.AndTogether(new QueryFilter[]
					{
						queryFilter,
						optionalData.AdditionalFilter
					});
				}
				return mailboxDataProvider.FindPaged<T>(queryFilter, rootId, false, null, 0);
			}
			notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
			SearchObjectId identity = this.objectIdentifier;
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				throw new NotSupportedException("Supplying Additional Filters and an ObjectIdentifier is not currently supported by this IdParameter.");
			}
			int num = this.identifier.IndexOf('\\');
			if (num == -1 || string.IsNullOrEmpty(this.identifier.Substring(0, num)))
			{
				SearchObjectBase searchObjectBase = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T)) as SearchObjectBase;
				if (searchObjectBase == null)
				{
					throw new ArgumentException("The generic type must be a SearchObjectBase");
				}
				identity = new SearchObjectId(identity, searchObjectBase.ObjectType);
			}
			T t = (T)((object)mailboxDataProvider.Read<T>(identity));
			if (t != null)
			{
				return new T[]
				{
					t
				};
			}
			return new T[0];
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00024E36 File Offset: 0x00023036
		public string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00024E3E File Offset: 0x0002303E
		public override string ToString()
		{
			return this.rawIdentity;
		}

		// Token: 0x040002A6 RID: 678
		private string identifier;

		// Token: 0x040002A7 RID: 679
		private string rawIdentity;

		// Token: 0x040002A8 RID: 680
		private SearchObjectId objectIdentifier;

		// Token: 0x040002A9 RID: 681
		private bool isFullyQualified = true;
	}
}
