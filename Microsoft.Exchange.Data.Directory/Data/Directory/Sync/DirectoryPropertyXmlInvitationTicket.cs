using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000885 RID: 2181
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryPropertyXmlInvitationTicket : DirectoryPropertyXml
	{
		// Token: 0x06006D61 RID: 28001 RVA: 0x00175726 File Offset: 0x00173926
		public override IList GetValues()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006D62 RID: 28002 RVA: 0x0017572D File Offset: 0x0017392D
		public sealed override void SetValues(IList values)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17002706 RID: 9990
		// (get) Token: 0x06006D63 RID: 28003 RVA: 0x00175734 File Offset: 0x00173934
		// (set) Token: 0x06006D64 RID: 28004 RVA: 0x0017573C File Offset: 0x0017393C
		[XmlElement("Value", Order = 0)]
		public XmlValueInvitationTicket[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04004778 RID: 18296
		private XmlValueInvitationTicket[] valueField;
	}
}
