using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000833 RID: 2099
	internal class ConstructorCallMessage : IConstructionCallMessage, IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x060059BC RID: 22972 RVA: 0x0013A13E File Offset: 0x0013833E
		private ConstructorCallMessage()
		{
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x0013A146 File Offset: 0x00138346
		[SecurityCritical]
		internal ConstructorCallMessage(object[] callSiteActivationAttributes, object[] womAttr, object[] typeAttr, RuntimeType serverType)
		{
			this._activationType = serverType;
			this._activationTypeName = RemotingServices.GetDefaultQualifiedTypeName(this._activationType);
			this._callSiteActivationAttributes = callSiteActivationAttributes;
			this._womGlobalAttributes = womAttr;
			this._typeAttributes = typeAttr;
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x0013A17C File Offset: 0x0013837C
		[SecurityCritical]
		public object GetThisPtr()
		{
			if (this._message != null)
			{
				return this._message.GetThisPtr();
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060059BF RID: 22975 RVA: 0x0013A1A1 File Offset: 0x001383A1
		public object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get
			{
				return this._callSiteActivationAttributes;
			}
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x0013A1A9 File Offset: 0x001383A9
		internal object[] GetWOMAttributes()
		{
			return this._womGlobalAttributes;
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x0013A1B1 File Offset: 0x001383B1
		internal object[] GetTypeAttributes()
		{
			return this._typeAttributes;
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060059C2 RID: 22978 RVA: 0x0013A1B9 File Offset: 0x001383B9
		public Type ActivationType
		{
			[SecurityCritical]
			get
			{
				if (this._activationType == null && this._activationTypeName != null)
				{
					this._activationType = RemotingServices.InternalGetTypeFromQualifiedTypeName(this._activationTypeName, false);
				}
				return this._activationType;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060059C3 RID: 22979 RVA: 0x0013A1E9 File Offset: 0x001383E9
		public string ActivationTypeName
		{
			[SecurityCritical]
			get
			{
				return this._activationTypeName;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x0013A1F1 File Offset: 0x001383F1
		public IList ContextProperties
		{
			[SecurityCritical]
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060059C5 RID: 22981 RVA: 0x0013A20C File Offset: 0x0013840C
		// (set) Token: 0x060059C6 RID: 22982 RVA: 0x0013A231 File Offset: 0x00138431
		public string Uri
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.Uri;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
			set
			{
				if (this._message != null)
				{
					this._message.Uri = value;
					return;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060059C7 RID: 22983 RVA: 0x0013A257 File Offset: 0x00138457
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.MethodName;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060059C8 RID: 22984 RVA: 0x0013A27C File Offset: 0x0013847C
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.TypeName;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060059C9 RID: 22985 RVA: 0x0013A2A1 File Offset: 0x001384A1
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.MethodSignature;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060059CA RID: 22986 RVA: 0x0013A2C6 File Offset: 0x001384C6
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.MethodBase;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060059CB RID: 22987 RVA: 0x0013A2EB File Offset: 0x001384EB
		public int InArgCount
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

		// Token: 0x060059CC RID: 22988 RVA: 0x0013A30D File Offset: 0x0013850D
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x0013A330 File Offset: 0x00138530
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060059CE RID: 22990 RVA: 0x0013A353 File Offset: 0x00138553
		public object[] InArgs
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

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060059CF RID: 22991 RVA: 0x0013A375 File Offset: 0x00138575
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.ArgCount;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x0013A39A File Offset: 0x0013859A
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			if (this._message != null)
			{
				return this._message.GetArg(argNum);
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x0013A3C0 File Offset: 0x001385C0
		[SecurityCritical]
		public string GetArgName(int index)
		{
			if (this._message != null)
			{
				return this._message.GetArgName(index);
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060059D2 RID: 22994 RVA: 0x0013A3E6 File Offset: 0x001385E6
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.HasVarArgs;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060059D3 RID: 22995 RVA: 0x0013A40B File Offset: 0x0013860B
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.Args;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060059D4 RID: 22996 RVA: 0x0013A430 File Offset: 0x00138630
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					object value = new CCMDictionary(this, new Hashtable());
					Interlocked.CompareExchange(ref this._properties, value, null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060059D5 RID: 22997 RVA: 0x0013A46A File Offset: 0x0013866A
		// (set) Token: 0x060059D6 RID: 22998 RVA: 0x0013A472 File Offset: 0x00138672
		public IActivator Activator
		{
			[SecurityCritical]
			get
			{
				return this._activator;
			}
			[SecurityCritical]
			set
			{
				this._activator = value;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060059D7 RID: 22999 RVA: 0x0013A47B File Offset: 0x0013867B
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x060059D8 RID: 23000 RVA: 0x0013A483 File Offset: 0x00138683
		// (set) Token: 0x060059D9 RID: 23001 RVA: 0x0013A490 File Offset: 0x00138690
		internal bool ActivateInContext
		{
			get
			{
				return (this._iFlags & 1) != 0;
			}
			set
			{
				this._iFlags = (value ? (this._iFlags | 1) : (this._iFlags & -2));
			}
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x0013A4AE File Offset: 0x001386AE
		[SecurityCritical]
		internal void SetFrame(MessageData msgData)
		{
			this._message = new Message();
			this._message.InitFields(msgData);
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x0013A4C7 File Offset: 0x001386C7
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this._message != null)
			{
				return this._message.GetLogicalCallContext();
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x0013A4EC File Offset: 0x001386EC
		[SecurityCritical]
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			if (this._message != null)
			{
				return this._message.SetLogicalCallContext(ctx);
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x0013A512 File Offset: 0x00138712
		internal Message GetMessage()
		{
			return this._message;
		}

		// Token: 0x04002871 RID: 10353
		private object[] _callSiteActivationAttributes;

		// Token: 0x04002872 RID: 10354
		private object[] _womGlobalAttributes;

		// Token: 0x04002873 RID: 10355
		private object[] _typeAttributes;

		// Token: 0x04002874 RID: 10356
		[NonSerialized]
		private RuntimeType _activationType;

		// Token: 0x04002875 RID: 10357
		private string _activationTypeName;

		// Token: 0x04002876 RID: 10358
		private IList _contextProperties;

		// Token: 0x04002877 RID: 10359
		private int _iFlags;

		// Token: 0x04002878 RID: 10360
		private Message _message;

		// Token: 0x04002879 RID: 10361
		private object _properties;

		// Token: 0x0400287A RID: 10362
		private ArgMapper _argMapper;

		// Token: 0x0400287B RID: 10363
		private IActivator _activator;

		// Token: 0x0400287C RID: 10364
		private const int CCM_ACTIVATEINCONTEXT = 1;
	}
}
