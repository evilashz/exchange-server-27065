using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x02000111 RID: 273
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MissingMethodException : MissingMemberException, ISerializable
	{
		// Token: 0x06001067 RID: 4199 RVA: 0x00031334 File Offset: 0x0002F534
		[__DynamicallyInvokable]
		public MissingMethodException() : base(Environment.GetResourceString("Arg_MissingMethodException"))
		{
			base.SetErrorCode(-2146233069);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00031351 File Offset: 0x0002F551
		[__DynamicallyInvokable]
		public MissingMethodException(string message) : base(message)
		{
			base.SetErrorCode(-2146233069);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00031365 File Offset: 0x0002F565
		[__DynamicallyInvokable]
		public MissingMethodException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233069);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003137A File Offset: 0x0002F57A
		protected MissingMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00031384 File Offset: 0x0002F584
		[__DynamicallyInvokable]
		public override string Message
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return Environment.GetResourceString("MissingMethod_Name", new object[]
				{
					this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : "")
				});
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000313ED File Offset: 0x0002F5ED
		private MissingMethodException(string className, string methodName, byte[] signature)
		{
			this.ClassName = className;
			this.MemberName = methodName;
			this.Signature = signature;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003140A File Offset: 0x0002F60A
		public MissingMethodException(string className, string methodName)
		{
			this.ClassName = className;
			this.MemberName = methodName;
		}
	}
}
