using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200011F RID: 287
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class VotingInformationType
	{
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00021D77 File Offset: 0x0001FF77
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00021D7F File Offset: 0x0001FF7F
		[XmlArrayItem("VotingOptionData", IsNullable = false)]
		public VotingOptionDataType[] UserOptions
		{
			get
			{
				return this.userOptionsField;
			}
			set
			{
				this.userOptionsField = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00021D88 File Offset: 0x0001FF88
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x00021D90 File Offset: 0x0001FF90
		public string VotingResponse
		{
			get
			{
				return this.votingResponseField;
			}
			set
			{
				this.votingResponseField = value;
			}
		}

		// Token: 0x0400090A RID: 2314
		private VotingOptionDataType[] userOptionsField;

		// Token: 0x0400090B RID: 2315
		private string votingResponseField;
	}
}
