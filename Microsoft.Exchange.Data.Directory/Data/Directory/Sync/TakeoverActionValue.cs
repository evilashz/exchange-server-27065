using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000900 RID: 2304
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class TakeoverActionValue
	{
		// Token: 0x1700275D RID: 10077
		// (get) Token: 0x06006ED9 RID: 28377 RVA: 0x001766DB File Offset: 0x001748DB
		// (set) Token: 0x06006EDA RID: 28378 RVA: 0x001766E3 File Offset: 0x001748E3
		[XmlAttribute]
		public TakeoverActionType ActionType
		{
			get
			{
				return this.actionTypeField;
			}
			set
			{
				this.actionTypeField = value;
			}
		}

		// Token: 0x1700275E RID: 10078
		// (get) Token: 0x06006EDB RID: 28379 RVA: 0x001766EC File Offset: 0x001748EC
		// (set) Token: 0x06006EDC RID: 28380 RVA: 0x001766F4 File Offset: 0x001748F4
		[XmlAttribute]
		public DateTime ActionCreationTimestamp
		{
			get
			{
				return this.actionCreationTimestampField;
			}
			set
			{
				this.actionCreationTimestampField = value;
			}
		}

		// Token: 0x1700275F RID: 10079
		// (get) Token: 0x06006EDD RID: 28381 RVA: 0x001766FD File Offset: 0x001748FD
		// (set) Token: 0x06006EDE RID: 28382 RVA: 0x00176705 File Offset: 0x00174905
		[XmlAttribute]
		public string VerifiedDomain
		{
			get
			{
				return this.verifiedDomainField;
			}
			set
			{
				this.verifiedDomainField = value;
			}
		}

		// Token: 0x17002760 RID: 10080
		// (get) Token: 0x06006EDF RID: 28383 RVA: 0x0017670E File Offset: 0x0017490E
		// (set) Token: 0x06006EE0 RID: 28384 RVA: 0x00176716 File Offset: 0x00174916
		[XmlAttribute]
		public string SourceContextId
		{
			get
			{
				return this.sourceContextIdField;
			}
			set
			{
				this.sourceContextIdField = value;
			}
		}

		// Token: 0x17002761 RID: 10081
		// (get) Token: 0x06006EE1 RID: 28385 RVA: 0x0017671F File Offset: 0x0017491F
		// (set) Token: 0x06006EE2 RID: 28386 RVA: 0x00176727 File Offset: 0x00174927
		[XmlAttribute]
		public string TargetContextId
		{
			get
			{
				return this.targetContextIdField;
			}
			set
			{
				this.targetContextIdField = value;
			}
		}

		// Token: 0x17002762 RID: 10082
		// (get) Token: 0x06006EE3 RID: 28387 RVA: 0x00176730 File Offset: 0x00174930
		// (set) Token: 0x06006EE4 RID: 28388 RVA: 0x00176738 File Offset: 0x00174938
		[XmlAttribute(DataType = "nonNegativeInteger")]
		public string EncodingVersion
		{
			get
			{
				return this.encodingVersionField;
			}
			set
			{
				this.encodingVersionField = value;
			}
		}

		// Token: 0x17002763 RID: 10083
		// (get) Token: 0x06006EE5 RID: 28389 RVA: 0x00176741 File Offset: 0x00174941
		// (set) Token: 0x06006EE6 RID: 28390 RVA: 0x00176749 File Offset: 0x00174949
		[XmlAttribute(DataType = "nonNegativeInteger")]
		public string GroupingId
		{
			get
			{
				return this.groupingIdField;
			}
			set
			{
				this.groupingIdField = value;
			}
		}

		// Token: 0x17002764 RID: 10084
		// (get) Token: 0x06006EE7 RID: 28391 RVA: 0x00176752 File Offset: 0x00174952
		// (set) Token: 0x06006EE8 RID: 28392 RVA: 0x0017675A File Offset: 0x0017495A
		[XmlAttribute(DataType = "nonNegativeInteger")]
		public string UserCount
		{
			get
			{
				return this.userCountField;
			}
			set
			{
				this.userCountField = value;
			}
		}

		// Token: 0x17002765 RID: 10085
		// (get) Token: 0x06006EE9 RID: 28393 RVA: 0x00176763 File Offset: 0x00174963
		// (set) Token: 0x06006EEA RID: 28394 RVA: 0x0017676B File Offset: 0x0017496B
		[XmlAttribute]
		public TakeoverStatus Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x17002766 RID: 10086
		// (get) Token: 0x06006EEB RID: 28395 RVA: 0x00176774 File Offset: 0x00174974
		// (set) Token: 0x06006EEC RID: 28396 RVA: 0x0017677C File Offset: 0x0017497C
		[XmlIgnore]
		public bool StatusSpecified
		{
			get
			{
				return this.statusFieldSpecified;
			}
			set
			{
				this.statusFieldSpecified = value;
			}
		}

		// Token: 0x04004800 RID: 18432
		private TakeoverActionType actionTypeField;

		// Token: 0x04004801 RID: 18433
		private DateTime actionCreationTimestampField;

		// Token: 0x04004802 RID: 18434
		private string verifiedDomainField;

		// Token: 0x04004803 RID: 18435
		private string sourceContextIdField;

		// Token: 0x04004804 RID: 18436
		private string targetContextIdField;

		// Token: 0x04004805 RID: 18437
		private string encodingVersionField;

		// Token: 0x04004806 RID: 18438
		private string groupingIdField;

		// Token: 0x04004807 RID: 18439
		private string userCountField;

		// Token: 0x04004808 RID: 18440
		private TakeoverStatus statusField;

		// Token: 0x04004809 RID: 18441
		private bool statusFieldSpecified;
	}
}
