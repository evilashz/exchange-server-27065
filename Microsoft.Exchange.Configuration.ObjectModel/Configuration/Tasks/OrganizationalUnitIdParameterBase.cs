using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E2 RID: 226
	[Serializable]
	public abstract class OrganizationalUnitIdParameterBase : ADIdParameter
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x0001E077 File Offset: 0x0001C277
		public OrganizationalUnitIdParameterBase()
		{
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001E07F File Offset: 0x0001C27F
		public OrganizationalUnitIdParameterBase(string identity) : base(identity)
		{
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001E088 File Offset: 0x0001C288
		public OrganizationalUnitIdParameterBase(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001E091 File Offset: 0x0001C291
		public OrganizationalUnitIdParameterBase(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001E09A File Offset: 0x0001C29A
		public override string ToString()
		{
			if (this.canonicalName != null)
			{
				return this.canonicalName;
			}
			return base.ToString();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001E0B4 File Offset: 0x0001C2B4
		internal string GetCanonicalName(IDirectorySession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (this.canonicalName == null)
			{
				if (base.InternalADObjectId == null || string.IsNullOrEmpty(base.InternalADObjectId.DistinguishedName))
				{
					this.GetDistinguishedName(session);
				}
				this.canonicalName = NativeHelpers.CanonicalNameFromDistinguishedName(base.InternalADObjectId.DistinguishedName);
				this.canonicalName = this.canonicalName.TrimEnd(new char[]
				{
					'/'
				});
			}
			return this.canonicalName;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001E134 File Offset: 0x0001C334
		internal string GetDistinguishedName(IDirectorySession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (base.InternalADObjectId == null && base.RawIdentity == null)
			{
				throw new InvalidOperationException(Strings.ErrorUninitializedParameter);
			}
			if (base.InternalADObjectId == null || string.IsNullOrEmpty(base.InternalADObjectId.DistinguishedName))
			{
				ADObjectId internalADObjectId = base.InternalADObjectId;
				if (internalADObjectId != null || ADIdParameter.TryResolveCanonicalName(base.RawIdentity, out internalADObjectId))
				{
					ADRawEntry adrawEntry = session.ReadADRawEntry(internalADObjectId, new ADPropertyDefinition[]
					{
						ADObjectSchema.Id
					});
					if (adrawEntry != null)
					{
						base.UpdateInternalADObjectId(adrawEntry.Id);
					}
				}
				if (base.InternalADObjectId == null || string.IsNullOrEmpty(base.InternalADObjectId.DistinguishedName))
				{
					throw new NameConversionException(Strings.ErrorConversionFailed(base.RawIdentity));
				}
			}
			return base.InternalADObjectId.DistinguishedName;
		}

		// Token: 0x04000254 RID: 596
		protected string canonicalName;
	}
}
