using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B9 RID: 2233
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class BacklogEstimateBatch
	{
		// Token: 0x1700274E RID: 10062
		// (get) Token: 0x06006E78 RID: 28280 RVA: 0x001763C4 File Offset: 0x001745C4
		// (set) Token: 0x06006E79 RID: 28281 RVA: 0x001763CC File Offset: 0x001745CC
		[XmlArray(IsNullable = true, Order = 0)]
		[XmlArrayItem(IsNullable = false)]
		public ContextBacklogMeasurement[] ContextBacklogs
		{
			get
			{
				return this.contextBacklogsField;
			}
			set
			{
				this.contextBacklogsField = value;
			}
		}

		// Token: 0x1700274F RID: 10063
		// (get) Token: 0x06006E7A RID: 28282 RVA: 0x001763D5 File Offset: 0x001745D5
		// (set) Token: 0x06006E7B RID: 28283 RVA: 0x001763DD File Offset: 0x001745DD
		[XmlElement(DataType = "base64Binary", IsNullable = true, Order = 1)]
		public byte[] NextPageToken
		{
			get
			{
				return this.nextPageTokenField;
			}
			set
			{
				this.nextPageTokenField = value;
			}
		}

		// Token: 0x17002750 RID: 10064
		// (get) Token: 0x06006E7C RID: 28284 RVA: 0x001763E6 File Offset: 0x001745E6
		// (set) Token: 0x06006E7D RID: 28285 RVA: 0x001763EE File Offset: 0x001745EE
		[XmlAttribute]
		public int StatusCode
		{
			get
			{
				return this.statusCodeField;
			}
			set
			{
				this.statusCodeField = value;
			}
		}

		// Token: 0x040047D7 RID: 18391
		private ContextBacklogMeasurement[] contextBacklogsField;

		// Token: 0x040047D8 RID: 18392
		private byte[] nextPageTokenField;

		// Token: 0x040047D9 RID: 18393
		private int statusCodeField;
	}
}
