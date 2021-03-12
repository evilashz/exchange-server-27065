using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000835 RID: 2101
	internal class CRMDictionary : MessageDictionary
	{
		// Token: 0x060059E3 RID: 23011 RVA: 0x0013A70F File Offset: 0x0013890F
		[SecurityCritical]
		public CRMDictionary(IConstructionReturnMessage msg, IDictionary idict) : base((msg.Exception != null) ? CRMDictionary.CRMkeysFault : CRMDictionary.CRMkeysNoFault, idict)
		{
			this.fault = (msg.Exception != null);
			this._crmsg = msg;
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x0013A744 File Offset: 0x00138944
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._crmsg.Uri;
			case 1:
				return this._crmsg.MethodName;
			case 2:
				return this._crmsg.MethodSignature;
			case 3:
				return this._crmsg.TypeName;
			case 4:
				if (!this.fault)
				{
					return this._crmsg.ReturnValue;
				}
				return this.FetchLogicalCallContext();
			case 5:
				return this._crmsg.Args;
			case 6:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x0013A7E4 File Offset: 0x001389E4
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			ReturnMessage returnMessage = this._crmsg as ReturnMessage;
			if (returnMessage != null)
			{
				return returnMessage.GetLogicalCallContext();
			}
			MethodResponse methodResponse = this._crmsg as MethodResponse;
			if (methodResponse != null)
			{
				return methodResponse.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x0013A82C File Offset: 0x00138A2C
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			ReturnMessage returnMessage = this._crmsg as ReturnMessage;
			MethodResponse methodResponse = this._crmsg as MethodResponse;
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

		// Token: 0x0400287F RID: 10367
		public static string[] CRMkeysFault = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__CallContext"
		};

		// Token: 0x04002880 RID: 10368
		public static string[] CRMkeysNoFault = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__Return",
			"__OutArgs",
			"__CallContext"
		};

		// Token: 0x04002881 RID: 10369
		internal IConstructionReturnMessage _crmsg;

		// Token: 0x04002882 RID: 10370
		internal bool fault;
	}
}
