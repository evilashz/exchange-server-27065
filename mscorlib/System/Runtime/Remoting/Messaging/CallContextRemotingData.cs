using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000863 RID: 2147
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06005BC3 RID: 23491 RVA: 0x00140E50 File Offset: 0x0013F050
		// (set) Token: 0x06005BC4 RID: 23492 RVA: 0x00140E58 File Offset: 0x0013F058
		internal string LogicalCallID
		{
			get
			{
				return this._logicalCallID;
			}
			set
			{
				this._logicalCallID = value;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06005BC5 RID: 23493 RVA: 0x00140E61 File Offset: 0x0013F061
		internal bool HasInfo
		{
			get
			{
				return this._logicalCallID != null;
			}
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x00140E6C File Offset: 0x0013F06C
		public object Clone()
		{
			return new CallContextRemotingData
			{
				LogicalCallID = this.LogicalCallID
			};
		}

		// Token: 0x04002928 RID: 10536
		private string _logicalCallID;
	}
}
