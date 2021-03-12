using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00002824 File Offset: 0x00001C24
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000280C File Offset: 0x00001C0C
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000027F8 File Offset: 0x00001BF8
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x04000196 RID: 406
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
