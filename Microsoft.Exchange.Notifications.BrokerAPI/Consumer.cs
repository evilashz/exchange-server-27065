using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000C RID: 12
	internal sealed class Consumer
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00003074 File Offset: 0x00001274
		private Consumer()
		{
			string key;
			switch (key = ApplicationName.Current.Name.ToLowerInvariant())
			{
			case "msexchangeowaapppool":
				this.ConsumerId = ConsumerId.OWA;
				return;
			case "perseusharnessruntime":
			case "perseusstudio":
			case "powershell":
			case "testconsumer":
			case "topoagent":
				this.ConsumerId = ConsumerId.Test;
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003141 File Offset: 0x00001341
		public static Consumer Current
		{
			get
			{
				return Consumer.lazy.Value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000314D File Offset: 0x0000134D
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003155 File Offset: 0x00001355
		public ConsumerId ConsumerId { get; private set; }

		// Token: 0x04000020 RID: 32
		private static readonly Lazy<Consumer> lazy = new Lazy<Consumer>(() => new Consumer());
	}
}
