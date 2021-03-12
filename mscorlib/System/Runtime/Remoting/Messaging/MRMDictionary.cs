using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000837 RID: 2103
	internal class MRMDictionary : MessageDictionary
	{
		// Token: 0x060059ED RID: 23021 RVA: 0x0013AAF7 File Offset: 0x00138CF7
		[SecurityCritical]
		public MRMDictionary(IMethodReturnMessage msg, IDictionary idict) : base((msg.Exception != null) ? MRMDictionary.MCMkeysFault : MRMDictionary.MCMkeysNoFault, idict)
		{
			this.fault = (msg.Exception != null);
			this._mrmsg = msg;
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x0013AB2C File Offset: 0x00138D2C
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				if (this.fault)
				{
					return this.FetchLogicalCallContext();
				}
				return this._mrmsg.Uri;
			case 1:
				return this._mrmsg.MethodName;
			case 2:
				return this._mrmsg.MethodSignature;
			case 3:
				return this._mrmsg.TypeName;
			case 4:
				if (this.fault)
				{
					return this._mrmsg.Exception;
				}
				return this._mrmsg.ReturnValue;
			case 5:
				return this._mrmsg.Args;
			case 6:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x060059EF RID: 23023 RVA: 0x0013ABE0 File Offset: 0x00138DE0
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			ReturnMessage returnMessage = this._mrmsg as ReturnMessage;
			if (returnMessage != null)
			{
				return returnMessage.GetLogicalCallContext();
			}
			MethodResponse methodResponse = this._mrmsg as MethodResponse;
			if (methodResponse != null)
			{
				return methodResponse.GetLogicalCallContext();
			}
			StackBasedReturnMessage stackBasedReturnMessage = this._mrmsg as StackBasedReturnMessage;
			if (stackBasedReturnMessage != null)
			{
				return stackBasedReturnMessage.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x060059F0 RID: 23024 RVA: 0x0013AC40 File Offset: 0x00138E40
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			ReturnMessage returnMessage = this._mrmsg as ReturnMessage;
			MethodResponse methodResponse = this._mrmsg as MethodResponse;
			if (keyNum != 0)
			{
				if (keyNum != 1)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
				}
				if (returnMessage != null)
				{
					returnMessage.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				if (methodResponse != null)
				{
					methodResponse.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
			else
			{
				if (returnMessage != null)
				{
					returnMessage.Uri = (string)value;
					return;
				}
				if (methodResponse != null)
				{
					methodResponse.Uri = (string)value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
		}

		// Token: 0x04002885 RID: 10373
		public static string[] MCMkeysFault = new string[]
		{
			"__CallContext"
		};

		// Token: 0x04002886 RID: 10374
		public static string[] MCMkeysNoFault = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__Return",
			"__OutArgs",
			"__CallContext"
		};

		// Token: 0x04002887 RID: 10375
		internal IMethodReturnMessage _mrmsg;

		// Token: 0x04002888 RID: 10376
		internal bool fault;
	}
}
