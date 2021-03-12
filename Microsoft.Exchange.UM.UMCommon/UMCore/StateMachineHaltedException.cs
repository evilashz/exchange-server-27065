using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001ED RID: 493
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class StateMachineHaltedException : LocalizedException
	{
		// Token: 0x06001067 RID: 4199 RVA: 0x00038B95 File Offset: 0x00036D95
		public StateMachineHaltedException(string id) : base(Strings.StateMachineHalted(id))
		{
			this.id = id;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00038BAA File Offset: 0x00036DAA
		public StateMachineHaltedException(string id, Exception innerException) : base(Strings.StateMachineHalted(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00038BC0 File Offset: 0x00036DC0
		protected StateMachineHaltedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00038BEA File Offset: 0x00036DEA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00038C05 File Offset: 0x00036E05
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000871 RID: 2161
		private readonly string id;
	}
}
