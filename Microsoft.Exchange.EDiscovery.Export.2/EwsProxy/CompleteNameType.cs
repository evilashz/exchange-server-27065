using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200014F RID: 335
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CompleteNameType
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00022C23 File Offset: 0x00020E23
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00022C2B File Offset: 0x00020E2B
		public string Title
		{
			get
			{
				return this.titleField;
			}
			set
			{
				this.titleField = value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00022C34 File Offset: 0x00020E34
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00022C3C File Offset: 0x00020E3C
		public string FirstName
		{
			get
			{
				return this.firstNameField;
			}
			set
			{
				this.firstNameField = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00022C45 File Offset: 0x00020E45
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00022C4D File Offset: 0x00020E4D
		public string MiddleName
		{
			get
			{
				return this.middleNameField;
			}
			set
			{
				this.middleNameField = value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00022C56 File Offset: 0x00020E56
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00022C5E File Offset: 0x00020E5E
		public string LastName
		{
			get
			{
				return this.lastNameField;
			}
			set
			{
				this.lastNameField = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00022C67 File Offset: 0x00020E67
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x00022C6F File Offset: 0x00020E6F
		public string Suffix
		{
			get
			{
				return this.suffixField;
			}
			set
			{
				this.suffixField = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00022C78 File Offset: 0x00020E78
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x00022C80 File Offset: 0x00020E80
		public string Initials
		{
			get
			{
				return this.initialsField;
			}
			set
			{
				this.initialsField = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00022C89 File Offset: 0x00020E89
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00022C91 File Offset: 0x00020E91
		public string FullName
		{
			get
			{
				return this.fullNameField;
			}
			set
			{
				this.fullNameField = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00022C9A File Offset: 0x00020E9A
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x00022CA2 File Offset: 0x00020EA2
		public string Nickname
		{
			get
			{
				return this.nicknameField;
			}
			set
			{
				this.nicknameField = value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00022CAB File Offset: 0x00020EAB
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x00022CB3 File Offset: 0x00020EB3
		public string YomiFirstName
		{
			get
			{
				return this.yomiFirstNameField;
			}
			set
			{
				this.yomiFirstNameField = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00022CBC File Offset: 0x00020EBC
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00022CC4 File Offset: 0x00020EC4
		public string YomiLastName
		{
			get
			{
				return this.yomiLastNameField;
			}
			set
			{
				this.yomiLastNameField = value;
			}
		}

		// Token: 0x04000A2F RID: 2607
		private string titleField;

		// Token: 0x04000A30 RID: 2608
		private string firstNameField;

		// Token: 0x04000A31 RID: 2609
		private string middleNameField;

		// Token: 0x04000A32 RID: 2610
		private string lastNameField;

		// Token: 0x04000A33 RID: 2611
		private string suffixField;

		// Token: 0x04000A34 RID: 2612
		private string initialsField;

		// Token: 0x04000A35 RID: 2613
		private string fullNameField;

		// Token: 0x04000A36 RID: 2614
		private string nicknameField;

		// Token: 0x04000A37 RID: 2615
		private string yomiFirstNameField;

		// Token: 0x04000A38 RID: 2616
		private string yomiLastNameField;
	}
}
