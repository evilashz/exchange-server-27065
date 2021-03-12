using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000021 RID: 33
	[Guid("6475565D-98F7-4362-8B1A-BB0AD3D3EB33")]
	[CoClass(typeof(MicrosoftClassificationEngine))]
	[ComImport]
	public interface IMicrosoftClassificationEngine : ICAClassificationEngine
	{
	}
}
