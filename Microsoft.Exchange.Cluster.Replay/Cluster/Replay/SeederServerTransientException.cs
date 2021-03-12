using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002AD RID: 685
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederServerTransientException : HaRpcServerTransientBaseException
	{
		// Token: 0x06001AD8 RID: 6872 RVA: 0x000737C0 File Offset: 0x000719C0
		public SeederServerTransientException(string errorMessage) : base(ReplayStrings.SeederServerTransientException(errorMessage))
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000737DA File Offset: 0x000719DA
		public SeederServerTransientException(string errorMessage, Exception innerException) : base(ReplayStrings.SeederServerTransientException(errorMessage), innerException)
		{
			this.m_exceptionInfo.ErrorMessage = errorMessage;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000737F5 File Offset: 0x000719F5
		protected SeederServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x000737FF File Offset: 0x000719FF
		public override string ErrorMessage
		{
			get
			{
				return this.m_exceptionInfo.ErrorMessage;
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0007380C File Offset: 0x00071A0C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
