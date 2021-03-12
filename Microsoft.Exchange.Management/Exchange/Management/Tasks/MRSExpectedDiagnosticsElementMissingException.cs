using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EAE RID: 3758
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MRSExpectedDiagnosticsElementMissingException : MRSDiagnosticQueryException
	{
		// Token: 0x0600A83A RID: 43066 RVA: 0x00289A39 File Offset: 0x00287C39
		public MRSExpectedDiagnosticsElementMissingException(string xPath, string xml) : base(Strings.MRSExpectedDiagnosticsElementMissing(xPath, xml))
		{
			this.xPath = xPath;
			this.xml = xml;
		}

		// Token: 0x0600A83B RID: 43067 RVA: 0x00289A56 File Offset: 0x00287C56
		public MRSExpectedDiagnosticsElementMissingException(string xPath, string xml, Exception innerException) : base(Strings.MRSExpectedDiagnosticsElementMissing(xPath, xml), innerException)
		{
			this.xPath = xPath;
			this.xml = xml;
		}

		// Token: 0x0600A83C RID: 43068 RVA: 0x00289A74 File Offset: 0x00287C74
		protected MRSExpectedDiagnosticsElementMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.xPath = (string)info.GetValue("xPath", typeof(string));
			this.xml = (string)info.GetValue("xml", typeof(string));
		}

		// Token: 0x0600A83D RID: 43069 RVA: 0x00289AC9 File Offset: 0x00287CC9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("xPath", this.xPath);
			info.AddValue("xml", this.xml);
		}

		// Token: 0x1700369F RID: 13983
		// (get) Token: 0x0600A83E RID: 43070 RVA: 0x00289AF5 File Offset: 0x00287CF5
		public string XPath
		{
			get
			{
				return this.xPath;
			}
		}

		// Token: 0x170036A0 RID: 13984
		// (get) Token: 0x0600A83F RID: 43071 RVA: 0x00289AFD File Offset: 0x00287CFD
		public string Xml
		{
			get
			{
				return this.xml;
			}
		}

		// Token: 0x04006005 RID: 24581
		private readonly string xPath;

		// Token: 0x04006006 RID: 24582
		private readonly string xml;
	}
}
