using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200017C RID: 380
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OlcConfigSchema : ConfigSchemaBase
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00020C44 File Offset: 0x0001EE44
		public override string Name
		{
			get
			{
				return "Olc";
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00020C4B File Offset: 0x0001EE4B
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x00020C58 File Offset: 0x0001EE58
		[ConfigurationProperty("OlcTopology", DefaultValue = null)]
		internal string OlcTopology
		{
			get
			{
				return this.InternalGetConfig<string>("OlcTopology");
			}
			set
			{
				this.InternalSetConfig<string>(value, "OlcTopology");
			}
		}
	}
}
