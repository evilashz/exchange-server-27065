using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200052E RID: 1326
	internal class Warning
	{
		// Token: 0x06002F81 RID: 12161 RVA: 0x000BF354 File Offset: 0x000BD554
		internal Warning(string value)
		{
			this.warningStr = string.Format("[{1}] : {0}", value, ExDateTime.Now.ToString("HH:mm:ss.fff"));
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x000BF38A File Offset: 0x000BD58A
		internal string Message
		{
			get
			{
				return this.warningStr;
			}
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000BF392 File Offset: 0x000BD592
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x040021F4 RID: 8692
		private readonly string warningStr;
	}
}
