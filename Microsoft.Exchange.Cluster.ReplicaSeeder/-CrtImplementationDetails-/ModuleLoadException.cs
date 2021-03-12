using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003450 File Offset: 0x00002850
		protected ModuleLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003438 File Offset: 0x00002838
		public ModuleLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003424 File Offset: 0x00002824
		public ModuleLoadException(string message) : base(message)
		{
		}

		// Token: 0x04000241 RID: 577
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
