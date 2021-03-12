using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002BE RID: 702
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class NonEmptyStateDefinitionType
	{
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x00027A9D File Offset: 0x00025C9D
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x00027AA5 File Offset: 0x00025CA5
		[XmlElement("LocationBasedStateDefinition", typeof(LocationBasedStateDefinitionType))]
		[XmlElement("DeleteFromFolderStateDefinition", typeof(DeleteFromFolderStateDefinitionType))]
		[XmlElement("DeletedOccurrenceStateDefinition", typeof(DeletedOccurrenceStateDefinitionType))]
		public BaseCalendarItemStateDefinitionType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x04001053 RID: 4179
		private BaseCalendarItemStateDefinitionType itemField;
	}
}
