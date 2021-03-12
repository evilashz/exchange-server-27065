using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F5 RID: 501
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidPerfCounterException : LocalizedException
	{
		// Token: 0x06001091 RID: 4241 RVA: 0x00038FF9 File Offset: 0x000371F9
		public InvalidPerfCounterException(string counterName) : base(Strings.InvalidPerfCounterException(counterName))
		{
			this.counterName = counterName;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003900E File Offset: 0x0003720E
		public InvalidPerfCounterException(string counterName, Exception innerException) : base(Strings.InvalidPerfCounterException(counterName), innerException)
		{
			this.counterName = counterName;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00039024 File Offset: 0x00037224
		protected InvalidPerfCounterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.counterName = (string)info.GetValue("counterName", typeof(string));
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0003904E File Offset: 0x0003724E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("counterName", this.counterName);
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00039069 File Offset: 0x00037269
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
		}

		// Token: 0x0400087B RID: 2171
		private readonly string counterName;
	}
}
