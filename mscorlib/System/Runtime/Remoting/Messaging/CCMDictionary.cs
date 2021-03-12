using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000834 RID: 2100
	internal class CCMDictionary : MessageDictionary
	{
		// Token: 0x060059DE RID: 23006 RVA: 0x0013A51A File Offset: 0x0013871A
		public CCMDictionary(IConstructionCallMessage msg, IDictionary idict) : base(CCMDictionary.CCMkeys, idict)
		{
			this._ccmsg = msg;
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x0013A530 File Offset: 0x00138730
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._ccmsg.Uri;
			case 1:
				return this._ccmsg.MethodName;
			case 2:
				return this._ccmsg.MethodSignature;
			case 3:
				return this._ccmsg.TypeName;
			case 4:
				return this._ccmsg.Args;
			case 5:
				return this.FetchLogicalCallContext();
			case 6:
				return this._ccmsg.CallSiteActivationAttributes;
			case 7:
				return null;
			case 8:
				return this._ccmsg.ContextProperties;
			case 9:
				return this._ccmsg.Activator;
			case 10:
				return this._ccmsg.ActivationTypeName;
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x0013A5F8 File Offset: 0x001387F8
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			ConstructorCallMessage constructorCallMessage = this._ccmsg as ConstructorCallMessage;
			if (constructorCallMessage != null)
			{
				return constructorCallMessage.GetLogicalCallContext();
			}
			if (this._ccmsg is ConstructionCall)
			{
				return ((MethodCall)this._ccmsg).GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x0013A648 File Offset: 0x00138848
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			if (keyNum == 0)
			{
				((ConstructorCallMessage)this._ccmsg).Uri = (string)value;
				return;
			}
			if (keyNum != 1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
			((ConstructorCallMessage)this._ccmsg).SetLogicalCallContext((LogicalCallContext)value);
		}

		// Token: 0x0400287D RID: 10365
		public static string[] CCMkeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__MethodSignature",
			"__TypeName",
			"__Args",
			"__CallContext",
			"__CallSiteActivationAttributes",
			"__ActivationType",
			"__ContextProperties",
			"__Activator",
			"__ActivationTypeName"
		};

		// Token: 0x0400287E RID: 10366
		internal IConstructionCallMessage _ccmsg;
	}
}
