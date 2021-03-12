using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000258 RID: 600
	[ImmutableObject(true)]
	public abstract class Protocol
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x0003FC0E File Offset: 0x0003DE0E
		protected Protocol(string protocolName, string displayName)
		{
			if (protocolName == null)
			{
				throw new ArgumentNullException("protocolName");
			}
			if (protocolName.IndexOf(':') != -1)
			{
				throw new ArgumentException(DataStrings.ExceptionInvlidCharInProtocolName);
			}
			this.protocolName = protocolName;
			this.displayName = displayName;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0003FC4D File Offset: 0x0003DE4D
		public string ProtocolName
		{
			get
			{
				return this.protocolName;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0003FC55 File Offset: 0x0003DE55
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0003FC5D File Offset: 0x0003DE5D
		public sealed override string ToString()
		{
			return this.protocolName;
		}

		// Token: 0x04000BEC RID: 3052
		private string protocolName;

		// Token: 0x04000BED RID: 3053
		private string displayName;
	}
}
