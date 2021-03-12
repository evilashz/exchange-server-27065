using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200022E RID: 558
	[Serializable]
	internal class OpenMPWithMultipleAppdomainsException : Exception
	{
		// Token: 0x0600012A RID: 298 RVA: 0x0000E0B8 File Offset: 0x0000D4B8
		protected OpenMPWithMultipleAppdomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000E0A4 File Offset: 0x0000D4A4
		public OpenMPWithMultipleAppdomainsException()
		{
		}
	}
}
