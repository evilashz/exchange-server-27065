using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000181 RID: 385
	[Serializable]
	public class UserIdParameter : RecipientIdParameter
	{
		// Token: 0x06000E01 RID: 3585 RVA: 0x0002A0DA File Offset: 0x000282DA
		public UserIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0002A0E3 File Offset: 0x000282E3
		public UserIdParameter()
		{
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0002A0EB File Offset: 0x000282EB
		public UserIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0002A0F4 File Offset: 0x000282F4
		public UserIdParameter(MailUser user) : base(user.Id)
		{
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0002A102 File Offset: 0x00028302
		public UserIdParameter(User user) : base(user.Id)
		{
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0002A110 File Offset: 0x00028310
		public UserIdParameter(Mailbox user) : base(user.Id)
		{
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0002A11E File Offset: 0x0002831E
		public UserIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0002A127 File Offset: 0x00028327
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return UserIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0002A12E File Offset: 0x0002832E
		public new static UserIdParameter Parse(string identity)
		{
			return new UserIdParameter(identity);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0002A138 File Offset: 0x00028338
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (wrapper.HasElements())
			{
				return wrapper;
			}
			SecurityPrincipalIdParameter securityPrincipalIdParameter = new SecurityPrincipalIdParameter(base.RawIdentity);
			OptionalIdentityData optionalIdentityData = (optionalData == null) ? new OptionalIdentityData() : optionalData.Clone();
			optionalIdentityData.AdditionalFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				optionalIdentityData.AdditionalFilter,
				RecipientIdParameter.GetRecipientTypeFilter(this.RecipientTypes)
			});
			return securityPrincipalIdParameter.GetObjects<T>(rootId, session, subTreeSession, optionalIdentityData, out notFoundReason);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0002A1B7 File Offset: 0x000283B7
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeUser(id);
		}

		// Token: 0x04000306 RID: 774
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.User,
			RecipientType.UserMailbox,
			RecipientType.MailUser
		};
	}
}
