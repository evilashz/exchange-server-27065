using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	public class MessageClassificationIdParameter : ADIdParameter
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x0002320C File Offset: 0x0002140C
		public MessageClassificationIdParameter()
		{
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00023214 File Offset: 0x00021414
		public MessageClassificationIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002321D File Offset: 0x0002141D
		public MessageClassificationIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00023226 File Offset: 0x00021426
		protected MessageClassificationIdParameter(string identity) : base(identity)
		{
			ADObjectId internalADObjectId = base.InternalADObjectId;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00023236 File Offset: 0x00021436
		public static MessageClassificationIdParameter Parse(string s)
		{
			return new MessageClassificationIdParameter(s);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00023240 File Offset: 0x00021440
		public override string ToString()
		{
			if (base.InternalADObjectId == null || base.InternalADObjectId.Parent == null)
			{
				return base.ToString();
			}
			string name = base.InternalADObjectId.Parent.Name;
			if (!name.Equals("Default", StringComparison.OrdinalIgnoreCase))
			{
				return name + "\\" + base.InternalADObjectId.Name;
			}
			return base.InternalADObjectId.Name;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000232AC File Offset: 0x000214AC
		internal static ADObjectId DefaultRoot(IConfigDataProvider session)
		{
			IConfigurationSession configurationSession = (IConfigurationSession)session;
			ADObjectId orgContainerId = configurationSession.GetOrgContainerId();
			return orgContainerId.GetDescendantId(MessageClassificationIdParameter.DefaultsRoot);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000232D4 File Offset: 0x000214D4
		internal override IEnumerable<T> GetObjectsInOrganization<T>(string identityString, ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData)
		{
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjectsInOrganization<T>(identityString, rootId, session, optionalData));
			if (wrapper.HasElements())
			{
				return wrapper;
			}
			int num = identityString.IndexOf('\\');
			if (0 < num && identityString.Length > num + 1)
			{
				string propertyValue = identityString.Substring(num + 1);
				string unescapedCommonName = identityString.Substring(0, num);
				OptionalIdentityData optionalIdentityData = null;
				if (optionalData != null)
				{
					optionalIdentityData = optionalData.Clone();
					optionalIdentityData.ConfigurationContainerRdn = null;
				}
				ADObjectId adobjectId = (rootId == null) ? MessageClassificationIdParameter.DefaultRoot((IConfigDataProvider)session) : rootId;
				ADObjectId childId = adobjectId.Parent.GetChildId(unescapedCommonName);
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, propertyValue);
				return base.PerformPrimarySearch<T>(filter, childId, session, false, optionalIdentityData);
			}
			return new T[0];
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00023384 File Offset: 0x00021584
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x0400028B RID: 651
		private const char LocaleIdentityDivider = '\\';

		// Token: 0x0400028C RID: 652
		private const string DefaultContainerName = "Default";

		// Token: 0x0400028D RID: 653
		public static ADObjectId DefaultsRoot = new ADObjectId("CN=Default,CN=Message Classifications,CN=Transport Settings");

		// Token: 0x0400028E RID: 654
		public static ADObjectId MessageClassificationRdn = new ADObjectId("CN=Message Classifications,CN=Transport Settings");
	}
}
