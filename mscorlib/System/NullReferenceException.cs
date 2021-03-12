using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000117 RID: 279
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class NullReferenceException : SystemException
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x00031651 File Offset: 0x0002F851
		[__DynamicallyInvokable]
		public NullReferenceException() : base(Environment.GetResourceString("Arg_NullReferenceException"))
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0003166E File Offset: 0x0002F86E
		[__DynamicallyInvokable]
		public NullReferenceException(string message) : base(message)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00031682 File Offset: 0x0002F882
		[__DynamicallyInvokable]
		public NullReferenceException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00031697 File Offset: 0x0002F897
		protected NullReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
