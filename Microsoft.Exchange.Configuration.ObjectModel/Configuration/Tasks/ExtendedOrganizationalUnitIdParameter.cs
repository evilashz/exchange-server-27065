using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	public sealed class ExtendedOrganizationalUnitIdParameter : OrganizationalUnitIdParameterBase
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002B3FF File Offset: 0x000295FF
		public ExtendedOrganizationalUnitIdParameter()
		{
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002B40E File Offset: 0x0002960E
		public ExtendedOrganizationalUnitIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002B41E File Offset: 0x0002961E
		public ExtendedOrganizationalUnitIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002B42E File Offset: 0x0002962E
		public ExtendedOrganizationalUnitIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0002B43E File Offset: 0x0002963E
		public ExtendedOrganizationalUnitIdParameter(ExtendedOrganizationalUnit organizationalUnit) : base(organizationalUnit.Id)
		{
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0002B453 File Offset: 0x00029653
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x0002B45B File Offset: 0x0002965B
		internal bool IncludeContainers
		{
			get
			{
				return this.includeContainers;
			}
			set
			{
				this.includeContainers = value;
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002B464 File Offset: 0x00029664
		public static ExtendedOrganizationalUnitIdParameter Parse(string identity)
		{
			return new ExtendedOrganizationalUnitIdParameter(identity);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0002B46C File Offset: 0x0002966C
		internal override IEnumerable<T> PerformSearch<T>(QueryFilter filter, ADObjectId rootId, IDirectorySession session, bool deepSearch)
		{
			TaskLogger.LogEnter();
			if (!typeof(ExtendedOrganizationalUnit).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			bool useGlobalCatalog = session.UseGlobalCatalog;
			bool useConfigNC = session.UseConfigNC;
			IEnumerable<T> result = null;
			try
			{
				session.UseGlobalCatalog = true;
				session.UseConfigNC = false;
				result = (deepSearch ? ((IEnumerable<T>)ExtendedOrganizationalUnit.FindSubTreeChildOrganizationalUnit(this.IncludeContainers, (IConfigurationSession)session, rootId, filter)) : ((IEnumerable<T>)ExtendedOrganizationalUnit.FindFirstLevelChildOrganizationalUnit(this.IncludeContainers, (IConfigurationSession)session, rootId, filter, null, 0)));
			}
			finally
			{
				session.UseGlobalCatalog = useGlobalCatalog;
				session.UseConfigNC = useConfigNC;
			}
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x0400031B RID: 795
		private bool includeContainers = true;
	}
}
