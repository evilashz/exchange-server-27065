using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200012C RID: 300
	internal class FxSession : ISession
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x00014F30 File Offset: 0x00013130
		public FxSession(IReadOnlyDictionary<PropertyTag, NamedProperty> namedPropertiesMapper)
		{
			this.propertyTagToNamedProperties = namedPropertiesMapper;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00014F3F File Offset: 0x0001313F
		bool ISession.TryResolveToNamedProperty(PropertyTag propertyTag, out NamedProperty namedProperty)
		{
			namedProperty = null;
			return propertyTag.IsNamedProperty && this.propertyTagToNamedProperties.TryGetValue(propertyTag, out namedProperty);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00014F5C File Offset: 0x0001315C
		bool ISession.TryResolveFromNamedProperty(NamedProperty namedProperty, ref PropertyTag propertyTag)
		{
			return false;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00014F60 File Offset: 0x00013160
		[Conditional("Debug")]
		private void ValidateUniquePropertyIDs(IReadOnlyDictionary<PropertyTag, NamedProperty> namedPropertiesMapper)
		{
			new HashSet<int>();
			foreach (PropertyTag propertyTag in namedPropertiesMapper.Keys)
			{
			}
		}

		// Token: 0x040005E7 RID: 1511
		private readonly IReadOnlyDictionary<PropertyTag, NamedProperty> propertyTagToNamedProperties;
	}
}
