using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	public sealed class EsentInvalidNameException : EsentUsageException
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x00012162 File Offset: 0x00010362
		public EsentInvalidNameException() : base("Invalid name", JET_err.InvalidName)
		{
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00012174 File Offset: 0x00010374
		private EsentInvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
