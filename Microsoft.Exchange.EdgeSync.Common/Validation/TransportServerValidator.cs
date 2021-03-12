using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200004E RID: 78
	internal class TransportServerValidator : ConfigValidator
	{
		// Token: 0x0600020A RID: 522 RVA: 0x0000A0A0 File Offset: 0x000082A0
		public TransportServerValidator(ReplicationTopology topology) : base(topology, "Transport Server")
		{
			base.ConfigDirectoryPath = "CN=" + AdministrativeGroup.DefaultName + ",CN=Administrative Groups";
			base.LdapQuery = Schema.Query.QueryBridgeheads;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A0D3 File Offset: 0x000082D3
		protected override string[] PayloadAttributes
		{
			get
			{
				return Schema.Server.PayloadAttributes;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000A0DC File Offset: 0x000082DC
		protected override string[] ReadAttributes
		{
			get
			{
				return new string[]
				{
					"msExchServerSite"
				};
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A0F9 File Offset: 0x000082F9
		protected override bool CompareAttributes(ExSearchResultEntry edgeEntry, ExSearchResultEntry hubEntry, string[] copyAttributes)
		{
			return this.Filter(hubEntry) && base.CompareAttributes(edgeEntry, hubEntry, copyAttributes);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A110 File Offset: 0x00008310
		protected override bool Filter(ExSearchResultEntry entry)
		{
			if (entry.Attributes.ContainsKey("msExchServerSite"))
			{
				string text = (string)entry.Attributes["msExchServerSite"][0];
				if (!string.IsNullOrEmpty(text) && string.Compare(text, base.Topology.LocalHub.ServerSite.DistinguishedName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000153 RID: 339
		private const string ServerSite = "msExchServerSite";
	}
}
