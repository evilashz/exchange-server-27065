using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class AddressRewriteEntryIdParameter : ADIdParameter
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002D71 File Offset: 0x00000F71
		public AddressRewriteEntryIdParameter()
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002D79 File Offset: 0x00000F79
		public AddressRewriteEntryIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002D82 File Offset: 0x00000F82
		public AddressRewriteEntryIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002D8B File Offset: 0x00000F8B
		public AddressRewriteEntryIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002D94 File Offset: 0x00000F94
		public ADObjectId AdIdentity
		{
			get
			{
				return base.InternalADObjectId;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002D9C File Offset: 0x00000F9C
		public string Name
		{
			get
			{
				return base.RawIdentity;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public static explicit operator string(AddressRewriteEntryIdParameter addressRewriteEntryIdParameter)
		{
			return addressRewriteEntryIdParameter.ToString();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002DAC File Offset: 0x00000FAC
		public static AddressRewriteEntryIdParameter Parse(string identity)
		{
			return new AddressRewriteEntryIdParameter(identity);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002DB4 File Offset: 0x00000FB4
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			TaskLogger.LogEnter();
			IEnumerable<T> result = null;
			try
			{
				if (typeof(T) != typeof(AddressRewriteEntry))
				{
					throw new InvalidOperationException();
				}
				if (session == null)
				{
					throw new ArgumentException(Strings.AddressRewriteSessionNull, "Session");
				}
				if (string.IsNullOrEmpty(base.RawIdentity) && base.InternalADObjectId == null)
				{
					throw new ArgumentException(Strings.AddressRewriteInvalidIdentity, "Identity");
				}
				notFoundReason = null;
				if (base.InternalADObjectId == null)
				{
					IConfigurationSession configurationSession = (IConfigurationSession)session;
					QueryFilter filter = new TextFilter(ADObjectSchema.Name, this.Name, MatchOptions.FullString, MatchFlags.IgnoreCase);
					result = base.PerformPrimarySearch<T>(filter, null, session, true, optionalData);
				}
				else
				{
					result = base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002E8C File Offset: 0x0000108C
		public override string ToString()
		{
			if (this.AdIdentity != null)
			{
				return this.AdIdentity.ObjectGuid.ToString();
			}
			return this.Name;
		}
	}
}
