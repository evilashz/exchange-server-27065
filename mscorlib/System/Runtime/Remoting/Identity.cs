using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Cryptography;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000784 RID: 1924
	internal class Identity
	{
		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06005426 RID: 21542 RVA: 0x0012A564 File Offset: 0x00128764
		internal static string ProcessIDGuid
		{
			get
			{
				return SharedStatics.Remoting_Identity_IDGuid;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06005427 RID: 21543 RVA: 0x0012A56B File Offset: 0x0012876B
		internal static string AppDomainUniqueId
		{
			get
			{
				if (Identity.s_configuredAppDomainGuid != null)
				{
					return Identity.s_configuredAppDomainGuid;
				}
				return Identity.s_originalAppDomainGuid;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06005428 RID: 21544 RVA: 0x0012A57F File Offset: 0x0012877F
		internal static string IDGuidString
		{
			get
			{
				return Identity.s_IDGuidString;
			}
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0012A588 File Offset: 0x00128788
		internal static string RemoveAppNameOrAppGuidIfNecessary(string uri)
		{
			if (uri == null || uri.Length <= 1 || uri[0] != '/')
			{
				return uri;
			}
			string text;
			if (Identity.s_configuredAppDomainGuidString != null)
			{
				text = Identity.s_configuredAppDomainGuidString;
				if (uri.Length > text.Length && Identity.StringStartsWith(uri, text))
				{
					return uri.Substring(text.Length);
				}
			}
			text = Identity.s_originalAppDomainGuidString;
			if (uri.Length > text.Length && Identity.StringStartsWith(uri, text))
			{
				return uri.Substring(text.Length);
			}
			string applicationName = RemotingConfiguration.ApplicationName;
			if (applicationName != null && uri.Length > applicationName.Length + 2 && string.Compare(uri, 1, applicationName, 0, applicationName.Length, true, CultureInfo.InvariantCulture) == 0 && uri[applicationName.Length + 1] == '/')
			{
				return uri.Substring(applicationName.Length + 2);
			}
			uri = uri.Substring(1);
			return uri;
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0012A664 File Offset: 0x00128864
		private static bool StringStartsWith(string s1, string prefix)
		{
			return s1.Length >= prefix.Length && string.CompareOrdinal(s1, 0, prefix, 0, prefix.Length) == 0;
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x0012A688 File Offset: 0x00128888
		internal static string ProcessGuid
		{
			get
			{
				return Identity.ProcessIDGuid;
			}
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x0012A68F File Offset: 0x0012888F
		private static int GetNextSeqNum()
		{
			return SharedStatics.Remoting_Identity_GetNextSeqNum();
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0012A698 File Offset: 0x00128898
		private static byte[] GetRandomBytes()
		{
			byte[] array = new byte[18];
			Identity.s_rng.GetBytes(array);
			return array;
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0012A6B9 File Offset: 0x001288B9
		internal Identity(string objURI, string URL)
		{
			if (URL != null)
			{
				this._flags |= 256;
				this._URL = URL;
			}
			this.SetOrCreateURI(objURI, true);
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0012A6E5 File Offset: 0x001288E5
		internal Identity(bool bContextBound)
		{
			if (bContextBound)
			{
				this._flags |= 16;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06005430 RID: 21552 RVA: 0x0012A6FF File Offset: 0x001288FF
		internal bool IsContextBound
		{
			get
			{
				return (this._flags & 16) == 16;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x0012A70E File Offset: 0x0012890E
		// (set) Token: 0x06005432 RID: 21554 RVA: 0x0012A718 File Offset: 0x00128918
		internal bool IsInitializing
		{
			get
			{
				return this._initializing;
			}
			set
			{
				this._initializing = value;
			}
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0012A723 File Offset: 0x00128923
		internal bool IsWellKnown()
		{
			return (this._flags & 256) == 256;
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x0012A738 File Offset: 0x00128938
		internal void SetInIDTable()
		{
			int flags;
			int value;
			do
			{
				flags = this._flags;
				value = (this._flags | 4);
			}
			while (flags != Interlocked.CompareExchange(ref this._flags, value, flags));
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x0012A768 File Offset: 0x00128968
		[SecurityCritical]
		internal void ResetInIDTable(bool bResetURI)
		{
			int flags;
			int value;
			do
			{
				flags = this._flags;
				value = (this._flags & -5);
			}
			while (flags != Interlocked.CompareExchange(ref this._flags, value, flags));
			if (bResetURI)
			{
				((ObjRef)this._objRef).URI = null;
				this._ObjURI = null;
			}
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x0012A7B1 File Offset: 0x001289B1
		internal bool IsInIDTable()
		{
			return (this._flags & 4) == 4;
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x0012A7C0 File Offset: 0x001289C0
		internal void SetFullyConnected()
		{
			int flags;
			int value;
			do
			{
				flags = this._flags;
				value = (this._flags & -4);
			}
			while (flags != Interlocked.CompareExchange(ref this._flags, value, flags));
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0012A7EE File Offset: 0x001289EE
		internal bool IsFullyDisconnected()
		{
			return (this._flags & 1) == 1;
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x0012A7FB File Offset: 0x001289FB
		internal bool IsRemoteDisconnected()
		{
			return (this._flags & 2) == 2;
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x0012A808 File Offset: 0x00128A08
		internal bool IsDisconnected()
		{
			return this.IsFullyDisconnected() || this.IsRemoteDisconnected();
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x0600543B RID: 21563 RVA: 0x0012A81A File Offset: 0x00128A1A
		internal string URI
		{
			get
			{
				if (this.IsWellKnown())
				{
					return this._URL;
				}
				return this._ObjURI;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x0600543C RID: 21564 RVA: 0x0012A831 File Offset: 0x00128A31
		internal string ObjURI
		{
			get
			{
				return this._ObjURI;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x0600543D RID: 21565 RVA: 0x0012A839 File Offset: 0x00128A39
		internal MarshalByRefObject TPOrObject
		{
			get
			{
				return (MarshalByRefObject)this._tpOrObject;
			}
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0012A846 File Offset: 0x00128A46
		internal object RaceSetTransparentProxy(object tpObj)
		{
			if (this._tpOrObject == null)
			{
				Interlocked.CompareExchange(ref this._tpOrObject, tpObj, null);
			}
			return this._tpOrObject;
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x0012A864 File Offset: 0x00128A64
		internal ObjRef ObjectRef
		{
			[SecurityCritical]
			get
			{
				return (ObjRef)this._objRef;
			}
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0012A871 File Offset: 0x00128A71
		[SecurityCritical]
		internal ObjRef RaceSetObjRef(ObjRef objRefGiven)
		{
			if (this._objRef == null)
			{
				Interlocked.CompareExchange(ref this._objRef, objRefGiven, null);
			}
			return (ObjRef)this._objRef;
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06005441 RID: 21569 RVA: 0x0012A894 File Offset: 0x00128A94
		internal IMessageSink ChannelSink
		{
			get
			{
				return (IMessageSink)this._channelSink;
			}
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x0012A8A1 File Offset: 0x00128AA1
		internal IMessageSink RaceSetChannelSink(IMessageSink channelSink)
		{
			if (this._channelSink == null)
			{
				Interlocked.CompareExchange(ref this._channelSink, channelSink, null);
			}
			return (IMessageSink)this._channelSink;
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06005443 RID: 21571 RVA: 0x0012A8C4 File Offset: 0x00128AC4
		internal IMessageSink EnvoyChain
		{
			get
			{
				return (IMessageSink)this._envoyChain;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06005444 RID: 21572 RVA: 0x0012A8D1 File Offset: 0x00128AD1
		// (set) Token: 0x06005445 RID: 21573 RVA: 0x0012A8D9 File Offset: 0x00128AD9
		internal Lease Lease
		{
			get
			{
				return this._lease;
			}
			set
			{
				this._lease = value;
			}
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x0012A8E2 File Offset: 0x00128AE2
		internal IMessageSink RaceSetEnvoyChain(IMessageSink envoyChain)
		{
			if (this._envoyChain == null)
			{
				Interlocked.CompareExchange(ref this._envoyChain, envoyChain, null);
			}
			return (IMessageSink)this._envoyChain;
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0012A905 File Offset: 0x00128B05
		internal void SetOrCreateURI(string uri)
		{
			this.SetOrCreateURI(uri, false);
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x0012A910 File Offset: 0x00128B10
		internal void SetOrCreateURI(string uri, bool bIdCtor)
		{
			if (!bIdCtor && this._ObjURI != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
			}
			if (uri == null)
			{
				string text = Convert.ToBase64String(Identity.GetRandomBytes());
				this._ObjURI = string.Concat(new string[]
				{
					Identity.IDGuidString,
					text.Replace('/', '_'),
					"_",
					Identity.GetNextSeqNum().ToString(CultureInfo.InvariantCulture.NumberFormat),
					".rem"
				}).ToLower(CultureInfo.InvariantCulture);
				return;
			}
			if (this is ServerIdentity)
			{
				this._ObjURI = Identity.IDGuidString + uri;
				return;
			}
			this._ObjURI = uri;
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0012A9C2 File Offset: 0x00128BC2
		internal static string GetNewLogicalCallID()
		{
			return Identity.IDGuidString + Identity.GetNextSeqNum();
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x0012A9D8 File Offset: 0x00128BD8
		[SecurityCritical]
		[Conditional("_DEBUG")]
		internal virtual void AssertValid()
		{
			if (this.URI != null)
			{
				Identity identity = IdentityHolder.ResolveIdentity(this.URI);
			}
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0012A9FC File Offset: 0x00128BFC
		[SecurityCritical]
		internal bool AddProxySideDynamicProperty(IDynamicProperty prop)
		{
			bool result;
			lock (this)
			{
				if (this._dph == null)
				{
					DynamicPropertyHolder dph = new DynamicPropertyHolder();
					lock (this)
					{
						if (this._dph == null)
						{
							this._dph = dph;
						}
					}
				}
				result = this._dph.AddDynamicProperty(prop);
			}
			return result;
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0012AA84 File Offset: 0x00128C84
		[SecurityCritical]
		internal bool RemoveProxySideDynamicProperty(string name)
		{
			bool result;
			lock (this)
			{
				if (this._dph == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), name));
				}
				result = this._dph.RemoveDynamicProperty(name);
			}
			return result;
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x0600544D RID: 21581 RVA: 0x0012AAEC File Offset: 0x00128CEC
		internal ArrayWithSize ProxySideDynamicSinks
		{
			[SecurityCritical]
			get
			{
				if (this._dph == null)
				{
					return null;
				}
				return this._dph.DynamicSinks;
			}
		}

		// Token: 0x04002687 RID: 9863
		private static string s_originalAppDomainGuid = Guid.NewGuid().ToString().Replace('-', '_');

		// Token: 0x04002688 RID: 9864
		private static string s_configuredAppDomainGuid = null;

		// Token: 0x04002689 RID: 9865
		private static string s_originalAppDomainGuidString = "/" + Identity.s_originalAppDomainGuid.ToLower(CultureInfo.InvariantCulture) + "/";

		// Token: 0x0400268A RID: 9866
		private static string s_configuredAppDomainGuidString = null;

		// Token: 0x0400268B RID: 9867
		private static string s_IDGuidString = "/" + Identity.s_originalAppDomainGuid.ToLower(CultureInfo.InvariantCulture) + "/";

		// Token: 0x0400268C RID: 9868
		private static RNGCryptoServiceProvider s_rng = new RNGCryptoServiceProvider();

		// Token: 0x0400268D RID: 9869
		protected const int IDFLG_DISCONNECTED_FULL = 1;

		// Token: 0x0400268E RID: 9870
		protected const int IDFLG_DISCONNECTED_REM = 2;

		// Token: 0x0400268F RID: 9871
		protected const int IDFLG_IN_IDTABLE = 4;

		// Token: 0x04002690 RID: 9872
		protected const int IDFLG_CONTEXT_BOUND = 16;

		// Token: 0x04002691 RID: 9873
		protected const int IDFLG_WELLKNOWN = 256;

		// Token: 0x04002692 RID: 9874
		protected const int IDFLG_SERVER_SINGLECALL = 512;

		// Token: 0x04002693 RID: 9875
		protected const int IDFLG_SERVER_SINGLETON = 1024;

		// Token: 0x04002694 RID: 9876
		internal int _flags;

		// Token: 0x04002695 RID: 9877
		internal object _tpOrObject;

		// Token: 0x04002696 RID: 9878
		protected string _ObjURI;

		// Token: 0x04002697 RID: 9879
		protected string _URL;

		// Token: 0x04002698 RID: 9880
		internal object _objRef;

		// Token: 0x04002699 RID: 9881
		internal object _channelSink;

		// Token: 0x0400269A RID: 9882
		internal object _envoyChain;

		// Token: 0x0400269B RID: 9883
		internal DynamicPropertyHolder _dph;

		// Token: 0x0400269C RID: 9884
		internal Lease _lease;

		// Token: 0x0400269D RID: 9885
		private volatile bool _initializing;
	}
}
