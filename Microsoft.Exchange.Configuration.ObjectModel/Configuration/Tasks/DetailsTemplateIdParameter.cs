using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000104 RID: 260
	[Serializable]
	public class DetailsTemplateIdParameter : ADIdParameter
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x000207F8 File Offset: 0x0001E9F8
		public DetailsTemplateIdParameter(string rawString) : base(rawString)
		{
			if (base.InternalADObjectId != null && base.InternalADObjectId.Rdn != null)
			{
				return;
			}
			string[] array = rawString.Split(new char[]
			{
				'\\'
			});
			if (array.Length > 2)
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(rawString), "Identity");
			}
			if (!array[0].Equals("*") && !Culture.TryGetCulture(array[0], out this.language))
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(rawString), "Identity");
			}
			if (array.Length == 2)
			{
				this.type = DetailsTemplate.TranslateTemplateNameToID(array[1]);
				if (this.type == null && !array[1].Equals("*"))
				{
					throw new ArgumentException(Strings.ErrorInvalidIdentity(rawString), "Identity");
				}
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000208CE File Offset: 0x0001EACE
		public DetailsTemplateIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000208D7 File Offset: 0x0001EAD7
		public DetailsTemplateIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000208E0 File Offset: 0x0001EAE0
		public DetailsTemplateIdParameter()
		{
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000208E8 File Offset: 0x0001EAE8
		public static DetailsTemplateIdParameter Parse(string rawString)
		{
			return new DetailsTemplateIdParameter(rawString);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000208F0 File Offset: 0x0001EAF0
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			TaskLogger.LogEnter();
			IEnumerable<T> enumerable = null;
			notFoundReason = null;
			if (typeof(T) != typeof(DetailsTemplate))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			try
			{
				if (base.InternalADObjectId != null)
				{
					return base.GetADObjectIdObjects<T>(base.InternalADObjectId, rootId, subTreeSession, optionalData);
				}
				ADObjectId childId = ((IConfigurationSession)session).GetOrgContainerId().GetChildId("Addressing").GetChildId("Display-Templates");
				QueryFilter filter = null;
				if (this.language != null)
				{
					childId = childId.GetChildId(this.language.LCID.ToString("X"));
				}
				if (this.type != null)
				{
					filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, this.type);
				}
				enumerable = base.PerformPrimarySearch<T>(filter, childId, session, true, optionalData);
				EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(enumerable);
				if (wrapper.HasElements())
				{
					return wrapper;
				}
				enumerable = base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return enumerable;
		}

		// Token: 0x04000271 RID: 625
		private Culture language;

		// Token: 0x04000272 RID: 626
		private string type;
	}
}
