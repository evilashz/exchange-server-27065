using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200077A RID: 1914
	[Serializable]
	internal class BinaryMethodReturnMessage
	{
		// Token: 0x060053DA RID: 21466 RVA: 0x00129664 File Offset: 0x00127864
		[SecurityCritical]
		internal BinaryMethodReturnMessage(object returnValue, object[] args, Exception e, LogicalCallContext callContext, object[] properties)
		{
			this._returnValue = returnValue;
			if (args == null)
			{
				args = new object[0];
			}
			this._outargs = args;
			this._args = args;
			this._exception = e;
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

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x001296BF File Offset: 0x001278BF
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060053DC RID: 21468 RVA: 0x001296C7 File Offset: 0x001278C7
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060053DD RID: 21469 RVA: 0x001296CF File Offset: 0x001278CF
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060053DE RID: 21470 RVA: 0x001296D7 File Offset: 0x001278D7
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._logicalCallContext;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060053DF RID: 21471 RVA: 0x001296DF File Offset: 0x001278DF
		public bool HasProperties
		{
			get
			{
				return this._properties != null;
			}
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x001296EC File Offset: 0x001278EC
		internal void PopulateMessageProperties(IDictionary dict)
		{
			foreach (DictionaryEntry dictionaryEntry in this._properties)
			{
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x0400265E RID: 9822
		private object[] _outargs;

		// Token: 0x0400265F RID: 9823
		private Exception _exception;

		// Token: 0x04002660 RID: 9824
		private object _returnValue;

		// Token: 0x04002661 RID: 9825
		private object[] _args;

		// Token: 0x04002662 RID: 9826
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x04002663 RID: 9827
		private object[] _properties;
	}
}
