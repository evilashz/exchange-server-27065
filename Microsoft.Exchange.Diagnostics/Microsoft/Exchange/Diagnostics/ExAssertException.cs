using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	internal class ExAssertException : BaseException
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000429B File Offset: 0x0000249B
		public ExAssertException(string assertText) : base(assertText)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000042A4 File Offset: 0x000024A4
		private ExAssertException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
