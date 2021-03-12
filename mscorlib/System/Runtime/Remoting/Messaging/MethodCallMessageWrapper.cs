using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000846 RID: 2118
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class MethodCallMessageWrapper : InternalMessageWrapper, IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005AE1 RID: 23265 RVA: 0x0013E106 File Offset: 0x0013C306
		public MethodCallMessageWrapper(IMethodCallMessage msg) : base(msg)
		{
			this._msg = msg;
			this._args = this._msg.Args;
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06005AE2 RID: 23266 RVA: 0x0013E127 File Offset: 0x0013C327
		// (set) Token: 0x06005AE3 RID: 23267 RVA: 0x0013E134 File Offset: 0x0013C334
		public virtual string Uri
		{
			[SecurityCritical]
			get
			{
				return this._msg.Uri;
			}
			set
			{
				this._msg.Properties[Message.UriKey] = value;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06005AE4 RID: 23268 RVA: 0x0013E14C File Offset: 0x0013C34C
		public virtual string MethodName
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodName;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06005AE5 RID: 23269 RVA: 0x0013E159 File Offset: 0x0013C359
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return this._msg.TypeName;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06005AE6 RID: 23270 RVA: 0x0013E166 File Offset: 0x0013C366
		public virtual object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodSignature;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x0013E173 File Offset: 0x0013C373
		public virtual LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._msg.LogicalCallContext;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x0013E180 File Offset: 0x0013C380
		public virtual MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._msg.MethodBase;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06005AE9 RID: 23273 RVA: 0x0013E18D File Offset: 0x0013C38D
		public virtual int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._args != null)
				{
					return this._args.Length;
				}
				return 0;
			}
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x0013E1A1 File Offset: 0x0013C3A1
		[SecurityCritical]
		public virtual string GetArgName(int index)
		{
			return this._msg.GetArgName(index);
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x0013E1AF File Offset: 0x0013C3AF
		[SecurityCritical]
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06005AEC RID: 23276 RVA: 0x0013E1B9 File Offset: 0x0013C3B9
		// (set) Token: 0x06005AED RID: 23277 RVA: 0x0013E1C1 File Offset: 0x0013C3C1
		public virtual object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x0013E1CA File Offset: 0x0013C3CA
		public virtual bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this._msg.HasVarArgs;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06005AEF RID: 23279 RVA: 0x0013E1D7 File Offset: 0x0013C3D7
		public virtual int InArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x0013E1F9 File Offset: 0x0013C3F9
		[SecurityCritical]
		public virtual object GetInArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x0013E21C File Offset: 0x0013C41C
		[SecurityCritical]
		public virtual string GetInArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x0013E23F File Offset: 0x0013C43F
		public virtual object[] InArgs
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06005AF3 RID: 23283 RVA: 0x0013E261 File Offset: 0x0013C461
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodCallMessageWrapper.MCMWrapperDictionary(this, this._msg.Properties);
				}
				return this._properties;
			}
		}

		// Token: 0x040028D9 RID: 10457
		private IMethodCallMessage _msg;

		// Token: 0x040028DA RID: 10458
		private IDictionary _properties;

		// Token: 0x040028DB RID: 10459
		private ArgMapper _argMapper;

		// Token: 0x040028DC RID: 10460
		private object[] _args;

		// Token: 0x02000C44 RID: 3140
		private class MCMWrapperDictionary : Hashtable
		{
			// Token: 0x06006F8E RID: 28558 RVA: 0x0017FD33 File Offset: 0x0017DF33
			public MCMWrapperDictionary(IMethodCallMessage msg, IDictionary idict)
			{
				this._mcmsg = msg;
				this._idict = idict;
			}

			// Token: 0x17001338 RID: 4920
			public override object this[object key]
			{
				[SecuritySafeCritical]
				get
				{
					string text = key as string;
					if (text != null)
					{
						if (text == "__Uri")
						{
							return this._mcmsg.Uri;
						}
						if (text == "__MethodName")
						{
							return this._mcmsg.MethodName;
						}
						if (text == "__MethodSignature")
						{
							return this._mcmsg.MethodSignature;
						}
						if (text == "__TypeName")
						{
							return this._mcmsg.TypeName;
						}
						if (text == "__Args")
						{
							return this._mcmsg.Args;
						}
					}
					return this._idict[key];
				}
				[SecuritySafeCritical]
				set
				{
					string text = key as string;
					if (text != null)
					{
						if (text == "__MethodName" || text == "__MethodSignature" || text == "__TypeName" || text == "__Args")
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
						}
						this._idict[key] = value;
					}
				}
			}

			// Token: 0x04003722 RID: 14114
			private IMethodCallMessage _mcmsg;

			// Token: 0x04003723 RID: 14115
			private IDictionary _idict;
		}
	}
}
