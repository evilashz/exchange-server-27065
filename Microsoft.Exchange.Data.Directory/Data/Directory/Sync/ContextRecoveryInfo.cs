using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008BA RID: 2234
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[Serializable]
	public class ContextRecoveryInfo
	{
		// Token: 0x17002751 RID: 10065
		// (get) Token: 0x06006E7F RID: 28287 RVA: 0x001763FF File Offset: 0x001745FF
		// (set) Token: 0x06006E80 RID: 28288 RVA: 0x00176407 File Offset: 0x00174607
		[XmlElement(DataType = "base64Binary", IsNullable = true, Order = 0)]
		public byte[] FilteredContextSyncCookie
		{
			get
			{
				return this.filteredContextSyncCookieField;
			}
			set
			{
				this.filteredContextSyncCookieField = value;
			}
		}

		// Token: 0x17002752 RID: 10066
		// (get) Token: 0x06006E81 RID: 28289 RVA: 0x00176410 File Offset: 0x00174610
		// (set) Token: 0x06006E82 RID: 28290 RVA: 0x00176418 File Offset: 0x00174618
		[XmlElement(DataType = "base64Binary", IsNullable = true, Order = 1)]
		public byte[] ContextRecoveryToken
		{
			get
			{
				return this.contextRecoveryTokenField;
			}
			set
			{
				this.contextRecoveryTokenField = value;
			}
		}

		// Token: 0x17002753 RID: 10067
		// (get) Token: 0x06006E83 RID: 28291 RVA: 0x00176421 File Offset: 0x00174621
		// (set) Token: 0x06006E84 RID: 28292 RVA: 0x00176429 File Offset: 0x00174629
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

		// Token: 0x040047DA RID: 18394
		private byte[] filteredContextSyncCookieField;

		// Token: 0x040047DB RID: 18395
		private byte[] contextRecoveryTokenField;

		// Token: 0x040047DC RID: 18396
		private int statusCodeField;
	}
}
