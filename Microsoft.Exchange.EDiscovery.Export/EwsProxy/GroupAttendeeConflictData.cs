using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000271 RID: 625
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GroupAttendeeConflictData : AttendeeConflictData
	{
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00027495 File Offset: 0x00025695
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x0002749D File Offset: 0x0002569D
		public int NumberOfMembers
		{
			get
			{
				return this.numberOfMembersField;
			}
			set
			{
				this.numberOfMembersField = value;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x000274A6 File Offset: 0x000256A6
		// (set) Token: 0x06001754 RID: 5972 RVA: 0x000274AE File Offset: 0x000256AE
		public int NumberOfMembersAvailable
		{
			get
			{
				return this.numberOfMembersAvailableField;
			}
			set
			{
				this.numberOfMembersAvailableField = value;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x000274B7 File Offset: 0x000256B7
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x000274BF File Offset: 0x000256BF
		public int NumberOfMembersWithConflict
		{
			get
			{
				return this.numberOfMembersWithConflictField;
			}
			set
			{
				this.numberOfMembersWithConflictField = value;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x000274C8 File Offset: 0x000256C8
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x000274D0 File Offset: 0x000256D0
		public int NumberOfMembersWithNoData
		{
			get
			{
				return this.numberOfMembersWithNoDataField;
			}
			set
			{
				this.numberOfMembersWithNoDataField = value;
			}
		}

		// Token: 0x04000FC5 RID: 4037
		private int numberOfMembersField;

		// Token: 0x04000FC6 RID: 4038
		private int numberOfMembersAvailableField;

		// Token: 0x04000FC7 RID: 4039
		private int numberOfMembersWithConflictField;

		// Token: 0x04000FC8 RID: 4040
		private int numberOfMembersWithNoDataField;
	}
}
