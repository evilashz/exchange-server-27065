using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E7 RID: 1767
	[DataContract]
	public class CompareStringValidatorInfo : CompareValidatorInfo
	{
		// Token: 0x06004A6A RID: 19050 RVA: 0x000E3834 File Offset: 0x000E1A34
		public CompareStringValidatorInfo(string controlToCompare) : base("CompareStringValidator", controlToCompare)
		{
		}
	}
}
