using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000E2 RID: 226
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmServerTransientException : HaRpcServerTransientBaseException
	{
		// Token: 0x060012F2 RID: 4850 RVA: 0x0006875A File Offset: 0x0006695A
		public AmServerTransientException(string errorMessage) : base(ServerStrings.AmServerTransientException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00068774 File Offset: 0x00066974
		public AmServerTransientException(string errorMessage, Exception innerException) : base(ServerStrings.AmServerTransientException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0006878F File Offset: 0x0006698F
		protected AmServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00068799 File Offset: 0x00066999
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000687A6 File Offset: 0x000669A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
