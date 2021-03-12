using System;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200013B RID: 315
	public class ExchangeDynamicServerSettings : ExchangeSettings
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x0002C98A File Offset: 0x0002AB8A
		public ExchangeDynamicServerSettings(IComponent owner) : base(owner)
		{
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0002C993 File Offset: 0x0002AB93
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0002C9A5 File Offset: 0x0002ABA5
		[UserScopedSetting]
		public Fqdn RemotePSServer
		{
			get
			{
				return (Fqdn)this["RemotePSServer"];
			}
			set
			{
				this["RemotePSServer"] = value;
			}
		}
	}
}
