using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200007D RID: 125
	[DataContract]
	[KnownType(typeof(MailboxIdentity))]
	[KnownType(typeof(MailboxFolderPermissionIdentity))]
	public class Identity : INamedIdentity
	{
		// Token: 0x06001B49 RID: 6985 RVA: 0x00056CB0 File Offset: 0x00054EB0
		public Identity(ADObjectId identity, string displayName) : this((identity.ObjectGuid == Guid.Empty) ? identity.DistinguishedName : identity.ObjectGuid.ToString(), displayName)
		{
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00056CF4 File Offset: 0x00054EF4
		public Identity(string rawIdentity, string displayName)
		{
			this.RawIdentity = rawIdentity;
			this.DisplayName = displayName;
			this.OnDeserialized(default(StreamingContext));
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00056D24 File Offset: 0x00054F24
		public Identity(string identity) : this(identity, identity)
		{
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00056D2E File Offset: 0x00054F2E
		public Identity(ADObjectId identity) : this(identity, identity.Name)
		{
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00056D3D File Offset: 0x00054F3D
		public Identity(DagNetworkObjectId id) : this(id.FullName, id.NetName)
		{
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00056D54 File Offset: 0x00054F54
		public Identity(AppId id) : this(id.MailboxOwnerId.ObjectGuid.ToString() + '\\' + id.AppIdValue, id.DisplayName)
		{
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00056D98 File Offset: 0x00054F98
		public Identity(MigrationBatchId id) : this(id.Id, id.ToString())
		{
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00056DAC File Offset: 0x00054FAC
		public Identity(MigrationUserId id) : this((id.JobItemGuid != Guid.Empty) ? id.JobItemGuid.ToString() : id.Id, id.ToString())
		{
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00056DF3 File Offset: 0x00054FF3
		public Identity(MigrationEndpointId id) : this(id.Id, id.ToString())
		{
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00056E07 File Offset: 0x00055007
		public Identity(MigrationReportId id) : this(id.ToString(), id.ToString())
		{
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00056E1B File Offset: 0x0005501B
		public Identity(MigrationStatisticsId id) : this(id.ToString(), id.ToString())
		{
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x00056E2F File Offset: 0x0005502F
		// (set) Token: 0x06001B55 RID: 6997 RVA: 0x00056E37 File Offset: 0x00055037
		[DataMember]
		public string DisplayName { get; private set; }

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x00056E40 File Offset: 0x00055040
		// (set) Token: 0x06001B57 RID: 6999 RVA: 0x00056E48 File Offset: 0x00055048
		[DataMember(IsRequired = true)]
		public string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
			private set
			{
				value.FaultIfNullOrEmpty("RawIdentity cannot be null.");
				this.rawIdentity = value;
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x00056E5C File Offset: 0x0005505C
		string INamedIdentity.Identity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x00056E64 File Offset: 0x00055064
		// (set) Token: 0x06001B5A RID: 7002 RVA: 0x00056E6C File Offset: 0x0005506C
		private ADObjectId InternalIdentity { get; set; }

		// Token: 0x06001B5B RID: 7003 RVA: 0x00056E78 File Offset: 0x00055078
		public static Identity FromExecutingUserId()
		{
			Identity identity = (EacRbacPrincipal.Instance.ExecutingUserId == null) ? new Identity(EacRbacPrincipal.Instance.Name, null) : new Identity(EacRbacPrincipal.Instance.ExecutingUserId, null);
			identity.InternalIdentity = EacRbacPrincipal.Instance.ExecutingUserId;
			return identity;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00056EC8 File Offset: 0x000550C8
		public static bool operator ==(Identity identity1, Identity identity2)
		{
			return (identity1 == null && identity2 == null) || (identity1 != null && identity2 != null && (string.Compare(identity1.RawIdentity, identity2.RawIdentity, StringComparison.OrdinalIgnoreCase) == 0 || (string.Compare(identity1.DisplayName, identity2.DisplayName, StringComparison.OrdinalIgnoreCase) == 0 && EacHttpContext.Instance.PostHydrationActionPresent)));
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00056F22 File Offset: 0x00055122
		public static bool operator !=(Identity identity1, Identity identity2)
		{
			return !(identity1 == identity2);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00056F30 File Offset: 0x00055130
		public object[] ToPipelineInput()
		{
			object obj = this.InternalIdentity ?? this;
			return new object[]
			{
				obj
			};
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00056F55 File Offset: 0x00055155
		internal static Identity FromIdParameter(object value)
		{
			if (value is string)
			{
				return new Identity((string)value, (string)value);
			}
			return null;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00056F72 File Offset: 0x00055172
		public static PeopleIdentity[] ConvertToPeopleIdentity(object value)
		{
			if (value is RecipientIdParameter[])
			{
				return ((RecipientIdParameter[])value).ToPeopleIdentityArray();
			}
			return null;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00056F91 File Offset: 0x00055191
		internal static Identity[] FromIdParameters(object value)
		{
			if (value is string[])
			{
				return (from v in (string[])value
				select Identity.FromIdParameter(v)).ToArray<Identity>();
			}
			return null;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x00056FCA File Offset: 0x000551CA
		internal static Identity ParseIdentity(string idStr)
		{
			if (string.IsNullOrEmpty(idStr))
			{
				return null;
			}
			return new Identity(idStr, idStr);
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00056FDD File Offset: 0x000551DD
		public bool Equals(Identity identity)
		{
			return this == identity;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00056FE6 File Offset: 0x000551E6
		public override bool Equals(object obj)
		{
			return this.Equals((Identity)obj);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00056FF4 File Offset: 0x000551F4
		public override int GetHashCode()
		{
			return this.RawIdentity.GetHashCode();
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00057001 File Offset: 0x00055201
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (string.IsNullOrEmpty(this.DisplayName))
			{
				this.DisplayName = this.RawIdentity;
			}
		}

		// Token: 0x04001B56 RID: 6998
		private static readonly Regex guidRegex = new Regex("^[0-9a-fA-F]{8}(\\-[0-9a-fA-F]{4}){3}\\-[0-9a-fA-F]{12}$", RegexOptions.CultureInvariant);

		// Token: 0x04001B57 RID: 6999
		private string rawIdentity;
	}
}
