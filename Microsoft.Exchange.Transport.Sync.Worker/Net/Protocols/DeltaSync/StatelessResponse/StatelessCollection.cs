using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000194 RID: 404
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class StatelessCollection
	{
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0001DD9A File Offset: 0x0001BF9A
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x0001DDA2 File Offset: 0x0001BFA2
		public string Class
		{
			get
			{
				return this.classField;
			}
			set
			{
				this.classField = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0001DDAB File Offset: 0x0001BFAB
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0001DDB3 File Offset: 0x0001BFB3
		[XmlElement(Namespace = "HMSYNC:")]
		public int Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0001DDBC File Offset: 0x0001BFBC
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		public StatelessCollectionCommands Commands
		{
			get
			{
				return this.commandsField;
			}
			set
			{
				this.commandsField = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001DDCD File Offset: 0x0001BFCD
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0001DDD5 File Offset: 0x0001BFD5
		public StatelessCollectionResponses Responses
		{
			get
			{
				return this.responsesField;
			}
			set
			{
				this.responsesField = value;
			}
		}

		// Token: 0x04000686 RID: 1670
		private string classField;

		// Token: 0x04000687 RID: 1671
		private int statusField;

		// Token: 0x04000688 RID: 1672
		private StatelessCollectionCommands commandsField;

		// Token: 0x04000689 RID: 1673
		private StatelessCollectionResponses responsesField;
	}
}
