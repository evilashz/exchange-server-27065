using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200013C RID: 316
	[Serializable]
	public sealed class EsentInvalidPathException : EsentUsageException
	{
		// Token: 0x0600088B RID: 2187 RVA: 0x00012322 File Offset: 0x00010522
		public EsentInvalidPathException() : base("Invalid file path", JET_err.InvalidPath)
		{
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00012334 File Offset: 0x00010534
		private EsentInvalidPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
