using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public class ExtendedRightIdParameter : ADIdParameter
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x000211EA File Offset: 0x0001F3EA
		public ExtendedRightIdParameter()
		{
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000211F2 File Offset: 0x0001F3F2
		public ExtendedRightIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000211FB File Offset: 0x0001F3FB
		public ExtendedRightIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00021204 File Offset: 0x0001F404
		protected ExtendedRightIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002120D File Offset: 0x0001F40D
		public static ExtendedRight[] AllExtendedRights
		{
			get
			{
				return ExtendedRightIdParameter.allExtendedRights;
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00021214 File Offset: 0x0001F414
		public static ExtendedRightIdParameter Parse(string identity)
		{
			return new ExtendedRightIdParameter(identity);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002121C File Offset: 0x0001F41C
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ExtendedRight))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (ExtendedRightIdParameter.AllExtendedRights == null)
			{
				ADPagedReader<ExtendedRight> adpagedReader = ((ITopologyConfigurationSession)session).GetAllExtendedRights();
				ExtendedRightIdParameter.allExtendedRights = adpagedReader.ReadAllPages();
			}
			base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			List<T> list = new List<T>();
			foreach (ExtendedRight extendedRight in ExtendedRightIdParameter.AllExtendedRights)
			{
				if (ADObjectId.Equals(extendedRight.Id, base.InternalADObjectId) || (base.InternalADObjectId != null && base.InternalADObjectId.ObjectGuid == extendedRight.RightsGuid) || (base.RawIdentity != null && (string.Compare(extendedRight.DisplayName, base.RawIdentity, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extendedRight.Name, base.RawIdentity, StringComparison.OrdinalIgnoreCase) == 0)))
				{
					list.Add((T)((object)extendedRight));
					break;
				}
			}
			return list;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002133A File Offset: 0x0001F53A
		internal override IEnumerable<T> PerformSearch<T>(QueryFilter filter, ADObjectId rootId, IDirectorySession session, bool deepSearch)
		{
			return new T[0];
		}

		// Token: 0x04000278 RID: 632
		private static ExtendedRight[] allExtendedRights;
	}
}
