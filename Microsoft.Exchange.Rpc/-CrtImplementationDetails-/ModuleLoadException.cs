using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000412 RID: 1042
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060011B7 RID: 4535 RVA: 0x0005ACA8 File Offset: 0x0005A0A8
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0005AC90 File Offset: 0x0005A090
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0005AC7C File Offset: 0x0005A07C
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x04001047 RID: 4167
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
