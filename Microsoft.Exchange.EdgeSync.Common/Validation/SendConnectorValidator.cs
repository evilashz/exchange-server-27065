using System;
using System.DirectoryServices.Protocols;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200004C RID: 76
	internal class SendConnectorValidator : ConfigValidator
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00009E03 File Offset: 0x00008003
		public SendConnectorValidator(ReplicationTopology topology) : base(topology, "Send Connector")
		{
			base.ConfigDirectoryPath = "CN=" + AdministrativeGroup.DefaultName + ",CN=Administrative Groups";
			base.LdapQuery = Schema.Query.QuerySendConnectors;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009E36 File Offset: 0x00008036
		protected override string[] PayloadAttributes
		{
			get
			{
				return Schema.SendConnector.PayloadAttributes;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009E40 File Offset: 0x00008040
		protected override string[] ReadAttributes
		{
			get
			{
				return new string[]
				{
					"msExchSourceBridgeheadServersDN"
				};
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009E5D File Offset: 0x0000805D
		protected override bool CompareAttributes(ExSearchResultEntry edgeEntry, ExSearchResultEntry hubEntry, string[] copyAttributes)
		{
			return this.Filter(hubEntry) && base.CompareAttributes(edgeEntry, hubEntry, copyAttributes);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009E74 File Offset: 0x00008074
		protected override bool Filter(ExSearchResultEntry entry)
		{
			if (entry.Attributes.ContainsKey("msExchSourceBridgeheadServersDN"))
			{
				DirectoryAttribute directoryAttribute = entry.Attributes["msExchSourceBridgeheadServersDN"];
				string value = string.Format("cn={0},", base.CurrentEdgeConnection.EdgeServer.Name.ToLower());
				foreach (object obj in directoryAttribute)
				{
					byte[] bytes = (byte[])obj;
					string @string = Encoding.UTF8.GetString(bytes);
					if (@string.ToLowerInvariant().Contains(value))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}
	}
}
