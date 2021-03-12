using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
	// Token: 0x02000336 RID: 822
	[ComVisible(true)]
	[Serializable]
	public class PolicyException : SystemException
	{
		// Token: 0x060029B0 RID: 10672 RVA: 0x00099DEF File Offset: 0x00097FEF
		public PolicyException() : base(Environment.GetResourceString("Policy_Default"))
		{
			base.HResult = -2146233322;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x00099E0C File Offset: 0x0009800C
		public PolicyException(string message) : base(message)
		{
			base.HResult = -2146233322;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x00099E20 File Offset: 0x00098020
		public PolicyException(string message, Exception exception) : base(message, exception)
		{
			base.HResult = -2146233322;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x00099E35 File Offset: 0x00098035
		protected PolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x00099E3F File Offset: 0x0009803F
		internal PolicyException(string message, int hresult) : base(message)
		{
			base.HResult = hresult;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x00099E4F File Offset: 0x0009804F
		internal PolicyException(string message, int hresult, Exception exception) : base(message, exception)
		{
			base.HResult = hresult;
		}
	}
}
