using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EAD RID: 3757
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MRSDiagnosticXmlParsingException : MRSDiagnosticQueryException
	{
		// Token: 0x0600A834 RID: 43060 RVA: 0x0028996C File Offset: 0x00287B6C
		public MRSDiagnosticXmlParsingException(string error, string xml) : base(Strings.MRSDiagnosticXmlParsingError(error, xml))
		{
			this.error = error;
			this.xml = xml;
		}

		// Token: 0x0600A835 RID: 43061 RVA: 0x00289989 File Offset: 0x00287B89
		public MRSDiagnosticXmlParsingException(string error, string xml, Exception innerException) : base(Strings.MRSDiagnosticXmlParsingError(error, xml), innerException)
		{
			this.error = error;
			this.xml = xml;
		}

		// Token: 0x0600A836 RID: 43062 RVA: 0x002899A8 File Offset: 0x00287BA8
		protected MRSDiagnosticXmlParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
			this.xml = (string)info.GetValue("xml", typeof(string));
		}

		// Token: 0x0600A837 RID: 43063 RVA: 0x002899FD File Offset: 0x00287BFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
			info.AddValue("xml", this.xml);
		}

		// Token: 0x1700369D RID: 13981
		// (get) Token: 0x0600A838 RID: 43064 RVA: 0x00289A29 File Offset: 0x00287C29
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x1700369E RID: 13982
		// (get) Token: 0x0600A839 RID: 43065 RVA: 0x00289A31 File Offset: 0x00287C31
		public string Xml
		{
			get
			{
				return this.xml;
			}
		}

		// Token: 0x04006003 RID: 24579
		private readonly string error;

		// Token: 0x04006004 RID: 24580
		private readonly string xml;
	}
}
