using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000CB RID: 203
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmServerException : HaRpcServerBaseException
	{
		// Token: 0x06001273 RID: 4723 RVA: 0x00067AC4 File Offset: 0x00065CC4
		public AmServerException(string errorMessage) : base(ServerStrings.AmServerException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00067ADE File Offset: 0x00065CDE
		public AmServerException(string errorMessage, Exception innerException) : base(ServerStrings.AmServerException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00067AF9 File Offset: 0x00065CF9
		protected AmServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00067B03 File Offset: 0x00065D03
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00067B10 File Offset: 0x00065D10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
