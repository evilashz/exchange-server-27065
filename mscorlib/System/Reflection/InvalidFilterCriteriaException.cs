using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005C2 RID: 1474
	[ComVisible(true)]
	[Serializable]
	public class InvalidFilterCriteriaException : ApplicationException
	{
		// Token: 0x06004541 RID: 17729 RVA: 0x000FDC4D File Offset: 0x000FBE4D
		public InvalidFilterCriteriaException() : base(Environment.GetResourceString("Arg_InvalidFilterCriteriaException"))
		{
			base.SetErrorCode(-2146232831);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x000FDC6A File Offset: 0x000FBE6A
		public InvalidFilterCriteriaException(string message) : base(message)
		{
			base.SetErrorCode(-2146232831);
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x000FDC7E File Offset: 0x000FBE7E
		public InvalidFilterCriteriaException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146232831);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000FDC93 File Offset: 0x000FBE93
		protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
