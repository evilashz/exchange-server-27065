using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003AB RID: 939
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TPREnabledInvalidOperationException : TransientException
	{
		// Token: 0x060027A8 RID: 10152 RVA: 0x000B657A File Offset: 0x000B477A
		public TPREnabledInvalidOperationException(string operationName) : base(ReplayStrings.TPREnabledInvalidOperation(operationName))
		{
			this.operationName = operationName;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000B658F File Offset: 0x000B478F
		public TPREnabledInvalidOperationException(string operationName, Exception innerException) : base(ReplayStrings.TPREnabledInvalidOperation(operationName), innerException)
		{
			this.operationName = operationName;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000B65A5 File Offset: 0x000B47A5
		protected TPREnabledInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationName = (string)info.GetValue("operationName", typeof(string));
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000B65CF File Offset: 0x000B47CF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationName", this.operationName);
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x000B65EA File Offset: 0x000B47EA
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x040013A3 RID: 5027
		private readonly string operationName;
	}
}
