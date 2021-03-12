using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200025D RID: 605
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FindPeopleResponseMessageType : ResponseMessageType
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x00026D3F File Offset: 0x00024F3F
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x00026D47 File Offset: 0x00024F47
		[XmlArrayItem("Persona", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public PersonaType[] People
		{
			get
			{
				return this.peopleField;
			}
			set
			{
				this.peopleField = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00026D50 File Offset: 0x00024F50
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x00026D58 File Offset: 0x00024F58
		public int TotalNumberOfPeopleInView
		{
			get
			{
				return this.totalNumberOfPeopleInViewField;
			}
			set
			{
				this.totalNumberOfPeopleInViewField = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00026D61 File Offset: 0x00024F61
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x00026D69 File Offset: 0x00024F69
		[XmlIgnore]
		public bool TotalNumberOfPeopleInViewSpecified
		{
			get
			{
				return this.totalNumberOfPeopleInViewFieldSpecified;
			}
			set
			{
				this.totalNumberOfPeopleInViewFieldSpecified = value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00026D72 File Offset: 0x00024F72
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x00026D7A File Offset: 0x00024F7A
		public int FirstMatchingRowIndex
		{
			get
			{
				return this.firstMatchingRowIndexField;
			}
			set
			{
				this.firstMatchingRowIndexField = value;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00026D83 File Offset: 0x00024F83
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x00026D8B File Offset: 0x00024F8B
		[XmlIgnore]
		public bool FirstMatchingRowIndexSpecified
		{
			get
			{
				return this.firstMatchingRowIndexFieldSpecified;
			}
			set
			{
				this.firstMatchingRowIndexFieldSpecified = value;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00026D94 File Offset: 0x00024F94
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x00026D9C File Offset: 0x00024F9C
		public int FirstLoadedRowIndex
		{
			get
			{
				return this.firstLoadedRowIndexField;
			}
			set
			{
				this.firstLoadedRowIndexField = value;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00026DA5 File Offset: 0x00024FA5
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x00026DAD File Offset: 0x00024FAD
		[XmlIgnore]
		public bool FirstLoadedRowIndexSpecified
		{
			get
			{
				return this.firstLoadedRowIndexFieldSpecified;
			}
			set
			{
				this.firstLoadedRowIndexFieldSpecified = value;
			}
		}

		// Token: 0x04000F57 RID: 3927
		private PersonaType[] peopleField;

		// Token: 0x04000F58 RID: 3928
		private int totalNumberOfPeopleInViewField;

		// Token: 0x04000F59 RID: 3929
		private bool totalNumberOfPeopleInViewFieldSpecified;

		// Token: 0x04000F5A RID: 3930
		private int firstMatchingRowIndexField;

		// Token: 0x04000F5B RID: 3931
		private bool firstMatchingRowIndexFieldSpecified;

		// Token: 0x04000F5C RID: 3932
		private int firstLoadedRowIndexField;

		// Token: 0x04000F5D RID: 3933
		private bool firstLoadedRowIndexFieldSpecified;
	}
}
