using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000836 RID: 2102
	internal class MCMDictionary : MessageDictionary
	{
		// Token: 0x060059E8 RID: 23016 RVA: 0x0013A94F File Offset: 0x00138B4F
		public MCMDictionary(IMethodCallMessage msg, IDictionary idict) : base(MCMDictionary.MCMkeys, idict)
		{
			this._mcmsg = msg;
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x0013A964 File Offset: 0x00138B64
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._mcmsg.Uri;
			case 1:
				return this._mcmsg.MethodName;
			case 2:
				return this._mcmsg.MethodSignature;
			case 3:
				return this._mcmsg.TypeName;
			case 4:
				return this._mcmsg.Args;
			case 5:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x0013A9E4 File Offset: 0x00138BE4
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			Message message = this._mcmsg as Message;
			if (message != null)
			{
				return message.GetLogicalCallContext();
			}
			MethodCall methodCall = this._mcmsg as MethodCall;
			if (methodCall != null)
			{
				return methodCall.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x0013AA2C File Offset: 0x00138C2C
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			Message message = this._mcmsg as Message;
			MethodCall methodCall = this._mcmsg as MethodCall;
			if (keyNum != 0)
			{
				if (keyNum != 1)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
				}
				if (message != null)
				{
					message.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
			else
			{
				if (message != null)
				{
					message.Uri = (string)value;
					return;
				}
				if (methodCall != null)
				{
					methodCall.Uri = (string)value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
		}

		// Token: 0x04002883 RID: 10371
		public static string[] MCMkeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__Args",
			"__CallContext"
		};

		// Token: 0x04002884 RID: 10372
		internal IMethodCallMessage _mcmsg;
	}
}
