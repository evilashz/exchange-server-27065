using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E8 RID: 1768
	[DataContract]
	[KnownType(typeof(CollectionItemValidatorInfo))]
	public class CollectionValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A6B RID: 19051 RVA: 0x000E3842 File Offset: 0x000E1A42
		public CollectionValidatorInfo(string typeName) : base(typeName)
		{
		}
	}
}
