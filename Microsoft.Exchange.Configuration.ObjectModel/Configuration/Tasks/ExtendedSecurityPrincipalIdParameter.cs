using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200019D RID: 413
	[Serializable]
	public sealed class ExtendedSecurityPrincipalIdParameter : ADIdParameter
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002B54C File Offset: 0x0002974C
		public ExtendedSecurityPrincipalIdParameter()
		{
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002B554 File Offset: 0x00029754
		public ExtendedSecurityPrincipalIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002B55D File Offset: 0x0002975D
		public ExtendedSecurityPrincipalIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002B566 File Offset: 0x00029766
		public ExtendedSecurityPrincipalIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0002B56F File Offset: 0x0002976F
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x0002B577 File Offset: 0x00029777
		public ADDomain IncludeDomainLocalFrom { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0002B580 File Offset: 0x00029780
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x0002B588 File Offset: 0x00029788
		public MultiValuedProperty<SecurityPrincipalType> Types { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0002B594 File Offset: 0x00029794
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					this.securityTargetFilter
				});
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002B5C0 File Offset: 0x000297C0
		public static ExtendedSecurityPrincipalIdParameter Parse(string identity)
		{
			return new ExtendedSecurityPrincipalIdParameter(identity);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002B5C8 File Offset: 0x000297C8
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			TaskLogger.LogEnter();
			if (!typeof(T).IsAssignableFrom(typeof(ExtendedSecurityPrincipal)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			IEnumerable<T> result = ExtendedSecurityPrincipalSearchHelper.PerformSearch(new ExtendedSecurityPrincipalSearcher(this.FindObjects), (IConfigDataProvider)session, rootId, (this.IncludeDomainLocalFrom != null) ? this.IncludeDomainLocalFrom.Id : null, this.Types).Cast<T>();
			TaskLogger.LogExit();
			notFoundReason = null;
			return result;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002B670 File Offset: 0x00029870
		private IEnumerable<ExtendedSecurityPrincipal> FindObjects(IConfigDataProvider session, ADObjectId rootId, QueryFilter targetFilter)
		{
			this.securityTargetFilter = targetFilter;
			LocalizedString? localizedString;
			return base.GetObjects<ExtendedSecurityPrincipal>(rootId, (IDirectorySession)session, (IDirectorySession)session, null, out localizedString);
		}

		// Token: 0x0400031C RID: 796
		private QueryFilter securityTargetFilter;
	}
}
