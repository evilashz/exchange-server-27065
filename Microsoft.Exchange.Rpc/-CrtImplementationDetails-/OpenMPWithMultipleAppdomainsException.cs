using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000415 RID: 1045
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x060011C4 RID: 4548 RVA: 0x0005B12C File Offset: 0x0005A52C
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0005B118 File Offset: 0x0005A518
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
