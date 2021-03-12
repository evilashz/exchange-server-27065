using System;
using System.Collections;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200083A RID: 2106
	internal class StackBasedReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage, IInternalMessage
	{
		// Token: 0x06005A0F RID: 23055 RVA: 0x0013B22C File Offset: 0x0013942C
		internal StackBasedReturnMessage()
		{
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x0013B234 File Offset: 0x00139434
		internal void InitFields(Message m)
		{
			this._m = m;
			if (this._h != null)
			{
				this._h.Clear();
			}
			if (this._d != null)
			{
				this._d.Clear();
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06005A11 RID: 23057 RVA: 0x0013B263 File Offset: 0x00139463
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this._m.Uri;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06005A12 RID: 23058 RVA: 0x0013B270 File Offset: 0x00139470
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this._m.MethodName;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06005A13 RID: 23059 RVA: 0x0013B27D File Offset: 0x0013947D
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this._m.TypeName;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06005A14 RID: 23060 RVA: 0x0013B28A File Offset: 0x0013948A
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this._m.MethodSignature;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06005A15 RID: 23061 RVA: 0x0013B297 File Offset: 0x00139497
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._m.MethodBase;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06005A16 RID: 23062 RVA: 0x0013B2A4 File Offset: 0x001394A4
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this._m.HasVarArgs;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06005A17 RID: 23063 RVA: 0x0013B2B1 File Offset: 0x001394B1
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this._m.ArgCount;
			}
		}

		// Token: 0x06005A18 RID: 23064 RVA: 0x0013B2BE File Offset: 0x001394BE
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this._m.GetArg(argNum);
		}

		// Token: 0x06005A19 RID: 23065 RVA: 0x0013B2CC File Offset: 0x001394CC
		[SecurityCritical]
		public string GetArgName(int index)
		{
			return this._m.GetArgName(index);
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06005A1A RID: 23066 RVA: 0x0013B2DA File Offset: 0x001394DA
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._m.Args;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06005A1B RID: 23067 RVA: 0x0013B2E7 File Offset: 0x001394E7
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._m.GetLogicalCallContext();
			}
		}

		// Token: 0x06005A1C RID: 23068 RVA: 0x0013B2F4 File Offset: 0x001394F4
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			return this._m.GetLogicalCallContext();
		}

		// Token: 0x06005A1D RID: 23069 RVA: 0x0013B301 File Offset: 0x00139501
		[SecurityCritical]
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			return this._m.SetLogicalCallContext(callCtx);
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06005A1E RID: 23070 RVA: 0x0013B30F File Offset: 0x0013950F
		public int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06005A1F RID: 23071 RVA: 0x0013B331 File Offset: 0x00139531
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005A20 RID: 23072 RVA: 0x0013B354 File Offset: 0x00139554
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06005A21 RID: 23073 RVA: 0x0013B377 File Offset: 0x00139577
		public object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06005A22 RID: 23074 RVA: 0x0013B399 File Offset: 0x00139599
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x0013B39C File Offset: 0x0013959C
		public object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._m.GetReturnValue();
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06005A24 RID: 23076 RVA: 0x0013B3AC File Offset: 0x001395AC
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary d;
				lock (this)
				{
					if (this._h == null)
					{
						this._h = new Hashtable();
					}
					if (this._d == null)
					{
						this._d = new MRMDictionary(this, this._h);
					}
					d = this._d;
				}
				return d;
			}
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06005A25 RID: 23077 RVA: 0x0013B418 File Offset: 0x00139618
		// (set) Token: 0x06005A26 RID: 23078 RVA: 0x0013B41B File Offset: 0x0013961B
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06005A27 RID: 23079 RVA: 0x0013B41D File Offset: 0x0013961D
		// (set) Token: 0x06005A28 RID: 23080 RVA: 0x0013B420 File Offset: 0x00139620
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x06005A29 RID: 23081 RVA: 0x0013B422 File Offset: 0x00139622
		[SecurityCritical]
		void IInternalMessage.SetURI(string val)
		{
			this._m.Uri = val;
		}

		// Token: 0x06005A2A RID: 23082 RVA: 0x0013B430 File Offset: 0x00139630
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
		{
			this._m.SetLogicalCallContext(newCallContext);
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x0013B43F File Offset: 0x0013963F
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			return this._h != null;
		}

		// Token: 0x0400288E RID: 10382
		private Message _m;

		// Token: 0x0400288F RID: 10383
		private Hashtable _h;

		// Token: 0x04002890 RID: 10384
		private MRMDictionary _d;

		// Token: 0x04002891 RID: 10385
		private ArgMapper _argMapper;
	}
}
