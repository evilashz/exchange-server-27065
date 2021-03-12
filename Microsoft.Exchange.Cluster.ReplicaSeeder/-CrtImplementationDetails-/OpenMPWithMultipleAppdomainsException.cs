using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x06000098 RID: 152 RVA: 0x000038D4 File Offset: 0x00002CD4
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000038C0 File Offset: 0x00002CC0
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
