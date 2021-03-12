using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Mapi.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200014C RID: 332
	[Serializable]
	public class StoreMailboxIdParameter : MapiIdParameter
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x000251E3 File Offset: 0x000233E3
		public StoreMailboxIdParameter()
		{
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000251EB File Offset: 0x000233EB
		public StoreMailboxIdParameter(MailboxId mailboxId) : base(mailboxId)
		{
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000251F4 File Offset: 0x000233F4
		public StoreMailboxIdParameter(Guid mailboxGuid) : this(new MailboxId(null, mailboxGuid))
		{
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00025203 File Offset: 0x00023403
		public StoreMailboxIdParameter(MailboxEntry mailbox) : this(mailbox.Identity)
		{
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00025211 File Offset: 0x00023411
		public StoreMailboxIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002522C File Offset: 0x0002342C
		private StoreMailboxIdParameter(string identity)
		{
			this.rawIdentity = identity;
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			MailboxId mailboxId = null;
			try
			{
				mailboxId = new MailboxId(null, new Guid(identity));
			}
			catch (FormatException)
			{
			}
			catch (ArgumentOutOfRangeException)
			{
			}
			if (null == mailboxId && CultureInfo.InvariantCulture.CompareInfo.IsPrefix(identity, "/o=", CompareOptions.IgnoreCase))
			{
				mailboxId = new MailboxId(identity);
			}
			if (null != mailboxId)
			{
				this.Initialize(mailboxId);
			}
			this.displayName = identity;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x000252CC File Offset: 0x000234CC
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x000252D4 File Offset: 0x000234D4
		internal ulong Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000252DD File Offset: 0x000234DD
		internal override string RawIdentity
		{
			get
			{
				return this.rawIdentity ?? base.RawIdentity;
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000252EF File Offset: 0x000234EF
		public static StoreMailboxIdParameter Parse(string identity)
		{
			return new StoreMailboxIdParameter(identity);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000252F8 File Offset: 0x000234F8
		public override string ToString()
		{
			MailboxId mailboxId = base.MapiObjectId as MailboxId;
			if (null != mailboxId)
			{
				return mailboxId.ToString();
			}
			if (!string.IsNullOrEmpty(this.displayName))
			{
				return this.displayName;
			}
			return string.Empty;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002533C File Offset: 0x0002353C
		internal override void Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			MailboxId mailboxId = objectId as MailboxId;
			if (null == mailboxId)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterType("objectId", typeof(MailboxId).Name), "objectId");
			}
			if (!string.IsNullOrEmpty(this.displayName))
			{
				throw new InvalidOperationException(Strings.ErrorChangeImmutableType);
			}
			if (Guid.Empty == mailboxId.MailboxGuid && string.IsNullOrEmpty(mailboxId.MailboxExchangeLegacyDn))
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("objectId"), "objectId");
			}
			base.Initialize(objectId);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000253F0 File Offset: 0x000235F0
		internal override IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (!typeof(MailboxEntry).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (rootId == null)
			{
				throw new ArgumentNullException("rootId");
			}
			if (!(session is MapiAdministrationSession))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(MapiAdministrationSession).Name), "session");
			}
			if (string.IsNullOrEmpty(this.displayName) && null == base.MapiObjectId)
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			notFoundReason = null;
			List<T> list = new List<T>();
			Guid mailboxGuid = Guid.Empty;
			if (base.MapiObjectId != null)
			{
				mailboxGuid = ((MailboxId)base.MapiObjectId).MailboxGuid;
			}
			QueryFilter filter = new MailboxContextFilter(mailboxGuid, this.flags);
			try
			{
				IEnumerable<T> enumerable = session.FindPaged<T>(filter, rootId, true, null, 0);
				if (null != base.MapiObjectId)
				{
					bool flag = Guid.Empty != ((MailboxId)base.MapiObjectId).MailboxGuid;
					bool flag2 = !string.IsNullOrEmpty(((MailboxId)base.MapiObjectId).MailboxExchangeLegacyDn);
					if (flag || flag2)
					{
						foreach (T t in enumerable)
						{
							IConfigurable configurable = t;
							MailboxEntry mailboxEntry = (MailboxEntry)configurable;
							if (flag && mailboxEntry.Identity.MailboxGuid == ((MailboxId)base.MapiObjectId).MailboxGuid)
							{
								list.Add((T)((object)mailboxEntry));
							}
							else if (flag2 && string.Equals(mailboxEntry.Identity.MailboxExchangeLegacyDn, ((MailboxId)base.MapiObjectId).MailboxExchangeLegacyDn, StringComparison.OrdinalIgnoreCase))
							{
								list.Add((T)((object)mailboxEntry));
							}
						}
					}
				}
				if (list.Count == 0 && typeof(MailboxStatistics).IsAssignableFrom(typeof(T)) && !string.IsNullOrEmpty(this.displayName))
				{
					foreach (T t2 in enumerable)
					{
						IConfigurable configurable2 = t2;
						MailboxStatistics mailboxStatistics = (MailboxStatistics)configurable2;
						if (string.Equals(this.displayName, mailboxStatistics.DisplayName, StringComparison.OrdinalIgnoreCase))
						{
							list.Add((T)((object)mailboxStatistics));
						}
					}
				}
			}
			catch (MapiObjectNotFoundException)
			{
			}
			return list;
		}

		// Token: 0x040002AE RID: 686
		private string displayName;

		// Token: 0x040002AF RID: 687
		private string rawIdentity;

		// Token: 0x040002B0 RID: 688
		private ulong flags;
	}
}
