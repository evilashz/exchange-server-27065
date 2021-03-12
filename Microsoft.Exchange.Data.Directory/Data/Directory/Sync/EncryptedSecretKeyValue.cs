using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000920 RID: 2336
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class EncryptedSecretKeyValue
	{
		// Token: 0x17002799 RID: 10137
		// (get) Token: 0x06006F6E RID: 28526 RVA: 0x00176BCD File Offset: 0x00174DCD
		// (set) Token: 0x06006F6F RID: 28527 RVA: 0x00176BD5 File Offset: 0x00174DD5
		[XmlElement(DataType = "base64Binary", Order = 0)]
		public byte[] EncryptedSecret
		{
			get
			{
				return this.encryptedSecretField;
			}
			set
			{
				this.encryptedSecretField = value;
			}
		}

		// Token: 0x1700279A RID: 10138
		// (get) Token: 0x06006F70 RID: 28528 RVA: 0x00176BDE File Offset: 0x00174DDE
		// (set) Token: 0x06006F71 RID: 28529 RVA: 0x00176BE6 File Offset: 0x00174DE6
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

		// Token: 0x1700279B RID: 10139
		// (get) Token: 0x06006F72 RID: 28530 RVA: 0x00176BEF File Offset: 0x00174DEF
		// (set) Token: 0x06006F73 RID: 28531 RVA: 0x00176BF7 File Offset: 0x00174DF7
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

		// Token: 0x1700279C RID: 10140
		// (get) Token: 0x06006F74 RID: 28532 RVA: 0x00176C00 File Offset: 0x00174E00
		// (set) Token: 0x06006F75 RID: 28533 RVA: 0x00176C08 File Offset: 0x00174E08
		[XmlAttribute]
		public SecretKeyType SecretKeyType
		{
			get
			{
				return this.secretKeyTypeField;
			}
			set
			{
				this.secretKeyTypeField = value;
			}
		}

		// Token: 0x04004849 RID: 18505
		private byte[] encryptedSecretField;

		// Token: 0x0400484A RID: 18506
		private string keyIdentifierField;

		// Token: 0x0400484B RID: 18507
		private int versionField;

		// Token: 0x0400484C RID: 18508
		private SecretKeyType secretKeyTypeField;
	}
}
