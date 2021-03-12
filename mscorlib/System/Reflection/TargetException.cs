using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005F4 RID: 1524
	[ComVisible(true)]
	[Serializable]
	public class TargetException : ApplicationException
	{
		// Token: 0x06004793 RID: 18323 RVA: 0x00102ED7 File Offset: 0x001010D7
		public TargetException()
		{
			base.SetErrorCode(-2146232829);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x00102EEA File Offset: 0x001010EA
		public TargetException(string message) : base(message)
		{
			base.SetErrorCode(-2146232829);
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x00102EFE File Offset: 0x001010FE
		public TargetException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146232829);
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x00102F13 File Offset: 0x00101113
		protected TargetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
