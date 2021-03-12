using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.ManageDelegation1
{
	// Token: 0x02000DB0 RID: 3504
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://domains.live.com/Service/ManageDelegation/V1.0")]
	[Serializable]
	public class AppIdInfo
	{
		// Token: 0x170029C2 RID: 10690
		// (get) Token: 0x06008627 RID: 34343 RVA: 0x00224F88 File Offset: 0x00223188
		// (set) Token: 0x06008628 RID: 34344 RVA: 0x00224F90 File Offset: 0x00223190
		public string AppId
		{
			get
			{
				return this.appIdField;
			}
			set
			{
				this.appIdField = value;
			}
		}

		// Token: 0x170029C3 RID: 10691
		// (get) Token: 0x06008629 RID: 34345 RVA: 0x00224F99 File Offset: 0x00223199
		// (set) Token: 0x0600862A RID: 34346 RVA: 0x00224FA1 File Offset: 0x002231A1
		public string AdminKey
		{
			get
			{
				return this.adminKeyField;
			}
			set
			{
				this.adminKeyField = value;
			}
		}

		// Token: 0x04004145 RID: 16709
		private string appIdField;

		// Token: 0x04004146 RID: 16710
		private string adminKeyField;
	}
}
