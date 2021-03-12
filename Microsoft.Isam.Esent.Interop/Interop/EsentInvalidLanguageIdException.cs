using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	public sealed class EsentInvalidLanguageIdException : EsentUsageException
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x00012616 File Offset: 0x00010816
		public EsentInvalidLanguageIdException() : base("Invalid or unknown language id", JET_err.InvalidLanguageId)
		{
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00012628 File Offset: 0x00010828
		private EsentInvalidLanguageIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
