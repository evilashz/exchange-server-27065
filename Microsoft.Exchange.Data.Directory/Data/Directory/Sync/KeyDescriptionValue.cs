using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000923 RID: 2339
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class KeyDescriptionValue
	{
		// Token: 0x1700279E RID: 10142
		// (get) Token: 0x06006F7A RID: 28538 RVA: 0x00176C32 File Offset: 0x00174E32
		// (set) Token: 0x06006F7B RID: 28539 RVA: 0x00176C3A File Offset: 0x00174E3A
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

		// Token: 0x1700279F RID: 10143
		// (get) Token: 0x06006F7C RID: 28540 RVA: 0x00176C43 File Offset: 0x00174E43
		// (set) Token: 0x06006F7D RID: 28541 RVA: 0x00176C4B File Offset: 0x00174E4B
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

		// Token: 0x170027A0 RID: 10144
		// (get) Token: 0x06006F7E RID: 28542 RVA: 0x00176C54 File Offset: 0x00174E54
		// (set) Token: 0x06006F7F RID: 28543 RVA: 0x00176C5C File Offset: 0x00174E5C
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

		// Token: 0x170027A1 RID: 10145
		// (get) Token: 0x06006F80 RID: 28544 RVA: 0x00176C65 File Offset: 0x00174E65
		// (set) Token: 0x06006F81 RID: 28545 RVA: 0x00176C6D File Offset: 0x00174E6D
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

		// Token: 0x170027A2 RID: 10146
		// (get) Token: 0x06006F82 RID: 28546 RVA: 0x00176C76 File Offset: 0x00174E76
		// (set) Token: 0x06006F83 RID: 28547 RVA: 0x00176C7E File Offset: 0x00174E7E
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

		// Token: 0x170027A3 RID: 10147
		// (get) Token: 0x06006F84 RID: 28548 RVA: 0x00176C87 File Offset: 0x00174E87
		// (set) Token: 0x06006F85 RID: 28549 RVA: 0x00176C8F File Offset: 0x00174E8F
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

		// Token: 0x170027A4 RID: 10148
		// (get) Token: 0x06006F86 RID: 28550 RVA: 0x00176C98 File Offset: 0x00174E98
		// (set) Token: 0x06006F87 RID: 28551 RVA: 0x00176CA0 File Offset: 0x00174EA0
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

		// Token: 0x170027A5 RID: 10149
		// (get) Token: 0x06006F88 RID: 28552 RVA: 0x00176CA9 File Offset: 0x00174EA9
		// (set) Token: 0x06006F89 RID: 28553 RVA: 0x00176CB1 File Offset: 0x00174EB1
		[XmlAttribute]
		public bool IsPrimary
		{
			get
			{
				return this.isPrimaryField;
			}
			set
			{
				this.isPrimaryField = value;
			}
		}

		// Token: 0x170027A6 RID: 10150
		// (get) Token: 0x06006F8A RID: 28554 RVA: 0x00176CBA File Offset: 0x00174EBA
		// (set) Token: 0x06006F8B RID: 28555 RVA: 0x00176CC2 File Offset: 0x00174EC2
		[XmlIgnore]
		public bool IsPrimarySpecified
		{
			get
			{
				return this.isPrimaryFieldSpecified;
			}
			set
			{
				this.isPrimaryFieldSpecified = value;
			}
		}

		// Token: 0x04004852 RID: 18514
		private string keyIdentifierField;

		// Token: 0x04004853 RID: 18515
		private int versionField;

		// Token: 0x04004854 RID: 18516
		private KeyType keyTypeField;

		// Token: 0x04004855 RID: 18517
		private KeyUsage keyUsageField;

		// Token: 0x04004856 RID: 18518
		private DateTime startTimestampField;

		// Token: 0x04004857 RID: 18519
		private DateTime endTimestampField;

		// Token: 0x04004858 RID: 18520
		private byte[] applicationKeyIdentifierField;

		// Token: 0x04004859 RID: 18521
		private bool isPrimaryField;

		// Token: 0x0400485A RID: 18522
		private bool isPrimaryFieldSpecified;
	}
}
