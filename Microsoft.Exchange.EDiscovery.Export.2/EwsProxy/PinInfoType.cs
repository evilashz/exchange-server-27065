using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000BD RID: 189
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PinInfoType
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0001FEE2 File Offset: 0x0001E0E2
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x0001FEEA File Offset: 0x0001E0EA
		public string PIN
		{
			get
			{
				return this.pINField;
			}
			set
			{
				this.pINField = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0001FEF3 File Offset: 0x0001E0F3
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x0001FEFB File Offset: 0x0001E0FB
		public bool IsValid
		{
			get
			{
				return this.isValidField;
			}
			set
			{
				this.isValidField = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001FF04 File Offset: 0x0001E104
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0001FF0C File Offset: 0x0001E10C
		public bool PinExpired
		{
			get
			{
				return this.pinExpiredField;
			}
			set
			{
				this.pinExpiredField = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0001FF15 File Offset: 0x0001E115
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x0001FF1D File Offset: 0x0001E11D
		public bool LockedOut
		{
			get
			{
				return this.lockedOutField;
			}
			set
			{
				this.lockedOutField = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001FF26 File Offset: 0x0001E126
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x0001FF2E File Offset: 0x0001E12E
		public bool FirstTimeUser
		{
			get
			{
				return this.firstTimeUserField;
			}
			set
			{
				this.firstTimeUserField = value;
			}
		}

		// Token: 0x04000570 RID: 1392
		private string pINField;

		// Token: 0x04000571 RID: 1393
		private bool isValidField;

		// Token: 0x04000572 RID: 1394
		private bool pinExpiredField;

		// Token: 0x04000573 RID: 1395
		private bool lockedOutField;

		// Token: 0x04000574 RID: 1396
		private bool firstTimeUserField;
	}
}
