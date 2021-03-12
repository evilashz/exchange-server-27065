using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000832 RID: 2098
	[SecurityCritical]
	internal class ConstructorReturnMessage : ReturnMessage, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x060059B7 RID: 22967 RVA: 0x0013A0B3 File Offset: 0x001382B3
		public ConstructorReturnMessage(MarshalByRefObject o, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IConstructionCallMessage ccm) : base(o, outArgs, outArgsCount, callCtx, ccm)
		{
			this._o = o;
			this._iFlags = 1;
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x0013A0D0 File Offset: 0x001382D0
		public ConstructorReturnMessage(Exception e, IConstructionCallMessage ccm) : base(e, ccm)
		{
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060059B9 RID: 22969 RVA: 0x0013A0DA File Offset: 0x001382DA
		public override object ReturnValue
		{
			[SecurityCritical]
			get
			{
				if (this._iFlags == 1)
				{
					return RemotingServices.MarshalInternal(this._o, null, null);
				}
				return base.ReturnValue;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x060059BA RID: 22970 RVA: 0x0013A0FC File Offset: 0x001382FC
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					object value = new CRMDictionary(this, new Hashtable());
					Interlocked.CompareExchange(ref this._properties, value, null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x0013A136 File Offset: 0x00138336
		internal object GetObject()
		{
			return this._o;
		}

		// Token: 0x0400286E RID: 10350
		private const int Intercept = 1;

		// Token: 0x0400286F RID: 10351
		private MarshalByRefObject _o;

		// Token: 0x04002870 RID: 10352
		private int _iFlags;
	}
}
