using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000B7 RID: 183
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class KeyDescriptionValue
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001F0CA File Offset: 0x0001D2CA
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0001F0D2 File Offset: 0x0001D2D2
		[XmlAttribute]
		public string KeyIdentifier
		{
			get
			{
				return this.keyIdentifierField;
			}
			set
			{
				this.keyIdentifierField = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001F0DB File Offset: 0x0001D2DB
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0001F0E3 File Offset: 0x0001D2E3
		[XmlAttribute]
		public int Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001F0EC File Offset: 0x0001D2EC
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x0001F0F4 File Offset: 0x0001D2F4
		[XmlAttribute]
		public KeyType KeyType
		{
			get
			{
				return this.keyTypeField;
			}
			set
			{
				this.keyTypeField = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001F0FD File Offset: 0x0001D2FD
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0001F105 File Offset: 0x0001D305
		[XmlAttribute]
		public KeyUsage KeyUsage
		{
			get
			{
				return this.keyUsageField;
			}
			set
			{
				this.keyUsageField = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001F10E File Offset: 0x0001D30E
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0001F116 File Offset: 0x0001D316
		[XmlAttribute]
		public DateTime StartTimestamp
		{
			get
			{
				return this.startTimestampField;
			}
			set
			{
				this.startTimestampField = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001F11F File Offset: 0x0001D31F
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0001F127 File Offset: 0x0001D327
		[XmlAttribute]
		public DateTime EndTimestamp
		{
			get
			{
				return this.endTimestampField;
			}
			set
			{
				this.endTimestampField = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001F130 File Offset: 0x0001D330
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0001F138 File Offset: 0x0001D338
		[XmlAttribute(DataType = "base64Binary")]
		public byte[] ApplicationKeyIdentifier
		{
			get
			{
				return this.applicationKeyIdentifierField;
			}
			set
			{
				this.applicationKeyIdentifierField = value;
			}
		}

		// Token: 0x04000322 RID: 802
		private string keyIdentifierField;

		// Token: 0x04000323 RID: 803
		private int versionField;

		// Token: 0x04000324 RID: 804
		private KeyType keyTypeField;

		// Token: 0x04000325 RID: 805
		private KeyUsage keyUsageField;

		// Token: 0x04000326 RID: 806
		private DateTime startTimestampField;

		// Token: 0x04000327 RID: 807
		private DateTime endTimestampField;

		// Token: 0x04000328 RID: 808
		private byte[] applicationKeyIdentifierField;
	}
}
