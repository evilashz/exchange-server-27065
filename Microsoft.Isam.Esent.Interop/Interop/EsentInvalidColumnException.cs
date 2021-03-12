using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000223 RID: 547
	[Serializable]
	public class EsentInvalidColumnException : EsentException
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x00014FAB File Offset: 0x000131AB
		public EsentInvalidColumnException()
		{
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00014FB3 File Offset: 0x000131B3
		protected EsentInvalidColumnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00014FBD File Offset: 0x000131BD
		public override string Message
		{
			get
			{
				return "Column is not valid for this operation";
			}
		}
	}
}
