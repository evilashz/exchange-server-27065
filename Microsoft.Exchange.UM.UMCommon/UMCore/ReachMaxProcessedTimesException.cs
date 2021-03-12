using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000205 RID: 517
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ReachMaxProcessedTimesException : LocalizedException
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x000394E0 File Offset: 0x000376E0
		public ReachMaxProcessedTimesException(string argName) : base(Strings.ReachMaxProcessedTimes(argName))
		{
			this.argName = argName;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000394F5 File Offset: 0x000376F5
		public ReachMaxProcessedTimesException(string argName, Exception innerException) : base(Strings.ReachMaxProcessedTimes(argName), innerException)
		{
			this.argName = argName;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003950B File Offset: 0x0003770B
		protected ReachMaxProcessedTimesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.argName = (string)info.GetValue("argName", typeof(string));
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00039535 File Offset: 0x00037735
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("argName", this.argName);
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00039550 File Offset: 0x00037750
		public string ArgName
		{
			get
			{
				return this.argName;
			}
		}

		// Token: 0x04000882 RID: 2178
		private readonly string argName;
	}
}
