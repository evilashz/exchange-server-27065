using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200002A RID: 42
	[Guid("67F2E67D-1231-4F95-A1ED-D31364A5862C")]
	[CoClass(typeof(MicrosoftFingerprintCreator))]
	[ComImport]
	public interface IMicrosoftFingerprintCreator : IFingerprintCreator
	{
	}
}
