using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public abstract class EsentFragmentationException : EsentDataException
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00011429 File Offset: 0x0000F629
		protected EsentFragmentationException(string description, JET_err err) : base(description, err)
		{
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00011433 File Offset: 0x0000F633
		protected EsentFragmentationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
