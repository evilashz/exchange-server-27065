using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200083D RID: 2109
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionCall : MethodCall, IConstructionCallMessage, IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005A76 RID: 23158 RVA: 0x0013C843 File Offset: 0x0013AA43
		public ConstructionCall(Header[] headers) : base(headers)
		{
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x0013C84C File Offset: 0x0013AA4C
		public ConstructionCall(IMessage m) : base(m)
		{
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x0013C855 File Offset: 0x0013AA55
		internal ConstructionCall(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x0013C860 File Offset: 0x0013AA60
		[SecurityCritical]
		internal override bool FillSpecialHeader(string key, object value)
		{
			if (key != null)
			{
				if (key.Equals("__ActivationType"))
				{
					this._activationType = null;
				}
				else if (key.Equals("__ContextProperties"))
				{
					this._contextProperties = (IList)value;
				}
				else if (key.Equals("__CallSiteActivationAttributes"))
				{
					this._callSiteActivationAttributes = (object[])value;
				}
				else if (key.Equals("__Activator"))
				{
					this._activator = (IActivator)value;
				}
				else
				{
					if (!key.Equals("__ActivationTypeName"))
					{
						return base.FillSpecialHeader(key, value);
					}
					this._activationTypeName = (string)value;
				}
			}
			return true;
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06005A7A RID: 23162 RVA: 0x0013C8FF File Offset: 0x0013AAFF
		public object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get
			{
				return this._callSiteActivationAttributes;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06005A7B RID: 23163 RVA: 0x0013C907 File Offset: 0x0013AB07
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

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06005A7C RID: 23164 RVA: 0x0013C937 File Offset: 0x0013AB37
		public string ActivationTypeName
		{
			[SecurityCritical]
			get
			{
				return this._activationTypeName;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06005A7D RID: 23165 RVA: 0x0013C93F File Offset: 0x0013AB3F
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

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06005A7E RID: 23166 RVA: 0x0013C95C File Offset: 0x0013AB5C
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary externalProperties;
				lock (this)
				{
					if (this.InternalProperties == null)
					{
						this.InternalProperties = new Hashtable();
					}
					if (this.ExternalProperties == null)
					{
						this.ExternalProperties = new CCMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06005A7F RID: 23167 RVA: 0x0013C9C8 File Offset: 0x0013ABC8
		// (set) Token: 0x06005A80 RID: 23168 RVA: 0x0013C9D0 File Offset: 0x0013ABD0
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

		// Token: 0x040028B0 RID: 10416
		internal Type _activationType;

		// Token: 0x040028B1 RID: 10417
		internal string _activationTypeName;

		// Token: 0x040028B2 RID: 10418
		internal IList _contextProperties;

		// Token: 0x040028B3 RID: 10419
		internal object[] _callSiteActivationAttributes;

		// Token: 0x040028B4 RID: 10420
		internal IActivator _activator;
	}
}
