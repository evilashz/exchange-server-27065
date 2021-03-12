using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000842 RID: 2114
	[Serializable]
	internal class TransitionCall : IMessage, IInternalMessage, IMessageSink, ISerializable
	{
		// Token: 0x06005AB3 RID: 23219 RVA: 0x0013D740 File Offset: 0x0013B940
		[SecurityCritical]
		internal TransitionCall(IntPtr targetCtxID, CrossContextDelegate deleg)
		{
			this._sourceCtxID = Thread.CurrentContext.InternalContextID;
			this._targetCtxID = targetCtxID;
			this._delegate = deleg;
			this._targetDomainID = 0;
			this._eeData = IntPtr.Zero;
			this._srvID = new ServerIdentity(null, Thread.GetContextInternal(this._targetCtxID));
			this._ID = this._srvID;
			this._ID.RaceSetChannelSink(CrossContextChannel.MessageSink);
			this._srvID.RaceSetServerObjectChain(this);
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x0013D7C4 File Offset: 0x0013B9C4
		[SecurityCritical]
		internal TransitionCall(IntPtr targetCtxID, IntPtr eeData, int targetDomainID)
		{
			this._sourceCtxID = Thread.CurrentContext.InternalContextID;
			this._targetCtxID = targetCtxID;
			this._delegate = null;
			this._targetDomainID = targetDomainID;
			this._eeData = eeData;
			this._srvID = null;
			this._ID = new Identity("TransitionCallURI", null);
			CrossAppDomainData data = new CrossAppDomainData(this._targetCtxID, this._targetDomainID, Identity.ProcessGuid);
			string text;
			IMessageSink channelSink = CrossAppDomainChannel.AppDomainChannel.CreateMessageSink(null, data, out text);
			this._ID.RaceSetChannelSink(channelSink);
		}

		// Token: 0x06005AB5 RID: 23221 RVA: 0x0013D850 File Offset: 0x0013BA50
		internal TransitionCall(SerializationInfo info, StreamingContext context)
		{
			if (info == null || context.State != StreamingContextStates.CrossAppDomain)
			{
				throw new ArgumentNullException("info");
			}
			this._props = (IDictionary)info.GetValue("props", typeof(IDictionary));
			this._delegate = (CrossContextDelegate)info.GetValue("delegate", typeof(CrossContextDelegate));
			this._sourceCtxID = (IntPtr)info.GetValue("sourceCtxID", typeof(IntPtr));
			this._targetCtxID = (IntPtr)info.GetValue("targetCtxID", typeof(IntPtr));
			this._eeData = (IntPtr)info.GetValue("eeData", typeof(IntPtr));
			this._targetDomainID = info.GetInt32("targetDomainID");
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06005AB6 RID: 23222 RVA: 0x0013D930 File Offset: 0x0013BB30
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._props == null)
				{
					lock (this)
					{
						if (this._props == null)
						{
							this._props = new Hashtable();
						}
					}
				}
				return this._props;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06005AB7 RID: 23223 RVA: 0x0013D988 File Offset: 0x0013BB88
		// (set) Token: 0x06005AB8 RID: 23224 RVA: 0x0013DA0C File Offset: 0x0013BC0C
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				if (this._targetDomainID != 0 && this._srvID == null)
				{
					lock (this)
					{
						if (Thread.GetContextInternal(this._targetCtxID) == null)
						{
							Context defaultContext = Context.DefaultContext;
						}
						this._srvID = new ServerIdentity(null, Thread.GetContextInternal(this._targetCtxID));
						this._srvID.RaceSetServerObjectChain(this);
					}
				}
				return this._srvID;
			}
			[SecurityCritical]
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06005AB9 RID: 23225 RVA: 0x0013DA1D File Offset: 0x0013BC1D
		// (set) Token: 0x06005ABA RID: 23226 RVA: 0x0013DA25 File Offset: 0x0013BC25
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return this._ID;
			}
			[SecurityCritical]
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x0013DA36 File Offset: 0x0013BC36
		[SecurityCritical]
		void IInternalMessage.SetURI(string uri)
		{
			throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x0013DA47 File Offset: 0x0013BC47
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext callContext)
		{
			throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
		}

		// Token: 0x06005ABD RID: 23229 RVA: 0x0013DA58 File Offset: 0x0013BC58
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
		}

		// Token: 0x06005ABE RID: 23230 RVA: 0x0013DA6C File Offset: 0x0013BC6C
		[SecurityCritical]
		public IMessage SyncProcessMessage(IMessage msg)
		{
			try
			{
				LogicalCallContext oldcctx = Message.PropagateCallContextFromMessageToThread(msg);
				if (this._delegate != null)
				{
					this._delegate();
				}
				else
				{
					CallBackHelper @object = new CallBackHelper(this._eeData, true, this._targetDomainID);
					CrossContextDelegate crossContextDelegate = new CrossContextDelegate(@object.Func);
					crossContextDelegate();
				}
				Message.PropagateCallContextFromThreadToMessage(msg, oldcctx);
			}
			catch (Exception e)
			{
				ReturnMessage returnMessage = new ReturnMessage(e, new ErrorMessage());
				returnMessage.SetLogicalCallContext((LogicalCallContext)msg.Properties[Message.CallContextKey]);
				return returnMessage;
			}
			return this;
		}

		// Token: 0x06005ABF RID: 23231 RVA: 0x0013DB0C File Offset: 0x0013BD0C
		[SecurityCritical]
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMessage msg2 = this.SyncProcessMessage(msg);
			replySink.SyncProcessMessage(msg2);
			return null;
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x0013DB2A File Offset: 0x0013BD2A
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06005AC1 RID: 23233 RVA: 0x0013DB30 File Offset: 0x0013BD30
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null || context.State != StreamingContextStates.CrossAppDomain)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("props", this._props, typeof(IDictionary));
			info.AddValue("delegate", this._delegate, typeof(CrossContextDelegate));
			info.AddValue("sourceCtxID", this._sourceCtxID);
			info.AddValue("targetCtxID", this._targetCtxID);
			info.AddValue("targetDomainID", this._targetDomainID);
			info.AddValue("eeData", this._eeData);
		}

		// Token: 0x040028C7 RID: 10439
		private IDictionary _props;

		// Token: 0x040028C8 RID: 10440
		private IntPtr _sourceCtxID;

		// Token: 0x040028C9 RID: 10441
		private IntPtr _targetCtxID;

		// Token: 0x040028CA RID: 10442
		private int _targetDomainID;

		// Token: 0x040028CB RID: 10443
		private ServerIdentity _srvID;

		// Token: 0x040028CC RID: 10444
		private Identity _ID;

		// Token: 0x040028CD RID: 10445
		private CrossContextDelegate _delegate;

		// Token: 0x040028CE RID: 10446
		private IntPtr _eeData;
	}
}
