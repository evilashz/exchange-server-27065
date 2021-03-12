using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000861 RID: 2145
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class LogicalCallContext : ISerializable, ICloneable
	{
		// Token: 0x06005BA5 RID: 23461 RVA: 0x001407FA File Offset: 0x0013E9FA
		internal LogicalCallContext()
		{
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x00140804 File Offset: 0x0013EA04
		[SecurityCritical]
		internal LogicalCallContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("__RemotingData"))
				{
					this.m_RemotingData = (CallContextRemotingData)enumerator.Value;
				}
				else if (enumerator.Name.Equals("__SecurityData"))
				{
					if (context.State == StreamingContextStates.CrossAppDomain)
					{
						this.m_SecurityData = (CallContextSecurityData)enumerator.Value;
					}
				}
				else if (enumerator.Name.Equals("__HostContext"))
				{
					this.m_HostContext = enumerator.Value;
				}
				else if (enumerator.Name.Equals("__CorrelationMgrSlotPresent"))
				{
					this.m_IsCorrelationMgr = (bool)enumerator.Value;
				}
				else
				{
					this.Datastore[enumerator.Name] = enumerator.Value;
				}
			}
		}

		// Token: 0x06005BA7 RID: 23463 RVA: 0x001408E8 File Offset: 0x0013EAE8
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(LogicalCallContext.s_callContextType);
			if (this.m_RemotingData != null)
			{
				info.AddValue("__RemotingData", this.m_RemotingData);
			}
			if (this.m_SecurityData != null && context.State == StreamingContextStates.CrossAppDomain)
			{
				info.AddValue("__SecurityData", this.m_SecurityData);
			}
			if (this.m_HostContext != null)
			{
				info.AddValue("__HostContext", this.m_HostContext);
			}
			if (this.m_IsCorrelationMgr)
			{
				info.AddValue("__CorrelationMgrSlotPresent", this.m_IsCorrelationMgr);
			}
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					info.AddValue((string)enumerator.Key, enumerator.Value);
				}
			}
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x001409B8 File Offset: 0x0013EBB8
		[SecuritySafeCritical]
		public object Clone()
		{
			LogicalCallContext logicalCallContext = new LogicalCallContext();
			if (this.m_RemotingData != null)
			{
				logicalCallContext.m_RemotingData = (CallContextRemotingData)this.m_RemotingData.Clone();
			}
			if (this.m_SecurityData != null)
			{
				logicalCallContext.m_SecurityData = (CallContextSecurityData)this.m_SecurityData.Clone();
			}
			if (this.m_HostContext != null)
			{
				logicalCallContext.m_HostContext = this.m_HostContext;
			}
			logicalCallContext.m_IsCorrelationMgr = this.m_IsCorrelationMgr;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				if (!this.m_IsCorrelationMgr)
				{
					while (enumerator.MoveNext())
					{
						logicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
					}
				}
				else
				{
					while (enumerator.MoveNext())
					{
						string text = (string)enumerator.Key;
						if (text.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
						{
							logicalCallContext.Datastore[text] = ((ICloneable)enumerator.Value).Clone();
						}
						else
						{
							logicalCallContext.Datastore[text] = enumerator.Value;
						}
					}
				}
			}
			return logicalCallContext;
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x00140AC0 File Offset: 0x0013ECC0
		[SecurityCritical]
		internal void Merge(LogicalCallContext lc)
		{
			if (lc != null && this != lc && lc.HasUserData)
			{
				IDictionaryEnumerator enumerator = lc.Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x00140B10 File Offset: 0x0013ED10
		public bool HasInfo
		{
			[SecurityCritical]
			get
			{
				bool result = false;
				if ((this.m_RemotingData != null && this.m_RemotingData.HasInfo) || (this.m_SecurityData != null && this.m_SecurityData.HasInfo) || this.m_HostContext != null || this.HasUserData)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06005BAB RID: 23467 RVA: 0x00140B5C File Offset: 0x0013ED5C
		private bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x00140B76 File Offset: 0x0013ED76
		internal CallContextRemotingData RemotingData
		{
			get
			{
				if (this.m_RemotingData == null)
				{
					this.m_RemotingData = new CallContextRemotingData();
				}
				return this.m_RemotingData;
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06005BAD RID: 23469 RVA: 0x00140B91 File Offset: 0x0013ED91
		internal CallContextSecurityData SecurityData
		{
			get
			{
				if (this.m_SecurityData == null)
				{
					this.m_SecurityData = new CallContextSecurityData();
				}
				return this.m_SecurityData;
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06005BAE RID: 23470 RVA: 0x00140BAC File Offset: 0x0013EDAC
		// (set) Token: 0x06005BAF RID: 23471 RVA: 0x00140BB4 File Offset: 0x0013EDB4
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06005BB0 RID: 23472 RVA: 0x00140BBD File Offset: 0x0013EDBD
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06005BB1 RID: 23473 RVA: 0x00140BD8 File Offset: 0x0013EDD8
		// (set) Token: 0x06005BB2 RID: 23474 RVA: 0x00140BEF File Offset: 0x0013EDEF
		internal IPrincipal Principal
		{
			get
			{
				if (this.m_SecurityData != null)
				{
					return this.m_SecurityData.Principal;
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				this.SecurityData.Principal = value;
			}
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x00140BFD File Offset: 0x0013EDFD
		[SecurityCritical]
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x00140C0B File Offset: 0x0013EE0B
		[SecurityCritical]
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x00140C19 File Offset: 0x0013EE19
		[SecurityCritical]
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
			if (name.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
			{
				this.m_IsCorrelationMgr = true;
			}
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x00140C3C File Offset: 0x0013EE3C
		private Header[] InternalGetOutgoingHeaders()
		{
			Header[] sendHeaders = this._sendHeaders;
			this._sendHeaders = null;
			this._recvHeaders = null;
			return sendHeaders;
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x00140C5F File Offset: 0x0013EE5F
		internal void InternalSetHeaders(Header[] headers)
		{
			this._sendHeaders = headers;
			this._recvHeaders = null;
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x00140C6F File Offset: 0x0013EE6F
		internal Header[] InternalGetHeaders()
		{
			if (this._sendHeaders != null)
			{
				return this._sendHeaders;
			}
			return this._recvHeaders;
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x00140C88 File Offset: 0x0013EE88
		[SecurityCritical]
		internal IPrincipal RemovePrincipalIfNotSerializable()
		{
			IPrincipal principal = this.Principal;
			if (principal != null && !principal.GetType().IsSerializable)
			{
				this.Principal = null;
			}
			return principal;
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x00140CB4 File Offset: 0x0013EEB4
		[SecurityCritical]
		internal void PropagateOutgoingHeadersToMessage(IMessage msg)
		{
			Header[] array = this.InternalGetOutgoingHeaders();
			if (array != null)
			{
				IDictionary properties = msg.Properties;
				foreach (Header header in array)
				{
					if (header != null)
					{
						string propertyKeyForHeader = LogicalCallContext.GetPropertyKeyForHeader(header);
						properties[propertyKeyForHeader] = header;
					}
				}
			}
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x00140CFE File Offset: 0x0013EEFE
		internal static string GetPropertyKeyForHeader(Header header)
		{
			if (header == null)
			{
				return null;
			}
			if (header.HeaderNamespace != null)
			{
				return header.Name + ", " + header.HeaderNamespace;
			}
			return header.Name;
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x00140D2C File Offset: 0x0013EF2C
		[SecurityCritical]
		internal void PropagateIncomingHeadersToCallContext(IMessage msg)
		{
			IInternalMessage internalMessage = msg as IInternalMessage;
			if (internalMessage != null && !internalMessage.HasProperties())
			{
				return;
			}
			IDictionary properties = msg.Properties;
			IDictionaryEnumerator enumerator = properties.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				string text = (string)enumerator.Key;
				if (!text.StartsWith("__", StringComparison.Ordinal) && enumerator.Value is Header)
				{
					num++;
				}
			}
			Header[] array = null;
			if (num > 0)
			{
				array = new Header[num];
				num = 0;
				enumerator.Reset();
				while (enumerator.MoveNext())
				{
					string text2 = (string)enumerator.Key;
					if (!text2.StartsWith("__", StringComparison.Ordinal))
					{
						Header header = enumerator.Value as Header;
						if (header != null)
						{
							array[num++] = header;
						}
					}
				}
			}
			this._recvHeaders = array;
			this._sendHeaders = null;
		}

		// Token: 0x0400291E RID: 10526
		private static Type s_callContextType = typeof(LogicalCallContext);

		// Token: 0x0400291F RID: 10527
		private const string s_CorrelationMgrSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x04002920 RID: 10528
		private Hashtable m_Datastore;

		// Token: 0x04002921 RID: 10529
		private CallContextRemotingData m_RemotingData;

		// Token: 0x04002922 RID: 10530
		private CallContextSecurityData m_SecurityData;

		// Token: 0x04002923 RID: 10531
		private object m_HostContext;

		// Token: 0x04002924 RID: 10532
		private bool m_IsCorrelationMgr;

		// Token: 0x04002925 RID: 10533
		private Header[] _sendHeaders;

		// Token: 0x04002926 RID: 10534
		private Header[] _recvHeaders;

		// Token: 0x02000C48 RID: 3144
		internal struct Reader
		{
			// Token: 0x06006F9A RID: 28570 RVA: 0x0017FFFF File Offset: 0x0017E1FF
			public Reader(LogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x1700133D RID: 4925
			// (get) Token: 0x06006F9B RID: 28571 RVA: 0x00180008 File Offset: 0x0017E208
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x1700133E RID: 4926
			// (get) Token: 0x06006F9C RID: 28572 RVA: 0x00180013 File Offset: 0x0017E213
			public bool HasInfo
			{
				get
				{
					return !this.IsNull && this.m_ctx.HasInfo;
				}
			}

			// Token: 0x06006F9D RID: 28573 RVA: 0x0018002A File Offset: 0x0017E22A
			public LogicalCallContext Clone()
			{
				return (LogicalCallContext)this.m_ctx.Clone();
			}

			// Token: 0x1700133F RID: 4927
			// (get) Token: 0x06006F9E RID: 28574 RVA: 0x0018003C File Offset: 0x0017E23C
			public IPrincipal Principal
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.Principal;
					}
					return null;
				}
			}

			// Token: 0x06006F9F RID: 28575 RVA: 0x00180053 File Offset: 0x0017E253
			[SecurityCritical]
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x17001340 RID: 4928
			// (get) Token: 0x06006FA0 RID: 28576 RVA: 0x0018006B File Offset: 0x0017E26B
			public object HostContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.HostContext;
					}
					return null;
				}
			}

			// Token: 0x04003728 RID: 14120
			private LogicalCallContext m_ctx;
		}
	}
}
