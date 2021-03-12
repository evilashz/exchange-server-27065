using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000240 RID: 576
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class ChangeHighlightsType
	{
		// Token: 0x04000F35 RID: 3893
		public bool HasLocationChanged;

		// Token: 0x04000F36 RID: 3894
		[XmlIgnore]
		public bool HasLocationChangedSpecified;

		// Token: 0x04000F37 RID: 3895
		public string Location;

		// Token: 0x04000F38 RID: 3896
		public bool HasStartTimeChanged;

		// Token: 0x04000F39 RID: 3897
		[XmlIgnore]
		public bool HasStartTimeChangedSpecified;

		// Token: 0x04000F3A RID: 3898
		public DateTime Start;

		// Token: 0x04000F3B RID: 3899
		[XmlIgnore]
		public bool StartSpecified;

		// Token: 0x04000F3C RID: 3900
		public bool HasEndTimeChanged;

		// Token: 0x04000F3D RID: 3901
		[XmlIgnore]
		public bool HasEndTimeChangedSpecified;

		// Token: 0x04000F3E RID: 3902
		public DateTime End;

		// Token: 0x04000F3F RID: 3903
		[XmlIgnore]
		public bool EndSpecified;
	}
}
