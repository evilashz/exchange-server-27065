using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200014C RID: 332
	[CollectionDataContract(Name = "Rules", ItemName = "Rule")]
	public class ActivationRuleCollection : Collection<ActivationRule>
	{
	}
}
