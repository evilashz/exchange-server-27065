using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000211 RID: 529
	[Serializable]
	public sealed class EsentSpaceHintsInvalidException : EsentUsageException
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x00013A6E File Offset: 0x00011C6E
		public EsentSpaceHintsInvalidException() : base("An element of the JET space hints structure was not correct or actionable.", JET_err.SpaceHintsInvalid)
		{
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00013A80 File Offset: 0x00011C80
		private EsentSpaceHintsInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
