using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000200 RID: 512
	[Serializable]
	public sealed class EsentFileInvalidTypeException : EsentInconsistentException
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x00013892 File Offset: 0x00011A92
		public EsentFileInvalidTypeException() : base("Invalid file type", JET_err.FileInvalidType)
		{
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000138A4 File Offset: 0x00011AA4
		private EsentFileInvalidTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
