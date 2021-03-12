using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200022B RID: 555
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x0600011D RID: 285 RVA: 0x0000DC34 File Offset: 0x0000D034
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000DC1C File Offset: 0x0000D01C
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000DC08 File Offset: 0x0000D008
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x04000368 RID: 872
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
