using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x0200073B RID: 1851
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "KeyInformation", Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04")]
	public class KeyInformation : IExtensibleDataObject
	{
		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x0010C8EC File Offset: 0x0010AAEC
		// (set) Token: 0x0600419D RID: 16797 RVA: 0x0010C8F4 File Offset: 0x0010AAF4
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x0010C8FD File Offset: 0x0010AAFD
		// (set) Token: 0x0600419F RID: 16799 RVA: 0x0010C905 File Offset: 0x0010AB05
		[DataMember]
		public string strID
		{
			get
			{
				return this.strIDField;
			}
			set
			{
				this.strIDField = value;
			}
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x0010C90E File Offset: 0x0010AB0E
		// (set) Token: 0x060041A1 RID: 16801 RVA: 0x0010C916 File Offset: 0x0010AB16
		[DataMember]
		public string strIDType
		{
			get
			{
				return this.strIDTypeField;
			}
			set
			{
				this.strIDTypeField = value;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x0010C91F File Offset: 0x0010AB1F
		// (set) Token: 0x060041A3 RID: 16803 RVA: 0x0010C927 File Offset: 0x0010AB27
		[DataMember(Order = 2)]
		public int nCSPType
		{
			get
			{
				return this.nCSPTypeField;
			}
			set
			{
				this.nCSPTypeField = value;
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x060041A4 RID: 16804 RVA: 0x0010C930 File Offset: 0x0010AB30
		// (set) Token: 0x060041A5 RID: 16805 RVA: 0x0010C938 File Offset: 0x0010AB38
		[DataMember(Order = 3)]
		public string strCSPName
		{
			get
			{
				return this.strCSPNameField;
			}
			set
			{
				this.strCSPNameField = value;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x0010C941 File Offset: 0x0010AB41
		// (set) Token: 0x060041A7 RID: 16807 RVA: 0x0010C949 File Offset: 0x0010AB49
		[DataMember(Order = 4)]
		public string strKeyContainerName
		{
			get
			{
				return this.strKeyContainerNameField;
			}
			set
			{
				this.strKeyContainerNameField = value;
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x0010C952 File Offset: 0x0010AB52
		// (set) Token: 0x060041A9 RID: 16809 RVA: 0x0010C95A File Offset: 0x0010AB5A
		[DataMember(Order = 5)]
		public int nKeyNumber
		{
			get
			{
				return this.nKeyNumberField;
			}
			set
			{
				this.nKeyNumberField = value;
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x0010C963 File Offset: 0x0010AB63
		// (set) Token: 0x060041AB RID: 16811 RVA: 0x0010C96B File Offset: 0x0010AB6B
		[DataMember(Order = 6)]
		public string strEncryptedPrivateKey
		{
			get
			{
				return this.strEncryptedPrivateKeyField;
			}
			set
			{
				this.strEncryptedPrivateKeyField = value;
			}
		}

		// Token: 0x04002963 RID: 10595
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002964 RID: 10596
		private string strIDField;

		// Token: 0x04002965 RID: 10597
		private string strIDTypeField;

		// Token: 0x04002966 RID: 10598
		private int nCSPTypeField;

		// Token: 0x04002967 RID: 10599
		private string strCSPNameField;

		// Token: 0x04002968 RID: 10600
		private string strKeyContainerNameField;

		// Token: 0x04002969 RID: 10601
		private int nKeyNumberField;

		// Token: 0x0400296A RID: 10602
		private string strEncryptedPrivateKeyField;
	}
}
