using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMPhoneSession
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	public class UMPhoneSessionIdentityParameter : IIdentityParameter
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x00025CC7 File Offset: 0x00023EC7
		public UMPhoneSessionIdentityParameter()
		{
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00025CCF File Offset: 0x00023ECF
		public UMPhoneSessionIdentityParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00025CDD File Offset: 0x00023EDD
		public UMPhoneSessionIdentityParameter(string identity)
		{
			this.identity = new UMPhoneSessionId(identity);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00025CF1 File Offset: 0x00023EF1
		public UMPhoneSessionIdentityParameter(UMPhoneSessionId identity)
		{
			this.identity = identity;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00025D00 File Offset: 0x00023F00
		public string RawIdentity
		{
			get
			{
				return this.identity.ToString();
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00025D0D File Offset: 0x00023F0D
		public static explicit operator string(UMPhoneSessionIdentityParameter identityParameter)
		{
			if (identityParameter != null)
			{
				return identityParameter.ToString();
			}
			return null;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00025D1A File Offset: 0x00023F1A
		public static UMPhoneSessionIdentityParameter Parse(string identity)
		{
			return new UMPhoneSessionIdentityParameter(identity);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00025D24 File Offset: 0x00023F24
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00025D3C File Offset: 0x00023F3C
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			T t = (T)((object)session.Read<T>(this.identity));
			if (t == null)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
				return new T[0];
			}
			notFoundReason = null;
			return new T[]
			{
				t
			};
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00025D99 File Offset: 0x00023F99
		public void Initialize(ObjectId objectId)
		{
			this.identity = (UMPhoneSessionId)objectId;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00025DA7 File Offset: 0x00023FA7
		public override string ToString()
		{
			return this.identity.ToString();
		}

		// Token: 0x04000565 RID: 1381
		private UMPhoneSessionId identity;
	}
}
