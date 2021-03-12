using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00002CA8 File Offset: 0x000020A8
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002C94 File Offset: 0x00002094
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
