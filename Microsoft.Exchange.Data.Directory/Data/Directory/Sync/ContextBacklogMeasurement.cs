using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B8 RID: 2232
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class ContextBacklogMeasurement
	{
		// Token: 0x1700274A RID: 10058
		// (get) Token: 0x06006E6F RID: 28271 RVA: 0x00176378 File Offset: 0x00174578
		// (set) Token: 0x06006E70 RID: 28272 RVA: 0x00176380 File Offset: 0x00174580
		[XmlAttribute]
		public string ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x1700274B RID: 10059
		// (get) Token: 0x06006E71 RID: 28273 RVA: 0x00176389 File Offset: 0x00174589
		// (set) Token: 0x06006E72 RID: 28274 RVA: 0x00176391 File Offset: 0x00174591
		[XmlAttribute]
		public uint Objects
		{
			get
			{
				return this.objectsField;
			}
			set
			{
				this.objectsField = value;
			}
		}

		// Token: 0x1700274C RID: 10060
		// (get) Token: 0x06006E73 RID: 28275 RVA: 0x0017639A File Offset: 0x0017459A
		// (set) Token: 0x06006E74 RID: 28276 RVA: 0x001763A2 File Offset: 0x001745A2
		[XmlAttribute]
		public uint Links
		{
			get
			{
				return this.linksField;
			}
			set
			{
				this.linksField = value;
			}
		}

		// Token: 0x1700274D RID: 10061
		// (get) Token: 0x06006E75 RID: 28277 RVA: 0x001763AB File Offset: 0x001745AB
		// (set) Token: 0x06006E76 RID: 28278 RVA: 0x001763B3 File Offset: 0x001745B3
		[XmlAttribute]
		public int StreamCode
		{
			get
			{
				return this.streamCodeField;
			}
			set
			{
				this.streamCodeField = value;
			}
		}

		// Token: 0x040047D3 RID: 18387
		private string contextIdField;

		// Token: 0x040047D4 RID: 18388
		private uint objectsField;

		// Token: 0x040047D5 RID: 18389
		private uint linksField;

		// Token: 0x040047D6 RID: 18390
		private int streamCodeField;
	}
}
