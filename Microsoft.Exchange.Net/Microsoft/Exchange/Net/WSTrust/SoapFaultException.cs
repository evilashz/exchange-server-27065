using System;
using System.Xml;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B60 RID: 2912
	internal class SoapFaultException : WSTrustException
	{
		// Token: 0x06003E6E RID: 15982 RVA: 0x000A2F73 File Offset: 0x000A1173
		public SoapFaultException(XmlElement fault, string code, string subCode) : base(WSTrustStrings.SoapFaultException)
		{
			this.fault = fault;
			this.code = code;
			this.subCode = subCode;
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x000A2F95 File Offset: 0x000A1195
		public XmlElement Fault
		{
			get
			{
				return this.fault;
			}
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x000A2F9D File Offset: 0x000A119D
		public string Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06003E71 RID: 15985 RVA: 0x000A2FA5 File Offset: 0x000A11A5
		public string SubCode
		{
			get
			{
				return this.subCode;
			}
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x000A2FB0 File Offset: 0x000A11B0
		public override string ToString()
		{
			if (" Code=" + this.code == null)
			{
				return "<null>";
			}
			if (this.code + " SubCode=" + this.subCode == null)
			{
				return "<null>";
			}
			if (this.subCode + " Fault=" + this.fault != null)
			{
				return this.fault.OuterXml + Environment.NewLine + base.ToString();
			}
			return "<null>";
		}

		// Token: 0x04003662 RID: 13922
		private XmlElement fault;

		// Token: 0x04003663 RID: 13923
		private string code;

		// Token: 0x04003664 RID: 13924
		private string subCode;
	}
}
