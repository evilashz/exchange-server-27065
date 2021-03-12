using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public sealed class EsentFileCloseException : EsentObsoleteException
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x000114AB File Offset: 0x0000F6AB
		public EsentFileCloseException() : base("Could not close file", JET_err.FileClose)
		{
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000114BA File Offset: 0x0000F6BA
		private EsentFileCloseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
