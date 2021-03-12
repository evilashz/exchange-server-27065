using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000779 RID: 1913
	[Serializable]
	internal sealed class BinaryMethodCallMessage
	{
		// Token: 0x060053D1 RID: 21457 RVA: 0x00129578 File Offset: 0x00127778
		[SecurityCritical]
		internal BinaryMethodCallMessage(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, LogicalCallContext callContext, object[] properties)
		{
			this._methodName = methodName;
			this._typeName = typeName;
			if (args == null)
			{
				args = new object[0];
			}
			this._inargs = args;
			this._args = args;
			this._instArgs = instArgs;
			this._methodSignature = methodSignature;
			if (callContext == null)
			{
				this._logicalCallContext = new LogicalCallContext();
			}
			else
			{
				this._logicalCallContext = callContext;
			}
			this._properties = properties;
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060053D2 RID: 21458 RVA: 0x001295E6 File Offset: 0x001277E6
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060053D3 RID: 21459 RVA: 0x001295EE File Offset: 0x001277EE
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060053D4 RID: 21460 RVA: 0x001295F6 File Offset: 0x001277F6
		public Type[] InstantiationArgs
		{
			get
			{
				return this._instArgs;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060053D5 RID: 21461 RVA: 0x001295FE File Offset: 0x001277FE
		public object MethodSignature
		{
			get
			{
				return this._methodSignature;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060053D6 RID: 21462 RVA: 0x00129606 File Offset: 0x00127806
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060053D7 RID: 21463 RVA: 0x0012960E File Offset: 0x0012780E
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060053D8 RID: 21464 RVA: 0x00129616 File Offset: 0x00127816
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x00129624 File Offset: 0x00127824
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x04002656 RID: 9814
		private object[] _inargs;

		// Token: 0x04002657 RID: 9815
		private string _methodName;

		// Token: 0x04002658 RID: 9816
		private string _typeName;

		// Token: 0x04002659 RID: 9817
		private object _methodSignature;

		// Token: 0x0400265A RID: 9818
		private Type[] _instArgs;

		// Token: 0x0400265B RID: 9819
		private object[] _args;

		// Token: 0x0400265C RID: 9820
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x0400265D RID: 9821
		private object[] _properties;
	}
}
