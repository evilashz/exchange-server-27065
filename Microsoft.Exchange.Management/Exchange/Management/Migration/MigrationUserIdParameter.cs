using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004E5 RID: 1253
	[Serializable]
	public class MigrationUserIdParameter : IIdentityParameter
	{
		// Token: 0x06002BC0 RID: 11200 RVA: 0x000AEB65 File Offset: 0x000ACD65
		public MigrationUserIdParameter() : this(new MigrationUserId(string.Empty, Guid.Empty))
		{
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000AEB7C File Offset: 0x000ACD7C
		public MigrationUserIdParameter(INamedIdentity namedIdentity)
		{
			if (namedIdentity == null)
			{
				throw new ArgumentNullException("namedIdentity");
			}
			this.MigrationUserId = MigrationUserIdParameter.ParseMigrationUserId(namedIdentity.Identity);
			this.RawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000AEBAF File Offset: 0x000ACDAF
		public MigrationUserIdParameter(MigrationUserId identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.MigrationUserId = identity;
			this.RawIdentity = this.MigrationUserId.ToString();
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000AEBDD File Offset: 0x000ACDDD
		public MigrationUserIdParameter(MigrationUser user) : this(user.Identity)
		{
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000AEBEC File Offset: 0x000ACDEC
		public MigrationUserIdParameter(SmtpAddress identity)
		{
			if (identity == SmtpAddress.Empty)
			{
				throw new ArgumentNullException("identity");
			}
			this.MigrationUserId = new MigrationUserId(identity.ToString(), Guid.Empty);
			this.RawIdentity = identity.ToString();
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000AEC47 File Offset: 0x000ACE47
		public MigrationUserIdParameter(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			this.MigrationUserId = MigrationUserIdParameter.ParseMigrationUserId(identity);
			this.RawIdentity = identity;
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000AEC78 File Offset: 0x000ACE78
		public MigrationUserIdParameter(Guid identity)
		{
			if (identity == Guid.Empty)
			{
				throw new ArgumentNullException("identity");
			}
			this.MigrationUserId = new MigrationUserId(string.Empty, identity);
			this.RawIdentity = identity.ToString();
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x000AECC7 File Offset: 0x000ACEC7
		// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x000AECCF File Offset: 0x000ACECF
		public MigrationUserId MigrationUserId { get; private set; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000AECD8 File Offset: 0x000ACED8
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x000AECE0 File Offset: 0x000ACEE0
		public string RawIdentity { get; private set; }

		// Token: 0x06002BCB RID: 11211 RVA: 0x000AECEC File Offset: 0x000ACEEC
		public static MigrationUserId ParseMigrationUserId(string identity)
		{
			Guid jobItemGuid;
			if (GuidHelper.TryParseGuid(identity, out jobItemGuid))
			{
				return new MigrationUserId(string.Empty, jobItemGuid);
			}
			return new MigrationUserId(identity, Guid.Empty);
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000AEEB8 File Offset: 0x000AD0B8
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			IConfigurable[] array = session.Find<T>(null, this.MigrationUserId, false, null);
			for (int i = 0; i < array.Length; i++)
			{
				T instance = (T)((object)array[i]);
				yield return instance;
			}
			yield break;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000AEEDC File Offset: 0x000AD0DC
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = new LocalizedString?(Strings.MigrationUserNotFound(this.RawIdentity));
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000AEF00 File Offset: 0x000AD100
		public void Initialize(ObjectId objectId)
		{
			MigrationUserId migrationUserId = objectId as MigrationUserId;
			if (migrationUserId == null)
			{
				throw new ArgumentException("objectId");
			}
			this.MigrationUserId = migrationUserId;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000AEF29 File Offset: 0x000AD129
		public override string ToString()
		{
			return this.RawIdentity;
		}
	}
}
