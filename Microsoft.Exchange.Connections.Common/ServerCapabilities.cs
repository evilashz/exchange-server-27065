using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ServerCapabilities : IServerCapabilities
	{
		// Token: 0x0600006E RID: 110 RVA: 0x000027FF File Offset: 0x000009FF
		internal ServerCapabilities()
		{
			this.Capabilities = new List<string>();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002815 File Offset: 0x00000A15
		internal ServerCapabilities(IEnumerable<string> capabilities)
		{
			this.Capabilities = from s in capabilities
			select s;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002846 File Offset: 0x00000A46
		internal ServerCapabilities(IServerCapabilities capabilities)
		{
			this.Capabilities = new List<string>(capabilities.Capabilities);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000285F File Offset: 0x00000A5F
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002867 File Offset: 0x00000A67
		public IEnumerable<string> Capabilities { get; private set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00002870 File Offset: 0x00000A70
		public IServerCapabilities Add(string capability)
		{
			((IList<string>)this.Capabilities).Add(capability);
			return this;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002884 File Offset: 0x00000A84
		public IServerCapabilities Remove(string capability)
		{
			if (!this.Capabilities.Contains(capability, StringComparer.OrdinalIgnoreCase))
			{
				throw new MissingCapabilitiesException(capability);
			}
			((IList<string>)this.Capabilities).Remove(capability);
			return this;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000028B3 File Offset: 0x00000AB3
		public bool Supports(string capability)
		{
			return this.Capabilities.Contains(capability, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000028C8 File Offset: 0x00000AC8
		public bool Supports(IEnumerable<string> desiredCapabilitiesList)
		{
			ServerCapabilities desiredCapabilities = new ServerCapabilities(desiredCapabilitiesList);
			return this.Supports(desiredCapabilities);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000028E4 File Offset: 0x00000AE4
		public bool Supports(IServerCapabilities desiredCapabilities)
		{
			IEnumerable<string> source = desiredCapabilities.NotIn(this);
			return !source.Any<string>();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002904 File Offset: 0x00000B04
		public IEnumerable<string> NotIn(IServerCapabilities desiredCapabilities)
		{
			return this.Capabilities.Except(desiredCapabilities.Capabilities, StringComparer.OrdinalIgnoreCase);
		}
	}
}
